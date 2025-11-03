using Microsoft.EntityFrameworkCore;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios
{
    public class InventarioService : IInventarioService
    {
        private readonly IGenericRepository<TbDetallesInventario> _inventarioRepo;

        public InventarioService(IGenericRepository<TbDetallesInventario> inventarioRepo)
        {
            this._inventarioRepo = inventarioRepo;
        }
        public async Task<List<ExistenciaDTO>> ExistenciasInventario()
        {
            IQueryable<TbDetallesInventario> listainventario = await this._inventarioRepo.Consultar();
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
    }
}
