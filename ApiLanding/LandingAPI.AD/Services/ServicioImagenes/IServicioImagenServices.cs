using LandingAPI.AD.Models.ServicioImagenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.ServicioImagenes
{
    public interface IServicioImagenServices
    {
        Task<List<M_ServicioImagenes>> GetServicioImagenes();
        Task<M_ServicioImagenes> GetServicioImagenById(int IdServicioImagen);
        Task<int> AgregarServicioImagen(M_ServicioImagenes servicioImagenes);
        Task<int> ModificarServicioImagenes(M_ServicioImagenes servicioImagenes);
        Task<M_ServicioImagenes> GetServicioImagenByName(string Descripcion);
    }
}
