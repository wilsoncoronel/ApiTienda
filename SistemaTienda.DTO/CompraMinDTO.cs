using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class CompraMinDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public string Documento { get; set; }
        public DateTime FechaCompra { get; set; }
        public int IdEstado { get; set; }
        public EstadoCompraDTO EstadoCompra { get; set; }
        public decimal Total { get; set; }
        public int IdUsuarioCreador { get; set; }
        public UsuarioMinDTO UsuarioCreadorMinDTO { get; set; }

        public ProveedorMinDTO ProveedorMinDto { get; set; }
    }
}
