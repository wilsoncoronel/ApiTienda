using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; }
        public string Documento { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdEstado { get; set; }
        public EstadoCompra EstadoCompra { get; set; }
        public bool EstadoVisual { get; set; }
        public List<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();
        public double ValorIva { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public int IdUsuarioCreador { get; set; }
        public Usuario UsuarioCreador { get; set; }
    }
}
