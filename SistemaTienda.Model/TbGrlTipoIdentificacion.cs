using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbGrlTipoIdentificacion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbGrlPersona> TbGrlPersonas { get; set; } = new List<TbGrlPersona>();
}
