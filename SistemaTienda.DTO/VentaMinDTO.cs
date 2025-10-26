using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class VentaMinDTO
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public ClienteMinDTO ClienteMinDTO { get; set; }
        public DateTime FechaVenta { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public EstadoVentaDTO EstadoVentaDTO { get; set; }
        public UsuarioMinDTO UsuarioMinDTO { get; set; }
    }
}
