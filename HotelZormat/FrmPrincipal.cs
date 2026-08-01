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
    public partial class FrmPrincipal : Form
    {
        private readonly HabitacionService _habitacionService = new HabitacionService();

        public FrmPrincipal()
        {
            InitializeComponent();
            ThemeHelper.AplicarTema(this);
            this.Load += FrmPrincipal_Load;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            if (Sesion.UsuarioActual != null)
            {
                lblBienvenida.Text = "Bienvenido, " + Sesion.UsuarioActual.NombreCompleto +
                    " (" + Sesion.UsuarioActual.RolNombre + ")";
                btnBitacora.Visible = Sesion.UsuarioActual.PuedeVerBitacora;
            }

            CargarDashboard();
        }

        /// <summary>Reto (switch): pinta cada tarjeta del panel segÃºn el Estado de la habitaciÃ³n.</summary>
        private void CargarDashboard()
        {
            try
            {
                pnlDashboard.Controls.Clear();
                List<Habitacion> habitaciones = _habitacionService.Listar(null, null);

                foreach (Habitacion habitacion in habitaciones)
                {
                    var tarjeta = new Panel
                    {
                        Width = 110,
                        Height = 70,
                        Margin = new Padding(5),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    switch (habitacion.Estado)
                    {
                        case "Disponible":
                            tarjeta.BackColor = Color.LightGreen;
                            break;
                        case "Ocupada":
                            tarjeta.BackColor = Color.LightCoral;
                            break;
                        case "Reservada":
                            tarjeta.BackColor = Color.Orange;
                            break;
                        case "Limpieza":
                            tarjeta.BackColor = Color.LightSkyBlue;
                            break;
                        default:
                            tarjeta.BackColor = Color.LightGray;
                            break;
                    }

                    var lbl = new Label
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Text = habitacion.Numero + "\n" + habitacion.TipoHabitacionNombre + "\n" + habitacion.Estado
                    };
                    tarjeta.Controls.Add(lbl);
                    pnlDashboard.Controls.Add(tarjeta);
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

        private void btnRefrescarDashboard_Click(object sender, EventArgs e)
        {
            CargarDashboard();
        }

        private void btnHabitaciones_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmGestionHabitaciones())
            {
                frm.ShowDialog(this);
            }
            CargarDashboard();
        }

        private void btnHuespedes_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmHuespedes())
            {
                frm.ShowDialog(this);
            }
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmReservas())
            {
                frm.ShowDialog(this);
            }
            CargarDashboard();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmReportes())
            {
                frm.ShowDialog(this);
            }
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmBitacora())
            {
                frm.ShowDialog(this);
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Sesion.CerrarSesion();
            this.Hide();

            using (var frmLogin = new FrmLogin())
            {
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                    FrmPrincipal_Load(sender, e);
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}

