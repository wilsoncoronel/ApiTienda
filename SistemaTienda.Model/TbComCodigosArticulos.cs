using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComCodigosArticulos
{
    public int Id { get; set; }

    public int IdArticulo { get; set; }

    public string? Codigo { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;
}
