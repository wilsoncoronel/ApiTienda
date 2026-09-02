using SistemaTienda.DTO;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IDevolucionCompraService
    {
        Task<int> CrearDevolucionCompra(DevolucionCompraCreacionDTO dto);
    }
}
