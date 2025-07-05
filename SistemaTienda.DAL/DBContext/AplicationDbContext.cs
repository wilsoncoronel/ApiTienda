using ApiTienda.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTienda.DAL.DBContext
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Articulo> TbArticulos { get; set; }
        public DbSet<Ciudad> TbCiudades { get; set; }
        public DbSet<Persona> TbPersonas { get; set; }
        public DbSet<Cliente> TbClientes { get; set; }
        public DbSet<Usuario> TbUsuarios { get; set; }
        public DbSet<Proveedor> TbProveedores { get; set; }
        public DbSet<Direccion> TbDirecciones { get; set; }
        public DbSet<EstadoCompra> TbEstadosCompras { get; set; }
        public DbSet<Compra> TbCompras { get; set; }
        public DbSet<DetalleCompra> TbDetallesCompras { get; set; }
        public DbSet<Venta> TbVentas { get; set; }
        public DbSet<DetalleVenta> TbDetallesVentas { get; set; }
        public DbSet<EstadoVenta> TbEstadosVentas { get; set; }
        public DbSet<Pedido> TbPedidos { get; set; }
        public DbSet<DetallePedido> TbDetallesPedidos { get; set; }
        public DbSet<EstadoPedido> TbEstadosPedidos { get; set; }
        public DbSet<ImpuestoArticulo> TbImpuestosArticulos { get; set; }
        public DbSet<EstadoImpuesto> TbEstadosImpuestos { get; set; }
        public DbSet<Inventario> TbInventario { get; set; }
        public DbSet<Marca> TbMarcas { get; set; }
        public DbSet<Menu> TbMenus { get; set; }
        public DbSet<PermisosRol> TbPermisosRol { get; set; }
        public DbSet<Rol> TbRol { get; set; }
        public DbSet<TipoArticulo> TbTiposArticulos { get; set; }
        public DbSet<TransaccionInventario> TbTransaccionesInventario { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-QO2URC6\\SQLEXPRESS;Database=TiendaDB;User Id=sa;Password=wili199308; Trusted_Connection=True; TrustServerCertificate=True;");*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
            .HasOne(p => p.Direccion)
            .WithOne(d => d.Persona)
            .HasForeignKey<Direccion>(d => d.IdPersona);

            modelBuilder.Entity<Compra>()
                .HasOne(c=> c.UsuarioCreador)
                .WithMany()
                .HasForeignKey(c=> c.IdUsuarioCreador)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Proveedor)
                .WithMany()
                .HasForeignKey(c => c.IdProveedor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.EstadoCompra)
                .WithMany()
                .HasForeignKey(c => c.IdEstado)
                .OnDelete(DeleteBehavior.Restrict);

            /*Pedido*/

            modelBuilder.Entity<Pedido>()
                .HasOne(c => c.UsuarioCreador)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Proveedor)
                .WithMany()
                .HasForeignKey(p => p.IdProveedor)
                .OnDelete(DeleteBehavior.Restrict);


            /*Venta*/

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.UsuarioCreador)
                .WithMany()
                .HasForeignKey(v => v.UsuarioCreadorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Proveedor)
                .WithMany()
                .HasForeignKey(p => p.IdProveedor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}

