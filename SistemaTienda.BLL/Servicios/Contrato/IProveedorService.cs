using SistemaTienda.DTO;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IProveedorService
    {
        Task<List<ProveedorDTO>> ListarProveedores();
        Task<List<CiudadDTO>> ListarCiudades();
        Task<List<TipoIdentificacionDTO>> ListarTiposIdentificacion();
        Task<int> CrearProveedor(ProveedorCreacionDTO proveedorCreacionDto);
        Task<bool> EditarProveedor(ProveedorEditarDTO proveedorEditarDto);
        Task<ProveedorDTO> BuscarProveedorCI(string identificacion);
    }
}