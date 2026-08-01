// CÃ©dula: 40232840757
using HotelZormat.Negocio;
using HotelZormat.Modelos;
using HotelZormat.Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelZormat
{
    public partial class FrmGestionHabitaciones : Form
    {
        private readonly HabitacionService _habitacionService = new HabitacionService();
        private readonly ReservaService _reservaService = new ReservaService();
        private readonly CheckInOutService _checkInOutService = new CheckInOutService();

        private Habitacion _habitacionActual;
        private List<TipoHabitacion> _tipos = new List<TipoHabitacion>();
        private List<Habitacion> _habitaciones = new List<Habitacion>();

        public FrmGestionHabitaciones()
        {
            InitializeComponent();
            ThemeHelper.AplicarTema(this);
            this.Load += FrmGestionHabitaciones_Load;
            this.cboTipo.SelectedIndexChanged += CbxTipo_SelectedIndexChanged;
        }

        private void FrmGestionHabitaciones_Load(object sender, EventArgs e)
        {
            try
            {
                CargarTipos();
                CargarHabitacionesPiso3();

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

                // Eliminar solo lo puede hacer el rol Administrador (PuedeEliminarHabitaciones = 1).
                bool puedeEliminar = Sesion.UsuarioActual != null && Sesion.UsuarioActual.PuedeEliminarHabitaciones;
                btnEliminarHabitacion.Enabled = puedeEliminar;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos al cargar habitaciones: " + ex.Message,
                    "Error de conexiÃ³n", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OcurriÃ³ un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarTipos()
        {
            _tipos = _habitacionService.ListarTipos();

            cboTipo.Items.Clear();
            // Reto 01 (foreach): llenado del combo a partir de la lista de tipos traÃ­da de la BD.
            foreach (var tipo in _tipos)
            {
                cboTipo.Items.Add(tipo.Nombre);
            }
            if (cboTipo.Items.Count > 0)
            {
                cboTipo.SelectedIndex = 0; // dispara SelectedIndexChanged
            }
        }

        private void CargarHabitacionesPiso3()
        {
            _habitaciones = _habitacionService.Listar(null, null);

            // Se limpia ANTES de recorrer: asÃ­, si el mÃ©todo se llama de
            // nuevo, la lista nunca queda duplicada.
            lstHabitaciones.Items.Clear();

            foreach (Habitacion habitacion in _habitaciones)
            {
                if (habitacion.Piso == 3)
                {
                    string linea = habitacion.Numero + " - " + habitacion.TipoHabitacionNombre;
                    lstHabitaciones.Items.Add(linea);
                }
            }
        }

        private void CbxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipo = cboTipo.Text;

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
                    // El formulario no se rompe ni queda vacÃ­o.
                    return "?";
            }
        }

        private decimal ObtenerTarifaPorTipo(string tipo)
        {
            TipoHabitacion encontrado = _tipos.FirstOrDefault(t => t.Nombre == tipo);
            if (encontrado == null)
            {
                throw new ArgumentException("Tipo de habitaciÃ³n no vÃ¡lido: " + tipo);
            }
            return encontrado.TarifaBase;
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
            btnReservar.Enabled = false;
            btnCheckIn.Enabled = false;
            btnCheckOut.Enabled = false;
            btnLimpieza.Enabled = false;

            switch (estado)
            {
                case "Disponible":
                    btnReservar.Enabled = true;
                    break;
                case "Reservada":
                    btnCheckIn.Enabled = true;
                    break;
                case "Ocupada":
                    btnCheckOut.Enabled = true;
                    break;
                case "Limpieza":
                    btnLimpieza.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (_habitacionActual == null)
            {
                MessageBox.Show("Primero debe buscar una habitaciÃ³n.", "HabitaciÃ³n requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string accion = cbxAccion.Text;
            string mensaje;

            switch (accion)
            {
                case "Reservar":
                    mensaje = "Â¿Desea crear una reserva para esta habitaciÃ³n?";
                    break;
                case "Check-In":
                    mensaje = "Â¿Desea registrar la entrada del huÃ©sped?";
                    break;
                case "Check-Out":
                    mensaje = "Â¿Desea registrar la salida y liberar la habitaciÃ³n?";
                    break;
                case "Marcar Limpia":
                    mensaje = "Â¿Desea marcar esta habitaciÃ³n como limpia y disponible?";
                    break;
                default:
                    MessageBox.Show("Debe seleccionar una acciÃ³n vÃ¡lida.", "AcciÃ³n requerida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            DialogResult respuesta = MessageBox.Show(mensaje, "Confirmar acciÃ³n",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                EjecutarAccion(accion);
            }
            catch (HabitacionOcupadaException ex)
            {
                MessageBox.Show(ex.Message, "HabitaciÃ³n ocupada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Dato invÃ¡lido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error de conexiÃ³n",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo completar la acciÃ³n",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EjecutarAccion(string accion)
        {
            int usuarioId = Sesion.UsuarioActual != null ? Sesion.UsuarioActual.Id : 0;

            switch (accion)
            {
                case "Reservar":
                    using (var frm = new FrmReservas(_habitacionActual.Numero))
                    {
                        frm.ShowDialog(this);
                    }
                    break;

                case "Check-In":
                    Reserva confirmada = _reservaService.ObtenerConfirmadaPorHabitacion(_habitacionActual.Id);
                    if (confirmada == null)
                    {
                        MessageBox.Show("No hay una reserva Confirmada para esta habitaciÃ³n.",
                            "Check-In no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _checkInOutService.RealizarCheckIn(confirmada.Id, usuarioId);
                    MessageBox.Show("Check-In registrado correctamente.", "Check-In",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case "Check-Out":
                    Estadia activa = _checkInOutService.ObtenerActivaPorHabitacion(_habitacionActual.Id);
                    if (activa == null)
                    {
                        MessageBox.Show("No hay una estadÃ­a activa para esta habitaciÃ³n.",
                            "Check-Out no disponible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Factura factura = _checkInOutService.RealizarCheckOut(activa.Id, usuarioId);
                    using (var frmFactura = new FrmFactura(factura))
                    {
                        frmFactura.ShowDialog(this);
                    }
                    break;

                case "Marcar Limpia":
                    _habitacionService.MarcarLimpia(_habitacionActual.Id);
                    MessageBox.Show("HabitaciÃ³n marcada como Disponible.", "Limpieza",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }

            RecargarHabitacionActual();
            CargarHabitacionesPiso3();
        }

        private void RecargarHabitacionActual()
        {
            if (_habitacionActual == null) return;
            _habitacionActual = _habitacionService.ObtenerPorNumero(_habitacionActual.Numero);
            if (_habitacionActual != null)
            {
                LblEstadoHabitacion.Text = "Estado: " + _habitacionActual.Estado;
                EstadoDeColor(_habitacionActual.Estado, LblEstadoHabitacion);
                ConfigurarBotonesPorEstado(_habitacionActual.Estado);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int numeroValidado = int.Parse(txtNumeroHabitacion.Text);
                string numeroBuscado = numeroValidado.ToString();

                Habitacion encontrada = _habitacionService.ObtenerPorNumero(numeroBuscado);

                if (encontrada == null)
                {
                    _habitacionActual = null;
                    lblMensajeBusqueda.Text = "Sin habitaciÃ³n seleccionada";
                    LblEstadoHabitacion.Text = "Estado:  -";
                    LblEstadoHabitacion.ForeColor = Color.Black;
                    MessageBox.Show("No existe una habitaciÃ³n con ese nÃºmero.",
                        "HabitaciÃ³n no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _habitacionActual = encontrada;
                lblMensajeBusqueda.Text = encontrada.TipoHabitacionNombre + " Â· Piso " + encontrada.Piso;
                LblEstadoHabitacion.Text = "Estado: " + encontrada.Estado;

                EstadoDeColor(encontrada.Estado, LblEstadoHabitacion);
                ConfigurarBotonesPorEstado(encontrada.Estado);
            }
            catch (FormatException)
            {
                MessageBox.Show("Debe escribir un nÃºmero vÃ¡lido. Ejemplo: 301",
                    "NÃºmero invÃ¡lido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Ese nÃºmero es demasiado grande.",
                    "NÃºmero invÃ¡lido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error de conexiÃ³n",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OcurriÃ³ un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            btnGuardarCambios.Enabled = false;

            try
            {
                if (_habitacionActual == null)
                {
                    MessageBox.Show("Primero debe buscar una habitaciÃ³n.",
                        "HabitaciÃ³n requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TipoHabitacion tipoSeleccionado = _tipos.FirstOrDefault(t => t.Nombre == cboTipo.Text);
                if (tipoSeleccionado != null)
                {
                    _habitacionActual.TipoHabitacionId = tipoSeleccionado.Id;
                }

                _habitacionService.Actualizar(_habitacionActual);

                MessageBox.Show("HabitaciÃ³n guardada correctamente.", "Guardado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarHabitacionesPiso3();
            }
            catch (HabitacionOcupadaException ex)
            {
                MessageBox.Show("No se puede guardar la habitaciÃ³n " + ex.NumeroHabitacion + " porque estÃ¡ ocupada.",
                    "HabitaciÃ³n ocupada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Dato invÃ¡lido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error de conexiÃ³n",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OcurriÃ³ un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardarCambios.Enabled = true;
            }
        }

        private void btnCrearHabitacion_Click(object sender, EventArgs e)
        {
            try
            {
                int piso = int.Parse(txtPisoCrud.Text);
                int capacidad = int.Parse(txtCapacidadCrud.Text);

                TipoHabitacion tipoSeleccionado = _tipos.FirstOrDefault(t => t.Nombre == cboTipo.Text);
                if (tipoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un tipo de habitaciÃ³n vÃ¡lido.", "Tipo requerido",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nueva = new Habitacion
                {
                    Numero = txtNumeroCrud.Text.Trim(),
                    TipoHabitacionId = tipoSeleccionado.Id,
                    Piso = piso,
                    Capacidad = capacidad,
                    Estado = "Disponible"
                };

                _habitacionService.Crear(nueva);

                MessageBox.Show("HabitaciÃ³n creada correctamente.", "Creado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNumeroCrud.Clear();
                txtPisoCrud.Clear();
                txtCapacidadCrud.Clear();
                CargarHabitacionesPiso3();
            }
            catch (FormatException)
            {
                MessageBox.Show("Piso y capacidad deben ser nÃºmeros.", "Dato invÃ¡lido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dato invÃ¡lido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error de conexiÃ³n",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OcurriÃ³ un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarHabitacion_Click(object sender, EventArgs e)
        {
            if (_habitacionActual == null)
            {
                MessageBox.Show("Primero debe buscar la habitaciÃ³n que desea eliminar.",
                    "HabitaciÃ³n requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "Â¿Confirma que desea eliminar la habitaciÃ³n " + _habitacionActual.Numero + "? Esta acciÃ³n no se puede deshacer.",
                "Confirmar eliminaciÃ³n", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool puedeEliminar = Sesion.UsuarioActual != null && Sesion.UsuarioActual.PuedeEliminarHabitaciones;
                _habitacionService.Eliminar(_habitacionActual.Id, puedeEliminar);

                MessageBox.Show("HabitaciÃ³n eliminada.", "Eliminado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _habitacionActual = null;
                lblMensajeBusqueda.Text = "Sin habitaciÃ³n seleccionada";
                LblEstadoHabitacion.Text = "Estado:  -";
                CargarHabitacionesPiso3();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: " + ex.Message, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error de base de datos: " + ex.Message, "Error de conexiÃ³n",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("OcurriÃ³ un error inesperado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

    }
}

