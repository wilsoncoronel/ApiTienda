using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class CompraEditarDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public string Documento { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdEstado { get; set; }
        public bool EstadoVisual { get; set; }
        public List<DetalleCompraCreacionDTO> DetalleComprasCreacionDto { get; set; } = [];
        public double ValorIva { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public int IdUsuarioCreador { get; set; }
    }
}
