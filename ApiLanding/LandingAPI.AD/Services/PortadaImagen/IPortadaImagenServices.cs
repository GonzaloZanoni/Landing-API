using LandingAPI.AD.Models.PortadaImagen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.PortadaImagen
{
    public interface IPortadaImagenServices
    {
        Task<List<M_PortadaImagen>> GetPortadaImagen();
        Task<M_PortadaImagen> GetPortadaById(int IdPortada);
        Task<int> AgregarPortadaImagen(M_PortadaImagen portadaImagen);
        Task<int> ModificarPortadaImagen(M_PortadaImagen portadaImagen);
    }
}
