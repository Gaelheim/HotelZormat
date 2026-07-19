namespace HotelZormat
{
    partial class FrmGestionHabitaciones
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
            this.GbxTipoHabitacion = new System.Windows.Forms.GroupBox();
            this.lblIcono = new System.Windows.Forms.Label();
            this.lblTarifa = new System.Windows.Forms.Label();
            this.CbxTipo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GbxBuscarHabitacion = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.tbxNumeroHabitacion = new System.Windows.Forms.TextBox();
            this.lblMensajeBusqueda = new System.Windows.Forms.Label();
            this.LblEstadoHabitacion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GbxPiso3 = new System.Windows.Forms.GroupBox();
            this.lbxHabitacionPiso3 = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.GbxAcciones = new System.Windows.Forms.GroupBox();
            this.BtnLimpieza = new System.Windows.Forms.Button();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.btnReservar = new System.Windows.Forms.Button();
            this.GbxGuardarConfirmar = new System.Windows.Forms.GroupBox();
            this.btnGuardarCambios = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.cbxAccion = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.GbxTipoHabitacion.SuspendLayout();
            this.GbxBuscarHabitacion.SuspendLayout();
            this.GbxPiso3.SuspendLayout();
            this.GbxAcciones.SuspendLayout();
            this.GbxGuardarConfirmar.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbxTipoHabitacion
            // 
            this.GbxTipoHabitacion.Controls.Add(this.lblIcono);
            this.GbxTipoHabitacion.Controls.Add(this.lblTarifa);
            this.GbxTipoHabitacion.Controls.Add(this.CbxTipo);
            this.GbxTipoHabitacion.Controls.Add(this.label3);
            this.GbxTipoHabitacion.Controls.Add(this.label2);
            this.GbxTipoHabitacion.Controls.Add(this.label1);
            this.GbxTipoHabitacion.Location = new System.Drawing.Point(9, 10);
            this.GbxTipoHabitacion.Margin = new System.Windows.Forms.Padding(2);
            this.GbxTipoHabitacion.Name = "GbxTipoHabitacion";
            this.GbxTipoHabitacion.Padding = new System.Windows.Forms.Padding(2);
            this.GbxTipoHabitacion.Size = new System.Drawing.Size(150, 114);
            this.GbxTipoHabitacion.TabIndex = 0;
            this.GbxTipoHabitacion.TabStop = false;
            this.GbxTipoHabitacion.Text = "Tipo de habitación";
            // 
            // lblIcono
            // 
            this.lblIcono.AutoSize = true;
            this.lblIcono.Location = new System.Drawing.Point(45, 54);
            this.lblIcono.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIcono.Name = "lblIcono";
            this.lblIcono.Size = new System.Drawing.Size(0, 13);
            this.lblIcono.TabIndex = 5;
            // 
            // lblTarifa
            // 
            this.lblTarifa.AutoSize = true;
            this.lblTarifa.Location = new System.Drawing.Point(55, 85);
            this.lblTarifa.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTarifa.Name = "lblTarifa";
            this.lblTarifa.Size = new System.Drawing.Size(38, 13);
            this.lblTarifa.TabIndex = 4;
            this.lblTarifa.Text = "RD$ 0";
            // 
            // CbxTipo
            // 
            this.CbxTipo.FormattingEnabled = true;
            this.CbxTipo.Location = new System.Drawing.Point(42, 22);
            this.CbxTipo.Margin = new System.Windows.Forms.Padding(2);
            this.CbxTipo.Name = "CbxTipo";
            this.CbxTipo.Size = new System.Drawing.Size(92, 21);
            this.CbxTipo.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tarifa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Icono:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo:";
            // 
            // GbxBuscarHabitacion
            // 
            this.GbxBuscarHabitacion.Controls.Add(this.btnBuscar);
            this.GbxBuscarHabitacion.Controls.Add(this.tbxNumeroHabitacion);
            this.GbxBuscarHabitacion.Controls.Add(this.lblMensajeBusqueda);
            this.GbxBuscarHabitacion.Controls.Add(this.LblEstadoHabitacion);
            this.GbxBuscarHabitacion.Controls.Add(this.label4);
            this.GbxBuscarHabitacion.Location = new System.Drawing.Point(176, 10);
            this.GbxBuscarHabitacion.Margin = new System.Windows.Forms.Padding(2);
            this.GbxBuscarHabitacion.Name = "GbxBuscarHabitacion";
            this.GbxBuscarHabitacion.Padding = new System.Windows.Forms.Padding(2);
            this.GbxBuscarHabitacion.Size = new System.Drawing.Size(178, 114);
            this.GbxBuscarHabitacion.TabIndex = 1;
            this.GbxBuscarHabitacion.TabStop = false;
            this.GbxBuscarHabitacion.Text = "Buscar Habitación";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnBuscar.Location = new System.Drawing.Point(109, 19);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(65, 19);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // tbxNumeroHabitacion
            // 
            this.tbxNumeroHabitacion.Location = new System.Drawing.Point(47, 20);
            this.tbxNumeroHabitacion.Margin = new System.Windows.Forms.Padding(2);
            this.tbxNumeroHabitacion.Name = "tbxNumeroHabitacion";
            this.tbxNumeroHabitacion.Size = new System.Drawing.Size(57, 20);
            this.tbxNumeroHabitacion.TabIndex = 4;
            // 
            // lblMensajeBusqueda
            // 
            this.lblMensajeBusqueda.AutoSize = true;
            this.lblMensajeBusqueda.Location = new System.Drawing.Point(11, 54);
            this.lblMensajeBusqueda.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMensajeBusqueda.Name = "lblMensajeBusqueda";
            this.lblMensajeBusqueda.Size = new System.Drawing.Size(140, 13);
            this.lblMensajeBusqueda.TabIndex = 3;
            this.lblMensajeBusqueda.Text = "Sin habitación seleccionada";
            // 
            // LblEstadoHabitacion
            // 
            this.LblEstadoHabitacion.AutoSize = true;
            this.LblEstadoHabitacion.Location = new System.Drawing.Point(4, 85);
            this.LblEstadoHabitacion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblEstadoHabitacion.Name = "LblEstadoHabitacion";
            this.LblEstadoHabitacion.Size = new System.Drawing.Size(52, 13);
            this.LblEstadoHabitacion.TabIndex = 2;
            this.LblEstadoHabitacion.Text = "Estado:  -";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Número:";
            // 
            // GbxPiso3
            // 
            this.GbxPiso3.Controls.Add(this.lbxHabitacionPiso3);
            this.GbxPiso3.Location = new System.Drawing.Point(368, 10);
            this.GbxPiso3.Margin = new System.Windows.Forms.Padding(2);
            this.GbxPiso3.Name = "GbxPiso3";
            this.GbxPiso3.Padding = new System.Windows.Forms.Padding(2);
            this.GbxPiso3.Size = new System.Drawing.Size(150, 114);
            this.GbxPiso3.TabIndex = 2;
            this.GbxPiso3.TabStop = false;
            this.GbxPiso3.Text = "Habitaciones piso 3";
            // 
            // lbxHabitacionPiso3
            // 
            this.lbxHabitacionPiso3.FormattingEnabled = true;
            this.lbxHabitacionPiso3.Location = new System.Drawing.Point(4, 17);
            this.lbxHabitacionPiso3.Margin = new System.Windows.Forms.Padding(2);
            this.lbxHabitacionPiso3.Name = "lbxHabitacionPiso3";
            this.lbxHabitacionPiso3.Size = new System.Drawing.Size(142, 82);
            this.lbxHabitacionPiso3.TabIndex = 0;
            // 
            // GbxAcciones
            // 
            this.GbxAcciones.Controls.Add(this.BtnLimpieza);
            this.GbxAcciones.Controls.Add(this.btnCheckOut);
            this.GbxAcciones.Controls.Add(this.btnCheckIn);
            this.GbxAcciones.Controls.Add(this.btnReservar);
            this.GbxAcciones.Location = new System.Drawing.Point(9, 128);
            this.GbxAcciones.Margin = new System.Windows.Forms.Padding(2);
            this.GbxAcciones.Name = "GbxAcciones";
            this.GbxAcciones.Padding = new System.Windows.Forms.Padding(2);
            this.GbxAcciones.Size = new System.Drawing.Size(508, 54);
            this.GbxAcciones.TabIndex = 2;
            this.GbxAcciones.TabStop = false;
            this.GbxAcciones.Text = "Acciones";
            this.GbxAcciones.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // BtnLimpieza
            // 
            this.BtnLimpieza.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.BtnLimpieza.Location = new System.Drawing.Point(402, 17);
            this.BtnLimpieza.Margin = new System.Windows.Forms.Padding(2);
            this.BtnLimpieza.Name = "BtnLimpieza";
            this.BtnLimpieza.Size = new System.Drawing.Size(102, 19);
            this.BtnLimpieza.TabIndex = 3;
            this.BtnLimpieza.Text = "Marcar Limpia";
            this.BtnLimpieza.UseVisualStyleBackColor = false;
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCheckOut.Location = new System.Drawing.Point(275, 17);
            this.btnCheckOut.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(86, 19);
            this.btnCheckOut.TabIndex = 2;
            this.btnCheckOut.Text = "Check Out";
            this.btnCheckOut.UseVisualStyleBackColor = false;
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCheckIn.Location = new System.Drawing.Point(147, 17);
            this.btnCheckIn.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(79, 19);
            this.btnCheckIn.TabIndex = 1;
            this.btnCheckIn.Text = "Check In";
            this.btnCheckIn.UseVisualStyleBackColor = false;
            // 
            // btnReservar
            // 
            this.btnReservar.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnReservar.Location = new System.Drawing.Point(7, 17);
            this.btnReservar.Margin = new System.Windows.Forms.Padding(2);
            this.btnReservar.Name = "btnReservar";
            this.btnReservar.Size = new System.Drawing.Size(95, 19);
            this.btnReservar.TabIndex = 0;
            this.btnReservar.Text = "Reservar";
            this.btnReservar.UseVisualStyleBackColor = false;
            // 
            // GbxGuardarConfirmar
            // 
            this.GbxGuardarConfirmar.Controls.Add(this.btnGuardarCambios);
            this.GbxGuardarConfirmar.Controls.Add(this.btnConfirmar);
            this.GbxGuardarConfirmar.Controls.Add(this.cbxAccion);
            this.GbxGuardarConfirmar.Controls.Add(this.label7);
            this.GbxGuardarConfirmar.Location = new System.Drawing.Point(9, 202);
            this.GbxGuardarConfirmar.Margin = new System.Windows.Forms.Padding(2);
            this.GbxGuardarConfirmar.Name = "GbxGuardarConfirmar";
            this.GbxGuardarConfirmar.Padding = new System.Windows.Forms.Padding(2);
            this.GbxGuardarConfirmar.Size = new System.Drawing.Size(508, 75);
            this.GbxGuardarConfirmar.TabIndex = 3;
            this.GbxGuardarConfirmar.TabStop = false;
            this.GbxGuardarConfirmar.Text = "Confirmación y guardar";
            // 
            // btnGuardarCambios
            // 
            this.btnGuardarCambios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnGuardarCambios.Location = new System.Drawing.Point(351, 31);
            this.btnGuardarCambios.Margin = new System.Windows.Forms.Padding(2);
            this.btnGuardarCambios.Name = "btnGuardarCambios";
            this.btnGuardarCambios.Size = new System.Drawing.Size(146, 19);
            this.btnGuardarCambios.TabIndex = 7;
            this.btnGuardarCambios.Text = "Guardar Cambios";
            this.btnGuardarCambios.UseVisualStyleBackColor = false;
            this.btnGuardarCambios.Click += new System.EventHandler(this.btnGuardarCambios_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnConfirmar.Location = new System.Drawing.Point(211, 31);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(2);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(68, 19);
            this.btnConfirmar.TabIndex = 6;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // cbxAccion
            // 
            this.cbxAccion.FormattingEnabled = true;
            this.cbxAccion.Location = new System.Drawing.Point(42, 28);
            this.cbxAccion.Margin = new System.Windows.Forms.Padding(2);
            this.cbxAccion.Name = "cbxAccion";
            this.cbxAccion.Size = new System.Drawing.Size(150, 21);
            this.cbxAccion.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Acción";
            // 
            // FrmGestionHabitaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 287);
            this.Controls.Add(this.GbxGuardarConfirmar);
            this.Controls.Add(this.GbxAcciones);
            this.Controls.Add(this.GbxPiso3);
            this.Controls.Add(this.GbxBuscarHabitacion);
            this.Controls.Add(this.GbxTipoHabitacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmGestionHabitaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HotelZormat - Gestion de Habitaciones";
            this.GbxTipoHabitacion.ResumeLayout(false);
            this.GbxTipoHabitacion.PerformLayout();
            this.GbxBuscarHabitacion.ResumeLayout(false);
            this.GbxBuscarHabitacion.PerformLayout();
            this.GbxPiso3.ResumeLayout(false);
            this.GbxAcciones.ResumeLayout(false);
            this.GbxGuardarConfirmar.ResumeLayout(false);
            this.GbxGuardarConfirmar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbxTipoHabitacion;
        private System.Windows.Forms.GroupBox GbxBuscarHabitacion;
        private System.Windows.Forms.GroupBox GbxPiso3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox GbxAcciones;
        private System.Windows.Forms.GroupBox GbxGuardarConfirmar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMensajeBusqueda;
        private System.Windows.Forms.Label LblEstadoHabitacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbxHabitacionPiso3;
        private System.Windows.Forms.ComboBox CbxTipo;
        private System.Windows.Forms.Button BtnLimpieza;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.Button btnReservar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.ComboBox cbxAccion;
        private System.Windows.Forms.Label lblTarifa;
        private System.Windows.Forms.Button btnGuardarCambios;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox tbxNumeroHabitacion;
        private System.Windows.Forms.Label lblIcono;
    }
}