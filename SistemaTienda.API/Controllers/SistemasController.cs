using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SistemasController : ControllerBase
    {
        private readonly IPersonaService personaService;
        private readonly ISistemaService sistemaService;

        public SistemasController(IPersonaService personaService, ISistemaService sistemaService) {
            this.personaService = personaService;
            this.sistemaService = sistemaService;
        }

        [HttpGet]
        [Route("ListarRoles")]
        public async Task<IActionResult> ListarRoles()
        {
            var resp = new Response<List<RolDTO>>();
            resp.status = true;
            resp.Value = await this.sistemaService.ListarRoles();
            resp.msg = "Roles listados exitosamente";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarPersonas")]
        public async Task<IActionResult> ListarPersonas()
        {
            var resp = new Response<List<PersonaDTO>>();
            resp.status = true;
            resp.Value = await this.personaService.ListaPersonas();
            resp.msg = "Personas listadas exitosamente";
            return Ok(resp);
        }

        [HttpGet]
        [Route("BuscarPersonas")]
        public async Task<IActionResult> PersonaCompleta(int idPersona)
        {
            var resp = new Response<PersonaCompletoDTO>();
            resp.status = true;
            resp.Value = await this.personaService.PersonaCompleta(idPersona);
            resp.msg = "Persona encontrada!!";
            return Ok(resp);
        }
    }
}
