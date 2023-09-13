using LandingAPI.AD.Models.GaleriaImagenes;
using LandingAPI.AD.Models.ServicioImagenes;
using LandingAPI.AD.Services.GaleriaImagenes;
using LandingAPI.AD.Services.ServicioImagenes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.GaleriaImagen
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaleriaImagenController : ControllerBase
    {
        private readonly IGaleriaImagenServices _igaleriaImagenes;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GaleriaImagenController(IGaleriaImagenServices igaleriaImagenes, IWebHostEnvironment webHostEnvironment)
        {
            _igaleriaImagenes = igaleriaImagenes;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetGaleriaImagenes")]
        public async Task<ActionResult> GetGaleriaImagenes()
        {
            try
            {
                var m_GaleriaImagenes = await _igaleriaImagenes.GetGaleriaImagenes();

                if (m_GaleriaImagenes != null)
                {
                    return Ok(m_GaleriaImagenes);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener la imagen en galeria " + ex.Message);
            }
        }

        [HttpPost, Route("AgregaModifica/Galeria-Imagen")]
        public async Task<ActionResult> AddModifyGaleriaImagen([FromForm] M_GaleriaImagen galeriaImagenes)
        {
            try
            {
                if (galeriaImagenes != null)
                {
                    M_GaleriaImagen m_GaleriaImagen = new();

                    if (galeriaImagenes.IdGaleriaImagen == 0)
                    {
                        m_GaleriaImagen = await _igaleriaImagenes.GetGaleriaImagenByName(galeriaImagenes.Descripcion);

                        if (m_GaleriaImagen == null)
                        {
                            var Imagen = await _igaleriaImagenes.AgregarGaleriaImagen(galeriaImagenes);
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
                        m_GaleriaImagen = await _igaleriaImagenes.GetGaleriaImagenById(galeriaImagenes.IdGaleriaImagen);

                        if (m_GaleriaImagen == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra la imagen a modificar");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _igaleriaImagenes.ModificarGaleriaImagen(galeriaImagenes);

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

        [HttpDelete, Route("Elimina/Galeria-Imagen/{id}")]
        public async Task<ActionResult> DeleteGaleriaImagen(int id)
        {
            try
            {
                var galeriaImagen = await _igaleriaImagenes.GetGaleriaImagenById(id);

                if (galeriaImagen == null)
                {
                    ModelState.AddModelError("Advertencia Eliminar", "No se encuentra la imagen a eliminar");
                    return BadRequest(ModelState);
                }
                else
                {
                    var isDeleted = await _igaleriaImagenes.EliminarGaleriaImagen(id);

                    if (isDeleted)
                    {
                        return Ok("Imagen eliminada exitosamente");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al eliminar una imagen " + ex.Message);
            }
        }

        [HttpPost, Route("SaveImage-Galeria")]
        public async Task<ActionResult> SaveGaleriaImage(IFormFile image)
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
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "Images", "Empresa", "Galeria", fileNameNew);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }

            return Ok($"Images/Empresa/Galeria{fileNameNew}");
        }



    }
}
