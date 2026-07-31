namespace HotelZormat
{
    partial class FrmFactura
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNCF = new System.Windows.Forms.Label();
            this.lblSubtotalCaption = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblITBISCaption = new System.Windows.Forms.Label();
            this.lblITBIS = new System.Windows.Forms.Label();
            this.lblPropinaCaption = new System.Windows.Forms.Label();
            this.lblPropina = new System.Windows.Forms.Label();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(160, 20);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Factura de consumo";
            //
            // lblNCF
            //
            this.lblNCF.AutoSize = true;
            this.lblNCF.Location = new System.Drawing.Point(20, 50);
            this.lblNCF.Name = "lblNCF";
            this.lblNCF.Size = new System.Drawing.Size(60, 13);
            this.lblNCF.TabIndex = 1;
            this.lblNCF.Text = "NCF: ---";
            //
            // lblSubtotalCaption
            //
            this.lblSubtotalCaption.AutoSize = true;
            this.lblSubtotalCaption.Location = new System.Drawing.Point(20, 85);
            this.lblSubtotalCaption.Name = "lblSubtotalCaption";
            this.lblSubtotalCaption.Size = new System.Drawing.Size(52, 13);
            this.lblSubtotalCaption.TabIndex = 2;
            this.lblSubtotalCaption.Text = "Subtotal:";
            //
            // lblSubtotal
            //
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(140, 85);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(46, 13);
            this.lblSubtotal.TabIndex = 3;
            this.lblSubtotal.Text = "RD$ 0";
            //
            // lblITBISCaption
            //
            this.lblITBISCaption.AutoSize = true;
            this.lblITBISCaption.Location = new System.Drawing.Point(20, 110);
            this.lblITBISCaption.Name = "lblITBISCaption";
            this.lblITBISCaption.Size = new System.Drawing.Size(70, 13);
            this.lblITBISCaption.TabIndex = 4;
            this.lblITBISCaption.Text = "ITBIS (18%):";
            //
            // lblITBIS
            //
            this.lblITBIS.AutoSize = true;
            this.lblITBIS.Location = new System.Drawing.Point(140, 110);
            this.lblITBIS.Name = "lblITBIS";
            this.lblITBIS.Size = new System.Drawing.Size(46, 13);
            this.lblITBIS.TabIndex = 5;
            this.lblITBIS.Text = "RD$ 0";
            //
            // lblPropinaCaption
            //
            this.lblPropinaCaption.AutoSize = true;
            this.lblPropinaCaption.Location = new System.Drawing.Point(20, 135);
            this.lblPropinaCaption.Name = "lblPropinaCaption";
            this.lblPropinaCaption.Size = new System.Drawing.Size(110, 13);
            this.lblPropinaCaption.TabIndex = 6;
            this.lblPropinaCaption.Text = "Propina legal (10%):";
            //
            // lblPropina
            //
            this.lblPropina.AutoSize = true;
            this.lblPropina.Location = new System.Drawing.Point(140, 135);
            this.lblPropina.Name = "lblPropina";
            this.lblPropina.Size = new System.Drawing.Size(46, 13);
            this.lblPropina.TabIndex = 7;
            this.lblPropina.Text = "RD$ 0";
            //
            // lblTotalCaption
            //
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTotalCaption.Location = new System.Drawing.Point(20, 165);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(43, 16);
            this.lblTotalCaption.TabIndex = 8;
            this.lblTotalCaption.Text = "Total:";
            //
            // lblTotal
            //
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(140, 165);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(54, 16);
            this.lblTotal.TabIndex = 9;
            this.lblTotal.Text = "RD$ 0";
            //
            // btnCerrar
            //
            this.btnCerrar.Location = new System.Drawing.Point(140, 200);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(90, 25);
            this.btnCerrar.TabIndex = 10;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            //
            // FrmFactura
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 245);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTotalCaption);
            this.Controls.Add(this.lblPropina);
            this.Controls.Add(this.lblPropinaCaption);
            this.Controls.Add(this.lblITBIS);
            this.Controls.Add(this.lblITBISCaption);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.lblSubtotalCaption);
            this.Controls.Add(this.lblNCF);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFactura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HotelZormat - Factura";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNCF;
        private System.Windows.Forms.Label lblSubtotalCaption;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblITBISCaption;
        private System.Windows.Forms.Label lblITBIS;
        private System.Windows.Forms.Label lblPropinaCaption;
        private System.Windows.Forms.Label lblPropina;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCerrar;
    }
}
