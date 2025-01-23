using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Entidades.Login;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Text;
using Entidades.Interfaces;
using Servicios.Usuarios;
using Entidades.Entidades;
namespace ApiEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioServices _usuarioService;

        public AuthController(IConfiguration configuration, IUsuarioServices usuarioServices)
        {
            _config = configuration;
            _usuarioService = usuarioServices;

        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginApp loginApp)
        {
            string token = await generateToken(loginApp);
            if (string.IsNullOrEmpty(token)) {
                return BadRequest( new { error = "Contraseña/Usuario incorrectos" });
            }
            else
            {
                return Ok( new { token = token });
            }
        }

        private async Task<string> generateToken(LoginApp loginApp)
        {
            LoginApp loginReal = await _usuarioService.LoginApp(loginApp);
            if (loginReal != null)
            {
                try
                {


                    int expires = Convert.ToInt32(_config.GetSection("JwtSettings:ExpirationTimeInMinutes").Value);
                    Claim[] claims = new[]
                    {
                new Claim (ClaimTypes.Name, loginApp.username),
                new Claim (ClaimTypes.Email,loginApp.email)
            };
                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:SecurityKey").Value));
                    SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                    JwtSecurityToken token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(expires),
                        signingCredentials: creds
                        );
                    string toekString = new JwtSecurityTokenHandler().WriteToken(token);
                    return toekString;
                }
                catch (Exception ex) { 
                return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

