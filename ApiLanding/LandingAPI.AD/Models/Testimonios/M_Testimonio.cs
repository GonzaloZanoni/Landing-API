using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Models.Testimonios
{
    public class M_Testimonio
    {
        public int IdTestimonio { get; set; }
        public string? NombreCliente { get; set; }

        public string? RutaImagen { get; set; }
        public string? Parrafo { get; set; }
        public string? Titulo { get; set;}

        public bool Activo { get; set; }
    }
}
