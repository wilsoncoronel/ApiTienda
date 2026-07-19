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

        public DateTime FechaIngreso { get; set; }

        public virtual ICollection<InventarioLoteDTO> InventarioLotesDTO { get; set; }= [];
    }
}
