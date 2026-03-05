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
    public class TipoArticuloService: ITipoArticuloService
    {
        private readonly IGenericRepository<TbComTiposArticulo> tipoArticuloRepository;
        private readonly IMapeos mapeos;
        private readonly TiendaDbContext _tiendaDb;
        public TipoArticuloService(IGenericRepository<TbComTiposArticulo> tipoArticuloRepository, IMapeos mapeos, TiendaDbContext tiendaDb)
        {
            this.tipoArticuloRepository = tipoArticuloRepository;
            this.mapeos = mapeos;
            this._tiendaDb = tiendaDb;
        }

        public async Task<int> CrearTipoArticulo(TipoArticuloCreacionDTO tipoArticuloCreacionDto)
        {
            try
            {
                var tiposArticulo = this.mapeos.MapeoTipoArticuloDtoATipoArticuloTb(tipoArticuloCreacionDto);
                await this.tipoArticuloRepository.Crear(tiposArticulo);
                if (tiposArticulo.Id == 0)
                    throw new Exception("No se pudo crear el tipo artículo!!");
                return tiposArticulo.Id;
            }
            catch
            {
                throw new Exception("Un error ha ocurrido un error creando la marca, comuníquese con el administrador del sistema!!!");
            }
        }

        public async Task<bool> EditarTipoArticulo(TipoArticuloEditarDTO tipoArticuloEditarDto)
        {
            try
            {
                var tipoArticuloTb = await this._tiendaDb.TbComTiposArticulos.Where(c => c.Id == tipoArticuloEditarDto.Id)
                    .FirstOrDefaultAsync();
                if (tipoArticuloTb is null)
                    throw new Exception("No se encontró el tipo artículo a editar");
                tipoArticuloTb.EstadoVisual = tipoArticuloEditarDto.EstadoVisual;
                tipoArticuloTb.Nombre = tipoArticuloEditarDto.Nombre;
                tipoArticuloTb.Descripcion = tipoArticuloEditarDto.Descripcion;
                var resp = await this.tipoArticuloRepository.Editar(tipoArticuloTb);
                if (resp == false)
                    throw new Exception("No se pudo editar el tipo artículo!!");
                return resp;
            }
            catch
            {
                throw new Exception("Ha ocurrido un error editando el tipo artículo, comuníquese con el administrador del sistema!!!");
            }

        }

        public async Task<List<TipoArticuloDTO>> ListarTiposArticulos()
        {
            try
            {
                var tiposList = await this._tiendaDb.TbComTiposArticulos.Where(tipArt=> tipArt.EstadoVisual == true).ToListAsync();
                return this.mapeos.MapeoListasTipoArticulosTbAListaTipoArticulosDto(tiposList);
            }
            catch
            {
                throw;
            }
        }
    }
}
