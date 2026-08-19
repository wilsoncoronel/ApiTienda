using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DTO
{
   
    public class SriContribuyenteDTO
    {
        public string? NumeroRuc { get; set; }

        public string? RazonSocial { get; set; }

        public string? EstadoContribuyenteRuc { get; set; }

        public string? ActividadEconomicaPrincipal { get; set; }

        public string? TipoContribuyente { get; set; }

        public string? Regimen { get; set; }

        public string? Categoria { get; set; }

        public string? ObligadoLlevarContabilidad { get; set; }

        public string? AgenteRetencion { get; set; }

        public string? ContribuyenteEspecial { get; set; }

        public SriInformacionFechasDTO? InformacionFechasDTO { get; set; }

        public object? RepresentantesLegales { get; set; }

        public string? MotivoCancelacionSuspension { get; set; }

        public string? ContribuyenteFantasma { get; set; }

        public string? TransaccionesInexistente { get; set; }
        public SriEstablecimientoDTO? Establecimiento { get; set; }
    }
}