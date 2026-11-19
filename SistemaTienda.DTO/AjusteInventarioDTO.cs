namespace SistemaTienda.DTO
{
    public class AjusteInventarioDTO : AjusteInventarioCrearDTO
    {
        public int Id { get; set; }
        public UsuarioMinDTO UsuarioDTO{ get; set; }
        public EstadosAjusteDTO EstadosAjusteDTO { get; set; }
        public TransaccionInventarioDTO transaccionInventarioDTO{ get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaReversion { get; set; }
    }
}
