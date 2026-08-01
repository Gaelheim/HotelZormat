using HotelZormat.Negocio.Servicios;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

// CÃ©dula: 40232840757
namespace HotelZormat
{
    public partial class FrmReportes : Form
    {
        private readonly ReporteService _reporteService = new ReporteService();

        public FrmReportes()
        {
            InitializeComponent();
            ThemeHelper.AplicarTema(this);
            this.Load += (s, e) =>
            {
                dtpDesde.Value = DateTime.Today.AddDays(-7);
                dtpHasta.Value = DateTime.Today;
                CargarOcupacion();
            };
        }

        private void CargarOcupacion()
        {
            try
            {
                dgvOcupacion.DataSource = _reporteService.OcupacionDelDia();
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

        private void btnRefrescarOcupacion_Click(object sender, EventArgs e)
        {
            CargarOcupacion();
        }

        private void btnGenerarIngresos_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow resultado = _reporteService.IngresosPorRango(dtpDesde.Value, dtpHasta.Value);

                if (resultado == null)
                {
                    lblResultadoIngresos.Text = "Sin facturas en ese rango.";
                    return;
                }

                decimal totalFacturado = resultado.Table.Columns.Contains("TotalFacturado")
                    ? Convert.ToDecimal(resultado["TotalFacturado"])
                    : Convert.ToDecimal(resultado[0]);
                int cantidadFacturas = resultado.Table.Columns.Contains("CantidadFacturas")
                    ? Convert.ToInt32(resultado["CantidadFacturas"])
                    : 0;

                lblResultadoIngresos.Text = "Ingresos: RD$ " + totalFacturado.ToString("N2") +
                    " (" + cantidadFacturas + " factura(s))";
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Rango de fechas invÃ¡lido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}

