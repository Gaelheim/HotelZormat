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
    public class FacturaRepositorio
    {
        //Reporte de ingresos entre dos fechas, vía sp_ReporteIngresosPorRango.
        public DataRow ReporteIngresosPorRango(System.DateTime fechaInicio, System.DateTime fechaFin)
        {
            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand("sp_ReporteIngresosPorRango", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                comando.Parameters.AddWithValue("@FechaFin", fechaFin.Date);

                var tabla = new DataTable();
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    tabla.Load(lector);
                }
                return tabla.Rows.Count > 0 ? tabla.Rows[0] : null;
            }
        }

        //Detalle de todas las facturas (vw_FacturasDetalle), para auditoría o consulta.
        public DataTable ListarDetalle()
        {
            const string consulta = "SELECT * FROM vw_FacturasDetalle ORDER BY FechaEmision DESC";

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