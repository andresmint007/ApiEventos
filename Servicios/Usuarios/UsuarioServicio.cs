using Entidades.Entidades;
using Entidades.Interfaces;
using Entidades.Login;
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

        public UsuarioServicio(GestionUsuarios gestionUsuarios)
        {
            _gestionUsuarios = gestionUsuarios;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _gestionUsuarios.ObtenerUsuariosCompletos();
        }
        public async Task<LoginApp> LoginApp(LoginApp loginTry)
        {
            return await _gestionUsuarios.LoginUsuarios(loginTry);
        }
    }
}
