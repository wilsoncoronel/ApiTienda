using Microsoft.EntityFrameworkCore;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DTO;
using SistemaTienda.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios
{
    public class SistemaService : ISistemaService
    {
        private readonly TiendaDbContext tiendaDb;
        private readonly IMapeosSistemas mapeo;

        public SistemaService(TiendaDbContext tiendaDb, IMapeosSistemas mapeo)
        {
            this.tiendaDb = tiendaDb;
            this.mapeo = mapeo;
        }
        public async Task<List<RolDTO>> ListarRoles()
        {
            var roles = await tiendaDb.TbSisRols.ToListAsync();
            return this.mapeo.MapeoListaRolDto(roles);
        }
    }
}
