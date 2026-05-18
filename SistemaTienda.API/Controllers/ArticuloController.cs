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

        [HttpGet]
        [Route("CargarListaTiposArticulos")]
        public async Task<ActionResult<List<TipoArticuloDTO>>> CargarListaTiposArticulos()
        {
            var resp = new Response<List<TipoArticuloDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.CargarListaTiposArticulos();
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("CargarListaImpuestosArticulos")]
        public async Task<ActionResult<List<ImpuestoArticuloDTO>>> CargarListaImpuestos()
        {
            var resp = new Response<List<ImpuestoArticuloDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.CargarListaImpuestos();
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("CargarListaMarcasArticulos")]
        public async Task<ActionResult<List<MarcaDTO>>> CargarListaMarcas()
        {
            var resp = new Response<List<MarcaDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.CargarListaMarca();
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListaArticulos")]
        public async Task<ActionResult<List<ArticuloDTO>>> ListaArticulos(DateTime fechaInicial, DateTime fechaFinal)
        {
            var resp = new Response<List<ArticuloDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.ListarArticulos(fechaInicial, fechaFinal);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListaCodigosArticulos")]
        public async Task<ActionResult<List<CodigoArticuloDTO>>> ListaCodigosArticulos(int idArticulo)
        {
            var resp = new Response<List<CodigoArticuloDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.ListarCodigosArticulos(idArticulo);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarTodosArticulos")]
        public async Task<ActionResult<List<ArticuloDTO>>> ListarTodosArticulos()
        {
            var resp = new Response<List<ArticuloDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.ListarTodosArticulos();
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
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

        [HttpPost]
        [Route("CrearArticulosLista")]
        public async Task<ActionResult<bool>> CrearArticulosLista([FromBody] List<ArticuloCreacionDTO> articulosCreacionDto)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.CrearArticulosLista(articulosCreacionDto);
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

        [HttpPut]
        [Route("DesactivarArticulo")]
        public async Task<ActionResult<bool>> DesactivarArticulo([FromBody] int idArticulo)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this._articuloService.DesactivarArticulo(idArticulo);
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
