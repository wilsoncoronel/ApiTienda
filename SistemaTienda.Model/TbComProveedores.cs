using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComProveedores
{
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool EstadoVisual { get; set; }

    public bool Estado { get; set; }

    public virtual TbGrlPersona IdPersonaNavigation { get; set; } = null!;

    public virtual ICollection<TbCompra> TbCompras { get; set; } = new List<TbCompra>();

    public virtual ICollection<TbPedidos> TbPedidos { get; set; } = new List<TbPedidos>();
}
