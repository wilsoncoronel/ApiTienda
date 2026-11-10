namespace SistemaTienda.DTO
{
    public class AjusteLoteDTO
    {
        public int Id { get; set; }

        public int IdLote { get; set; }

        public string NumeroLote { get; set; } = null!;

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public bool Estado { get; set; }
    }
}
