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
    public class ProveedorService : IProveedorService
    {
        private readonly TiendaDbContext _tiendaDbContext;
        private readonly IGenericRepository<TbComProveedores> _proveedorRepository;
        private readonly IMapeos mapper;

        public ProveedorService(TiendaDbContext tiendaDbContext, IMapeos mapper, IGenericRepository<TbComProveedores> proveedorRepository)
        {
            _tiendaDbContext = tiendaDbContext;
            this.mapper = mapper;
            this._proveedorRepository = proveedorRepository;
        }

        public async Task<int> CrearProveedor(ProveedorCreacionDTO proveedorCreacionDto)
        {
            using (var transaccion = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var proveedor = this.mapper.MapeoProveedorDtoAProveedorTb(proveedorCreacionDto);
                    await this._proveedorRepository.Crear(proveedor);
                    if (proveedor.Id == 0)
                        throw new Exception("No se pudo crear el proveedor");
                    transaccion.Commit();
                    return proveedor.Id;
                }
                catch
                {
                    transaccion.Rollback();
                    throw new Exception("Error ha ocurrido un error creando el proveedor, comuniquese con el administrador del sistema!!!");
                }
            }
        }
    }
}