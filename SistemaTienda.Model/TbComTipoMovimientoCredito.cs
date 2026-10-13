using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComTipoMovimientoCredito
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<TbComCreditoProveedorMovimiento> TbComCreditoProveedorMovimientos { get; set; } = new List<TbComCreditoProveedorMovimiento>();
}
