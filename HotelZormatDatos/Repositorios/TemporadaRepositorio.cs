using HotelZormat.Modelos;
using HotelZormatDatos.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormatDatos.Repositorios
{
    // 40232840757
    public class TemporadaRepositorio
    {
        public List<Temporada> Listar()
        {
            var temporadas = new List<Temporada>();

            using (SqlConnection conexion = ConexionBD.ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(
                "SELECT Id, Nombre, FactorDescuento FROM Temporadas ORDER BY Id", conexion))
            {
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        temporadas.Add(new Temporada
                        {
                            Id = (int)lector["Id"],
                            Nombre = (string)lector["Nombre"],
                            FactorDescuento = (decimal)lector["FactorDescuento"]
                        });
                    }
                }
            }
            return temporadas;
        }
    }
}
