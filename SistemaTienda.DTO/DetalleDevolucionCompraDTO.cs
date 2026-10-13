using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleDevolucionCompraDTO
    {
        public int Id { get; set; }
        public int IdDetalleCompra { get; set; }
        public DetalleCompraDTO DetalleCompraDTO { get; set; }
        public int Cantidad { get; set; }

        public bool Estado { get; set; }
    }
}
