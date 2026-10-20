using SistemaTienda.DTO;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IDevolucionCompraService
    {
        Task<int> CrearDevolucionCompra(DevolucionCompraCreacionDTO dto);
        Task ReversarDevolucionCompra(int id);
        Task<List<TipoDevolucionDTO>> ListaTiposDevolucion();
        Task<List<DevolucionCompraDTO>> ListarDevolucionesFechas(DateOnly fechaInicio, DateOnly fechaFin);
        Task<CreditoProveedorDTO> ObtenerCreditoPorDevolucion(int idDevolucion);
        Task<List<DetalleDevolucionCompraDTO>> ListarDetallesDevolucion(int idDevolucionCompra);
        Task<DevolucionCompraDTO> DevolucionCompra(int idDevolucion);
    }
}