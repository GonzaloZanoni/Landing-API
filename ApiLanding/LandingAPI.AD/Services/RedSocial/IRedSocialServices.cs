using LandingAPI.AD.Models.RedSocial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.RedSocial
{
    public interface IRedSocialServices
    {
        Task<List<M_RedSocial>> GetRedSocial();
        Task<M_RedSocial> GetRedSocialById(int IdRedSocial);
        Task<M_RedSocial> GetRedSocialByName(string Nombre);
        Task<int> ModificarRedSocial(M_RedSocial redSocial);
        Task<int> AgregarRedSocial(M_RedSocial redSocial);
        Task<bool> EliminarRedSocial(int IdRedSocial);
    }
}
