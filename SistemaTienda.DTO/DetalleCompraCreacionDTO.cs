using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleCompraCreacionDTO
    {
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorCompra { get; set; }

        public decimal? ValorVenta { get; set; }
        public int ArticuloId { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ImpuestoValor { get; set; }
    }
}
