using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService _inventarioService;
        private readonly ILogger<InventarioController> logger;

        public InventarioController(IInventarioService inventarioService, ILogger<InventarioController> logger)
        {
            this._inventarioService = inventarioService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("ExistenciasInventario")]
        public async Task<IActionResult> ExistenciasInventario()
        {
            var resp = new Response<List<ExistenciaDTO>>();
            try
            {
                resp.status = true;
                
                resp.Value = await this._inventarioService.ExistenciasInventario();
            }
            catch(Exception ex)
            {
                resp.status = false;
                this.logger.LogError("Error en la conexion "+ ex);
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListaTransaccionesInventario")]
        public async Task<IActionResult> ListaTransaccionesInventario()
        {
            var resp = new Response<List<TransaccionInventarioDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._inventarioService.ListaTransaccionesInventario();
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListaInventario")]
        public async Task<IActionResult> ListaInventario(DateOnly fechaInicio, DateOnly fechaFin)
        {
            var resp = new Response<List<MovimientoDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._inventarioService.ListaInventario(fechaInicio, fechaFin);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListaDetallesInventario")]
        public async Task<IActionResult> ListaDetallesInventario(int IdMovimiento)
        {
            var resp = new Response<List<InventarioLoteDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._inventarioService.ListaDetallesMovimiento(IdMovimiento);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        /*[HttpGet]
        [Route("ResumenVentasDiario")]
        public async Task<IActionResult> ResumenVentasDiario(DateOnly fechaResumen)
        {
            var resp = new Response<List<ResumenVentasDiarioDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._inventarioService.ResumenVentasDiario(fechaResumen);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }*/

       /* [HttpGet]
        [Route("ResumenVentasMensual")]
        public async Task<IActionResult> ResumenVentasMensual(DateOnly fechaResumen)
        {
            var resp = new Response<List<ResumenVentasDiarioDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._inventarioService.ResumenVentasMensual(fechaResumen);
            }
            catch(Exception ex)
            {
                resp.status = false;
                this.logger.LogError("Error en la conexion " + ex.Message);
                throw;
            }
            return Ok(resp);
        }*/
    }
}
