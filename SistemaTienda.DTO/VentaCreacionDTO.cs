using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class VentaCreacionDTO
    {
        public int IdCliente { get; set; }
        public string Documento { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdEstado { get; set; }
        public EstadoVenta EstadoVenta { get; set; }
        public bool EstadoVisual { get; set; }
        public List<DetalleVentaCreacionDTO> DetalleVenta { get; set; } = [];
        public double ValorIva { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public int UsuarioCreadorId { get; set; }
    }
}