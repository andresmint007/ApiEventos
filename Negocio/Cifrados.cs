using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Cifrados
    {
        public static string EncodeToBase64(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new ArgumentNullException(nameof(cadena), "El texto no puede ser nulo o vacío.");
            byte[] convert = Encoding.UTF8.GetBytes(cadena);

            return Convert.ToBase64String(convert);
        }

        public static string DecodeFromBase64(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                throw new ArgumentNullException(nameof(cadena), "El texto codificado no puede ser nulo o vacío.");

            try
            {
                byte[] decodificar = Convert.FromBase64String(cadena);

                return Encoding.UTF8.GetString(decodificar);
            }
            catch (FormatException)
            {
                throw new FormatException("El texto proporcionado no está en un formato Base64 válido.");
            }
        }
    }
}
