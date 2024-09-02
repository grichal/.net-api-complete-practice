using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class Role
{
    public int UserRoleId { get; set; }

    public string UserRole { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
