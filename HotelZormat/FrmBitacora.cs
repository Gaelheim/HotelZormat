
using HotelZormat.Negocio;
using HotelZormat.Negocio.Servicios;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace HotelZormat
{
    //40232840757 
    //Consulta de la bitacora (solo adm puede verlo)
    public partial class FrmBitacora : Form
    {
        private readonly BitacoraService _bitacoraService = new BitacoraService(); // Instancia del servicio de bitacora

        public FrmBitacora()
        {
            InitializeComponent();
            ThemeHelper.AplicarTema(this);
            this.Load += FrmBitacora_Load; 
        }

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
          
            CargarBitacora();
        }


        // Metodo para cargar la bitacora en el DataGridView
        private void CargarBitacora()
        {
            // Verificar si el usuario actual tiene permiso para ver la bitacora
            try
            {
                bool puedeVer = Sesion.UsuarioActual != null && Sesion.UsuarioActual.PuedeVerBitacora;

                dgvBitacora.AutoGenerateColumns = true;

                dgvBitacora.DataSource = _bitacoraService.Listar(puedeVer);
            }
            // Manejo de excepciones especificas

            catch (UnauthorizedAccessException ex) // Captura la excepcion de acceso denegado
            {
                MessageBox.Show(ex.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: " + ex.Message, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex) // Captura la excepcion de SQL
            {
                MessageBox.Show(ex.Message,"Base de datos",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception ex) // Captura cualquier otra excepcion
            {
                MessageBox.Show( ex.ToString(), "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        // Evento del botón de refrescar para recargar la bitacora
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarBitacora();
        }
    }
}



