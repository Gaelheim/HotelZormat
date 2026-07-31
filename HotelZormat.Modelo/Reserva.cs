using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    // 40232840757
    public class Reserva
    {
        public int Id { get; set; }
        public int HuespedId { get; set; }
        public int HabitacionId { get; set; }
        public int TemporadaId { get; set; }
        public DateTime FechaCheckIn { get; set; }
        public DateTime FechaCheckOut { get; set; }
        public string Estado { get; set; } // Pendiente | Confirmada | Cancelada
        public int Noches { get; set; }
        public decimal MontoEstimado { get; set; }
        public int UsuarioCreacionId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string HuespedNombre { get; set; }
        public string HabitacionNumero { get; set; }
    }
}
