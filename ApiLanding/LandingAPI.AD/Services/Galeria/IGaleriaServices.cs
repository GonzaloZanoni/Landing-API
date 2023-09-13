using LandingAPI.AD.Models.Galeria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Galeria
{
    public interface IGaleriaServices
    {
        Task<List<M_Galeria>> GetGaleria();
        Task<M_Galeria> GetGaleriaById(int IdGaleria);
        Task<int> AgregarGaleria(M_Galeria galeria);
        Task<int> ModificarGaleria(M_Galeria galeria);
    }
}
