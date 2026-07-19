using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class DetalleCompraEditarDTO
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string? NumeroLote { get; set; }
        public string? Codigo { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenta { get; set; }
        public int ArticuloId { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ImpuestoValor { get; set; }
        public DateOnly? FechaCaducidad { get; set; }
    }
}
