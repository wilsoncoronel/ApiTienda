using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComDevolucionCompra
{
    public int Id { get; set; }

    public int IdTipoDevolucion { get; set; }

    public int IdProveedor { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaReversion { get; set; }

    public int Estado { get; set; }

    public string Motivo { get; set; } = null!;

    public virtual TbComProveedores IdProveedorNavigation { get; set; } = null!;

    public virtual TbComTipoDevolucionCompra IdTipoDevolucionNavigation { get; set; } = null!;

    public virtual TbComCreditoProveedor? TbComCreditoProveedor { get; set; }

    public virtual ICollection<TbComDetalleDevolucionCompra> TbComDetalleDevolucionCompras { get; set; } = new List<TbComDetalleDevolucionCompra>();
}
