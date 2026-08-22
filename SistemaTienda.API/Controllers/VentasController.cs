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
            resp.status = true;
            resp.Value = await this._ventasService.RegistrarVenta(ventaCreacionDto);
            resp.msg = "Venta registrada con exito!!";
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarVenta")]
        public async Task<IActionResult> EditarVenta(VentaEditarDTO ventaEditarDto)
        {
            var resp = new Response<bool>();
            resp.status = true;
            resp.Value = await this._ventasService.EditarVenta(ventaEditarDto); // Simulación de ID de compra registrada
            resp.msg = "Venta ediatada correctamente";
            return Ok(resp);
        }

        [HttpPost]
        [Route("ReversarVenta")]
        public async Task<IActionResult> ReversarVenta(int id, string documento)
        {
            var resp = new Response<bool>();
            resp.status = true;
            resp.Value = await this._ventasService.ReversarVenta(id, documento);
            resp.msg = "Venta reversada!!";
            return Ok(resp);
        }
        [HttpGet]
        [Route("ListarEstadosVenta")]
        public async Task<IActionResult> ListarEstadosVenta()
        {
            var resp = new Response<List<EstadoVentaDTO>>();
            resp.status = true;
            resp.Value = await this._ventasService.ListarEstadosVentas();
            resp.msg = "Estados listado correctamente!!";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarVentas")]
        public async Task<IActionResult> ListarVentas(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            var resp = new Response<List<VentaMinDTO>>();
            resp.status = true;
            resp.Value = await this._ventasService.ListarVentas(fechaInicial, fechaFinal);
            resp.msg = "Ventas listadas correctamente!!";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ObtenerVenta")]
        public async Task<IActionResult> ObtenerVenta(int idVenta)
        {
            var resp = new Response<VentaDTO>();
            resp.status = true;
            resp.Value = await this._ventasService.ObtenerVenta(idVenta);
            resp.msg = "Veta cargada!!";
            return Ok(resp);
        }

    }
}
