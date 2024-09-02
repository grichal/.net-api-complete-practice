using System;
using System.Collections.Generic;

namespace DPPAPP.Models;

public partial class MiembrosDeOrganizacionView
{
    public int IdMiembro { get; set; }

    public string TituloaDesc { get; set; }

    public string NombreMiembro { get; set; }

    public string ApellidoMiembro { get; set; }

    public int? Edad { get; set; }

    public string Direccion { get; set; }

    public string Telefono { get; set; }

    public string Municipio { get; set; }

    public string Provincia { get; set; }

    public DateTime? FechaDesignacion { get; set; }

    public string Genero { get; set; }

    public int? Tituloadesc1 { get; set; }

    public string Cedula { get; set; }

    public string Email { get; set; }

    public int? Isactive { get; set; }

    public int? Imagen { get; set; }

    public byte[] Imgen { get; set; }

    public string NombreOrganizacion { get; set; }

    public int Id { get; set; }

    public string Acronimo { get; set; }

    public string DireccionDeSede { get; set; }

    public string MunicipioDeSede { get; set; }

    public string ProvinciaDeSede { get; set; }

    public string TelefonoDeSede { get; set; }

    public string EmailOrganizacion { get; set; }

    public string Logo { get; set; }

    public int CargoId { get; set; }

    public string Cargo { get; set; }
}
