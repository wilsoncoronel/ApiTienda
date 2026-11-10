namespace SistemaTienda.DTO
{
    public class LoteInventarioDTO
    {
        public int IdLote { get; set; }

        public int IdArticulo { get; set; }

        public string NumeroLote { get; set; } = null!;

        public string? Codigo { get; set; }

        public decimal StockDisponible { get; set; }
    }
}
