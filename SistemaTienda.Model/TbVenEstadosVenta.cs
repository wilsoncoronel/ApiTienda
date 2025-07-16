using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenEstadosVenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<TbVenta> TbVenta { get; set; } = new List<TbVenta>();
}
