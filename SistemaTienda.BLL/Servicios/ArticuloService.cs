using Microsoft.EntityFrameworkCore;
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
                DateTime fechaCreacion = DateTime.Now;
                articuloTb.FechaCreacion = fechaCreacion;
                   var articuloCreado =  await this._articuloRepository.Crear(articuloTb);
                if (articuloCreado.Id == null)
                    throw new Exception("No se pudo crear el artículo!!!");
                return articuloCreado.Id;
            } catch {
                throw;
            }
        }

        public async Task<bool> DesactivarArticulo(int idArticulo)
        {
            try
            {
                DateTime fechaActualizacion = DateTime.Now;
                var articuloTb = await this._articuloRepository.ListarId(a => a.Id == idArticulo);
                articuloTb.Estado = false;
                articuloTb.FechaActualizacion = fechaActualizacion;
                var resp = await this._articuloRepository.Editar(articuloTb);
                if (resp == false)
                    throw new Exception("No se pudo desactivar el artículo!!");
                return resp;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EditarArticulo(ArticuloEdicionDTO articuloEditarDto)
        {
            try
            {
                DateTime fechaActualizacion = DateTime.Now;
                var articuloTb = await this._articuloRepository.ListarId(a => a.Id == articuloEditarDto.Id);

                if (articuloTb.Id == null)
                    throw new Exception("No se pudo editar el articulo , no existen en la bd!!");
                articuloTb.IdImpuesto = articuloEditarDto.IdImpuesto;
                articuloTb.IdMarca = articuloEditarDto.IdMarca;
                articuloTb.Nombre = articuloEditarDto.Nombre;
                articuloTb.IdImpuesto = articuloEditarDto.IdImpuesto;
                articuloTb.Codigo = articuloEditarDto.Codigo;
                articuloTb.FechaActualizacion = fechaActualizacion;
                articuloTb.Unidad = articuloEditarDto.Unidad;
                articuloTb.UnidadValor = articuloEditarDto.UnidadValor;
                articuloTb.ValorCompra = articuloEditarDto.ValorCompra;
                articuloTb.ValorVenta = articuloEditarDto.ValorVenta;
                articuloTb.Descripcion = articuloEditarDto.Descripcion;
                articuloTb.Estado = articuloEditarDto.Estado;
                articuloTb.FechaCaducidad = articuloEditarDto.FechaCaducidad;

                var resp = await this._articuloRepository.Editar(articuloTb);
                if (resp == false)
                    throw new Exception("No se pudo editar el artículo");
                return resp;
            }
            catch {
                throw;
            }
        }

        public Task<List<ArticuloDTO>> ListarUsuarios()
        {
            throw new NotImplementedException();
        }
    }
}
