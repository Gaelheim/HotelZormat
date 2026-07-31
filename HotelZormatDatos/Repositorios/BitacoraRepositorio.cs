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
    //40232840757
    public class BitacoraRepositorio
    {
        //Registra una acción crítica vía sp_RegistrarBitacora (login, check-in, check-out, facturación).
        public void Registrar(int usuarioId, string accion, string detalle)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_RegistrarBitacora", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@UsuarioId", usuarioId);
                comando.Parameters.AddWithValue("@Accion", accion);
                comando.Parameters.AddWithValue("@Detalle", (object)detalle ?? System.DBNull.Value);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        //Consulta completa de la bitácora (solo accesible desde la UI para el rol Administrador).
        public DataTable Listar()
        {
            const string consulta =
                "SELECT b.FechaHora, u.NombreUsuario, r.Nombre AS Rol, b.Accion, b.Detalle " +
                "FROM Bitacora b " +
                "JOIN Usuarios u ON u.Id = b.UsuarioId " +
                "JOIN Roles r ON r.Id = u.RolId " +
                "ORDER BY b.FechaHora DESC";

            var tabla = new DataTable();
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    tabla.Load(lector);
                }
            }
            return tabla;
        }
    }
}

