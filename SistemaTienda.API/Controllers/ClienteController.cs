using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;
using SistemaTienda.API.Utilidad;


namespace SistemaTienda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            this._clienteService = clienteService;
        }
        // GET: ClienteController
        [HttpGet]
        [Route("BuscarClienteCI")]
        public async Task<IActionResult> BuscarClienteCI(string identificacion)
        {
            var resp = new Response<ClienteDTO>();
            try
            {
                resp.status = true;
                resp.Value = await this._clienteService.BuscarClienteCI(identificacion);
                resp.msg = "Cliente encontrado!!";
            }
            catch
            {
                resp.status = false;
                resp.msg = "Error al buscar el cliente, comuníquese con el administrador del sistema!!!";
                throw;
            }
            return Ok(resp);
        }

        /*[HttpPost]
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
        }*/
    }
}
