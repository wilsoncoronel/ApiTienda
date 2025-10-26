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
    public class VentaService : IVentaService
    {
        private readonly TiendaDbContext _tiendaDbContext;
        private readonly IGenericRepository<TbVenta> _ventaRepository;
        private readonly IGenericRepository<TbVenDetalleVenta> _detalleRepository;
        private readonly IGenericRepository<TbInvInventario> _inventarioRepository;
        private readonly IMapeos _mapper;

        public VentaService(TiendaDbContext tiendaDbContext, IGenericRepository<TbVenta> ventaRepository, IGenericRepository<TbVenDetalleVenta> detalleRepository, IGenericRepository<TbInvInventario> inventarioRepository, IMapeos mapper)
        {
            this._tiendaDbContext = tiendaDbContext;
            this._ventaRepository = ventaRepository;
            this._detalleRepository = detalleRepository;
            this._inventarioRepository = inventarioRepository;
            this._mapper = mapper;
        }
        public async Task<bool> EditarVenta(VentaEditarDTO ventaDto)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var tbVenta = await this._tiendaDbContext.TbVentas.Include(c => c.IdEstadoVentaNavigation)
                        .Include(d => d.TbVenDetalleVenta)
                        .FirstOrDefaultAsync(c => c.Id == ventaDto.Id);
                    var idsDto = ventaDto.DetalleVentaEditarDto.Select(x => x.Id).ToList();
                    var eliminados = tbVenta.TbVenDetalleVenta
                        .Where(x => !idsDto.Contains(x.Id)).ToList();

                    foreach (var e in eliminados)
                    {
                        _tiendaDbContext.TbVenDetalleVenta.Remove(e);
                    }

                    foreach (var detDto in ventaDto.DetalleVentaEditarDto)
                    {
                        var existente = tbVenta.TbVenDetalleVenta.FirstOrDefault(x => x.Id == detDto.Id);
                        if (existente != null)
                        {
                            // actualizar
                            existente.IdArticulo = detDto.ArticuloId;
                            existente.Cantidad = detDto.Cantidad;
                            existente.Descripcion = detDto.Descripcion;
                            existente.ImpuestoValor = detDto.ImpuestoValor;
                            existente.ValorCompra = detDto.ValorCompra;
                            existente.ValorVenta = detDto.ValorVenta;   
                            existente.ValotTotal = detDto.ValorTotal;
                        }
                        else
                        {
                            // nuevo
                            tbVenta.TbVenDetalleVenta.Add(new TbVenDetalleVenta
                            {
                                IdArticulo = detDto.ArticuloId,
                                Cantidad = detDto.Cantidad,
                                Descripcion = detDto.Descripcion,
                                ImpuestoValor = detDto.ImpuestoValor,
                                ValorCompra = detDto.ValorCompra,
                                ValorVenta = detDto.ValorVenta,
                                ValotTotal = detDto.ValorTotal
                            });
                        }
                    }
                    this._mapper.MapeoVentaEdicionDtoAVentaTb(ventaDto, tbVenta);
                    var resp = await this._ventaRepository.Editar(tbVenta);
                    if (resp == false)
                        throw new Exception("No se pudo editar la venta!!!");
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

        public async Task<List<EstadoVentaDTO>> ListarEstadosVentas()
        {
            List<TbVenEstadosVenta> tbVenEstadosVentas = await this._tiendaDbContext.TbVenEstadosVentas.Where(est => est.EstadoVisual == true).ToListAsync();
            try
            {
                var listaEstadosDto = this._mapper.MapeoListaEstadosVentaTbaAListaEstadosVentaDto(tbVenEstadosVentas);
                return listaEstadosDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VentaMinDTO>> ListarVentas(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            var inicio = fechaInicial.ToDateTime(TimeOnly.MinValue);
            var fin = fechaFinal.ToDateTime(TimeOnly.MaxValue);
            IQueryable<TbVenta> tbVentas = await this._ventaRepository.Consultar();
            var listaResultado = new List<TbVenta>();
            try
            {
                listaResultado = await tbVentas.Where(c => c.FechaVenta >= inicio && c.FechaVenta <= fin)
                    .Include(u => u.IdUsuarioCreadorNavigation)
                    .ThenInclude(per => per.IdPersonaNavigation)
                    .Include(e => e.IdEstadoVentaNavigation)
                    .Include(p => p.IdClienteNavigation)
                    .ThenInclude(per => per.IdPersonaNavigation)
                    .ToListAsync();
                var listaVentasDto = this._mapper.MapeoListaVentasTbAListaVentasDto(listaResultado);
                return listaVentasDto;
            }
            catch
            {
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<VentaDTO> ObtenerVenta(int idVenta)
        {
            try
            {
                TbVenta tbVenta= await this._tiendaDbContext.TbVentas
                    .Include(u => u.IdUsuarioCreadorNavigation)
                    .ThenInclude(p => p.IdPersonaNavigation)
                    .ThenInclude(id => id.IdTipoIdentificacionNavigation)
                    .Include(prov => prov.IdClienteNavigation)
                    .ThenInclude(per => per.IdPersonaNavigation)
                    .ThenInclude(dir => dir.TbGrlDireccione)
                    .ThenInclude(ciu => ciu.IdCiudadNavigation)
                    .Include(est => est.IdEstadoVentaNavigation)
                    .Include(det => det.TbVenDetalleVenta)
                    .ThenInclude(art => art.IdArticuloNavigation)
                    .ThenInclude(imp => imp.IdImpuestoNavigation)
                    .Include(det => det.TbVenDetalleVenta)
                    .ThenInclude(art => art.IdArticuloNavigation)
                    .ThenInclude(mar => mar.IdMarcaNavigation)
                    .Include(det => det.TbVenDetalleVenta)
                    .ThenInclude(art => art.IdArticuloNavigation)
                    .ThenInclude(tp => tp.IdTipoArticuloNavigation)
                    .FirstOrDefaultAsync(c => c.Id == idVenta);

                if (tbVenta == null)
                    throw new Exception("No se encontró la venta!!");

                var ventaDto = this._mapper.MapeoVentaTbAVentaCompletaDto(tbVenta);
                return ventaDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> RegistrarVenta(VentaCreacionDTO ventaDto)
        {
            using (var transaction = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var tbVenta = this._mapper.MapeoVentaCreacionDtoAVentaTb(ventaDto);
                    await this._ventaRepository.Crear(tbVenta);
                    if (tbVenta.Id == 0)
                        throw new Exception("No se pudo registrar la venta!!");
                    tbVenta = await this._tiendaDbContext.TbVentas.Where(c => c.Id == tbVenta.Id)
                        .Include(det => det.TbVenDetalleVenta)
                        .ThenInclude(art => art.IdArticuloNavigation).FirstOrDefaultAsync();
                    var respInv = await this.AlimentarInventario(tbVenta);
                    if (respInv == false)
                        throw new Exception("No se pudo actualizar el inventario");
                    transaction.Commit();
                    return tbVenta.Id;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private async Task<bool> AlimentarInventario(TbVenta ventaTb)
        {
            var inv = new TbInvInventario
            {
                IdVenta = ventaTb.Id,
                FechaCreacion = DateTime.Now,
                IdCompra = null,
                TbDetallesInventarios = ventaTb.TbVenDetalleVenta.Select(d => new TbDetallesInventario
                {
                    IdArticulo = d.IdArticulo,
                    Cantidad = d.Cantidad,
                    PrecioCompra = d.ValorCompra,
                    PrecioVenta = d.ValorVenta,
                    IdTransaccionInventario = 2, // Asumiendo que 1 es el ID para "Entrada" en la tabla de transacciones de inventario 
                }).ToList(),
            };
            await this._inventarioRepository.Crear(inv);
            return true;
        }

        public Task<bool> ReversarVenta(int id)
        {
            throw new NotImplementedException();
        }
    }
}
