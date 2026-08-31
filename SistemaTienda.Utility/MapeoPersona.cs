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
        PersonaCompletoDTO MapeoPersonaCompletoDto(TbGrlPersona perosna, TbComCliente cliente, TbComProveedores proveedor, TbSisUsuario usuario);
    }

    public class MapeoPersona : IMapeoPersona
    {
        public PersonaCompletoDTO MapeoPersonaCompletoDto(TbGrlPersona perosna, TbComCliente cliente, TbComProveedores proveedor, TbSisUsuario usuario)
        {
            return new PersonaCompletoDTO
            {
                Id = perosna.Id,
                Apellidos = perosna.Nombres,
                Nombres = perosna.Nombres,
                Telefono = perosna.Telefono,
                TipoIdentificacionDTO = new TipoIdentificacionDTO
                {
                    Id = perosna.IdTipoIdentificacionNavigation.Id,
                    Nombre = perosna.IdTipoIdentificacionNavigation.Nombre
                },
                IdTipoIdentificacion = perosna.IdTipoIdentificacion,
                Identificacion = perosna.Identificacion,
                DireccionesDTO = new DireccionDTO
                {
                    Id = perosna.TbGrlDireccione.Id,
                    Descripcion = perosna.TbGrlDireccione.Descripcion,
                    Ciudad = new CiudadDTO
                    {
                        Id = perosna.TbGrlDireccione.IdCiudadNavigation.Id,
                        Nombre = perosna.TbGrlDireccione.IdCiudadNavigation.Nombre
                    }
                },
                FechaCreacion = perosna.FechaCreacion,
                FechaModificacion = perosna.FechaModificacion,
                Mail = perosna.Mail,
                //Cliente
                EstadoVisualCliente = cliente.EstadoVisual,
                EstadoCliente = cliente.Estado,
                //Proveedor
                EstadoProveedor = proveedor.Estado,
                EstadoVisualProveedor = proveedor.EstadoVisual,
                RazonSocial = proveedor.RazonSocial,
                Descripcion = proveedor.Descripcion,
                //Usuario
                Rol = new RolDTO
                {
                    Id = usuario.IdRolNavigation.Id,
                    Nombre = usuario.IdRolNavigation.Nombre
                },
                NombreUsuario = usuario.NombreUsuario,
                Password = usuario.Password,
                EstadoUsuario = usuario.Estado,
            };

        }
    }
}
