using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComUnidadesMedida
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbComArticulo> TbComArticulos { get; set; } = new List<TbComArticulo>();
}
