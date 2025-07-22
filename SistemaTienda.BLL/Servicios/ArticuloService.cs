using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios
{
    public class ArticuloService : IArticuloService
    {
        public Task<int> CrearArticulo(ArticuloCreacionDTO articuloCreacionDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DesactivarArticulo(int idArticulo)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditarArticulo(ArticuloDTO articuloEditarDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArticuloDTO>> ListarUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}
