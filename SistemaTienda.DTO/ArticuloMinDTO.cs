using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ArticuloMinDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public ImpuestoArticuloDTO ImpuestoArticuloDto { get; set; }
        public bool? Papeleria { get; set; }
    }
}
