using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleDevolucionCompraDTO
    {
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public ArticuloDTO ArticuloDTO { get; set; } = null!;
        public decimal Valor { get; set; }
        public int Cantidad { get; set; }
        public bool Estado { get; set; }
        public List<ImpuestoDetalleDevolucionDTO> Impuestos { get; set; } = new List<ImpuestoDetalleDevolucionDTO>();
    }
}
