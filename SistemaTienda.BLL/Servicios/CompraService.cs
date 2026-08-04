using Microsoft.EntityFrameworkCore;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios
{
    public class CompraService : ICompraServicio
    {
        private readonly TiendaDbContext _tiendaDbContext;
        public readonly IGenericRepository<TbCompra> _compraRepository;
        public readonly IGenericRepository<TbInvMovimiento> _movInventarioRepository;
        public readonly IGenericRepository<TbComDetallesCompra> _detalleRepository;
        private readonly IMapeos _mapper;
        public CompraService(TiendaDbContext tiendaDbContext, IGenericRepository<TbCompra> compraRepository, IGenericRepository<TbComDetallesCompra> detalleRepository, IGenericRepository<TbInvMovimiento> movInventarioRepository, IMapeos mapper)
        {
            _tiendaDbContext = tiendaDbContext;
            _compraRepository = compraRepository;
            _detalleRepository = detalleRepository;
            _movInventarioRepository = movInventarioRepository;
            _mapper = mapper;
        }



        private async Task ProcesarMovimientoInventario(TbInvMovimiento movimientoOrigen, TbCompra tbCompra)
        {
            // Crear diccionario de lotes existentes por llave (IdArticulo, NumeroLote, Codigo)
            var lotesExistentes = movimientoOrigen.TbInvLotes.ToDictionary(l => (l.IdArticulo, l.NumeroLote ?? string.Empty, l.Codigo ?? string.Empty), l => l);

            // Para cada detalle actual en la compra, actualizar o crear lote en el movimientoOrigen
            foreach (var detalle in tbCompra.TbComDetallesCompras)
            {
                var key = (detalle.IdArticulo, detalle.NumeroLote ?? string.Empty, detalle.Codigo ?? string.Empty);
                if (lotesExistentes.TryGetValue(key, out var lote))
                {
                    // Ajustar cantidad disponible al nuevo valor
                    lote.StockDisponible = detalle.Cantidad;
                    lote.CostoUnitario = detalle.ValorCompra;
                    lote.FechaExpiracion = detalle.FechaExpiracion;
                    _tiendaDbContext.TbInvLotes.Update(lote);
                    lotesExistentes.Remove(key);
                }
                else
                {
                    // Crear nuevo lote asociado al movimiento
                    var nuevoLote = new TbInvLote
                    {
                        NumeroLote = detalle.NumeroLote,
                        CostoUnitario = detalle.ValorCompra,
                        IdArticulo = detalle.IdArticulo,
                        Codigo = detalle.Codigo,
                        FechaIngreso = DateTime.Now,
                        FechaExpiracion = detalle.FechaExpiracion,
                        StockDisponible = detalle.Cantidad,
                        StockMinimo = 6,
                        Estado = true,
                        // Relacionar con movimiento
                    };
                    movimientoOrigen.TbInvLotes.Add(nuevoLote);
                    _tiendaDbContext.TbInvLotes.Add(nuevoLote);
                }
            }

            // Los lotes que queden en lotesExistentes ya no existen en la compra: eliminarlos
            foreach (var loteRemover in lotesExistentes.Values)
            {
                _tiendaDbContext.TbInvLotes.Remove(loteRemover);
            }

            // Actualizar referencia y fecha del movimiento de inventario (usar formato Documento+"ID"+IdCompra)
            movimientoOrigen.Referencia = tbCompra.Documento + "ID" + tbCompra.Id;
            movimientoOrigen.Fecha = DateTime.Now;
            _tiendaDbContext.TbInvMovimientos.Update(movimientoOrigen);

            await Task.CompletedTask;
        }

        public async Task<List<EstadoCompraDTO>> ListarEstadosCompras()
        {
            List<TbComEstadosCompra> tbComEstadosCompras = await this._tiendaDbContext.TbComEstadosCompras.Where(est => est.EstadoVisual == true).ToListAsync();
            try
            {
                
                var listaComprasDto = this._mapper.MapeoListaEstadosCompraTbaAListaEstadosCompraDto(tbComEstadosCompras);
                return listaComprasDto;
            }
            catch
            {
                throw;
            }
        }

       
        public async Task<List<CompraMinDTO>> ListarCompras(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            var inicio = fechaInicial.ToDateTime(TimeOnly.MinValue);
            var fin = fechaFinal.ToDateTime(TimeOnly.MaxValue);
            
            try
            {
                var tbCompras = await this._tiendaDbContext.TbCompras.Where(c => c.FechaCompra >= inicio && c.FechaCompra <= fin)
                    .Include(u => u.IdUsuarioCreadorNavigation)
                    .ThenInclude(per => per.IdPersonaNavigation)
                    .Include(e => e.IdEstadoCompraNavigation)
                    .Include(p => p.IdProveedorNavigation)
                    .ThenInclude(per => per.IdPersonaNavigation)
                    .ToListAsync();

                return this._mapper.MapeoListaCompraTbAListaCompraDto(tbCompras);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CompraDTO> ObtenerCompra(int idCompra)
        {   
            try
            {
                TbCompra tbCompra = await this._tiendaDbContext.TbCompras
                    .Include(u => u.IdUsuarioCreadorNavigation)
                    .ThenInclude(p => p.IdPersonaNavigation)
                    .ThenInclude(id => id.IdTipoIdentificacionNavigation)
                    .Include(prov => prov.IdProveedorNavigation)
                    .ThenInclude(per => per.IdPersonaNavigation)
                    .ThenInclude(dir => dir.TbGrlDireccione)
                        .ThenInclude(ciu => ciu.IdCiudadNavigation)
                    .Include(est => est.IdEstadoCompraNavigation)
                    .Include(art => art.TbComDetallesCompras)
                        .ThenInclude(art => art.IdArticuloNavigation)
                    .Include(det => det.TbComDetallesCompras)
                        .ThenInclude(art => art.IdArticuloNavigation)
                            .ThenInclude(imp => imp.IdImpuestoNavigation)
                    .Include(det => det.TbComDetallesCompras)
                        .ThenInclude(art=> art.IdArticuloNavigation)
                            .ThenInclude(mar => mar.IdMarcaNavigation)
                    .Include(det => det.TbComDetallesCompras)
                        .ThenInclude(art => art.IdArticuloNavigation)
                            .ThenInclude(tp => tp.IdTipoArticuloNavigation)
                    .FirstOrDefaultAsync(c => c.Id == idCompra);

                if (tbCompra == null)
                    throw new Exception("No se encontró la compra");
                
                var compraDto = this._mapper.MapeoCompraTbACompraCompletaDto(tbCompra);
                return compraDto;
            }
            catch {
                throw;
            }
        }

        public async Task<int> RegistrarCompra(CompraCreacionDTO compraDto)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var tbCompra = this._mapper.MapeoCompraCreacionDtoACompraTb(compraDto);
                    await this._compraRepository.Crear(tbCompra);
                    if (tbCompra.Id == 0)
                        throw new Exception("No se pudo registrar la compra");
                    tbCompra = await this._tiendaDbContext.TbCompras.Where(c => c.Id == tbCompra.Id)
                        .Include(det => det.TbComDetallesCompras)
                        .ThenInclude(art => art.IdArticuloNavigation).FirstOrDefaultAsync();
                    
                    var respInv = await this.AlimentarInventario(tbCompra);
                    if (respInv == false)
                        throw new Exception("No se pudo actualizar el inventario");
                    transaction.Commit();
                    return tbCompra.Id;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Nueva sobrecarga: editar movimiento/lotes por idCompra y documento (referencia)
        
        private async Task<bool> AlimentarInventario(TbCompra tbCompra) {
            var tbinvMovimiento = new TbInvMovimiento
            {
                Referencia = tbCompra.Documento + "ID" + tbCompra.Id,
                Fecha = DateTime.Now,
                IdTransaccionInventario = tbCompra.IdTransaccion,
                TbInvLotes = tbCompra.TbComDetallesCompras.Select(det => new TbInvLote
                {
                    NumeroLote = det.NumeroLote,
                    CostoUnitario = det.ValorCompra,
                    IdArticulo = det.IdArticulo,
                    Codigo = det.Codigo,
                    FechaIngreso = DateTime.Now,
                    FechaExpiracion = det.FechaExpiracion,
                    StockDisponible = det.Cantidad,
                    StockMinimo = 6,
                    Estado = true
                }).ToList()
            };
            await this._movInventarioRepository.Crear(tbinvMovimiento);
            return true;
        }

        public async Task<bool> ReversarCompra(int id)
        {
            // Paso 1: validaciones y búsquedas iniciales
            var tbCompra = await this._tiendaDbContext.TbCompras.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (tbCompra is null)
                throw new Exception("No existe una compra con el id indicado!!!!");
            if (string.IsNullOrWhiteSpace(tbCompra.Documento))
                throw new Exception("La compra no tiene documento válido");

            // Obtener id de estado 'Reversada'
            var estadoReversada = await _tiendaDbContext.TbComEstadosCompras.FirstOrDefaultAsync(e => e.Nombre.ToLower() == "reversada");
            if (estadoReversada == null)
                throw new Exception("No existe el estado 'Reversada' en TbComEstadosCompras");

            // Si la compra ya está en estado reversada, no permitir doble reversión
            if (tbCompra.IdEstadoCompra == estadoReversada.Id)
                throw new Exception("Compra ya reversada");

            // Obtener transacciones necesarias
            var transReversar = await _tiendaDbContext.TbInvTransacciones.FirstOrDefaultAsync(t => t.Nombre.ToLower() == "reversion compra");
            if (transReversar == null)
                throw new Exception("No existe la transacción 'Reversar Compra'");

            var transReversion = await _tiendaDbContext.TbInvTransacciones.FirstOrDefaultAsync(t => t.Nombre.ToLower() == "reversion compra");
            if (transReversion == null)
                throw new Exception("No existe la transacción 'Reversion compra'");

            // Buscar movimientos asociados por referencia (documento)
            var movimientos = await _tiendaDbContext.TbInvMovimientos
                .Where(m => m.Referencia == tbCompra.Documento)
                .Include(m => m.TbInvLotes)
                    .ThenInclude(l => l.TbInvConsumoLotes)
                .Include(m => m.TbInvConsumoLotes)
                .ToListAsync();

            if (movimientos == null || !movimientos.Any())
                throw new Exception("No se encontró movimiento origen para la compra");

            // Seleccionar el movimiento origen: preferir aquel cuyo IdTransaccionInventario == tbCompra.IdTransaccion si existe
            var movimientoOrigen = movimientos.FirstOrDefault(m => m.IdTransaccionInventario == tbCompra.IdTransaccion)
                ?? movimientos.First();

            // Verificar que no existan consumos asociados a los lotes de la compra
            var lotesConConsumo = movimientoOrigen.TbInvLotes.Any(l => l.TbInvConsumoLotes != null && l.TbInvConsumoLotes.Any());
            if (lotesConConsumo)
                throw new Exception("La compra tiene movimientos de consumo en sus lotes y no se puede reversar");

            // Verificar que no exista ya una reversión para esta referencia
            var existeReversionPrev = movimientos.Any(m => m.IdTransaccionInventario == transReversion.Id || m.IdTransaccionInventario == transReversar.Id);
            if (existeReversionPrev)
                throw new Exception("Compra ya tiene reversiones o movimientos relacionados y no puede reversarse");

            // Iniciar transacción
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    // Procesar cambios de inventario en método separado
                    var movimientoReversion = await ProcesarReversionInventarioAsync(movimientoOrigen, tbCompra.Documento, transReversar, transReversion);

                    // Actualizar estado de la compra a 'Reversada' (acción relacionada con la compra)
                    tbCompra.IdEstadoCompra = estadoReversada.Id;
                    tbCompra.FechaModificacion = DateTime.Now;
                    tbCompra.IdTransaccion = transReversion.Id;
                    _tiendaDbContext.TbCompras.Update(tbCompra);

                    await _tiendaDbContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Procesa la reversión en inventario: crea el movimiento de reversión (sin lotes), marca lotes y consumos del movimiento origen
        /// y actualiza el IdTransaccionInventario del movimiento origen a la transacción de reversión.
        /// Esta operación asume que la transacción externa ya fue iniciada y la entidad movimientoOrigen ya fue cargada con sus relaciones.
        /// </summary>
        private async Task<TbInvMovimiento> ProcesarReversionInventarioAsync(TbInvMovimiento movimientoOrigen, string referenciaCompra, TbInvTransacciones transReversar, TbInvTransacciones transReversion)
        {
            // Crear nuevo movimiento de reversión sin crear lotes
            var movimientoReversion = new TbInvMovimiento
            {
                IdMovimientoOrigen = movimientoOrigen.Id,
                IdTransaccionInventario = transReversar.Id,
                Referencia = $"Reversión compra {referenciaCompra} (mov {movimientoOrigen.Id})",
                Fecha = DateTime.Now
            };
            _tiendaDbContext.TbInvMovimientos.Add(movimientoReversion);
            await _tiendaDbContext.SaveChangesAsync();

            // Marcar lotes del movimiento origen como Estado = false (si el campo existe)
            foreach (var lote in movimientoOrigen.TbInvLotes)
            {
                lote.Estado = false;
                _tiendaDbContext.TbInvLotes.Update(lote);
            }

            // Marcar consumos relacionados (si existieran) como reversados
            foreach (var consumo in movimientoOrigen.TbInvConsumoLotes)
            {
                consumo.Estado = false;
                _tiendaDbContext.TbInvConsumoLotes.Update(consumo);
            }

            // Cambiar el IdTransaccionInventario del movimiento origen a la transacción de 'Reversion compra'
            movimientoOrigen.IdTransaccionInventario = transReversion.Id;
            _tiendaDbContext.TbInvMovimientos.Update(movimientoOrigen);

            return movimientoReversion;
        }

        public async Task<bool> EditarCompra(CompraEditarDTO compraEditarDTO)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var tbCompra = await this._tiendaDbContext.TbCompras.Where(c => c.Id == compraEditarDTO.Id)
                     .Include(det => det.TbComDetallesCompras)
                     .FirstOrDefaultAsync();

                    if (tbCompra == null)
                        throw new Exception("No existe la compra indicada");

                    // Cargar movimientos de inventario asociados a la referencia (documento)
                    var movimientos = await _tiendaDbContext.TbInvMovimientos
                        .Where(m => m.Referencia == tbCompra.Documento)
                        .Include(m => m.TbInvLotes)
                            .ThenInclude(l => l.TbInvConsumoLotes)
                        .Include(m => m.TbInvConsumoLotes)
                        .ToListAsync();

                    TbInvMovimiento movimientoOrigen = null;
                    if (movimientos != null && movimientos.Any())
                    {
                        movimientoOrigen = movimientos.FirstOrDefault(m => m.IdTransaccionInventario == tbCompra.IdTransaccion) ?? movimientos.First();

                        // Obtener transacción de reversion venta si existe
                        var transReversionVenta = await _tiendaDbContext.TbInvTransacciones.FirstOrDefaultAsync(t => t.Nombre.ToLower() == "reversion venta");

                        // Si el movimiento origen tiene consumos, no se puede editar
                        bool movimientoTieneConsumos = (movimientoOrigen.TbInvConsumoLotes != null && movimientoOrigen.TbInvConsumoLotes.Any())
                            || movimientoOrigen.TbInvLotes.Any(l => l.TbInvConsumoLotes != null && l.TbInvConsumoLotes.Any());
                        if (movimientoTieneConsumos)
                            throw new Exception("No se puede editar la compra: existen consumos por venta relacionados.");

                        // Si existe algún movimiento que tenga IdMovimientoOrigen apuntando al movimientoOrigen -> no permitir edición
                        var movimientosRelacionados = await _tiendaDbContext.TbInvMovimientos.Where(m => m.IdMovimientoOrigen == movimientoOrigen.Id).ToListAsync();
                        if (movimientosRelacionados.Any())
                            throw new Exception("No se puede editar la compra: existen movimientos relacionados al movimiento de inventario.");

                        // Si existe una venta reversada relacionada (movimiento con transacción 'Reversion Venta')
                        if (transReversionVenta != null && movimientos.Any(m => m.IdTransaccionInventario == transReversionVenta.Id))
                            throw new Exception("No se puede editar la compra: existe una venta reversada relacionada.");
                    }

                    // Aplicar eliminados/actualizaciones/creaciones en detalles de la compra
                    var idsDto = compraEditarDTO.DetalleComprasEditarDto.Select(x => x.Id).ToList();
                    var eliminados = tbCompra.TbComDetallesCompras.Where(x => !idsDto.Contains(x.Id)).ToList();

                    foreach (var e in eliminados)
                    {
                        _tiendaDbContext.TbComDetallesCompras.Remove(e);
                    }

                    foreach (var detDto in compraEditarDTO.DetalleComprasEditarDto)
                    {
                        var existente = tbCompra.TbComDetallesCompras.FirstOrDefault(x => x.Id == detDto.Id);
                        if (existente != null)
                        {
                            // actualizar
                            existente.IdArticulo = detDto.ArticuloId;
                            existente.Cantidad = detDto.Cantidad;
                            existente.Descripcion = detDto.Descripcion;
                            existente.ImpuestoValor = detDto.ImpuestoValor;
                            existente.ValorCompra = detDto.ValorCompra;
                            existente.ValorVenta = detDto.ValorVenta;
                            existente.ValorTotal = detDto.ValorTotal;
                            existente.FechaExpiracion = detDto.FechaCaducidad;
                            existente.NumeroLote = detDto.NumeroLote;
                            existente.Codigo = detDto.Codigo;
                        }
                        else
                        {
                            // nuevo
                            tbCompra.TbComDetallesCompras.Add(new TbComDetallesCompra
                            {
                                IdArticulo = detDto.ArticuloId,
                                Cantidad = detDto.Cantidad,
                                Codigo = detDto.Codigo,
                                FechaExpiracion = detDto.FechaCaducidad,
                                NumeroLote = detDto.NumeroLote,
                                Descripcion = detDto.Descripcion,
                                ImpuestoValor = detDto.ImpuestoValor,
                                ValorCompra = detDto.ValorCompra,
                                ValorVenta = detDto.ValorVenta,
                                ValorTotal = detDto.ValorTotal
                            });
                        }
                    }

                    // Mapear campos de la compra y actualizar fecha de modificación
                    this._mapper.MapeoCompraEdicionDtoACompraTb(compraEditarDTO, tbCompra);
                    tbCompra.FechaModificacion = DateTime.Now;

                    var resp = await this._compraRepository.Editar(tbCompra);
                    if (resp == false)
                        throw new Exception("No se pudo editar la compra!!");

                    // Si existe movimiento de inventario asociado, actualizar sus lotes para reflejar los cambios en la compra
                    if (movimientoOrigen != null)
                    {
                        await ProcesarMovimientoInventario(movimientoOrigen, tbCompra);
                    }

                    

                    await _tiendaDbContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
