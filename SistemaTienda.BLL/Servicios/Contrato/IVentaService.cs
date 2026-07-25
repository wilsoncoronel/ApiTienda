using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IVentaService
    {
        Task<List<EstadoVentaDTO>> ListarEstadosVentas();
        Task<int> RegistrarVenta(VentaCreacionDTO ventaDto);
        Task<bool> EditarVenta(VentaEditarDTO compraDto);
        Task<List<VentaMinDTO>> ListarVentas(DateOnly fechaInicial, DateOnly fechaFinal);
        Task<VentaDTO> ObtenerVenta(int idVenta);
        Task<bool> ReversarVenta(int id, string documento);
    }
}
