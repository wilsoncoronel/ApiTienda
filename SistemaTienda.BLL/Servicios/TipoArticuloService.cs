using SistemaTienda.BLL.Servicios.Contrato;
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
        public TipoArticuloService(IGenericRepository<TbComTiposArticulo> tipoArticuloRepository, IMapeos mapeos)
        {
            this.tipoArticuloRepository = tipoArticuloRepository;
            this.mapeos = mapeos;
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
            IQueryable<TbComTiposArticulo> tipoArticuloTb = await this.tipoArticuloRepository.Consultar();

            try
            {
                var tipoArticulo = tipoArticuloTb.Where(c => c.Id == tipoArticuloEditarDto.Id)
                    .FirstOrDefault();
                if (tipoArticulo is null)
                    throw new Exception("No se encontró el tipo artículo a editar");
                tipoArticulo.EstadoVisual = tipoArticuloEditarDto.EstadoVisual;
                tipoArticulo.Nombre = tipoArticuloEditarDto.Nombre;
                tipoArticulo.Descripcion = tipoArticuloEditarDto.Descripcion;
                var resp = await this.tipoArticuloRepository.Editar(tipoArticulo);
                if (resp == false)
                    throw new Exception("No se pudo editar el tipo artículo!!");
                return resp;
            }
            catch
            {

                throw new Exception("Error ha ocurrido un error editando el tipo artículo, comuníquese con el administrador del sistema!!!");
            }

        }

        public async Task<List<TipoArticuloDTO>> ListarTiposArticulos()
        {
            try
            {
                IQueryable<TbComTiposArticulo> tiposList = await this.tipoArticuloRepository.Consultar();
                var listaTiposArticulos = tiposList.ToList();
                var listaTiposArticulosDto = this.mapeos.MapeoListasTipoArticulosTbAListaTipoArticulosDto(listaTiposArticulos);
                return listaTiposArticulosDto;
            }
            catch
            {
                throw;
            }
        }
    }
}
