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
            var tiposArticulo = this.mapeos.MapeoTipoArticuloDtoATipoArticuloTb(tipoArticuloCreacionDto);
            await this.tipoArticuloRepository.Crear(tiposArticulo);
            if (tiposArticulo.Id == 0)
                throw new BadRequestException("No se pudo crear el tipo artículo!!");
            return tiposArticulo.Id;
        }

        public async Task<bool> EditarTipoArticulo(TipoArticuloEditarDTO tipoArticuloEditarDto)
        {
            var tipoArticuloTb = await this._tiendaDb.TbComTiposArticulos.Where(c => c.Id == tipoArticuloEditarDto.Id)
                .FirstOrDefaultAsync();
            if (tipoArticuloTb is null)
                throw new NotFoundException("No se encontró el tipo artículo a editar");
            tipoArticuloTb.EstadoVisual = tipoArticuloEditarDto.EstadoVisual;
            tipoArticuloTb.Nombre = tipoArticuloEditarDto.Nombre;
            tipoArticuloTb.Descripcion = tipoArticuloEditarDto.Descripcion;
            var resp = await this.tipoArticuloRepository.Editar(tipoArticuloTb);
            if (resp == false)
                throw new BadRequestException("No se pudo editar el tipo artículo!!");
            return resp;
        }

        public async Task<List<TipoArticuloDTO>> ListarTiposArticulos()
        {
            var tiposList = await this._tiendaDb.TbComTiposArticulos.Where(tipArt=> tipArt.EstadoVisual == true).ToListAsync();
            return this.mapeos.MapeoListasTipoArticulosTbAListaTipoArticulosDto(tiposList);
        }
    }
}
