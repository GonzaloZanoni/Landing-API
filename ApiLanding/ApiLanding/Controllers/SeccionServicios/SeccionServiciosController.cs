using LandingAPI.AD.Models.PortadaImagen;
using LandingAPI.AD.Models.SeccionServicios;
using LandingAPI.AD.Services.PortadaImagen;
using LandingAPI.AD.Services.SeccionServicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.SeccionServicios
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionServiciosController : ControllerBase
    {
        private readonly ISeccionServicioServices _iseccionServicios;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SeccionServiciosController(ISeccionServicioServices iseccionServicios, IWebHostEnvironment webHostEnvironment)
        {
            _iseccionServicios = iseccionServicios;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetSeccionServicios")]
        public async Task<ActionResult> GetSeccionServicios()
        {
            try
            {
                var m_SeccionServicios = await _iseccionServicios.GetSeccionServicios();

                if (m_SeccionServicios != null)
                {
                    return Ok(m_SeccionServicios);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener seccion servicios " + ex.Message);
            }
        }

        [HttpPost, Route("AgregaModificaPortadaImagen")]
        public async Task<ActionResult> AgregarModificarSeccionServicios([FromForm] M_SeccionServicios seccionServicios)
        {
            try
            {
                if (seccionServicios != null)
                {
                    M_SeccionServicios m_SeccionServicios = new();

                    m_SeccionServicios = await _iseccionServicios.GetSeccionServiciosById(seccionServicios.IdSeccionServicio);

                    if (m_SeccionServicios == null)
                    {
                        ModelState.AddModelError("Advertencia Modificar", "No se encuentra la seccion servicio a modificar");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var rowAffected = await _iseccionServicios.ModificarSeccionServicio(seccionServicios);

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
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al agregar/modificar la seccion servicio " + ex.Message);
            }
        }
    }
}
