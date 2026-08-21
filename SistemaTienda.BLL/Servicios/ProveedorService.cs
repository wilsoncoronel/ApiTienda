using Microsoft.EntityFrameworkCore;
using SistemaTienda.API.Exceptions;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;

namespace SistemaTienda.BLL.Servicios
{
    public class ProveedorService : IProveedorService
    {
        private readonly TiendaDbContext _tiendaDbContext;
        private readonly IGenericRepository<TbComProveedores> _proveedorRepository;
        private readonly IMapeos mapper;

        public ProveedorService(TiendaDbContext tiendaDbContext, IMapeos mapper, IGenericRepository<TbComProveedores> proveedorRepository)
        {
            _tiendaDbContext = tiendaDbContext;
            this.mapper = mapper;
            this._proveedorRepository = proveedorRepository;
        }
        
        public async Task<int> CrearProveedor(ProveedorCreacionDTO proveedorCreacionDto)
        {
            using var transaccion = await _tiendaDbContext.Database.BeginTransactionAsync();
            try
            {
                var proveedor = this.mapper.MapeoProveedorDtoAProveedorTb(proveedorCreacionDto);
                await this._proveedorRepository.Crear(proveedor);
                if (proveedor.Id == 0)
                    throw new Exception("No se pudo crear el proveedor");
                await transaccion.CommitAsync();
                return proveedor.Id;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw new Exception("Error ha ocurrido un error creando el proveedor, comuniquese con el administrador del sistema!!!");
            }
        }

        public async Task<ProveedorDTO> BuscarProveedorCI(string identificacion,bool verPersona)
        {
            identificacion = identificacion.Trim();

            if (verPersona)
            {
                var persona = await _tiendaDbContext.TbGrlPersonas
                    .Include(p => p.IdTipoIdentificacionNavigation)
                    .Include(p => p.TbGrlDireccione)
                        .ThenInclude(d => d.IdCiudadNavigation)
                    .FirstOrDefaultAsync(
                        p => p.Identificacion.Trim() == identificacion);

                if (persona is not null)
                {
                    var proveedor = await _tiendaDbContext.TbComProveedores
                        .Include(p => p.IdPersonaNavigation)
                            .ThenInclude(p => p.IdTipoIdentificacionNavigation)
                        .Include(p => p.IdPersonaNavigation)
                            .ThenInclude(p => p.TbGrlDireccione)
                                .ThenInclude(d => d.IdCiudadNavigation)
                        .FirstOrDefaultAsync(
                            p => p.IdPersona == persona.Id);

                    if (proveedor is null)
                    {
                        proveedor = await CrearSoloProveedor(persona);
                    }

                    return mapper.MapeoProveedorTbAProveedorDto(proveedor);
                }
            }

            var proveedorExistente =
                await _tiendaDbContext.TbComProveedores
                    .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(p => p.IdTipoIdentificacionNavigation)
                    .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(p => p.TbGrlDireccione)
                            .ThenInclude(d => d.IdCiudadNavigation)
                    .FirstOrDefaultAsync(
                        p => p.IdPersonaNavigation.Identificacion.Trim()
                            == identificacion);

            if (proveedorExistente is null)
            {
                throw new NotFoundException(
                    "No se encontró ningún proveedor con la identificación proporcionada.");
            }

            return mapper.MapeoProveedorTbAProveedorDto(
                proveedorExistente);
        }

        private async Task<TbComProveedores> CrearSoloProveedor(TbGrlPersona persona)
        {
            var proveedor = new TbComProveedores
            {
                IdPersona = persona.Id,
                RazonSocial = $"{persona.Apellidos} {persona.Nombres}",
                Estado = true,
                EstadoVisual = true
            };

            _tiendaDbContext.TbComProveedores.Add(proveedor);
            await _tiendaDbContext.SaveChangesAsync();
            return proveedor;
        }

        public async Task<bool> EditarProveedor(ProveedorEditarDTO proveedorEditarDto)
        {
            using var transaction = await _tiendaDbContext.Database.BeginTransactionAsync();
            try
            {
                var proveedor = await this._tiendaDbContext.TbComProveedores
                    .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                    .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(d => d.TbGrlDireccione)
                    .ThenInclude(c => c.IdCiudadNavigation)
                    .FirstOrDefaultAsync(p => p.Id == proveedorEditarDto.Id);
                if(proveedor is null)
                {
                    throw new NotFoundException("No se encontró el proveedor a editar");
                }
                this.mapper.MapeoProveedorEditarDtoAProveedorTb(proveedorEditarDto, proveedor);
                var resp = await this._proveedorRepository.Editar(proveedor);
                if (resp == false)
                    throw new ConflictException("No se pudo editar el proveedor");
                await transaction.CommitAsync();
                return resp;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
        }

        public async Task<List<ProveedorDTO>> ListarProveedores()
        {
            var proveedoresList = await this._tiendaDbContext.TbComProveedores.Where(prov=> prov.EstadoVisual == true)
                .Include(p => p.IdPersonaNavigation)
                .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                .Include(p => p.IdPersonaNavigation)
                .ThenInclude(d => d.TbGrlDireccione)
                .ThenInclude(c => c.IdCiudadNavigation).ToListAsync();
            return this.mapper.MapeoProveedorTbListaAProveedorDtoLista(proveedoresList);
        }

        public async Task<List<CiudadDTO>> ListarCiudades()
        {
            List<TbGrlCiudades> tbCiudades = await this._tiendaDbContext.TbGrlCiudades.Where(est => est.EstadoVisual == true).ToListAsync();
            return this.mapper.MapeoListaCiudadesTbaAListaCiudadesDto(tbCiudades);
        }

        public async Task<List<TipoIdentificacionDTO>> ListarTiposIdentificacion()
        {
            List<TbGrlTipoIdentificacion> tbTiposIdentificacion = await this._tiendaDbContext.TbGrlTipoIdentificacions.Where(est => est.EstadoVisual == true).ToListAsync();
            return this.mapper.MapeoListTiposIdentificacionTbaAListaTiposIDentificacionDto(tbTiposIdentificacion);
        }
    }
}