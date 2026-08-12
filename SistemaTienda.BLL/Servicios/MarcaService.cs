using Microsoft.EntityFrameworkCore;
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
            try
            {
                var marca = this.mapeos.MapeoMarcaCreacionDtoAMarcaTb(marcaCreacionDto);
                await this.marcaRepository.Crear(marca);
                if (marca.Id == 0)
                    throw new Exception("No se pudo crear la marca");
                return marca.Id;
            }
            catch
            {
                throw new Exception("Un error ha ocurrido un error creando la marca, comuníquese con el administrador del sistema!!!");
            }
        }

        public async Task<bool> EditarMarca(MarcaEditarDTO marcaEditarDto)
        {
            try
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
                    throw new Exception("No se pudo editar la marca");
                return resp;
            }
            catch
            {
                   
                throw new Exception("Error ha ocurrido un error editando la marca, comuníquese con el administrador del sistema!!!");
            }
        }

        public async Task<List<MarcaDTO>> ListarMarcas()
        {
            try
            {
                var marcasList = await this._tiendaDb.TbComMarcas.Where(mar => mar.EstadoVisual == true ).ToListAsync();
                return this.mapeos.MapeoListasMarcaTbAListaMarcaDto(marcasList);
            }
            catch
            {
                throw;
            }
        }
    }
}
