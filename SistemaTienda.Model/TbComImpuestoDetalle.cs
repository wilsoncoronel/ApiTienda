using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComImpuestoDetalle
{
    public int Id { get; set; }

    public int IdDetalleCompra { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoCalculo { get; set; } = null!;

    public decimal Valor { get; set; }

    public virtual TbComDetallesCompra IdDetalleCompraNavigation { get; set; } = null!;
}
