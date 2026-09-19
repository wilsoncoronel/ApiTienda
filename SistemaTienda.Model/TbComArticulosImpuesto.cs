using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComArticulosImpuesto
{
    public int Id { get; set; }

    public int IdArticulo { get; set; }

    public int IdImpuesto { get; set; }

    public bool Estado { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;

    public virtual TbComImpuesto IdImpuestoNavigation { get; set; } = null!;
}
