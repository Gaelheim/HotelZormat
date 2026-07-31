using HotelZormat.Negocio.Modelo;
using System;
using System.Windows.Forms;

namespace HotelZormat
{
    /// <summary>Muestra en pantalla el desglose de la factura generada al cerrar una estadía (Check-Out).</summary>
    public partial class FrmFactura : Form
    {
        public FrmFactura(Factura factura)
        {
            InitializeComponent();

            if (factura != null)
            {
                lblNCF.Text = "NCF: " + factura.NCF;
                lblSubtotal.Text = "RD$ " + factura.Subtotal.ToString("N2");
                lblITBIS.Text = "RD$ " + factura.ITBIS.ToString("N2");
                lblPropina.Text = "RD$ " + factura.Propina.ToString("N2");
                lblTotal.Text = "RD$ " + factura.Total.ToString("N2");
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
