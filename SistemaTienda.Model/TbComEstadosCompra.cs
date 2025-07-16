using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComEstadosCompra
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<TbCompra> TbCompras { get; set; } = new List<TbCompra>();
}
