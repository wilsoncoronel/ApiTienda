using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ClienteEditarDTO: ClienteCreacionDTO
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
