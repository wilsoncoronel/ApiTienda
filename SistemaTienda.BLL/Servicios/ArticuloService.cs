using Microsoft.EntityFrameworkCore;
using SistemaTienda.API.Exceptions;
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
            var articuloTb = this._mapper.MapeoArticuloCreacionDtoAArticuloTb(articuloCreacionDto);
            articuloTb.FechaCreacion = DateTime.Now;
            articuloTb.Estado = true;
            articuloTb.EstadoVisual = true;
                var articuloCreado =  await this._articuloRepository.Crear(articuloTb);
            if (articuloCreado.Id == null)
                throw new DbUpdateException("No se pudo crear el artículo!!!");
            return articuloCreado.Id;
        }
        
        public async Task<bool> DesactivarArticulo(int idArticulo)
        {   
            var articuloTb = await this._articuloRepository.ListarId(a => a.Id == idArticulo);
            articuloTb.Estado = false;
            articuloTb.FechaActualizacion = DateTime.Now;
            var resp = await this._articuloRepository.Editar(articuloTb);
            if (resp == false)
                throw new DbUpdateException("No se pudo desactivar el artículo!!");
            return resp;
        }

        public async Task<bool> EditarArticulo(ArticuloEdicionDTO articuloEditarDto)
        {
            var articuloTb = await this._articuloRepository.ListarId(a => a.Id == articuloEditarDto.Id);
            if (articuloTb == null)
                throw new NotFoundException("No se pudo editar el articulo , no existen en la bd!!");
            articuloTb.IdImpuesto = articuloEditarDto.IdImpuesto;
            articuloTb.IdMarca = articuloEditarDto.IdMarca;
            articuloTb.Nombre = articuloEditarDto.Nombre;
            articuloTb.IdImpuesto = articuloEditarDto.IdImpuesto;
            articuloTb.IdTipoArticulo = articuloEditarDto.IdTipoArticulo;
            articuloTb.FechaActualizacion = DateTime.Now;
            articuloTb.IdUnidad = articuloEditarDto.IdUnidad;
            articuloTb.UnidadValor = articuloEditarDto.UnidadValor;
            articuloTb.ValorCompra = articuloEditarDto.ValorCompra;
            articuloTb.ValorVenta = articuloEditarDto.ValorVenta;
            articuloTb.Descripcion = articuloEditarDto.Descripcion;
            articuloTb.Estado = articuloEditarDto.Estado;
            articuloTb.FechaCaducidad = articuloEditarDto.FechaCaducidad;
            articuloTb.Papeleria = articuloEditarDto.Papeleria;
            articuloTb.IdPorcentajeGanancia = articuloEditarDto.IdPorcentajeGanancia;
            var resp = await this._articuloRepository.Editar(articuloTb);
            if (resp == false)
                throw new DbUpdateException("No se pudo editar el articulo , no existen en la bd!!");
            return resp;
        }

        public async Task<List<ArticuloInventarioDTO>> ListarTodosArticulos(bool esVenta)
        {
            // Identificar transacciones que representan reversiones (por nombre)
            var idsTransReversion = await this.tiendaDbContext.TbInvTransacciones
                .Where(t => t.Nombre.ToLower().Contains("reversion compra"))
                .Select(t => t.Id)
                .ToListAsync();

            // Obtener lotes existentes y traer la entidad Articulo relacionada,
            // excluyendo lotes con Estado = false o que pertenezcan a movimientos de reversión
            var lotes = await this.tiendaDbContext.TbInvLotes
                .Where(l => l.Estado == true && l.StockDisponible > 0 && !idsTransReversion.Contains(l.IdMovimientoNavigation.IdTransaccionInventario))
                .Include(l => l.IdMovimientoNavigation)
                .Include(l => l.IdArticuloNavigation)
                    .ThenInclude(a => a.IdMarcaNavigation)
                .Include(l => l.IdArticuloNavigation)
                    .ThenInclude(a => a.IdTipoArticuloNavigation)
                .Include(l => l.IdArticuloNavigation)
                    .ThenInclude(a => a.IdImpuestoNavigation)
                .ToListAsync();

            var resultado = new List<ArticuloInventarioDTO>();

            // Mapear cada lote a ArticuloInventarioDTO (una entrada por lote)
            resultado.AddRange(lotes.Select(l => new ArticuloInventarioDTO
            {
                Articulo = this._mapper.MapeoArticuloTbAArticuloDto(l.IdArticuloNavigation, esVenta),
                NumeroLote = l.NumeroLote ?? string.Empty,
                Codigo = l.Codigo ?? string.Empty,
                FechaIngreso = l.FechaIngreso,
                FechaExpiracion = l.FechaExpiracion,
                StockDisponible = l.StockDisponible,
                StockMinimo = l.StockMinimo,
                CostoUnitario = l.CostoUnitario
            }));

            // Agregar artículos activos que no tienen lotes con stock
            var idsConLote = lotes.Select(l => l.IdArticulo).Distinct().ToList();
            var articulosSinLote = await this.tiendaDbContext.TbComArticulos
                .Where(a => a.Estado == true && a.EstadoVisual == true && !idsConLote.Contains(a.Id))
                .Include(a => a.IdImpuestoNavigation)
                .Include(a => a.IdMarcaNavigation)
                .Include(a => a.IdTipoArticuloNavigation)
                .ToListAsync();

            resultado.AddRange(articulosSinLote.Select(a => new ArticuloInventarioDTO
            {
                Articulo = this._mapper.MapeoArticuloTbAArticuloDto(a, esVenta),
                NumeroLote = string.Empty,
                Codigo = string.Empty,
                FechaIngreso = null,
                FechaExpiracion = DateOnly.FromDateTime(DateTime.Now),
                StockDisponible = 0m,
                StockMinimo = 0m,
                CostoUnitario = 0m
            }));

            return resultado;
        }

        public async Task<List<ArticuloDTO>> ListarArticulos(DateTime fechaInicial, DateTime fechaFinal)
        {
            var listaArticulos = await this.tiendaDbContext.TbComArticulos.Where(art => art.Estado == true && art.EstadoVisual == true && art.FechaCreacion >= fechaInicial && art.FechaCreacion <= fechaFinal )
                .Include(a => a.IdMarcaNavigation)
                .Include(a => a.IdTipoArticuloNavigation)
                .Include(a => a.IdImpuestoNavigation)
                .Include(p => p.IdPorcentajeGananciaNavigation).ToListAsync();
            return this._mapper.MapeoListaArticulosDtoPrincipal(listaArticulos);
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
                    Nombre = t.Nombre,
                    ValorImpuesto = t.ValorImpuesto
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
            using var transaction = await tiendaDbContext.Database.BeginTransactionAsync();
            try {
                var articulosTb = this._mapper.MapeoListaArticulosCreacionAListaArticulosTb(articulosCreacionDto);
                this.tiendaDbContext.TbComArticulos.AddRange(articulosTb);
                await this.tiendaDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch {
                await transaction.RollbackAsync();
                throw;
            }
            
        }
    }
}
