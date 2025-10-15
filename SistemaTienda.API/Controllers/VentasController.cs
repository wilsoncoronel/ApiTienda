using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaTienda.DTO;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;

namespace SistemaTienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventasService;

        public VentasController(IVentaService ventasService)
        {
            this._ventasService = ventasService;
        }

        [HttpPost]
        [Route("RegistrarVenta")]
        public async Task<IActionResult> RegistrarVenta(VentaCreacionDTO ventaCreacionDto)
        {

            var resp = new Response<int>();
            try
            {
                resp.status = true;
                resp.Value = await this._ventasService.RegistrarVenta(ventaCreacionDto);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarVenta")]
        public async Task<IActionResult> EditarVenta(VentaEditarDTO ventaEditarDto)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this._ventasService.EditarVenta(ventaEditarDto); // Simulación de ID de compra registrada
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarEstadosVenta")]
        public async Task<IActionResult> ListarEstadosVenta()
        {
            var resp = new Response<List<EstadoVentaDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._ventasService.ListarEstadosVentas();
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
