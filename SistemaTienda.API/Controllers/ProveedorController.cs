using Microsoft.AspNetCore.Mvc;
using SistemaTienda.API.Exceptions;
using SistemaTienda.API.Utilidad;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DTO;

namespace SistemaTienda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService proveedorService;
        private readonly ISriService sriService;

        public ProveedorController(IProveedorService proveedorService, ISriService sriService)
        {
            this.proveedorService = proveedorService;
            this.sriService = sriService;
        }
        [HttpGet("ConsultarRuc")]
        public async Task<IActionResult> ConsultarRuc(string ruc)
        {
            if (string.IsNullOrWhiteSpace(ruc))
            {
                throw new BadRequestException(
                    "Debe ingresar un RUC.");
            }

            if (ruc.Length > 13 ||
                !ruc.All(char.IsDigit))
            {
                throw new BadRequestException(
                    "La identificación no es valida!! Por favbor revisela!!");
            }

            var contribuyente = await sriService.ConsultarProveedorPorRuc(ruc);

            if (contribuyente == null)
            {
                throw new NotFoundException(
                    "No se encontró información para el RUC indicado.");
            }

            return Ok(new Response<SriContribuyenteDTO>
            {
                status = true,
                Value = contribuyente,
                msg = "Información del RUC obtenida correctamente."
            });
        }

        [HttpGet]
        [Route("BuscarProveedorCI")]
        public async Task<IActionResult> BuscarProveedorCI(string identificacion, bool verPersona)
        {
            var resp = new Response<ProveedorDTO>();
            resp.status = true;
            resp.Value = await this.proveedorService.BuscarProveedorCI(identificacion, verPersona);
            resp.msg = "Error al buscar el proveedor, comuniquese con el administrador del sistema!!!";
            return Ok(resp);
        }

        [HttpPost]
        [Route("CrearProveedor")]
        public async Task<IActionResult> CrearProveedor([FromBody] ProveedorCreacionDTO proveedor)
        {
            var resp = new Response<int>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.CrearProveedor(proveedor);
                resp.msg = "Proveedor creado exitosamente";
            }
            catch
            {
                resp.status = false;
                resp.msg = "Error al crear el proveedor, comuniquese con el administrador del sistema!!!";
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarProveedores")]
        public async Task<IActionResult> ListarProveedores()
        {
            var resp = new Response<List<ProveedorDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.ListarProveedores();
                resp.msg = "Proveedores listados listados exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarCiudades")]
        public async Task<IActionResult> ListarCiudades()
        {
            var resp = new Response<List<CiudadDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.ListarCiudades();
                resp.msg = "Ciudades listadas exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }

        [HttpGet]
        [Route("ListarTiposIdentificacion")]
        public async Task<IActionResult> ListarTiposIdentificacion()
        {
            var resp = new Response<List<TipoIdentificacionDTO>>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.ListarTiposIdentificacion();
                resp.msg = "Tipos identificación listadas exitosamente";
            }
            catch
            {
                resp.status = false;
                throw;
            }
            return Ok(resp);
        }


        [HttpPut]
        [Route("EditarProveedor")]
        public async Task<IActionResult> EditarProveedor([FromBody] ProveedorEditarDTO proveedor)
        {
            var resp = new Response<bool>();
            try
            {
                resp.status = true;
                resp.Value = await this.proveedorService.EditarProveedor(proveedor);
                resp.msg = "Proveedor editado exitosamente";
            }
            catch
            {
                resp.status = false;
                resp.msg = "Error al crear el proveedor, comuniquese con el administrador del sistema!!!";
                throw;
            }
            return Ok(resp);
        }
    }
}
