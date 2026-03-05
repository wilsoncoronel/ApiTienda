using Microsoft.EntityFrameworkCore;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (var transaccion = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var proveedor = this.mapper.MapeoProveedorDtoAProveedorTb(proveedorCreacionDto);
                    await this._proveedorRepository.Crear(proveedor);
                    if (proveedor.Id == 0)
                        throw new Exception("No se pudo crear el proveedor");
                    transaccion.Commit();
                    return proveedor.Id;
                }
                catch
                {
                    transaccion.Rollback();
                    throw new Exception("Error ha ocurrido un error creando el proveedor, comuniquese con el administrador del sistema!!!");
                }
            }
        }

        public async Task<ProveedorDTO> BuscarProveedorCI(string identificacion)
        {
            
            try
            {
                var proveedor = await this._tiendaDbContext.TbComProveedores
                    .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                    .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(d => d.TbGrlDireccione)
                    .ThenInclude(c => c.IdCiudadNavigation)
                    .Where(p => p.IdPersonaNavigation.Identificacion.Trim() == identificacion.Trim()).FirstOrDefaultAsync();
                if(proveedor is null)
                {
                    throw new Exception("No se encontró ningún proveedor con la identificación proporcionada");
                }
                return this.mapper.MapeoProveedorTbAProveedorDto(proveedor);

            }
            catch(Exception ex)
            {
                throw new Exception("Erro buscando el proveedor", ex);
            }
        }
        
        public async Task<bool> EditarProveedor(ProveedorEditarDTO proveedorEditarDto)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {

                    var proveedor = await this._tiendaDbContext.TbComProveedores
                        .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                        .Include(p => p.IdPersonaNavigation)
                        .ThenInclude(d => d.TbGrlDireccione)
                        .ThenInclude(c => c.IdCiudadNavigation)
                        .FirstOrDefaultAsync(p => p.Id == proveedorEditarDto.Id);
                    this.mapper.MapeoProveedorEditarDtoAProveedorTb(proveedorEditarDto, proveedor);
                    var resp = await this._proveedorRepository.Editar(proveedor);
                    if (resp == false)
                        throw new Exception("No se pudo editar el proveedor");
                    transaction.Commit();
                    return resp;
                }
                catch
                {
                    transaction.Rollback();
                    throw new Exception("Ha ocurrido un error editando el proveedor, comuníquese con el administrador del sistema!!!");
                }
            }
        }

        public async Task<List<ProveedorDTO>> ListarProveedores()
        {
            try
            {
                var proveedoresList = await this._tiendaDbContext.TbComProveedores.Where(prov=> prov.EstadoVisual == true)
                    .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                    .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(d => d.TbGrlDireccione)
                    .ThenInclude(c => c.IdCiudadNavigation).ToListAsync();
                return this.mapper.MapeoProveedorTbListaAProveedorDtoLista(proveedoresList);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CiudadDTO>> ListarCiudades()
        {
            List<TbGrlCiudades> tbCiudades = await this._tiendaDbContext.TbGrlCiudades.Where(est => est.EstadoVisual == true).ToListAsync();
            try
            {
                return this.mapper.MapeoListaCiudadesTbaAListaCiudadesDto(tbCiudades);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TipoIdentificacionDTO>> ListarTiposIdentificacion()
        {
            List<TbGrlTipoIdentificacion> tbTiposIdentificacion = await this._tiendaDbContext.TbGrlTipoIdentificacions.Where(est => est.EstadoVisual == true).ToListAsync();
            try
            {

                return this.mapper.MapeoListTiposIdentificacionTbaAListaTiposIDentificacionDto(tbTiposIdentificacion);
            }
            catch
            {
                throw;
            }
        }
    }
}