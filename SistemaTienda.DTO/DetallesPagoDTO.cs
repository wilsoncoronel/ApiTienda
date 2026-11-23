namespace SistemaTienda.DTO
{
    public class DetallesPagoDTO : DetallePagoCreacionDTO
    {
        public int Id { get; set; }
        public int IdPagoVenta { get; set; }
        public TipoPagoDTO TipoPagoDTO { get; set; } = null!;
    }
}
