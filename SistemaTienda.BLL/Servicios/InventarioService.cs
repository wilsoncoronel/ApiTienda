using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;

namespace SistemaTienda.BLL.Servicios
{
    public class InventarioService : IInventarioService
    {
        private readonly IGenericRepository<TbInvMovimiento> _inventarioRepo;
        private readonly IMapeos mapeo;
        private readonly IGenericRepository<TbInvTransacciones> _transacRepository;
        private readonly IGenericRepository<TbInvLote> _loteRepository;
        private readonly IGenericRepository<TbInvConsumoLote> _consumoRepository;
        private readonly ILogger<InventarioService> _logger;
        private readonly TiendaDbContext _tiendaDb;

        public InventarioService(IGenericRepository<TbInvTransacciones> transacRepository,
            IGenericRepository<TbInvMovimiento> _inventarioRepo, IMapeos mapeo, ILogger<InventarioService> logger, TiendaDbContext tiendaDb, IGenericRepository<TbInvLote> loteRepository, IGenericRepository<TbInvConsumoLote> consumoRepository)
        {
            this._transacRepository = transacRepository;
            this._inventarioRepo = _inventarioRepo;
            this.mapeo = mapeo;
            this._logger = logger;
            this._tiendaDb = tiendaDb;
            this._loteRepository = loteRepository;
            _consumoRepository = consumoRepository;
        }
        public async Task<List<InventarioLoteDTO>> ExistenciasInventario(bool incluirCeros = false)
        {
            var listaMovimientos = await this._tiendaDb.TbInvMovimientos
                .Where(tra => tra.IdTransaccionInventario == 1)
                .Include(m => m.TbInvLotes)
                    .ThenInclude(l => l.IdArticuloNavigation)
                        .ThenInclude(a => a.IdImpuestoNavigation) // Asegurar carga del impuesto para evitar NullReference
                .ToListAsync();

            // Aplanar los lotes de todos los movimientos
            var lotes = listaMovimientos.SelectMany(m => m.TbInvLotes).ToList();

            // Mapear a DTO usando el mapeador existente
            var listaDto = this.mapeo.MapeoListaDetallesLotesTbAListaDetallesLotesDto(lotes);

            // Filtrar según stock (por defecto ocultar ceros)
            var resultado = incluirCeros
                ? listaDto.Where(l => l.StockDisponible > 0).ToList()
                : listaDto.Where(l => l.StockDisponible <= 0).ToList();

            return resultado;
        }
        public async Task<List<MovimientoDTO>> ListaInventario(DateOnly FechaInicio, DateOnly FechaFinal)
        {
            var inicio = FechaInicio.ToDateTime(TimeOnly.MinValue);
            var fin = FechaFinal.ToDateTime(TimeOnly.MaxValue);
            
            var tbInventario = await this._tiendaDb.TbInvMovimientos.Where(c => c.Fecha >= inicio && c.Fecha <= fin)
                .Include(t => t.IdTransaccionInventarioNavigation)
                .ToListAsync();
            var listaIventarioDto = this.mapeo.MapeoListaMovimientosTbAListaMovimientosDto(tbInventario);
            return listaIventarioDto;
        }

        public async Task<List<InventarioLoteDTO>> ListaDetallesMovimiento(int IdMovimiento)
        {
            var transaccion = await _tiendaDb.TbInvMovimientos.Where(m => m.Id == IdMovimiento).Include(tra => tra.IdTransaccionInventarioNavigation).FirstOrDefaultAsync();
                
            var nombreTransaccion = transaccion?.IdTransaccionInventarioNavigation?.Nombre;
            var listaIventarioDto = new List<InventarioLoteDTO> { };
            if (nombreTransaccion == "Compra" || nombreTransaccion == "Reversion Compra")
            {
                IQueryable<TbInvLote> tbInvLote = await this._loteRepository.Consultar();
                var listaResultado = new List<TbInvLote>();
                listaResultado = tbInvLote.Where(det => det.IdMovimiento == IdMovimiento)
                    .Include(art => art.IdArticuloNavigation)
                    .ThenInclude(imp => imp.IdImpuestoNavigation)
                    .ToList();
                listaIventarioDto = this.mapeo.MapeoListaDetallesLotesTbAListaDetallesLotesDto(listaResultado);
            }
            else if(nombreTransaccion == "Venta" || nombreTransaccion == "Reversion Venta")
            {
                IQueryable<TbInvConsumoLote> tbInvConsumo = await this._consumoRepository.Consultar();
                var listaResultado = new List<TbInvConsumoLote>();
                listaResultado = tbInvConsumo.Where(con => con.IdMovimiento == IdMovimiento)
                    .Include(det => det.IdDetalleVentaNavigation)
                        .ThenInclude(art => art.IdArticuloNavigation)
                        .ThenInclude(imp => imp.IdImpuestoNavigation)
                    .Include(lot => lot.IdLoteNavigation)
                    .ToList();
                listaIventarioDto = this.mapeo.MapeoListaDetallesConsumosTbAListaDetallesConsumosDto(listaResultado);
            }
            else
            {
                listaIventarioDto = new List<InventarioLoteDTO> { };
            }
            return listaIventarioDto;
        }

        public async Task<List<TransaccionInventarioDTO>> ListaTransaccionesInventario()
        {
            var listaTranInventario = await this._transacRepository.Consultar();
            var resultado = listaTranInventario.ToList().Select(tran => new TransaccionInventarioDTO
            {
                Id = tran.Id,
                Nombre = tran.Nombre,
                Signo = tran.Signo,
                Estado = tran.Estado
            }).ToList();

            return resultado;
        }

        public async Task<List<ResumenVentasDiarioDTO>> ResumenVentasDiario(DateOnly fecha)
        {
            DateTime desde = fecha.ToDateTime(TimeOnly.MinValue);
            DateTime hasta = desde.AddDays(1);

            var consumos = await this._inventarioRepo.Consultar(mov =>
                mov.Fecha >= desde &&
                mov.Fecha < hasta &&
                mov.IdTransaccionInventario == 2);

            var tbConsumos = await consumos
                .Include(con => con.TbInvConsumoLotes)
                    .ThenInclude(det => det.IdDetalleVentaNavigation)
                        .ThenInclude(art => art.IdArticuloNavigation)
                .ToListAsync();

            if (!tbConsumos.Any())
                return new List<ResumenVentasDiarioDTO>();

            return tbConsumos
                .SelectMany(mov => mov.TbInvConsumoLotes)
                .GroupBy(det => new
                {
                    det.IdDetalleVentaNavigation.IdArticuloNavigation.Id,
                    det.IdDetalleVentaNavigation.IdArticuloNavigation.Nombre
                })
                .Select(res => new ResumenVentasDiarioDTO
                {
                    Articulo = new ArticuloMinDTO
                    {
                        Id = res.Key.Id,
                        Nombre = res.Key.Nombre
                    },

                    CantidadVendida = res.Sum(d => d.Cantidad),

                    ValorCompra = res.Sum(d =>
                        d.Cantidad * d.PrecioUnitario),

                    ValorVenta = res.Sum(d =>
                        d.Cantidad * d.IdDetalleVentaNavigation.ValorVenta),

                    UtilidadBruta = res.Sum(d =>
                        (d.Cantidad * d.IdDetalleVentaNavigation.ValorVenta) -
                        (d.Cantidad * d.PrecioUnitario))
                })
                .OrderBy(r => r.Articulo.Nombre)
                .ToList();
        }

        /*public async Task<List<ResumenVentasDiarioDTO>> ResumenVentasDiario(DateOnly fecha)
        {
            //DateTime fechaActual = DateTime.Now.Date;
            DateTime desde = fecha.ToDateTime(TimeOnly.MinValue);  // 00:00
            DateTime hasta = desde.AddDays(1);
            List<ResumenVentasDiarioDTO> resumen = new List<ResumenVentasDiarioDTO>();
            IQueryable <TbInvMovimiento> tbInventarios = await this._inventarioRepo.Consultar(inv =>
            inv.Fecha >= desde &&
            inv.Fecha< hasta);
            if(!tbInventarios.Any())
            {
                resumen = [];
                return resumen;
            }
            var listaResultado = new List<TbInvConsumoLote>();
            listaResultado = await tbInventarios.Where(inv => inv.IdTransaccionInventario == 1)
                .SelectMany(det => det.TbInvConsumoLotes).Include(det => det.IdDetalleVentaNavigation).ThenInclude(art => art.IdArticuloNavigation).ToListAsync();
            resumen = listaResultado.GroupBy(det => new
            {
                det.IdDetalleVentaNavigation.IdArticuloNavigation.Id,
                det.IdDetalleVentaNavigation.IdArticuloNavigation.Nombre
            }).Select(
                    res => new ResumenVentasDiarioDTO
                    {
                        Articulo = new ArticuloMinDTO
                        {
                            Id = res.Key.Id,
                            Nombre = res.Key.Nombre,
                        },
                        CantidadVendida = res.Sum(d => d.Cantidad),
                        ValorCompra = res.Sum(d => d.Cantidad * d.PrecioUnitario),
                        ValorVenta = res.Sum(d => d.IdDetalleVentaNavigation.Cantidad * d.IdDetalleVentaNavigation.ValorVenta),
                        UtilidadBruta = res.Sum(d => d.Cantidad * d.IdDetalleVentaNavigation.ValorVenta) - res.Sum(d => d.Cantidad * d.PrecioUnitario)
                    }
                ).ToList();
        }*/

        /*public async Task<List<ResumenVentasDiarioDTO>> ResumenVentasMensual(DateOnly fechaResumen)
        {
            DateTime fechaFin = fechaResumen.ToDateTime(TimeOnly.MinValue);  // 00:00
            DateTime fechaInicio = new DateTime(fechaFin.Year, fechaFin.Month, 1);
            List<ResumenVentasDiarioDTO> resumen = new List<ResumenVentasDiarioDTO>();
            IQueryable<TbInvInventario> tbInventarios = await this._inventarioRepo.Consultar(inv =>
            inv.FechaCreacion >= fechaInicio &&
            inv.FechaCreacion <= fechaFin);
            if (!tbInventarios.Any())
            {
                this._logger.LogInformation("No se encontraron inventarios en el rango de fechas {FechaInicio} - {FechaFin}", fechaInicio, fechaFin);
                resumen = [];
                return resumen;
            }
            var listaResultado = new List<TbDetallesInventario>();
            try
            {
                listaResultado = await tbInventarios.Where(inv => inv.IdVenta != null)
                   .SelectMany(det => det.TbDetallesInventarios).Include(art => art.IdArticuloNavigation).ThenInclude(imp => imp.IdImpuestoNavigation).ToListAsync();
                resumen = listaResultado.GroupBy(det => new
                {
                    det.IdArticulo,
                    det.IdArticuloNavigation.Nombre
                }).Select(
                        res => new ResumenVentasDiarioDTO
                        {
                            Articulo = new ArticuloMinDTO
                            {
                                Id = res.Key.IdArticulo,
                                Nombre = res.Key.Nombre,
                            },
                            CantidadVendida = res.Sum(d => d.Cantidad),
                            ValorCompra = res.Sum(d => d.Cantidad * d.PrecioCompra),
                            ValorVenta = res.Sum(d => d.Cantidad * d.PrecioVenta),
                            UtilidadBruta = res.Sum(d => d.Cantidad * d.PrecioVenta) - res.Sum(d => d.Cantidad * d.PrecioCompra)
                        }
                    ).ToList();
                return resumen;
            }
            catch
            {
                this._logger.LogError("Error al generar el resumen de ventas mensual para el rango de fechas {FechaInicio} - {FechaFin}", fechaInicio, fechaFin);
                throw;
            }
        }*/
    }
}
