using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenDevolucionVenta
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public DateTime Fecha { get; set; }

    public string Motivo { get; set; } = null!;

    public int Estado { get; set; }
}
