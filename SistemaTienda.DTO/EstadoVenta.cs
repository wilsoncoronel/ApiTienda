using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class EstadoVenta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool EstadoVisual { get; set; }
        public string Descripcion { get; set; }
    }
}
