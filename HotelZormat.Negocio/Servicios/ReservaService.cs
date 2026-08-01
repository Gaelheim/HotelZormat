using HotelZormat.Modelos;
using HotelZormatDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelZormat.Negocio.Servicios
{
    // 40232840757
    public class ReservaService
    {
        
        private readonly ReservaRepositorio _reservaRepositorio = new ReservaRepositorio();
        private readonly HabitacionRepositorio _habitacionRepositorio = new HabitacionRepositorio();
        private readonly TemporadaRepositorio _temporadaRepositorio = new TemporadaRepositorio();

        // Listar todas las temporadas disponibles
        public System.Collections.Generic.List<Temporada> ListarTemporadas()
        {
            return _temporadaRepositorio.Listar();
        }

        // Crear una reserva y validar que la habitación esté disponible
        public Reserva CrearReserva(Reserva reserva)
        {
            if (reserva.FechaCheckOut <= reserva.FechaCheckIn)
            {
                throw new ArgumentException("La fecha de check-out debe ser posterior al check-in.");
            }

            // Validar que la habitación esté disponible
            Habitacion habitacion = _habitacionRepositorio.ObtenerPorId(reserva.HabitacionId);
            if (habitacion == null || !habitacion.EstaDisponible())
            {
                throw new HabitacionOcupadaException(habitacion != null ? habitacion.Numero : "?");
            }

           
            Reserva creada = _reservaRepositorio.Crear(reserva);

            // Validar factor de descuento de temporada usando switch en C#
            var temporadas = _temporadaRepositorio.Listar();
            var temp = temporadas.FirstOrDefault(t => t.Id == reserva.TemporadaId);
            if (temp != null)
            {
                // Validar que el factor de descuento de la temporada coincida con las reglas de negocio
                decimal factorCalculado = ObtenerFactorDescuentoTemporada(temp.Nombre);
                if (temp.FactorDescuento != factorCalculado)
                {
                    throw new InvalidOperationException("El factor de descuento de la temporada no coincide con las reglas de negocio.");
                }
            }

            // Al crear la reserva la habitación queda apartada para ese huésped.
            _habitacionRepositorio.CambiarEstado(reserva.HabitacionId, "Reservada");

            return creada;
        }

        // Confirmar una reserva y cambiar el estado de la habitación a "Ocupada"
        public void Confirmar(int reservaId)
        {
            _reservaRepositorio.CambiarEstado(reservaId, "Confirmada");
        }

        // Cancelar una reserva y cambiar el estado de la habitación a "Disponible"
        public void Cancelar(int reservaId, int habitacionId)
        {
            _reservaRepositorio.CambiarEstado(reservaId, "Cancelada");
            _habitacionRepositorio.CambiarEstado(habitacionId, "Disponible");
        }

        // Obtener una reserva confirmada por habitación
        public Reserva ObtenerConfirmadaPorHabitacion(int habitacionId)
        {
            return _reservaRepositorio.ObtenerConfirmadaPorHabitacion(habitacionId);
        }

        // Listar reservas confirmadas para los próximos 7 días
        public DataTable ListarProximas7Dias()
        {
            return _reservaRepositorio.ListarProximas7Dias();
        }

        public decimal ObtenerFactorDescuentoTemporada(string temporadaNombre)
        {
            // Aplicar factor por temporada con switch: Alta (sin descuento), Media (10% descuento), Baja (20% descuento).
            switch (temporadaNombre)
            {
                case "Alta":
                    return 0.00m;
                case "Media":
                    return 0.10m;
                case "Baja":
                    return 0.20m;
                default:
                    return 0.00m;
            }
        }
    }
}