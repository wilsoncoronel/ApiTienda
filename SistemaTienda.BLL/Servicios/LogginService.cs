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

namespace SistemaTienda.BLL.Servicios
{
    public class LogginService : ILogginService
    {

        public readonly IGenericRepository<TbSisUsuario> _usuarioRepositorio;
        public readonly IMapeos _mapeos;
        public readonly TiendaDbContext _dbContext;
        public LogginService(IGenericRepository<TbSisUsuario> usuarioRepositorio,IMapeos mapeos, TiendaDbContext dbContext)
        {
            this._usuarioRepositorio = usuarioRepositorio;
            this._mapeos = mapeos;
            this._dbContext = dbContext;
        }

        public async Task<UsuarioDTO> ObtenerPerfil(int id)
        {
            try
            {
                var usuario = await this._usuarioRepositorio.Consultar(u => u.Id == id);
                TbSisUsuario user = usuario
                    .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(t => t.IdTipoIdentificacionNavigation)
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

        public async Task<SesionDTO> ValidarCredenciales(string user, string clave)
         {
             try
             {
                var usuario = await this._usuarioRepositorio.Consultar(u => u.IdPersonaNavigation.Mail == user && u.Password == clave);
                if (usuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("Usuario no existe o esta desactivado");
                TbSisUsuario userTb = usuario.Include(p => p.IdPersonaNavigation)
                    .Include(r => r.IdRolNavigation).First();
                 return this._mapeos.MapeoUsuarioDtoASesionDto(userTb);
             }
             catch
             {
                 throw;
             }
         }
    }
}
