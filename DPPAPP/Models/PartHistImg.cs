using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class PartHistImg
{
    public int IdHisImg { get; set; }

    public byte[] HisImg { get; set; }

    public DateTime? Fecha { get; set; }

    public int? OrgId { get; set; }

    public virtual OrganizacionPolitica Org { get; set; }
}
