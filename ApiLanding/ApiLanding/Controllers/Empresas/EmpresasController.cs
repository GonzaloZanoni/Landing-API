using LandingAPI.AD.Models.Empresas;
using LandingAPI.AD.Models.GaleriaImagenes;
using LandingAPI.AD.Services.Empresas;
using LandingAPI.AD.Services.Footer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.Empresas
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaServices _iempresaServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmpresasController(IEmpresaServices iempresaServices, IWebHostEnvironment webHostEnvironment)
        {
            _iempresaServices = iempresaServices;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetEmpresa")]
        public async Task<ActionResult> GetEmpresa()
        {
            try
            {
                var m_Empresas = await _iempresaServices.GetEmpresa();

                if (m_Empresas != null)
                {
                    return Ok(m_Empresas);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener empresa " + ex.Message);
            }
        }

        [HttpPost, Route("AgregaModifica/Empresa")]
        public async Task<ActionResult> AddModifyEmpresa([FromForm] M_Empresas empresa)
        {
            try
            {
                if (empresa != null)
                {
                    M_Empresas m_Empresa = new();

                    if (empresa.IdEmpresa == 0)
                    {
                        m_Empresa = await _iempresaServices.GetEmpresaByName(empresa.Nombre);

                        if (m_Empresa == null)
                        {
                            var Imagen = await _iempresaServices.AgregarEmpresa(empresa);
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
                        m_Empresa = await _iempresaServices.GetEmpresaById(empresa.IdEmpresa);

                        if (m_Empresa == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra la imagen a modificar");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _iempresaServices.ModificarEmpresa(empresa);

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
                throw new Exception("Controller: Se produjo un error al agregar/modificar una empresa " + ex.Message);
            }
        }

        [HttpDelete, Route("Elimina/Empresa/{id}")]
        public async Task<ActionResult> DeleteEmpresa(int id)
        {
            try
            {
                var empresa = await _iempresaServices.GetEmpresaById(id);

                if (empresa == null)
                {
                    ModelState.AddModelError("Advertencia Eliminar", "No se encuentra la empresa a eliminar");
                    return BadRequest(ModelState);
                }
                else
                {
                    var isDeleted = await _iempresaServices.EliminarEmpresa(id);

                    if (isDeleted)
                    {
                        return Ok("Empresa eliminada exitosamente");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al eliminar una empresa " + ex.Message);
            }
        }

    }
}
