using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbSisPermisosRol
{
    public int Id { get; set; }

    public int IdRol { get; set; }

    public int IdMenu { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual TbSisMenu IdMenuNavigation { get; set; } = null!;

    public virtual TbSisRol IdRolNavigation { get; set; } = null!;
}
