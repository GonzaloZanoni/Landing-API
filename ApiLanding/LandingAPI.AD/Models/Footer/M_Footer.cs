using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Models.Footer
{
    public class M_Footer
    {
        public int IdFooter { get; set; }
        public string? DescripcionDireccion { get; set; }
        public string? RutaDireccion { get; set; }
        public string? Telefono { get; set; }
        public string? RutaTelefono { get; set;}
        public string? DescripcionEmail { get; set; }

        public string? RutaEmail { get;set; }
        public string? Localidad { get; set; }

    }
}
