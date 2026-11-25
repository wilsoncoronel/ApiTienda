namespace SistemaTienda.DTO
{
    public class PagoVentaCompletoDTO
    {
        public int Id { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public int IdEstadoPago { get; set; }
        public string Nota { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaPago { get; set; }
        public EstadosPagoDTO EstadosPagoDTO { get; set; } = null!;
        public List<DetallesPagoVentaCompletoDTO> DetallesPago { get; set; } = [];
    }
}
