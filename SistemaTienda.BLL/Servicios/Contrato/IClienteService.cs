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
        Task<int> CrearCliente(ClienteCreacionDTO proveedorCreacionDto);
        Task<List<TipoIdentificacionDTO>> ListarTiposIdentificacion();
        Task<bool> EditarCliente(ClienteEditarDTO proveedorEditarDto);
        Task<ClienteDTO> BuscarClienteCI(string identificacion);
    }
}
