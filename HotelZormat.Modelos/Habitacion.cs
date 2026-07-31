using System;

namespace HotelZormat.Modelos
{
    // 40232840757
    public class Habitacion
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int TipoHabitacionId { get; set; }
        public string TipoHabitacionNombre { get; set; } //viene del JOIN con TiposHabitacion
        public decimal TarifaBase { get; set; }           //viene de TiposHabitacion.TarifaBase
        public int Piso { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; } // Disponible | Ocupada | Reservada | Limpieza

        public bool EstaDisponible()
        {
            return Estado == "Disponible";
        }
    }
}
