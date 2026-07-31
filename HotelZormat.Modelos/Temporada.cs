using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    // 40232840757
    public class Temporada
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // Alta | Media | Baja
        public decimal FactorDescuento { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
