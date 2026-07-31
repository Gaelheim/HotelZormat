using HotelZormat.Modelos;
using HotelZormatDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negocio.Servicios
{
    //40232840757
    public class CheckInOutService
    {
        private readonly EstadiaRepositorio _estadiaRepositorio = new EstadiaRepositorio();
        private readonly ReservaRepositorio _reservaRepositorio = new ReservaRepositorio();
        private readonly BitacoraService _bitacoraService = new BitacoraService();

        //Convierte una reserva Confirmada en una estadía activa (habitación -> Ocupada).
        public int RealizarCheckIn(int reservaId, int usuarioId)
        {
            int estadiaId = _estadiaRepositorio.CheckIn(reservaId, usuarioId);
            _bitacoraService.Registrar(usuarioId, "CheckIn", "Reserva " + reservaId);
            return estadiaId;
        }

        //Cierra la estadía, genera la factura con NCF y libera la habitación a Limpieza.
        public Factura RealizarCheckOut(int estadiaId, int usuarioId)
        {
            Factura factura = _estadiaRepositorio.CheckOut(estadiaId, usuarioId);
            _bitacoraService.Registrar(usuarioId, "CheckOut", "Estadia " + estadiaId + " - NCF " + factura.NCF);
            _bitacoraService.Registrar(usuarioId, "Facturacion", "NCF " + factura.NCF + " Total " + factura.Total);
            return factura;
        }

        public Estadia ObtenerActivaPorHabitacion(int habitacionId)
        {
            return _estadiaRepositorio.ObtenerActivaPorHabitacion(habitacionId);
        }
    }
}
