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
    public class ImpuestoArticuloService : IImpuestoArticuloService
    {
        private readonly IMapeos _mapeos;
        private readonly TiendaDbContext _tiendaDbContext;
        private readonly IGenericRepository<TbComImpuesto> _impuestosRepository;
        private readonly IGenericRepository<TbComEstadosImpuesto> _estadosImpuestosRepository;
        public ImpuestoArticuloService(IMapeos mapeos, TiendaDbContext tiendaDbContext, IGenericRepository<TbComImpuesto> impuestosRepository, IGenericRepository<TbComEstadosImpuesto> estadosImpuestosRepository)
        {
            this._mapeos = mapeos;
            this._tiendaDbContext = tiendaDbContext;
            this._impuestosRepository = impuestosRepository;
            _estadosImpuestosRepository = estadosImpuestosRepository;
        }

        public async Task<int> CrearImpuestos(ImpuestoCrearDTO impuestoArticuloCreacionDto)
        {
            using var transaccion = await _tiendaDbContext.Database.BeginTransactionAsync();
            try
            {
                var impuesto = this._mapeos.MapeoImpuestoDtoAImpuestoTb(impuestoArticuloCreacionDto);
                await this._impuestosRepository.Crear(impuesto);
                if (impuesto.Id == 0)
                    throw new BadRequestException("No se pudo crear el impuesto!!");
                transaccion.Commit();
                return impuesto.Id;
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public async Task<bool> EditarImpuesto(ImpuestoDTO impuestoArticuloEditarDto)
        {
            var imp = await this._tiendaDbContext.TbComImpuestos.Where(c => c.Id == impuestoArticuloEditarDto.Id)
                .FirstOrDefaultAsync();
            if (imp is null)
                throw new NotFoundException("No se encontró el impuesto a editar!!");
            imp.Nombre = impuestoArticuloEditarDto.Nombre;
            imp.TipoCalculo = impuestoArticuloEditarDto.TipoCalculo;
            imp.Valor = impuestoArticuloEditarDto.Valor;
            imp.Estado = impuestoArticuloEditarDto.Estado;
            var resp = await this._impuestosRepository.Editar(imp);
            if (resp == false)
                throw new BadRequestException("No se pudo editar el impuesto!!");
            return resp;
        }

        public async Task<List<EstadoImpuestoDTO>> ListarEstados()
        {
            var estadosList = await this._tiendaDbContext.TbComEstadosImpuestos.Where(estimp => estimp.EstadoVisual == true).ToListAsync();
            return this._mapeos.MapeoListaEstadosImpuestosTbAListaEstadosImpuestosDto(estadosList);
        }

        public async Task<List<ImpuestoDTO>> ListarImpuestos()
        {
            var impuestosList = await this._tiendaDbContext.TbComImpuestos.ToListAsync();
            return this._mapeos.MapeoListaImpuestosTbAListaImpuestosDto(impuestosList);
        }
    }
}
