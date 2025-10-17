
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
        List<UsuarioDTO> MapeoArrayUsuarioTbAUsuarioDto(IEnumerable<TbSisUsuario> usuariosListTb);
        TbComArticulo MapeoArticuloCreacionDtoAArticuloTb(ArticuloCreacionDTO articuloCreacionDto);
        TbComArticulo MapeoArticuloDtoAArticuloTb(ArticuloDTO articuloDto);
        void MapeoUsuarioEdicionDtoATbUsuario(UsuarioEditarDTO usuarioEditarDTO, TbSisUsuario usuarioTb);
        TbCompra MapeoCompraCreacionDtoACompraTb(CompraCreacionDTO compraCreacionDto);
        TbComDetallesCompra MapeoDetalleCompraCreacionDtoADetalleCompraDto(DetalleCompraCreacionDTO detalleComraCreacionDto);

        TbComProveedores MapeoProveedorDtoAProveedorTb(ProveedorCreacionDTO provedorCreacion);
        void MapeoProveedorEditarDtoAProveedorTb(ProveedorEditarDTO provedorEditar, TbComProveedores proveedoresTb);
        void MapeoCompraEdicionDtoACompraTb(CompraEditarDTO compraEditarDto, TbCompra compraTb);
        List<CompraMinDTO> MapeoListaCompraTbAListaCompraDto(List<TbCompra> listaResultado);
        CompraMinDTO MapeoCompraTbACompraDto(TbCompra compraTb);
        CompraDTO MapeoCompraTbACompraCompletaDto(TbCompra compraTb);
        PermisosRolDTO MapeoTbSisPermisorRolAPermisosRolDTO(TbSisPermisosRol permisosRolTb);
        List<PermisosRolDTO> MapeoListaTbSisPermisosRolAPermisosRolDTO(IEnumerable<TbSisPermisosRol> listaPermisosRolTb);
        List<ArticuloDTO> MapeoListaArticulosDto(List<TbComArticulo> listaArticulosTb);
        ArticuloDTO MapeoArticuloTbAArticuloDto(TbComArticulo articuloTb);
        ProveedorDTO MapeoProveedorTbAProveedorDto(TbComProveedores provedorTb);
        EstadoCompraDTO MapeoEstadosCompraTbaAEstadosCompraDto(TbComEstadosCompra EstadosTb);
        List<EstadoCompraDTO> MapeoListaEstadosCompraTbaAListaEstadosCompraDto(List<TbComEstadosCompra> ListaEstadosDto);
        TbVenta MapeoVentaCreacionDtoAVentaTb(VentaCreacionDTO ventaCreacionDto);
        TbVenDetalleVenta MapeoDetalleVentaCreacionDtoADetalleVentaDto(DetalleVentaCreacionDTO detalleVentaCreacionDto);
        EstadoVentaDTO MapeoEstadosVentaTbaAEstadosVentaDto(TbVenEstadosVenta EstadosTb);
        List<EstadoVentaDTO> MapeoListaEstadosVentaTbaAListaEstadosVentaDto(List<TbVenEstadosVenta> ListaEstadosDto);
        void MapeoVentaEdicionDtoAVentaTb(VentaEditarDTO ventaEditarDto, TbVenta ventaTb);
        ClienteDTO MapeoClienteTbAClienteDto(TbComCliente clienteTb);
    }
    public class Mapeos : IMapeos
    {
        private DateTime FechaGrl = DateTime.Now;

        public EstadoCompraDTO MapeoEstadosCompraTbaAEstadosCompraDto(TbComEstadosCompra EstadosTb)
        {
            return new EstadoCompraDTO
            {
                Id = EstadosTb.Id,
                Nombre = EstadosTb.Nombre,
            };
        }

        public List<EstadoCompraDTO> MapeoListaEstadosCompraTbaAListaEstadosCompraDto(List<TbComEstadosCompra> ListaEstadosDto)
        {

            return ListaEstadosDto.Select( est =>this.MapeoEstadosCompraTbaAEstadosCompraDto(est)).ToList();
        }


        public EstadoVentaDTO MapeoEstadosVentaTbaAEstadosVentaDto(TbVenEstadosVenta EstadosTb)
        {
            return new EstadoVentaDTO
            {
                Id = EstadosTb.Id,
                Nombre = EstadosTb.Nombre,
            };
        }

        public List<EstadoVentaDTO> MapeoListaEstadosVentaTbaAListaEstadosVentaDto(List<TbVenEstadosVenta> ListaEstadosDto)
        {

            return ListaEstadosDto.Select(est => this.MapeoEstadosVentaTbaAEstadosVentaDto(est)).ToList();
        }
        public TbComArticulo MapeoArticuloCreacionDtoAArticuloTb(ArticuloCreacionDTO articuloCreacionDto){
            var articuloTb = new TbComArticulo {
                Codigo = articuloCreacionDto.Codigo,
                Descripcion = articuloCreacionDto.Descripcion,
                FechaCaducidad = articuloCreacionDto.FechaCaducidad,
                FechaCreacion = articuloCreacionDto.FechaCreacion,
                Estado = articuloCreacionDto.Estado,
                EstadoVisual = articuloCreacionDto.EstadoVisual,
                IdImpuesto = articuloCreacionDto.IdImpuesto,
                Nombre = articuloCreacionDto.Nombre,
                Unidad = articuloCreacionDto.Unidad,
                ValorCompra = articuloCreacionDto.ValorCompra,
                UnidadValor = articuloCreacionDto.UnidadValor,
                ValorVenta = articuloCreacionDto.UnidadValor,
                IdTipoArticulo = articuloCreacionDto.IdTipoArticulo,
                IdMarca = articuloCreacionDto.IdMarca,
                IdUsuarioCreador = articuloCreacionDto.IdUsuarioCreador,
            };
            return articuloTb;
        }

        public TbComArticulo MapeoArticuloDtoAArticuloTb(ArticuloDTO articuloDto)
        {
            var articuloTb = new TbComArticulo
            {
                Id = articuloDto.Id,
                Codigo = articuloDto.Codigo,
                Descripcion = articuloDto.Descripcion,
                Estado = articuloDto.Estado,
                EstadoVisual = articuloDto.EstadoVisual,
                IdImpuestoNavigation = new TbComImpuestosArticulo
                {
                    Id = articuloDto.ImpuestoArticuloDto.Id,
                    ValorImpuesto = articuloDto.ImpuestoArticuloDto.ValorImpuesto,
                },
                Nombre = articuloDto.Nombre,
                Unidad = articuloDto.Unidad,
                ValorCompra = articuloDto.ValorCompra,
                UnidadValor = articuloDto.UnidadValor,
                ValorVenta = articuloDto.ValorVenta,
                IdTipoArticuloNavigation = new TbComTiposArticulo
                {
                    Id = articuloDto.IdTipoArticulo,
                    Descripcion = articuloDto.TipoArticuloDTO.Descripcion,
                    Nombre = articuloDto.TipoArticuloDTO.Nombre,
                },
            };
            return articuloTb;
        }

        public ArticuloDTO MapeoArticuloTbAArticuloDto(TbComArticulo articuloTb)
        {
            var articuloDto = new ArticuloDTO
            {
                Id = articuloTb.Id,
                Codigo = articuloTb.Codigo,
                Descripcion = articuloTb.Descripcion,
                Estado = articuloTb.Estado,
                EstadoVisual = articuloTb.EstadoVisual,
                ImpuestoArticuloDto = new ImpuestoArticuloDTO
                {
                    Id = articuloTb.IdImpuestoNavigation.Id,
                    ValorImpuesto = articuloTb.IdImpuestoNavigation.ValorImpuesto,
                    Nombre = articuloTb.IdImpuestoNavigation.Nombre,
                },
                Nombre = articuloTb.Nombre,
                Unidad = articuloTb.Unidad,
                ValorCompra = articuloTb.ValorCompra,
                UnidadValor = articuloTb.UnidadValor,
                ValorVenta = articuloTb.ValorVenta,
                FechaActualizacion = articuloTb.FechaActualizacion ?? articuloTb.FechaCreacion,
                FechaCaducidad = articuloTb.FechaCaducidad,
                FechaCreacion = articuloTb.FechaCreacion,
                TipoArticuloDTO = new TipoArticuloDTO
                {
                    Id = articuloTb.IdTipoArticulo,
                    Descripcion = articuloTb.IdTipoArticuloNavigation.Descripcion,
                    Nombre = articuloTb.IdTipoArticuloNavigation.Nombre,
                },
                MarcaDTO = new MarcaDTO
                {
                    Id = articuloTb.IdMarcaNavigation.Id,
                    Descripcion = articuloTb.IdMarcaNavigation.Descripcion,
                    Nombre = articuloTb.IdMarcaNavigation.Nombre,
                },
            };
            return articuloDto;
        }

        public List<ArticuloDTO> MapeoListaArticulosDto(List<TbComArticulo> listaArticulosTb)
        {
            return listaArticulosTb.Select(a => this.MapeoArticuloTbAArticuloDto(a)).ToList();
        }

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
        public SesionDTO MapeoUsuarioTbASesionDto(TbSisUsuario usuarioTb)
        {
            var sesionDto = new SesionDTO
            {
                Id = usuarioTb.Id,
                Usuario = usuarioTb.IdPersonaNavigation.Mail,
                Clave = usuarioTb.Password,
                RolDto = new RolDTO
                {
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


             var usuarioDto =  new UsuarioDTO{
                 Id = usuarioTb.Id,
                 Apellidos = usuarioTb.IdPersonaNavigation.Apellidos,
                 Nombres = usuarioTb.IdPersonaNavigation.Nombres,
                 Identificacion = usuarioTb.IdPersonaNavigation.Identificacion,
                 NombreUsuario = usuarioTb.NombreUsuario,
                 IdPersona = usuarioTb.IdPersona,
                 Mail = usuarioTb.IdPersonaNavigation.Mail,
                 Estado = usuarioTb.Estado,
                 DireccionDto = new DireccionDTO
                 {
                     Id = usuarioTb.IdPersonaNavigation.TbGrlDireccione.Id,
                     IdPersona = usuarioTb.IdPersona,
                     Ciudad = new CiudadDTO
                     {
                         Id = usuarioTb.IdPersonaNavigation.TbGrlDireccione.IdCiudadNavigation.Id,
                         Nombre = usuarioTb.IdPersonaNavigation.TbGrlDireccione.IdCiudadNavigation.Nombre,
                     },
                     IdCiudad = usuarioTb.IdPersonaNavigation.TbGrlDireccione.IdCiudad,
                     Descripcion = usuarioTb.IdPersonaNavigation.TbGrlDireccione.Descripcion,
                     EstadoVisual = usuarioTb.IdPersonaNavigation.TbGrlDireccione.EstadoVisual,
                 },
                 EstadoVisual = true,
                 Rol = new RolDTO
                 {
                     Id = usuarioTb.IdRol,
                     Nombre = usuarioTb.IdRolNavigation.Nombre,
                 },
                 Password = usuarioTb.Password,
                 TipoIdentificacionDTO = new TipoIdentificacionDTO
                 {
                     Id = usuarioTb.IdPersonaNavigation.IdTipoIdentificacionNavigation.Id,
                     Nombre = usuarioTb.IdPersonaNavigation.IdTipoIdentificacionNavigation.Nombre,
                 } 
             };

            return usuarioDto;
         }

        public List<UsuarioDTO> MapeoArrayUsuarioTbAUsuarioDto(IEnumerable<TbSisUsuario> usuariosListTb) {
            return usuariosListTb.Select(u => this.MapeoUsuarioTbAUsuarioDto(u)).ToList();
        }

        public TbSisUsuario MapeoUsuarioCreacionDtoATbUsuario(UsuarioCreacionDTO usuarioCreacionDto) {
            return new TbSisUsuario
            {
                IdPersona = usuarioCreacionDto.IdPersona,
                NombreUsuario = usuarioCreacionDto.NombreUsuario,
                Estado = usuarioCreacionDto.Estado,
              
                IdPersonaNavigation = new TbGrlPersona
                {
                    Apellidos = usuarioCreacionDto.Apellidos,
                    Nombres = usuarioCreacionDto.Nombres,
                    Identificacion = usuarioCreacionDto.Identificacion,
                    FechaCreacion = FechaGrl,
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

        public void MapeoUsuarioEdicionDtoATbUsuario(UsuarioEditarDTO usuarioEditarDTO, TbSisUsuario usuarioTb)
        {
            usuarioTb.NombreUsuario = usuarioEditarDTO.NombreUsuario;
            usuarioTb.Estado = usuarioEditarDTO.Estado;
            usuarioTb.IdPersona = usuarioEditarDTO.IdPersona;

            // ⚡️ actualizar sobre la entidad ya existente
            if (usuarioTb.IdPersonaNavigation != null)
            {
                usuarioTb.IdPersonaNavigation.Apellidos = usuarioEditarDTO.Apellidos;
                usuarioTb.IdPersonaNavigation.Nombres = usuarioEditarDTO.Nombres;
                usuarioTb.IdPersonaNavigation.Identificacion = usuarioEditarDTO.Identificacion;
                usuarioTb.IdPersonaNavigation.Mail = usuarioEditarDTO.Mail;
                usuarioTb.IdPersonaNavigation.FechaModificacion = FechaGrl;
                usuarioTb.IdPersonaNavigation.IdTipoIdentificacion = usuarioEditarDTO.IdTipoIdentificacion;

                // actualizar dirección
                if (usuarioTb.IdPersonaNavigation.TbGrlDireccione != null)
                {
                    usuarioTb.IdPersonaNavigation.TbGrlDireccione.IdCiudad = usuarioEditarDTO.DireccionEdicionDto.IdCiudad;
                    usuarioTb.IdPersonaNavigation.TbGrlDireccione.EstadoVisual = true;
                    usuarioTb.IdPersonaNavigation.TbGrlDireccione.Descripcion = usuarioEditarDTO.DireccionEdicionDto.Descripcion;
                }
            }

            usuarioTb.Password = usuarioEditarDTO.Password;
            usuarioTb.IdRol = usuarioEditarDTO.IdRol;
        }

        public TbVenDetalleVenta MapeoDetalleVentaCreacionDtoADetalleVenta(DetalleVentaCreacionDTO detalleVentaCreacionDto)
        {
            return new TbVenDetalleVenta
            {
                IdArticulo = detalleVentaCreacionDto.ArticuloId,
                Cantidad = detalleVentaCreacionDto.Cantidad,
                Descripcion = detalleVentaCreacionDto.Descripcion,
                ImpuestoValor = detalleVentaCreacionDto.ImpuestoValor,
                ValorCompra = detalleVentaCreacionDto.ValorCompra,
                ValotTotal = detalleVentaCreacionDto.ValorTotal,
                IdVenta = detalleVentaCreacionDto.IdVenta,
            };
        }

        public TbVenta MapeoVentaAVentaCreacion(VentaCreacionDTO ventaCreacionDto)
        {
            throw new NotImplementedException();
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
        public TbCompra MapeoCompraCreacionDtoACompraTb(CompraCreacionDTO compraCreacionDto)
        {
            return new TbCompra
            {
                IdProveedor = compraCreacionDto.IdProveedor,
                Documento = compraCreacionDto.Documento,
                IdEstadoCompra = compraCreacionDto.IdEstado,
                EstadoVisual = true,
                IdUsuarioCreador = compraCreacionDto.IdUsuarioCreador,
                TbComDetallesCompras = compraCreacionDto.DetalleComprasCreacionDto.Select(this.MapeoDetalleCompraCreacionDtoADetalleCompraDto).ToList(),
                FechaCreacion = FechaGrl,
                FechaCompra = compraCreacionDto.FechaCompra,
            };
        }




        public TbComDetallesCompra MapeoDetalleCompraCreacionDtoADetalleCompraDto(DetalleCompraCreacionDTO detalleComraCreacionDto)
        {
            return new TbComDetallesCompra
            {
                IdArticulo = detalleComraCreacionDto.ArticuloId,
                Cantidad = detalleComraCreacionDto.Cantidad,
                Descripcion = detalleComraCreacionDto.Descripcion,
                ImpuestoValor = detalleComraCreacionDto.ImpuestoValor,
                ValorCompra = detalleComraCreacionDto.ValorCompra,
                ValorVenta = detalleComraCreacionDto.ValorVenta,
                ValorTotal = detalleComraCreacionDto.ValorTotal,
            };

        }

        public TbVenta MapeoVentaCreacionDtoAVentaTb(VentaCreacionDTO ventaCreacionDto)
        {
            return new TbVenta
            {
                IdCliente = ventaCreacionDto.IdCliente,
                Documento = ventaCreacionDto.Documento,
                IdEstadoVenta = ventaCreacionDto.IdEstado,
                EstadoVisual = true,
                IdUsuarioCreador = ventaCreacionDto.UsuarioCreadorId,
                TbVenDetalleVenta = ventaCreacionDto.DetalleVentaCreacionDto.Select(this.MapeoDetalleVentaCreacionDtoADetalleVentaDto).ToList(),
                FechaCreacion = FechaGrl,
                FechaVenta = ventaCreacionDto.FechaCompra,
            };
        }

        public TbVenDetalleVenta MapeoDetalleVentaCreacionDtoADetalleVentaDto(DetalleVentaCreacionDTO detalleVentaCreacionDto)
        {
            return new TbVenDetalleVenta
            {
                IdArticulo = detalleVentaCreacionDto.ArticuloId,
                Cantidad = detalleVentaCreacionDto.Cantidad,
                Descripcion = detalleVentaCreacionDto.Descripcion,
                ImpuestoValor = detalleVentaCreacionDto.ImpuestoValor,
                ValorCompra = detalleVentaCreacionDto.ValorCompra,
                ValorVenta = detalleVentaCreacionDto.ValorVenta,
                ValotTotal = detalleVentaCreacionDto.ValorTotal,
            };
        }
        public TbComProveedores MapeoProveedorDtoAProveedorTb(ProveedorCreacionDTO provedorCreacion)
        {
            return new TbComProveedores
            {
               
                Descripcion = provedorCreacion.Descripcion,
                Estado = true,
                EstadoVisual = true,
                IdPersonaNavigation = new TbGrlPersona
                {
                    Apellidos = provedorCreacion.Apellidos,
                    Nombres = provedorCreacion.Nombres,
                    Identificacion = provedorCreacion.Identificacion,
                    FechaCreacion = FechaGrl,
                    Mail = provedorCreacion.Mail,
                    TbGrlDireccione = new TbGrlDirecciones
                    {
                        IdCiudad = provedorCreacion.DireccionCreacionDto.IdCiudad,
                        EstadoVisual = true,
                        Descripcion = provedorCreacion.DireccionCreacionDto.Descripcion,
                    },
                    IdTipoIdentificacion = provedorCreacion.IdIdentificacion,
                },
                RazonSocial = provedorCreacion.RazonSocial,
            };
        }

        public ProveedorDTO MapeoProveedorTbAProveedorDto(TbComProveedores provedorTb)
        {
            return new ProveedorDTO
            {
                Descripcion = provedorTb.Descripcion,
                EstadoVisual = true,
                Nombres = provedorTb.IdPersonaNavigation.Nombres,
                Apellidos = provedorTb.IdPersonaNavigation.Apellidos,
                Identificacion = provedorTb.IdPersonaNavigation.Identificacion,
                Mail = provedorTb.IdPersonaNavigation.Mail,
                Telefono = provedorTb.IdPersonaNavigation.TbGrlDireccione.Descripcion,
                RazonSocial = provedorTb.RazonSocial,
                Direccion = $"Dirección: {provedorTb.IdPersonaNavigation.TbGrlDireccione.Descripcion} Ciudad: {provedorTb.IdPersonaNavigation.TbGrlDireccione.IdCiudadNavigation.Nombre}",
                Id = provedorTb.Id,
            };
        }

        public ClienteDTO MapeoClienteTbAClienteDto(TbComCliente clienteTb)
        {
            return new ClienteDTO
            {
                EstadoVisual = true,
                Nombres = clienteTb.IdPersonaNavigation.Nombres,
                Apellidos = clienteTb.IdPersonaNavigation.Apellidos,
                Identificacion = clienteTb.IdPersonaNavigation.Identificacion,
                Mail = clienteTb.IdPersonaNavigation.Mail,
                Telefono = clienteTb.IdPersonaNavigation.Telefono,
                DireccionDto = new DireccionDTO
                {
                    Descripcion = clienteTb.IdPersonaNavigation.TbGrlDireccione.Descripcion,
                    IdCiudad = clienteTb.IdPersonaNavigation.TbGrlDireccione.IdCiudad,
                    Ciudad = new CiudadDTO
                    {
                        Id = clienteTb.IdPersonaNavigation.TbGrlDireccione.IdCiudadNavigation.Id,
                        Nombre = clienteTb.IdPersonaNavigation.TbGrlDireccione.IdCiudadNavigation.Nombre,
                    },
                },
                Id = clienteTb.Id,
            };
        }

        public void MapeoProveedorEditarDtoAProveedorTb(ProveedorEditarDTO provedorEditar, TbComProveedores proveedoresTb)
        {
            proveedoresTb.Estado = provedorEditar.Estado;
            proveedoresTb.RazonSocial = provedorEditar.RazonSocial;
            proveedoresTb.Descripcion = provedorEditar.Descripcion;
            // ⚡️ actualizar sobre la entidad ya existente
            if (proveedoresTb.IdPersonaNavigation != null)
            {
                proveedoresTb.IdPersonaNavigation.Apellidos = provedorEditar.Apellidos;
                proveedoresTb.IdPersonaNavigation.Nombres = provedorEditar.Nombres;
                proveedoresTb.IdPersonaNavigation.Identificacion = provedorEditar.Identificacion;
                proveedoresTb.IdPersonaNavigation.Mail = provedorEditar.Mail;
                proveedoresTb.IdPersonaNavigation.FechaModificacion = FechaGrl;
                proveedoresTb.IdPersonaNavigation.IdTipoIdentificacion = provedorEditar.IdIdentificacion;
                // actualizar dirección
                if (proveedoresTb.IdPersonaNavigation.TbGrlDireccione != null)
                {
                    proveedoresTb.IdPersonaNavigation.TbGrlDireccione.IdCiudad = provedorEditar.DireccionEdicionDto.IdCiudad;
                    proveedoresTb.IdPersonaNavigation.TbGrlDireccione.EstadoVisual = true;
                    proveedoresTb.IdPersonaNavigation.TbGrlDireccione.Descripcion = provedorEditar.DireccionEdicionDto.Descripcion;
                }
            }
        }

        public void MapeoCompraEdicionDtoACompraTb(CompraEditarDTO compraEditarDto, TbCompra compraTb)
        {
            compraTb.FechaModificacion = FechaGrl;
            compraTb.IdEstadoCompra = compraEditarDto.IdEstado;
            compraTb.Documento = compraEditarDto.Documento;
            compraTb.FechaModificacion = FechaGrl;
            
        }

        public void MapeoVentaEdicionDtoAVentaTb(VentaEditarDTO ventaEditarDto, TbVenta ventaTb)
        {
            ventaTb.FechaModificacion = FechaGrl;
            ventaTb.IdEstadoVenta = ventaEditarDto.IdEstado;
            ventaTb.Documento = ventaEditarDto.Documento;
            ventaTb.FechaModificacion = FechaGrl;

        }

        public List<CompraMinDTO> MapeoListaCompraTbAListaCompraDto(List<TbCompra> listaResultado)
        {
            return listaResultado.Select(this.MapeoCompraTbACompraDto).ToList();
        }

        public CompraMinDTO MapeoCompraTbACompraDto(TbCompra compraTb)
        {
            
            var compra = new CompraMinDTO
            {
                Id = compraTb.Id,
                IdProveedor = compraTb.IdProveedor,
                UsuarioCreadorMinDTO = new UsuarioMinDTO
                {
                    Id = compraTb.IdUsuarioCreadorNavigation.Id,
                    Apellidos = compraTb.IdUsuarioCreadorNavigation.IdPersonaNavigation.Apellidos,
                    Nombres = compraTb.IdUsuarioCreadorNavigation.IdPersonaNavigation.Nombres,
                },
                ProveedorMinDto = new ProveedorMinDTO
                {
                    Id = compraTb.IdProveedorNavigation.Id,
                    RazonSocial = compraTb.IdProveedorNavigation.RazonSocial,
                    Descripcion = compraTb.IdProveedorNavigation.Descripcion,
                    Identificacion = compraTb.IdProveedorNavigation.IdPersonaNavigation.Identificacion,
                    Mail = compraTb.IdProveedorNavigation.IdPersonaNavigation.Mail,
                },
                Documento = compraTb.Documento,
                FechaCompra = compraTb.FechaCompra,
                IdEstado = compraTb.IdEstadoCompra,
                EstadoCompra = new EstadoCompraDTO
                {
                    Id = compraTb.IdEstadoCompraNavigation.Id,
                    Nombre = compraTb.IdEstadoCompraNavigation.Nombre,
                },

            };

            return compra;
        }

        public CompraDTO MapeoCompraTbACompraCompletaDto(TbCompra compraTb)
        {

            var compra = new CompraDTO
            {
                Id = compraTb.Id,
                IdProveedor = compraTb.IdProveedor,
                UsuarioCreador = new UsuarioDTO
                {
                    Id = compraTb.IdUsuarioCreadorNavigation.Id,
                    Apellidos = compraTb.IdUsuarioCreadorNavigation.IdPersonaNavigation.Apellidos,
                    Nombres = compraTb.IdUsuarioCreadorNavigation.IdPersonaNavigation.Nombres,
                    Identificacion = compraTb.IdUsuarioCreadorNavigation.IdPersonaNavigation.Identificacion,
                    Mail = compraTb.IdUsuarioCreadorNavigation.IdPersonaNavigation.Mail,
                },
                ProveedorDto = new ProveedorDTO
                {
                    Id = compraTb.IdProveedorNavigation.Id,
                    RazonSocial = compraTb.IdProveedorNavigation.RazonSocial,
                    Descripcion = compraTb.IdProveedorNavigation.Descripcion,
                    Identificacion = compraTb.IdProveedorNavigation.IdPersonaNavigation.Identificacion,
                    Direccion = compraTb.IdProveedorNavigation.IdPersonaNavigation.TbGrlDireccione.Descripcion,
                },
                Documento = compraTb.Documento,
                FechaCompra = compraTb.FechaCompra,
                FechaCreacion = compraTb.FechaCreacion,
                FechaModificacion = compraTb.FechaModificacion,
                IdEstado = compraTb.IdEstadoCompra,
                EstadoCompra = new EstadoCompraDTO
                {
                    Id = compraTb.IdEstadoCompraNavigation.Id,
                    Nombre = compraTb.IdEstadoCompraNavigation.Nombre,
                },
                DetalleCompras = compraTb.TbComDetallesCompras.Select(d => new DetalleCompraDTO
                {
                    Id = d.Id,
                    Articulo = new ArticuloDTO
                    {
                        Id = d.IdArticuloNavigation.Id,
                        Codigo = d.IdArticuloNavigation.Codigo,
                        Descripcion = d.IdArticuloNavigation.Descripcion,
                        Nombre = d.IdArticuloNavigation.Nombre,
                        Unidad = d.IdArticuloNavigation.Unidad,
                        UnidadValor = d.IdArticuloNavigation.UnidadValor,
                        FechaActualizacion = d.IdArticuloNavigation.FechaActualizacion,
                        FechaCreacion = d.IdArticuloNavigation.FechaCreacion,
                        FechaCaducidad = d.IdArticuloNavigation.FechaCaducidad,
                        ImpuestoArticuloDto = new ImpuestoArticuloDTO
                        {
                            Id = d.IdArticuloNavigation.IdImpuestoNavigation.Id,
                            Nombre = d.IdArticuloNavigation.IdImpuestoNavigation.Nombre,
                            ValorImpuesto = d.IdArticuloNavigation.IdImpuestoNavigation.ValorImpuesto,
                        },
                        TipoArticuloDTO = new TipoArticuloDTO
                        {
                            Id = d.IdArticuloNavigation.IdTipoArticuloNavigation.Id,
                            Nombre = d.IdArticuloNavigation.IdTipoArticuloNavigation.Nombre,
                            Descripcion = d.IdArticuloNavigation.IdTipoArticuloNavigation.Descripcion,
                        },
                        MarcaDTO = new MarcaDTO
                        {
                            Id = d.IdArticuloNavigation.IdMarcaNavigation.Id,
                            Nombre = d.IdArticuloNavigation.IdMarcaNavigation.Nombre,
                            Descripcion = d.IdArticuloNavigation.IdMarcaNavigation.Descripcion,
                        }
                    },
                    Cantidad = d.Cantidad,
                    Descripcion = d.Descripcion,
                    ImpuestoValor = d.ImpuestoValor,
                    ValorCompra = d.ValorCompra,
                    ValorVenta = d.ValorVenta,
                    ValorTotal = d.ValorTotal,
                }).ToList(),

            };
            return compra;
        }

        public PermisosRolDTO MapeoTbSisPermisorRolAPermisosRolDTO(TbSisPermisosRol permisosRolTb)
        {
            return new PermisosRolDTO
            {
                Id = permisosRolTb.Id,
                IdMenu = permisosRolTb.IdMenu,
                IdRol = permisosRolTb.IdRol,
                Menu = new MenuDTO
                {
                    Id = permisosRolTb.IdMenuNavigation.Id,
                    Nombre = permisosRolTb.IdMenuNavigation.Nombre,
                },
            };
        }
        public List<PermisosRolDTO> MapeoListaTbSisPermisosRolAPermisosRolDTO(IEnumerable<TbSisPermisosRol> listaPermisosRolTb)
        {
            return listaPermisosRolTb.Select(this.MapeoTbSisPermisorRolAPermisosRolDTO).ToList();
        }
    }
}
