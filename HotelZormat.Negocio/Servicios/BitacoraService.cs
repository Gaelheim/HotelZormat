using HotelZormat.Modelos;
using HotelZormatDatos.Repositorios;
using System;
using System.Collections.Generic;

namespace HotelZormat.Negocio.Servicios
{
    //40232840757
    public class BitacoraService
    {
        private readonly BitacoraRepositorio _repositorio =
            new BitacoraRepositorio();

        public void Registrar(int usuarioId,
                              string accion,
                              string detalle)
        {
            _repositorio.Registrar(usuarioId, accion, detalle);
        }

        public List<Bitacora> Listar(bool puedeVerBitacora)
        {
            if (!puedeVerBitacora)
                throw new UnauthorizedAccessException(
                    "Su rol no tiene permiso para consultar la bitácora.");

            return _repositorio.Listar();
        }
    }
}
