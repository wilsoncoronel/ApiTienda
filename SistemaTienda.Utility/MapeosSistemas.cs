using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaTienda.DTO;
using SistemaTienda.Model;

namespace SistemaTienda.Utility
{
    public interface IMapeosSistemas
    {
        RolDTO MapeoRolTbARolDto(TbSisRol rolTb);
        List<RolDTO> MapeoListaRolDto(List<TbSisRol> ListaRolTb);
    }

    public class MapeosSistemas : IMapeosSistemas
    {
        public List<RolDTO> MapeoListaRolDto(List<TbSisRol> ListaRolTb)
        {
            return ListaRolTb.Select(rol => this.MapeoRolTbARolDto(rol)).ToList();    
        }

        public RolDTO MapeoRolTbARolDto(TbSisRol rolTb)
        {
            return new RolDTO
            {
                Id = rolTb.Id,
                Nombre = rolTb.Nombre,
            };
        }
    }
}
