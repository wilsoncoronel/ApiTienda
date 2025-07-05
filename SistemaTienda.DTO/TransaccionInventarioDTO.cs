using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class TransaccionInventarioDTO
    {
        public int Id { get; set; }
        public int Nombre { get; set; }
        public int Signo { get; set; }
        public bool Estado { get; set; }
    }
}
