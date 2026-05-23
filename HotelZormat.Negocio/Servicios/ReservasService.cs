using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negocio.Servicios
{
    public class ReservasService
    {
        public bool ValidarTipoHabitacion(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
            {
                return false;
            }

            if (tipo == "Sencilla" || tipo == "Doble" || tipo == "Suite")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      /*  public bool ValidarTipoHabitacion(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo)) return false;
            return tipo == "Sencilla" || tipo == "Doble" || tipo == "Suite";
        } */

    }
}
