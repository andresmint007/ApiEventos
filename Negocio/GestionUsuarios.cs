using Entidades.Entidades;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestionUsuarios
    {
        private readonly IRepositoryComun<Usuario> _usuarioRepository;

        public GestionUsuarios(IRepositoryComun<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> ObtenerUsuariosCompletos()
        {
            List < Usuario > objReturn = new List< Usuario >();
            try
            {
                objReturn= await _usuarioRepository.ObtenerTodosAsync();
            }
            catch (Exception ex)
            {
                return objReturn;
            }
            return objReturn;
        }

    }
}

