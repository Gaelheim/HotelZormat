using System;
using System.Configuration;
using System.Data.SqlClient;

namespace HotelZormatDatos.Conexion
{
    public static class ConexionBD
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings["HotelZormatDB"].ConnectionString;
            return new SqlConnection(cadena);
        }


    }
}
