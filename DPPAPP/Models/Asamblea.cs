using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class Asamblea
{
    public int IdAsamblea { get; set; }

    public byte[] Asambleas { get; set; }

    public DateTime? Fecha { get; set; }

    public int? IdOrganizacion { get; set; }

    public virtual ICollection<OrganizacionPolitica> OrganizacionPoliticas { get; } = new List<OrganizacionPolitica>();
}
