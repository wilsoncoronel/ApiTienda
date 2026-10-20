using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComDetalleDevolucionCompra
{
    public int Id { get; set; }

    public int IdDevolucionCompra { get; set; }

    public int IdArticulo { get; set; }

    public decimal Valor { get; set; }

    public int Cantidad { get; set; }

    public bool Estado { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;

    public virtual TbComDevolucionCompra IdDevolucionCompraNavigation { get; set; } = null!;

    public virtual ICollection<TbComImpuestoDetalleDevolucionCompra> TbComImpuestoDetalleDevolucionCompras { get; set; } = new List<TbComImpuestoDetalleDevolucionCompra>();

    public virtual ICollection<TbInvDevolucionLote> TbInvDevolucionLotes { get; set; } = new List<TbInvDevolucionLote>();
}
