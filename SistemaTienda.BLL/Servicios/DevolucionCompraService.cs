using Microsoft.EntityFrameworkCore;
using SistemaTienda.API.Exceptions;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios
{
    public class DevolucionCompraService : IDevolucionCompraService
    {
        private readonly TiendaDbContext _tiendaDbContext;
        private readonly IGenericRepository<TbComDevolucionCompra> _devolucionRepository;
        private readonly IGenericRepository<TbComDetalleDevolucionCompra> _detalleDevolucionRepository;
        private readonly IMapeoDevolucionCompra _mapeo;

        public DevolucionCompraService(TiendaDbContext tiendaDbContext,
            IGenericRepository<TbComDevolucionCompra> devolucionRepository,
            IGenericRepository<TbComDetalleDevolucionCompra> detalleDevolucionRepository,
            IMapeoDevolucionCompra mapeo)
        {
            _tiendaDbContext = tiendaDbContext;
            _devolucionRepository = devolucionRepository;
            _detalleDevolucionRepository = detalleDevolucionRepository;
            _mapeo = mapeo;
        }

        public async Task<int> CrearDevolucionCompra(DevolucionCompraCreacionDTO dto)
        {
            if (dto == null || dto.IdCompra <= 0 || dto.IdTipoDevolucion <= 0 || dto.DetalleDevolucionCompraCreacionDTO == null || !dto.DetalleDevolucionCompraCreacionDTO.Any())
                throw new BadRequestException("Los datos de la devolución son inválidos");

            if (dto.DetalleDevolucionCompraCreacionDTO.Any(d => d.IdDetalleCompra <= 0 || d.Cantidad <= 0))
                throw new BadRequestException("Cada detalle debe tener una cantidad mayor que cero");

            // Validar existencia de compra
            var compra = await _tiendaDbContext.TbCompras.Where(c => c.Id == dto.IdCompra).FirstOrDefaultAsync();
            if (compra == null)
                throw new NotFoundException("No existe la compra indicada");

            var tipoDevolucion = await _tiendaDbContext.TbComTipoDevolucionCompras
                .FirstOrDefaultAsync(t => t.Id == dto.IdTipoDevolucion && t.Estado);
            if (tipoDevolucion == null)
                throw new BadRequestException("El tipo de devolución no existe o está inactivo");

            // Verificar que la compra no esté reversada
            var estadoReversada = await _tiendaDbContext.TbComEstadosCompras.FirstOrDefaultAsync(e => e.Nombre.ToLower() == "reversada");
            if (estadoReversada != null && compra.IdEstadoCompra == estadoReversada.Id)
                throw new ConflictException("La compra está reversada y no se puede realizar la devolución");

            using var transaction = await _tiendaDbContext.Database.BeginTransactionAsync();
            try
            {
                // Validar detalles y stock por lote
                foreach (var det in dto.DetalleDevolucionCompraCreacionDTO)
                {
                    var detalleCompra = await _tiendaDbContext.TbComDetallesCompras.Where(d => d.Id == det.IdDetalleCompra).FirstOrDefaultAsync();
                    if (detalleCompra == null)
                        throw new NotFoundException($"No existe el detalle de compra con id {det.IdDetalleCompra}");

                    if (detalleCompra.IdCompra != dto.IdCompra)
                        throw new BadRequestException($"El detalle de compra {det.IdDetalleCompra} no pertenece a la compra indicada");

                    // Buscar lote asociado (por articulo + numero lote)
                    var lote = await _tiendaDbContext.TbInvLotes.Where(l => l.IdArticulo == detalleCompra.IdArticulo && (l.NumeroLote == detalleCompra.NumeroLote || string.IsNullOrEmpty(detalleCompra.NumeroLote))).FirstOrDefaultAsync();
                    if (lote == null)
                        throw new NotFoundException($"No existe lote asociado para el detalle de compra {detalleCompra.Id}");

                    // El lote debe tener stock positivo y suficiente
                    if (lote.StockDisponible <= 0)
                        throw new ConflictException($"El lote asociado al detalle de compra {detalleCompra.Id} tiene stock cero o negativo y no se puede devolver");

                    if (det.Cantidad > lote.StockDisponible)
                        throw new ConflictException($"La cantidad a devolver ({det.Cantidad}) excede el stock disponible del lote ({lote.StockDisponible})");
                }

                // Crear entidad devolucion (TbComDevolucionCompra + detalles) y obtener IDs generados
                var tbDevolucion = await CrearEntidadDevolucionAsync(dto);

                if (tipoDevolucion.Nombre.Equals("CREDITO", StringComparison.OrdinalIgnoreCase))
                    await GenerarCreditoProveedorAsync(tbDevolucion);

                // Crear movimiento y registros en TbInvDevolucionLote (reduce stock)
                await CrearMovimientoYDevolucionLotesAsync(tbDevolucion);

                await transaction.CommitAsync();
                return tbDevolucion.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task ReversarDevolucionCompra(int id)
        {
            if (id <= 0)
                throw new BadRequestException("La devolución indicada no es válida");

            await using var transaction = await _tiendaDbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var devolucion = await _tiendaDbContext.TbComDevolucionCompras
                    .Include(d => d.IdTipoDevolucionNavigation)
                    .Include(d => d.TbComCreditoProveedor)
                    .ThenInclude(c => c!.TbComCreditoProveedorMovimientos)
                            .ThenInclude(m => m.IdTipoMovimientoNavigation)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (devolucion == null)
                    throw new NotFoundException("No existe la devolución indicada");

                if (devolucion.FechaReversion.HasValue)
                    throw new ConflictException("La devolución ya fue reversada");

                if (devolucion.IdTipoDevolucionNavigation.Nombre.Equals("CREDITO", StringComparison.OrdinalIgnoreCase))
                {
                    var credito = devolucion.TbComCreditoProveedor;
                    if (credito == null)
                        throw new NotFoundException("No existe el crédito generado para la devolución");

                    var usoActivo = credito.TbComCreditoProveedorMovimientos.Any(m =>
                        m.Estado && m.IdTipoMovimientoNavigation.Nombre.Equals("USO", StringComparison.OrdinalIgnoreCase));
                    if (usoActivo)
                        throw new ConflictException("No se puede reversar la devolución porque el crédito generado ya fue aplicado a una o varias compras.");

                    credito.Estado = false;
                    credito.ValorDisponible = 0;
                }

                devolucion.Estado = 0;
                devolucion.FechaReversion = DateTime.Now;
                await _tiendaDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<TbComDevolucionCompra> CrearEntidadDevolucionAsync(DevolucionCompraCreacionDTO dto)
        {
            var tbDevolucion = _mapeo.MapeoDevolucionCompraCreacionDtoATb(dto);
            tbDevolucion.FechaCreacion = DateTime.Now;
            _tiendaDbContext.TbComDevolucionCompras.Add(tbDevolucion);
            await _tiendaDbContext.SaveChangesAsync();
            return tbDevolucion;
        }

        private async Task<decimal> CalcularValorCreditoAsync(TbComDevolucionCompra devolucion)
        {
            var idsDetalle = devolucion.TbComDetalleDevolucionCompras
                .Select(d => d.IdDetalleCompra)
                .ToList();

            var detallesCompra = await _tiendaDbContext.TbComDetallesCompras
                .Where(d => idsDetalle.Contains(d.Id))
                .Include(d => d.TbComImpuestoDetalles)
                .ToListAsync();

            var detallesPorId = detallesCompra.ToDictionary(d => d.Id);
            decimal total = 0m;

            foreach (var detalleDevolucion in devolucion.TbComDetalleDevolucionCompras)
            {
                if (!detallesPorId.TryGetValue(detalleDevolucion.IdDetalleCompra, out var detalleCompra))
                    throw new NotFoundException($"No existe el detalle de compra original con id {detalleDevolucion.IdDetalleCompra}");

                var valor = detalleDevolucion.Cantidad * detalleCompra.ValorCompra;
                var valorUnidad = valor;

                foreach (var impuesto in detalleCompra.TbComImpuestoDetalles)
                {
                    if (impuesto.TipoCalculo.Equals("UNIDAD", StringComparison.OrdinalIgnoreCase))
                    {
                        valor += detalleDevolucion.Cantidad * impuesto.Valor;
                    }
                    else if (impuesto.TipoCalculo.Equals("PORCENTAJE", StringComparison.OrdinalIgnoreCase))
                    {
                        valor += valorUnidad * impuesto.Valor;
                    }
                    else
                    {
                        throw new BadRequestException($"El tipo de cálculo de impuesto '{impuesto.TipoCalculo}' no es válido");
                    }
                }

                total += valor;
            }

            return decimal.Round(total, 4, MidpointRounding.AwayFromZero);
        }

        private async Task GenerarCreditoProveedorAsync(TbComDevolucionCompra devolucion)
        {
            var creditoExistente = await _tiendaDbContext.TbComCreditoProveedors
                .AnyAsync(c => c.IdDevolucionCompra == devolucion.Id);
            if (creditoExistente)
                throw new ConflictException("La devolución ya tiene un crédito generado");

            var valor = await CalcularValorCreditoAsync(devolucion);
            if (valor <= 0)
                throw new BadRequestException("El valor del crédito debe ser mayor que cero");

            var credito = new TbComCreditoProveedor
            {
                IdDevolucionCompra = devolucion.Id,
                ValorOriginal = valor,
                ValorDisponible = valor,
                FechaCreacion = DateTime.Now,
                Estado = true
            };
            _tiendaDbContext.TbComCreditoProveedors.Add(credito);
            await _tiendaDbContext.SaveChangesAsync();
        }

        private async Task CrearMovimientoYDevolucionLotesAsync(TbComDevolucionCompra devolucion)
        {
            // Obtener transaccion inventario para devolucion
            var trans = await _tiendaDbContext.TbInvTransacciones.FirstOrDefaultAsync(t => t.Nombre.ToLower() == "devolucion compra");
            if (trans == null)
                throw new NotFoundException("No existe la transacción 'Devolucion Compra' en TbInvTransacciones");

            var movimiento = new TbInvMovimiento
            {
                IdTransaccionInventario = trans.Id,
                Referencia = devolucion.Id.ToString(),
                Fecha = DateTime.Now
            };
            _tiendaDbContext.TbInvMovimientos.Add(movimiento);
            await _tiendaDbContext.SaveChangesAsync();

            // Por cada detalle de la devolucion, crear TbInvDevolucionLote y reducir stock del lote
            foreach (var detalleDevolucion in devolucion.TbComDetalleDevolucionCompras)
            {
                // Obtener detalle de compra original
                var detalleCompra = await _tiendaDbContext.TbComDetallesCompras.FirstOrDefaultAsync(d => d.Id == detalleDevolucion.IdDetalleCompra);
                if (detalleCompra == null)
                    throw new NotFoundException($"No existe el detalle de compra original con id {detalleDevolucion.IdDetalleCompra}");

                // Buscar lote asociado
                var lote = await _tiendaDbContext.TbInvLotes.FirstOrDefaultAsync(l => l.IdArticulo == detalleCompra.IdArticulo && (l.NumeroLote == detalleCompra.NumeroLote || string.IsNullOrEmpty(detalleCompra.NumeroLote)));
                if (lote == null)
                    throw new NotFoundException($"No existe lote asociado para el detalle de compra {detalleCompra.Id}");

                var cantidad = (decimal)detalleDevolucion.Cantidad;

                if (cantidad > lote.StockDisponible)
                    throw new ConflictException($"La cantidad a devolver ({cantidad}) excede el stock disponible del lote ({lote.StockDisponible})");

                var devolucionLote = new TbInvDevolucionLote
                {
                    IdMovimiento = movimiento.Id,
                    IdDetalleDevolucionCompra = detalleDevolucion.Id,
                    IdLote = lote.Id,
                    Cantidad = cantidad,
                    CostoUnitario = lote.CostoUnitario,
                    Estado = true
                };
                _tiendaDbContext.TbInvDevolucionLotes.Add(devolucionLote);

                // Reducir stock disponible del lote
                lote.StockDisponible = lote.StockDisponible - cantidad;
                _tiendaDbContext.TbInvLotes.Update(lote);
            }

            await _tiendaDbContext.SaveChangesAsync();
        }

        public async Task<List<TipoDevolucionDTO>> ListaTiposDevolucion()
        {
            var TiposDevoluciones = await _tiendaDbContext.TbComTipoDevolucionCompras.Where(t => t.Estado == true && t.EstadoVisual == true).ToListAsync();
            return TiposDevoluciones.Select(t => new TipoDevolucionDTO
            {
                Id = t.Id,
                Nombre = t.Nombre
            }).ToList();
        }
        public async Task<List<DevolucionCompraDTO>> ListarDevolucionesFechas(DateOnly fechaInicio, DateOnly fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new BadRequestException("La fecha inicial no puede ser mayor que la fecha final");

            var inicio = fechaInicio.ToDateTime(TimeOnly.MinValue);
            var fin = fechaFin.ToDateTime(TimeOnly.MaxValue);

            var devoluciones = await _tiendaDbContext.TbComDevolucionCompras
                .AsNoTracking()
                .Where(d => d.FechaCreacion >= inicio && d.FechaCreacion <= fin)
                .Include(d => d.IdTipoDevolucionNavigation)
                .Include(d => d.IdCompraNavigation)
                    .ThenInclude(c => c.IdProveedorNavigation)
                        .ThenInclude(p => p.IdPersonaNavigation)
                .Include(d => d.IdCompraNavigation)
                    .ThenInclude(c => c.IdEstadoCompraNavigation)
                .Include(d => d.IdCompraNavigation)
                    .ThenInclude(c => c.IdUsuarioCreadorNavigation)
                        .ThenInclude(u => u.IdPersonaNavigation)
                .OrderByDescending(d => d.FechaCreacion)
                .ToListAsync();

            return _mapeo.MapeoListaDevolucionesCompraTbADto(devoluciones);
        }

        public async Task<CreditoProveedorDTO> ObtenerCreditoPorDevolucion(int idDevolucion)
        {
            if (idDevolucion <= 0)
                throw new BadRequestException("La devolución indicada no es válida");

            var devolucion = await _tiendaDbContext.TbComDevolucionCompras
                .AsNoTracking()
                .Include(d => d.IdCompraNavigation)
                    .ThenInclude(c => c.IdProveedorNavigation)
                .Include(d => d.TbComCreditoProveedor)
                    .ThenInclude(c => c!.TbComCreditoProveedorMovimientos)
                        .ThenInclude(m => m.IdTipoMovimientoNavigation)
                .FirstOrDefaultAsync(d => d.Id == idDevolucion);

            if (devolucion == null)
                throw new NotFoundException("No existe la devolución indicada");

            var credito = devolucion.TbComCreditoProveedor;
            if (credito == null)
                throw new NotFoundException("No existe un crédito asociado a la devolución indicada");

            return new CreditoProveedorDTO
            {
                Id = credito.Id,
                IdDevolucionCompra = credito.IdDevolucionCompra,
                IdProveedor = devolucion.IdCompraNavigation.IdProveedor,
                Proveedor = devolucion.IdCompraNavigation.IdProveedorNavigation.RazonSocial,
                ValorOriginal = credito.ValorOriginal,
                ValorDisponible = credito.ValorDisponible,
                FechaCreacion = credito.FechaCreacion,
                Detalles = [],
                Estado = credito.Estado,
                Movimientos = credito.TbComCreditoProveedorMovimientos
                    .Select(m => new CreditoProveedorMovimientoDTO
                    {
                        Id = m.Id,
                        IdCreditoProveedor = m.IdCreditoProveedor,
                        IdCompra = m.IdCompra,
                        IdTipoMovimiento = m.IdTipoMovimiento,
                        TipoMovimiento = m.IdTipoMovimientoNavigation.Nombre,
                        Valor = m.Valor,
                        FechaCreacion = m.FechaCreacion,
                        Observacion = m.Observacion,
                        Estado = m.Estado
                    })
                    .ToList()
            };
        }

        public async Task<List<DetalleDevolucionCompraDTO>> ListarDetallesDevolucion(int idDevolucionCompra)
        {
            var detalles = await _tiendaDbContext.TbComDetalleDevolucionCompras
                .AsNoTracking()
                .Where(d => d.IdDevolucionCompra == idDevolucionCompra)
                .Include(d => d.IdDetalleCompraNavigation)
                    .ThenInclude(dc => dc.IdArticuloNavigation)
                .Include(d => d.IdDetalleCompraNavigation)
                    .ThenInclude(dc => dc.TbComImpuestoDetalles)
                .ToListAsync();
            if(detalles == null)
                throw new NotFoundException("No se encontraron detalles para la devolución indicada");


            return this._mapeo.MapeoListaDetallesDevolucionTbADto(detalles);
        }
    }
}
