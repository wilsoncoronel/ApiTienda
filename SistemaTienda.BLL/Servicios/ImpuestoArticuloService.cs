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

        public async Task<int> CrearImpuestos(ImpuestoArticuloCreacionDTO impuestoArticuloCreacionDto)
        {
            using (var transaccion = _tiendaDbContext.Database.BeginTransaction())
            {
                try
                {
                    var impuesto = this._mapeos.MapeoImpuestoDtoAImpuestoTb(impuestoArticuloCreacionDto);
                    await this._impuestosArticulosRepsitory.Crear(impuesto);
                    if (impuesto.Id == 0)
                        throw new Exception("No se pudo crear el impuesto!!");
                    transaccion.Commit();
                    return impuesto.Id;
                }
                catch
                {
                    transaccion.Rollback();
                    throw new Exception("Error ha ocurrido un error creando el impuesto, comuníquese con el administrador del sistema!!!");
                }
            }
        }

        public async Task<bool> EditarImpuesto(ImpuestoArticuloEditarDTO impuestoArticuloEditarDto)
        {
            IQueryable<TbComImpuestosArticulo> impuestosTb = await this._impuestosArticulosRepsitory.Consultar();

            try
            {
                var imp = impuestosTb.Where(c => c.Id == impuestoArticuloEditarDto.Id)
                    .FirstOrDefault();
                if (imp is null)
                    throw new Exception("No se encontró el impuesto a editar!!");
                imp.IdEstadoImpuesto = impuestoArticuloEditarDto.IdEstadoImpuesto;
                imp.Nombre = impuestoArticuloEditarDto.Nombre;
                imp.Descripcion = impuestoArticuloEditarDto.Descripcion;
                imp.ValorImpuesto = impuestoArticuloEditarDto.ValorImpuesto;
                var resp = await this._impuestosArticulosRepsitory.Editar(imp);
                if (resp == false)
                    throw new Exception("No se pudo editar el impuesto!!");
                return resp;
            }
            catch
            {

                throw new Exception("Error ha ocurrido un error editando el impuesto, comuníquese con el administrador del sistema!!!");
            }
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
