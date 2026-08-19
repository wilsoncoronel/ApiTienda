using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ICompraServicio _compraService;
        public ComprasController(ICompraServicio compraService )
        {
            this._compraService = compraService;
        }

        [HttpPost]
        [Route("RegistrarCompra")]
        public async Task<IActionResult> RegistrarCompra(CompraCreacionDTO compraCreacionDto)
        {
           
            var resp = new Response<int>();
            resp.status = true;
            resp.Value = await this._compraService.RegistrarCompra(compraCreacionDto); // Simulación de ID de compra registrada
            resp.msg = "Compra registrada correctamente";
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarCompra")]
        public async Task<IActionResult> EditarCompra(CompraEditarDTO compraEditarDto)
        {

            var resp = new Response<bool>();
            resp.status = true;
            resp.Value = await this._compraService.EditarCompra(compraEditarDto); // Simulación de ID de compra registrada
            resp.msg = "Compra editada correctamente";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarEstadosCompra")]
        public async Task<IActionResult> ListarEstadosCompra()
        {
            var resp = new Response<List<EstadoCompraDTO>>();
            resp.status = true;
            resp.Value = await this._compraService.ListarEstadosCompras();
            resp.msg = "Estados de compra listados correctamente";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarCompras")]
        public async Task<IActionResult> ListarCompras(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            var resp = new Response<List<CompraMinDTO>>();
            resp.status = true;
            resp.Value = await this._compraService.ListarCompras(fechaInicial, fechaFinal);
            resp.msg = "Compras listadas correctamente";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ObtenerCompra")]
        public async Task<IActionResult> ObtenerCompra(int idCompra)
        {

            var resp = new Response<CompraDTO>();
            resp.status = true;
            resp.Value = await this._compraService.ObtenerCompra(idCompra);
            resp.msg = "Compra obtenida correctamente";
            return Ok(resp);
        }

        [HttpGet]
        [Route("ReversarCompra")]
        public async Task<IActionResult> ReversarCompra(int idCompra)
        {
            var resp = new Response<bool>();    
            resp.status = true;
            resp.Value = await this._compraService.ReversarCompra(idCompra);
            resp.msg = "Reversion de la compra exitosa";
            return Ok(resp);
        }
    }
}
