using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbGrlDirecciones
{
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public int IdCiudad { get; set; }

    public string? Descripcion { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual TbGrlCiudades IdCiudadNavigation { get; set; } = null!;

    public virtual TbGrlPersona IdPersonaNavigation { get; set; } = null!;
}
