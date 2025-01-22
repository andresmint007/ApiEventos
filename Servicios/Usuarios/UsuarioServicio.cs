using Entidades.Entidades;
using Entidades.Interfaces;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Usuarios
{
    public class UsuarioServicio : IUsuarioServices
    {
        private readonly GestionUsuarios _gestionUsuarios;

        // Inyección de dependencias a través del constructor
        public UsuarioServicio(GestionUsuarios gestionUsuarios)
        {
            _gestionUsuarios = gestionUsuarios;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _gestionUsuarios.ObtenerUsuariosCompletos();
        }
    }
}
