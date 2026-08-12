using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ArticuloEdicionDTO
    {
        public int Id { get; set; }

        public int IdMarca { get; set; }

        public int IdTipoArticulo { get; set; }

        public int IdUsuarioCreador { get; set; }

        public int IdImpuesto { get; set; }
        public int? IdPorcentajeGanancia { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Codigo { get; set; }

        public DateTime? FechaCaducidad { get; set; }

        public bool EstadoVisual { get; set; }

        public bool Estado { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public string? Unidad { get; set; }

        public decimal? UnidadValor { get; set; }

        public decimal ValorCompra { get; set; }

        public decimal ValorVenta { get; set; }
        public bool? Papeleria { get; set; }
    }
}
