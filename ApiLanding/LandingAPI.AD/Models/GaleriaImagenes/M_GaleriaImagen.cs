using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Models.GaleriaImagenes
{
    public class M_GaleriaImagen
    {
        public int IdGaleriaImagen { get; set;}
        public int IdGaleria { get; set;}
        public string? RutaImagen { get; set;}
        public string? Descripcion { get; set;}
    }
}
