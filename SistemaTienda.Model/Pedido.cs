using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }
        [StringLength(1000, ErrorMessage ="El campo {0} debe tener al menos {1} caracteres!")]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!!")]
        public DateTime FechaEntrega { get; set; }
        public Usuario UsuarioCreador { get; set; }
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!!")]
        public bool Estado { get; set; }
        public List<DetallePedido> DetallePedido { get; set; } = [];
        public int IdEstadoPedido { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
    }
}
