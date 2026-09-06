using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class UnidadCreacionDTO
    {
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public bool EstadoVisual { get; set; }
    }
}
