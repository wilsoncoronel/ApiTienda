using System;
using System.Collections.Generic;

namespace SistemaTienda.DTO
{
    public class DetalleDevolucionCompraCreacionDTO
    {
        public int IdDetalleCompra { get; set; }

        public int Cantidad { get; set; }

        public bool Estado { get; set; }
    }
}
