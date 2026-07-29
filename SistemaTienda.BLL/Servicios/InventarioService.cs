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
        private readonly ILogger<InventarioService> _logger;
        private readonly TiendaDbContext _tiendaDb;

        public InventarioService(IGenericRepository<TbInvTransacciones> transacRepository,
            IGenericRepository<TbInvMovimiento> _inventarioRepo, IMapeos mapeo, ILogger<InventarioService> logger, TiendaDbContext tiendaDb, IGenericRepository<TbInvLote> loteRepository)
        {
            this._transacRepository = transacRepository;
            this._inventarioRepo = _inventarioRepo;
            this.mapeo = mapeo;
            this._logger = logger;
            this._tiendaDb = tiendaDb;
            this._loteRepository = loteRepository;
        }
        public async Task<List<ExistenciaDTO>> ExistenciasInventario()
        {
            /*try
            {
                var listainventario = await this._tiendaDb.TbDetallesInventarios.Where(tra => tra.IdTransaccionInventario != 3).Include(art => art.IdArticuloNavigation)
                    .Include(tra => tra.IdTransaccionInventarioNavigation).ToListAsync();

                var resultado = listainventario.GroupBy(det => new
                {
                    det.IdArticuloNavigation.Nombre
                }).Select(g => new ExistenciaDTO
                {
                    IdArticulo = g.First().IdArticulo,
                    NombreArticulo = g.Key.Nombre,
                    TotalCantidad = g.Sum(d => d.Cantidad * d.IdTransaccionInventarioNavigation.Signo),

                }).ToList();*/
                return [];
            /*}
            catch
            {
                throw;
            }*/
        }
        public async Task<List<MovimientoDTO>> ListaInventario(DateOnly FechaInicio, DateOnly FechaFinal)
        {
            var inicio = FechaInicio.ToDateTime(TimeOnly.MinValue);
            var fin = FechaFinal.ToDateTime(TimeOnly.MaxValue);
            
            try
            {
                var tbInventario = await this._tiendaDb.TbInvMovimientos.Where(c => c.Fecha >= inicio && c.Fecha <= fin)
                    .Include(t => t.IdTransaccionInventarioNavigation)
                    .ToListAsync();
                var listaIventarioDto = this.mapeo.MapeoListaMovimientosTbAListaMovimientosDto(tbInventario);
                return listaIventarioDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<InventarioLoteDTO>> ListaDetallesMovimiento(int IdMovimiento)
        {
            try
            {
                IQueryable<TbInvLote> tbInvLote = await this._loteRepository.Consultar();
                var listaResultado = new List<TbInvLote>();
                listaResultado = tbInvLote.Where(det=> det.IdMovimiento == IdMovimiento)
                    .Include(art => art.IdArticuloNavigation)
                    .ThenInclude(imp => imp.IdImpuestoNavigation)
                    .ToList();
                var listaIventarioDto = this.mapeo.MapeoListaDetallesLotesTbAListaDetallesLotesDto(listaResultado);
                return listaIventarioDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TransaccionInventarioDTO>> ListaTransaccionesInventario()
        {
            try
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
            catch
            {
                throw;
            }
        }

        /*public async Task<List<ResumenVentasDiarioDTO>> ResumenVentasDiario(DateOnly fecha)
        {
            //DateTime fechaActual = DateTime.Now.Date;
            DateTime desde = fecha.ToDateTime(TimeOnly.MinValue);  // 00:00
            DateTime hasta = desde.AddDays(1);
            List<ResumenVentasDiarioDTO> resumen = new List<ResumenVentasDiarioDTO>();
            IQueryable <TbInvInventario> tbInventarios = await this._inventarioRepo.Consultar(inv =>
            inv.FechaCreacion >= desde &&
            inv.FechaCreacion < hasta);
            if(!tbInventarios.Any())
            {
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
                throw;
            }
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
