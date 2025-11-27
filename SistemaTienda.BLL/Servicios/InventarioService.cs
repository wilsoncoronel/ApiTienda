using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaTienda.BLL.Servicios.Contrato;
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
    public class InventarioService : IInventarioService
    {
        private readonly IGenericRepository<TbDetallesInventario> _detInventarioRepo;
        private readonly IGenericRepository<TbInvInventario> _inventarioRepo;
        private readonly IMapeos mapeo;
        private readonly IGenericRepository<TbInvTransacciones> _transacRepository;
        private readonly ILogger<InventarioService> _logger;

        public InventarioService(IGenericRepository<TbDetallesInventario> detInventarioRepo, IGenericRepository<TbInvTransacciones> transacRepository, IGenericRepository<TbInvInventario> _inventarioRepo, IMapeos mapeo, ILogger<InventarioService> logger)
        {
            this._detInventarioRepo = detInventarioRepo;
            this._transacRepository = transacRepository;
            this._inventarioRepo = _inventarioRepo;
            this.mapeo = mapeo;
            this._logger = logger;
        }
        public async Task<List<ExistenciaDTO>> ExistenciasInventario()
        {
            IQueryable<TbDetallesInventario> listainventario = await this._detInventarioRepo.Consultar();

            try
            {
                var existencias = listainventario.Where(tra => tra.IdTransaccionInventario != 3).Include(art => art.IdArticuloNavigation)
                    .Include(tra => tra.IdTransaccionInventarioNavigation).ToList();

                var resultado = existencias.GroupBy(det => new
                {
                    det.IdArticuloNavigation.Nombre
                }).Select(g => new ExistenciaDTO
                {
                    IdArticulo = g.First().IdArticulo,
                    NombreArticulo = g.Key.Nombre,
                    TotalCantidad = g.Sum(d => d.Cantidad * d.IdTransaccionInventarioNavigation.Signo),

                }).ToList();
                return resultado;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<InventarioDTO>> ListaInventario(DateOnly FechaInicio, DateOnly FechaFinal)
        {
            var inicio = FechaInicio.ToDateTime(TimeOnly.MinValue);
            var fin = FechaFinal.ToDateTime(TimeOnly.MaxValue);
            IQueryable<TbInvInventario> tbInventario = await this._inventarioRepo.Consultar();
            var listaResultado = new List<TbInvInventario>();
            try
            {
                listaResultado = await tbInventario.Where(c => c.FechaCreacion >= inicio && c.FechaCreacion <= fin)
                    .Include(e => e.IdCompraNavigation)
                    .Include(p => p.IdVentaNavigation)
                    .ToListAsync();
                var listaIventarioDto = this.mapeo.MapeoListaInventarioTbaListaInventarioDto(listaResultado);
                return listaIventarioDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<DetalleInventarioDTO>> ListaDetallesInventario(int IdInventario)
        {
            
            IQueryable<TbDetallesInventario> tbDetallesInventario = await this._detInventarioRepo.Consultar();
            var listaResultado = new List<TbDetallesInventario>();
            try
            {
                listaResultado = tbDetallesInventario.Where(det=> det.IdInventario == IdInventario)
                    .Include(art => art.IdArticuloNavigation)
                    .ThenInclude(imp => imp.IdImpuestoNavigation)
                    .Include(tra => tra.IdTransaccionInventarioNavigation)
                    .ToList();
                var listaIventarioDto = this.mapeo.MapeoListaDetallesInventarioTbAListaDetallesInventarioDto(listaResultado);
                return listaIventarioDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TransaccionInventarioDTO>> ListaTransaccionesInventario()
        {
            IQueryable<TbInvTransacciones> listaTranInventario = await this._transacRepository.Consultar();

            try
            {
                var existencias = listaTranInventario.ToList();
                var resultado = existencias.Select(tran => new TransaccionInventarioDTO
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

        public async Task<List<ResumenVentasDiarioDTO>> ResumenVentasDiario(DateOnly fecha)
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
        }

        public async Task<List<ResumenVentasDiarioDTO>> ResumenVentasMensual(DateOnly fechaResumen)
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
        }
    }
}
