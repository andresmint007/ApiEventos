using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Response
{
    public class RespuestaGeneral<T>
    {
        public int statusCode {  get; set; }
        public string message { get; set; }
        public  T data {  get; set; }
    }
}
