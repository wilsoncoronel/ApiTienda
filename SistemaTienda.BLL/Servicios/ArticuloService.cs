using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios
{
    public class ArticuloService : IArticuloService
    {
        private readonly TiendaDbContext tiendaDbContext;
        public readonly IGenericRepository<TbComArticulo> _articuloRepository;
        public readonly IMapeos _mapper;
        public ArticuloService(TiendaDbContext tiendaDbContext, IGenericRepository<TbComArticulo> articuloRepository, IMapeos mapper)
        {
            this.tiendaDbContext = tiendaDbContext;
            this._articuloRepository = articuloRepository;
            this._mapper = mapper;
        }

    

        public async Task<int> CrearArticulo(ArticuloCreacionDTO articuloCreacionDto)
        {
            try {
                var articuloTb = this._mapper.MapeoArticuloCreacionDtoAArticuloTb(articuloCreacionDto);
                   var articuloCreado =  await this._articuloRepository.Crear(articuloTb);
                if (articuloCreado.Id == null)
                    throw new Exception("No se pudo crear el artículo!!!");
                return articuloCreado.Id;
            } catch {
                throw;
            }
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
