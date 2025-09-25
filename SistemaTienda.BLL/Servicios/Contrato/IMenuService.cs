using SistemaTienda.DTO;
using SistemaTienda.Model;

namespace SistemaTienda.BLL.Servicios.Contrato
{
    public interface IMenuService
    {
        Task<List<PermisosRolDTO>> ObtenerMenu(int idRol);
    }
}