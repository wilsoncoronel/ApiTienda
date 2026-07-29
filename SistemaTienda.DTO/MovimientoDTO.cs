using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class MovimientoDTO
    {
        public int Id { get; set; }
        public int IdMovimientoOrigen { get; set; }
        public int IdTransaccion { get; set; }
        public TransaccionInventarioDTO TransaccionDTO { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Referencia { get; set; }
    }
}