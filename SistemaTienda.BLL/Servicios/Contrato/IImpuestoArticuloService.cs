using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IImpuestoArticuloService
    {
        Task<int> CrearImpuestos(ImpuestoArticuloCreacionDTO impuestoArticuloCreacionDto);
        Task<bool> EditarImpuesto(ImpuestoArticuloEditarDTO impuestoArticuloEditarDto);
        Task<List<ImpuestoArticuloDTO>> ListarImpuestos();
        Task<List<EstadoImpuestoDTO>> ListarEstados();
    }
}
