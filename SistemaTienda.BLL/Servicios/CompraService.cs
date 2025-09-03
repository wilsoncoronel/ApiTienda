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
    public class CompraService : ICompraServicio
    {
        private readonly TiendaDbContext _tiendaDbContext;
        public readonly IGenericRepository<TbCompra> _compraRepository;
        public readonly IGenericRepository<TbComDetallesCompra> _detalleRepository;
        private readonly IMapeos _mapper;
        public CompraService(TiendaDbContext tiendaDbContext, IGenericRepository<TbCompra> compraRepository, IGenericRepository<TbComDetallesCompra> detalleRepository, IMapeos mapper)
        {
            _tiendaDbContext = tiendaDbContext;
            _compraRepository = compraRepository;
            _detalleRepository = detalleRepository;
            _mapper = mapper;
        }

        public Task<bool> EditarCompra(CompraEditarDTO compraDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<CompraDTO>> ListarCompras()
        {
            throw new NotImplementedException();
        }

        public Task<CompraDTO> ObtenerCompra(int idCompra)
        {
            throw new NotImplementedException();
        }

        public async Task<int> RegistrarCompra(CompraCreacionDTO compraDto)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var tbCompra = this._mapper.MapeoCompraCreacionDtoACompraTb(compraDto);
                    await this._compraRepository.Crear(tbCompra);
                    if (tbCompra.Id == 0)
                        throw new Exception("No se pudo registrar la compra");
                    transaction.Commit();
                    return tbCompra.Id;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Task<bool> ReversarCompra(int id)
        {
            throw new NotImplementedException();
        }
    }
}
