# SistemaTienda API Documentation

Base URL: `https://localhost:7205/api`

All endpoints return a JSON response in the following format:
```json
{
  "status": true,
  "msg": "message",
  "Value": { }
}
```

---

## ArticuloController

### GET /api/Articulo/CargarListaTiposArticulos
Get list of article types.

**Response:** `List<TipoArticuloDTO>`

### GET /api/Articulo/CargarListaImpuestosArticulos
Get list of article taxes.

**Response:** `List<ImpuestoArticuloDTO>`

### GET /api/Articulo/CargarListaMarcasArticulos
Get list of brands.

**Response:** `List<MarcaDTO>`

### GET /api/Articulo/ListaArticulos
Get articles filtered by date range.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| fechaInicial | DateTime | Start date |
| fechaFinal | DateTime | End date |

**Response:** `List<ArticuloDTO>`

### GET /api/Articulo/ListarTodosArticulos
Get all articles.

**Response:** `List<ArticuloDTO>`

### POST /api/Articulo/CrearArticulo
Create a new article.

**Body:** `ArticuloCreacionDTO`

**Response:** `int` (created article ID)

### POST /api/Articulo/CrearArticulosLista
Create multiple articles at once.

**Body:** `List<ArticuloCreacionDTO>`

**Response:** `bool`

### PUT /api/Articulo/EditarArticulo
Update an article.

**Body:** `ArticuloEdicionDTO`

**Response:** `bool`

### PUT /api/Articulo/DesactivarArticulo
Deactivate an article.

**Body:** `int` (article ID)

**Response:** `bool`

---

## ClienteController

### GET /api/Cliente/BuscarClienteCI
Search client by identification number.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| identificacion | string | Client identification |

**Response:** `ClienteDTO`

### POST /api/Cliente/CrearCliente
Create a new client.

**Body:** `ClienteCreacionDTO`

**Response:** `int` (created client ID)

### GET /api/Cliente/ListarTiposIdentificacion
Get list of identification types.

**Response:** `List<TipoIdentificacionDTO>`

### GET /api/Cliente/ListarClientes
Get all clients.

**Response:** `List<ClienteDTO>`

### GET /api/Cliente/ListarCiudades
Get list of cities.

**Response:** `List<CiudadDTO>`

### PUT /api/Cliente/EditarCliente
Update a client.

**Body:** `ClienteEditarDTO`

**Response:** `bool`

---

## ProveedorController

### GET /api/Proveedor/BuscarProveedorCI
Search supplier by identification number.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| identificacion | string | Supplier identification |

**Response:** `ProveedorDTO`

### POST /api/Proveedor/CrearProveedor
Create a new supplier.

**Body:** `ProveedorCreacionDTO`

**Response:** `int` (created supplier ID)

### GET /api/Proveedor/ListarProveedores
Get all suppliers.

**Response:** `List<ProveedorDTO>`

### GET /api/Proveedor/ListarCiudades
Get list of cities.

**Response:** `List<CiudadDTO>`

### GET /api/Proveedor/ListarTiposIdentificacion
Get list of identification types.

**Response:** `List<TipoIdentificacionDTO>`

### PUT /api/Proveedor/EditarProveedor
Update a supplier.

**Body:** `ProveedorEditarDTO`

**Response:** `bool`

---

## InventarioController

### GET /api/Inventario/ExistenciasInventario
Get current stock/existences.

**Response:** `List<ExistenciaDTO>`

### GET /api/Inventario/ListaTransaccionesInventario
Get inventory transaction list.

**Response:** `List<TransaccionInventarioDTO>`

### GET /api/Inventario/ListaInventario
Get inventory filtered by date range.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| fechaInicio | DateOnly | Start date |
| fechaFin | DateOnly | End date |

**Response:** `List<InventarioDTO>`

### GET /api/Inventario/ListaDetallesInventario
Get inventory details by inventory ID.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| IdInventario | int | Inventory ID |

**Response:** `List<DetalleInventarioDTO>`

### GET /api/Inventario/ResumenVentasDiario
Get daily sales summary.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| fechaResumen | DateOnly | Summary date |

**Response:** `List<ResumenVentasDiarioDTO>`

### GET /api/Inventario/ResumenVentasMensual
Get monthly sales summary.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| fechaResumen | DateOnly | Summary date |

**Response:** `List<ResumenVentasDiarioDTO>`

---

## VentasController

### POST /api/Ventas/RegistrarVenta
Register a new sale.

**Body:** `VentaCreacionDTO`

**Response:** `int` (sale ID)

### PUT /api/Ventas/EditarVenta
Update a sale.

**Body:** `VentaEditarDTO`

**Response:** `bool`

### GET /api/Ventas/ListarEstadosVenta
Get list of sale statuses.

**Response:** `List<EstadoVentaDTO>`

### GET /api/Ventas/ListarVentas
Get sales filtered by date range.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| fechaInicial | DateOnly | Start date |
| fechaFinal | DateOnly | End date |

**Response:** `List<VentaMinDTO>`

### GET /api/Ventas/ObtenerVenta
Get a specific sale by ID.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| idVenta | int | Sale ID |

**Response:** `VentaDTO`

---

## ComprasController

### POST /api/Compras/RegistrarCompra
Register a new purchase.

**Body:** `CompraCreacionDTO`

**Response:** `int` (purchase ID)

### PUT /api/Compras/EditarCompra
Update a purchase.

**Body:** `CompraEditarDTO`

**Response:** `bool`

### GET /api/Compras/ListarEstadosCompra
Get list of purchase statuses.

**Response:** `List<EstadoCompraDTO>`

### GET /api/Compras/ListarCompras
Get purchases filtered by date range.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| fechaInicial | DateOnly | Start date |
| fechaFinal | DateOnly | End date |

**Response:** `List<CompraMinDTO>`

### GET /api/Compras/ObtenerCompra
Get a specific purchase by ID.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| idCompra | int | Purchase ID |

**Response:** `CompraDTO`

### GET /api/Compras/ReversarCompra
Reverse a purchase.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| idCompra | int | Purchase ID |

**Response:** `bool`

---

## UsuarioController

### POST /api/Usuario/CrearUsuario
Create a new user.

**Body:** `UsuarioCreacionDTO`

**Response:** `int` (user ID)

### GET /api/Usuario/ListarUsuarios
Get all users.

**Response:** `List<UsuarioDTO>`

### GET /api/Usuario/ListarUsuarioId
Get a user by ID.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| IdUsuario | int | User ID |

**Response:** `UsuarioDTO`

### PUT /api/Usuario/EditarUsuario
Update a user.

**Body:** `UsuarioEditarDTO`

**Response:** `int`

---

## LoginController

### GET /api/Login/IniciarSesion
Authenticate user.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| usuario | string | Username |
| password | string | Password |

**Response:** `List<PermisosRolDTO>`

### GET /api/Login/ExtraerSesion
Get session profile by username.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| usuario | string | Username |

**Response:** `SesionDTO`

### GET /api/Login/ObtenerPerfil
Get user profile by ID.

**Query Parameters:**
| Parameter | Type | Description |
|-----------|------|-------------|
| id | int | User ID |

**Response:** `UsuarioDTO`

---

## ConfiguracionesController

### GET /api/Configuraciones/ListarMarcas
Get all brands.

**Response:** `List<MarcaDTO>`

### POST /api/Configuraciones/CrearMarca
Create a new brand.

**Body:** `MarcaCreacionDTO`

**Response:** `int` (brand ID)

### PUT /api/Configuraciones/EditarMarca
Update a brand.

**Body:** `MarcaEditarDTO`

**Response:** `bool`

### GET /api/Configuraciones/ListarTiposArticulos
Get all article types.

**Response:** `List<TipoArticuloDTO>`

### POST /api/Configuraciones/CrearTipoArticulo
Create a new article type.

**Body:** `TipoArticuloCreacionDTO`

**Response:** `int` (article type ID)

### PUT /api/Configuraciones/EditarTipoArticulo
Update an article type.

**Body:** `TipoArticuloEditarDTO`

**Response:** `bool`

### GET /api/Configuraciones/ListarImpuestosArticulos
Get all article taxes.

**Response:** `List<ImpuestoArticuloDTO>`

### GET /api/Configuraciones/ListarEstados
Get list of statuses.

**Response:** `List<EstadoImpuestoDTO>`

### POST /api/Configuraciones/CrearImpuesto
Create a new tax.

**Body:** `ImpuestoArticuloCreacionDTO`

**Response:** `int` (tax ID)

### PUT /api/Configuraciones/EditarImpuesto
Update a tax.

**Body:** `ImpuestoArticuloEditarDTO`

**Response:** `bool`
