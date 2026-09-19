using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ArticuloImpuestoDTO
    {
            public int Id { get; set; }

            public int IdArticulo { get; set; }

            public int IdImpuesto { get; set; }

            public bool Estado { get; set; }

            public ArticuloDTO ArticuloDTO { get; set; } = null!;

            public ImpuestoDTO ImpuestoDTO { get; set; } = null!;
    }
}
