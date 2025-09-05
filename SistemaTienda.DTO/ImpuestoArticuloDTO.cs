using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ImpuestoArticuloDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal? ValorImpuesto { get; set; }
        public string Descripcion { get; set; }
        public int IdEstado { get; set; }
        public EstadoImpuestoDTO EstadoImpuesto { get; set; }
    }
}
