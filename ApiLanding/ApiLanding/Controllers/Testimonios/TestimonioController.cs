using LandingAPI.AD.Models.PortadaImagen;
using LandingAPI.AD.Models.Testimonios;
using LandingAPI.AD.Services.PortadaImagen;
using LandingAPI.AD.Services.Testimonios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.Testimonios
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonioController : ControllerBase
    {
        private readonly ITestimonioServices _itestimonio;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TestimonioController(ITestimonioServices itestimonio, IWebHostEnvironment webHostEnvironment)
        {
            _itestimonio = itestimonio;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost, Route("AgregaModificaTestimonio")]
        public async Task<ActionResult> AddModifyTestimonios([FromForm] M_Testimonio testimonio)
        {
            try
            {
                if (testimonio != null)
                {
                    M_Testimonio m_Testimonio = new();

                    if (testimonio.IdTestimonio == 0)
                    {
                        m_Testimonio = await _itestimonio.GetTestimonioByName(testimonio.NombreCliente);

                        if (m_Testimonio == null)
                        {
                            var Testimonio = await _itestimonio.AgregarTestimonio(testimonio);
                            return Ok(Testimonio);
                        }
                        else
                        {
                            ModelState.AddModelError("Advertencia Alta", "El testimonio con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        m_Testimonio = await _itestimonio.GetTestimonioById(testimonio.IdTestimonio);

                        if (m_Testimonio == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra el testimonio a modificar");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _itestimonio.ModificarTestimonio(testimonio);

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
                throw new Exception("Controller: Se produjo un error al agregar/modificar un testimonio " + ex.Message);
            }
        }

        [HttpGet, Route("GetTestimonio")]
        public async Task<ActionResult> GetTestimonio()
        {
            try
            {
                var m_Testimonios = await _itestimonio.GetTestimonio();

                if (m_Testimonios != null)
                {
                    return Ok(m_Testimonios);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener los testimonios " + ex.Message);
            }
        }

        [HttpPost, Route("SaveHeaderImage-Testimonios\"")]
        public async Task<ActionResult> SaveHeaderImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest();
            }

            string fileName = image.FileName;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string Extension = Path.GetExtension(fileName);
            string[] allow = { ".jpg", ".jpeg", ".png" };

            if (!allow.Contains(Extension.ToLower()))
            {
                return BadRequest();
            }

            string fileNameNew = $"{Guid.NewGuid()}-{fileNameWithoutExtension}{Extension}";
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "Images", "Empresa", "Testimonios", fileNameNew);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }

            return Ok($"Images/Empresa/Testimonios{fileNameNew}");
        }

    }
}
