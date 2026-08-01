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
    //40232840757
    public class HuespedService
    {

        // Instancia del repositorio de huespedes para interactuar con la base de datos
        private readonly HuespedRepositorio _huespedRepositorio = new HuespedRepositorio();

        // Método para listar todos los huespedes
        public List<Huesped> Listar()
        {
            return _huespedRepositorio.Listar();
        }

        // Método para buscar huespedes por texto (nombre, apellido, documento, etc.)
        public List<Huesped> Buscar(string texto)
        {
            return _huespedRepositorio.Buscar(texto);
        }

        // Método para obtener un huesped por su ID
        public DataTable HistorialEstadias(int huespedId)
        {
            return _huespedRepositorio.HistorialEstadias(huespedId);
        }

        // Método para crear un nuevo huesped
        public void Crear(Huesped huesped)
        {
            ValidarDatos(huesped);
            _huespedRepositorio.Crear(huesped);
        }

        // Método para actualizar un huesped existente
        public void Actualizar(Huesped huesped)
        {
            ValidarDatos(huesped);
            _huespedRepositorio.Actualizar(huesped);
        }

        // Método para eliminar un huesped por su ID
        public void Eliminar(int id)
        {
            _huespedRepositorio.Eliminar(id);
        }

        // Método privado para validar los datos de un huesped antes de crear o actualizar
        private void ValidarDatos(Huesped huesped)
        {
            if (string.IsNullOrWhiteSpace(huesped.Nombre) || string.IsNullOrWhiteSpace(huesped.Apellido))
            {
                throw new ArgumentException("Nombre y apellido son obligatorios.");
            }

            if (huesped.TipoDocumento != "Cedula" && huesped.TipoDocumento != "Pasaporte")
            {
                throw new ArgumentException("El tipo de documento debe ser Cedula o Pasaporte.");
            }

            if (huesped.TipoDocumento == "Cedula")
            {
                ValidarCedula(huesped.NumeroDocumento);
            }
            else if (string.IsNullOrWhiteSpace(huesped.NumeroDocumento))
            {
                throw new ArgumentException("El número de pasaporte es obligatorio.");
            }
        }


        // La cédula dominicana debe tener exactamente 11 dígitos numéricos.
        // Se lanza FormatException si contiene algo que no sea un dígito,
        //para que la UI la capture en su catch (FormatException, SqlException, Exception).

        public void ValidarCedula(string numeroDocumento)
        {
            if (string.IsNullOrWhiteSpace(numeroDocumento) || numeroDocumento.Length != 11)
            {
                throw new FormatException("La cédula debe tener exactamente 11 dígitos.");
            }

            foreach (char caracter in numeroDocumento)
            {
                if (!char.IsDigit(caracter))
                {
                    throw new FormatException("La cédula solo puede contener dígitos.");
                }
            }
        }

        // Servicio para obtener información del Club Millas de un huésped
        private readonly ClubMillasService _clubMillasService = new ClubMillasService();

        // Método para obtener los datos del Club Millas de un huésped por su ID
        public ClubMillas ObtenerMillasClub(int huespedId)
        {
            return _clubMillasService.ObtenerDatosClub(huespedId);
        }
    }
}