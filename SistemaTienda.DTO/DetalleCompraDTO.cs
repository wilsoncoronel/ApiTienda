using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleCompraDTO
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorCompra { get; set; }

        public ArticuloDTO Articulo { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ImpuestoValor { get; set; }
    }
}
