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
        private readonly IPorcentajeGananciaService _porcentajeService;

        public ArticuloController(IArticuloService articuloService, IPorcentajeGananciaService porcentajeService)
        {
            this._articuloService = articuloService;
            this._porcentajeService = porcentajeService;
        }

        [HttpGet]
        [Route("CargarListaTiposArticulos")]
        public async Task<ActionResult<List<TipoArticuloDTO>>> CargarListaTiposArticulos()
        {
            var resp = new Response<List<TipoArticuloDTO>>();
           
            resp.status = true;
            resp.Value = await this._articuloService.CargarListaTiposArticulos();
            resp.msg = "Lista de tipos de artículos cargada correctamente.";
            return Ok(resp);
        }

        [HttpGet]
        [Route("CargarListaImpuestosArticulos")]
        public async Task<ActionResult<List<ImpuestoArticuloDTO>>> CargarListaImpuestos()
        {
            var resp = new Response<List<ImpuestoArticuloDTO>>();
           
            resp.status = true;
            resp.Value = await this._articuloService.CargarListaImpuestos();
            resp.msg = "Lista de impuestos de artículos cargada correctamente.";

            return Ok(resp);
        }

        [HttpGet]
        [Route("CargarListaMarcasArticulos")]
        public async Task<ActionResult<List<MarcaDTO>>> CargarListaMarcas()
        {
            var resp = new Response<List<MarcaDTO>>();
           
            resp.status = true;
            resp.Value = await this._articuloService.CargarListaMarca();
            resp.msg = "Lista de marcas de artículos cargada correctamente.";

            return Ok(resp);
        }

        [HttpGet]
        [Route("ListaArticulos")]
        public async Task<ActionResult<List<ArticuloDTO>>> ListaArticulos(DateTime fechaInicial, DateTime fechaFinal)
        {
            var resp = new Response<List<ArticuloDTO>>();
           
            resp.status = true;
            resp.Value = await this._articuloService.ListarArticulos(fechaInicial, fechaFinal);            
            resp.msg = "Error al cargar la lista de artículos.";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarPorcentajes")]
        public async Task<ActionResult<List<PorcentajeGananciaDTO>>> ListaPorcentajes()
        {
            var resp = new Response<List<PorcentajeGananciaDTO>>();
            resp.status = true;
            resp.Value = await this._porcentajeService.ListarPorcentaje();
            resp.msg = "Lista de porcentajes cargada correctamente.";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarTodosArticulos")]
        public async Task<ActionResult<List<ArticuloInventarioDTO>>> ListarTodosArticulos(bool esVenta = false)
        {
            var resp = new Response<List<ArticuloInventarioDTO>>();
            resp.status = true;
            resp.Value = await this._articuloService.ListarTodosArticulos(esVenta);
            resp.msg = "Lista de artículos cargada correctamente.";
            return Ok(resp);
        }


        [HttpPost]
        [Route("CrearArticulo")]
        public async Task<ActionResult<int>> CrearArticulo([FromBody] ArticuloCreacionDTO articuloCreacionDto)
        {
            var resp = new Response<int>();
            resp.status = true;
            resp.Value = await this._articuloService.CrearArticulo(articuloCreacionDto);
            resp.msg = "Artículo creado correctamente.";
            return Ok(resp);
        }

        [HttpPost]
        [Route("CrearArticulosLista")]
        public async Task<ActionResult<bool>> CrearArticulosLista([FromBody] List<ArticuloCreacionDTO> articulosCreacionDto)
        {
            var resp = new Response<bool>();
            resp.status = true;
            resp.Value = await this._articuloService.CrearArticulosLista(articulosCreacionDto);
            resp.msg = "Artículos creados correctamente.";
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarArticulo")]
        public async Task<ActionResult<int>> EditarArticulo([FromBody] ArticuloEdicionDTO articuloEdicionDto)
        {
            var resp = new Response<bool>();
            resp.status = true;
            resp.Value = await this._articuloService.EditarArticulo(articuloEdicionDto);
            resp.msg = "Artículo editado correctamente.";
            return Ok(resp);
        }

        [HttpPut]
        [Route("DesactivarArticulo")]
        public async Task<ActionResult<bool>> DesactivarArticulo([FromBody] int idArticulo)
        {
            var resp = new Response<bool>();
            resp.status = true;
            resp.Value = await this._articuloService.DesactivarArticulo(idArticulo);
            resp.msg = "Artículo desactivado correctamente.";
            return Ok(resp);
        }
    }
}
