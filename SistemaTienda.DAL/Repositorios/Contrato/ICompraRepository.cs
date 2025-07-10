using ApiTienda.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DAL.Repositorios.Contrato
{
    public interface ICompraRepository:IGenericRepository<Compra> 
    {
        Task<Compra> Registrar(Compra modelo);
    }
}
