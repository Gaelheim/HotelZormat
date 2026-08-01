using HotelZormat.Negocio;
using HotelZormat.Modelos;
using HotelZormat.Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

//40232840757
namespace HotelZormat
{
    public partial class FrmReservas : Form
    {
        private readonly HabitacionService _habitacionService = new HabitacionService();
        private readonly HuespedService _huespedService = new HuespedService();
        private readonly ReservaService _reservaService = new ReservaService();

        private List<Habitacion> _disponibles = new List<Habitacion>();
        private List<Huesped> _huespedes = new List<Huesped>();
        private List<Temporada> _temporadas = new List<Temporada>();
        private int _ultimaReservaCreadaId;

        public FrmReservas() : this(null) { }

        public FrmReservas(string numeroHabitacionPreseleccionada)
        {
            InitializeComponent();
            ThemeHelper.AplicarTema(this);
            this.Load += (s, e) => CargarDatos(numeroHabitacionPreseleccionada);
        }

        // Método para cargar los datos iniciales en los controles
        private void CargarDatos(string numeroPreseleccionado)
        {
            try
            {
                _disponibles = _habitacionService.Listar(null, "Disponible");
                cboHabitacion.Items.Clear();

                // Agregar las habitaciones disponibles al ComboBox
                foreach (var habitacion in _disponibles)
                {
                    cboHabitacion.Items.Add(habitacion.Numero + " - " + habitacion.TipoHabitacionNombre);
                }
                // Seleccionar la habitación preseleccionada si se proporciona
                if (!string.IsNullOrEmpty(numeroPreseleccionado))
                {
                    int indice = _disponibles.FindIndex(h => h.Numero == numeroPreseleccionado);
                    if (indice >= 0) cboHabitacion.SelectedIndex = indice;
                }
                // Si no hay preselección, seleccionar la primera habitación disponible
                else if (cboHabitacion.Items.Count > 0)
                {
                    cboHabitacion.SelectedIndex = 0;
                }

                _huespedes = _huespedService.Listar();
                cboHuesped.Items.Clear();
                // Agregar los huéspedes al ComboBox
                foreach (var huesped in _huespedes)
                {
                    cboHuesped.Items.Add(huesped.NombreCompleto() + " (" + huesped.NumeroDocumento + ")");
                }
                // Seleccionar el primer huésped si hay alguno en la lista
                if (cboHuesped.Items.Count > 0) cboHuesped.SelectedIndex = 0;

                _temporadas = _reservaService.ListarTemporadas();
                cboTemporada.Items.Clear();
                // Agregar las temporadas al ComboBox
                foreach (var temporada in _temporadas)
                {
                    cboTemporada.Items.Add(temporada.Nombre);
                }
                // Seleccionar la primera temporada si hay alguna en la lista
                if (cboTemporada.Items.Count > 0) cboTemporada.SelectedIndex = 0;

                dtpCheckIn.Value = DateTime.Today;
                dtpCheckOut.Value = DateTime.Today.AddDays(1);

                CargarProximas();
            }
            
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error de conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProximas()
        {
            dgvProximas.DataSource = _reservaService.ListarProximas7Dias();
        }

        private void btnCrearReserva_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado habitación, huésped y temporada
            try
            {
                if (cboHabitacion.SelectedIndex < 0 || cboHuesped.SelectedIndex < 0 || cboTemporada.SelectedIndex < 0)
                {
                    MessageBox.Show("Debe seleccionar habitación, huésped y temporada.", "Datos incompletos",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Habitacion habitacion = _disponibles[cboHabitacion.SelectedIndex];
                Huesped huesped = _huespedes[cboHuesped.SelectedIndex];
                Temporada temporada = _temporadas[cboTemporada.SelectedIndex];

                var reserva = new Reserva
                {
                    HabitacionId = habitacion.Id,
                    HuespedId = huesped.Id,
                    TemporadaId = temporada.Id,
                    FechaCheckIn = dtpCheckIn.Value.Date,
                    FechaCheckOut = dtpCheckOut.Value.Date,
                    UsuarioCreacionId = Sesion.UsuarioActual != null ? Sesion.UsuarioActual.Id : 1
                };

                Reserva creada = _reservaService.CrearReserva(reserva);
                _ultimaReservaCreadaId = creada.Id;
                btnConfirmarReserva.Enabled = true;

                lblResultado.Text = "Reserva #" + creada.Id + ": " + creada.Noches + " noche(s), monto estimado RD$ " +
                    creada.MontoEstimado.ToString("N2");

                CargarDatos(null);
            }
            catch (HabitacionOcupadaException ex)
            {
                MessageBox.Show(ex.Message, "Habitación no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Fechas inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: " + ex.Message, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error de conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfirmarReserva_Click(object sender, EventArgs e)
        {
            // Validar que se haya creado una reserva antes de confirmar
            try
            {
                if (_ultimaReservaCreadaId <= 0)
                {
                    MessageBox.Show("Primero debe crear una reserva.", "Reserva requerida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _reservaService.Confirmar(_ultimaReservaCreadaId);
                MessageBox.Show("Reserva confirmada. Ya puede hacer Check-In desde Gestión de Habitaciones.",
                    "Confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnConfirmarReserva.Enabled = false;
                CargarProximas();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: " + ex.Message, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error de conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

