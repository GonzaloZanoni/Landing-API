using LandingAPI.AD.Models.ServicioImagenes;
using LandingAPI.AD.Models.Testimonios;
using LandingAPI.AD.Services.Contactos;
using LandingAPI.AD.Services.SeccionServicios;
using LandingAPI.AD.Services.ServicioImagenes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.ServicioImagenes
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioImagenesController : ControllerBase
    {
        private readonly IServicioImagenServices _iservicioImagenes;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServicioImagenesController(IServicioImagenServices iservicioImagenes, IWebHostEnvironment webHostEnvironment)
        {
            _iservicioImagenes = iservicioImagenes;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetServicioImagenes")]
        public async Task<ActionResult> GetServicioImagenes()
        {
            try
            {
                var m_ServicioImagenes = await _iservicioImagenes.GetServicioImagenes();

                if (m_ServicioImagenes != null)
                {
                    return Ok(m_ServicioImagenes);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener el contacto " + ex.Message);
            }
        }

        [HttpPost, Route("AgregaModifica/Servicio-Imagen")]
        public async Task<ActionResult> AddModifyTestimonios([FromForm] M_ServicioImagenes servicioImagenes)
        {
            try
            {
                if (servicioImagenes != null)
                {
                    M_ServicioImagenes m_ServicioImagenes = new();

                    if (servicioImagenes.IdServicioImagen == 0)
                    {
                        m_ServicioImagenes = await _iservicioImagenes.GetServicioImagenByName(servicioImagenes.Descripcion);

                        if (m_ServicioImagenes == null)
                        {
                            var Imagen = await _iservicioImagenes.AgregarServicioImagen(servicioImagenes);
                            return Ok(Imagen);
                        }
                        else
                        {
                            ModelState.AddModelError("Advertencia Alta", "La imagen con esa descripción ya existe!");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        m_ServicioImagenes = await _iservicioImagenes.GetServicioImagenById(servicioImagenes.IdServicioImagen);

                        if (m_ServicioImagenes == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra la imagen a modificar");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _iservicioImagenes.ModificarServicioImagenes(servicioImagenes);

                            if (rowAffected != 0)
                            {
                                return Ok(rowAffected);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al agregar/modificar una imagen " + ex.Message);
            }
        }
    }
}
