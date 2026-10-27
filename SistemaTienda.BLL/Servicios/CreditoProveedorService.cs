using System.Data;
using Microsoft.EntityFrameworkCore;
using SistemaTienda.API.Exceptions;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DTO;
using SistemaTienda.Model;

namespace SistemaTienda.BLL.Servicios;

public class CreditoProveedorService : ICreditoProveedorService
{
    private readonly TiendaDbContext _context;

    public CreditoProveedorService(TiendaDbContext context)
    {
        _context = context;
    }

    public async Task<List<CreditoProveedorListaDTO>> ListarCreditosProveedor(int idProveedor)
    {
        if (idProveedor <= 0)
            throw new BadRequestException("El proveedor indicado no es válido");

        var proveedorExiste = await _context.TbComProveedores.AnyAsync(p => p.Id == idProveedor);
        if (!proveedorExiste)
            throw new NotFoundException("No existe el proveedor indicado");

        return await _context.TbComCreditoProveedors
            .AsNoTracking()
            .Where(c => c.IdDevolucionCompraNavigation.IdProveedor == idProveedor)
            .Select(c => new CreditoProveedorListaDTO
            {
                Id = c.Id,
                IdDevolucionCompra = c.IdDevolucionCompra,
                IdProveedor = c.IdDevolucionCompraNavigation.IdProveedor,
                Proveedor = c.IdDevolucionCompraNavigation.IdProveedorNavigation.RazonSocial,
                ValorOriginal = c.ValorOriginal,
                ValorDisponible = c.ValorDisponible,
                FechaCreacion = c.FechaCreacion,
                Estado = c.Estado
            })
            .ToListAsync();
    }

    public async Task<CreditoProveedorDTO> VerCredito(int id)
    {
        if (id <= 0)
            throw new BadRequestException("El crédito indicado no es válido");

        var credito = await ObtenerCreditoCompletoAsync(id, false);
        if (credito == null)
            throw new NotFoundException("No existe el crédito indicado");

        //return MapearCredito(credito);
        return new CreditoProveedorDTO(); // Placeholder return statement
    }

    public async Task AplicarCreditoCompra(AplicarCreditoCompraDTO dto)
    {
        if (dto == null || dto.IdCreditoProveedor <= 0 || dto.IdCompra <= 0)
            throw new BadRequestException("Los datos de aplicación del crédito son inválidos");

        var transaction = _context.Database.CurrentTransaction == null
            ? await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable)
            : null;
        try
        {
            var credito = await ObtenerCreditoCompletoAsync(dto.IdCreditoProveedor, true);
            if (credito == null)
                throw new NotFoundException("No existe el crédito indicado");

            if (!credito.Estado || credito.ValorDisponible <= 0)
                throw new BadRequestException("El crédito no está activo o no tiene saldo disponible");

            var compra = await _context.TbCompras
                .Include(c => c.IdEstadoCompraNavigation)
                .Include(c => c.TbComDetallesCompras)
                    .ThenInclude(d => d.TbComImpuestoDetalles)
                .FirstOrDefaultAsync(c => c.Id == dto.IdCompra);
            if (compra == null)
                throw new NotFoundException("No existe la compra indicada");

            var proveedorCredito = credito.IdDevolucionCompraNavigation.IdProveedor;
            if (proveedorCredito != compra.IdProveedor)
                throw new BadRequestException("El proveedor del crédito no coincide con el proveedor de la compra");

            var estadoReversada = compra.IdEstadoCompraNavigation?.Nombre;
            if (estadoReversada != null && estadoReversada.Equals("reversada", StringComparison.OrdinalIgnoreCase))
                throw new BadRequestException("La compra está reversada y no puede utilizar créditos");

            var totalCompra = CalcularTotalCompra(compra);
            if (totalCompra <= 0)
                throw new BadRequestException("El total de la compra debe ser mayor que cero");

            var valorAplicar = decimal.Round(
                Math.Min(totalCompra, credito.ValorDisponible),
                4,
                MidpointRounding.AwayFromZero);
            if (valorAplicar <= 0)
                throw new BadRequestException("El valor a aplicar debe ser mayor que cero");

            var tieneGeneracion = credito.TbComCreditoProveedorMovimientos.Any(m =>
                m.Estado && m.IdTipoMovimientoNavigation.Nombre.Equals("GENERACION", StringComparison.OrdinalIgnoreCase));
            if (!tieneGeneracion)
            {
                var tipoGeneracion = await _context.TbComTipoMovimientoCreditos
                    .FirstOrDefaultAsync(t => t.Estado && t.Nombre.ToUpper() == "GENERACION");
                if (tipoGeneracion == null)
                    throw new NotFoundException("No existe el tipo de movimiento de crédito GENERACION");

                _context.TbComCreditoProveedorMovimientos.Add(new TbComCreditoProveedorMovimiento
                {
                    IdCreditoProveedor = credito.Id,
                    IdTipoMovimiento = tipoGeneracion.Id,
                    Valor = credito.ValorOriginal,
                    FechaCreacion = DateTime.Now,
                    Observacion = "Generación automática por devolución de compra",
                    Estado = true
                });
            }

            var tipoUso = await _context.TbComTipoMovimientoCreditos
                .FirstOrDefaultAsync(t => t.Estado && t.Nombre.ToUpper() == "USO");
            if (tipoUso == null)
                throw new NotFoundException("No existe el tipo de movimiento de crédito USO");

            credito.ValorDisponible = decimal.Round(credito.ValorDisponible - valorAplicar, 4, MidpointRounding.AwayFromZero);
            credito.Estado = credito.ValorDisponible > 0;
            _context.TbComCreditoProveedorMovimientos.Add(new TbComCreditoProveedorMovimiento
            {
                IdCreditoProveedor = credito.Id,
                IdCompra = compra.Id,
                IdTipoMovimiento = tipoUso.Id,
                Valor = valorAplicar,
                FechaCreacion = DateTime.Now,
                Observacion = "Aplicación de crédito a compra",
                Estado = true
            });

            await _context.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync();
        }
        catch
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            if (transaction != null)
                await transaction.DisposeAsync();
        }
    }

    private static decimal CalcularTotalCompra(TbCompra compra)
    {
        decimal total = 0m;

        foreach (var detalle in compra.TbComDetallesCompras)
        {
            var subtotal = detalle.Cantidad * detalle.ValorCompra;
            var totalDetalle = subtotal;

            foreach (var impuesto in detalle.TbComImpuestoDetalles)
            {
                if (impuesto.IdTipoCalculoNavigation.TipoCalculo.Equals("UNIDAD", StringComparison.OrdinalIgnoreCase))
                    totalDetalle += detalle.Cantidad * impuesto.Valor;
                else if (impuesto.IdTipoCalculoNavigation.TipoCalculo.Equals("PORCENTAJE", StringComparison.OrdinalIgnoreCase))
                    totalDetalle += subtotal * impuesto.Valor;
                else
                    throw new BadRequestException($"El tipo de cálculo de impuesto '{impuesto.IdTipoCalculoNavigation.TipoCalculo}' no es válido");
            }

            total += totalDetalle;
        }

        return decimal.Round(total, 4, MidpointRounding.AwayFromZero);
    }

    public async Task ReversarCreditosCompra(int idCompra)
    {
        if (idCompra <= 0)
            throw new BadRequestException("La compra indicada no es válida");

        var movimientosUso = await _context.TbComCreditoProveedorMovimientos
            .Include(m => m.IdCreditoProveedorNavigation)
            .Include(m => m.IdTipoMovimientoNavigation)
            .Where(m => m.IdCompra == idCompra && m.Estado &&
                        m.IdTipoMovimientoNavigation.Nombre.ToUpper() == "USO")
            .ToListAsync();

        if (movimientosUso.Count == 0)
            return;

        var tipoReverso = await _context.TbComTipoMovimientoCreditos
            .FirstOrDefaultAsync(t => t.Estado && t.Nombre.ToUpper() == "REVERSO");
        if (tipoReverso == null)
            throw new NotFoundException("No existe el tipo de movimiento de crédito REVERSO");

        foreach (var grupoCredito in movimientosUso.GroupBy(m => m.IdCreditoProveedor))
        {
            var credito = grupoCredito.First().IdCreditoProveedorNavigation;
            var movimientosCredito = await _context.TbComCreditoProveedorMovimientos
                .Include(m => m.IdTipoMovimientoNavigation)
                .Where(m => m.IdCreditoProveedor == credito.Id && m.Estado)
                .ToListAsync();

            var valorUsosActivos = movimientosCredito
                .Where(m => m.IdTipoMovimientoNavigation.Nombre.ToUpper() == "USO" &&
                            !grupoCredito.Any(revertido => revertido.Id == m.Id))
                .Sum(m => m.Valor);

            var valorDisponible = decimal.Round(
                Math.Max(0m, credito.ValorOriginal - valorUsosActivos),
                4,
                MidpointRounding.AwayFromZero);

            credito.ValorDisponible = Math.Min(credito.ValorOriginal, valorDisponible);
            credito.Estado = credito.ValorDisponible > 0;

            foreach (var movimiento in grupoCredito)
            {
                movimiento.IdTipoMovimiento = tipoReverso.Id;
                movimiento.Estado = false;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task ReversarUso(int idMovimiento)
    {
        if (idMovimiento <= 0)
            throw new BadRequestException("El movimiento indicado no es válido");

        await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            var movimiento = await _context.TbComCreditoProveedorMovimientos
                .Include(m => m.IdCreditoProveedorNavigation)
                .Include(m => m.IdTipoMovimientoNavigation)
                .FirstOrDefaultAsync(m => m.Id == idMovimiento && m.Estado);
            if (movimiento == null)
                throw new NotFoundException("No existe el movimiento indicado");

            if (!movimiento.IdTipoMovimientoNavigation.Nombre.Equals("USO", StringComparison.OrdinalIgnoreCase))
                throw new BadRequestException("Solo se pueden reversar movimientos USO");

            var observacionReverso = $"Reverso del movimiento {movimiento.Id}";
            var yaReversado = await _context.TbComCreditoProveedorMovimientos
                .AnyAsync(m => m.IdCreditoProveedor == movimiento.IdCreditoProveedor && m.Estado && m.Observacion == observacionReverso);
            if (yaReversado)
                throw new ConflictException("El movimiento ya fue reversado");

            var tipoReverso = await _context.TbComTipoMovimientoCreditos
                .FirstOrDefaultAsync(t => t.Estado && t.Nombre.ToUpper() == "REVERSO");
            if (tipoReverso == null)
                throw new NotFoundException("No existe el tipo de movimiento de crédito REVERSO");

            movimiento.IdCreditoProveedorNavigation.ValorDisponible += movimiento.Valor;
            movimiento.IdCreditoProveedorNavigation.ValorDisponible = decimal.Round(
                movimiento.IdCreditoProveedorNavigation.ValorDisponible,
                4,
                MidpointRounding.AwayFromZero);
            movimiento.IdCreditoProveedorNavigation.Estado = movimiento.IdCreditoProveedorNavigation.ValorDisponible > 0;
            _context.TbComCreditoProveedorMovimientos.Add(new TbComCreditoProveedorMovimiento
            {
                IdCreditoProveedor = movimiento.IdCreditoProveedor,
                IdCompra = movimiento.IdCompra,
                IdTipoMovimiento = tipoReverso.Id,
                Valor = movimiento.Valor,
                FechaCreacion = DateTime.Now,
                Observacion = observacionReverso,
                Estado = true
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task<TbComCreditoProveedor?> ObtenerCreditoCompletoAsync(int id, bool forUpdate)
    {
        var query = _context.TbComCreditoProveedors
            .Include(c => c.IdDevolucionCompraNavigation)
                    .ThenInclude(c => c.IdProveedorNavigation)
            .Include(c => c.IdDevolucionCompraNavigation)
                .ThenInclude(d => d.TbComDetalleDevolucionCompras)
            .Include(c => c.TbComCreditoProveedorMovimientos)
                .ThenInclude(m => m.IdTipoMovimientoNavigation)
            .Where(c => c.Id == id);

        return await query.FirstOrDefaultAsync();
    }

    /*private static CreditoProveedorDTO MapearCredito(TbComCreditoProveedor credito)
    {
        var devolucion = credito.IdDevolucionCompraNavigation;
        var result = new CreditoProveedorDTO
        {
            Id = credito.Id,
            IdDevolucionCompra = credito.IdDevolucionCompra,
            ValorOriginal = credito.ValorOriginal,
            ValorDisponible = credito.ValorDisponible,
            FechaCreacion = credito.FechaCreacion,
            Estado = credito.Estado
        };

        foreach (var detalle in devolucion.TbComDetalleDevolucionCompras)
        {
            var subtotal = detalle.Cantidad * compraDetalle.ValorCompra;
            var total = subtotal;
            foreach (var impuesto in compraDetalle.TbComImpuestoDetalles)
            {
                total += impuesto.TipoCalculo.Equals("Unidad", StringComparison.OrdinalIgnoreCase)
                    ? detalle.Cantidad * impuesto.Valor
                    : subtotal * impuesto.Valor;
            }

            result.Detalles.Add(new CreditoProveedorDetalleDTO
            {
                Id = detalle.Id,
                IdArticulo = compraDetalle.IdArticulo,
                Descripcion = compraDetalle.Descripcion,
                Cantidad = detalle.Cantidad,
                ValorCompra = compraDetalle.ValorCompra,
                Subtotal = subtotal,
                Impuestos = total - subtotal,
                Total = total,
                Estado = detalle.Estado
            });
        }

        result.Movimientos = credito.TbComCreditoProveedorMovimientos
            .Select(m => new CreditoProveedorMovimientoDTO
            {
                Id = m.Id,
                IdCreditoProveedor = m.IdCreditoProveedor,
                IdCompra = m.IdCompra,
                IdTipoMovimiento = m.IdTipoMovimiento,
                TipoMovimiento = m.IdTipoMovimientoNavigation.Nombre,
                Valor = m.Valor,
                FechaCreacion = m.FechaCreacion,
                Observacion = m.Observacion,
                Estado = m.Estado
            })
            .ToList();

        return result;
    }*/
}
