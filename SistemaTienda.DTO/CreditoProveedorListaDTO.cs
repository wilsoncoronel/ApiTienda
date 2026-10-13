namespace SistemaTienda.DTO;

public class CreditoProveedorListaDTO
{
    public int Id { get; set; }
    public int IdDevolucionCompra { get; set; }
    public int IdProveedor { get; set; }
    public string Proveedor { get; set; } = string.Empty;
    public decimal ValorOriginal { get; set; }
    public decimal ValorDisponible { get; set; }
    public DateTime FechaCreacion { get; set; }
    public bool Estado { get; set; }
}
