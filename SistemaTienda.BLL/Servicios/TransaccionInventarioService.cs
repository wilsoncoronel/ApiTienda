using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios
{
    public class TransaccionInventarioService: ITransaccionInventarioService
    {
        private readonly IGenericRepository<TbInvTransacciones> genericRepository;
        private readonly IMapeos mapper;
        private readonly TiendaDbContext tiendaDbContext;

        public TransaccionInventarioService(IGenericRepository<TbInvTransacciones> genericRepository, IMapeos mapper, TiendaDbContext tiendaDbContext)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.tiendaDbContext = tiendaDbContext;
        }

        public async Task<List<TransaccionInventarioDTO>> ListarTransaccionesInventario()
        {
            var transacciones = await tiendaDbContext.TbInvTransacciones.Where(t => t.Estado == true).ToListAsync();
            return transacciones.Select(t => mapper.MapeoTransaccionInventarioTbADto(t)).ToList();
        }
    }
}
