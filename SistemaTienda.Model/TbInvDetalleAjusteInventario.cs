using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvDetalleAjusteInventario
{
    public int Id { get; set; }

    public int IdAjusteInventario { get; set; }

    public int IdLote { get; set; }

    public int Cantidad { get; set; }

    public decimal CostoUnitario { get; set; }

    public bool Estado { get; set; }

    public virtual TbInvAjusteInventario IdAjusteInventarioNavigation { get; set; } = null!;

    public virtual TbInvLote IdLoteNavigation { get; set; } = null!;
}
