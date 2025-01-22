using Entidades.Entidades;
using Entidades.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioService;
        public UsuarioController(IUsuarioServices usuarioServices) {
            _usuarioService = usuarioServices;
        }

        [HttpGet]
        [Route("ObtenerUsuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetUsuarios();

                return Ok(usuarios);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al obtener los usuarios: {ex.Message}");
            }
        }


    }
}
