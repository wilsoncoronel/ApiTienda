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
using System.Linq.Expressions;
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
                if (articuloCreacionDto.ListaCodigosArticulosDTO.Count == 0)
                    throw new Exception("No se puede crear el artículo sin códigos de artículos!!");
                var articuloTb = this._mapper.MapeoArticuloCreacionDtoAArticuloTb(articuloCreacionDto);
                articuloTb.FechaCreacion = DateTime.Now;
                articuloTb.Estado = true;
                articuloTb.EstadoVisual = true;
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
                var articuloTb = await this._articuloRepository.ListarId(a => a.Id == idArticulo);
                articuloTb.Estado = false;
                articuloTb.FechaActualizacion = DateTime.Now;
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
                var articuloTb = await this._articuloRepository.ListarId(a => a.Id == articuloEditarDto.Id);

                if (articuloTb.Id == null)
                    throw new Exception("No se pudo editar el articulo , no existen en la bd!!");
                articuloTb.IdImpuesto = articuloEditarDto.IdImpuesto;
                articuloTb.IdMarca = articuloEditarDto.IdMarca;
                articuloTb.Nombre = articuloEditarDto.Nombre;
                articuloTb.IdImpuesto = articuloEditarDto.IdImpuesto;
                articuloTb.IdTipoArticulo = articuloEditarDto.IdTipoArticulo;
                articuloTb.Codigo = articuloEditarDto.Codigo;
                articuloTb.FechaActualizacion = DateTime.Now;
                articuloTb.Unidad = articuloEditarDto.Unidad;
                articuloTb.UnidadValor = articuloEditarDto.UnidadValor;
                articuloTb.ValorCompra = articuloEditarDto.ValorCompra;
                articuloTb.ValorVenta = articuloEditarDto.ValorVenta;
                articuloTb.Descripcion = articuloEditarDto.Descripcion;
                articuloTb.Estado = articuloEditarDto.Estado;
                articuloTb.FechaCaducidad = articuloEditarDto.FechaCaducidad;
                articuloTb.Papeleria = articuloEditarDto.Papeleria;
                var resp = await this._articuloRepository.Editar(articuloTb);
                if (resp == false)
                    throw new Exception("No se pudo editar el artículo");
                return resp;
            }
            catch {
                throw;
            }
        }

        public async Task<List<ArticuloDTO>> ListarTodosArticulos()
        {
            var listaArticulos = await this.tiendaDbContext.TbComArticulos.Where(art => art.Estado == true && art.EstadoVisual == true && art.Estado == true && art.EstadoVisual == true)
                .Include(a => a.IdMarcaNavigation)
                .Include(a => a.IdTipoArticuloNavigation)
                .Include(a => a.IdImpuestoNavigation).ToListAsync();
            return this._mapper.MapeoListaArticulosDtoVentas(listaArticulos);
        }

        public async Task<List<ArticuloDTO>> ListarArticulos(DateTime fechaInicial, DateTime fechaFinal)
        {
            var listaArticulos = await this.tiendaDbContext.TbComArticulos.Where(art => art.Estado == true && art.EstadoVisual == true && art.FechaCreacion >= fechaInicial && art.FechaCreacion <= fechaFinal )
                .Include(a => a.IdMarcaNavigation)
                .Include(a => a.IdTipoArticuloNavigation)
                .Include(a => a.IdImpuestoNavigation).ToListAsync();
            return this._mapper.MapeoListaArticulosDto(listaArticulos);
        }

        public async Task<List<CodigoArticuloDTO>> ListarCodigosArticulos(int idArticulo)
        {
            var listaCodigos = await this.tiendaDbContext.TbComCodigosArticulos.Where(cod => cod.IdArticulo == idArticulo).ToListAsync();
            return this._mapper.MapeoListaArticulosDto(listaCodigos);
        }

        public async Task<List<TipoArticuloDTO>> CargarListaTiposArticulos()
        {
            var listaTiposArticulos = await this.tiendaDbContext.TbComTiposArticulos
                .Where(t => t.EstadoVisual == true)
                .Select(t => new TipoArticuloDTO
                {
                    Id = t.Id,
                    Nombre = t.Nombre
                }).ToListAsync();
            return listaTiposArticulos;
        }

        public async Task<List<ImpuestoArticuloDTO>> CargarListaImpuestos()
        {
            var listaImpuestosArticulos = await this.tiendaDbContext.TbComImpuestosArticulos
                .Where(t => t.IdEstadoImpuestoNavigation.EstadoVisual == true)
                .Select(t => new ImpuestoArticuloDTO
                {
                    Id = t.Id,
                    Nombre = t.Nombre
                }).ToListAsync();
            return listaImpuestosArticulos;
        }

        public async Task<List<MarcaDTO>> CargarListaMarca()
        {
            var listaMarcasArticulos = await this.tiendaDbContext.TbComMarcas
            .Where(t => t.EstadoVisual == true)
            .Select(t => new MarcaDTO
            {
                Id = t.Id,
                Nombre = t.Nombre
            }).ToListAsync();
            return listaMarcasArticulos;

        }

        public async Task<bool> CrearArticulosLista(List<ArticuloCreacionDTO> articulosCreacionDto)
        {
            using (var transaction = tiendaDbContext.Database.BeginTransaction())
            {
                try {
                    var articulosTb = this._mapper.MapeoListaArticulosCreacionAListaArticulosTb(articulosCreacionDto);
                    this.tiendaDbContext.TbComArticulos.AddRange(articulosTb);
                    await this.tiendaDbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return true;
        }
    }
}
