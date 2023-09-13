using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Models.Empresas
{
    public class M_Empresas
    {
        public int IdEmpresa { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }   
        public string? Telefono { get; set; }
        public string? Localidad { get; set; }
        public string? Email { get; set; }
        public string? Descripcion { get; set; }
        public string? RazonSocial { get; set; }


    }
}
