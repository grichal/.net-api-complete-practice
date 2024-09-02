using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class Convencione
{
    public int IdConvencion { get; set; }

    public byte[] Convencion { get; set; }

    public DateTime? Fecha { get; set; }

    public int? IdOrganizacion { get; set; }

    public virtual ICollection<OrganizacionPolitica> OrganizacionPoliticas { get; } = new List<OrganizacionPolitica>();
}
