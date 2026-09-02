using System;
using System.Collections.Generic;

namespace SistemaTienda.DTO
{
    public class DevolucionCompraCreacionDTO
    {
        public int Id { get; set; }

        public int IdCompra { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaReversion { get; set; }

        public int Estado { get; set; }

        public string Motivo { get; set; } = string.Empty;

        // Nota: Se omite la propiedad de navegación a TbCompra para evitar dependencia directa al proyecto de modelos
        public ICollection<DetalleDevolucionCompraCreacionDTO> DetalleDevolucionCompraDto { get; set; } = new List<DetalleDevolucionCompraCreacionDTO>();
    }
}
