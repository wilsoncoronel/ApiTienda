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
            try
            {
                // Aquí iría la lógica para registrar la compra utilizando un servicio
                // Por ejemplo: resp.Value = await this._compraService.RegistrarCompra(ventaCreacionDto);
                
                resp.status = true;
                resp.Value = await this._compraService.RegistrarCompra(compraCreacionDto); // Simulación de ID de compra registrada
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpPut]
        [Route("EditarCompra")]
        public async Task<IActionResult> EditarCompra(CompraEditarDTO compraEditarDto)
        {

            var resp = new Response<bool>();
            try
            {
                // Aquí iría la lógica para registrar la compra utilizando un servicio
                // Por ejemplo: resp.Value = await this._compraService.RegistrarCompra(ventaCreacionDto);

                resp.status = true;
                resp.Value = await this._compraService.EditarCompra(compraEditarDto); // Simulación de ID de compra registrada
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarEstadosCompra")]
        public async Task<IActionResult> ListarEstadosCompra()
        {
            var resp = new Response<List<EstadoCompraDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._compraService.ListarEstadosCompras();
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarCompras")]
        public async Task<IActionResult> ListarCompras(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            var resp = new Response<List<CompraMinDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this._compraService.ListarCompras(fechaInicial, fechaFinal);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }


        [HttpGet]
        [Route("ObtenerCompra")]
        public async Task<IActionResult> ObtenerCompra(int idCompra)
        {

            var resp = new Response<CompraDTO>();
            try
            {
                resp.status = true;
                resp.Value = await this._compraService.ObtenerCompra(idCompra);
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ReversarCompra")]
        public async Task<IActionResult> ReversarCompra(int idCompra)
        {

            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this._compraService.ReversarCompra(idCompra);
                resp.msg = "Reversion de la comra exitosa";
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
