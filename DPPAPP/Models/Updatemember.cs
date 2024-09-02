using System;
using System.Collections.Generic;

namespace DPPAPP.Models
{
    public partial class Updatemember
    {
        public string Nombremiembro { get; set; } = null!;

        public string ApellidoMiembro { get; set; } = null!;

        public int Edad { get; set; }

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Provincia { get; set; } = null!;

        public string Municipio { get; set; } = null!;

        public int OrganizacionId { get; set; }

        public int Cargo_Id { get; set; }

        public DateTime? Fecha_designacion { get; set; }

        public string Genero { get; set; } = null!;

        public int Tituloadesc { get; set; }

        public string Cedula { get; set; } = null!;

        public string Email { get; set; } = null!;

    }
}
