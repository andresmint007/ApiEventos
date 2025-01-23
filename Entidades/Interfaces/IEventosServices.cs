using Entidades.Entidades;
using Entidades.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IEventosServices
    {
        
        public Task<RespuestaGeneral<int>> CrearEvento(Evento evento);
        public Task<RespuestaGeneral<bool>> EditarEvento(Evento evento,int idEvento);
        public Task <RespuestaGeneral<bool>> EliminarEvento(int idEvento);
        public Task<RespuestaGeneral<List<Evento>>> ListarEventos();
        public Task<RespuestaGeneral<List<EventoIsncritos>>> ObtenerEventos();

        public Task<RespuestaGeneral<string>> InscribirUsuario(int idEvento,int idUsuario);
       

    }
}
