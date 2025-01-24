using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Login;
using Entidades.Response;
namespace Entidades.Interfaces
{
    public interface IUsuarioServices
    {

        public Task<List<Usuario>> GetUsuarios();
        public Task<LoginApp> LoginApp(LoginApp loginTry);
        public Task<RespuestaGeneral<Usuario>> ObtenerUsuarioporEmail(string email);


    }
}
