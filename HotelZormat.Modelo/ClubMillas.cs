using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    //40232840757
    public class ClubMillas
    {
        public int HuespedId { get; set; }
        public int PuntosAcumulados { get; set; }
        public int NochesAcumuladas { get; set; }
        public string Rango { get; set; } // Hierro, Bronce, Plata, Oro, Platinum, Diamante
    }
}
