namespace SistemaTienda.DTO;

public class CreditoProveedorDetalleDTO
{
    public int Id { get; set; }
    public int IdDetalleCompra { get; set; }
    public int IdArticulo { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal ValorCompra { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Impuestos { get; set; }
    public decimal Total { get; set; }
    public bool Estado { get; set; }
}
