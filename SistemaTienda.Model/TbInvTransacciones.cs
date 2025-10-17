using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvTransaccione
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Signo { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<TbDetallesInventario> TbDetallesInventarios { get; set; } = new List<TbDetallesInventario>();
}
