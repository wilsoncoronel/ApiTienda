using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbDetallesInventario
{
    public int Id { get; set; }

    public int IdInventario { get; set; }

    public int IdArticulo { get; set; }

    public int IdTransaccionInventario { get; set; }

    public int Cantidad { get; set; }

    public decimal? PrecioCompra { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;

    public virtual TbInvInventario IdInventarioNavigation { get; set; } = null!;

    public virtual TbInvTransacciones IdTransaccionInventarioNavigation { get; set; } = null!;
}
