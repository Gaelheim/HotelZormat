using HotelZormat.Modelos;
using HotelZormat.Negocio.Servicios;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

// CÃ©dula: 40232840757
namespace HotelZormat
{
    public partial class FrmHuespedes : Form
    {
        private readonly HuespedService _huespedService = new HuespedService();
        private DataTable _tablaHuespedes;
        private int _idSeleccionado;

        public FrmHuespedes()
        {
            InitializeComponent();
            ThemeHelper.AplicarTema(this);
            this.Load += (s, e) => CargarLista(null);
        }

        private void CargarLista(string filtro)
        {
            try
            {
                var huespedes = string.IsNullOrWhiteSpace(filtro)
                    ? _huespedService.Listar()
                    : _huespedService.Buscar(filtro);

                _tablaHuespedes = new DataTable();
                _tablaHuespedes.Columns.Add("Id", typeof(int));
                _tablaHuespedes.Columns.Add("Nombre", typeof(string));
                _tablaHuespedes.Columns.Add("Apellido", typeof(string));
                _tablaHuespedes.Columns.Add("TipoDocumento", typeof(string));
                _tablaHuespedes.Columns.Add("NumeroDocumento", typeof(string));
                _tablaHuespedes.Columns.Add("Nacionalidad", typeof(string));
                _tablaHuespedes.Columns.Add("RangoClub", typeof(string));
                _tablaHuespedes.Columns.Add("PuntosClub", typeof(string));

                foreach (Huesped huesped in huespedes)
                {
                    ClubMillas club = _huespedService.ObtenerMillasClub(huesped.Id);
                    _tablaHuespedes.Rows.Add(huesped.Id, huesped.Nombre, huesped.Apellido,
                        huesped.TipoDocumento, huesped.NumeroDocumento, huesped.Nacionalidad,
                        club.Rango, club.PuntosAcumulados.ToString("N0") + " pts");
                }

                dgvHuespedes.DataSource = _tablaHuespedes;
                if (dgvHuespedes.Columns["Id"] != null)
                {
                    dgvHuespedes.Columns["Id"].Visible = false;
                }
                if (dgvHuespedes.Columns["RangoClub"] != null)
                {
                    dgvHuespedes.Columns["RangoClub"].HeaderText = "Rango Club";
                }
                if (dgvHuespedes.Columns["PuntosClub"] != null)
                {
                    dgvHuespedes.Columns["PuntosClub"].HeaderText = "Puntos Club";
                }
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarLista(txtBuscar.Text);
        }

        private void dgvHuespedes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHuespedes.CurrentRow == null || dgvHuespedes.CurrentRow.DataBoundItem == null) return;

            DataRowView fila = (DataRowView)dgvHuespedes.CurrentRow.DataBoundItem;
            _idSeleccionado = Convert.ToInt32(fila["Id"]);
            txtNombre.Text = fila["Nombre"].ToString();
            txtApellido.Text = fila["Apellido"].ToString();
            cboTipoDocumento.SelectedItem = fila["TipoDocumento"].ToString();
            txtNumeroDocumento.Text = fila["NumeroDocumento"].ToString();
            txtNacionalidad.Text = fila["Nacionalidad"].ToString();

            // Cargar y mostrar los puntos club
            ClubMillas club = _huespedService.ObtenerMillasClub(_idSeleccionado);
            lblPuntosClub.Text = "Club: " + club.Rango + " | " + club.PuntosAcumulados.ToString("N0") + " pts | " + club.NochesAcumuladas + " noches";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            _idSeleccionado = 0;
            txtNombre.Clear();
            txtApellido.Clear();
            cboTipoDocumento.SelectedIndex = -1;
            txtNumeroDocumento.Clear();
            txtNacionalidad.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            lblPuntosClub.Text = "Club: Hierro | 0 pts | 0 noches";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTipoDocumento.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar el tipo de documento.", "Dato requerido",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var huesped = new Huesped
                {
                    Id = _idSeleccionado,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    TipoDocumento = cboTipoDocumento.SelectedItem.ToString(),
                    NumeroDocumento = txtNumeroDocumento.Text.Trim(),
                    Nacionalidad = txtNacionalidad.Text.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim()
                };

                if (_idSeleccionado > 0)
                {
                    _huespedService.Actualizar(huesped);
                }
                else
                {
                    _huespedService.Crear(huesped);
                }

                MessageBox.Show("HuÃ©sped guardado correctamente.", "Guardado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarLista(txtBuscar.Text);
            }
            catch (FormatException ex)
            {
                // CÃ©dula con formato invÃ¡lido (menos/mÃ¡s de 11 dÃ­gitos, o no numÃ©rica).
                MessageBox.Show(ex.Message, "CÃ©dula invÃ¡lida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un huÃ©sped de la lista.", "HuÃ©sped requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("Â¿Confirma que desea eliminar este huÃ©sped?",
                "Confirmar eliminaciÃ³n", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _huespedService.Eliminar(_idSeleccionado);
                btnNuevo_Click(sender, e);
                CargarLista(txtBuscar.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: " + ex.Message, "Error de formato",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un huÃ©sped de la lista.", "HuÃ©sped requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable historial = _huespedService.HistorialEstadias(_idSeleccionado);
                using (var frm = new Form())
                {
                    frm.Text = "Historial de estadÃ­as";
                    frm.Width = 500;
                    frm.Height = 300;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    var grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, DataSource = historial, AllowUserToAddRows = false };
                    frm.Controls.Add(grid);
                    frm.ShowDialog(this);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: " + ex.Message, "Error de formato",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}

