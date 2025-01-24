using Entidades.Entidades;
using Entidades.Interfaces;
using Entidades.Login;
using Entidades.Response;
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

        public async Task<RespuestaGeneral<Usuario>> ObtenerUsuarioporEmail(string email)
        {
            RespuestaGeneral<Usuario> response = new RespuestaGeneral<Usuario>();
            try
            {
                Usuario user = new Usuario();
                
                user = await _gestionUsuarios.ObtenerUsuarioPorEmial(email);
                if (user == null)
                {
                    response.statusCode = 400;
                    response.message = "El usuario no existe";
                    response.data = null;
                }
                else
                {
                    user.password = string.Empty;
                    response.statusCode = 200;
                    response.message = "Conusltado Ok";
                    response.data = user;
                }
            }
            catch (Exception ex) {
                response.statusCode = 400;
                response.message = "Ocurrio un error en el servicio de obtener usuario";
                response.data = null;
            }
            return response;
        }
    }
}
