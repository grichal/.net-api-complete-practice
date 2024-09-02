using System;
using System.Collections.Generic;

namespace DPPAPP.Models
{
    public partial class CreateMember
    {
        public int Organizacion_id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public int Edad { get; set; }

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public int Cargo_Id { get; set; }

        public string Provincia { get; set; } = null!;

        public string Municipio { get; set; } = null!;

        public DateTime? Fecha_designacion { get; set; } = null!;

        public string Genero { get; set; } = null!;

        public int Tituloadesc { get; set; }

        public string Cedula { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int? imagen { get; set; }

    }
}