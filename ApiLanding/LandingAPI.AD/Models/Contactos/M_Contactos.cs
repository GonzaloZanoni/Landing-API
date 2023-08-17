using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Models.Contactos
{
    public class M_Contactos
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public string? Telefono { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;

        public string Mensaje { get; set; } = string.Empty;
    }
}
