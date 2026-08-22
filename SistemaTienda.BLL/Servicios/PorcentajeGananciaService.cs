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
    public class PorcentajeGananciaService : IPorcentajeGananciaService
    {
        private readonly IGenericRepository<TbComPorcentajeGanancia> porcentajeRepository;
        private readonly IMapeos mapeos;
        private readonly TiendaDbContext _tiendaDb;
        public PorcentajeGananciaService(IGenericRepository<TbComPorcentajeGanancia> porcentajeRepository, IMapeos mapeos, TiendaDbContext _tiendaDb)
        {
            this.porcentajeRepository = porcentajeRepository;
            this.mapeos = mapeos;
            this._tiendaDb = _tiendaDb;
        }
        public async Task<int> CrearPorcentaje(PorcentajeGananciaCreacionDTO porcentajeGananciaCreacionDto)
        {
            var porcentaje = this.mapeos.MapeoPorcentajeCreacionDtoAPorcentajeTb(porcentajeGananciaCreacionDto);
            await this.porcentajeRepository.Crear(porcentaje);
            if (porcentaje.Id == 0)
                throw new BadRequestException("No se pudo crear el porcentaje de ganancia");
            return porcentaje.Id;
        }

        public async Task<bool> EditarPorcentaje(PorcentajeGananciaDTO porcentajeGananciaEdicionDto)
        {
            var porcentajeGanancia = await this._tiendaDb.TbComPorcentajeGanancia.Where(p => p.Id == porcentajeGananciaEdicionDto.Id)
                .FirstOrDefaultAsync();
            if (porcentajeGanancia is null)
                throw new NotFoundException("No se encontró la marca a editar");
            porcentajeGanancia.EstadoVisual = porcentajeGananciaEdicionDto.EstadoVisual;
            porcentajeGanancia.Valor = porcentajeGanancia.Valor;
            porcentajeGanancia.PorcentajeGanancia = porcentajeGanancia.PorcentajeGanancia;
            var resp = await this.porcentajeRepository.Editar(porcentajeGanancia);
            if (resp == false)
                throw new BadRequestException("No se pudo editar el porcentaje!!");
            return resp;
        }

        public async Task<List<PorcentajeGananciaDTO>> ListarPorcentaje()
        {
            var porcentajesList = await this._tiendaDb.TbComPorcentajeGanancia.ToListAsync();
            return this.mapeos.MapeoListasPorcentajeTbAListaPorcentajeDto(porcentajesList);
        }
    }
}
