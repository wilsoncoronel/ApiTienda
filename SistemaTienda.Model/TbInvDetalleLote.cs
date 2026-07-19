using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvDetalleLote
{
    public int Id { get; set; }

    public int IdLote { get; set; }

    public int IdArticulo { get; set; }

    public decimal Cantidad { get; set; }

    public string? Codigo { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;

    public virtual TbInvLote IdLoteNavigation { get; set; } = null!;
}
