using HotelZormat.Negocio;
using HotelZormat.Negocio.Servicios;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace HotelZormat
{
    //40232840757 
    //Consulta de la bitácora (solo adm puede verlo)
    public partial class FrmBitacora : Form
    {
        private readonly BitacoraService _bitacoraService = new BitacoraService();

        public FrmBitacora()
        {
            InitializeComponent();
            this.Load += FrmBitacora_Load;
        }

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            bool puedeVer = Sesion.UsuarioActual != null && Sesion.UsuarioActual.PuedeVerBitacora;
            if (!puedeVer)
            {
                MessageBox.Show("Solo el rol Administrador puede consultar la bitácora.",
                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            CargarBitacora();
        }

        private void CargarBitacora()
        {
            try
            {
                bool puedeVer = Sesion.UsuarioActual != null && Sesion.UsuarioActual.PuedeVerBitacora;
                dgvBitacora.DataSource = _bitacoraService.Listar(puedeVer);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
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

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarBitacora();
        }
    }
}


