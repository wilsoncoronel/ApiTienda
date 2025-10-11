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
    public class CompraService : ICompraServicio
    {
        private readonly TiendaDbContext _tiendaDbContext;
        public readonly IGenericRepository<TbCompra> _compraRepository;
        public readonly IGenericRepository<TbInvInventario> _inventarioRepository;
        public readonly IGenericRepository<TbComDetallesCompra> _detalleRepository;
        private readonly IMapeos _mapper;
        public CompraService(TiendaDbContext tiendaDbContext, IGenericRepository<TbCompra> compraRepository, IGenericRepository<TbComDetallesCompra> detalleRepository, IGenericRepository<TbInvInventario> inventarioRepository, IMapeos mapper)
        {
            _tiendaDbContext = tiendaDbContext;
            _compraRepository = compraRepository;
            _detalleRepository = detalleRepository;
            _inventarioRepository = inventarioRepository;
            _mapper = mapper;
        }

        public async Task<bool> EditarCompra(CompraEditarDTO compraDto)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var tbCompra = await this._tiendaDbContext.TbCompras.Include(c => c.IdEstadoCompraNavigation)
                        .Include(d => d.TbComDetallesCompras)
                        .FirstOrDefaultAsync(c => c.Id == compraDto.Id);
                    var idsDto = compraDto.DetalleComprasEditarDto.Select(x => x.Id).ToList();
                    var eliminados = tbCompra.TbComDetallesCompras
                        .Where(x => !idsDto.Contains(x.Id)).ToList();

                    foreach(var e in eliminados)
                    {
                        _tiendaDbContext.TbComDetallesCompras.Remove(e);
                    }

                    foreach (var detDto in compraDto.DetalleComprasEditarDto)
                    {
                        var existente = tbCompra.TbComDetallesCompras.FirstOrDefault(x => x.Id == detDto.Id);
                        if (existente != null)
                        {
                            // actualizar
                            existente.IdArticulo = detDto.ArticuloId;
                            existente.Cantidad = detDto.Cantidad;
                            existente.Descripcion = detDto.Descripcion;
                            existente.ImpuestoValor = detDto.ImpuestoValor;
                            existente.ValorCompra = detDto.ValorCompra;
                            existente.ValorVenta = detDto.ValorVenta;
                            existente.ValorTotal = detDto.ValorTotal;
                        }
                        else
                        {
                            // nuevo
                            tbCompra.TbComDetallesCompras.Add(new TbComDetallesCompra
                            {
                                IdArticulo = detDto.ArticuloId,
                                Cantidad = detDto.Cantidad,
                                Descripcion = detDto.Descripcion,
                                ImpuestoValor = detDto.ImpuestoValor,
                                ValorCompra = detDto.ValorCompra,
                                ValorVenta = detDto.ValorVenta,
                                ValorTotal = detDto.ValorTotal
                            });
                        }
                    }
                    this._mapper.MapeoCompraEdicionDtoACompraTb(compraDto, tbCompra);
                    var resp = await this._compraRepository.Editar(tbCompra);
                    if (resp == false)
                        throw new Exception("No se pudo editar la compra!!!");
                    transaction.Commit();
                    return resp;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public async Task<List<EstadoCompraDTO>> ListarEstadosCompras()
        {
            List<TbComEstadosCompra> tbComEstadosCompras = await this._tiendaDbContext.TbComEstadosCompras.Where(est => est.EstadoVisual == true).ToListAsync();
            var listaResultado = new List<TbCompra>();
            try
            {
                
                var listaComprasDto = this._mapper.MapeoListaEstadosCompraTbaAListaEstadosCompraDto(tbComEstadosCompras);
                return listaComprasDto;
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<CompraMinDTO>> ListarCompras(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            var inicio = fechaInicial.ToDateTime(TimeOnly.MinValue);
            var fin = fechaFinal.ToDateTime(TimeOnly.MaxValue);
            IQueryable<TbCompra> tbCompras = await this._compraRepository.Consultar();
            var listaResultado = new List<TbCompra>();
            try
            {
                listaResultado = await tbCompras.Where(c => c.FechaCompra >= inicio && c.FechaCompra <= fin)
                    .Include(u => u.IdUsuarioCreadorNavigation)
                    .ThenInclude(per => per.IdPersonaNavigation)
                    .Include(e => e.IdEstadoCompraNavigation)
                    .Include(p => p.IdProveedorNavigation)
                    .ThenInclude(per => per.IdPersonaNavigation)
                    .ToListAsync();
                var listaComprasDto = this._mapper.MapeoListaCompraTbAListaCompraDto(listaResultado);
                return listaComprasDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CompraDTO> ObtenerCompra(int idCompra)
        {   
            try
            {
                TbCompra tbCompra = await this._tiendaDbContext.TbCompras
                    .Include(u => u.IdUsuarioCreadorNavigation)
                    .ThenInclude(p => p.IdPersonaNavigation)
                    .ThenInclude(id => id.IdTipoIdentificacionNavigation)
                    .Include(prov => prov.IdProveedorNavigation)
                    .ThenInclude(per => per.IdPersonaNavigation)
                    .ThenInclude(dir => dir.TbGrlDireccione)
                    .Include(est => est.IdEstadoCompraNavigation)
                    .Include(det => det.TbComDetallesCompras)
                    .ThenInclude(art => art.IdArticuloNavigation)
                    .ThenInclude(imp => imp.IdImpuestoNavigation)
                    .Include(det => det.TbComDetallesCompras)
                    .ThenInclude(art=> art.IdArticuloNavigation)
                    .ThenInclude(mar => mar.IdMarcaNavigation)
                    .Include(det => det.TbComDetallesCompras)
                    .ThenInclude(art => art.IdArticuloNavigation)
                    .ThenInclude(tp => tp.IdTipoArticuloNavigation)
                    .FirstOrDefaultAsync(c => c.Id == idCompra);

                if (tbCompra == null)
                    throw new Exception("No se encontró la compra");
                
                var compraDto = this._mapper.MapeoCompraTbACompraCompletaDto(tbCompra);
                return compraDto;
            }
            catch {
                throw;
            }
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
                    tbCompra = await this._tiendaDbContext.TbCompras.Where(c => c.Id == tbCompra.Id)
                        .Include(det => det.TbComDetallesCompras)
                        .ThenInclude(art => art.IdArticuloNavigation).FirstOrDefaultAsync();
                    var respInv = await this.AlimentarInventario(tbCompra);
                    if (respInv == false)
                        throw new Exception("No se pudo actualizar el inventario");
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

        private async Task<bool> AlimentarInventario(TbCompra compraTb){
            var inv = new TbInvInventario{
                IdCompra = compraTb.Id,
                FechaCreacion = DateTime.Now,
                TbDetallesInventarios = compraTb.TbComDetallesCompras.Select(d => new TbDetallesInventario{
                    IdArticulo = d.IdArticulo,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.ValorCompra,
                    PrecioCompra = d.ValorVenta,
                    IdTransaccionInventario = 1, // Asumiendo que 1 es el ID para "Entrada" en la tabla de transacciones de inventario 
                }).ToList(),
            };
            await this._inventarioRepository.Crear(inv);
            return true;
        }
        public async Task<bool> ReversarCompra(int id)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var tbCompra = await this._tiendaDbContext.TbCompras.Where(c => c.Id == id)
                        .FirstOrDefaultAsync();
                    if (tbCompra.IdEstadoCompra == 2)
                    {
                        throw new Exception("No se puede reversar una compra ya reversada!!!");
                    }
                    if (tbCompra is null)
                        throw new Exception("No existe una compra con el id Indicado!!!!");
                    tbCompra.IdEstadoCompra = 2;
                    tbCompra.FechaModificacion = DateTime.Now;

                    var resp = await this._compraRepository.Editar(tbCompra);
                    if (!resp) throw new Exception("No se reverso la compra, el inventario no fue afectado!!!");
                    var respInv = await this.ReversarInventario(tbCompra.Id);
                    if(!respInv) throw new Exception("No se reverso el inventario, el inventario no fue afectado!!!");
                    transaction.Commit();
                    return respInv;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        
        private async Task<bool> ReversarInventario(int idCompra)
        {
            var transacciones = await this._tiendaDbContext.TbDetallesInventarios.Where(inv => inv.IdInventario == idCompra).ToListAsync();
            var signo = await this._tiendaDbContext.TbInvTransacciones.Where(inv => inv.Nombre == "Reversion").FirstOrDefaultAsync();
            foreach (var item in transacciones)
            {
                item.IdTransaccionInventario = signo.Id;
                item.Cantidad = item.Cantidad * signo.Signo;
            }
            this._tiendaDbContext.UpdateRange(transacciones);
            await this._tiendaDbContext.SaveChangesAsync();
            return true;
        }
    }
}
