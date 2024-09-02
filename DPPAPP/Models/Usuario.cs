using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class Usuario
{
    public int IdUsiario { get; set; }

    public string NombreUsuario { get; set; }

    public string Clave { get; set; }

    public int? UserRole { get; set; }

    public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; } = new List<HistorialRefreshToken>();

    public virtual Role UserRoleNavigation { get; set; }
}
