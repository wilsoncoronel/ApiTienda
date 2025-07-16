using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class SesionDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = null!;

        public string Clave { get; set; } = null!;
        //public string Correo { get; set; } = null!;
        public RolDTO RolDto { get; set; }

    }
}
