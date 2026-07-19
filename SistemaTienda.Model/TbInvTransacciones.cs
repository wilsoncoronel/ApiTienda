using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvTransacciones
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Signo { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<TbInvMovimiento> TbInvMovimientos { get; set; } = new List<TbInvMovimiento>();
}
