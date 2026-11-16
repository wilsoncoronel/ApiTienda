using System.Collections.Generic;

namespace SistemaTienda.DTO
{
    public class ArticuloLoteInventarioDTO : LoteInventarioDTO
    {
        public DateOnly? FechaExpiracion { get; set; }
        public string NombreArticulo { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string UnidadMedida { get; set; } = null!;
        public decimal? Unidad { get; set; }
        public decimal PrecioCompra { get; set; }
        public string NombreCompuesto { get; set; } = null!;
        public List<DetalleImpuestoDTO> Impuestos { get; set; } = new();
    }
}
