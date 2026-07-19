using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvLote
{
    public int Id { get; set; }

    public int IdMovimiento { get; set; }

    public string? NumeroLote { get; set; }

    public DateTime FechaIngreso { get; set; }

    public decimal StockDisponible { get; set; }

    public decimal StockMinimo { get; set; }

    public decimal CostoUnitario { get; set; }

    public DateOnly? FechaExpiracion { get; set; }

    public virtual TbInvMovimiento IdMovimientoNavigation { get; set; } = null!;

    public virtual ICollection<TbInvDetalleLote> TbInvDetalleLotes { get; set; } = new List<TbInvDetalleLote>();
}
