using LandingAPI.AD.Models.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Empresas
{
    public interface IEmpresaServices
    {
        Task<List<M_Empresas>> GetEmpresa();
        Task<M_Empresas> GetEmpresaById(int IdEmpresa);
        Task<M_Empresas> GetEmpresaByName(string empresa);
        Task<int> AgregarEmpresa(M_Empresas empresa);
        Task<int> ModificarEmpresa(M_Empresas empresa);
        Task<bool> EliminarEmpresa(int IdEmpresa);
    }
}
