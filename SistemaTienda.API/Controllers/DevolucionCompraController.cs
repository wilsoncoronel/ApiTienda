using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;
using System.Threading.Tasks;

namespace SistemaTienda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevolucionCompraController : ControllerBase
    {
        private readonly IDevolucionCompraService _devolucionService;

        public DevolucionCompraController(IDevolucionCompraService devolucionService)
        {
            _devolucionService = devolucionService;
        }
        
        [HttpGet("ListarDetallesDevolucion")]
        public async Task<IActionResult> ListarDetallesDevolucion(int idDevolucionCompra)
        {
            
            var detalles = await _devolucionService.ListarDetallesDevolucion(idDevolucionCompra);
            return Ok(new Response<List<DetalleDevolucionCompraDTO>>
            {
                status = true,
                Value = detalles,
                msg = "Detalles de devolución listados correctamente"
            });
        }

        [HttpGet("ListarTiposDevolucion")]
        public async Task<IActionResult> ListarTiposDevolucion()
        {
            var tipos = await _devolucionService.ListaTiposDevolucion();
            return Ok(new Response<List<TipoDevolucionDTO>>
            {
                status = true,
                Value = tipos,
                msg = "Tipos de devolución listados correctamente"
            });
        }

        [HttpPost("CrearDevolucionCompra")]
        public async Task<IActionResult> CrearDevolucionCompra([FromBody] DevolucionCompraCreacionDTO dto)
        {
            var resp = new Response<int>();
            resp.status = true;
            resp.Value = await this._devolucionService.CrearDevolucionCompra(dto);
            resp.msg = "Devolucion creada con éxito!!";
            return Ok(resp);
        }

        [HttpPost("ReversarDevolucion/{id:int}")]
        public async Task<IActionResult> ReversarDevolucion(int id)
        {
            await _devolucionService.ReversarDevolucionCompra(id);
            return Ok(new Response<object>
            {
                status = true,
                Value = null,
                msg = "Devolución reversada correctamente"
            });
        }

        [HttpGet("ListarDevolucionesFechas")]
        public async Task<IActionResult> ListarDevolucionesFechas(
            [FromQuery] DateOnly fechaInicio,
            [FromQuery] DateOnly fechaFin)
        {
            var devoluciones = await _devolucionService.ListarDevolucionesFechas(fechaInicio, fechaFin);
            return Ok(new Response<List<DevolucionCompraDTO>>
            {
                status = true,
                Value = devoluciones,
                msg = "Devoluciones listadas correctamente"
            });
        }

        [HttpGet("VerCreditoPorDevolucion")]
        public async Task<IActionResult> VerCreditoPorDevolucion(int idDevolucion)
        {
            var credito = await _devolucionService.ObtenerCreditoPorDevolucion(idDevolucion);
            return Ok(new Response<CreditoProveedorDTO>
            {
                status = true,
                Value = credito,
                msg = "Crédito consultado correctamente"
            });
        }
    }
}
