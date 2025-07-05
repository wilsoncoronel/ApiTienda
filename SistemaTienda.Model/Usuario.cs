using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public Persona Persona { get; set; }
        public int IdPersona { get; set; }
        public int IdRol { get; set; }
        public Rol Rol { get; set; }
    }
}
