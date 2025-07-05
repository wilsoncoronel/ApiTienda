using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ImpuestoArticuloCreacionDTO
    {
        public string Nombre { get; set; }
        public double ValorImpuesto { get; set; }
        public string Descripcion { get; set; }
        public int IdEstado { get; set; }
    }
}
