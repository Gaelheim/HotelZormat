using HotelZormat.Negocio.Servicios;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelZormat
{
    public partial class FrmReportes : Form
    {
        private readonly ReporteService _reporteService = new ReporteService();

        public FrmReportes()
        {
            InitializeComponent();
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
                MessageBox.Show(ex.Message, "Rango de fechas inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
