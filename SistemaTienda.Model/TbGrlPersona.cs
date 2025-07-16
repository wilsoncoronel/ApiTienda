using System;
using System.Collections.Generic;

namespace SistemaTienda.Model;

public partial class TbGrlPersona
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public int IdTipoIdentificacion { get; set; }

    public virtual TbGrlTipoIdentificacion IdTipoIdentificacionNavigation { get; set; } = null!;

    public virtual ICollection<TbComCliente> TbComClientes { get; set; } = new List<TbComCliente>();

    public virtual ICollection<TbComProveedores> TbComProveedores { get; set; } = new List<TbComProveedores>();

    public virtual TbGrlDirecciones? TbGrlDireccione { get; set; }

    public virtual ICollection<TbSisUsuario> TbSisUsuarios { get; set; } = new List<TbSisUsuario>();
}
