using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelZormat.Modelos;

namespace HotelZormat.Negocio
{
    // 40232840757

    // Guarda el usuario que inició sesión durante la vida de la aplicación.
    //Se llena en FrmLogin tras un login exitoso y lo consultan las demás
    public static class Sesion
    {
        public static Usuario UsuarioActual { get; set; }

        public static bool HayUsuarioActivo()
        {
            return UsuarioActual != null;
        }

        public static void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}
