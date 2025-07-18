using Microsoft.EntityFrameworkCore.Storage;
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
    public class UsuarioService:IUsuarioService
    {
        private readonly TiendaDbContext _tiendaDbContext;
        public readonly IGenericRepository<TbSisUsuario> _usuarioRepository;
        public readonly IGenericRepository<TbGrlPersona> _personaRepository;
        private readonly IMapeos _mapper;

        public UsuarioService(TiendaDbContext tiendaDbContext, IGenericRepository<TbSisUsuario> usuarioRepository, IGenericRepository<TbGrlPersona> personaRepository, IMapeos mapper)
        {
            this._tiendaDbContext = tiendaDbContext;
            this._usuarioRepository = usuarioRepository;
            this._personaRepository = personaRepository;
            this._mapper = mapper;
        }

        public async Task<int> CrearUsuario(UsuarioCreacionDTO userDto)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var usuarioTb = this._mapper.MapeoUsuarioCreacionDtoATbUsuario(userDto);
                    await this._usuarioRepository.Crear(usuarioTb);
                    if (usuarioTb.Id == null)
                        throw new Exception("No se creo el usuario");
                    transaction.Commit();
                    return usuarioTb.Id;
                }
                catch
                {
                    transaction.Rollback();
                    throw;

                }
            }
                

        }

        public Task<IEnumerable<UsuarioDTO>> ListarUsuario()
        {
            throw new NotImplementedException();
        }
    }
}
