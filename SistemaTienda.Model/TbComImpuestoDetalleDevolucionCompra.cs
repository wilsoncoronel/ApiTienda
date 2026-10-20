using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComImpuestoDetalleDevolucionCompra
{
    public int Id { get; set; }

    public int IdDetalleDevolucionCompra { get; set; }

    public int IdImpuesto { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoCalculo { get; set; } = null!;

    public decimal Valor { get; set; }

    public bool Estado { get; set; }

    public virtual TbComDetalleDevolucionCompra IdDetalleDevolucionCompraNavigation { get; set; } = null!;
}
