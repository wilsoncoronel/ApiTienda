using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComArticulo
{
    public int Id { get; set; }

    public int IdMarca { get; set; }

    public int IdTipoArticulo { get; set; }

    public int IdUsuarioCreador { get; set; }

    public int IdImpuesto { get; set; }

    public int? IdPorcentajeGanancia { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaCaducidad { get; set; }

    public bool EstadoVisual { get; set; }

    public bool Estado { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public string? Unidad { get; set; }

    public decimal? UnidadValor { get; set; }

    public decimal ValorCompra { get; set; }

    public decimal ValorVenta { get; set; }

    public bool? Papeleria { get; set; }

    public virtual TbComImpuestosArticulo IdImpuestoNavigation { get; set; } = null!;

    public virtual TbComMarca IdMarcaNavigation { get; set; } = null!;

    public virtual TbComPorcentajeGanancia? IdPorcentajeGananciaNavigation { get; set; }

    public virtual TbComTiposArticulo IdTipoArticuloNavigation { get; set; } = null!;

    public virtual TbSisUsuario IdUsuarioCreadorNavigation { get; set; } = null!;

    public virtual ICollection<TbComDetallesCompra> TbComDetallesCompras { get; set; } = new List<TbComDetallesCompra>();

    public virtual ICollection<TbInvLote> TbInvLotes { get; set; } = new List<TbInvLote>();

    public virtual ICollection<TbPedDetallesPedido> TbPedDetallesPedidos { get; set; } = new List<TbPedDetallesPedido>();

    public virtual ICollection<TbVenDetalleVenta> TbVenDetalleVenta { get; set; } = new List<TbVenDetalleVenta>();
}
