using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class MiembrosDeOrganizacion
{
    public int IdMiembro { get; set; }

    public int? MiembroDesc { get; set; }

    public int? OrganizacionId { get; set; }

    public virtual MiembroDesc MiembroDescNavigation { get; set; }

    public virtual OrganizacionPolitica Organizacion { get; set; }
}
