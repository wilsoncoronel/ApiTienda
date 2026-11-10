namespace SistemaTienda.DTO
{
    public class ImpuestoDetalleAjusteDTO
    {
        public int Id { get; set; }

        public int IdDetalleAjuste { get; set; }

        public int IdTipoCalculo { get; set; }

        public int IdImpuesto { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal Valor { get; set; }

        public bool Estado { get; set; }
    }
}
