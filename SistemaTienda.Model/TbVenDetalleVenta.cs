using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenDetalleVenta
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public int IdArticulo { get; set; }

    public string? Descripcion { get; set; }

    public int Cantidad { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public decimal ValorCompra { get; set; }

    public decimal ValorVenta { get; set; }

    public decimal ImpuestoValor { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;

    public virtual TbVenta IdVentaNavigation { get; set; } = null!;

    public virtual ICollection<TbInvConsumoLote> TbInvConsumoLotes { get; set; } = new List<TbInvConsumoLote>();
}
