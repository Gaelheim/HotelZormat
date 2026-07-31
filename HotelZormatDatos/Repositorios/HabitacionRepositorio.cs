using HotelZormatDatos.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelZormat.Modelos;

namespace HotelZormatDatos.Repositorios
{
    public class HabitacionRepositorio
    {
        private const string SelectBase =
           "SELECT h.Id, h.Numero, h.TipoHabitacionId, th.Nombre AS TipoNombre, th.TarifaBase, " +
           "h.Piso, h.Capacidad, h.Estado " +
           "FROM Habitaciones h JOIN TiposHabitacion th ON th.Id = h.TipoHabitacionId ";

        private Habitacion Mapear(SqlDataReader lector)
        {
            return new Habitacion
            {
                Id = (int)lector["Id"],
                Numero = (string)lector["Numero"],
                TipoHabitacionId = (int)lector["TipoHabitacionId"],
                TipoHabitacionNombre = (string)lector["TipoNombre"],
                TarifaBase = (decimal)lector["TarifaBase"],
                Piso = (int)lector["Piso"],
                Capacidad = (int)lector["Capacidad"],
                Estado = (string)lector["Estado"]
            };
        }

        //Listar con filtros opcionales por piso y estado (ambos nulos = todas).
        public List<Habitacion> Listar(int? piso, string estado)
        {
            var habitaciones = new List<Habitacion>();
            string consulta = SelectBase + " WHERE (@Piso IS NULL OR h.Piso = @Piso) " +
                               "AND (@Estado IS NULL OR h.Estado = @Estado) ORDER BY h.Piso, h.Numero";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Piso", (object)piso ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);

                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    foreach (IDataRecord _ in Iterar(lector))
                    {
                        habitaciones.Add(Mapear(lector));
                    }
                }
            }
            return habitaciones;
        }

        //Para poder usar foreach sobre el SqlDataReader sin exponerlo.
        private IEnumerable<IDataRecord> Iterar(SqlDataReader lector)
        {
            while (lector.Read())
            {
                yield return lector;
            }
        }

        public Habitacion ObtenerPorNumero(string numero)
        {
            string consulta = SelectBase + " WHERE h.Numero = @Numero";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Numero", numero);
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

        public Habitacion ObtenerPorId(int id)
        {
            string consulta = SelectBase + " WHERE h.Id = @Id";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
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

        public void Crear(Habitacion habitacion)
        {
            const string consulta = "INSERT INTO Habitaciones (Numero, TipoHabitacionId, Piso, Capacidad, Estado) " +
                                     "VALUES (@Numero, @TipoHabitacionId, @Piso, @Capacidad, @Estado)";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Numero", habitacion.Numero);
                comando.Parameters.AddWithValue("@TipoHabitacionId", habitacion.TipoHabitacionId);
                comando.Parameters.AddWithValue("@Piso", habitacion.Piso);
                comando.Parameters.AddWithValue("@Capacidad", habitacion.Capacidad);
                comando.Parameters.AddWithValue("@Estado", habitacion.Estado);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        //Actualiza tipo, estado y capacidad (lo que exige el requisito de Actualizar).
        public void Actualizar(Habitacion habitacion)
        {
            const string consulta = "UPDATE Habitaciones SET TipoHabitacionId = @TipoHabitacionId, " +
                                     "Capacidad = @Capacidad, Estado = @Estado WHERE Id = @Id";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@TipoHabitacionId", habitacion.TipoHabitacionId);
                comando.Parameters.AddWithValue("@Capacidad", habitacion.Capacidad);
                comando.Parameters.AddWithValue("@Estado", habitacion.Estado);
                comando.Parameters.AddWithValue("@Id", habitacion.Id);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void CambiarEstado(int habitacionId, string nuevoEstado)
        {
            const string consulta = "UPDATE Habitaciones SET Estado = @Estado WHERE Id = @Id";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Estado", nuevoEstado);
                comando.Parameters.AddWithValue("@Id", habitacionId);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            const string consulta = "DELETE FROM Habitaciones WHERE Id = @Id";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}
