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
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<TbSisPermisosRol> permisosRolRepositorio;
        private readonly IMapeos mapper;

        public MenuService(IGenericRepository<TbSisPermisosRol> permisosRolRepositorio, IMapeos mapper)
        {
            this.permisosRolRepositorio = permisosRolRepositorio;
            this.mapper = mapper;
        }

        public async Task<List<PermisosRolDTO>> ObtenerMenu(int idRol)
        {
            try
            {
                var menu = await this.permisosRolRepositorio.Consultar(p => p.IdRol == idRol && p.EstadoVisual == true);
                if (menu.Count() == 0)
                    throw new Exception("No se encontraron permisos asignados a este rol");
                var tbPermisoRol = menu.Include(m => m.IdMenuNavigation).ToList();
                var ListMenuDto = this.mapper.MapeoListaTbSisPermisosRolAPermisosRolDTO(tbPermisoRol);
                return ListMenuDto.OrderBy(m => m.Menu.Nombre).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
