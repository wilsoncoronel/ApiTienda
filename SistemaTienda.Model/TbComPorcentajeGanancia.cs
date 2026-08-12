using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComPorcentajeGanancia
{
    public int Id { get; set; }

    public string PorcentajeGanancia { get; set; } = null!;

    public decimal Valor { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbComArticulo> TbComArticulos { get; set; } = new List<TbComArticulo>();
}
