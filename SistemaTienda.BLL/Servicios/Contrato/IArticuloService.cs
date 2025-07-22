using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IArticuloService
    {
        Task<int> CrearArticulo(ArticuloCreacionDTO articuloCreacionDto);
        Task<List<ArticuloDTO>> ListarUsuarios();
        Task<int> EditarArticulo(ArticuloDTO articuloEditarDto);
        Task<bool> DesactivarArticulo(int idArticulo);
    }
}
