using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ArticuloImpuestoMinDTO
    {
        public int Id { get; set; }
        public int IdImpuesto { get; set; }
        public bool Estado { get; set; }
        public ImpuestoDTO ImpuestoDTO { get; set; } = null!;
    }
}
