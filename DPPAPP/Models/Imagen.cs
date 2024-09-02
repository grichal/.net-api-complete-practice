using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class Imagen
{
    public int IdImagen { get; set; }

    public byte[] Imgen { get; set; }

    public virtual ICollection<MiembroDesc> MiembroDescs { get; } = new List<MiembroDesc>();
}
