using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenTipoPago
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

    public bool EstadiVisual { get; set; }

    public virtual ICollection<TbVenDetallePago> TbVenDetallePagos { get; set; } = new List<TbVenDetallePago>();
}
