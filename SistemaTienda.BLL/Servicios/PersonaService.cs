using Microsoft.EntityFrameworkCore;
using SistemaTienda.API.Exceptions;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DTO;
using SistemaTienda.Utility;
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
        private readonly IMapeoPersona mapeoPersona;

        public PersonaService(TiendaDbContext tiendaDbContext, IMapeoPersona mapeoPersona)
        {
            this.tiendaDbContext = tiendaDbContext;
            this.mapeoPersona = mapeoPersona;
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

        public async Task<PersonaCompletoDTO> PersonaCompleta(int idPersona)
        {
            var persona = await tiendaDbContext.TbGrlPersonas.Where(per => per.Id == idPersona)
                .Include(i => i.IdTipoIdentificacionNavigation)
                .Include(dir => dir.TbGrlDireccione).FirstOrDefaultAsync();
            if (persona is null)
                throw new NotFoundException("No existe la persona!!");
            var cli = await tiendaDbContext.TbComClientes.Where(cli => cli.IdPersona == persona.Id).FirstOrDefaultAsync();
            var usu = await tiendaDbContext.TbSisUsuarios.Where(usu => usu.IdPersona == persona.Id).FirstOrDefaultAsync();
            var prov = await tiendaDbContext.TbComProveedores.Where(prov => prov.IdPersona == persona.Id).FirstOrDefaultAsync();

            return mapeoPersona.MapeoPersonaCompletoDto(persona, cli, prov, usu);
        }
    }
}
