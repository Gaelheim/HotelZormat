using HotelZormat.Modelos;
using HotelZormatDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotelZormat.Modelos.HabitacionesOcupadas;

namespace HotelZormat.Negocio.Servicios
{
    // 40232840757
    public class HabitacionService
    {
        private readonly HabitacionRepositorio _habitacionRepositorio = new HabitacionRepositorio();
        private readonly TipoHabitacionRepositorio _tipoHabitacionRepositorio = new TipoHabitacionRepositorio();
        public List<TipoHabitacion> ListarTipos()
        {
            return _tipoHabitacionRepositorio.Listar();
        }

        public List<Habitacion> Listar(int? piso, string estado)
        {
            return _habitacionRepositorio.Listar(piso, estado);
        }

        public Habitacion ObtenerPorNumero(string numero)
        {
            return _habitacionRepositorio.ObtenerPorNumero(numero);
        }

        public void Crear(Habitacion habitacion)
        {
            ValidarDatos(habitacion);
            _habitacionRepositorio.Crear(habitacion);
        }

        public void Actualizar(Habitacion habitacion)
        {
            ValidarDatos(habitacion);

            // Regla de negocio: una habitación Ocupada no se puede reconfigurar (tipo/capacidad)
            // sin antes hacer check-out. Se avisa con la excepción propia del negocio.
            Habitacion actual = _habitacionRepositorio.ObtenerPorId(habitacion.Id);
            if (actual != null && actual.Estado == "Ocupada" && habitacion.Estado != "Ocupada")
            {
                // Cambiar el estado manualmente desde aquí (saltándose Check-Out) no está permitido.
                throw new HabitacionOcupadaException(actual.Numero);
            }

            _habitacionRepositorio.Actualizar(habitacion);
        }

        //Solo el rol Administrador puede eliminar (se valida también en la UI).
        public void Eliminar(int id, bool puedeEliminar)
        {
            if (!puedeEliminar)
            {
                throw new System.UnauthorizedAccessException("Su rol no tiene permiso para eliminar habitaciones.");
            }
            _habitacionRepositorio.Eliminar(id);
        }

        public void MarcarLimpia(int habitacionId)
        {
            _habitacionRepositorio.CambiarEstado(habitacionId, "Disponible");
        }

        private void ValidarDatos(Habitacion habitacion)
        {
            if (string.IsNullOrWhiteSpace(habitacion.Numero))
            {
                throw new System.ArgumentException("El número de habitación es obligatorio.");
            }
            if (habitacion.Capacidad <= 0)
            {
                throw new System.ArgumentException("La capacidad debe ser mayor a cero.");
            }
            if (habitacion.Piso < 0)
            {
                throw new System.ArgumentException("El piso no puede ser negativo.");
            }
        }
    }
}
