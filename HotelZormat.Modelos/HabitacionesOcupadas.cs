using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    // 40232840757
    public class HabitacionesOcupadas : Exception
    {
        public class HabitacionOcupadaException : Exception
        {
            public string NumeroHabitacion { get; }

            public HabitacionOcupadaException(string numeroHabitacion)
                : base("La habitación " + numeroHabitacion + " está ocupada.")
            {
                NumeroHabitacion = numeroHabitacion;
            }
        }
    }
}
