using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleInventarioDTO
    {
        public int Id { get; set; }

        public int IdInventario { get; set; }

        public int IdArticulo { get; set; }

        public int IdTransaccionInventario { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public ArticuloDTO ArticuloDTO { get; set; } = null!;
        public TransaccionInventarioDTO TransaccionesDTO { get; set; } = null!;
    }
}
