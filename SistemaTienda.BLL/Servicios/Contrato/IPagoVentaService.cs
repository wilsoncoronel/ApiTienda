using SistemaTienda.DTO;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IPagoVentaService
    {
        Task<List<PagosVentasDTO>> ListarPagos(DateOnly fechaInicial, DateOnly fechaFinal);
        Task<List<DetallesPagoDTO>> ListaDetallesPagos(int idPago);
        Task<bool> AnularPago(int idDetallePago);
        Task<bool> ActualizarComprobantePago(ActualizarComprobantePagoDTO comprobanteDto);
    }
}
