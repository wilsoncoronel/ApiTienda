using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class Inventario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public double PrecioUnitario { get; set; }
        public TransaccionInventario TransaccionInventario { get; set; }
        public int IdTransaccion { get; set; }
    }
}
