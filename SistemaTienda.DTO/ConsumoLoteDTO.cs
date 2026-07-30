using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ConsumoLoteDTO
    {
        public int Id { get; set; }
        public int IdMovimiento { get; set; }
        public int IdDetalleVenta { get; set; }
        public int IdLote { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool? Estado { get; set; }
        public DetalleVentaDTO DetalleVentaDTO { get; set; } = null!;
    }
}
