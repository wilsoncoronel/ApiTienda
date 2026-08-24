using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComDevolucionCompra
{
    public int Id { get; set; }

    public int IdCompra { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaReversion { get; set; }

    public int Estado { get; set; }

    public string Motivo { get; set; } = null!;

    public virtual TbCompra IdCompraNavigation { get; set; } = null!;

    public virtual ICollection<TbComDetalleDevolucionCompra> TbComDetalleDevolucionCompras { get; set; } = new List<TbComDetalleDevolucionCompra>();
}
