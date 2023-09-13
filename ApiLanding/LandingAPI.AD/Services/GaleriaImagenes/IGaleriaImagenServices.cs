using LandingAPI.AD.Models.GaleriaImagenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.GaleriaImagenes
{
    public interface IGaleriaImagenServices
    {
        Task<List<M_GaleriaImagen>> GetGaleriaImagenes();
        Task<M_GaleriaImagen> GetGaleriaImagenById(int IdGaleriaImagen);
        Task<M_GaleriaImagen> GetGaleriaImagenByName(string Descripcion);
        Task<int> ModificarGaleriaImagen(M_GaleriaImagen galeriaImagen);
        Task<int> AgregarGaleriaImagen(M_GaleriaImagen galeriaImagen);

        Task<bool> EliminarGaleriaImagen(int idImagen);
    }
}
