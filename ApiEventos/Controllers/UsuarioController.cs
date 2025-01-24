using Entidades.Entidades;
using Entidades.Interfaces;
using Entidades.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Servicios.Eventos;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEventos.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioService;
        public UsuarioController(IUsuarioServices usuarioServices) {
            _usuarioService = usuarioServices;
        }

        [HttpGet]
        [Route("ObtenerUsuarioEmail")]
        public async Task<ActionResult<RespuestaGeneral<Usuario>>> ObtenerUsuarioEmail(string email)
        {
            try
            {
                RespuestaGeneral<Usuario> respuestaGeneral = await _usuarioService.ObtenerUsuarioporEmail(email);
                return StatusCode(respuestaGeneral.statusCode, respuestaGeneral);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


    }
}
