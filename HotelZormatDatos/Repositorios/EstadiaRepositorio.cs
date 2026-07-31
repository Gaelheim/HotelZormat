using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelZormatDatos.Conexion;
using System.Data;
using System.Data.SqlClient;


namespace HotelZormatDatos.Repositorios
{
    // 40232840757
    public class EstadiaRepositorio
    {
        //Check-in vía sp_CheckIn: confirma la reserva, crea la estadía y ocupa la habitación.
        public int CheckIn(int reservaId, int usuarioId)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_CheckIn", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@ReservaId", reservaId);
                comando.Parameters.AddWithValue("@UsuarioId", usuarioId);

                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return Convert.ToInt32(lector["EstadiaId"]);
                    }
                }
            }
            throw new InvalidOperationException("No se pudo registrar el check-in.");
        }

        //Check-out vía sp_CheckOut: cierra la estadía, genera la factura (NCF) y libera la habitación.
        public Factura CheckOut(int estadiaId, int usuarioId)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_CheckOut", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@EstadiaId", estadiaId);
                comando.Parameters.AddWithValue("@UsuarioId", usuarioId);

                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new Factura
                        {
                            EstadiaId = estadiaId,
                            NCF = (string)lector["NCF"],
                            Subtotal = Convert.ToDecimal(lector["Subtotal"]),
                            ITBIS = Convert.ToDecimal(lector["ITBIS"]),
                            Propina = Convert.ToDecimal(lector["Propina"]),
                            Total = Convert.ToDecimal(lector["Total"]),
                            UsuarioId = usuarioId
                        };
                    }
                }
            }
            throw new InvalidOperationException("No se pudo registrar el check-out.");
        }

        //Busca la estadía activa asociada a una habitación (la que necesita el Check-Out).
        public Estadia ObtenerActivaPorHabitacion(int habitacionId)
        {
            const string consulta =
                "SELECT Id, ReservaId, HabitacionId, HuespedId, FechaCheckInReal, FechaCheckOutReal, " +
                "Estado, UsuarioCheckInId, UsuarioCheckOutId " +
                "FROM Estadias WHERE HabitacionId = @HabitacionId AND Estado = 'Activa'";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@HabitacionId", habitacionId);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new Estadia
                        {
                            Id = (int)lector["Id"],
                            ReservaId = (int)lector["ReservaId"],
                            HabitacionId = (int)lector["HabitacionId"],
                            HuespedId = (int)lector["HuespedId"],
                            FechaCheckInReal = (DateTime)lector["FechaCheckInReal"],
                            FechaCheckOutReal = lector["FechaCheckOutReal"] == DBNull.Value ? (DateTime?)null : (DateTime)lector["FechaCheckOutReal"],
                            Estado = (string)lector["Estado"],
                            UsuarioCheckInId = (int)lector["UsuarioCheckInId"],
                            UsuarioCheckOutId = lector["UsuarioCheckOutId"] == DBNull.Value ? (int?)null : (int)lector["UsuarioCheckOutId"]
                        };
                    }
                }
            }
            return null;
        }
    }
}
