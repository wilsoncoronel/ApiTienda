using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ImpuestoCrearDTO
    {
        public int IdImpuesto { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdTipoCalculo { get; set; }
        public decimal Valor { get; set; }
        public bool Estado { get; set; }
    }
}
