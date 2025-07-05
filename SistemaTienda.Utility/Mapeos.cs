using ApiTienda.Model;
using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.Utility
{
    public interface IMapeos
    {
        Usuario MapeoUsuarioCreacionDtoAUsuario(UsuarioCreacionDTO usuarioCreacionDto);
        Venta MapeoVentaAVentaCreacion(VentaCreacionDTO ventaCreacionDto);
        DetalleVenta MapeoDetalleVentaCreacionDtoADetalleVenta(DetalleVentaCreacionDTO detalleVentaCreacionDto);
        UsuarioDTO MapeoUsuarioTbAUsuarioDto(Usuario usuarioTb);
    }
    public class Mapeos:IMapeos
    {
        public Pedido MapeoPedidoDtoAPedido(PedidoDTO pedidoDto){
            return new Pedido {
                Id = pedidoDto.Id,
                Descripcion = pedidoDto.Descripcion,
                Estado = pedidoDto.Estado,
                DetallePedido = pedidoDto.DetallePedidoDto.Select({
                    
                }).ToList(),
            };
        }
        public DetallePedido MapeoDetallePedidoDtoADetallePedido(DetallePedidoDTO detallePedidoDto){
            return new DetallePedido
            {
                Id = detallePedidoDto.Id,
                Articulo = new Articulo
                {
                    Id = detallePedidoDto.ArticuloDTO.Id,
                    Codigo = detallePedidoDto.ArticuloDTO.Codigo,
                    Descripcion = detallePedidoDto.ArticuloDTO.Descripcion,
                    Marca = new Marca
                    {
                        Id = detallePedidoDto.ArticuloDTO.MarcaDTO.Id,
                        Descripcion = detallePedidoDto.ArticuloDTO.MarcaDTO.Descripcion,
                        Nombre = detallePedidoDto.ArticuloDTO.MarcaDTO.Nombre,
                        Estado = detallePedidoDto.ArticuloDTO.MarcaDTO.Estado
                    },
                    Estado = detallePedidoDto.ArticuloDTO.Estado,
                    Nombre = detallePedidoDto.ArticuloDTO.Nombre,
                    FechaCaducidad = detallePedidoDto.ArticuloDTO.FechaCaducidad,
                    EstadoVisual = detallePedidoDto.ArticuloDTO.EstadoVisual,
                    ImpuestoArticulo = new ImpuestoArticulo
                    {
                        Id = detallePedidoDto.ArticuloDTO.ImpuestoArticuloDto.Id,
                        Descripcion = detallePedidoDto.ArticuloDTO.ImpuestoArticuloDto.Descripcion,
                        ValorImpuesto = detallePedidoDto.ArticuloDTO.ImpuestoArticuloDto.ValorImpuesto,
                        Nombre = detallePedidoDto.ArticuloDTO.ImpuestoArticuloDto.Nombre,
                    },
                    Unidad = detallePedidoDto.ArticuloDTO.Unidad,
                    FechaCreacion = detallePedidoDto.ArticuloDTO.FechaCaducidad,
                    UnidadValor = detallePedidoDto.ArticuloDTO.UnidadValor,
                    TipoArticulo = new TipoArticulo
                    {
                        Id = detallePedidoDto.ArticuloDTO.TipoArticuloDTO.Id,
                        Nombre = detallePedidoDto.ArticuloDTO.TipoArticuloDTO.Nombre,
                        Descripcion = detallePedidoDto.ArticuloDTO.TipoArticuloDTO.Descripcion,
                    },
                    FechaActualizacion = detallePedidoDto.ArticuloDTO.FechaActualizacion,
                },
                Cantidad = detallePedidoDto.Cantidad,
                ImpuestoValor = detallePedidoDto.ImpuestoValor,
                ValorCompra = detallePedidoDto.ValorCompra,
                ValorTotal = detallePedidoDto.ValorTotal,
                
            };
        }
        public UsuarioDTO MapeoUsuarioTbAUsuarioDto(Usuario usuarioTb){
            return new UsuarioDTO{
                Id = usuarioTb.Id,
                Apellidos = usuarioTb.Persona.Apellidos,
                Nombres = usuarioTb.Persona.Nombres,
                Identificacion = usuarioTb.Persona.Identificacion,
                Telefono = usuarioTb.Persona.Telefono,
                NombreUsuario = usuarioTb.NombreUsuario,
                Mail = usuarioTb.Persona.Mail,
                EstadoVisual = true,
                Rol = new RolDTO
                {
                    Id = usuarioTb.IdRol,
                    Nombre = usuarioTb.Rol.Nombre,
                },
                Password = usuarioTb.Password,
            };
        }

        public Usuario MapeoUsuarioCreacionDtoAUsuario(UsuarioCreacionDTO usuarioCreacionDto) {
            return new Usuario
            {
                IdPersona = usuarioCreacionDto.IdPersona,
                NombreUsuario = usuarioCreacionDto.NombreUsuario,
                Persona = new Persona
                {
                    Apellidos = usuarioCreacionDto.Apellidos,
                    Nombres = usuarioCreacionDto.Nombres,
                    EstadoVisual = usuarioCreacionDto.EstadoVisual,
                    Identificacion = usuarioCreacionDto.Identificacion,
                    Mail = usuarioCreacionDto.Mail,
                    Telefono = usuarioCreacionDto.Telefono,
                    Direccion = new Direccion
                    {
                        IdCiudad = usuarioCreacionDto.DireccionCreacionDTO.IdCiudad,
                        Descripcion = usuarioCreacionDto.DireccionCreacionDTO.Descripcion,
                    },
                },
                Password = usuarioCreacionDto.Password,
                IdRol = usuarioCreacionDto.IdRol,

                
            };
        }

        public DetalleVenta MapeoDetalleVentaCreacionDtoADetalleVenta(DetalleVentaCreacionDTO detalleVentaCreacionDto) {
            return new DetalleVenta{
                IdArticulo = detalleVentaCreacionDto.ArticuloId,
                Cantidad = detalleVentaCreacionDto.Cantidad,
                Descripcion = detalleVentaCreacionDto.Descripcion,
                ImpuestoValor = detalleVentaCreacionDto.ImpuestoValor,
                ValorCompra = detalleVentaCreacionDto.ValorCompra,
                ValorTotal = detalleVentaCreacionDto.ValorTotal,
            };
        }
        public Venta MapeoVentaAVentaCreacion(VentaCreacionDTO ventaCreacionDto) {
            return new Venta
            {
                IdCliente = ventaCreacionDto.IdCliente,
                Documento = ventaCreacionDto.Documento,
                IdEstado = ventaCreacionDto.IdEstado,
                EstadoVisual = ventaCreacionDto.EstadoVisual,
                SubTotal = ventaCreacionDto.SubTotal,
                Total = ventaCreacionDto.Total,
                UsuarioCreadorId = ventaCreacionDto.UsuarioCreadorId,
                DetalleVenta = ventaCreacionDto.DetalleVenta.Select(this.MapeoDetalleVentaCreacionDtoADetalleVenta).ToList(),
                FechaCreacion = DateTime.Now,
                FechaVenta = DateTime.Now,
                ValorIva = ventaCreacionDto.ValorIva,
            };
        }

    }
}
