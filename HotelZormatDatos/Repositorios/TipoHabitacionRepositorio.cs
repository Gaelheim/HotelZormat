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
    public class TipoHabitacionRepositorio
    {
      
            public List<TipoHabitacion> Listar()
            {
                var tipos = new List<TipoHabitacion>();

                using (SqlConnection conexion = ConexionBD.ObtenerConexion())
                using (SqlCommand comando = new SqlCommand(
                    "SELECT Id, Nombre, TarifaBase, CapacidadMax FROM TiposHabitacion ORDER BY Nombre", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            tipos.Add(new TipoHabitacion
                            {
                                Id = (int)lector["Id"],
                                Nombre = (string)lector["Nombre"],
                                TarifaBase = (decimal)lector["TarifaBase"],
                                CapacidadMax = (int)lector["CapacidadMax"]
                            });
                        }
                    }
                }
                return tipos;
            }

     }   
    
}
