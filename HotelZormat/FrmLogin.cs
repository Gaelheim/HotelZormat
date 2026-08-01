// CÃ©dula: 40232840757
using HotelZormat.Negocio;
using HotelZormat.Modelos;
using HotelZormat.Negocio.Servicios;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelZormat
{
    public partial class FrmLogin : Form
    {
        private readonly AutenticacionService _autenticacionService = new AutenticacionService();

        public FrmLogin()
        {
            InitializeComponent();
            ThemeHelper.AplicarTema(this);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;

            try
            {
                Usuario usuario = _autenticacionService.Login(txtUsuario.Text.Trim(), txtPassword.Text);

                if (usuario == null)
                {
                    lblMensaje.Text = "Usuario o contraseÃ±a incorrectos.";
                    return;
                }

                Sesion.UsuarioActual = usuario;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                lblMensaje.Text = ex.Message;
            }
            catch (FormatException ex)
            {
                lblMensaje.Text = "Error de formato: " + ex.Message;
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

