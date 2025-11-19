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

        public InventarioController(IInventarioService inventarioService)
        {
            this._inventarioService = inventarioService;
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
            catch
            {
                resp.status = false;
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
            var resp = new Response<List<InventarioDTO>>();
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
        public async Task<IActionResult> ListaDetallesInventario(int IdInventario)
        {
            var resp = new Response<List<DetalleInventarioDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._inventarioService.ListaDetallesInventario(IdInventario);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }
    }
}
