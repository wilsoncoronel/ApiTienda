using SistemaTienda.Model;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DAL.Repositorios
{
    public class CompraRepository : GenericRepository<TbCompra>, ICompraRepository
    {
        private readonly TiendaDbContext _context;

        public CompraRepository(TiendaDbContext context): base(context) {
            this._context = context;
        }
        public async Task<TbCompra> Registrar(TbCompra modelo)
        {
            TbCompra compra = new TbCompra();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.AddAsync(modelo);
                    await _context.SaveChangesAsync();
                    compra = modelo;
                    transaction.Commit();
                }
                catch {
                    transaction.Rollback();
                    throw;
                }
            }
            return compra;
        }
    }
}
