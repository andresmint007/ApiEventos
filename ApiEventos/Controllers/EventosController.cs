using Entidades.Entidades;
using Entidades.Interfaces;
using Entidades.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiEventos.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {

        private readonly IEventosServices _eventoService;
        public EventosController(IEventosServices eventoServices)
        {
            _eventoService = eventoServices;
        }


        [HttpPost]
        [Route("CrearEvento")]
        public async Task<ActionResult<RespuestaGeneral<int>>> CrearEvento([FromBody] Evento evento)
        {
            try
            {
                RespuestaGeneral<int> respuestaGeneral = await _eventoService.CrearEvento(evento);
                return StatusCode(respuestaGeneral.statusCode, respuestaGeneral);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("EditarEvento")]
        public async Task<ActionResult<RespuestaGeneral<bool>>> EditarEvento([FromBody] Evento evento)
        {
            try
            {
                RespuestaGeneral<bool> respuestaGeneral = await _eventoService.EditarEvento(evento,evento.idEvento);
                return StatusCode(respuestaGeneral.statusCode, respuestaGeneral);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error : {ex.Message}");
            }
        }
        [HttpPost]
        [Route("DesactivarEvento")]
        public async Task<ActionResult<RespuestaGeneral<bool>>> DesactivarEvento([FromBody]int idevento)
        {
            try
            {
                RespuestaGeneral<bool> respuestaGeneral = await _eventoService.EliminarEvento(idevento);
                return StatusCode(respuestaGeneral.statusCode, respuestaGeneral);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("InscribirEvento")]
        public async Task<ActionResult<RespuestaGeneral<bool>>> InscribirEvento([FromBody] Inscripcion inscripcion)
        {
            try
            {
                RespuestaGeneral<string> respuestaGeneral = await _eventoService.InscribirUsuario(inscripcion.idEvento,inscripcion.idUsuario);
                return StatusCode(respuestaGeneral.statusCode, respuestaGeneral);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("ObtenerEventos")]
        public async Task<ActionResult<RespuestaGeneral<List<EventoIsncritos>>>> ObtenerEventos()
        {
            try
            {
                RespuestaGeneral<List<EventoIsncritos>> respuestaGeneral = await _eventoService.ObtenerEventos();
                return StatusCode(respuestaGeneral.statusCode, respuestaGeneral);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}

