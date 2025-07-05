using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class Articulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaCaducidad { get; set; }

        public bool EstadoVisual { get; set; }
        public bool Estado { get; set; }

        public string Descripcion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Unidad { get; set; }
        public double UnidadValor { get; set; }
        public Marca Marca { get; set; }
        public int IdMarca { get; set; }
        public int IdTipoArticulo { get; set; }
        public TipoArticulo TipoArticulo { get; set; }
        public Usuario UsuarioCreador { get; set; }
        public int IdUsuarioCreador { get; set; }
        public int IdImpuesto { get; set; }
        public ImpuestoArticulo ImpuestoArticulo { get; set; }
    }
}
