using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvLote
{
    public int Id { get; set; }

    public int IdMovimiento { get; set; }

    public int IdArticulo { get; set; }

    public string NumeroLote { get; set; } = null!;

    public string? Codigo { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public decimal StockDisponible { get; set; }

    public decimal StockMinimo { get; set; }

    public decimal CostoUnitario { get; set; }

    public DateOnly? FechaExpiracion { get; set; }

    public bool Estado { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;

    public virtual TbInvMovimiento IdMovimientoNavigation { get; set; } = null!;

    public virtual ICollection<TbInvConsumoLote> TbInvConsumoLotes { get; set; } = new List<TbInvConsumoLote>();
}
