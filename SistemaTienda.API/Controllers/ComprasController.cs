using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        [HttpPost]
        [Route("RegistrarCompra")]
        public async Task<IActionResult> RegistrarCompra(VentaCreacionDTO ventaCreacionDto)
        {
            var resp = new Response<int>();
            try
            {
                // Aquí iría la lógica para registrar la compra utilizando un servicio
                // Por ejemplo: resp.Value = await this._compraService.RegistrarCompra(ventaCreacionDto);
                resp.status = true;
                resp.Value = 1; // Simulación de ID de compra registrada
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
