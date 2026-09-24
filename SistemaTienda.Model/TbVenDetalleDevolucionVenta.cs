using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenDetalleDevolucionVenta
{
    public int Id { get; set; }

    public int IdDevolucionVenta { get; set; }

    public int IdDetalleVenta { get; set; }

    public int Cantidad { get; set; }

    public bool Estado { get; set; }

    public string Motivo { get; set; } = null!;

    public virtual TbVenDetalleVenta IdDetalleVentaNavigation { get; set; } = null!;

    public virtual TbVenDevolucionVenta IdDevolucionVentaNavigation { get; set; } = null!;
}
