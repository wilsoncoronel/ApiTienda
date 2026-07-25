using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class VentaCreacionDTO
    {
        public int IdCliente { get; set; }
        public string Documento { get; set; }
        public int? IdTransaccion { get; set; }

        public DateTime FechaCompra { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdEstado { get; set; }
        public bool EstadoVisual { get; set; }
        [Required]
        public List<DetalleVentaCreacionDTO> DetalleVentaCreacionDto { get; set; }
        public int UsuarioCreadorId { get; set; }
    }
}