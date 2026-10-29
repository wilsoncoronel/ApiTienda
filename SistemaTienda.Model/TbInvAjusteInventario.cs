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

    public int IdEstado { get; set; }

    public DateTime? FechaReversion { get; set; }

    public virtual TbInvEstadosAjuste IdEstadoNavigation { get; set; } = null!;

    public virtual TbInvTransacciones IdTransaccionNavigation { get; set; } = null!;

    public virtual ICollection<TbInvDetalleAjusteInventario> TbInvDetalleAjusteInventarios { get; set; } = new List<TbInvDetalleAjusteInventario>();
}
