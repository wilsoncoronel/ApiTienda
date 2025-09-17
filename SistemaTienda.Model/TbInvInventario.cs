using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvInventario
{
    public int Id { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int IdCompra { get; set; }

    public DateTime? FechaReversion { get; set; }

    public virtual TbCompra IdCompraNavigation { get; set; } = null!;

    public virtual ICollection<TbDetallesInventario> TbDetallesInventarios { get; set; } = new List<TbDetallesInventario>();
}
