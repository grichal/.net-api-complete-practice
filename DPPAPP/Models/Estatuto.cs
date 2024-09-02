using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class Estatuto
{
    public int IdEstatuto { get; set; }

    public byte[] Estatuto1 { get; set; }

    public DateTime? Fecha { get; set; }

    public int? IdOrganizacion { get; set; }

    public virtual ICollection<OrganizacionPolitica> OrganizacionPoliticas { get; } = new List<OrganizacionPolitica>();
}
