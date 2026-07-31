using HotelZormat.Negocio;
using HotelZormat.Negocio.Modelo;
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
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;

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
                // FormatException primero en el resto de la app; aquí ArgumentException
                // cubre los campos vacíos, que es la validación propia de este formulario.
                lblMensaje.Text = ex.Message;
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
