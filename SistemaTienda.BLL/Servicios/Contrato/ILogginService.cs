using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface ILogginService
    {
        Task<UsuarioDTO> ObtenerPerfil(int id);
        Task<SesionDTO> ValidarCredenciales(string usuario, string clave);
    }
}
