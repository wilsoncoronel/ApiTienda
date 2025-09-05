using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaTienda.Model;

namespace SistemaTienda.DAL.DBContext;

public partial class TiendaDbContext : DbContext
{
    public TiendaDbContext()
    {
    }

    public TiendaDbContext(DbContextOptions<TiendaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbComArticulo> TbComArticulos { get; set; }

    public virtual DbSet<TbComCliente> TbComClientes { get; set; }

    public virtual DbSet<TbComDetallesCompra> TbComDetallesCompras { get; set; }

    public virtual DbSet<TbComEstadosCompra> TbComEstadosCompras { get; set; }

    public virtual DbSet<TbComEstadosImpuesto> TbComEstadosImpuestos { get; set; }

    public virtual DbSet<TbComImpuestosArticulo> TbComImpuestosArticulos { get; set; }

    public virtual DbSet<TbComMarca> TbComMarcas { get; set; }

    public virtual DbSet<TbComProveedores> TbComProveedores { get; set; }

    public virtual DbSet<TbComTiposArticulo> TbComTiposArticulos { get; set; }

    public virtual DbSet<TbCompra> TbCompras { get; set; }

    public virtual DbSet<TbGrlCiudades> TbGrlCiudades { get; set; }

    public virtual DbSet<TbGrlDirecciones> TbGrlDirecciones { get; set; }

    public virtual DbSet<TbGrlPersona> TbGrlPersonas { get; set; }

    public virtual DbSet<TbGrlTipoIdentificacion> TbGrlTipoIdentificacions { get; set; }

    public virtual DbSet<TbInvTransacciones> TbInvTransacciones { get; set; }

    public virtual DbSet<TbInventario> TbInventarios { get; set; }

    public virtual DbSet<TbPedDetallesPedido> TbPedDetallesPedidos { get; set; }

    public virtual DbSet<TbPedEstadosPedido> TbPedEstadosPedidos { get; set; }

    public virtual DbSet<TbPedidos> TbPedidos { get; set; }

    public virtual DbSet<TbSisMenu> TbSisMenus { get; set; }

    public virtual DbSet<TbSisPermisosRol> TbSisPermisosRols { get; set; }

    public virtual DbSet<TbSisRol> TbSisRols { get; set; }

    public virtual DbSet<TbSisUsuario> TbSisUsuarios { get; set; }

    public virtual DbSet<TbVenDetalleVenta> TbVenDetalleVenta { get; set; }

    public virtual DbSet<TbVenEstadosVenta> TbVenEstadosVentas { get; set; }

    public virtual DbSet<TbVenta> TbVentas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbComArticulo>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCaducidad).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Unidad).HasMaxLength(100);
            entity.Property(e => e.UnidadValor).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorCompra).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorVenta).HasColumnType("numeric(18, 4)");

            entity.HasOne(d => d.IdImpuestoNavigation).WithMany(p => p.TbComArticulos)
                .HasForeignKey(d => d.IdImpuesto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbComArticulos_TbComImpuestosArticulos");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.TbComArticulos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbComArticulos_TbComMarca");

            entity.HasOne(d => d.IdTipoArticuloNavigation).WithMany(p => p.TbComArticulos)
                .HasForeignKey(d => d.IdTipoArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbComArticulos_TbComTiposArticulos");

            entity.HasOne(d => d.IdUsuarioCreadorNavigation).WithMany(p => p.TbComArticulos)
                .HasForeignKey(d => d.IdUsuarioCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbComArticulos_TbSisUsuarios");
        });

        modelBuilder.Entity<TbComCliente>(entity =>
        {
            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TbComClientes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbComClientes_TbGrlPersonas");
        });

        modelBuilder.Entity<TbComDetallesCompra>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.ImpuestoValor).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorCompra).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorTotal).HasColumnType("numeric(18, 4)");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.TbComDetallesCompras)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbComDetallesCompras_TbComArticulos");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.TbComDetallesCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbComDetallesCompras_TbCompras");
        });

        modelBuilder.Entity<TbComEstadosCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TbComEstadoCompra");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TbComEstadosImpuesto>(entity =>
        {
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TbComImpuestosArticulo>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.ValorImpuesto).HasColumnType("numeric(18, 4)");

            entity.HasOne(d => d.IdEstadoImpuestoNavigation).WithMany(p => p.TbComImpuestosArticulos)
                .HasForeignKey(d => d.IdEstadoImpuesto)
                .HasConstraintName("FK_TbComImpuestosArticulos_TbComEstadosImpuestos");
        });

        modelBuilder.Entity<TbComMarca>(entity =>
        {
            entity.ToTable("TbComMarca");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TbComProveedores>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.RazonSocial).HasMaxLength(250);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TbComProveedores)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbComProveedores_TbGrlPersonas");
        });

        modelBuilder.Entity<TbComTiposArticulo>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<TbCompra>(entity =>
        {
            entity.Property(e => e.Documento).HasMaxLength(50);
            entity.Property(e => e.FechaCompra).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.Total).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorIva).HasColumnType("numeric(18, 4)");

            entity.HasOne(d => d.IdEstadoCompraNavigation).WithMany(p => p.TbCompras)
                .HasForeignKey(d => d.IdEstadoCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbCompras_TbComEstadosCompras");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TbCompras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbCompras_TbComProveedores");

            entity.HasOne(d => d.IdUsuarioCreadorNavigation).WithMany(p => p.TbCompras)
                .HasForeignKey(d => d.IdUsuarioCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbCompras_TbSisUsuarios");
        });

        modelBuilder.Entity<TbGrlCiudades>(entity =>
        {
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<TbGrlDirecciones>(entity =>
        {
            entity.HasIndex(e => e.IdPersona, "UQ_DireccionPersona_IdPersona").IsUnique();

            entity.Property(e => e.Descripcion).HasMaxLength(500);

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.TbGrlDirecciones)
                .HasForeignKey(d => d.IdCiudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbGrlDirecciones_TbGrlCiudades");

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.TbGrlDireccione)
                .HasForeignKey<TbGrlDirecciones>(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbGrlDirecciones_TbGrlPersonas");
        });

        modelBuilder.Entity<TbGrlPersona>(entity =>
        {
            entity.Property(e => e.Apellidos).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.Mail).HasMaxLength(200);
            entity.Property(e => e.Nombres).HasMaxLength(500);

            entity.HasOne(d => d.IdTipoIdentificacionNavigation).WithMany(p => p.TbGrlPersonas)
                .HasForeignKey(d => d.IdTipoIdentificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbGrlPersonas_TbGrlTipoIdentificacion");
        });

        modelBuilder.Entity<TbGrlTipoIdentificacion>(entity =>
        {
            entity.ToTable("TbGrlTipoIdentificacion");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TbInvTransacciones>(entity =>
        {
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<TbInventario>(entity =>
        {
            entity.ToTable("TbInventario");

            entity.Property(e => e.PrecioUnitario).HasColumnType("numeric(18, 4)");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.TbInventarios)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbInventario_TbComArticulos");

            entity.HasOne(d => d.IdTransaccionInventarioNavigation).WithMany(p => p.TbInventarios)
                .HasForeignKey(d => d.IdTransaccionInventario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbInventario_TbInvTransacciones");
        });

        modelBuilder.Entity<TbPedDetallesPedido>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.ImpuestoValor).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorCompra).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorTotal).HasColumnType("numeric(18, 4)");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.TbPedDetallesPedidos)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPedDetallesPedidos_TbComArticulos");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.TbPedDetallesPedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPedDetallesPedidos_TbPedidos");
        });

        modelBuilder.Entity<TbPedEstadosPedido>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TbPedidos>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdEstadoPedidoNavigation).WithMany(p => p.TbPedidos)
                .HasForeignKey(d => d.IdEstadoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPedidos_TbPedEstadosPedidos");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.TbPedidos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPedidos_TbComProveedores");

            entity.HasOne(d => d.IdUsuarioCreadorNavigation).WithMany(p => p.TbPedidos)
                .HasForeignKey(d => d.IdUsuarioCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbPedidos_TbSisUsuarios");
        });

        modelBuilder.Entity<TbSisMenu>(entity =>
        {
            entity.ToTable("TbSisMenu");

            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Url).HasMaxLength(200);
        });

        modelBuilder.Entity<TbSisPermisosRol>(entity =>
        {
            entity.ToTable("TbSisPermisosRol");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.TbSisPermisosRols)
                .HasForeignKey(d => d.IdMenu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSisPermisosRol_TbSisMenu");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TbSisPermisosRols)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSisPermisosRol_TbSisRol");
        });

        modelBuilder.Entity<TbSisRol>(entity =>
        {
            entity.ToTable("TbSisRol");

            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<TbSisUsuario>(entity =>
        {
            entity.Property(e => e.NombreUsuario).HasMaxLength(200);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TbSisUsuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSisUsuarios_TbGrlPersonas");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.TbSisUsuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbSisUsuarios_TbSisRol");
        });

        modelBuilder.Entity<TbVenDetalleVenta>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.ImpuestoValor).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorCompra).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorTotal).HasColumnType("numeric(18, 4)");

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.TbVenDetalleVenta)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbVenDetalleVenta_TbComArticulos");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.TbVenDetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbVenDetalleVenta_TbVentas");
        });

        modelBuilder.Entity<TbVenEstadosVenta>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<TbVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TbVenta");

            entity.Property(e => e.Documento).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaVenta).HasColumnType("datetime");
            entity.Property(e => e.SubTotal).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.Total).HasColumnType("numeric(18, 4)");
            entity.Property(e => e.ValorIva).HasColumnType("numeric(18, 4)");

            entity.HasOne(d => d.IdEstadoVentaNavigation).WithMany(p => p.TbVenta)
                .HasForeignKey(d => d.IdEstadoVenta)
                .HasConstraintName("FK_TbVentas_TbVenEstadosVentas");

            entity.HasOne(d => d.IdUsuarioCreadorNavigation).WithMany(p => p.TbVenta)
                .HasForeignKey(d => d.IdUsuarioCreador)
                .HasConstraintName("FK_TbVentas_TbSisUsuarios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
