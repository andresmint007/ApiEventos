using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class Evento
    {
        public int EVT_IdEvento { get; set; } 
        public int USU_IdUsuario { get; set; }
        public string EVT_Nombre { get; set; }
        public string EVT_Descripcion { get; set; }
        public DateTime EVT_FechaHora { get; set; }
        public string EVT_Ubicacion { get; set; }
        public int? EVT_Capacidad { get; set; }
    }
}
