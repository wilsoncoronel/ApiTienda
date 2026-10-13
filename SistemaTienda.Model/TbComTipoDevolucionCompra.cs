using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComTipoDevolucionCompra
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

    public bool EstadoVisual { get; set; }

    public virtual ICollection<TbComDevolucionCompra> TbComDevolucionCompras { get; set; } = new List<TbComDevolucionCompra>();
}
