using Microsoft.EntityFrameworkCore;
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
    public class ClienteService: IClienteService
    {
        private readonly TiendaDbContext _tiendaDbContext;
        private readonly IMapeos _mapper;
        private readonly IGenericRepository<TbComCliente> _clienteRepository;

        public ClienteService(TiendaDbContext tiendaDbContext, IMapeos mapper, IGenericRepository<TbComCliente> clienteRepository)
        {
            this._tiendaDbContext = tiendaDbContext;
            this._mapper = mapper;
            this._clienteRepository = clienteRepository;
        }

        public async Task<ClienteDTO> BuscarClienteCI(string identificacion)
        {
            try
            {
                var cliente = await this._tiendaDbContext.TbComClientes
                    .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                    .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(d => d.TbGrlDireccione)
                    .ThenInclude(c => c.IdCiudadNavigation)
                    .Where(p => p.IdPersonaNavigation.Identificacion.Trim() == identificacion.Trim()).FirstOrDefaultAsync();
                if (cliente is null)
                {
                    throw new Exception("No se encontró ningún cliente con la identificación proporcionada");
                }
                return this._mapper.MapeoClienteTbAClienteDto(cliente);

            }
            catch (Exception ex)
            {
                throw new Exception("Error buscando el cliente!!", ex);
            }
        }


        public async Task<List<CiudadDTO>> ListarCiudades()
        {
            List<TbGrlCiudades> tbCiudades = await this._tiendaDbContext.TbGrlCiudades.Where(est => est.EstadoVisual == true).ToListAsync();
            try
            {
                return this._mapper.MapeoListaCiudadesTbaAListaCiudadesDto(tbCiudades);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CrearCliente(ClienteCreacionDTO clienteCreacionDto)
        {
            using (var transaccion = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var cliente = this._mapper.MapeoCLienteDtoAClienteTb(clienteCreacionDto);
                    await this._clienteRepository.Crear(cliente);
                    if (cliente.Id == 0)
                        throw new Exception("No se pudo crear el cliente");
                    transaccion.Commit();
                    return cliente.Id;
                }
                catch
                {
                    transaccion.Rollback();
                    throw new Exception("Error ha ocurrido un error creando el cliente, comuníquese con el administrador del sistema!!!");
                }
            }
        }

        public async Task<bool> EditarCliente(ClienteEditarDTO clienteEditarDto)
        {

            IQueryable<TbComCliente>  clientesTb = await this._clienteRepository.Consultar();
            using (var transaccion = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var cliente = clientesTb.Where(c => c.Id == clienteEditarDto.Id)
                        .Include(per =>per.IdPersonaNavigation)
                        .ThenInclude(ti => ti.IdTipoIdentificacionNavigation)
                        .Include(per => per.IdPersonaNavigation)
                        .ThenInclude(dir => dir.TbGrlDireccione)
                            .ThenInclude(ciu => ciu.IdCiudadNavigation)
                        .FirstOrDefault();
                    if(cliente is null)
                        throw new Exception("No se encontró el cliente a editar");
                    cliente.IdPersonaNavigation.Telefono = clienteEditarDto.Telefono;
                    cliente.IdPersonaNavigation.Mail = clienteEditarDto.Mail;
                    cliente.IdPersonaNavigation.FechaModificacion = DateTime.Now;
                    cliente.IdPersonaNavigation.TbGrlDireccione.IdCiudad = clienteEditarDto.DireccionEdicionDto.IdCiudad;
                    cliente.IdPersonaNavigation.TbGrlDireccione.Descripcion = clienteEditarDto.DireccionEdicionDto.Descripcion;
                    cliente.IdPersonaNavigation.Nombres = clienteEditarDto.Nombres;
                    cliente.IdPersonaNavigation.Apellidos = clienteEditarDto.Apellidos;
                    cliente.IdPersonaNavigation.Identificacion = clienteEditarDto.Identificacion;
                    cliente.EstadoVisual = clienteEditarDto.EstadoVisual;
                    cliente.Estado = clienteEditarDto.Estado;
                    cliente.IdPersonaNavigation.IdTipoIdentificacion = clienteEditarDto.IdTipoIdentificacion;
                    var resp = await this._clienteRepository.Editar(cliente);
                    if (resp == false)
                        throw new Exception("No se pudo editar el cliente");
                    transaccion.Commit();
                    return resp;
                }
                catch
                {
                    transaccion.Rollback();
                    throw new Exception("Error ha ocurrido un error creando el cliente, comuníquese con el administrador del sistema!!!");
                }
            }
        }

        public async Task<List<TipoIdentificacionDTO>> ListarTiposIdentificacion()
        {
            List<TbGrlTipoIdentificacion> tbTiposIdentificacion= await this._tiendaDbContext.TbGrlTipoIdentificacions.Where(est => est.EstadoVisual == true).ToListAsync();
            try
            {
                return this._mapper.MapeoListTiposIdentificacionTbaAListaTiposIDentificacionDto(tbTiposIdentificacion);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ClienteDTO>> ListarClientes()
        {
            try
            {
                var clientesList = await this._tiendaDbContext.TbComClientes.Where(cli => cli.EstadoVisual == true).Include(p => p.IdPersonaNavigation)
                    .ThenInclude(t => t.IdTipoIdentificacionNavigation)
                    .Include(p => p.IdPersonaNavigation)
                    .ThenInclude(d => d.TbGrlDireccione)
                    .ThenInclude(c => c.IdCiudadNavigation).ToListAsync();
                var listaClientesDto = this._mapper.MapeoListaClientesTbaAListaClientesDto(clientesList);
                return listaClientesDto;
            }
            catch
            {
                throw;
            }
        }
    }
}
