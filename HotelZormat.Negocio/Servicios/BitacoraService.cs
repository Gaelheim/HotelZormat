using HotelZormat.Modelos;
using HotelZormatDatos.Repositorios;
using System;
using System.Collections.Generic;

namespace HotelZormat.Negocio.Servicios
{
    //40232840757
    public class BitacoraService
    {
        // Repositorio de bitácora para registrar y listar eventos.
        private readonly BitacoraRepositorio _repositorio = new BitacoraRepositorio();

        // Método para registrar un evento en la bitácora.
        public void Registrar(int usuarioId, string accion, string detalle)
        {
            _repositorio.Registrar(usuarioId, accion, detalle);
        }

        // Método para listar los eventos de la bitácora, solo accesible si el usuario tiene permiso.
        public List<Bitacora> Listar(bool puedeVerBitacora)
        {
            if (!puedeVerBitacora) throw new UnauthorizedAccessException("Su rol no tiene permiso para consultar la bitácora.");

            return _repositorio.Listar();
        }
    }
}