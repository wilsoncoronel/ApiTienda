using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvMovimiento
{
    public int Id { get; set; }

    public int? IdMovimientoOrigen { get; set; }

    public int IdTransaccionInventario { get; set; }

    public DateTime Fecha { get; set; }

    public string? Referencia { get; set; }

    public virtual TbInvTransacciones IdTransaccionInventarioNavigation { get; set; } = null!;

    public virtual ICollection<TbInvConsumoLote> TbInvConsumoLotes { get; set; } = new List<TbInvConsumoLote>();

    public virtual ICollection<TbInvDevolucionLote> TbInvDevolucionLotes { get; set; } = new List<TbInvDevolucionLote>();

    public virtual ICollection<TbInvLote> TbInvLotes { get; set; } = new List<TbInvLote>();
}
