using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IPorcentajeGananciaService
    {
        Task<int> CrearPorcentaje(PorcentajeGananciaCreacionDTO porcentajeGananciaCreacionDto);
        Task<bool> EditarPorcentaje(PorcentajeGananciaDTO porcentajeGananciaEdicionDto);
        Task<List<PorcentajeGananciaDTO>> ListarPorcentaje();
    }
}
