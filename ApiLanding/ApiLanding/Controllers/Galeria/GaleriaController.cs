using LandingAPI.AD.Models.Galeria;
using LandingAPI.AD.Models.SeccionServicios;
using LandingAPI.AD.Services.Galeria;
using LandingAPI.AD.Services.SeccionServicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.Galeria
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaleriaController : ControllerBase
    {
        private readonly IGaleriaServices _igaleriaServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GaleriaController(IGaleriaServices igaleriaServices, IWebHostEnvironment webHostEnvironment)
        {
            _igaleriaServices = igaleriaServices;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetGaleria")]
        public async Task<ActionResult> GetGaleria()
        {
            try
            {
                var m_Galeria = await _igaleriaServices.GetGaleria();

                if (m_Galeria != null)
                {
                    return Ok(m_Galeria);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener galeria " + ex.Message);
            }
        }

        [HttpPost, Route("AgregaModificaGaleria")]
        public async Task<ActionResult> AgregarModificarGaleria([FromForm] M_Galeria galeria)
        {
            try
            {
                if (galeria != null)
                {
                    M_Galeria m_Galeria = new();

                    m_Galeria = await _igaleriaServices.GetGaleriaById(galeria.IdGaleria);

                    if (m_Galeria == null)
                    {
                        ModelState.AddModelError("Advertencia Modificar", "No se encuentra la seccion Galeria a modificar");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var rowAffected = await _igaleriaServices.ModificarGaleria(galeria);

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
