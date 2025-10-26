using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ClienteCreacionDTO
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Mail { get; set; }
        public string Identificacion { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string Telefono { get; set; }
        public bool EstadoVisual { get; set; }
        public bool Estado { get; set; }
        public DireccionCreacionDTO DireccionCreacionDto { get; set; }
    }
}
