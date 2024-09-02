using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class MiembroDesc
{
    public int IdMiembro { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public int? Edad { get; set; }

    public string Direccion { get; set; }

    public string Telefono { get; set; }

    public int? CargoId { get; set; }

    public string Provincia { get; set; }

    public string Municipio { get; set; }

    public DateTime? FechaDesignacion { get; set; }

    public string Genero { get; set; }

    public int? Tituloadesc { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public string Cedula { get; set; }

    public DateTime? FechaInsercion { get; set; }

    public string Email { get; set; }

    public int? Isactive { get; set; }

    public int? Imagen { get; set; }

    public virtual Cargo Cargo { get; set; }

    public virtual Imagen ImagenNavigation { get; set; }

    public virtual ICollection<MiembrosDeOrganizacion> MiembrosDeOrganizacions { get; } = new List<MiembrosDeOrganizacion>();

    public virtual TituloAbreviado TituloadescNavigation { get; set; }
}
