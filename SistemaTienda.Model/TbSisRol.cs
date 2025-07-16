using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbSisRol
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbSisPermisosRol> TbSisPermisosRols { get; set; } = new List<TbSisPermisosRol>();

    public virtual ICollection<TbSisUsuario> TbSisUsuarios { get; set; } = new List<TbSisUsuario>();
}
