using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleImpuestoDTO: DetalleImpuestoCreacionDTO
    {
        public int Id { get; set; }
        public int IdDetalleCompra { get; set; }
    }
}
