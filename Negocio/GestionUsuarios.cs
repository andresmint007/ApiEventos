using Entidades.Entidades;
using Entidades.Interfaces;
using Entidades.Login;
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
        private Cifrados cifrar = new Cifrados();
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

        public async Task<Usuario> ObtenerUsuarioPorID(int id)
        {
            Usuario objReturn = new Usuario();
            try
            {
                objReturn = await _usuarioRepository.ObtenerPorIdAsync(id);
            }
            catch (Exception ex)
            {
                return objReturn;
            }
            return objReturn;
        }
        public async Task<Usuario> ObtenerUsuarioPorEmial(string email)
        {
            Usuario objReturn = new Usuario();
            try
            {
                objReturn = await _usuarioRepository.ObtenerPorEmail(email);
            }
            catch (Exception ex)
            {
                return objReturn;
            }
            return objReturn;
        }

        public async Task<LoginApp> LoginUsuarios(LoginApp loginTry)
        {
            LoginApp objReturn = new LoginApp();
            try
            {
                Usuario userLogin = await _usuarioRepository.ObtenerPorEmail(loginTry.email);

                if (userLogin != null) {
                    string pwdDescifrada = Cifrados.DecodeFromBase64(userLogin.password);
                    if (pwdDescifrada.Equals(loginTry.password))
                    {
                        loginTry.email = userLogin.email;
                        loginTry.username = userLogin.nombre;
                        objReturn = loginTry;
                    }
                    else
                    {
                        objReturn = null;
                    }

                }
                else
                {
                    objReturn = null;
                }
            }
            catch (Exception ex)
            {
                return objReturn;
            }
            return objReturn;
        }

    }
}

