using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComImpuestoDetalle
{
    public int Id { get; set; }

    public int IdDetalleCompra { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdTipoCalculo { get; set; }

    public decimal Valor { get; set; }

    public virtual TbComDetallesCompra IdDetalleCompraNavigation { get; set; } = null!;

    public virtual TbComTipoCalculoImpuesto IdTipoCalculoNavigation { get; set; } = null!;
}
