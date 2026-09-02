using SistemaTienda.DTO;
using SistemaTienda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.Utility
{
    public interface IMapeoPersona
    {
        PersonaCompletoDTO MapeoPersonaCompletoDto(TbGrlPersona persona, TbComCliente? cliente, TbComProveedores? proveedor, TbSisUsuario? usuario);
    }

    public class MapeoPersona : IMapeoPersona
    {
        public PersonaCompletoDTO MapeoPersonaCompletoDto(
            TbGrlPersona persona,
            TbComCliente? cliente,
            TbComProveedores? proveedor,
            TbSisUsuario? usuario)
        {
            bool esCLiente = cliente != null;
            bool esUsuario = usuario != null;
            bool esProveedor = proveedor != null;

            return new PersonaCompletoDTO
            {
                Id = persona.Id,

                Apellidos = persona.Apellidos,
                Nombres = persona.Nombres,
                Telefono = persona.Telefono,

                TipoIdentificacionDTO = persona.IdTipoIdentificacionNavigation == null
                    ? null
                    : new TipoIdentificacionDTO
                    {
                        Id = persona.IdTipoIdentificacionNavigation.Id,
                        Nombre = persona.IdTipoIdentificacionNavigation.Nombre
                    },

                IdTipoIdentificacion = persona.IdTipoIdentificacion,
                Identificacion = persona.Identificacion,

                DireccionesDTO = persona.TbGrlDireccione == null
                    ? null
                    : new DireccionDTO
                    {
                        Id = persona.TbGrlDireccione.Id,
                        Descripcion = persona.TbGrlDireccione.Descripcion,

                        Ciudad = persona.TbGrlDireccione.IdCiudadNavigation == null
                            ? null
                            : new CiudadDTO
                            {
                                Id = persona.TbGrlDireccione.IdCiudadNavigation.Id,
                                Nombre = persona.TbGrlDireccione.IdCiudadNavigation.Nombre
                            }
                    },

                FechaCreacion = persona.FechaCreacion,
                FechaModificacion = persona.FechaModificacion,
                Mail = persona.Mail,

                // =========================
                // CLIENTE
                // =========================
                IdCliente = cliente?.Id ?? 0,
                EstadoVisualCliente = cliente?.EstadoVisual ?? false,
                EstadoCliente = cliente?.Estado ?? false,

                // =========================
                // PROVEEDOR
                // =========================
                IdProveedor = proveedor?.Id ?? 0,
                EstadoProveedor = proveedor?.Estado ?? false,
                EstadoVisualProveedor = proveedor?.EstadoVisual ?? false,

                RazonSocial = proveedor?.RazonSocial ?? string.Empty,
                Descripcion = proveedor?.Descripcion ?? string.Empty,

                // =========================
                // USUARIO
                // =========================
                IdUsuario = proveedor?.Id ?? 0,
                Rol = usuario?.IdRolNavigation == null
                    ? null
                    : new RolDTO
                    {
                        Id = usuario.IdRolNavigation.Id,
                        Nombre = usuario.IdRolNavigation.Nombre
                    },

                NombreUsuario = usuario?.NombreUsuario ?? string.Empty,
                Password = usuario?.Password ?? string.Empty,
                EstadoUsuario = usuario?.Estado ?? false,
                EsCliente = esCLiente,
                EsProveedor = esProveedor,
                EsUsuario = esUsuario
            };
        }
    }
}
