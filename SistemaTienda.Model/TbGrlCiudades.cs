using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbGrlCiudades
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbGrlDirecciones> TbGrlDirecciones { get; set; } = new List<TbGrlDirecciones>();
}
