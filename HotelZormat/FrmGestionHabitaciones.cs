using HotelZormat.Negocio.Modelo;
using HotelZormat.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelZormat
{
    public partial class FrmGestionHabitaciones : Form
    {
        private Habitacion _habitacionActual;

        // Datos de ejemplo en memoria (el día que conectemos a SQL Server, esto se reemplaza por una llamada a la capa Datos).
        private List<Habitacion> habitaciones = new List<Habitacion>
        {
            new Habitacion { Id = 1, Numero = "301", Tipo = "Sencilla", Piso = 3, Capacidad = 1, TarifaBase = 1500m, Estado = "Disponible" },
            new Habitacion { Id = 2, Numero = "302", Tipo = "Doble",    Piso = 3, Capacidad = 2, TarifaBase = 2500m, Estado = "Ocupada" },
            new Habitacion { Id = 3, Numero = "303", Tipo = "Suite",    Piso = 3, Capacidad = 4, TarifaBase = 4500m, Estado = "Reservada" },
            new Habitacion { Id = 4, Numero = "304", Tipo = "Sencilla", Piso = 3, Capacidad = 1, TarifaBase = 1500m, Estado = "Limpieza" },
            new Habitacion { Id = 5, Numero = "201", Tipo = "Doble",    Piso = 2, Capacidad = 2, TarifaBase = 2500m, Estado = "Disponible" },
            new Habitacion { Id = 6, Numero = "401", Tipo = "Suite",    Piso = 4, Capacidad = 4, TarifaBase = 4500m, Estado = "Ocupada" },
        };

        public FrmGestionHabitaciones()
        {
            InitializeComponent();
            this.Load += FrmGestionHabitaciones_Load;
            this.CbxTipo.SelectedIndexChanged += CbxTipo_SelectedIndexChanged;
            this.btnBuscar.Click += btnBuscar_Click;
            this.btnConfirmar.Click += btnConfirmar_Click;
            this.btnGuardarCambios.Click += btnGuardarCambios_Click;
        }

        private void FrmGestionHabitaciones_Load(object sender, EventArgs e)
        {
            CbxTipo.Items.Clear();
            var tipos = new List<string> { "Sencilla", "Doble", "Suite" };
            foreach (var tipo in tipos)
            {
                CbxTipo.Items.Add(tipo);
            }
            if (CbxTipo.Items.Count > 0)
            {
                CbxTipo.SelectedIndex = 0; // dispara SelectedIndexChanged
            }

            cbxAccion.Items.Clear();
            var acciones = new List<string> { "Reservar", "Check-In", "Check-Out", "Marcar Limpia" };
            foreach (var accion in acciones)
            {
                cbxAccion.Items.Add(accion);
            }
            if (cbxAccion.Items.Count > 0)
            {
                cbxAccion.SelectedIndex = 0;
            }

            CargarHabitacionesPiso3();
        }

        private void CargarHabitacionesPiso3()
        {
            // Se limpia ANTES de recorrer: así, si el método se llama de
            // nuevo, la lista nunca queda duplicada.
            lbxHabitacionPiso3.Items.Clear();

            foreach (Habitacion habitacion in habitaciones)
            {
                if (habitacion.Piso == 3)
                {
                    string linea = habitacion.Numero + " - " + habitacion.Tipo;
                    lbxHabitacionPiso3.Items.Add(linea);
                }
            }
        }

        private void CbxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = CbxTipo.Text;

            lblIcono.Text = ObtenerIconoPorTipo(tipo);

            try
            {
                decimal tarifa = ObtenerTarifaPorTipo(tipo);
                lblTarifa.Text = "RD$ " + tarifa;
            }
            catch (ArgumentException)
            {
                lblTarifa.Text = "RD$ ---";
            }
        }

        private string ObtenerIconoPorTipo(string tipo)
        {
            switch (tipo)
            {
                case "Sencilla":
                    return "S";
                case "Doble":
                    return "D";
                case "Suite":
                    return "VIP";
                default:
                    // El formulario no se rompe ni queda vacío.
                    return "?";
            }
        }

        private decimal ObtenerTarifaPorTipo(string tipo)
        {
            switch (tipo)
            {
                case "Sencilla":
                    return 1500m;
                case "Doble":
                    return 2500m;
                case "Suite":
                    return 4500m;
                default:
                    throw new ArgumentException("Tipo de habitación no válido: " + tipo);
            }
        }

        private void EstadoDeColor(string estado, Label etiqueta)
        {
            switch (estado)
            {
                case "Disponible":
                    etiqueta.ForeColor = Color.Green;
                    break;
                case "Ocupada":
                    etiqueta.ForeColor = Color.Red;
                    break;
                case "Reservada":
                    etiqueta.ForeColor = Color.DarkOrange;
                    break;
                case "Limpieza":
                    etiqueta.ForeColor = Color.Blue;
                    break;
                default:
                    etiqueta.ForeColor = Color.Gray;
                    break;
            }
        }
        private void ConfigurarBotonesPorEstado(string estado)
        {
            // Primero se apagan los 4; el switch solo prende el correcto.
            btnReservar.Enabled = false;
            btnCheckIn.Enabled = false;
            btnCheckOut.Enabled = false;
            BtnLimpieza.Enabled = false;

            switch (estado)
            {
                case "Disponible":
                    btnReservar.Enabled = true;
                    break;
                case "Reservada":
                    btnCheckIn.Enabled = true; // llega el huésped
                    break;
                case "Ocupada":
                    btnCheckOut.Enabled = true;
                    break;
                case "Limpieza":
                    BtnLimpieza.Enabled = true; // vuelve a Disponible
                    break;
                default:
                    break;
            }
        }
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string accion = cbxAccion.Text;
            string mensaje;

            switch (accion)
            {
                case "Reservar":
                    mensaje = "¿Desea crear una reserva para esta habitación?";
                    break;
                case "Check-In":
                    mensaje = "¿Desea registrar la entrada del huésped?";
                    break;
                case "Check-Out":
                    mensaje = "¿Desea registrar la salida y liberar la habitación?";
                    break;
                case "Marcar Limpia":
                    mensaje = "¿Desea marcar esta habitación como limpia y disponible?";
                    break;
                default:
                    MessageBox.Show("Debe seleccionar una acción válida.", "Acción requerida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            DialogResult respuesta = MessageBox.Show(mensaje, "Confirmar acción",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                MessageBox.Show("Acción confirmada.", "Confirmado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Si responde "No", no pasa nada — permite arrepentirse.
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Aunque Habitacion.Numero es string, validamos que lo
                // escrito "parezca" un número real antes de buscar — así
                // seguimos atrapando basura como "305a".
                int numeroValidado = int.Parse(tbxNumeroHabitacion.Text);
                string numeroBuscado = numeroValidado.ToString();

                Habitacion encontrada = null;
                foreach (Habitacion habitacion in habitaciones)
                {
                    if (habitacion.Numero == numeroBuscado)
                    {
                        encontrada = habitacion;
                        break;
                    }
                }

                if (encontrada == null)
                {
                    _habitacionActual = null;
                    lblMensajeBusqueda.Text = "Sin habitación seleccionada";
                    LblEstadoHabitacion.Text = "Estado:  -";
                    LblEstadoHabitacion.ForeColor = Color.Black;
                    MessageBox.Show("No existe una habitación con ese número.",
                        "Habitación no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _habitacionActual = encontrada;
                lblMensajeBusqueda.Text = encontrada.Tipo + " · Piso " + encontrada.Piso;
                LblEstadoHabitacion.Text = "Estado: " + encontrada.Estado;

                // Reutiliza los métodos de los PASOS 5 y 6, tal como pide el reto.
                EstadoDeColor(encontrada.Estado, LblEstadoHabitacion);
                ConfigurarBotonesPorEstado(encontrada.Estado);
            }
            catch (FormatException)
            {
                MessageBox.Show("Debe escribir un número válido. Ejemplo: 301",
                    "Número inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Ese número es demasiado grande.",
                    "Número inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            btnGuardarCambios.Enabled = false; // deshabilitado ANTES de intentar guardar

            try
            {
                if (_habitacionActual == null)
                {
                    MessageBox.Show("Primero debe buscar una habitación.",
                        "Habitación requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_habitacionActual.Estado == "Ocupada")
                {
                    throw new HabitacionOcupadaException(_habitacionActual.Numero);
                }

                MessageBox.Show("Habitación guardada correctamente.", "Guardado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HabitacionOcupadaException ex)
            {
                // Catch específico ANTES que el genérico.
                MessageBox.Show("No se puede guardar la habitación " + ex.NumeroHabitacion + " porque está ocupada.",
                    "Habitación ocupada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Se ejecuta SIEMPRE, pase lo que pase arriba.
                btnGuardarCambios.Enabled = true;
            }
        }
    
        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

    }
}
