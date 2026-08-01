// CÃ©dula: 40232840757
using HotelZormat.Negocio;
using HotelZormat.Negocio.Servicios;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace HotelZormat
{
    //40232840757 
    //Consulta de la bitÃ¡cora (solo adm puede verlo)
    public partial class FrmBitacora : Form
    {
        private readonly BitacoraService _bitacoraService = new BitacoraService(); // Instancia del servicio de bitÃ¡cora

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


        // MÃ©todo para cargar la bitÃ¡cora en el DataGridView
        private void CargarBitacora()
        {
            // Verificar si el usuario actual tiene permiso para ver la bitÃ¡cora
            try
            {
                bool puedeVer = Sesion.UsuarioActual != null && Sesion.UsuarioActual.PuedeVerBitacora;

                dgvBitacora.AutoGenerateColumns = true;

                dgvBitacora.DataSource = _bitacoraService.Listar(puedeVer);
            }
            // Manejo de excepciones especÃ­ficas

            catch (UnauthorizedAccessException ex) // Captura la excepciÃ³n de acceso denegado
            {
                MessageBox.Show(ex.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error de formato: " + ex.Message, "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex) // Captura la excepciÃ³n de SQL
            {
                MessageBox.Show(ex.Message,"Base de datos",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception ex) // Captura cualquier otra excepciÃ³n
            {
                MessageBox.Show( ex.ToString(), "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        // Evento del botÃ³n de refrescar para recargar la bitÃ¡cora
        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarBitacora();
        }
    }
}



