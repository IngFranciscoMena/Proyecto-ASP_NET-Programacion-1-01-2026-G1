using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroUsuariosApp.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int IdRol { get; set; }

    public int IdEstado { get; set; }

    public virtual EstadosUsuario IdEstadoNavigation { get; set; } = null!;

    public virtual Role IdRolNavigation { get; set; } = null!;
}
