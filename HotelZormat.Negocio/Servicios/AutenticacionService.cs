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
        // Repositorios y servicios necesarios para la autenticación
        private readonly UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        private readonly SeguridadService _seguridadService = new SeguridadService();
        private readonly BitacoraService _bitacoraService = new BitacoraService();


        //Intenta iniciar sesión. Devuelve el Usuario autenticado, o null si las
        //credenciales no son válidas. Registra el login exitoso en la Bitácora.

        public Usuario Login(string nombreUsuario, string password)
        {
            // Validar que el nombre de usuario y la contraseña no estén vacíos
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Debe escribir usuario y contraseña.");
            }

            // Calcular el hash de la contraseña proporcionada
            byte[] hash = _seguridadService.CalcularHash(password);
            Usuario usuario = _usuarioRepositorio.Login(nombreUsuario, hash);

            // Registrar el inicio de sesión en la bitácora si el usuario es válido
            if (usuario != null)
            {
                _bitacoraService.Registrar(usuario.Id, "Login", "Inicio de sesión de " + usuario.NombreUsuario);
            }

            // Devolver el usuario autenticado o null si las credenciales no son válidas
            return usuario;
        }
    }
}