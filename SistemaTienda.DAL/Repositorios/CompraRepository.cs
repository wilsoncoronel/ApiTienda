using ApiTienda.Model;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DAL.Repositorios
{
    public class CompraRepository : GenericRepository<Compra>, ICompraRepository
    {
        private readonly AplicationDbContext _context;
        public CompraRepository(AplicationDbContext context): base(context) {
            this._context = context;
        }
        public async Task<Compra> Registrar(Compra modelo)
        {
            Compra compra = new Compra();
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
