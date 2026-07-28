using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguracionesController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        private readonly ITipoArticuloService _tipoArticuloService;
        private readonly IImpuestoArticuloService _impuestoArticuloService;
        private readonly ITransaccionInventarioService _transaccionInventarioService;
        public ConfiguracionesController(IMarcaService marcaService, ITipoArticuloService tipoArticuloService, IImpuestoArticuloService impuestoArticuloService, ITransaccionInventarioService transaccionInventarioService)
        {
            this._marcaService = marcaService;
            _tipoArticuloService = tipoArticuloService;
            _impuestoArticuloService = impuestoArticuloService;
            _transaccionInventarioService = transaccionInventarioService;
        }

        [HttpGet]
        [Route("ListarTransacciones")]
        public async Task<IActionResult> ListarTransacciones()
        {
            var resp = new Response<List<TransaccionInventarioDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._transaccionInventarioService.ListarTransaccionesInventario();
                resp.msg = "Transacciones listadas exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarMarcas")]
        public async Task<IActionResult> ListarMarcas()
        {
            var resp = new Response<List<MarcaDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._marcaService.ListarMarcas();
                resp.msg = "Marcas listadas exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPost]
        [Route("CrearMarca")]
        public async Task<IActionResult> CrearMarca(MarcaCreacionDTO marcaDto)
        {
            var resp = new Response<int>();
            try
            {
                resp.status = true;
                resp.Value = await this._marcaService.CrearMarca(marcaDto);
                resp.msg = "Marca creada exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarMarca")]
        public async Task<IActionResult> EditarMarca(MarcaEditarDTO marcaDto)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this._marcaService.EditarMarca(marcaDto);
                resp.msg = "Marca editada exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarTiposArticulos")]
        public async Task<IActionResult> ListarTiposArticulos()
        {
            var resp = new Response<List<TipoArticuloDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._tipoArticuloService.ListarTiposArticulos();
                resp.msg = "Tipos Artículos listados exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPost]
        [Route("CrearTipoArticulo")]
        public async Task<IActionResult> CrearTipoArticulo(TipoArticuloCreacionDTO tipoDto)
        {
            var resp = new Response<int>();
            try
            {
                resp.status = true;
                resp.Value = await this._tipoArticuloService.CrearTipoArticulo(tipoDto);
                resp.msg = "Tipo Articulo creado exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarTipoArticulo")]
        public async Task<IActionResult> EditarTipoArticulo(TipoArticuloEditarDTO tipoDto)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this._tipoArticuloService.EditarTipoArticulo(tipoDto);
                resp.msg = "Tipo artículo editada exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarImpuestosArticulos")]
        public async Task<IActionResult> ListarImpuestosArticulos()
        {
            var resp = new Response<List<ImpuestoArticuloDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._impuestoArticuloService.ListarImpuestos();
                resp.msg = "Impuestos Artículos listados exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarEstados")]
        public async Task<IActionResult> ListarEstados()
        {
            var resp = new Response<List<EstadoImpuestoDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._impuestoArticuloService.ListarEstados();
                resp.msg = "Estados listados exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPost]
        [Route("CrearImpuesto")]
        public async Task<IActionResult> CrearImpuesto(ImpuestoArticuloCreacionDTO impuestoCrearDto)
        {
            var resp = new Response<int>();
            try
            {
                resp.status = true;
                resp.Value = await this._impuestoArticuloService.CrearImpuestos(impuestoCrearDto);
                resp.msg = "Impuesto creado exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarImpuesto")]
        public async Task<IActionResult> EditarImpuesto(ImpuestoArticuloEditarDTO impuestoEditarDto)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this._impuestoArticuloService.EditarImpuesto(impuestoEditarDto);
                resp.msg = "Marca editada exitosamente";
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
