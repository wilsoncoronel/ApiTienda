using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVentEstadosPago
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbVenPagoVenta> TbVenPagoVenta { get; set; } = new List<TbVenPagoVenta>();
}
