using SistemaTienda.DTO;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IPagoVentaService
    {
        Task<PagosVentasDTO> PagoVenta(int idVenta);
        Task<int> CrearDetallePago(DetallePagoCreacionDTO detallePagoCrear);
        Task<List<EstadosPagoDTO>> ListarEstadosPago();
        Task<List<TipoPagoDTO>> ListarTiposPago();
        Task<List<PagosVentasDTO>> ListarPagos(DateOnly fechaInicial, DateOnly fechaFinal);
        Task<List<DetallesPagoDTO>> ListaDetallesPagos(int idPago);
        Task<bool> AnularPago(int idDetallePago);
        Task<bool> ActualizarComprobantePago(ActualizarComprobantePagoDTO comprobanteDto);
    }
}
