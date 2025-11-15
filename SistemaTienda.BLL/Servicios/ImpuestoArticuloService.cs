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
    public class ImpuestoArticuloService : IImpuestoArticuloService
    {
        private readonly IMapeos _mapeos;
        private readonly TiendaDbContext _tiendaDbContext;
        private readonly IGenericRepository<TbComImpuestosArticulo> _impuestosArticulosRepsitory;
        private readonly IGenericRepository<TbComEstadosImpuesto> _estadosImpuestosRepository;
        public ImpuestoArticuloService(IMapeos mapeos, TiendaDbContext tiendaDbContext, IGenericRepository<TbComImpuestosArticulo> impuestosArticulosRepsitory, IGenericRepository<TbComEstadosImpuesto> estadosImpuestosRepository)
        {
            this._mapeos = mapeos;
            this._tiendaDbContext = tiendaDbContext;
            this._impuestosArticulosRepsitory = impuestosArticulosRepsitory;
            _estadosImpuestosRepository = estadosImpuestosRepository;
        }

        public Task<int> CrearImpuestos(ImpuestoArticuloCreacionDTO impuestoArticuloCreacionDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarImpuesto(ImpuestoArticuloDTO impuestoArticuloEditarDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EstadoImpuestoDTO>> ListarEstados()
        {
            try
            {
                IQueryable<TbComEstadosImpuesto> estadosList = await this._estadosImpuestosRepository.Consultar();
                var estadosListCon = estadosList.ToList();
                var listaEstadosImpuestosDto = this._mapeos.MapeoListaEstadosImpuestosTbAListaEstadosImpuestosDto(estadosListCon);
                return listaEstadosImpuestosDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ImpuestoArticuloDTO>> ListarImpuestos()
        {
            try
            {
                IQueryable<TbComImpuestosArticulo> impuestosList = await this._impuestosArticulosRepsitory.Consultar();
                var impuestosListConIncludes = impuestosList
                    .Include(est => est.IdEstadoImpuestoNavigation).ToList();
                var listaImpuestosDto = this._mapeos.MapeoListaImpuestosTbAListaImpuestosDto(impuestosListConIncludes);
                return listaImpuestosDto;
            }
            catch
            {
                throw;
            }
        }
    }
}
