using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleImpuestoCreacionDTO
    {
        public int IdImpuesto { get; set; }
        public int IdArticulo { get; set; }
        public string Nombre { get; set; } = null!;
        public string TipoCalculo { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}
