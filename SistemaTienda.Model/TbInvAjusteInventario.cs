using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvAjusteInventario
{
    public int Id { get; set; }

    public int IdTransaccion { get; set; }

    public int IdUsuarioCreador { get; set; }

    public string Motivo { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public int Estado { get; set; }

    public DateTime? FechaReversion { get; set; }

    public virtual TbInvTransacciones IdTransaccionNavigation { get; set; } = null!;

    public virtual ICollection<TbInvDetalleAjusteInventario> TbInvDetalleAjusteInventarios { get; set; } = new List<TbInvDetalleAjusteInventario>();
}
