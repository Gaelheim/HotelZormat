using HotelZormat.Modelos;
using HotelZormatDatos.Repositorios;
using System;

namespace HotelZormat.Negocio.Servicios
{
    // 40232840757
    public class ClubMillasService
    {
        // Repositorio de Club Millas para obtener y actualizar datos de los huéspedes.
        private readonly ClubMillasRepositorio _millasRepositorio = new ClubMillasRepositorio();

        // Constructor de la clase ClubMillasService.
        public ClubMillas ObtenerDatosClub(int huespedId)
        {
            return _millasRepositorio.ObtenerPorHuespedId(huespedId);
        }

        // Método para registrar puntos por estadía de un huésped.
        public void RegistrarPuntosPorEstadia(int huespedId, int nochesEstadia)
        {
            if (nochesEstadia <= 0) return;

            ClubMillas actual = _millasRepositorio.ObtenerPorHuespedId(huespedId);

            //Determinar rango actual basado en noches acumuladas antes de esta estadía
            string rangoPrevio = ObtenerRango(actual.NochesAcumuladas);

            //Determinar multiplicador según rango previo
            int multiplicador = ObtenerMultiplicadorPuntos(rangoPrevio);
            int puntosGanados = nochesEstadia * multiplicador;

            //Nuevos valores acumulados
            int nochesNuevas = actual.NochesAcumuladas + nochesEstadia;
            string nuevoRango = ObtenerRango(nochesNuevas);

            //Regla de bono al llegar a Diamante (si pasa de < 500 noches a >= 500 noches)
            bool esNuevoDiamante = (rangoPrevio != "Diamante" && nuevoRango == "Diamante");
            if (esNuevoDiamante)
            {
                //TODO: Implementar lógica de bono de matrícula anti-IA
                int bonoMatricula = 20233607; 
                puntosGanados += bonoMatricula;
            }

            //Actualizar en base de datos
            _millasRepositorio.ActualizarClub(huespedId, nochesEstadia, puntosGanados, nuevoRango);
        }

        // Método para obtener el rango basado en el número de noches acumuladas.
        public static string ObtenerRango(int noches)
        {
            if (noches >= 500) return "Diamante";
            if (noches >= 400) return "Platinum";
            if (noches >= 300) return "Oro";
            if (noches >= 200) return "Plata";
            if (noches >= 100) return "Bronce";
            return "Hierro";
        }

        // Método para obtener el multiplicador de puntos basado en el rango del huésped.
        public static int ObtenerMultiplicadorPuntos(string rango)
        {
            // El valor inicia siendo 2 por noche para Hierro y se suma 2 a cada rango
            switch (rango)
            {
                case "Diamante":
                    return 12; // Hierro (2) + 2*5
                case "Platinum":
                    return 10;
                case "Oro":
                    return 8;
                case "Plata":
                    return 6;
                case "Bronce":
                    return 4;
                case "Hierro":
                default:
                    return 2;
            }
        }
    }
}