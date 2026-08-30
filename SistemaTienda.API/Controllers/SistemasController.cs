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

        public SistemasController(IPersonaService personaService) {
            this.personaService = personaService;
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
    }
}
