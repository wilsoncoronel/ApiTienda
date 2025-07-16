using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbSisMenu
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Url { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbSisPermisosRol> TbSisPermisosRols { get; set; } = new List<TbSisPermisosRol>();
}
