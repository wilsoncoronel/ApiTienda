using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class PersonaCompletoDTO
    {
        public int Id { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Mail { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public TipoIdentificacionDTO TipoIdentificacionDTO { get; set; } = null!;
        public DireccionDTO? DireccionesDTO { get; set; }
        public bool EsUsuario { get; set; }
        public bool EsCliente { get; set; }
        public bool EsProveedor { get; set; }
        // Cliente
        public int IdCliente { get; set; }
        public bool EstadoCliente { get; set; }
        public bool EstadoVisualCliente { get; set; }
        // Proveedor
        public int IdProveedor { get; set; }
        public bool EstadoVisualProveedor { get; set; }
        public string RazonSocial { get; set; }
        public string Descripcion { get; set; }
        public bool EstadoProveedor { get; set; }
        //Usuario
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public RolDTO Rol { get; set; }
        public bool? EstadoUsuario { get; set; }
    }
}
