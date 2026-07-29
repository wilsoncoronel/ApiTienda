using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class InventarioLoteDTO
    {
        public int Id { get; set; }

        public int IdMovimiento { get; set; }

        public int IdArticulo { get; set; }
        public ArticuloMinDTO ArticuloDTO { get; set; } = null!;

        public string NumeroLote { get; set; } = null!;

        public string? Codigo { get; set; }

        public DateTime FechaIngreso { get; set; }

        public decimal StockDisponible { get; set; }

        public decimal StockMinimo { get; set; }

        public decimal CostoUnitario { get; set; }

        public DateOnly? FechaExpiracion { get; set; }

        public bool Estado { get; set; }
    }
}
