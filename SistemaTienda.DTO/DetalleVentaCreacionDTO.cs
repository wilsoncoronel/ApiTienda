using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleVentaCreacionDTO
    {
        public int IdVenta { get; set; }
        public int IdArticulo { get; set; }
        public string Codigo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenta { get; set; }
        public decimal ValotTotal { get; set; }
        public decimal ImpuestoValor { get; set; }
    }
}
