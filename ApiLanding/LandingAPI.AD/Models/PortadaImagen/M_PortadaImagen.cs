using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Models.PortadaImagen
{
    public class M_PortadaImagen
    {
        public int IdPortada { get; set; }
        public string? Titulo { get; set; } = string.Empty;

        public string? RutaImagen { get; set; } = string.Empty;
        public string? ColorFuente { get; set; } = string.Empty;
        
    }
}
