using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaTienda.API.Exceptions;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public class SriService : ISriService
    {
        private readonly HttpClient httpClient;
        private readonly TiendaDbContext tiendaDbContext;

        public SriService(HttpClient httpClient, TiendaDbContext tiendaDbContext)
        {
            this.httpClient = httpClient;
            this.tiendaDbContext = tiendaDbContext;
        }
        public async Task<bool> ExisteProveedorConIdentificacionAsync(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion))
                return false;

            return await tiendaDbContext.TbComProveedores.AsNoTracking().AnyAsync(p =>
                    p.IdPersonaNavigation.Identificacion == identificacion); // si aplica en tu modelo
        }

        public async Task<bool> ExistePersonaConIdentificacion(string iden)
        {
            return await tiendaDbContext.TbGrlPersonas
            .AsNoTracking().AnyAsync(per => per.Identificacion == iden);
        }

        public async Task<SriContribuyenteDTO?> ConsultarProveedorPorRuc(string ruc)
        {
            bool existePro = await this.ExisteProveedorConIdentificacionAsync(ruc);
            if(existePro)
                throw new ConflictException("Ya existe un proveedor en el sistema con la identificación proporcionada");
            bool existePer = await this.ExistePersonaConIdentificacion(ruc);
            if (existePer)
                throw new ConflictException("Ya existe una persona en el sistema con la identificación proporcionada, revise el modulo de personas");


            if (string.IsNullOrWhiteSpace(ruc) || ruc.Length != 13 || !ruc.All(char.IsDigit))
            {
                throw new BadRequestException("El RUC debe contener exactamente 13 dígitos.");
            }
            if (ruc.Length != 13)
                return null;
            string urlExistente =
                $"sri-catastro-sujeto-servicio-internet/rest/" +
                $"ConsolidadoContribuyente/" +
                $"existePorNumeroRuc?numeroRuc={ruc}";

            HttpResponseMessage responseExiste =
                await httpClient.GetAsync(urlExistente);

            responseExiste.EnsureSuccessStatusCode();

            string contenidoExiste =
                await responseExiste.Content.ReadAsStringAsync();

            bool existe =
                bool.TryParse(
                    contenidoExiste,
                    out bool resultado)
                && resultado;

            if (!existe)
                return null;
            // Obtener información del contribuyente
            string urlInformacion =
                $"sri-catastro-sujeto-servicio-internet/rest/" +
                $"ConsolidadoContribuyente/" +
                $"obtenerPorNumerosRuc?&ruc={ruc}";

            HttpResponseMessage responseContribuyente =
                await httpClient.GetAsync(urlInformacion);

            responseContribuyente.EnsureSuccessStatusCode();

            string contenidoContribuyente =
                await responseContribuyente.Content.ReadAsStringAsync();

            var contribuyentes =
                JsonConvert.DeserializeObject<
                    List<SriContribuyenteDTO>>(
                        contenidoContribuyente);

            var contribuyente =
                contribuyentes?.FirstOrDefault();

            if (contribuyente == null)
                return null;
            // Obtener establecimientos
            string urlEstablecimiento =
                $"sri-catastro-sujeto-servicio-internet/rest/" +
                $"Establecimiento/" +
                $"consultarPorNumeroRuc?numeroRuc={ruc}";

            HttpResponseMessage responseEstablecimiento =
                await httpClient.GetAsync(urlEstablecimiento);

            responseEstablecimiento.EnsureSuccessStatusCode();

            string contenidoEstablecimiento =
                await responseEstablecimiento.Content.ReadAsStringAsync();

            var establecimientos =
                JsonConvert.DeserializeObject<
                    List<SriEstablecimientoDTO>>(
                        contenidoEstablecimiento);
            contribuyente.Establecimiento =
                establecimientos?.FirstOrDefault();
            return contribuyente;
        }
    }
}
