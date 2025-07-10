using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios;
using SistemaTienda.DAL.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.IOC
{
    public static class Dependencias
    {
        public static void InyectarDependecias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }); 
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
