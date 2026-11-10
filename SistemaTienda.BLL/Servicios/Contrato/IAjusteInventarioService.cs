using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IAjusteInventarioService
    {
        Task<int> CrearAjusteInventario(AjusteInventarioCrearDTO modelo);
        Task<bool> ReversarAjusteInventario(int idAjuste);
        Task<List<AjusteInventarioDTO>> ListarAjusteInventarioFechas(DateOnly fechaInicio, DateOnly fechaFinal);
        Task<AjusteInventarioCompletoDTO> ListarAjusteInventarioCompleto(int idAjuste);
        Task<List<EstadosAjusteDTO>> ListaEstadosAjuste();
    }
}
