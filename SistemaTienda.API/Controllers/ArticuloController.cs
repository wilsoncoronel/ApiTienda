using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            this._articuloService = articuloService;
        }

        [HttpPost]
        [Route("CrearArticulo")]
        public async Task<ActionResult<int>> CrearArticulo([FromBody] ArticuloCreacionDTO articuloCreacionDto)
        {
            var resp = new Response<int>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.CrearArticulo(articuloCreacionDto);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarArticulo")]
        public async Task<ActionResult<int>> EditarArticulo([FromBody] ArticuloEdicionDTO articuloEdicionDto)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.EditarArticulo(articuloEdicionDto);
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
