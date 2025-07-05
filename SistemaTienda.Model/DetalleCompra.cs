using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class DetalleCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!!")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!!")]
        public double ValorCompra { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!!")]
        public int IdArticulo { get; set; }
        public Articulo Articulo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!!")]
        public double ValorTotal { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido!!")]
        public double ImpuestoValor { get; set; }

    }
}
