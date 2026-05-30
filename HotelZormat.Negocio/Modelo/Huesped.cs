using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negocio.Modelo
{
    public class Huesped
    {
        public int Id {  get; set; } 
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; } //Cedula o Pasaporte
        public string NumeroDocumento { get; set; }
        public string Nacionalidad { get; set; }
        public string Telefono {  get; set; }
        public string Email { get; set; }

        public string NombreCompleto()
        {
            return Nombre + " " + Apellido;

        }

    }
}
