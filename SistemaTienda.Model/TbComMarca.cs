using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComMarca
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbComArticulo> TbComArticulos { get; set; } = new List<TbComArticulo>();

    public virtual ICollection<TbInvInventario> TbInvInventarios { get; set; } = new List<TbInvInventario>();
}
