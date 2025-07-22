using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<int> CrearUsuario(UsuarioCreacionDTO userDto);
        Task<List<UsuarioDTO>> ListarUsuario();

        Task<UsuarioDTO> ListarUsuarioId(int idUsuario);
    }
}
