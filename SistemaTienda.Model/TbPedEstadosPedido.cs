using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbPedEstadosPedido
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<TbPedidos> TbPedidos { get; set; } = new List<TbPedidos>();
}
