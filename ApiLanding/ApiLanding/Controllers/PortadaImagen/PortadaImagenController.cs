using LandingAPI.AD.Models.Header;
using LandingAPI.AD.Models.PortadaImagen;
using LandingAPI.AD.Services.Header;
using LandingAPI.AD.Services.PortadaImagen;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace ApiLanding.Controllers.PortadaImagen
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortadaImagenController : ControllerBase
    {
        private readonly IPortadaImagenServices _iportadaImagen;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PortadaImagenController(IPortadaImagenServices iportadaImagen, IWebHostEnvironment webHostEnvironment)
        {
            _iportadaImagen = iportadaImagen;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetPortadaImagen")]
        public async Task<ActionResult> GetPortadaImagen()
        {
            try
            {
                var m_PortadaImagen = await _iportadaImagen.GetPortadaImagen();

                if (m_PortadaImagen != null)
                {
                    return Ok(m_PortadaImagen);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener la portada " + ex.Message);
            }
        }

        [HttpPost, Route("AgregaModificaPortadaImagen")]
        public async Task<ActionResult> AgregarModificarPortadaImagen([FromForm] M_PortadaImagen portadaImagen)
        {
            try
            {
                if (portadaImagen != null)
                {
                    M_PortadaImagen m_PortadaImagen = new();

                    m_PortadaImagen = await _iportadaImagen.GetPortadaById(portadaImagen.IdPortada);

                    if (m_PortadaImagen == null)
                    {
                        ModelState.AddModelError("Advertencia Modificar", "No se encuentra la portada a modificar");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var rowAffected = await _iportadaImagen.ModificarPortadaImagen(portadaImagen);

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
                throw new Exception("Controller: Se produjo un error al agregar/modificar la portada Imagen " + ex.Message);
            }
        }

        [HttpPost, Route("SaveHeaderImage-Portada")]
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
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "Images", "Empresa", "Portada", fileNameNew);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }

            return Ok($"Images/Empresa/Portada{fileNameNew}");
        }
    }
}
