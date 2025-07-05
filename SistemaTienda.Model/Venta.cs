using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]

        public int IdCliente { get; set; }
        [Required]
        public Cliente Cliente { get; set; }
        [Required]
        public string Documento { get; set; }
        [Required]
        public DateTime FechaVenta { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public int IdEstado { get; set; }
        public EstadoVenta EstadoVenta { get; set; }
        [Required]
        public bool EstadoVisual { get; set; }
        public List<DetalleVenta> DetalleVenta { get; set; } = [];
        public double ValorIva { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public int UsuarioCreadorId { get; set; }
        public Usuario UsuarioCreador { get; set; }
    }
}
