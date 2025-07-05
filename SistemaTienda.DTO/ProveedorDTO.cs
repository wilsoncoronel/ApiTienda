using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Mail { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public bool EstadoVisual { get; set; }
        public string RazonSocial { get; set; }
        public string Descripcion { get; set; }
    }
}
