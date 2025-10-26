using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComCliente
{
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public bool EstadoVisual { get; set; }

    public bool Estado { get; set; }

    public virtual TbGrlPersona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<TbVenta> TbVenta { get; set; } = new List<TbVenta>();
}
