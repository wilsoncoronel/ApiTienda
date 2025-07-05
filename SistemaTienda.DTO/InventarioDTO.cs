using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class InventarioDTO
    {
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public ArticuloDTO Articulo { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCreacion { get; set; }

        public double PrecioUnitario { get; set; }

        public TransaccionInventarioDTO TransaccionInventario { get; set; }

        public int IdTransaccion { get; set; }
    }
}
