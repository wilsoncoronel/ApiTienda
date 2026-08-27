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
    public class MarcaService : IMarcaService
    {
        private readonly IGenericRepository<TbComMarca> marcaRepository;
        private readonly IMapeos mapeos;
        private readonly TiendaDbContext _tiendaDb;

        public MarcaService(IGenericRepository<TbComMarca> marcaRepository, IMapeos mapeos, TiendaDbContext tiendaDb)
        {
            this.marcaRepository = marcaRepository;
            this.mapeos = mapeos;
            this._tiendaDb = tiendaDb;
        }
        public async Task<int> CrearMarca(MarcaCreacionDTO marcaCreacionDto)
        {   
            var marca = this.mapeos.MapeoMarcaCreacionDtoAMarcaTb(marcaCreacionDto);
            await this.marcaRepository.Crear(marca);
            if (marca.Id == 0)
                throw new BadRequestException("No se pudo crear la marca");
            return marca.Id;
        }

        public async Task<bool> EditarMarca(MarcaEditarDTO marcaEditarDto)
        {
            var marca = await this._tiendaDb.TbComMarcas.Where(c => c.Id == marcaEditarDto.Id)
                .FirstOrDefaultAsync();
            if (marca is null)
                throw new Exception("No se encontró la marca a editar");
            marca.EstadoVisual = marcaEditarDto.EstadoVisual;
            marca.Nombre = marcaEditarDto.Nombre;
            marca.Descripcion = marcaEditarDto.Descripcion;
            var resp = await this.marcaRepository.Editar(marca);
            if (resp == false)
                throw new BadRequestException("No se pudo editar la marca");
            return resp;
        }

        public async Task<List<MarcaDTO>> ListarMarcas()
        {
            var marcasList = await this._tiendaDb.TbComMarcas.Where(mar => mar.EstadoVisual == true ).ToListAsync();
            return this.mapeos.MapeoListasMarcaTbAListaMarcaDto(marcasList);
        }
    }
}
