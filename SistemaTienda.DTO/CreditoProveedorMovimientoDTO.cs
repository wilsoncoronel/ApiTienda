namespace SistemaTienda.DTO;

public class CreditoProveedorMovimientoDTO
{
    public int Id { get; set; }
    public int IdCreditoProveedor { get; set; }
    public int? IdCompra { get; set; }
    public int IdTipoMovimiento { get; set; }
    public string TipoMovimiento { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? Observacion { get; set; }
    public bool Estado { get; set; }
}
