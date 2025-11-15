using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface ITipoArticuloService
    {
        Task<int> CrearTipoArticulo(TipoArticuloCreacionDTO tipoArticuloCreacionDto);
        Task<bool> EditarTipoArticulo(TipoArticuloEditarDTO tipoArticuloEditarDto);
        Task<List<TipoArticuloDTO>> ListarTiposArticulos();
    }
}
