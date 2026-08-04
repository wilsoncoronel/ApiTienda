using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IInventarioService
    {
        Task<List<InventarioLoteDTO>> ExistenciasInventario(bool incluirCeros = false);
        Task<List<TransaccionInventarioDTO>> ListaTransaccionesInventario();
        Task<List<MovimientoDTO>> ListaInventario(DateOnly FechaInicio, DateOnly FechaFinal);
        Task<List<InventarioLoteDTO>> ListaDetallesMovimiento(int idMovimiento);
        Task<List<ResumenVentasDiarioDTO>> ResumenVentasDiario(DateOnly fechaResumen);

        /*Task<List<ResumenVentasDiarioDTO>> ResumenVentasMensual(DateOnly fechaResumen);*/
    }
}
