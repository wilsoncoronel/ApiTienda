using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            this.proveedorService = proveedorService;
        }
        [HttpPost]
        [Route("CrearProveedor")]
        public async Task<IActionResult> CrearProveedor([FromBody] ProveedorCreacionDTO proveedor)
        {
            var resp = new Response<int>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.CrearProveedor(proveedor);
                resp.msg = "Proveedor creado exitosamente";
            }
            catch
            {
                resp.status = false;
                resp.msg = "Error al crear el proveedor, comuniquese con el administrador del sistema!!!";
                throw;
            }
            return Ok(resp);
        }
    }
}
