using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComCreditoProveedorMovimiento
{
    public int Id { get; set; }

    public int IdCreditoProveedor { get; set; }

    public int? IdCompra { get; set; }

    public int IdTipoMovimiento { get; set; }

    public decimal Valor { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string? Observacion { get; set; }

    public bool Estado { get; set; }

    public virtual TbCompra? IdCompraNavigation { get; set; }

    public virtual TbComCreditoProveedor IdCreditoProveedorNavigation { get; set; } = null!;

    public virtual TbComTipoMovimientoCredito IdTipoMovimientoNavigation { get; set; } = null!;
}
