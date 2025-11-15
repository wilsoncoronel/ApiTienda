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

        public MarcaService(IGenericRepository<TbComMarca> marcaRepository, IMapeos mapeos)
        {
            this.marcaRepository = marcaRepository;
            this.mapeos = mapeos;
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
            IQueryable<TbComMarca> marcaTb = await this.marcaRepository.Consultar();
            
                try
                {
                    var marca = marcaTb.Where(c => c.Id == marcaEditarDto.Id)
                        .FirstOrDefault();
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
                IQueryable<TbComMarca> marcasList = await this.marcaRepository.Consultar();
                var listaMarcas = marcasList.ToList();
                var listaMarcasDto = this.mapeos.MapeoListasMarcaTbAListaMarcaDto(listaMarcas);
                return listaMarcasDto;
            }
            catch
            {
                throw;
            }
        }
    }
}
