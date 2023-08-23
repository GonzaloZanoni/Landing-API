using LandingAPI.AD.Models.Testimonios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Testimonios
{
    public interface ITestimonioServices
    {
        Task<List<M_Testimonio>> GetTestimonio();
        Task<M_Testimonio> GetTestimonioById(int IdTestimonio);

        Task<int> ModificarTestimonio(M_Testimonio testimonio);

        Task<int> AgregarTestimonio(M_Testimonio testimonio);
        Task<M_Testimonio> GetTestimonioByName(string? nombreCliente);
    }
}
