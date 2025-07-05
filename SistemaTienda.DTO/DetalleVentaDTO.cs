using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public double ValorCompra { get; set; }
        public int ArticuloId { get; set; }
        public ArticuloDTO Articulo { get; set; }
        public double ValorTotal { get; set; }
        public double ImpuestoValor { get; set; }
    }
}
