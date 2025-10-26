using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public ClienteDTO Cliente { get; set; }
        public string Documento { get; set; }
        public DateTime FechaVenta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int IdEstado { get; set; }
        public EstadoVentaDTO EstadoVenta { get; set; }
        public bool EstadoVisual { get; set; }
        public List<DetalleVentaDTO> DetalleVenta { get; set; } = [];
        public int UsuarioCreadorId { get; set; }
        public UsuarioDTO UsuarioCreador { get; set; }
    }
}
