using Entidades.Entidades;
using Entidades.Interfaces;
using Entidades.Response;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Eventos
{
    public class EventosService : IEventosServices
    {
        private readonly GestionEventos _gestionEventos;

        // Inyección de dependencias a través del constructor
        public EventosService(GestionEventos gestionEventos)
        {
            _gestionEventos = gestionEventos;
        }

        public async Task<RespuestaGeneral<int>> CrearEvento(Evento evento)
        {
            RespuestaGeneral<int> response = new RespuestaGeneral<int>();
            try
            {
                int reason = await _gestionEventos.CrearEvento(evento);
                if (reason > 0) {
                    response.statusCode = 201;
                    response.message = "Evento Creado satisfactoriamente";
                    response.data = reason;
                }
                else
                {
                    response.statusCode = 400;
                    response.message = "El evento no pudo ser creado, revise la configuracion";
                    response.data = 0;
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                response.statusCode = 500;
                response.message = "Ocurrio un error al crear el evento";
                response.data = 0;
            }
            return response;
        }

        public async Task<RespuestaGeneral<bool>> EditarEvento(Evento evento, int idEvento)
        {

            
            RespuestaGeneral<bool> response = new RespuestaGeneral<bool>();
            try
            {
                if (await validarEvento(idEvento))
                {


                    bool reason = await _gestionEventos.EditarEvento(evento);
                    if (reason)
                    {
                        response.statusCode = 200;
                        response.message = "Evento Editado satisfactoriamente";
                        response.data = reason;
                    }
                    else
                    {
                        response.statusCode = 400;
                        response.message = "El evento no pudo ser editado, revise la configuracion";
                        response.data = false;
                    }
                }
                else
                {
                    response.statusCode = 400;
                    response.message = "El evento no existe o esta inactivo";
                    response.data = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                response.statusCode = 500;
                response.message = "Ocurrio un error al editar el evento";
                response.data = false;
            }
            return response;
        }

        public async Task<RespuestaGeneral<bool>> EliminarEvento(int idEvento)
        {
            RespuestaGeneral<bool> response = new RespuestaGeneral<bool>();
            try
            {

                if (await validarEvento(idEvento))
                {
                    bool reason = await _gestionEventos.DesactivarEvento(idEvento);
                    if (reason)
                    {
                        response.statusCode = 200;
                        response.message = "Evento desactivado satisfactoriamente";
                        response.data = reason;
                    }
                    else
                    {
                        response.statusCode = 400;
                        response.message = "El evento no pudo ser desactivado, revise la configuracion";
                        response.data = false;
                    }
                }
                else
                {
                    response.statusCode = 400;
                    response.message = "El evento no existe o ya esta inactivo";
                    response.data = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                response.statusCode = 500;
                response.message = "Ocurrio un error al desactivar el evento";
                response.data = false;
            }
            return response;
        }

        private async Task<bool> validarEvento(int idEvento)
        {
            try
            {
                Evento evento = await _gestionEventos.ObetnerPorID(idEvento);

                return evento ==null ?false :true;
            }
            catch
            {
                return false;
            }
        }
        public Task<RespuestaGeneral<List<Evento>>> ListarEventos()
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaGeneral<string>> InscribirUsuario(int idEvento, int idUsuario)
        {
            RespuestaGeneral<string> response = new RespuestaGeneral<string>();
            try
            {
               string reason= await _gestionEventos.InscribirUsuario(idEvento, idUsuario);
                if (reason.Contains("ERROR"))
                {

                    response.statusCode = 400;
                    response.message = reason;
                    response.data =string.Empty;
                }
                else
                {
                    response.statusCode = 200;
                    response.message = reason;
                    response.data = reason;
                }
            }
            catch
            {
                response.statusCode = 400;
                response.message = "ERRO: Ocurrio un problema al invocar el servicio de crear usuario";
                response.data = string.Empty;
            }
            return response;
        }

        public async Task<RespuestaGeneral<List<EventoIsncritos>>> ObtenerEventos()
        {
            RespuestaGeneral<List<EventoIsncritos>> response = new RespuestaGeneral<List<EventoIsncritos>>();
            try
            {
                List<EventoIsncritos> eventoIsncritos = await _gestionEventos.ObtenerEventos();
                if(eventoIsncritos.Count > 0)
                {
                    response.statusCode = 200;
                    response.message = "Consulta Exitosa";
                    response.data = eventoIsncritos;
                }
                else
                {
                    response.statusCode = 200;
                    response.message = "No existen Registros";
                    response.data = null;
                }
            }
            catch
            {
                response.statusCode = 400;
                response.message = "ERROR: OCurrio un error al invocar el servicio de obtener eventos";
                response.data = null;
            }
            return response;
        }
    }
}
