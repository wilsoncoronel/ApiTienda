using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoVentaService _pagoVentaService;

        public PagosController(IPagoVentaService pagoVentaService)
        {
            _pagoVentaService = pagoVentaService;
        }

        [HttpGet]
        [Route("PagoVenta")]
        public async Task<IActionResult> PagoVenta(int idVenta)
        {
            var response = new Response<PagosVentasDTO>
            {
                status = true,
                Value = await _pagoVentaService.PagoVenta(idVenta),
                msg = "Pago de venta cargado correctamente!!"
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("CrearDetallePago")]
        public async Task<IActionResult> CrearDetallePago(DetallePagoCreacionDTO detallePagoCrear)
        {
            var response = new Response<int>
            {
                status = true,
                Value = await _pagoVentaService.CrearDetallePago(detallePagoCrear),
                msg = "Detalle de pago creado correctamente!!"
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("ListarEstadosPago")]
        public async Task<IActionResult> ListarEstadosPago()
        {
            var response = new Response<List<EstadosPagoDTO>>
            {
                status = true,
                Value = await _pagoVentaService.ListarEstadosPago(),
                msg = "Estados de pago listados correctamente!!"
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("ListarTiposPago")]
        public async Task<IActionResult> ListarTiposPago()
        {
            var response = new Response<List<TipoPagoDTO>>
            {
                status = true,
                Value = await _pagoVentaService.ListarTiposPago(),
                msg = "Tipos de pago listados correctamente!!"
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("ListarPagos")]
        public async Task<IActionResult> ListarPagos(DateOnly fechaInicial, DateOnly fechaFinal)
        {
            var response = new Response<List<PagosVentasDTO>>
            {
                status = true,
                Value = await _pagoVentaService.ListarPagos(fechaInicial, fechaFinal),
                msg = "Pagos listados correctamente!!"
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("ListaDetallesPagos")]
        public async Task<IActionResult> ListaDetallesPagos(int idPago)
        {
            var response = new Response<List<DetallesPagoDTO>>
            {
                status = true,
                Value = await _pagoVentaService.ListaDetallesPagos(idPago),
                msg = "Detalles de pago listados correctamente!!"
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("AnularPago")]
        public async Task<IActionResult> AnularPago(int idDetallePago)
        {
            var response = new Response<bool>
            {
                status = true,
                Value = await _pagoVentaService.AnularPago(idDetallePago),
                msg = "Pago anulado correctamente!!"
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("ActualizarComprobantePago")]
        public async Task<IActionResult> ActualizarComprobantePago(ActualizarComprobantePagoDTO comprobanteDto)
        {
            var response = new Response<bool>
            {
                status = true,
                Value = await _pagoVentaService.ActualizarComprobantePago(comprobanteDto),
                msg = "Comprobante de pago actualizado correctamente!!"
            };
            return Ok(response);
        }
    }
}
