namespace HotelZormat
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnHabitaciones = new System.Windows.Forms.Button();
            this.btnHuespedes = new System.Windows.Forms.Button();
            this.btnReservas = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnBitacora = new System.Windows.Forms.Button();
            this.gbxDashboard = new System.Windows.Forms.GroupBox();
            this.pnlDashboard = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefrescarDashboard = new System.Windows.Forms.Button();
            this.gbxDashboard.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.Location = new System.Drawing.Point(12, 15);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(88, 17);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Bienvenido";
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(690, 12);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(98, 25);
            this.btnCerrarSesion.TabIndex = 1;
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // btnHabitaciones
            // 
            this.btnHabitaciones.Location = new System.Drawing.Point(12, 50);
            this.btnHabitaciones.Name = "btnHabitaciones";
            this.btnHabitaciones.Size = new System.Drawing.Size(150, 32);
            this.btnHabitaciones.TabIndex = 2;
            this.btnHabitaciones.Text = "Gestión de Habitaciones";
            this.btnHabitaciones.UseVisualStyleBackColor = true;
            this.btnHabitaciones.Click += new System.EventHandler(this.btnHabitaciones_Click);
            // 
            // btnHuespedes
            // 
            this.btnHuespedes.Location = new System.Drawing.Point(168, 50);
            this.btnHuespedes.Name = "btnHuespedes";
            this.btnHuespedes.Size = new System.Drawing.Size(120, 32);
            this.btnHuespedes.TabIndex = 3;
            this.btnHuespedes.Text = "Huéspedes";
            this.btnHuespedes.UseVisualStyleBackColor = true;
            this.btnHuespedes.Click += new System.EventHandler(this.btnHuespedes_Click);
            // 
            // btnReservas
            // 
            this.btnReservas.Location = new System.Drawing.Point(294, 50);
            this.btnReservas.Name = "btnReservas";
            this.btnReservas.Size = new System.Drawing.Size(120, 32);
            this.btnReservas.TabIndex = 4;
            this.btnReservas.Text = "Reservas";
            this.btnReservas.UseVisualStyleBackColor = true;
            this.btnReservas.Click += new System.EventHandler(this.btnReservas_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.Location = new System.Drawing.Point(420, 50);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(120, 32);
            this.btnReportes.TabIndex = 5;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnBitacora
            // 
            this.btnBitacora.Location = new System.Drawing.Point(546, 50);
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Size = new System.Drawing.Size(120, 32);
            this.btnBitacora.TabIndex = 6;
            this.btnBitacora.Text = "Bitácora (Admin)";
            this.btnBitacora.UseVisualStyleBackColor = true;
            this.btnBitacora.Click += new System.EventHandler(this.btnBitacora_Click);
            // 
            // gbxDashboard
            // 
            this.gbxDashboard.Controls.Add(this.pnlDashboard);
            this.gbxDashboard.Controls.Add(this.btnRefrescarDashboard);
            this.gbxDashboard.Location = new System.Drawing.Point(12, 92);
            this.gbxDashboard.Name = "gbxDashboard";
            this.gbxDashboard.Size = new System.Drawing.Size(776, 400);
            this.gbxDashboard.TabIndex = 7;
            this.gbxDashboard.TabStop = false;
            this.gbxDashboard.Text = "Dashboard de habitaciones ";
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.AutoScroll = true;
            this.pnlDashboard.Location = new System.Drawing.Point(6, 19);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Size = new System.Drawing.Size(764, 340);
            this.pnlDashboard.TabIndex = 0;
            // 
            // btnRefrescarDashboard
            // 
            this.btnRefrescarDashboard.Location = new System.Drawing.Point(6, 365);
            this.btnRefrescarDashboard.Name = "btnRefrescarDashboard";
            this.btnRefrescarDashboard.Size = new System.Drawing.Size(110, 25);
            this.btnRefrescarDashboard.TabIndex = 1;
            this.btnRefrescarDashboard.Text = "Refrescar";
            this.btnRefrescarDashboard.UseVisualStyleBackColor = true;
            this.btnRefrescarDashboard.Click += new System.EventHandler(this.btnRefrescarDashboard_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 504);
            this.Controls.Add(this.gbxDashboard);
            this.Controls.Add(this.btnBitacora);
            this.Controls.Add(this.btnReportes);
            this.Controls.Add(this.btnReservas);
            this.Controls.Add(this.btnHuespedes);
            this.Controls.Add(this.btnHabitaciones);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.lblBienvenida);
            this.MaximizeBox = false;
            this.Name = "FrmPrincipal";
            this.Text = "Hotel Zormat — Sistema de Gestión";
            this.gbxDashboard.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnHabitaciones;
        private System.Windows.Forms.Button btnHuespedes;
        private System.Windows.Forms.Button btnReservas;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnBitacora;
        private System.Windows.Forms.GroupBox gbxDashboard;
        private System.Windows.Forms.FlowLayoutPanel pnlDashboard;
        private System.Windows.Forms.Button btnRefrescarDashboard;
    }
}
