using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbContPartidaContable
{
    public int Id { get; set; }

    public int IdAsientoContable { get; set; }

    public int IdCuentaContable { get; set; }

    public decimal Debe { get; set; }

    public decimal Haber { get; set; }

    public virtual TbContAsientoContable IdAsientoContableNavigation { get; set; } = null!;

    public virtual TbContCuentaContable IdCuentaContableNavigation { get; set; } = null!;
}
