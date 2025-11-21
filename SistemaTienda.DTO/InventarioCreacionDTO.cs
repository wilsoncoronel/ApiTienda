using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class InventarioCreacionDTO
    {
        public DateTime FechaCreacion { get; set; }
        public List<DetalleInventarioCreacionDTO> ListaDetallesDTO { get; set; } = [];
    }
}
