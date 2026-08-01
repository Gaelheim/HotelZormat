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

        private readonly ClubMillasService _clubMillasService = new ClubMillasService();

        //Cierra la estadía, genera la factura con NCF y libera la habitación a Limpieza.
        public Factura RealizarCheckOut(int estadiaId, int usuarioId)
        {
            int huespedId = 0;
            int noches = 0;

            // Obtener datos de millas antes del check-out
            try
            {
                _estadiaRepositorio.ObtenerDatosMillas(estadiaId, out huespedId, out noches);
            }
            // Manejar cualquier excepción que pueda ocurrir al obtener los datos de millas
            catch (Exception ex)
            {
                _bitacoraService.Registrar(usuarioId, "ErrorDatosMillas", "Error obteniendo datos de millas: " + ex.Message);
            }

            // Realizar el check-out y generar la factura
            Factura factura = _estadiaRepositorio.CheckOut(estadiaId, usuarioId);
            _bitacoraService.Registrar(usuarioId, "CheckOut", "Estadia " + estadiaId + " - NCF " + factura.NCF);
            _bitacoraService.Registrar(usuarioId, "Facturacion", "NCF " + factura.NCF + " Total " + factura.Total);

            // Registrar puntos en el Club de Millas si se obtuvieron datos válidos
            if (huespedId > 0 && noches > 0)
            {
                // Intentar registrar los puntos en el Club de Millas y manejar cualquier excepción
                try
                {
                    _clubMillasService.RegistrarPuntosPorEstadia(huespedId, noches);
                }
                // Manejar cualquier excepción que pueda ocurrir al registrar los puntos en el Club de Millas
                catch (Exception ex)
                {
                    _bitacoraService.Registrar(usuarioId, "ErrorRegistroMillas", "Error registrando puntos club: " + ex.Message);
                }
            }

            return factura;
        }

        // Obtiene la estadía activa para una habitación específica.
        public Estadia ObtenerActivaPorHabitacion(int habitacionId)
        {
            return _estadiaRepositorio.ObtenerActivaPorHabitacion(habitacionId);
        }
    }
}