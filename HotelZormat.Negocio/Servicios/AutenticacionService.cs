using HotelZormat.Modelos;
using HotelZormatDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negocio.Servicios
{
    // 40232840757
    public class AutenticacionService
    {
        private readonly UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        private readonly SeguridadService _seguridadService = new SeguridadService();
        private readonly BitacoraService _bitacoraService = new BitacoraService();

        
        //Intenta iniciar sesión. Devuelve el Usuario autenticado, o null si las
        //credenciales no son válidas. Registra el login exitoso en la Bitácora.
       
        public Usuario Login(string nombreUsuario, string password)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Debe escribir usuario y contraseña.");
            }

            byte[] hash = _seguridadService.CalcularHash(password);
            Usuario usuario = _usuarioRepositorio.Login(nombreUsuario, hash);

            if (usuario != null)
            {
                _bitacoraService.Registrar(usuario.Id, "Login", "Inicio de sesión de " + usuario.NombreUsuario);
            }

            return usuario;
        }
    }
}
