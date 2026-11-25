namespace SistemaTienda.DTO
{
    public class DetallesPagoVentaCompletoDTO
    {
        public int Id { get; set; }
        public int IdPagoVenta { get; set; }
        public int IdTipoPago { get; set; }
        public decimal Valor { get; set; }
        public string? NumeroComprobante { get; set; }
        public DateTime FechaPago { get; set; }
        public string? Nota { get; set; }
        public bool Estado { get; set; }
        public TipoPagoDTO TipoPagoDTO { get; set; } = null!;
    }
}