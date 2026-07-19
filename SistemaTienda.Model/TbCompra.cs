using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbCompra
{
    public int Id { get; set; }

    public int IdProveedor { get; set; }

    public int IdEstadoCompra { get; set; }

    public int IdUsuarioCreador { get; set; }

    public string Documento { get; set; } = null!;

    public DateTime FechaCompra { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual TbComEstadosCompra IdEstadoCompraNavigation { get; set; } = null!;

    public virtual TbComProveedores IdProveedorNavigation { get; set; } = null!;

    public virtual TbSisUsuario IdUsuarioCreadorNavigation { get; set; } = null!;

    public virtual ICollection<TbComDetallesCompra> TbComDetallesCompras { get; set; } = new List<TbComDetallesCompra>();

    public virtual TbContAsientoContable? TbContAsientoContable { get; set; }

    public virtual ICollection<TbInvMovimiento> TbInvMovimientos { get; set; } = new List<TbInvMovimiento>();
}
