using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DireccionDTO
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdCiudad { get; set; }
        public CiudadDTO Ciudad { get; set; }
        public string Descripcion { get; set; }
        public bool EstadoVisual { get; set; }
    }
}
