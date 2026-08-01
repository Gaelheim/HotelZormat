using HotelZormat.Modelos;
using HotelZormatDatos.Conexion;
using System;
using System.Data;
using System.Data.SqlClient;


namespace HotelZormatDatos.Repositorios
{
    // Cédula: 40232840757
    public class ClubMillasRepositorio
    {
        public ClubMillas ObtenerPorHuespedId(int huespedId)
        {
            // Consulta para obtener los datos del Club de Millas del huésped
            const string consulta = "SELECT HuespedId, PuntosAcumulados, NochesAcumuladas, Rango FROM ClubMillas WHERE HuespedId = @HuespedId";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@HuespedId", huespedId);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new ClubMillas
                        {
                            HuespedId = (int)lector["HuespedId"],
                            PuntosAcumulados = (int)lector["PuntosAcumulados"],
                            NochesAcumuladas = (int)lector["NochesAcumuladas"],
                            Rango = lector["Rango"].ToString()
                        };
                    }
                }
            }

            // Si no tiene registro, se crea uno inicial por defecto
            return new ClubMillas
            {
                HuespedId = huespedId,
                PuntosAcumulados = 0,
                NochesAcumuladas = 0,
                Rango = "Hierro"
            };
        }

        // Actualiza los puntos, noches y rango del huésped en el Club de Millas
        public void ActualizarClub(int huespedId, int nochesSumar, int puntosSumar, string nuevoRango)
        {
            const string consulta = @"
                IF EXISTS(SELECT 1 FROM ClubMillas WHERE HuespedId = @HuespedId)
                BEGIN
                    UPDATE ClubMillas 
                    SET PuntosAcumulados = PuntosAcumulados + @PuntosSumar,
                        NochesAcumuladas = NochesAcumuladas + @NochesSumar,
                        Rango = @NuevoRango
                    WHERE HuespedId = @HuespedId
                END
                ELSE
                BEGIN
                    INSERT INTO ClubMillas (HuespedId, PuntosAcumulados, NochesAcumuladas, Rango)
                    VALUES (@HuespedId, @PuntosSumar, @NochesSumar, @NuevoRango)
                END";

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@HuespedId", huespedId);
                comando.Parameters.AddWithValue("@NochesSumar", nochesSumar);
                comando.Parameters.AddWithValue("@PuntosSumar", puntosSumar);
                comando.Parameters.AddWithValue("@NuevoRango", nuevoRango);

                conexion.Open();
                comando.ExecuteNonQuery();
            }

        }

    }

}
        
