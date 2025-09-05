using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ProveedorMinDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Descripcion { get; set; }
        public string Identificacion { get; set; }
        public string Mail { get; set; }
    }
}
