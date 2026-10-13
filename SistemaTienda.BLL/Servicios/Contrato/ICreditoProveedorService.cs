using SistemaTienda.DTO;

namespace SistemaTienda.BLL.Servicios.Contrato;

public interface ICreditoProveedorService
{
    Task<List<CreditoProveedorListaDTO>> ListarCreditosProveedor(int idProveedor);
    Task<CreditoProveedorDTO> VerCredito(int id);
    Task AplicarCreditoCompra(AplicarCreditoCompraDTO dto);
    Task ReversarCreditosCompra(int idCompra);
}
