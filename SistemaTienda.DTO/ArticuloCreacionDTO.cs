using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ArticuloCreacionDTO
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdUsuarioCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public bool EstadoVisual { get; set; }
        public bool Estado { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorVenta { get; set; }
        public decimal UnidadValor { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Unidad { get; set; }
        public int IdMarca { get; set; }
        public int IdTipoArticulo { get; set; }
        public int IdImpuesto { get; set; }
        public bool? Papeleria { get; set; }
        public List<CodigoArticuloCreacionDTO> ListaCodigosArticulosDTO { get; set; }
    }
}
