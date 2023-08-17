using LandingAPI.AD.Models.Header;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Header
{
    public interface IHeaderServices
    {
        Task<int> ModificarHeader(M_Header header);
        Task<int> AgregarHeader(M_Header header);
        Task<List<M_Header>> GetHeader();

        Task<M_Header> GetHeaderById(int IdHeader);
    }
}
