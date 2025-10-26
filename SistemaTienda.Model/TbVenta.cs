using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenta
{
    public int Id { get; set; }

    public int IdCliente { get; set; }

    public int IdEstadoVenta { get; set; }

    public int IdUsuarioCreador { get; set; }

    public string? Documento { get; set; }

    public DateTime FechaVenta { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual TbComCliente IdClienteNavigation { get; set; } = null!;

    public virtual TbVenEstadosVenta IdEstadoVentaNavigation { get; set; } = null!;

    public virtual TbSisUsuario IdUsuarioCreadorNavigation { get; set; } = null!;

    public virtual TbContAsientoContable? TbContAsientoContable { get; set; }

    public virtual TbInvInventario? TbInvInventario { get; set; }

    public virtual ICollection<TbVenDetalleVenta> TbVenDetalleVenta { get; set; } = new List<TbVenDetalleVenta>();
}
