using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ArticuloVentaDTO: ArticuloCompraDTO
    {
        public string Lote { get; set; }
        public string Codigo { get; set; }
    }
}
