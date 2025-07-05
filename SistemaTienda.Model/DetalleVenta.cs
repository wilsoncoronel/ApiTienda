using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class DetalleVenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public double ValorCompra { get; set; }
        public int IdArticulo { get; set; }
        public Articulo Articulo { get; set; }
        public double ValorTotal { get; set; }
        public double ImpuestoValor { get; set; }
    }
}