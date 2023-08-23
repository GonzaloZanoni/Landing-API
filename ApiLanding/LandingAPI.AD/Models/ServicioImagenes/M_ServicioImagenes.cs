using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Models.ServicioImagenes
{
    public class M_ServicioImagenes
    {
        public int IdServicioImagen { get; set; }
        public string? Descripcion { get; set; }
        public string? RutaImagen { get; set; }
        public int IdSeccionServicio { get; set; }

        //JOIN
        //public string Titulo { get; set; } = string.Empty;
    }
}
