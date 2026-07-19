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
        public int IdCompra { get; set; }

        public int IdArticulo { get; set; }

        public string? NumeroLote { get; set; }

        public string? Codigo { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Cantidad { get; set; }

        public decimal ValorCompra { get; set; }

        public decimal ValorVenta { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal ImpuestoValor { get; set; }

        public DateOnly? FechaExpiracion { get; set; }
    }
}
