using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvMovimiento
{
    public int Id { get; set; }

    public int IdTransaccionInventario { get; set; }

    public int? IdCompra { get; set; }

    public int? IdVenta { get; set; }

    public DateTime Fecha { get; set; }

    public string? Referencia { get; set; }

    public virtual TbCompra? IdCompraNavigation { get; set; }

    public virtual TbInvTransacciones IdTransaccionInventarioNavigation { get; set; } = null!;

    public virtual TbVenta? IdVentaNavigation { get; set; }

    public virtual ICollection<TbInvLote> TbInvLotes { get; set; } = new List<TbInvLote>();
}
