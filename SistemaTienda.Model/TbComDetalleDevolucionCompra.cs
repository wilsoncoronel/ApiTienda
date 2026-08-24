using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComDetalleDevolucionCompra
{
    public int Id { get; set; }

    public int IdDevolucionCompra { get; set; }

    public int IdDetalleCompra { get; set; }

    public int Cantidad { get; set; }

    public bool Estado { get; set; }

    public virtual TbComDetallesCompra IdDetalleCompraNavigation { get; set; } = null!;

    public virtual TbComDevolucionCompra IdDevolucionCompraNavigation { get; set; } = null!;
}
