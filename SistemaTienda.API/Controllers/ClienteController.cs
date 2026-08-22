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
            resp.status = true;
            resp.Value = await this._clienteService.BuscarClienteCI(identificacion);
            resp.msg = "Cliente encontrado!!";
            return Ok(resp);
        }

        [HttpPost]
        [Route("CrearCliente")]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteCreacionDTO cliente)
        {
            var resp = new Response<int>();
            resp.status = true;
            resp.Value = await this._clienteService.CrearCliente(cliente);
            resp.msg = "Cliente creado exitosamente";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarTiposIdentificacion")]
        public async Task<IActionResult> ListarTiposIdentificacion()
        {
            var resp = new Response<List<TipoIdentificacionDTO>>();
            resp.status = true;
            resp.Value = await this._clienteService.ListarTiposIdentificacion();
            resp.msg = "Tipos de identificación listados exitosamente";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarClientes")]
        public async Task<IActionResult> ListarClientes()
        {
            var resp = new Response<List<ClienteDTO>>();
            resp.status = true;
            resp.Value = await this._clienteService.ListarClientes();
            resp.msg = "Clientes listados listados exitosamente";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarCiudades")]
        public async Task<IActionResult> ListarCiudades()
        {
            var resp = new Response<List<CiudadDTO>>();
            resp.status = true;
            resp.Value = await this._clienteService.ListarCiudades();
            resp.msg = "Tipos de identificación listados exitosamente";
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarCliente")]
        public async Task<IActionResult> EditarCliente(ClienteEditarDTO Cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resp = new Response<bool>();
            resp.status = true;
            resp.Value = await this._clienteService.EditarCliente(Cliente);
            resp.msg = "Cliente editado exitosamente";
            return Ok(resp);
        }
    }
}
