using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    // 40232840757
    public class Estadia
    {
        public int Id { get; set; }
        public int ReservaId { get; set; }
        public int HabitacionId { get; set; }
        public int HuespedId { get; set; }
        public DateTime FechaCheckInReal { get; set; }
        public DateTime? FechaCheckOutReal { get; set; }
        public string Estado { get; set; } // Activa | Cerrada
        public int UsuarioCheckInId { get; set; }
        public int? UsuarioCheckOutId { get; set; }
    }
}
