using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenPagoVenta
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public decimal TotalVenta { get; set; }

    public decimal TotalPagado { get; set; }

    public decimal SaldoPendiente { get; set; }

    public int IdEstadoPago { get; set; }

    public string Nota { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaPago { get; set; }

    public virtual TbVentEstadosPago IdEstadoPagoNavigation { get; set; } = null!;

    public virtual TbVenta IdVentaNavigation { get; set; } = null!;

    public virtual ICollection<TbVenDetallePago> TbVenDetallePagos { get; set; } = new List<TbVenDetallePago>();
}
