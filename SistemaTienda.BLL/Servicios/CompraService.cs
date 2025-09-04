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
        public readonly IGenericRepository<TbComDetallesCompra> _detalleRepository;
        private readonly IMapeos _mapper;
        public CompraService(TiendaDbContext tiendaDbContext, IGenericRepository<TbCompra> compraRepository, IGenericRepository<TbComDetallesCompra> detalleRepository, IMapeos mapper)
        {
            _tiendaDbContext = tiendaDbContext;
            _compraRepository = compraRepository;
            _detalleRepository = detalleRepository;
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

        public async Task<List<CompraDTO>> ListarCompras(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            try {
                var tbCompras = await this._compraRepository.Consultar(c => c.FechaCompra >= Convert.ToDateTime(fechaInicial) && c.FechaCompra <= Convert.ToDateTime(fechaFinal));
            }
            catch { 
                throw;
            }
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
