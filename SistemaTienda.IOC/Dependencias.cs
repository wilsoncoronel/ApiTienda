using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaTienda.BLL.Servicios;
using SistemaTienda.BLL.Servicios.Contrato;
using SistemaTienda.DAL.DBContext;
using SistemaTienda.DAL.Repositorios;
using SistemaTienda.DAL.Repositorios.Contrato;
using SistemaTienda.Utility;
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
            services.AddDbContext<TiendaDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }); 
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IMapeos, Mapeos>();
            services.AddTransient<ILogginService, LogginService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IArticuloService, ArticuloService>();
            services.AddTransient<IProveedorService, ProveedorService>();
        }
    }
}
