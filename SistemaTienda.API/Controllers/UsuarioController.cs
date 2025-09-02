using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this._usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioCreacionDTO usuario)
        {
            var resp = new Response<int>();
            try
            {
                resp.status = true;
                resp.Value = await this._usuarioService.CrearUsuario(usuario);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarUsuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var resp = new Response<List<UsuarioDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._usuarioService.ListarUsuario();
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }


        [HttpGet]
        [Route("ListarUsuarioId")]
        public async Task<IActionResult> ListarUsuarioId(int IdUsuario)
        {
            var resp = new Response<UsuarioDTO>();
            int Id = IdUsuario;
            try
            {
                resp.status = true;
                resp.Value = await this._usuarioService.ListarUsuarioId(Id);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario([FromBody] UsuarioEditarDTO usuarioEditarDto)
        {
            var resp = new Response<int>();
            try
            {
                resp.status = true;
                resp.Value = await this._usuarioService.EditarUsuario(usuarioEditarDto);
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