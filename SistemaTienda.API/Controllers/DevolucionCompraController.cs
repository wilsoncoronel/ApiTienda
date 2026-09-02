using Microsoft.AspNetCore.Mvc;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;
using System.Threading.Tasks;

namespace SistemaTienda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevolucionCompraController : ControllerBase
    {
        private readonly IDevolucionCompraService _service;

        public DevolucionCompraController(IDevolucionCompraService service)
        {
            _service = service;
        }

        [HttpPost("CrearDevolucionCompra")]
        public async Task<IActionResult> CrearDevolucionCompra([FromBody] DevolucionCompraCreacionDTO dto)
        {
            var id = await _service.CrearDevolucionCompra(dto);
            return CreatedAtAction(nameof(CrearDevolucionCompra), new { id }, new { id });
        }
    }
}
