using SistemaTienda.DTO;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IProveedorService
    {
        Task<int> CrearProveedor(ProveedorCreacionDTO proveedorCreacionDto);
        Task<bool> EditarProveedor(ProveedorEditarDTO proveedorEditarDto);
        Task<ProveedorDTO> BuscarProveedorCI(string identificacion);
    }
}