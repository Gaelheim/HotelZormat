using HotelZormat.Modelos;
using HotelZormatDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotelZormat.Modelos.HabitacionesOcupadas;

namespace HotelZormat.Negocio.Servicios
{
    // 40232840757
    public class ReservaService
    {
        private readonly ReservaRepositorio _reservaRepositorio = new ReservaRepositorio();
        private readonly HabitacionRepositorio _habitacionRepositorio = new HabitacionRepositorio();
        private readonly TemporadaRepositorio _temporadaRepositorio = new TemporadaRepositorio();

        public System.Collections.Generic.List<Temporada> ListarTemporadas()
        {
            return _temporadaRepositorio.Listar();
        }

        public Reserva CrearReserva(Reserva reserva)
        {
            if (reserva.FechaCheckOut <= reserva.FechaCheckIn)
            {
                throw new ArgumentException("La fecha de check-out debe ser posterior al check-in.");
            }

            Habitacion habitacion = _habitacionRepositorio.ObtenerPorId(reserva.HabitacionId);
            if (habitacion == null || !habitacion.EstaDisponible())
            {
                throw new HabitacionOcupadaException(habitacion != null ? habitacion.Numero : "?");
            }

            Reserva creada = _reservaRepositorio.Crear(reserva);

            // Al crear la reserva la habitación queda apartada para ese huésped.
            _habitacionRepositorio.CambiarEstado(reserva.HabitacionId, "Reservada");

            return creada;
        }

        public void Confirmar(int reservaId)
        {
            _reservaRepositorio.CambiarEstado(reservaId, "Confirmada");
        }

        public void Cancelar(int reservaId, int habitacionId)
        {
            _reservaRepositorio.CambiarEstado(reservaId, "Cancelada");
            _habitacionRepositorio.CambiarEstado(habitacionId, "Disponible");
        }

        public Reserva ObtenerConfirmadaPorHabitacion(int habitacionId)
        {
            return _reservaRepositorio.ObtenerConfirmadaPorHabitacion(habitacionId);
        }

        public DataTable ListarProximas7Dias()
        {
            return _reservaRepositorio.ListarProximas7Dias();
        }
    }
}
