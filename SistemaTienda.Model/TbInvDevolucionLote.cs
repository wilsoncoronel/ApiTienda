using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvDevolucionLote
{
    public int Id { get; set; }

    public int IdMovimiento { get; set; }

    public int IdDetalleDevolucionCompra { get; set; }

    public int IdLote { get; set; }

    public decimal Cantidad { get; set; }

    public decimal CostoUnitario { get; set; }

    public bool Estado { get; set; }

    public virtual TbComDetalleDevolucionCompra IdDetalleDevolucionCompraNavigation { get; set; } = null!;

    public virtual TbInvLote IdLoteNavigation { get; set; } = null!;

    public virtual TbInvMovimiento IdMovimientoNavigation { get; set; } = null!;
}
