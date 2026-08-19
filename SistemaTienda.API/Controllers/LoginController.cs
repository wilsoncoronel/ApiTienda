using Microsoft.AspNetCore.Mvc;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.API.Utilidad;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogginService _loginService;
        public LoginController(ILogginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpGet]
        [Route("IniciarSesion")]
        public async Task<IActionResult> Loggin(string usuario, string password) {
            var resp = new Response<List<PermisosRolDTO>>();
            
            resp.status = true;
            resp.Value = await this._loginService.ValidarCredenciales(usuario, password);
            resp.msg = "Inicio de sesión exitoso!!";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ExtraerSesion")]
        public async Task<IActionResult> ExtraerSesion(string usuario)
        {
            var resp = new Response<SesionDTO>();
            
            resp.status = true;
            resp.Value = await this._loginService.ExtraerPerfil(usuario);
            resp.msg = "Sesión extraída exitosamente!!";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ObtenerPerfil")]
        public async Task<IActionResult> ObtenerPerfil(int id)
        {
            int idUsuario = id;
            var resp = new Response<UsuarioDTO>();
           
            resp.status = true;
            resp.Value = await this._loginService.ObtenerPerfil(idUsuario);
            resp.msg = "Perfil obtenido exitosamente!!";
            return Ok(resp);
        }
    }
}