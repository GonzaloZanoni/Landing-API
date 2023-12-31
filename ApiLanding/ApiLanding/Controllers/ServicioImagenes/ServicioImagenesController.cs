﻿using LandingAPI.AD.Models.ServicioImagenes;
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
        public async Task<ActionResult> AddModifyServicioImagen([FromForm] M_ServicioImagenes servicioImagenes)
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

        [HttpDelete, Route("Elimina/Servicio-Imagen/{id}")]
        public async Task<ActionResult> DeleteServicioImagen(int id)
        {
            try
            {
                var servicioImagenes = await _iservicioImagenes.GetServicioImagenById(id);

                if (servicioImagenes == null)
                {
                    ModelState.AddModelError("Advertencia Eliminar", "No se encuentra la imagen a eliminar");
                    return BadRequest(ModelState);
                }
                else
                {
                    var isDeleted = await _iservicioImagenes.EliminarServicioImagen(id);

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
                throw new Exception("Controller: Se produjo un error al eliminar una imagen en Servicios " + ex.Message);
            }
        }

        [HttpPost, Route("SaveImage-Servicio")]
        public async Task<ActionResult> SaveServicioImage(IFormFile image)
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
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "Images", "Empresa", "Servicios", fileNameNew);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }

            return Ok($"Images/Empresa/Servicios{fileNameNew}");
        }
    }
}
