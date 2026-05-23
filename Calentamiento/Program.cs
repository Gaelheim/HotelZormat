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

            Console.WriteLine("\n--- RETO 02: ObtenerDescuentoPorTemporada ---");
            Console.WriteLine("Baja  → " + servicio.ObtenerDescuentoPorTemporada("Baja"));
            Console.WriteLine("Media → " + servicio.ObtenerDescuentoPorTemporada("Media"));
            Console.WriteLine("Alta  → " + servicio.ObtenerDescuentoPorTemporada("Alta"));
            Console.WriteLine("Pico  → " + servicio.ObtenerDescuentoPorTemporada("Pico"));

            try
            {
                servicio.ObtenerDescuentoPorTemporada("Otoño");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Otoño → excepción: " + ex.Message);
            }

            Console.WriteLine("\n--- RETO 03: GenerarLineasFactura ---");
            var lineas = servicio.GenerarLineasFactura(3, 4500m);
            Console.WriteLine("Reserva 3 noches a RD$ 4500:");
            foreach (var linea in lineas)
            {
                Console.WriteLine("  " + linea);
            }

            Console.WriteLine("\nReserva 0 noches:");
            var vacia = servicio.GenerarLineasFactura(0, 2000m);
            Console.WriteLine("  Total líneas: " + vacia.Count);

            for (int i = 0; i <= 12; i = i + 1 )
            {
                Console.WriteLine("===========La tabla del 7===========");

                Console.WriteLine( "7 * " +  i + " es igual a " + 7 * i);

            }

            Console.WriteLine("\n--- Tests finalizados ---");
            Console.ReadKey();
        }
    }
}
