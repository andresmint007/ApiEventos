using Entidades.Entidades;
using Moq;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasUnitarias
{
    public class UsusariosTest
    {
        private readonly Mock<GestionUsuarios> _gestionUserMoq;
        public UsusariosTest()
        {
            _gestionUserMoq = new Mock<GestionUsuarios>();
        }
        [Fact]
        public async Task ObtenerUsuarioPorID_CuandoUsuarioExiste_DeberiaRetornarUsuario()
        {
            int usuarioId = 1;
            Usuario usuarioEsperado = new Usuario
            {
                idUsuario = usuarioId,
                nombre = "Juan Pérez",
                email = "juan.perez@example.com"
            };

            _gestionUserMoq
                .Setup(repo => repo.ObtenerUsuarioPorID(usuarioId))
                .ReturnsAsync(usuarioEsperado);             
        }
    }
}
