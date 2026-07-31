using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    //40232840757
    public class Bitacora
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } 
        public string RolNombre { get; set; }   
        public string Accion { get; set; }
        public string Detalle { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
