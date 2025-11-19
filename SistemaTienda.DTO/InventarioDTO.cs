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

        public DateTime FechaCreacion { get; set; }

        public int? IdCompra { get; set; }

        public int? IdVenta { get; set; }

        public DateTime? FechaReversion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public CompraMinDTO? CompraDTO { get; set; }

        public VentaMinDTO? VentaDTO { get; set; }
    }
}
