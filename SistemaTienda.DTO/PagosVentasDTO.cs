namespace SistemaTienda.DTO
{
    public class PagosVentasDTO : PagoVentaCreacionDTO
    {
        public int Id { get; set; }
        public EstadosPagoDTO EstadosPagoDTO { get; set; } = null!;
        public VentaDTO VentaDTO { get; set; } = null!;
    }
}
