using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class PorcentajeGananciaDTO
    {
        public int Id { get; set; }
        public string PorcentajeGanancia { get; set; } = null!;
        public decimal Valor { get; set; }
        public bool EstadoVisual { get; set; }
    }
}
