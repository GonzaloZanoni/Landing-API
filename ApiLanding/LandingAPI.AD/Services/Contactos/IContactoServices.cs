using LandingAPI.AD.Models.Contactos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.Contactos
{
    public interface IContactoServices
    {
        Task<List<M_Contactos>> GetContactos();
        Task<int> AgregarContacto(M_Contactos contactos);
        Task<M_Contactos> GetContactoById(int idContacto);
        Task<M_Contactos> GetContactosByName(string contactoName);

        Task<int> ModificarContacto(M_Contactos contactos);
    }
}
