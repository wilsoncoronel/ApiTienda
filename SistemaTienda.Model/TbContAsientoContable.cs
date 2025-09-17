using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbContAsientoContable
{
    public int Id { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Concepto { get; set; } = null!;

    public int? IdCompra { get; set; }

    public int? IdVenta { get; set; }

    public virtual TbCompra? IdCompraNavigation { get; set; }

    public virtual TbVenta? IdVentaNavigation { get; set; }

    public virtual ICollection<TbContPartidaContable> TbContPartidaContables { get; set; } = new List<TbContPartidaContable>();
}
