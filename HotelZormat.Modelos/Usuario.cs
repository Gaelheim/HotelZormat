using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Modelos
{
    // 40232840757
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public byte[] PasswordHash { get; set; }
        public int RolId { get; set; }
        public string NombreCompleto { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string RolNombre { get; set; }
        public bool PuedeEliminarHabitaciones { get; set; }
        public bool PuedeVerBitacora { get; set; }

        public bool EsAdministrador()
        {
            return RolNombre == "Administrador";
        }
    }
}
