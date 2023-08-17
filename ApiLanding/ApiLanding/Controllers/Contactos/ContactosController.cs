using LandingAPI.AD.Models.Contactos;
using LandingAPI.AD.Services.Contactos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLanding.Controllers.Contactos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly IContactoServices _IContactoServices;

        public ContactosController(IContactoServices IContactoServices)
        {
            _IContactoServices = IContactoServices;
        }
        //[Authorize]
        [HttpPost, Route("Agregar-ModificarContacto")]
        public async Task<ActionResult> AgregarContacto([FromBody] M_Contactos contactos)


        {
            try
            {
                if (contactos != null)
                {
                    M_Contactos m_Contactos = new();

                    if (contactos.IdContacto == 0)
                    {
                        m_Contactos = await _IContactoServices.GetContactosByName(contactos.Nombre);

                        if (m_Contactos == null)
                        {
                            var Id_Causa = await _IContactoServices.AgregarContacto(contactos);
                            return Ok(Id_Causa);
                        }
                        else
                        {
                            ModelState.AddModelError("Advertencia Agregar", "El contacto con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        m_Contactos = await _IContactoServices.GetContactoById(contactos.IdContacto);

                        if (m_Contactos == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra el contacto a modificar!");
                            return BadRequest(ModelState);
                        }

                        m_Contactos = await _IContactoServices.GetContactosByName(contactos.Nombre);

                        if (m_Contactos != null && m_Contactos.IdContacto != contactos.IdContacto && m_Contactos.Nombre == contactos.Nombre)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "La causa con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _IContactoServices.ModificarContacto(contactos);

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
                {
                    return BadRequest(contactos);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al agregar/modificar un contacto " + ex.Message);
            }

        }


    }
}
