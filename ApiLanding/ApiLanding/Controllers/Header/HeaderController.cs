using LandingAPI.AD.Models.Header;
using LandingAPI.AD.Services.Header;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.Header
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly IHeaderServices _iheader;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HeaderController(IHeaderServices iheader, IWebHostEnvironment webHostEnvironment)
        {
            _iheader = iheader;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetHeader")]
        public async Task<ActionResult> GetHeader()
        {
            try
            {
                var m_Header = await _iheader.GetHeader();

                if (m_Header != null)
                {
                    return Ok(m_Header);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener el header " + ex.Message);
            }
        }

        [HttpPost, Route("SaveHeaderImage-Logo")]
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
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "Images", "Empresa","Logo", fileNameNew);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }

            return Ok($"Images/Empresa/Logo{fileNameNew}");
        }

        [HttpPost, Route("AgregaModificaHeader")]
        public async Task<ActionResult> AgregarModificarHeader([FromForm] M_Header header)
        {
            try
            {
                if (header != null)
                {
                    M_Header m_Header = new();
                   
                        m_Header = await _iheader.GetHeaderById(header.IdHeader);

                        if (m_Header == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra el header a modificar");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _iheader.ModificarHeader(header);

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
                throw new Exception("Controller: Se produjo un error al agregar/modificar el header " + ex.Message);
            }
        }
    }
}
