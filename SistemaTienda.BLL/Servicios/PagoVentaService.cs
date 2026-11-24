using Microsoft.EntityFrameworkCore;
using SistemaTienda.API.Exceptions;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;

namespace SistemaTienda.BLL.Servicios
{
    public class PagoVentaService : IPagoVentaService
    {
        private readonly TiendaDbContext _context;
        private readonly IMapeos _mapper;

        public PagoVentaService(TiendaDbContext context, IMapeos mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagosVentasDTO> PagoVenta(int idVenta)
        {
            var pago = await QueryPagos()
                .Where(pag => pag.IdVenta == idVenta)
                .FirstOrDefaultAsync();

            if (pago == null)
                throw new NotFoundException("Pago de venta no encontrado");

            return MapPago(pago);
        }

        public async Task<int> CrearDetallePago(DetallePagoCreacionDTO detallePagoCrear)
        {
            if (detallePagoCrear.Valor <= 0)
                throw new ConflictException("El valor del detalle de pago debe ser mayor que cero");

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pago = await _context.TbVenPagoVenta
                    .FirstOrDefaultAsync(p => p.Id == detallePagoCrear.IdPagoVenta);
                if (pago == null)
                    throw new NotFoundException("Pago de venta no encontrado");

                var tipoPagoExiste = await _context.TbVenTipoPagos
                    .AnyAsync(t => t.Id == detallePagoCrear.IdTipoPago && t.Estado && t.EstadiVisual);
                if (!tipoPagoExiste)
                    throw new NotFoundException("Tipo de pago no encontrado o no disponible");

                var totalPagadoActual = await _context.TbVenDetallePagos
                    .Where(d => d.IdPagoVenta == pago.Id && d.Estado)
                    .SumAsync(d => (decimal?)d.Valor) ?? 0m;
                if (totalPagadoActual + detallePagoCrear.Valor > pago.TotalVenta)
                    throw new ConflictException("El total pagado no puede superar el total de la venta");

                var ahora = DateTime.Now;
                var detalle = new TbVenDetallePago
                {
                    IdPagoVenta = pago.Id,
                    IdTipoPago = detallePagoCrear.IdTipoPago,
                    Valor = detallePagoCrear.Valor,
                    NumeroComprobante = detallePagoCrear.NumeroComprobante,
                    FechaPago = detallePagoCrear.FechaPago == default ? ahora : detallePagoCrear.FechaPago,
                    Nota = detallePagoCrear.Nota,
                    Estado = true
                };

                _context.TbVenDetallePagos.Add(detalle);
                await _context.SaveChangesAsync();
                await RecalcularPagoAsync(pago);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return detalle.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<EstadosPagoDTO>> ListarEstadosPago()
        {
            return await _context.TbVentEstadosPagos
                .Where(e => e.Estado && e.EstadoVisual)
                .OrderBy(e => e.Nombre)
                .Select(e => new EstadosPagoDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre
                })
                .ToListAsync();
        }

        public async Task<List<TipoPagoDTO>> ListarTiposPago()
        {
            return await _context.TbVenTipoPagos
                .Where(t => t.Estado && t.EstadiVisual)
                .OrderBy(t => t.Nombre)
                .Select(t => new TipoPagoDTO
                {
                    Id = t.Id,
                    Nombre = t.Nombre
                })
                .ToListAsync();
        }

        public async Task<List<PagosVentasDTO>> ListarPagos(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            if (fechaFinal < fechaInicial)
                throw new ConflictException("El rango de fechas no es válido");

            var inicio = fechaInicial.ToDateTime(TimeOnly.MinValue);
            var fin = fechaFinal.ToDateTime(TimeOnly.MaxValue);
            var pagos = await QueryPagos()
                .Where(p => p.FechaCreacion >= inicio && p.FechaCreacion <= fin)
                .OrderByDescending(p => p.FechaCreacion)
                .ToListAsync();

            return pagos.Select(MapPago).ToList();
        }

        public async Task<List<DetallesPagoDTO>> ListaDetallesPagos(int idPago)
        {
            var existe = await _context.TbVenPagoVenta.AnyAsync(p => p.Id == idPago);
            if (!existe)
                throw new NotFoundException("Pago de venta no encontrado");

            var detalles = await _context.TbVenDetallePagos
                .Include(d => d.IdTipoPagoNavigation)
                .Where(d => d.IdPagoVenta == idPago)
                .OrderByDescending(d => d.FechaPago)
                .ToListAsync();

            return detalles.Select(MapDetalle).ToList();
        }

        public async Task<bool> AnularPago(int idDetallePago)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var detalle = await _context.TbVenDetallePagos
                    .Include(d => d.IdPagoVentaNavigation)
                    .FirstOrDefaultAsync(d => d.Id == idDetallePago);
                if (detalle == null)
                    throw new NotFoundException("Detalle de pago no encontrado");
                if (!detalle.Estado)
                    throw new ConflictException("El detalle de pago ya está anulado");

                detalle.Estado = false;
                var pago = detalle.IdPagoVentaNavigation;
                await RecalcularPagoAsync(pago);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ActualizarComprobantePago(ActualizarComprobantePagoDTO comprobanteDto)
        {
            if (string.IsNullOrWhiteSpace(comprobanteDto.NumeroComprobante))
                throw new ConflictException("El número de comprobante es obligatorio");

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var detalle = await _context.TbVenDetallePagos
                    .FirstOrDefaultAsync(d => d.Id == comprobanteDto.IdDetallePago);
                if (detalle == null)
                    throw new NotFoundException("Detalle de pago no encontrado");
                if (!detalle.Estado)
                    throw new ConflictException("No se puede actualizar un detalle de pago anulado");

                detalle.NumeroComprobante = comprobanteDto.NumeroComprobante.Trim();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private IQueryable<TbVenPagoVenta> QueryPagos()
        {
            return _context.TbVenPagoVenta
                .Include(p => p.IdEstadoPagoNavigation)
                .Include(p => p.TbVenDetallePagos)
                    .ThenInclude(d => d.IdTipoPagoNavigation)
                .Include(p => p.IdVentaNavigation)
                    .ThenInclude(v => v.IdEstadoVentaNavigation)
                .Include(p => p.IdVentaNavigation)
                    .ThenInclude(v => v.IdClienteNavigation)
                        .ThenInclude(c => c.IdPersonaNavigation)
                            .ThenInclude(persona => persona.TbGrlDireccione)
                                .ThenInclude(direccion => direccion.IdCiudadNavigation)
                .Include(p => p.IdVentaNavigation)
                    .ThenInclude(v => v.IdUsuarioCreadorNavigation)
                        .ThenInclude(usuario => usuario.IdPersonaNavigation)
                .Include(p => p.IdVentaNavigation)
                    .ThenInclude(v => v.TbVenDetalleVenta)
                        .ThenInclude(d => d.IdArticuloNavigation)
                            .ThenInclude(a => a.IdUnidadNavigation)
                .Include(p => p.IdVentaNavigation)
                    .ThenInclude(v => v.TbVenDetalleVenta)
                        .ThenInclude(d => d.IdArticuloNavigation)
                            .ThenInclude(a => a.IdTipoArticuloNavigation)
                .Include(p => p.IdVentaNavigation)
                    .ThenInclude(v => v.TbVenDetalleVenta)
                        .ThenInclude(d => d.IdArticuloNavigation)
                            .ThenInclude(a => a.IdMarcaNavigation);
        }

        private PagosVentasDTO MapPago(TbVenPagoVenta pago)
        {
            return new PagosVentasDTO
            {
                Id = pago.Id,
                TotalVenta = pago.TotalVenta,
                TotalPagado = pago.TotalPagado,
                SaldoPendiente = pago.SaldoPendiente,
                IdEstadoPago = pago.IdEstadoPago,
                Nota = pago.Nota,
                FechaCreacion = pago.FechaCreacion,
                FechaPago = pago.FechaPago,
                EstadosPagoDTO = new EstadosPagoDTO
                {
                    Id = pago.IdEstadoPagoNavigation.Id,
                    Nombre = pago.IdEstadoPagoNavigation.Nombre
                },
                VentaDTO = _mapper.MapeoVentaTbAVentaCompletaDto(pago.IdVentaNavigation),
                DetallesPago = pago.TbVenDetallePagos.Select(MapDetalle).Cast<DetallePagoCreacionDTO>().ToList()
            }; 
        }

        private DetallesPagoDTO MapDetalle(TbVenDetallePago detalle)
        {
            return new DetallesPagoDTO
            {
                Id = detalle.Id,
                IdPagoVenta = detalle.IdPagoVenta,
                IdTipoPago = detalle.IdTipoPago,
                Valor = detalle.Valor,
                NumeroComprobante = detalle.NumeroComprobante,
                FechaPago = detalle.FechaPago,
                Nota = detalle.Nota,
                Estado = detalle.Estado,
                TipoPagoDTO = new TipoPagoDTO
                {
                    Id = detalle.IdTipoPagoNavigation.Id,
                    Nombre = detalle.IdTipoPagoNavigation.Nombre
                }
            };
        }

        private async Task<int> ObtenerIdEstadoAsync(string nombre)
        {
            var estado = await _context.TbVentEstadosPagos
                .FirstOrDefaultAsync(e => e.Estado && e.Nombre.ToLower() == nombre);
            if (estado == null)
                throw new NotFoundException($"No existe el estado de pago '{nombre}'");
            return estado.Id;
        }

        private async Task RecalcularPagoAsync(TbVenPagoVenta pago)
        {
            var detallesActivos = await _context.TbVenDetallePagos
                .Where(d => d.IdPagoVenta == pago.Id && d.Estado)
                .OrderByDescending(d => d.FechaPago)
                .ToListAsync();

            var totalPagado = detallesActivos.Sum(d => d.Valor);
            pago.TotalPagado = totalPagado;
            pago.SaldoPendiente = pago.TotalVenta - totalPagado;

            var nombreEstado = totalPagado == 0m
                ? "pendiente"
                : totalPagado >= pago.TotalVenta
                    ? "pagado"
                    : "parcial";

            pago.IdEstadoPago = await ObtenerIdEstadoAsync(nombreEstado);
            pago.FechaPago = totalPagado >= pago.TotalVenta
                ? detallesActivos.FirstOrDefault()?.FechaPago
                : null;
        }
    }
}
