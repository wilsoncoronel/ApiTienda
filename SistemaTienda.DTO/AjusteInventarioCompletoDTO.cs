using System;
using System.Collections.Generic;

namespace SistemaTienda.DTO
{
    public class AjusteInventarioCompletoDTO
    {
        public int Id { get; set; }

        public int IdTransaccion { get; set; }

        public int IdUsuarioCreador { get; set; }

        public string NombresUsuarioCreador { get; set; } = null!;

        public string ApellidosUsuarioCreador { get; set; } = null!;

        public string Motivo { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public int IdEstado { get; set; }

        public string NombreEstado { get; set; } = null!;
        public DateTime? FechaReversion { get; set; }

        public TransaccionInventarioDTO TransaccionInventarioDTO { get; set; } = null!;

        public List<DetalleAjusteInventarioCompletoDTO> Detalles { get; set; } = new List<DetalleAjusteInventarioCompletoDTO>();
    }
}
