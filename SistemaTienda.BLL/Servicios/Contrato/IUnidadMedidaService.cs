using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IUnidadMedidaService
    {
        Task<int> CrearUnidadMedida(UnidadCreacionDTO unidadCreacionDto);
        Task<bool> EditarUnidadMedida(UnidadEditarDTO unidadEditarDto);
        Task<List<UnidadMedidaDTO>> ListarUnidadesMedida();
    }
}
