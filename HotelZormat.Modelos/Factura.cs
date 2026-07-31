using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    // 40232840757
    public class Factura
    {
        public int Id { get; set; }
        public int EstadiaId { get; set; }
        public string NCF { get; set; }
        public decimal Subtotal { get; set; }
        public decimal ITBIS { get; set; }
        public decimal Propina { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaEmision { get; set; }
        public int UsuarioId { get; set; }
    }
}
