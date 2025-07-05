using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ApiTienda.Model
{
    public class Direccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public Persona Persona { get; set; }

        public int  IdCiudad { get; set; }
        public Ciudad Ciudad { get; set; }
        public string Descripcion { get; set; }
        public bool EstadoVisual { get; set; }
    }
}