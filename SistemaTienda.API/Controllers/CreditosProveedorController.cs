using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreditosProveedorController : ControllerBase
{
    private readonly ICreditoProveedorService _creditoService;

    public CreditosProveedorController(ICreditoProveedorService creditoService)
    {
        _creditoService = creditoService;
    }

    [HttpGet("ListarCreditosProveedor/{idProveedor:int}")]
    public async Task<IActionResult> ListarCreditosProveedor(int idProveedor)
    {
        var response = new Response<List<CreditoProveedorListaDTO>>
        {
            status = true,
            Value = await _creditoService.ListarCreditosProveedor(idProveedor),
            msg = "Créditos consultados correctamente"
        };
        return Ok(response);
    }

    [HttpGet("VerCredito/{id:int}")]
    public async Task<IActionResult> VerCredito(int id)
    {
        var response = new Response<CreditoProveedorDTO>
        {
            status = true,
            Value = await _creditoService.VerCredito(id),
            msg = "Crédito consultado correctamente"
        };
        return Ok(response);
    }

    [HttpPost("AplicarCreditoCompra")]
    public async Task<IActionResult> AplicarCreditoCompra([FromBody] AplicarCreditoCompraDTO dto)
    {
        await _creditoService.AplicarCreditoCompra(dto);
        return Ok(new Response<object>
        {
            status = true,
            Value = null,
            msg = "Crédito aplicado correctamente"
        });
    }
}
