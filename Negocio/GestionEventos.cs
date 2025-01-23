using Datos;
using Entidades.Entidades;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestionEventos
    {
        private readonly IRepositoryComun<Evento> _eventoRepository;
        private readonly InscripcionesContextoDB _inscripcionesRepository;
        private readonly GestionUsuarios _gestionUsuarios;

        private Cifrados cifrar = new Cifrados();
        public GestionEventos(IRepositoryComun<Evento> eventoRepository,InscripcionesContextoDB inscripcionesContextoDB, GestionUsuarios gestionUsuarios)
        {
            _eventoRepository = eventoRepository;
            _inscripcionesRepository =inscripcionesContextoDB;
            _gestionUsuarios = gestionUsuarios;
        }
        public async Task<int> CrearEvento(Evento eventoCrear)
        {
            int eventoCreado = 0;
            try
            {
                eventoCreado =await _eventoRepository.CrearAsync(eventoCrear);

            }
            catch (Exception ex) {
                Console.WriteLine("Ocurrio un error al crear el eventos");
            } 
            return eventoCreado;
        }
        public async Task<bool> EditarEvento(Evento eventoeditar)
        {
            bool eventoEditado = false;
            try
            {
                eventoEditado = await _eventoRepository.ActualizarAsync(eventoeditar);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al crear el eventos");
            }
            return eventoEditado;
        }
        public async Task<bool> DesactivarEvento(int idEvento)
        {
            bool razonEliminado = false;
            try
            {
                razonEliminado = await _eventoRepository.EliminarAsync(idEvento);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al crear el eventos");
            }
            return razonEliminado;
        }
        public async Task <Evento> ObetnerPorID (int idEvento)
        {
            Evento evento = null;
            try
            {
                evento = await _eventoRepository.ObtenerPorIdAsync(idEvento);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al crear el eventos");
            }
            return evento;
        }
        public async Task<string> InscribirUsuario (int idEvento, int idUsuario)
        {
            try
            {
                string errorPosible = await ValidarInscripcion(idEvento, idUsuario);
                if (string.IsNullOrEmpty(errorPosible))
                {
                    int idInscripcion= await _inscripcionesRepository.InscribirEventoUsuario(idEvento, idUsuario);
                    if (idInscripcion > 0)
                    {
                        return "El usuario se inscribio exitosamente";
                    }
                    else
                    {
                        return "ERROR No pudo inscrbirse el usuario intente mas tarde.";


                    }
                }
                else
                {
                    return errorPosible;
                }

            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return "EROR: Ocurrio un error al insribir el usuario";

            }
        }
        public async Task<string> ValidarInscripcion(int idEvento, int idUsuario)
        {
            try
            {
                Evento evento_valido = await _eventoRepository.ObtenerPorIdAsync(idEvento);
                Usuario usuario_valido = await _gestionUsuarios.ObtenerUsuarioPorID(idUsuario);
                int numeroAistententes = await _inscripcionesRepository.ObetnerNumeroParticipantes(idEvento);
                int numeroEventosUsuario = await _inscripcionesRepository.ObetnerNumeroEventosUsuario(idEvento,idUsuario);
                if(evento_valido.idUsuario == usuario_valido.idUsuario)
                {
                    return "ERROR: El usuario de creación del evento no puede inscribirse asimismo";
                }
                else if(numeroAistententes >= evento_valido.capacidad)
                {
                    return "ERROR: El evento ya alcanzo su capacidad maxima: "+ evento_valido.capacidad.ToString()+ "Personas";
                }
                else if (numeroEventosUsuario>=3)
                {
                    return "ERROR: El usuario: "+ usuario_valido.nombre+" tiene registrados 3 eventos no puede inscrbirse a mas";
                }
                else
                {
                    return string.Empty;
                }


            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());   
                return  "ERROR: Ocurrio un error al insribir el usuario";

            }
        }
        public async Task<List<EventoIsncritos>> ObtenerEventos()
        {
            List<EventoIsncritos> eventosinscritos= new List<EventoIsncritos>();
            try
            {
                List<EventoIsncritos>  eventosBD = await _inscripcionesRepository.ObetenerEventosInscritos();
                eventosinscritos = eventosBD.Select(x => new EventoIsncritos
                {
                    nombre = x.nombre,
                    descripcion = x.descripcion,
                    capacidad = x.capacidad,
                    fechaHora = x.fechaHora,
                    ubicacion = x.ubicacion,
                    usuarios = string.IsNullOrWhiteSpace(x.listaUsuarios)
                                                        ? new List<int>() 
                                                        :x.listaUsuarios.Split(',')
                                                        .Select(u => int.Parse(u.Trim())) 
                                                        .ToList()
                }).ToList();
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                

            }
            return eventosinscritos;
        }

    }
}
