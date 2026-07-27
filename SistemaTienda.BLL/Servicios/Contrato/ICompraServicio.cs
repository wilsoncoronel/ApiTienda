using SistemaTienda.DTO;
using SistemaTienda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface ICompraServicio
    {
        Task<List<EstadoCompraDTO>> ListarEstadosCompras();
        Task<int> RegistrarCompra(CompraCreacionDTO compraDto);
        Task<bool> EditarCompra(CompraEditarDTO compraDto);
        // Nueva sobrecarga: editar movimiento/lotes por idCompra y documento (referencia)
        Task<List<CompraMinDTO>> ListarCompras(DateOnly fechaInicial, DateOnly fechaFinal);
        Task<CompraDTO> ObtenerCompra(int idCompra);
        Task<bool> ReversarCompra(int id);
    }
}
