using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class TituloAbreviado
{
    public int Id { get; set; }

    public string TituloaDesc { get; set; }

    public virtual ICollection<MiembroDesc> MiembroDescs { get; } = new List<MiembroDesc>();
}
