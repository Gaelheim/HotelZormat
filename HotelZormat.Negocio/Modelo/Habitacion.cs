using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negocio.Modelo
{
    public class Habitacion
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public int Piso { get; set; }
        public int Capacidad { get; set; }
        public decimal TarifaBase   { get; set; }
        public string Estado { get; set; }

        public bool EstaDisponible()
        {
            return Estado == "Disponible";
        }

    }
}
