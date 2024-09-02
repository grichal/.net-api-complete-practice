using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class OrganizacionPolitica
{
    public int Id { get; set; }

    public string Tipo { get; set; }

    public string Nombre { get; set; }

    public string Acronimo { get; set; }

    public string DireccionDeSede { get; set; }

    public string MunicipioDeSede { get; set; }

    public string ProvinciaDeSede { get; set; }

    public string TelefonoDeSede { get; set; }

    public string Email { get; set; }

    public string Logo { get; set; }

    public int? Position { get; set; }

    public string Website { get; set; }

    public DateTime? AnoReconocimiento { get; set; }

    public string NoActa { get; set; }

    public string NoResolucion { get; set; }

    public int? Estatutos { get; set; }

    public int? Convenciones { get; set; }

    public int? Asambleas { get; set; }

    public int? Escano { get; set; }

    public virtual Asamblea AsambleasNavigation { get; set; }

    public virtual Convencione ConvencionesNavigation { get; set; }

    public virtual Estatuto EstatutosNavigation { get; set; }

    public virtual ICollection<MiembrosDeOrganizacion> MiembrosDeOrganizacions { get; set; } = new List<MiembrosDeOrganizacion>();

    public virtual ICollection<PartHistImg> PartHistImgs { get; set; } = new List<PartHistImg>();
}
