using LandingAPI.AD.Models.Footer;
using LandingAPI.AD.Models.Galeria;
using LandingAPI.AD.Services.Footer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.Footer
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterController : ControllerBase
    {
        private readonly IFooterServices _ifooterServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FooterController(IFooterServices ifooterServices, IWebHostEnvironment webHostEnvironment)
        {
            _ifooterServices = ifooterServices;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetFooter")]
        public async Task<ActionResult> GetFooter()
        {
            try
            {
                var m_Footer = await _ifooterServices.GetFooter();

                if (m_Footer != null)
                {
                    return Ok(m_Footer);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener footer " + ex.Message);
            }
        }

        [HttpPost("AgregaModificaGaleria")]
        public async Task<ActionResult> AgregarModificarFooter([FromForm] M_Footer footer)
        {
            try
            {
                if (footer != null)
                {
                    M_Footer m_Footer = new();

                    m_Footer = await _ifooterServices.GetFooterById(footer.IdFooter);

                    if (m_Footer == null)
                    {
                        ModelState.AddModelError("Advertencia Modificar", "No se encuentra la seccion Galeria a modificar");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var rowAffected = await _ifooterServices.ModificarFooter(footer);

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
                throw new Exception("Controller: Se produjo un error al agregar/modificar el footer " + ex.Message);
            }
        }
    }
}
