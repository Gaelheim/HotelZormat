using HotelZormatDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negocio.Servicios
{
    //40232840757
    public class BitacoraService
    {
        private readonly BitacoraRepositorio _bitacoraRepositorio = new BitacoraRepositorio();

        public void Registrar(int usuarioId, string accion, string detalle)
        {
            _bitacoraRepositorio.Registrar(usuarioId, accion, detalle);
        }

        //Solo debe llamarse desde una pantalla ya validada como Administrador.
        public DataTable Listar(bool puedeVerBitacora)
        {
            if (!puedeVerBitacora)
            {
                throw new System.UnauthorizedAccessException("Su rol no tiene permiso para ver la bitácora.");
            }
            return _bitacoraRepositorio.Listar();
        }
    }
}
