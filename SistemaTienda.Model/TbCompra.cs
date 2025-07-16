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

    public DateOnly FechaCompra { get; set; }

    public DateOnly FechaCreacion { get; set; }

    public bool EstadoVisual { get; set; }

    public decimal ValorIva { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Total { get; set; }

    public virtual TbComEstadosCompra IdEstadoCompraNavigation { get; set; } = null!;

    public virtual TbSisUsuario IdUsuarioCreadorNavigation { get; set; } = null!;

    public virtual ICollection<TbComDetallesCompra> TbComDetallesCompras { get; set; } = new List<TbComDetallesCompra>();
}
