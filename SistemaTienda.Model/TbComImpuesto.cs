using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComImpuesto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoCalculo { get; set; } = null!;

    public decimal Valor { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<TbComArticulosImpuesto> TbComArticulosImpuestos { get; set; } = new List<TbComArticulosImpuesto>();
}
