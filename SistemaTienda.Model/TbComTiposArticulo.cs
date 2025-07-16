using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComTiposArticulo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbComArticulo> TbComArticulos { get; set; } = new List<TbComArticulo>();
}
