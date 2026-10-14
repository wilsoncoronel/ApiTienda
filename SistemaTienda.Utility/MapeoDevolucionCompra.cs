using SistemaTienda.DTO;
using SistemaTienda.Model;
using System.Linq;

namespace SistemaTienda.Utility
{
    public interface IMapeoDevolucionCompra
    {
        TbComDevolucionCompra MapeoDevolucionCompraCreacionDtoATb(DevolucionCompraCreacionDTO dto);
        List<DevolucionCompraDTO> MapeoListaDevolucionesCompraTbADto(List<TbComDevolucionCompra> devoluciones);
        List<DetalleDevolucionCompraDTO> MapeoListaDetallesDevolucionTbADto(List<TbComDetalleDevolucionCompra> detallesDevolucion);
    }

    public class MapeoDevolucionCompra : IMapeoDevolucionCompra
    {
        public TbComDevolucionCompra MapeoDevolucionCompraCreacionDtoATb(DevolucionCompraCreacionDTO dto)
        {
            var tb = new TbComDevolucionCompra
            {
                IdCompra = dto.IdCompra,
                IdTipoDevolucion = dto.IdTipoDevolucion,
                FechaCreacion = dto.FechaCreacion == default ? System.DateTime.Now : dto.FechaCreacion,
                Estado = dto.Estado,
                Motivo = dto.Motivo ?? string.Empty
            };
            if (dto.DetalleDevolucionCompraCreacionDTO != null && dto.DetalleDevolucionCompraCreacionDTO.Any())
            {
                tb.TbComDetalleDevolucionCompras = dto.DetalleDevolucionCompraCreacionDTO.Select(d => new TbComDetalleDevolucionCompra
                {
                    IdDetalleCompra = d.IdDetalleCompra,
                    Cantidad = d.Cantidad,
                    Estado = d.Estado
                }).ToList();
            }
            return tb;
        }

        public List<DetalleDevolucionCompraDTO> MapeoListaDetallesDevolucionTbADto(List<TbComDetalleDevolucionCompra> detallesDevolucion)
        {
            return detallesDevolucion.Select(det => this.MapeoDetalleDevolucion(det)).ToList();
        }
        public DetalleDevolucionCompraDTO MapeoDetalleDevolucion(TbComDetalleDevolucionCompra detalle)
        {
            var detalleCompra = detalle.IdDetalleCompraNavigation;
            var producto = detalleCompra?.IdArticuloNavigation;
            return new DetalleDevolucionCompraDTO
            {
                Id = detalle.Id,
                IdDetalleCompra = detalle.IdDetalleCompra,
                Cantidad = detalle.Cantidad,
                Estado = detalle.Estado,
                DetalleCompraDTO = detalleCompra == null ? null! : new DetalleCompraDTO
                {
                    Id = detalleCompra.Id,
                    IdCompra = detalleCompra.IdCompra,
                    IdArticulo = detalleCompra.IdArticulo,
                    Lote = detalleCompra.NumeroLote,
                    Cantidad = detalleCompra.Cantidad,
                    ValorCompra = detalleCompra.ValorCompra,
                    ArticuloDTO= producto == null ? null! : new ArticuloDTO
                    {
                        Id = producto.Id,
                        Nombre = producto.Nombre,
                        Descripcion = producto.Descripcion,
                        ValorCompra = producto.ValorCompra,
                        EstadoVisual = producto.EstadoVisual,
                        Estado = producto.Estado
                    },
                    Impuestos = detalleCompra.TbComImpuestoDetalles.Select(impuesto => new DetalleImpuestoDTO
                    {
                        Id = impuesto.Id,
                        IdImpuesto = 0,
                        IdDetalleCompra = impuesto.IdDetalleCompra,
                        Valor = impuesto.Valor,
                        Nombre = impuesto.Nombre ?? string.Empty,
                        TipoCalculo = impuesto.TipoCalculo ?? string.Empty,
                                                                                                
                    }).ToList()
                }
            };
        }
        public List<DevolucionCompraDTO> MapeoListaDevolucionesCompraTbADto(List<TbComDevolucionCompra> devoluciones)
        {
            return devoluciones.Select(devolucion =>
            {
                var compra = devolucion.IdCompraNavigation;
                var proveedor = compra?.IdProveedorNavigation;
                var personaProveedor = proveedor?.IdPersonaNavigation;
                var usuario = compra?.IdUsuarioCreadorNavigation;
                var personaUsuario = usuario?.IdPersonaNavigation;
                var estadoCompra = compra?.IdEstadoCompraNavigation;

                return new DevolucionCompraDTO
                {
                    Id = devolucion.Id,
                    IdCompra = devolucion.IdCompra,
                    IdTipoDevolucion = devolucion.IdTipoDevolucion,
                    TipoDevolucionDTO = devolucion.IdTipoDevolucionNavigation == null ? null! : new TipoDevolucionDTO
                    {
                        Id = devolucion.IdTipoDevolucionNavigation.Id,
                        Nombre = devolucion.IdTipoDevolucionNavigation.Nombre
                    },
                    FechaCreacion = devolucion.FechaCreacion,
                    Estado = devolucion.Estado,
                    Motivo = devolucion.Motivo,
                    CompraDTO = compra == null ? null! : new CompraDTO
                    {
                        Id = compra.Id,
                        IdProveedor = compra.IdProveedor,
                        Documento = compra.Documento,
                        FechaCompra = compra.FechaCompra,
                        FechaCreacion = compra.FechaCreacion,
                        FechaModificacion = compra.FechaModificacion,
                        IdEstado = compra.IdEstadoCompra,
                        IdTransaccion = compra.IdTransaccion,
                        EstadoVisual = compra.EstadoVisual,
                        IdUsuarioCreador = compra.IdUsuarioCreador,
                        EstadoCompra = estadoCompra == null ? null! : new EstadoCompraDTO
                        {
                            Id = estadoCompra.Id,
                            Nombre = estadoCompra.Nombre,
                            EstadoVisual = estadoCompra.EstadoVisual,
                            Descripcion = estadoCompra.Descripcion
                        },
                        ProveedorDto = proveedor == null ? null! : new ProveedorDTO
                        {
                            Id = proveedor.Id,
                            Nombres = personaProveedor?.Nombres,
                            Apellidos = personaProveedor?.Apellidos,
                            Mail = personaProveedor?.Mail,
                            Identificacion = personaProveedor?.Identificacion,
                            Telefono = personaProveedor?.Telefono,
                            RazonSocial = proveedor.RazonSocial,
                            Descripcion = proveedor.Descripcion,
                            EstadoVisual = proveedor.EstadoVisual,
                            Estado = proveedor.Estado,
                            FechaCreacion = personaProveedor?.FechaCreacion,
                            FechaModificacion = personaProveedor?.FechaModificacion
                        },
                        UsuarioCreador = usuario == null ? null! : new UsuarioDTO
                        {
                            Id = usuario.Id,
                            IdPersona = usuario.IdPersona,
                            IdRol = usuario.IdRol,
                            NombreUsuario = usuario.NombreUsuario,
                            Estado = usuario.Estado,
                            Nombres = personaUsuario?.Nombres,
                            Apellidos = personaUsuario?.Apellidos,
                            Mail = personaUsuario?.Mail,
                            Identificacion = personaUsuario?.Identificacion,
                            Telefono = personaUsuario?.Telefono
                        }
                    }
                };
            }).ToList();
        }
    }
}
