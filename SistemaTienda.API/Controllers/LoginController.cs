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
            var resp = new Response<SesionDTO>();
            try
            {
                resp.status = true;
                resp.Value = await this._loginService.ValidarCredenciales(usuario, password);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ObtenerPerfil")]
        public async Task<IActionResult> ObtenerPerfil(int id)
        {
            int idUsuario = id;
            var resp = new Response<UsuarioDTO>();
            try
            {
                resp.status = true;
                resp.Value = await this._loginService.ObtenerPerfil(idUsuario);
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