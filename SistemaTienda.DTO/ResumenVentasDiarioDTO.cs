using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ResumenVentasDiarioDTO
    {
        public ArticuloMinDTO Articulo { get; set; }
        public TransaccionInventarioDTO TransaccionesDTO { get; set; }
        public int CantidadVendida { get; set; }
        public decimal ValorVenta { get; set; }
        public decimal ValorCompra { get; set; }
        public List<DetalleImpuestoDTO> Impuestos { get; set; } = [];
    }
}
