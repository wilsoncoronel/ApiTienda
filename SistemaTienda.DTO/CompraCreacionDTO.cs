using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class CompraCreacionDTO
    {
        public int IdProveedor { get; set; }
        public string Documento { get; set; }
        public DateTime FechaCompra { get; set; }
        public int IdEstado { get; set; }
        public bool EstadoVisual { get; set; }
        public List<DetalleCompraCreacionDTO> DetalleComprasCreacionDto   { get; set; } = [];
        public decimal ValorIva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public int IdUsuarioCreador { get; set; }
    }
}
