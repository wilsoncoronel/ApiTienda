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

        [HttpPost("CrearDevolucionCompra")]
        public async Task<IActionResult> CrearDevolucionCompra([FromBody] DevolucionCompraCreacionDTO dto)
        {
            var resp = new Response<int>();
            resp.status = true;
            resp.Value = await this._devolucionService.CrearDevolucionCompra(dto);
            resp.msg = "Devolucion creada con éxito!!";
            return Ok(resp);
        }
    }
}
