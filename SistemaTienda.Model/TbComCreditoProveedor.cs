using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComCreditoProveedor
{
    public int Id { get; set; }

    public int IdDevolucionCompra { get; set; }

    public decimal ValorOriginal { get; set; }

    public decimal ValorDisponible { get; set; }

    public DateTime FechaCreacion { get; set; }

    public bool Estado { get; set; }

    public virtual TbComDevolucionCompra IdDevolucionCompraNavigation { get; set; } = null!;

    public virtual ICollection<TbComCreditoProveedorMovimiento> TbComCreditoProveedorMovimientos { get; set; } = new List<TbComCreditoProveedorMovimiento>();
}
