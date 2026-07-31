using HotelZormat.Modelos;
using HotelZormatDatos.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormatDatos.Repositorios
{
    // 40232840757
    public class UsuarioRepositorio
    {
        // Valida usuario/hash contra sp_Login. Devuelve null si no hay coincidencia.
        public Usuario Login(string nombreUsuario, byte[] passwordHash)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_Login", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                comando.Parameters.AddWithValue("@PasswordHash", passwordHash);

                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new Usuario
                        {
                            Id = (int)lector["Id"],
                            NombreUsuario = (string)lector["NombreUsuario"],
                            NombreCompleto = (string)lector["NombreCompleto"],
                            RolNombre = (string)lector["Rol"],
                            PuedeEliminarHabitaciones = (bool)lector["PuedeEliminarHabitaciones"],
                            PuedeVerBitacora = (bool)lector["PuedeVerBitacora"]
                        };
                    }
                }
            }
            return null;
        }
    }
}
