using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbContCuentaContable
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public virtual ICollection<TbContPartidaContable> TbContPartidaContables { get; set; } = new List<TbContPartidaContable>();
}
