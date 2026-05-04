using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComCodigosProducto
{
    public int Id { get; set; }

    public int IdProducto { get; set; }

    public string? Codigo { get; set; }

    public virtual TbComArticulo IdProductoNavigation { get; set; } = null!;
}
