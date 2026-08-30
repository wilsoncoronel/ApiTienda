using Microsoft.EntityFrameworkCore;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios
{
    public class PersonaService : IPersonaService
    {
        private readonly TiendaDbContext tiendaDbContext;

        public PersonaService(TiendaDbContext tiendaDbContext)
        {
            this.tiendaDbContext = tiendaDbContext;
        }
        public Task<int> ConvertirCliente(int IdPersona)
        {
            throw new NotImplementedException();
        }

        public Task<int> ConvertirProveedor(int IdPersona)
        {
            throw new NotImplementedException();
        }

        public Task<int> ConvertirUsuario(int IdPersona)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PersonaDTO>> ListaPersonas()
        {
            var personas = await tiendaDbContext.TbGrlPersonas
                .Include(i => i.IdTipoIdentificacionNavigation)
                .Include(dir => dir.TbGrlDireccione)
            .Select(p => new PersonaDTO
            {
                Id = p.Id,
                Identificacion = p.Identificacion,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                Mail = p.Mail,
                TipoIdentificacionDTO = new TipoIdentificacionDTO
                {
                    Id = p.IdTipoIdentificacionNavigation.Id,
                    Nombre = p.IdTipoIdentificacionNavigation.Nombre
                },
                DireccionesDTO = new DireccionDTO
                {
                    Id = p.TbGrlDireccione.Id,
                    Descripcion = p.TbGrlDireccione.Descripcion,
                    Ciudad = new CiudadDTO
                    {
                        Id = p.TbGrlDireccione.IdCiudadNavigation.Id,
                        Nombre = p.TbGrlDireccione.IdCiudadNavigation.Nombre
                    }
                },
                Telefono = p.Telefono,
                FechaCreacion = p.FechaCreacion,
                FechaModificacion = p.FechaModificacion?? DateTime.Now,
                EsUsuario = p.TbSisUsuarios.Any(),
                EsCliente = p.TbComClientes.Any(),
                EsProveedor = p.TbComProveedores.Any()
            })
            .ToListAsync();

            return personas;
        }
    }
}
