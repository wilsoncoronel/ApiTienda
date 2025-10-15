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

        public Task<int> CrearCliente(ClienteCreacionDTO proveedorCreacionDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarCliente(ClienteEditarDTO proveedorEditarDto)
        {
            throw new NotImplementedException();
        }
    }
}
