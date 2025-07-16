using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbPedDetallesPedido
{
    public int Id { get; set; }

    public int IdPedido { get; set; }

    public int IdArticulo { get; set; }

    public string? Descripcion { get; set; }

    public decimal ValorCompra { get; set; }

    public int Cantidad { get; set; }

    public decimal ValorTotal { get; set; }

    public decimal ImpuestoValor { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;

    public virtual TbPedidos IdPedidoNavigation { get; set; } = null!;
}
