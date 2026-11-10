namespace SistemaTienda.DTO
{
    public class EstadosAjusteDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public bool EstadoVisual { get; set; }

        public string Descripcion { get; set; } = null!;
    }
}
