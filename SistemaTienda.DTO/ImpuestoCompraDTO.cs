namespace SistemaTienda.DTO
{
    public class ImpuestoCompraDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int IdTipoCalculo { get; set; }

        public decimal Valor { get; set; }
    }
}
