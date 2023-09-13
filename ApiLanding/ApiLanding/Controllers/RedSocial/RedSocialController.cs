using LandingAPI.AD.Models.RedSocial;
using LandingAPI.AD.Models.ServicioImagenes;
using LandingAPI.AD.Services.RedSocial;
using LandingAPI.AD.Services.ServicioImagenes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.RedSocial
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedSocialController : ControllerBase
    {
        private readonly IRedSocialServices _iRedSocialServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RedSocialController(IRedSocialServices iRedSocialServices, IWebHostEnvironment webHostEnvironment)
        {
            _iRedSocialServices = iRedSocialServices;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetRedSocial")]
        public async Task<ActionResult> GetRedSocial()
        {
            try
            {
                var m_RedSocial = await _iRedSocialServices.GetRedSocial();

                if (m_RedSocial != null)
                {
                    return Ok(m_RedSocial);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener la Red Social " + ex.Message);
            }
        }

        [HttpPost, Route("AgregaModifica/RedSocial")]
        public async Task<ActionResult> AddModifyRedSocial([FromForm] M_RedSocial redSocial)
        {
            try
            {
                if (redSocial != null)
                {
                    M_RedSocial m_RedSocial = new();

                    if (redSocial.IdRedSocial == 0)
                    {
                        m_RedSocial = await _iRedSocialServices.GetRedSocialByName(redSocial.Nombre);

                        if (m_RedSocial == null)
                        {
                            var Imagen = await _iRedSocialServices.AgregarRedSocial(redSocial);
                            return Ok(Imagen);
                        }
                        else
                        {
                            ModelState.AddModelError("Advertencia Alta", "La Red social con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        m_RedSocial = await _iRedSocialServices.GetRedSocialById(redSocial.IdRedSocial);

                        if (m_RedSocial == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra la Red Social a modificar");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _iRedSocialServices.ModificarRedSocial(redSocial);

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
                throw new Exception("Controller: Se produjo un error al agregar/modificar una Red Social " + ex.Message);
            }
        }

        [HttpDelete, Route("Elimina/RedSocial/{id}")]
        public async Task<ActionResult> DeleteServicioImagen(int id)
        {
            try
            {
                var redSocial = await _iRedSocialServices.GetRedSocialById(id);

                if (redSocial == null)
                {
                    ModelState.AddModelError("Advertencia Eliminar", "No se encuentra la Red social a eliminar");
                    return BadRequest(ModelState);
                }
                else
                {
                    var isDeleted = await _iRedSocialServices.EliminarRedSocial(id);

                    if (isDeleted)
                    {
                        return Ok("Red social eliminada exitosamente");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al eliminar una Red social " + ex.Message);
            }
        }

        [HttpPost, Route("SaveImage-RedSocial")]
        public async Task<ActionResult> SaveRedSocialImage(IFormFile image)
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
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "Images", "Empresa", "RedSocial", fileNameNew);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }

            return Ok($"Images/Empresa/RedSocial{fileNameNew}");
        }
    }
}
