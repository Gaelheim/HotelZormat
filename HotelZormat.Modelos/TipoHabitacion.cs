using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    // 40232840757
    public class TipoHabitacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Sencilla, Doble, Suite
        public decimal TarifaBase { get; set; }
        public int CapacidadMax { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
