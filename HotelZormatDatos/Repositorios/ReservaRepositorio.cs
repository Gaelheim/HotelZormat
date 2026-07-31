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
    public class ReservaRepositorio
    {
        //Crea la reserva vía sp_CrearReserva (el SP valida fechas y calcula noches/monto).
        public Reserva Crear(Reserva reserva)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_CrearReserva", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@HuespedId", reserva.HuespedId);
                comando.Parameters.AddWithValue("@HabitacionId", reserva.HabitacionId);
                comando.Parameters.AddWithValue("@TemporadaId", reserva.TemporadaId);
                comando.Parameters.AddWithValue("@FechaCheckIn", reserva.FechaCheckIn.Date);
                comando.Parameters.AddWithValue("@FechaCheckOut", reserva.FechaCheckOut.Date);
                comando.Parameters.AddWithValue("@UsuarioCreacionId", reserva.UsuarioCreacionId);

                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        reserva.Id = Convert.ToInt32(lector["ReservaId"]);
                        reserva.Noches = Convert.ToInt32(lector["Noches"]);
                        reserva.MontoEstimado = Convert.ToDecimal(lector["MontoEstimado"]);
                    }
                }
            }
            return reserva;
        }

        public void CambiarEstado(int reservaId, string nuevoEstado)
        {
            const string consulta = "UPDATE Reservas SET Estado = @Estado WHERE Id = @Id";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@Estado", nuevoEstado);
                comando.Parameters.AddWithValue("@Id", reservaId);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        //Reserva Confirmada más reciente para una habitación (la que usa el Check-In).
        public Reserva ObtenerConfirmadaPorHabitacion(int habitacionId)
        {
            const string consulta =
                "SELECT TOP 1 r.Id, r.HuespedId, r.HabitacionId, r.TemporadaId, r.FechaCheckIn, r.FechaCheckOut, " +
                "r.Estado, r.Noches, r.MontoEstimado, r.UsuarioCreacionId, r.FechaCreacion, " +
                "hu.Nombre + ' ' + hu.Apellido AS HuespedNombre, h.Numero AS HabitacionNumero " +
                "FROM Reservas r " +
                "JOIN Huespedes hu ON hu.Id = r.HuespedId " +
                "JOIN Habitaciones h ON h.Id = r.HabitacionId " +
                "WHERE r.HabitacionId = @HabitacionId AND r.Estado = 'Confirmada' " +
                "ORDER BY r.FechaCreacion DESC";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@HabitacionId", habitacionId);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return MapearConNombres(lector);
                    }
                }
            }
            return null;
        }

        private Reserva MapearConNombres(SqlDataReader lector)
        {
            return new Reserva
            {
                Id = (int)lector["Id"],
                HuespedId = (int)lector["HuespedId"],
                HabitacionId = (int)lector["HabitacionId"],
                TemporadaId = (int)lector["TemporadaId"],
                FechaCheckIn = (DateTime)lector["FechaCheckIn"],
                FechaCheckOut = (DateTime)lector["FechaCheckOut"],
                Estado = (string)lector["Estado"],
                Noches = (int)lector["Noches"],
                MontoEstimado = (decimal)lector["MontoEstimado"],
                UsuarioCreacionId = (int)lector["UsuarioCreacionId"],
                FechaCreacion = (DateTime)lector["FechaCreacion"],
                HuespedNombre = (string)lector["HuespedNombre"],
                HabitacionNumero = (string)lector["HabitacionNumero"]
            };
        }

        //Próximas reservas (próximos 7 días) vía la vista vw_ReservasProximas7Dias.
        public DataTable ListarProximas7Dias()
        {
            const string consulta = "SELECT * FROM vw_ReservasProximas7Dias ORDER BY FechaCheckIn";

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
