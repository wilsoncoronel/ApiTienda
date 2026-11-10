using System;
using System.Collections.Generic;

namespace SistemaTienda.DTO
{
    public class AjusteInventarioCrearDTO
    {
        public int IdTransaccion { get; set; }

        public int IdUsuarioCreador { get; set; }

        public string Motivo { get; set; } = null!;

        public int IdEstado { get; set; }
        public TransaccionInventarioDTO TransaccionInventarioDTO { get; set; } = null!;
        public ICollection<DetalleAjusteInventarioCrearDTO> Detalles { get; set; } = new List<DetalleAjusteInventarioCrearDTO>();
    }
}
