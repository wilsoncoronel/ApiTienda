using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbComImpuestosArticulo
{
    public int Id { get; set; }

    public int IdEstadoImpuesto { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal ValorImpuesto { get; set; }

    public string? Descripcion { get; set; }

    public virtual TbComEstadosImpuesto IdEstadoImpuestoNavigation { get; set; } = null!;

    public virtual ICollection<TbComArticulo> TbComArticulos { get; set; } = new List<TbComArticulo>();
}
