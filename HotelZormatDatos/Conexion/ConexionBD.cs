using System;
using System.Configuration;
using System.Data.SqlClient;

namespace HotelZormatDatos.Conexion
{
    // 40232840757
    public static class ConexionBD
    {
        // Helper centralizado de conexión. La cadena vive en App.config
        public static SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings["HotelZormatDB"].ConnectionString;
            return new SqlConnection(cadena);
        }


    }
}
