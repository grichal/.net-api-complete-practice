using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class Cargo
{
    public int CargoId { get; set; }

    public string Descripcion { get; set; }

    public virtual ICollection<MiembroDesc> MiembroDescs { get; } = new List<MiembroDesc>();
}
