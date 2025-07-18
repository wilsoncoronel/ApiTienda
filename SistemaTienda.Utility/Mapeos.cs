
using SistemaTienda.DTO;
using SistemaTienda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.Utility
{
    public interface IMapeos
    {
        TbVenta MapeoVentaAVentaCreacion(VentaCreacionDTO ventaCreacionDto);
        TbVenDetalleVenta MapeoDetalleVentaCreacionDtoADetalleVenta(DetalleVentaCreacionDTO detalleVentaCreacionDto);
        UsuarioDTO MapeoUsuarioTbAUsuarioDto(TbSisUsuario usuarioTb);

        SesionDTO MapeoUsuarioDtoASesionDto(TbSisUsuario usuarioTb);
        TbSisUsuario MapeoUsuarioCreacionDtoATbUsuario(UsuarioCreacionDTO usuarioCreacionDto);
    }
    public class Mapeos : IMapeos
    {

         
         public SesionDTO MapeoUsuarioDtoASesionDto(TbSisUsuario usuarioTb)
         {
             var sesionDto = new SesionDTO {
                 Id = usuarioTb.Id,
                 Usuario = usuarioTb.IdPersonaNavigation.Mail,
                 Clave = usuarioTb.Password,
                 RolDto = new RolDTO {
                     Id = usuarioTb.IdRol,
                     Nombre = usuarioTb.IdRolNavigation.Nombre
                 },
             };
             return sesionDto;
         }
        public TbPedidos MapeoPedidoDtoATbPedido(PedidoDTO pedidoDto){
             return new TbPedidos
             {
                 Id = pedidoDto.Id,
                 Descripcion = pedidoDto.Descripcion,
                 Estado = pedidoDto.Estado,
                 IdUsuarioCreador = pedidoDto.IdUsuarioCreador,
                 IdEstadoPedidoNavigation = new TbPedEstadosPedido
                 {
                     Id = pedidoDto.EstadoPedidoDto.Id,
                     Nombre = pedidoDto.EstadoPedidoDto.Nombre,
                 },
                 TbPedDetallesPedidos = pedidoDto.DetallePedidoDto.Select(d => new TbPedDetallesPedido {
                     Id = d.Id,
                     Cantidad = d.Cantidad,
                     Descripcion = d.Descripcion,
                     IdArticuloNavigation = new TbComArticulo {
                         Id = d.ArticuloDTO.Id,
                         Estado = d.ArticuloDTO.Estado,
                         Codigo = d.ArticuloDTO.Codigo,
                         Nombre = d.ArticuloDTO.Nombre,
                         Unidad = d.ArticuloDTO.Unidad,
                         UnidadValor = d.ArticuloDTO.UnidadValor,
                         IdImpuestoNavigation = new TbComImpuestosArticulo { 
                             Id = d.ArticuloDTO.ImpuestoArticuloDto.Id,
                             Nombre = d.ArticuloDTO.ImpuestoArticuloDto.Nombre,
                             ValorImpuesto = d.ArticuloDTO.ImpuestoArticuloDto.ValorImpuesto,
                         },
                         ValorCompra = d.ArticuloDTO.ValorCompra,
                         
                     } 
                 }).ToList(),
             };
         }
        /* public DetallePedido MapeoDetallePedidoDtoADetallePedido(DetallePedidoDTO detallePedidoDto){
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
         }*/
         public UsuarioDTO MapeoUsuarioTbAUsuarioDto(TbSisUsuario usuarioTb){
             return new UsuarioDTO{
                 Id = usuarioTb.Id,
                 Apellidos = usuarioTb.IdPersonaNavigation.Apellidos,
                 Nombres = usuarioTb.IdPersonaNavigation.Nombres,
                 Identificacion = usuarioTb.IdPersonaNavigation.Identificacion,
                 NombreUsuario = usuarioTb.NombreUsuario,
                 Mail = usuarioTb.IdPersonaNavigation.Mail,
                 EstadoVisual = true,
                 Rol = new RolDTO
                 {
                     Id = usuarioTb.IdRol,
                     Nombre = usuarioTb.IdRolNavigation.Nombre,
                 },
                 Password = usuarioTb.Password,
             };
         }
        
         public TbSisUsuario MapeoUsuarioCreacionDtoATbUsuario(UsuarioCreacionDTO usuarioCreacionDto) {
             return new TbSisUsuario
             {
                 IdPersona = usuarioCreacionDto.IdPersona,
                 NombreUsuario = usuarioCreacionDto.NombreUsuario,
                 IdPersonaNavigation = new TbGrlPersona
                 {
                     Apellidos = usuarioCreacionDto.Apellidos,
                     Nombres = usuarioCreacionDto.Nombres,
                     Identificacion = usuarioCreacionDto.Identificacion,
                     Mail = usuarioCreacionDto.Mail,
                     TbGrlDireccione = new TbGrlDirecciones
                     {

                         IdCiudad = usuarioCreacionDto.DireccionCreacionDTO.IdCiudad,
                         EstadoVisual = true,
                         Descripcion = usuarioCreacionDto.DireccionCreacionDTO.Descripcion,
                     },
                     IdTipoIdentificacion = usuarioCreacionDto.IdTipoIdentificacion,
                     
                 },
                 Password = usuarioCreacionDto.Password,
                 IdRol = usuarioCreacionDto.IdRol,
             };
         }
        /*
         public TbVenDetalleVenta MapeoDetalleVentaCreacionDtoADetalleVenta(DetalleVentaCreacionDTO detalleVentaCreacionDto) {
             return new TbVenDetalleVenta
             {
                 IdArticulo = detalleVentaCreacionDto.ArticuloId,
                 Cantidad = detalleVentaCreacionDto.Cantidad,
                 Descripcion = detalleVentaCreacionDto.Descripcion,
                 ImpuestoValor = detalleVentaCreacionDto.ImpuestoValor,
                 ValorCompra = detalleVentaCreacionDto.ValorCompra,
                 ValorTotal = detalleVentaCreacionDto.ValorTotal,
                 IdVenta = detalleVentaCreacionDto.IdVenta,
                 //IdArticuloNavigation = new TbComArticulo
                 //{
                 //    Id = detalleVentaCreacionDto.ArticuloDetalleDto.Id,
                 //    Codigo = detalleVentaCreacionDto.ArticuloDetalleDto.Codigo,
                 //    IdImpuestoNavigation = new Tbcom
                 //    {
                 //        Id = detalleVentaCreacionDto.ArticuloDetalleDto.ImpuestoArticuloDto.Id,
                 //        ValorImpuesto = detalleVentaCreacionDto.ArticuloDetalleDto.ImpuestoArticuloDto.ValorImpuesto,
                 //        Nombre = detalleVentaCreacionDto.ArticuloDetalleDto.ImpuestoArticuloDto.Nombre
                 //    },
                 //    Unidad = detalleVentaCreacionDto.ArticuloDetalleDto.Unidad,
                 //    Nombre = detalleVentaCreacionDto.ArticuloDetalleDto.Nombre,

                 //}
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
         }*/
        public TbVenDetalleVenta MapeoDetalleVentaCreacionDtoADetalleVenta(DetalleVentaCreacionDTO detalleVentaCreacionDto)
        {
            throw new NotImplementedException();
        }

        

        public TbVenta MapeoVentaAVentaCreacion(VentaCreacionDTO ventaCreacionDto)
        {
            throw new NotImplementedException();
        }
    }
}
