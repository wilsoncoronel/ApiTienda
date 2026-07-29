using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbVenDetalleDevolucionVentum
{
    public int Id { get; set; }

    public int IdDevolucionVenta { get; set; }

    public int IdDetalleVenta { get; set; }

    public int Cantidad { get; set; }
}
