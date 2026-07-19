using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvInventarioLote
{
    public int Id { get; set; }

    public int IdMovimiento { get; set; }

    public int IdArticulo { get; set; }

    public string NumeroLote { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public int StockActual { get; set; }

    public decimal PrecioCompra { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public DateTime FechaIngreso { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;

    public virtual TbInvMovimiento IdMovimientoNavigation { get; set; } = null!;
}
