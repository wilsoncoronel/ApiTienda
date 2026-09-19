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
        Task<int> CrearImpuestos(ImpuestoCrearDTO impuestoArticuloCreacionDto);
        Task<bool> EditarImpuesto(ImpuestoDTO impuestoArticuloEditarDto);
        Task<List<ImpuestoDTO>> ListarImpuestos();
        Task<List<EstadoImpuestoDTO>> ListarEstados();
    }
}
