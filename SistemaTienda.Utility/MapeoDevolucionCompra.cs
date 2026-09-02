using SistemaTienda.DTO;
using SistemaTienda.Model;
using System.Linq;

namespace SistemaTienda.Utility
{
    public interface IMapeoDevolucionCompra
    {
        TbComDevolucionCompra MapeoDevolucionCompraCreacionDtoATb(DevolucionCompraCreacionDTO dto);
    }

    public class MapeoDevolucionCompra : IMapeoDevolucionCompra
    {
        public TbComDevolucionCompra MapeoDevolucionCompraCreacionDtoATb(DevolucionCompraCreacionDTO dto)
        {
            var tb = new TbComDevolucionCompra
            {
                IdCompra = dto.IdCompra,
                FechaCreacion = dto.FechaCreacion == default ? System.DateTime.Now : dto.FechaCreacion,
                FechaReversion = dto.FechaReversion,
                Estado = dto.Estado,
                Motivo = dto.Motivo ?? string.Empty
            };
            if (dto.DetalleDevolucionCompraDto != null && dto.DetalleDevolucionCompraDto.Any())
            {
                tb.TbComDetalleDevolucionCompras = dto.DetalleDevolucionCompraDto.Select(d => new TbComDetalleDevolucionCompra
                {
                    IdDetalleCompra = d.IdDetalleCompra,
                    Cantidad = d.Cantidad,
                    Estado = d.Estado
                }).ToList();
            }
            return tb;
        }
    }
}
