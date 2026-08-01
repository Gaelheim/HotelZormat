using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelZormatDatos.Conexion;
using System.Data;
using System.Data.SqlClient;
using HotelZormat.Modelos;

namespace HotelZormatDatos.Repositorios
{
    // 40232840757
    public class HuespedRepositorio
    {
        // Base de la consulta para evitar repetirla en cada método.
        private const string SelectBase =
            "SELECT Id, Nombre, Apellido, TipoDocumento, NumeroDocumento, Nacionalidad, Telefono, Email, FechaRegistro " +
            "FROM Huespedes ";
        
        //Mapea un registro de SqlDataReader a un objeto Huesped.
        private Huesped Mapear(SqlDataReader lector)
        {
            return new Huesped
            {
                Id = (int)lector["Id"],
                Nombre = (string)lector["Nombre"],
                Apellido = (string)lector["Apellido"],
                TipoDocumento = (string)lector["TipoDocumento"],
                NumeroDocumento = (string)lector["NumeroDocumento"],
                Nacionalidad = (string)lector["Nacionalidad"],
                Telefono = lector["Telefono"] == DBNull.Value ? null : (string)lector["Telefono"],
                Email = lector["Email"] == DBNull.Value ? null : (string)lector["Email"],
                FechaRegistro = (DateTime)lector["FechaRegistro"]
            };
        }

        //Lista todos los huéspedes en la base de datos, ordenados por apellido y nombre.
        public List<Huesped> Listar()
        {
            var huespedes = new List<Huesped>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(SelectBase + " ORDER BY Apellido, Nombre", conexion))
            {
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        huespedes.Add(Mapear(lector));
                    }
                }
            }
            return huespedes;
        }

        //Búsqueda por número de documento (cédula/pasaporte) o por nombre/apellido.
        public List<Huesped> Buscar(string texto)
        {
            var huespedes = new List<Huesped>();
            string consulta = SelectBase +
                " WHERE NumeroDocumento LIKE @Texto OR Nombre LIKE @Texto OR Apellido LIKE @Texto " +
                " ORDER BY Apellido, Nombre";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Texto", "%" + texto + "%");
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        huespedes.Add(Mapear(lector));
                    }
                }
            }
            return huespedes;
        }

        //Obtiene un huésped por su ID.
        public Huesped ObtenerPorId(int id)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(SelectBase + " WHERE Id = @Id", conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return Mapear(lector);
                    }
                }
            }
            return null;
        }

        // Crea un nuevo huésped en la base de datos.
        public void Crear(Huesped huesped)
        {
            const string consulta =
                "INSERT INTO Huespedes (Nombre, Apellido, TipoDocumento, NumeroDocumento, Nacionalidad, Telefono, Email) " +
                "VALUES (@Nombre, @Apellido, @TipoDocumento, @NumeroDocumento, @Nacionalidad, @Telefono, @Email)";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                AgregarParametros(comando, huesped);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Actualiza los datos de un huésped existente en la base de datos.
        public void Actualizar(Huesped huesped)
        {
            const string consulta =
                "UPDATE Huespedes SET Nombre = @Nombre, Apellido = @Apellido, TipoDocumento = @TipoDocumento, " +
                "NumeroDocumento = @NumeroDocumento, Nacionalidad = @Nacionalidad, Telefono = @Telefono, Email = @Email " +
                "WHERE Id = @Id";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                AgregarParametros(comando, huesped);
                comando.Parameters.AddWithValue("@Id", huesped.Id);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Agrega los parámetros del objeto Huesped al comando SQL.
        private void AgregarParametros(SqlCommand comando, Huesped huesped)
        {
            comando.Parameters.AddWithValue("@Nombre", huesped.Nombre);
            comando.Parameters.AddWithValue("@Apellido", huesped.Apellido);
            comando.Parameters.AddWithValue("@TipoDocumento", huesped.TipoDocumento);
            comando.Parameters.AddWithValue("@NumeroDocumento", huesped.NumeroDocumento);
            comando.Parameters.AddWithValue("@Nacionalidad", huesped.Nacionalidad);
            comando.Parameters.AddWithValue("@Telefono", (object)huesped.Telefono ?? DBNull.Value);
            comando.Parameters.AddWithValue("@Email", (object)huesped.Email ?? DBNull.Value);
        }

        // Elimina un huésped de la base de datos por su ID.
        public void Eliminar(int id)
        {
            const string consulta = "DELETE FROM Huespedes WHERE Id = @Id";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        //Historial de estadías del huésped (habitación, fechas, estado)
        public DataTable HistorialEstadias(int huespedId)
        {
            const string consulta =
                "SELECT h.Numero AS Habitacion, e.FechaCheckInReal, e.FechaCheckOutReal, e.Estado " +
                "FROM Estadias e JOIN Habitaciones h ON h.Id = e.HabitacionId " +
                "WHERE e.HuespedId = @HuespedId ORDER BY e.FechaCheckInReal DESC";

            var tabla = new DataTable();
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@HuespedId", huespedId);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    tabla.Load(lector);
                }
            }
            return tabla;
        }

        // Obtiene el total de noches hospedadas por un huésped en todas sus estadías cerradas.
        public int ObtenerTotalNochesHospedadas(int huespedId)
        {
            const string consulta = @"
                SELECT COALESCE(SUM(r.Noches), 0)
                FROM Estadias e
                JOIN Reservas r ON r.Id = e.ReservaId
                WHERE e.HuespedId = @HuespedId AND e.Estado = 'Cerrada'";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@HuespedId", huespedId);
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }
    }
}