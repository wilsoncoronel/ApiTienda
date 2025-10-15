using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleVentaEditarDTO
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorCompra { get; set; }
        public int ArticuloId { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ImpuestoValor { get; set; }
        public ArticuloDTO ArticuloDetalleDto { get; set; }
    }
}
