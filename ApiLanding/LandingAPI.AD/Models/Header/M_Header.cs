using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Models.Header
{
    public class M_Header
    {
        public int IdHeader { get; set; }
        public string RutaLogo { get; set; } = string.Empty;

        public string? Nombre { get; set; } = string.Empty;
        public string? ColorFuente { get; set; } = string.Empty;
        public string? ColorFondo { get; set; } = string.Empty;
    }
}
