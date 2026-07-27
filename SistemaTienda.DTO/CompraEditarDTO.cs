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
        public DateTime? FechaModificacion { get; set; }
        public int IdEstado { get; set; }
        public bool EstadoVisual { get; set; }
        public int IdUsuarioCreador { get; set; }
        public List<DetalleCompraEditarDTO> DetalleComprasEditarDto { get; set; } = [];
    }
}
