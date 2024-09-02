using System;
using System.Buffers.Text;
using System.Collections.Generic;

namespace DPPAPP.Models
{

    public partial class EstatutoDTO 
    {
        public string Estatuto1 { get; set; } = null!;

        public DateTime? Fecha { get; set; }

        public int? IdOrganizacion { get; set; }
    }
}
