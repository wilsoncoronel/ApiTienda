using SistemaTienda.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IArticuloService
    {
        Task<int> CrearArticulo(ArticuloCreacionDTO articuloCreacionDto);
        Task<bool> CrearArticulosLista(List<ArticuloCreacionDTO> articulosCreacionDto);
        Task<List<ArticuloDTO>> ListarArticulos(DateTime fechaInicio, DateTime fechaFinal);
        Task<bool> EditarArticulo(ArticuloEdicionDTO articuloEditarDto);
        Task<bool> DesactivarArticulo(int idArticulo);
        Task<List<TipoArticuloDTO>> CargarListaTiposArticulos();
        Task<List<ImpuestoArticuloDTO>> CargarListaImpuestos();
        Task<List<MarcaDTO>> CargarListaMarca();
        Task<List<InventarioLoteDTO>> ListarTodosArticulos();
    }
}
