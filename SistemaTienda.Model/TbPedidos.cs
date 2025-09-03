using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbPedidos
{
    public int Id { get; set; }

    public int IdProveedor { get; set; }

    public int IdEstadoPedido { get; set; }

    public int IdUsuarioCreador { get; set; }

    public string? Descripcion { get; set; }

    public DateTime FechaEntrega { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public bool Estado { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual TbPedEstadosPedido IdEstadoPedidoNavigation { get; set; } = null!;

    public virtual TbComProveedores IdProveedorNavigation { get; set; } = null!;

    public virtual TbSisUsuario IdUsuarioCreadorNavigation { get; set; } = null!;

    public virtual ICollection<TbPedDetallesPedido> TbPedDetallesPedidos { get; set; } = new List<TbPedDetallesPedido>();
}
