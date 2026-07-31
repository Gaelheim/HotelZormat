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
    public class ReporteRepositorio
    {
        public class ReporteRepository
        {
            //Reporte 1: ocupación del día, vía la vista vw_OcupacionDelDia.
            public DataTable OcupacionDelDia()
            {
                const string consulta = "SELECT * FROM vw_OcupacionDelDia ORDER BY Habitacion";

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
}
