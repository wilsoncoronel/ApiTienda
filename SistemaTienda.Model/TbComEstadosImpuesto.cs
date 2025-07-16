using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComEstadosImpuesto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbComImpuestosArticulo> TbComImpuestosArticulos { get; set; } = new List<TbComImpuestosArticulo>();
}
