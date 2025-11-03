using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
    public class ExistenciaDTO
    {
        public int IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public decimal TotalCantidad { get; set; }
    }
}