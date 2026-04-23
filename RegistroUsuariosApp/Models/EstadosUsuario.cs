using System;
using System.Collections.Generic;

namespace RegistroUsuariosApp.Models;

public partial class EstadosUsuario
{
    public int IdEstado { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
