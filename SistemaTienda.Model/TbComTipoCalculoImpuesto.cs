using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComTipoCalculoImpuesto
{
    public int Id { get; set; }

    public string TipoCalculo { get; set; } = null!;

    public bool Estado { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbComImpuestoDetalleDevolucionCompra> TbComImpuestoDetalleDevolucionCompras { get; set; } = new List<TbComImpuestoDetalleDevolucionCompra>();

    public virtual ICollection<TbComImpuestoDetalle> TbComImpuestoDetalles { get; set; } = new List<TbComImpuestoDetalle>();

    public virtual ICollection<TbComImpuesto> TbComImpuestos { get; set; } = new List<TbComImpuesto>();
}
