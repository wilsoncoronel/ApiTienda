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
                            existente.ValorTotal = detDto.ValorTotal;
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
                                ValorTotal = detDto.ValorTotal
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

        public Task<List<VentaMinDTO>> ListarVentas(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            throw new NotImplementedException();
        }

        public Task<VentaDTO> ObtenerVenta(int idVenta)
        {
            throw new NotImplementedException();
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
                TbDetallesInventarios = ventaTb.TbVenDetalleVenta.Select(d => new TbDetallesInventario
                {
                    IdArticulo = d.IdArticulo,
                    Cantidad = d.Cantidad,
                    PrecioCompra = d.ValorCompra,
                    PrecioVenta = d.ValorTotal,
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
