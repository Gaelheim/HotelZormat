using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    // 40232840757
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Administrador | Recepcionista
        public bool PuedeEliminarHabitaciones { get; set; }
        public bool PuedeVerBitacora { get; set; }
    }
}
