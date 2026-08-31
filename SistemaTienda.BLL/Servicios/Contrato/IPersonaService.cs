using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IPersonaService
    {
        Task<List<PersonaDTO>> ListaPersonas();
        Task<int> ConvertirUsuario(int IdPersona);
        Task<int> ConvertirCliente(int IdPersona);
        Task<int> ConvertirProveedor(int IdPersona);
        Task<PersonaCompletoDTO> PersonaCompleta(int idPersona);
    }
}
