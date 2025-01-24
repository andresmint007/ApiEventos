using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    public class EventoIsncritos
    {
        public int idEvento {  get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaHora { get; set; }
        public string ubicacion { get; set; }
        public int? capacidad { get; set; }
        [JsonIgnore]
        public string listaUsuarios { get; set; }
        public List<int > usuarios { get; set; } = new List<int>();
    }
}
