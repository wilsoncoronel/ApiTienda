using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class UsuarioEditarDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Mail { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public bool EstadoVisual { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public int IdPersona { get; set; }
        public int IdCiudad { get; set; }
        public int IdRol { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public bool Estado { get; set; }
        public DireccionEdicionDTO DireccionEdicionDto { get; set; }
    }
}
