using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbSisUsuario
{
    public int Id { get; set; }

    public int IdPersona { get; set; }

    public int IdRol { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual TbGrlPersona IdPersonaNavigation { get; set; } = null!;

    public virtual TbSisRol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<TbComArticulo> TbComArticulos { get; set; } = new List<TbComArticulo>();

    public virtual ICollection<TbCompra> TbCompras { get; set; } = new List<TbCompra>();

    public virtual ICollection<TbPedidos> TbPedidos { get; set; } = new List<TbPedidos>();

    public virtual ICollection<TbVenta> TbVenta { get; set; } = new List<TbVenta>();
}
