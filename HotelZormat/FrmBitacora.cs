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
        private readonly BitacoraService _bitacoraService = new BitacoraService(); // Instancia del servicio de bitácora

        public FrmBitacora()
        {
            InitializeComponent();
            this.Load += FrmBitacora_Load; 
        }

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
          
            CargarBitacora();
        }


        // Método para cargar la bitácora en el DataGridView
        private void CargarBitacora()
        {
            // Verificar si el usuario actual tiene permiso para ver la bitácora
            try
            {
                bool puedeVer = Sesion.UsuarioActual != null && Sesion.UsuarioActual.PuedeVerBitacora;

                dgvBitacora.AutoGenerateColumns = true;

                dgvBitacora.DataSource = _bitacoraService.Listar(puedeVer);
            }
            // Manejo de excepciones específicas

            catch (UnauthorizedAccessException ex) // Captura la excepción de acceso denegado
            {
                MessageBox.Show(ex.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            catch (SqlException ex) // Captura la excepción de SQL
            {
                MessageBox.Show(ex.Message,"Base de datos",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception ex) // Captura cualquier otra excepción
            {
                MessageBox.Show( ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        // Evento del botón de refrescar para recargar la bitácora
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarBitacora();
        }
    }
}


