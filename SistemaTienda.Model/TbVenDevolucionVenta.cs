using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenDevolucionVenta
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaReversion { get; set; }

    public string Motivo { get; set; } = null!;

    public int Estado { get; set; }

    public virtual TbVenta IdVentaNavigation { get; set; } = null!;

    public virtual ICollection<TbVenDetalleDevolucionVenta> TbVenDetalleDevolucionVenta { get; set; } = new List<TbVenDetalleDevolucionVenta>();
}
