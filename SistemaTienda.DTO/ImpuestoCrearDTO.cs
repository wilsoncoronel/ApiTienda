using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ImpuestoCrearDTO
    {
        public string Nombre { get; set; } = null!;

        public string TipoCalculo { get; set; } = null!;

        public decimal Valor { get; set; }

        public bool Estado { get; set; }
    }
}
