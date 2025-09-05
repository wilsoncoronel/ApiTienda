using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ArticuloDTO
    {
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
        public decimal? UnidadValor { get; set; }
        public decimal ValorVenta { get; set; }
        public decimal ValorCompra { get; set; }
        public int IdMarca { get; set; }
        public MarcaDTO MarcaDTO { get; set; }
        public int IdTipoArticulo { get; set; }
        public TipoArticuloDTO TipoArticuloDTO { get; set; }
        public ImpuestoArticuloDTO ImpuestoArticuloDto { get; set; }
        public int IdImpuesto { get; set; }
    }
}
