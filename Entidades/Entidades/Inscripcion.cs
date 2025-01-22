using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class Inscripcion
    {
        public int INS_IdInscripcion { get; set; } 
        public int EVT_IdEvento { get; set; } 
        public int USU_IdUsuario { get; set; } 
        public DateTime INS_FechaInscripcion { get; set; } = DateTime.Now; 
    }
}
