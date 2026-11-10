namespace SistemaTienda.DTO
{
    public class AjusteInventarioDTO : AjusteInventarioCrearDTO
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaReversion { get; set; }
    }
}
