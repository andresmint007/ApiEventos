using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Entidades;

using Negocio;
namespace PruebasUnitarias
{
    public class EventosTest
    {
        private readonly Mock<GestionEventos> _gestionEventosMoq;
        public EventosTest()
        {
            _gestionEventosMoq = new Mock<GestionEventos>();
        }
        [Fact]
        
        public async Task CrearEvento()
        {
            Evento eventoCrear = new Evento
            {
                nombre = "Evento de Prueba",
                descripcion = "Descripción del evento",
                fechaHora = DateTime.Now,
                ubicacion = "Ubicación de prueba",
                capacidad = 100
            };
            _gestionEventosMoq.Setup(g => g.CrearEvento(It.IsAny<Evento>())).ReturnsAsync(1);
            int resultado = await _gestionEventosMoq.Object.CrearEvento(eventoCrear);
            Assert.Equal(1, resultado);
        }
        [Fact]
        public async Task EditarEvento()
        {
            Evento eventoEditar = new Evento
            {

                fechaHora = DateTime.Now.AddDays(14),
                ubicacion = "Ubicación nueva",
                capacidad = 300
            };
            _gestionEventosMoq.Setup(g => g.EditarEvento(It.IsAny<Evento>())).ReturnsAsync(true);
            bool resultado = await _gestionEventosMoq.Object.EditarEvento(eventoEditar);
            Assert.True(resultado);
        }
        [Fact]
        public async Task DesactivarEvento()
        {
            int evento = 15;
            _gestionEventosMoq.Setup(g => g.DesactivarEvento(It.IsAny<int>())).ReturnsAsync(true);
            bool resultado = await _gestionEventosMoq.Object.DesactivarEvento(evento);
            Assert.True(resultado);
        }
    }
}
