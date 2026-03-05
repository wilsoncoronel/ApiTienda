using Microsoft.EntityFrameworkCore;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;

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

        public async Task<List<UsuarioDTO>> ListarUsuario()
        {
            var queryUsuarios = await this._tiendaDbContext.TbSisUsuarios.Where(usu => usu.Estado == true).Include(p => p.IdPersonaNavigation)
                    .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(d => d.TbGrlDireccione)
                .Include(p => p.IdPersonaNavigation)
                .ThenInclude(d => d.TbGrlDireccione)
                .ThenInclude(c => c.IdCiudadNavigation)
                .Include(r => r.IdRolNavigation)
                .ToListAsync();
            
            return this._mapper.MapeoArrayUsuarioTbAUsuarioDto(queryUsuarios);
        }


        public async Task<UsuarioDTO> ListarUsuarioId(int idUsuario)
        {
            var user = await this._tiendaDbContext.TbSisUsuarios.Where(u => u.Id == idUsuario)
                .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                    .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(d => d.TbGrlDireccione)
                            .ThenInclude(c => c.IdCiudadNavigation)
                    .Include(r => r.IdRolNavigation).FirstOrDefaultAsync();
            return this._mapper.MapeoUsuarioTbAUsuarioDto(user);
        }

        public async Task<int> EditarUsuario(UsuarioEditarDTO userEditarDto)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var usuarioDb = await this._tiendaDbContext.TbSisUsuarios.Include(p => p.IdPersonaNavigation)
                        .ThenInclude(d => d.TbGrlDireccione)
                            .ThenInclude(c => c.IdCiudadNavigation)
                        .Include(r => r.IdRolNavigation)
                            .FirstOrDefaultAsync(x => x.Id == userEditarDto.Id);
                    if (usuarioDb.Id == null)
                        throw new Exception("No se modificó el usuario con Id: " + userEditarDto.Id);
                    this._mapper.MapeoUsuarioEdicionDtoATbUsuario(userEditarDto, usuarioDb);
                    await this._usuarioRepository.Editar(usuarioDb);
                    transaction.Commit();
                    return usuarioDb.Id;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
