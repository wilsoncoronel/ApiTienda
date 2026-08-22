using Microsoft.EntityFrameworkCore;
using SistemaTienda.API.Exceptions;
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
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<TbSisPermisosRol> permisosRolRepositorio;
        private readonly IMapeos mapper;
        private readonly TiendaDbContext _tienda;

        public MenuService(IGenericRepository<TbSisPermisosRol> permisosRolRepositorio, IMapeos mapper, TiendaDbContext tienda)
        {
            this.permisosRolRepositorio = permisosRolRepositorio;
            this.mapper = mapper;
            this._tienda = tienda;
        }

        public async Task<List<PermisosRolDTO>> ObtenerMenu(int idRol)
        {
            var menu = await this._tienda.TbSisPermisosRols.Where(p => p.IdRol == idRol && p.EstadoVisual == true).Include(m => m.IdMenuNavigation).ToListAsync();
            if (menu is null)
                throw new NotFoundException("No se encontraron permisos asignados a este rol");
                
            return this.mapper.MapeoListaTbSisPermisosRolAPermisosRolDTO(menu).OrderBy(m => m.Menu.Nombre).ToList();
        }
    }
}
