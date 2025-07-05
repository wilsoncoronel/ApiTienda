using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaTienda.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MigracionDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbCiudades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCiudades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbEstadosCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbEstadosCompras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbEstadosImpuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbEstadosImpuestos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbEstadosPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbEstadosPedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbEstadosVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbEstadosVentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbMarcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbMarcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbPersonas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPersonas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbRol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbTiposArticulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbTiposArticulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbTransaccionesInventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Signo = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbTransaccionesInventario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbImpuestosArticulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorImpuesto = table.Column<double>(type: "float", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbImpuestosArticulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbImpuestosArticulos_TbEstadosImpuestos_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "TbEstadosImpuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbClientes_TbPersonas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "TbPersonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbDirecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    IdCiudad = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDirecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbDirecciones_TbCiudades_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "TbCiudades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbDirecciones_TbPersonas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "TbPersonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbProveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbProveedores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbProveedores_TbPersonas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "TbPersonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbPermisosRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    IdMenu = table.Column<int>(type: "int", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPermisosRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbPermisosRol_TbMenus_IdMenu",
                        column: x => x.IdMenu,
                        principalTable: "TbMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbPermisosRol_TbRol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "TbRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbUsuarios_TbPersonas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "TbPersonas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbUsuarios_TbRol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "TbRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbArticulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCaducidad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Unidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnidadValor = table.Column<double>(type: "float", nullable: false),
                    IdMarca = table.Column<int>(type: "int", nullable: false),
                    IdTipoArticulo = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioCreador = table.Column<int>(type: "int", nullable: false),
                    IdImpuesto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbArticulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbArticulos_TbImpuestosArticulos_IdImpuesto",
                        column: x => x.IdImpuesto,
                        principalTable: "TbImpuestosArticulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbArticulos_TbMarcas_IdMarca",
                        column: x => x.IdMarca,
                        principalTable: "TbMarcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbArticulos_TbTiposArticulos_IdTipoArticulo",
                        column: x => x.IdTipoArticulo,
                        principalTable: "TbTiposArticulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbArticulos_TbUsuarios_IdUsuarioCreador",
                        column: x => x.IdUsuarioCreador,
                        principalTable: "TbUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false),
                    ValorIva = table.Column<double>(type: "float", nullable: false),
                    SubTotal = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    IdUsuarioCreador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbCompras_TbEstadosCompras_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "TbEstadosCompras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbCompras_TbProveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "TbProveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbCompras_TbUsuarios_IdUsuarioCreador",
                        column: x => x.IdUsuarioCreador,
                        principalTable: "TbUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    IdEstadoPedido = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbPedidos_TbEstadosPedidos_IdEstadoPedido",
                        column: x => x.IdEstadoPedido,
                        principalTable: "TbEstadosPedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbPedidos_TbProveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "TbProveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbPedidos_TbUsuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "TbUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    EstadoVisual = table.Column<bool>(type: "bit", nullable: false),
                    ValorIva = table.Column<double>(type: "float", nullable: false),
                    SubTotal = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbVentas_TbClientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "TbClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbVentas_TbEstadosVentas_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "TbEstadosVentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbVentas_TbUsuarios_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "TbUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbInventario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrecioUnitario = table.Column<double>(type: "float", nullable: false),
                    IdTransaccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbInventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbInventario_TbArticulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "TbArticulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbInventario_TbTransaccionesInventario_IdTransaccion",
                        column: x => x.IdTransaccion,
                        principalTable: "TbTransaccionesInventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbDetallesCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ValorCompra = table.Column<double>(type: "float", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<double>(type: "float", nullable: false),
                    ImpuestoValor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDetallesCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbDetallesCompras_TbArticulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "TbArticulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbDetallesCompras_TbCompras_IdCompra",
                        column: x => x.IdCompra,
                        principalTable: "TbCompras",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TbDetallesPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ValorCompra = table.Column<double>(type: "float", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<double>(type: "float", nullable: false),
                    ImpuestoValor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDetallesPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbDetallesPedidos_TbArticulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "TbArticulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbDetallesPedidos_TbPedidos_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "TbPedidos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TbDetallesVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVenta = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ValorCompra = table.Column<double>(type: "float", nullable: false),
                    IdArticulo = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<double>(type: "float", nullable: false),
                    ImpuestoValor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDetallesVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbDetallesVentas_TbArticulos_IdArticulo",
                        column: x => x.IdArticulo,
                        principalTable: "TbArticulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbDetallesVentas_TbVentas_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "TbVentas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbArticulos_IdImpuesto",
                table: "TbArticulos",
                column: "IdImpuesto");

            migrationBuilder.CreateIndex(
                name: "IX_TbArticulos_IdMarca",
                table: "TbArticulos",
                column: "IdMarca");

            migrationBuilder.CreateIndex(
                name: "IX_TbArticulos_IdTipoArticulo",
                table: "TbArticulos",
                column: "IdTipoArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_TbArticulos_IdUsuarioCreador",
                table: "TbArticulos",
                column: "IdUsuarioCreador");

            migrationBuilder.CreateIndex(
                name: "IX_TbClientes_IdPersona",
                table: "TbClientes",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_TbCompras_IdEstado",
                table: "TbCompras",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_TbCompras_IdProveedor",
                table: "TbCompras",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_TbCompras_IdUsuarioCreador",
                table: "TbCompras",
                column: "IdUsuarioCreador");

            migrationBuilder.CreateIndex(
                name: "IX_TbDetallesCompras_IdArticulo",
                table: "TbDetallesCompras",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_TbDetallesCompras_IdCompra",
                table: "TbDetallesCompras",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_TbDetallesPedidos_ArticuloId",
                table: "TbDetallesPedidos",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_TbDetallesPedidos_IdPedido",
                table: "TbDetallesPedidos",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_TbDetallesVentas_IdArticulo",
                table: "TbDetallesVentas",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_TbDetallesVentas_IdVenta",
                table: "TbDetallesVentas",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_TbDirecciones_IdCiudad",
                table: "TbDirecciones",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_TbDirecciones_IdPersona",
                table: "TbDirecciones",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbImpuestosArticulos_IdEstado",
                table: "TbImpuestosArticulos",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_TbInventario_IdArticulo",
                table: "TbInventario",
                column: "IdArticulo");

            migrationBuilder.CreateIndex(
                name: "IX_TbInventario_IdTransaccion",
                table: "TbInventario",
                column: "IdTransaccion");

            migrationBuilder.CreateIndex(
                name: "IX_TbPedidos_IdEstadoPedido",
                table: "TbPedidos",
                column: "IdEstadoPedido");

            migrationBuilder.CreateIndex(
                name: "IX_TbPedidos_IdProveedor",
                table: "TbPedidos",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_TbPedidos_UsuarioId",
                table: "TbPedidos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TbPermisosRol_IdMenu",
                table: "TbPermisosRol",
                column: "IdMenu");

            migrationBuilder.CreateIndex(
                name: "IX_TbPermisosRol_IdRol",
                table: "TbPermisosRol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_TbProveedores_IdPersona",
                table: "TbProveedores",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_TbUsuarios_IdPersona",
                table: "TbUsuarios",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_TbUsuarios_IdRol",
                table: "TbUsuarios",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_TbVentas_IdCliente",
                table: "TbVentas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_TbVentas_IdEstado",
                table: "TbVentas",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_TbVentas_UsuarioCreadorId",
                table: "TbVentas",
                column: "UsuarioCreadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbDetallesCompras");

            migrationBuilder.DropTable(
                name: "TbDetallesPedidos");

            migrationBuilder.DropTable(
                name: "TbDetallesVentas");

            migrationBuilder.DropTable(
                name: "TbDirecciones");

            migrationBuilder.DropTable(
                name: "TbInventario");

            migrationBuilder.DropTable(
                name: "TbPermisosRol");

            migrationBuilder.DropTable(
                name: "TbCompras");

            migrationBuilder.DropTable(
                name: "TbPedidos");

            migrationBuilder.DropTable(
                name: "TbVentas");

            migrationBuilder.DropTable(
                name: "TbCiudades");

            migrationBuilder.DropTable(
                name: "TbArticulos");

            migrationBuilder.DropTable(
                name: "TbTransaccionesInventario");

            migrationBuilder.DropTable(
                name: "TbMenus");

            migrationBuilder.DropTable(
                name: "TbEstadosCompras");

            migrationBuilder.DropTable(
                name: "TbEstadosPedidos");

            migrationBuilder.DropTable(
                name: "TbProveedores");

            migrationBuilder.DropTable(
                name: "TbClientes");

            migrationBuilder.DropTable(
                name: "TbEstadosVentas");

            migrationBuilder.DropTable(
                name: "TbImpuestosArticulos");

            migrationBuilder.DropTable(
                name: "TbMarcas");

            migrationBuilder.DropTable(
                name: "TbTiposArticulos");

            migrationBuilder.DropTable(
                name: "TbUsuarios");

            migrationBuilder.DropTable(
                name: "TbEstadosImpuestos");

            migrationBuilder.DropTable(
                name: "TbPersonas");

            migrationBuilder.DropTable(
                name: "TbRol");
        }
    }
}
