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

        [HttpGet]
        [Route("BuscarProveedorCI")]
        public async Task<IActionResult> BuscarProveedorCI(string identificacion)
        {
            var resp = new Response<ProveedorDTO>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.BuscarProveedorCI(identificacion);
                resp.msg = "";
            }
            catch
            {
                resp.status = false;
                resp.msg = "Error al buscar el proveedor, comuniquese con el administrador del sistema!!!";
                throw;
            }
            return Ok(resp);
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

        [HttpGet]
        [Route("ListarProveedores")]
        public async Task<IActionResult> ListarProveedores()
        {
            var resp = new Response<List<ProveedorDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.ListarProveedores();
                resp.msg = "Proveedores listados listados exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarCiudades")]
        public async Task<IActionResult> ListarCiudades()
        {
            var resp = new Response<List<CiudadDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.ListarCiudades();
                resp.msg = "Ciudades listadas exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarTiposIdentificacion")]
        public async Task<IActionResult> ListarTiposIdentificacion()
        {
            var resp = new Response<List<TipoIdentificacionDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.ListarTiposIdentificacion();
                resp.msg = "Tipos identificación listadas exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }


        [HttpPut]
        [Route("EditarProveedor")]
        public async Task<IActionResult> EditarProveedor([FromBody] ProveedorEditarDTO proveedor)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.EditarProveedor(proveedor);
                resp.msg = "Proveedor editado exitosamente";
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
