using HotelZormatDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotelZormatDatos.Repositorios.ReporteRepositorio;

namespace HotelZormat.Negocio.Servicios
{
    // 40232840757
    public class ReporteService
    {
        private readonly ReporteRepositorio _reporteRepositorio = new ReporteRepositorio();
        private readonly FacturaRepositorio _facturaRepositorio = new FacturaRepositorio();

        //Reporte 1: habitaciones ocupadas hoy con su huésped.
        public DataTable OcupacionDelDia()
        {
            return _reporteRepositorio.OcupacionDelDia();
        }

        //Reporte 2: ingresos (suma de facturas) entre dos fechas.
        public DataRow IngresosPorRango(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaFin < fechaInicio)
            {
                throw new ArgumentException("La fecha final no puede ser anterior a la fecha inicial.");
            }
            return _facturaRepositorio.ReporteIngresosPorRango(fechaInicio, fechaFin);
        }
    }
}