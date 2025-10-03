using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComDetallesCompra
{
    public int Id { get; set; }

    public int IdCompra { get; set; }

    public int IdArticulo { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal ValorCompra { get; set; }

    public decimal ValorVenta { get; set; }

    public decimal ValorTotal { get; set; }

    public decimal ImpuestoValor { get; set; }

    public virtual TbComArticulo IdArticuloNavigation { get; set; } = null!;

    public virtual TbCompra IdCompraNavigation { get; set; } = null!;
}
