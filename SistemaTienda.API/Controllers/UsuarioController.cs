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
            
            resp.status = true;
            resp.Value = await this._usuarioService.CrearUsuario(usuario);
            resp.msg = "Usuario creado con exito!!";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarUsuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var resp = new Response<List<UsuarioDTO>>();
            
            resp.status = true;
            resp.Value = await this._usuarioService.ListarUsuario();
            resp.msg = "Lista de usuario cargada correctamente";
            return Ok(resp);
        }


        [HttpGet]
        [Route("ListarUsuarioId")]
        public async Task<IActionResult> ListarUsuarioId(int IdUsuario)
        {
            var resp = new Response<UsuarioDTO>();
            int Id = IdUsuario;
            
            resp.status = true;
            resp.Value = await this._usuarioService.ListarUsuarioId(Id);
            resp.msg = "Usuario cargado!!";
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario([FromBody] UsuarioEditarDTO usuarioEditarDto)
        {
            var resp = new Response<int>();
            resp.status = true;
            resp.Value = await this._usuarioService.EditarUsuario(usuarioEditarDto);
            resp.msg = "Usuario editado correctamente!!";
            return Ok(resp);
        }
    }
}