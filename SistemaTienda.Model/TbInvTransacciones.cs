using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvTransacciones
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Signo { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<TbCompra> TbCompras { get; set; } = new List<TbCompra>();

    public virtual ICollection<TbInvMovimiento> TbInvMovimientos { get; set; } = new List<TbInvMovimiento>();

    public virtual ICollection<TbVenta> TbVenta { get; set; } = new List<TbVenta>();
}
