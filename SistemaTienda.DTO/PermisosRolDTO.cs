using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class PermisosRolDTO
    {
        public int Id { get; set; }
        public RolDTO Rol { get; set; }
        public int IdRol { get; set; }
        public int IdMenu { get; set; }
        public MenuDTO Menu { get; set; }
        public bool EstadoVisual { get; set; }
    }
}
