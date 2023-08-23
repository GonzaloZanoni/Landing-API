using LandingAPI.AD.Models.SeccionServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingAPI.AD.Services.SeccionServicios
{
    public interface ISeccionServicioServices
    {
        Task<List<M_SeccionServicios>> GetSeccionServicios();
        Task<int> ModificarSeccionServicio(M_SeccionServicios seccionServicios);
        Task<int> AgregarSeccionServicio(M_SeccionServicios seccionServicios);
        Task<M_SeccionServicios> GetSeccionServiciosById(int idSeccionServicio);
    }
}
