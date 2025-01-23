using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class Inscripcion
    {
        public int idInscripcion { get; set; } 
        public int idEvento { get; set; } 
        public int idUsuario { get; set; } 
        public DateTime fechaInscripcion { get; set; } = DateTime.Now; 
    }
}
