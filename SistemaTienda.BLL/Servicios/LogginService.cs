using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using SistemaTienda.Utility;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.Model;
using Microsoft.IdentityModel.Tokens;

namespace SistemaTienda.BLL.Servicios
{
    public class LogginService : ILogginService
    {

        public readonly IGenericRepository<TbSisUsuario> _usuarioRepositorio;
        public readonly IMapeos _mapeos;
        public readonly TiendaDbContext _dbContext;
        private readonly IMenuService menuService;

        public LogginService(IGenericRepository<TbSisUsuario> usuarioRepositorio,IMapeos mapeos, TiendaDbContext dbContext, IMenuService menuService)
        {
            this._usuarioRepositorio = usuarioRepositorio;
            this._mapeos = mapeos;
            this._dbContext = dbContext;
            this.menuService = menuService;
        }

        public async Task<UsuarioDTO> ObtenerPerfil(int id)
        {
            try
            {
                var usuario = await this._usuarioRepositorio.Consultar(u => u.Id == id);
                TbSisUsuario user = usuario
                    .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                    .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(d => d.TbGrlDireccione)
                            .ThenInclude(c => c.IdCiudadNavigation)
                    .Include(r => r.IdRolNavigation).First();
                if (user == null)
                    throw new Exception("No se encuentra ningun usuario con esas credenciales");
                return this._mapeos.MapeoUsuarioTbAUsuarioDto(user);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SesionDTO> ExtraerPerfil(string us)
        {
            try
            {
                var  query = await this._usuarioRepositorio.Consultar(u => u.NombreUsuario == us);

                var user = query.Include(p => p.IdPersonaNavigation)
                        .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                    .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(d => d.TbGrlDireccione)
                            .ThenInclude(c => c.IdCiudadNavigation)
                    .Include(r => r.IdRolNavigation).First();

                if (user is null)
                    throw new Exception("No se encuentra ningun usuario con esas credenciales");
                

                SesionDTO sesion = this._mapeos.MapeoUsuarioDtoASesionDto(user);

                return sesion;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PermisosRolDTO>> ValidarCredenciales(string user, string clave)
         {
             try
             {
                var usuario = await this._dbContext.TbSisUsuarios.Where(u => u.NombreUsuario == user && u.Password == clave)
                    .Include(p => p.IdPersonaNavigation)
                        .Include(r => r.IdRolNavigation)
                    .FirstOrDefaultAsync();
                if (usuario is null)
                    throw new TaskCanceledException("Usuario no existe o esta desactivado");
                
                return await this.menuService.ObtenerMenu(usuario.IdRol);
            }
             catch
             { 
                 throw;
             }
         }
    }
}

