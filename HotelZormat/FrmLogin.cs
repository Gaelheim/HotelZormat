
using HotelZormat.Negocio;
using HotelZormat.Modelos;
using HotelZormat.Negocio.Servicios;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelZormat
{
    // 40232840757
    public partial class FrmLogin : Form
    {
        private readonly AutenticacionService _autenticacionService = new AutenticacionService();

        public FrmLogin()
        {
            InitializeComponent();
            ThemeHelper.AplicarTema(this);
        }

        // Evento que se dispara al hacer clic en el botón "Ingresar"
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;

            // Validar que los campos de usuario y contraseña no estén vacíos
            try
            {
                Usuario usuario = _autenticacionService.Login(txtUsuario.Text.Trim(), txtPassword.Text);

                if (usuario == null)
                {
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
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

