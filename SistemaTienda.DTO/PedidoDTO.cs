using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public ProveedorDTO Proveedor { get; set; }
        [StringLength(1000, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres!")]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!!")]
        public DateTime FechaEntrega { get; set; }
        public UsuarioDTO UsuarioCreadorDto { get; set; }
        public int IdUsuarioCreador { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!!")]
        public bool Estado { get; set; }
        public List<DetallePedidoDTO> DetallePedidoDto { get; set; } = [];
        public int IdEstadoPedido { get; set; }
        public EstadoPedidoDTO EstadoPedidoDto { get; set; }
    }
}
