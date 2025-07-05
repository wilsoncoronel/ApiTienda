using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class ImpuestoArticulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double ValorImpuesto { get; set; }
        public string Descripcion { get; set; }
        public int IdEstado { get; set; }
        public EstadoImpuesto EstadoImpuesto { get; set; }

        public List<Articulo> Articulos { get; set; } = [];
    }
}
