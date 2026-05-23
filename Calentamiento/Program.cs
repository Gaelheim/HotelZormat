using System;
using HotelZormat.Negocio.Modelo;
using HotelZormat.Negocio.Servicios;

namespace HotelZormat.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var servicio = new ReservasService();

            // Aquí llamaremos los métodos de cada reto para probarlos

            Console.WriteLine("--- RETO 01: ValidarTipoHabitacion ---");
            Console.WriteLine("Suite     → " + servicio.ValidarTipoHabitacion("Suite"));
            Console.WriteLine("Sencilla  → " + servicio.ValidarTipoHabitacion("Sencilla"));
            Console.WriteLine("Penthouse → " + servicio.ValidarTipoHabitacion("Penthouse"));
            Console.WriteLine("(vacío)   → " + servicio.ValidarTipoHabitacion(""));
            Console.WriteLine("(null)    → " + servicio.ValidarTipoHabitacion(null));

            Console.WriteLine("\n--- Tests finalizados ---");
            Console.ReadKey();
        }
    }
}
