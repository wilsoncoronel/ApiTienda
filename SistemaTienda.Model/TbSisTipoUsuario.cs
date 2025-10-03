using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbSisTipoUsuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstadoVisual { get; set; }

    public bool Estado { get; set; }


    public virtual ICollection<TbSisUsuario> TbSisUsuarios { get; set; } = new List<TbSisUsuario>();
}
