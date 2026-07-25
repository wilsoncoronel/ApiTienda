using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvConsumoLote
{
    public int Id { get; set; }

    public int IdMovimiento { get; set; }

    public int IdDetalleVenta { get; set; }

    public int IdLote { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public bool? Estado { get; set; }

    public virtual TbVenDetalleVenta IdDetalleVentaNavigation { get; set; } = null!;

    public virtual TbInvLote IdLoteNavigation { get; set; } = null!;

    public virtual TbInvMovimiento IdMovimientoNavigation { get; set; } = null!;
}
