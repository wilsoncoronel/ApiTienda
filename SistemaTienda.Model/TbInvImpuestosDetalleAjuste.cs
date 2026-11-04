using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbInvImpuestosDetalleAjuste
{
    public int Id { get; set; }

    public int IdDetalleAjuste { get; set; }

    public int IdTipoCalculo { get; set; }

    public int IdImpuesto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Valor { get; set; }

    public bool Estado { get; set; }

    public virtual TbInvDetalleAjusteInventario IdDetalleAjusteNavigation { get; set; } = null!;

    public virtual TbComTipoCalculoImpuesto IdTipoCalculoNavigation { get; set; } = null!;
}
