using LandingAPI.AD.Models.Footer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Footer
{
    public interface IFooterServices
    {
        Task<List<M_Footer>> GetFooter();
        Task<M_Footer> GetFooterById(int IdFooter);
        Task<int>AgregarFooter(M_Footer footer);
        Task<int>ModificarFooter(M_Footer footer);
        
    }
}
