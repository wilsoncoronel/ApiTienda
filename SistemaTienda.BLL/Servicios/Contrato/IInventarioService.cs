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
        Task<List<ExistenciaDTO>> ExistenciasInventario();
        Task<List<TransaccionInventarioDTO>> ListaTransaccionesInventario();
        Task<List<InventarioDTO>> ListaInventario(DateOnly FechaInicio, DateOnly FechaFinal);
        Task<List<DetalleInventarioDTO>> ListaDetallesInventario(int idInventario);
        Task<List<ResumenVentasDiarioDTO>> ResumenVentasDiario(DateOnly fechaResumen);

        Task<List<ResumenVentasDiarioDTO>> ResumenVentasMensual(DateOnly fechaResumen);
    }
}
