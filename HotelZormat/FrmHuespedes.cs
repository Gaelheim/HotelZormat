using HotelZormat.Negocio.Modelo;
using HotelZormat.Negocio.Servicios;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

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

                foreach (Huesped huesped in huespedes)
                {
                    _tablaHuespedes.Rows.Add(huesped.Id, huesped.Nombre, huesped.Apellido,
                        huesped.TipoDocumento, huesped.NumeroDocumento, huesped.Nacionalidad);
                }

                dgvHuespedes.DataSource = _tablaHuespedes;
                if (dgvHuespedes.Columns["Id"] != null)
                {
                    dgvHuespedes.Columns["Id"].Visible = false;
                }
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

                MessageBox.Show("Huésped guardado correctamente.", "Guardado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarLista(txtBuscar.Text);
            }
            catch (FormatException ex)
            {
                // Cédula con formato inválido (menos/más de 11 dígitos, o no numérica).
                MessageBox.Show(ex.Message, "Cédula inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un huésped de la lista.", "Huésped requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Confirma que desea eliminar este huésped?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion != DialogResult.Yes) return;

            try
            {
                _huespedService.Eliminar(_idSeleccionado);
                btnNuevo_Click(sender, e);
                CargarLista(txtBuscar.Text);
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

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MessageBox.Show("Seleccione un huésped de la lista.", "Huésped requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable historial = _huespedService.HistorialEstadias(_idSeleccionado);
                using (var frm = new Form())
                {
                    frm.Text = "Historial de estadías";
                    frm.Width = 500;
                    frm.Height = 300;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    var grid = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, DataSource = historial, AllowUserToAddRows = false };
                    frm.Controls.Add(grid);
                    frm.ShowDialog(this);
                }
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
