using System;
using System.Collections.Generic;

namespace DPPAPP.Models
{
    public partial class AsambleaDTO
    {
        public string Asambleas { get; set; } = null!;

        public DateTime? Fecha { get; set; }

        public int? IdOrganizacion { get; set; }
    }
}
