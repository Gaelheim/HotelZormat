using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormatDatos.Conexion
{
    public static class ConexionBD
    {
        private static readonly string _cadena =
            "Data Source= .;Initial Catalog=HotelZormatDB; Integrated Security=True;";

        public static SqlConnection ObtenerConexion()
        { 

            return new SqlConnection(_cadena);

        }


    }
}
