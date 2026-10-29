using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvEstadosAjuste
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<TbInvAjusteInventario> TbInvAjusteInventarios { get; set; } = new List<TbInvAjusteInventario>();
}
