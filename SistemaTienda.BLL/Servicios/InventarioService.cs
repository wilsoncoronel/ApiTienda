using Microsoft.EntityFrameworkCore;
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

        public InventarioService(IGenericRepository<TbDetallesInventario> detInventarioRepo, IGenericRepository<TbInvTransacciones> transacRepository, IGenericRepository<TbInvInventario> _inventarioRepo, IMapeos mapeo)
        {
            this._detInventarioRepo = detInventarioRepo;
            this._transacRepository = transacRepository;
            this._inventarioRepo = _inventarioRepo;
            this.mapeo = mapeo;
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
    }
}
