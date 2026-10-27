using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComImpuesto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdTipoCalculo { get; set; }

    public decimal Valor { get; set; }

    public bool Estado { get; set; }

    public virtual TbComTipoCalculoImpuesto IdTipoCalculoNavigation { get; set; } = null!;

    public virtual ICollection<TbComArticulosImpuesto> TbComArticulosImpuestos { get; set; } = new List<TbComArticulosImpuesto>();
}
