using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenDetallePago
{
    public int Id { get; set; }

    public int IdPagoVenta { get; set; }

    public int IdTipoPago { get; set; }

    public decimal Valor { get; set; }

    public string? NumeroComprobante { get; set; }

    public DateTime FechaPago { get; set; }

    public string? Nota { get; set; }

    public bool Estado { get; set; }

    public virtual TbVenPagoVenta IdPagoVentaNavigation { get; set; } = null!;

    public virtual TbVenTipoPago IdTipoPagoNavigation { get; set; } = null!;
}
