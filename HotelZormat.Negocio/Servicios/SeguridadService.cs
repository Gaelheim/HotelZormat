using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negocio.Servicios
{
    public class SeguridadService
    {
        // Genera el hash de contraseñas con SHA-256, exactamente el mismo algoritmo que usa el script de BD
        // para poder comparar el hash calculado en C# contra el guardado en Usuarios.PasswordHash.
        public byte[] CalcularHash(string textoPlano)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(textoPlano));
            }
        }
    }
}
