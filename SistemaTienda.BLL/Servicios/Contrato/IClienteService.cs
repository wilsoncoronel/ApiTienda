using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> ListarClientes();
        Task<int> CrearCliente(ClienteCreacionDTO clienteCreacionDto);
        Task<List<TipoIdentificacionDTO>> ListarTiposIdentificacion();
        Task<bool> EditarCliente(ClienteEditarDTO clienteEditarDto);
        Task<ClienteDTO> BuscarClienteCI(string identificacion);
        Task<List<CiudadDTO>> ListarCiudades();
    }
}
