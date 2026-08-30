using Microsoft.EntityFrameworkCore;
using SistemaTienda.API.Exceptions;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.Model;
using SistemaTienda.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios
{
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly IGenericRepository<TbComUnidadesMedida> unidadRepository;
        private readonly IMapeos mapeos;
        private readonly TiendaDbContext tiendaDb;

        public UnidadMedidaService(IGenericRepository<TbComUnidadesMedida> unidadRepository, IMapeos mapeos, TiendaDbContext tiendaDb)
        {
            this.unidadRepository = unidadRepository;
            this.mapeos = mapeos;
            this.tiendaDb = tiendaDb;
        }
        public async Task<int> CrearUnidadMedida(UnidadCreacionDTO unidadCreacionDto)
        {
            var unidad = this.mapeos.MapeoUnidadCreacionDtoAUnidadTb(unidadCreacionDto);
            await this.unidadRepository.Crear(unidad);
            if (unidad.Id == 0)
                throw new BadRequestException("No se pudo crear la unidad!!");
            return unidad.Id;
        }

        public async Task<bool> EditarUnidadMedida(UnidadEditarDTO unidadEditarDto)
        {
            var unidad = await this.tiendaDb.TbComUnidadesMedida.Where(u => u.Id == unidadEditarDto.Id).FirstOrDefaultAsync();
            if (unidad is null)
                throw new NotFoundException("No se encontró la unidad a editar");
            unidad.EstadoVisual = unidadEditarDto.EstadoVisual;
            unidad.Nombre = unidadEditarDto.Nombre;
            unidad.Estado = unidadEditarDto.Estado;
            var resp = await this.unidadRepository.Editar(unidad);
            if (resp == false)
                throw new BadRequestException("No se pudo editar la unidad!!");
            return resp;
        }

        public async Task<List<UnidadMedidaDTO>> ListarUnidadesMedida()
        {
            var unidadList = await this.tiendaDb.TbComUnidadesMedida.Where(mar => mar.EstadoVisual == true).ToListAsync();
            return this.mapeos.MapeoListaUnidadTbAListaUnidadDto(unidadList);
        }
    }
}
