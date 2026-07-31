namespace HotelZormat
{
    partial class FrmReservas
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
            this.gbxNueva = new System.Windows.Forms.GroupBox();
            this.lblHabitacion = new System.Windows.Forms.Label();
            this.cboHabitacion = new System.Windows.Forms.ComboBox();
            this.lblHuesped = new System.Windows.Forms.Label();
            this.cboHuesped = new System.Windows.Forms.ComboBox();
            this.lblTemporada = new System.Windows.Forms.Label();
            this.cboTemporada = new System.Windows.Forms.ComboBox();
            this.lblCheckIn = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.btnCrearReserva = new System.Windows.Forms.Button();
            this.btnConfirmarReserva = new System.Windows.Forms.Button();
            this.lblResultado = new System.Windows.Forms.Label();
            this.gbxProximas = new System.Windows.Forms.GroupBox();
            this.dgvProximas = new System.Windows.Forms.DataGridView();
            this.gbxNueva.SuspendLayout();
            this.gbxProximas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximas)).BeginInit();
            this.SuspendLayout();
            //
            // gbxNueva
            //
            this.gbxNueva.Controls.Add(this.lblHabitacion);
            this.gbxNueva.Controls.Add(this.cboHabitacion);
            this.gbxNueva.Controls.Add(this.lblHuesped);
            this.gbxNueva.Controls.Add(this.cboHuesped);
            this.gbxNueva.Controls.Add(this.lblTemporada);
            this.gbxNueva.Controls.Add(this.cboTemporada);
            this.gbxNueva.Controls.Add(this.lblCheckIn);
            this.gbxNueva.Controls.Add(this.dtpCheckIn);
            this.gbxNueva.Controls.Add(this.lblCheckOut);
            this.gbxNueva.Controls.Add(this.dtpCheckOut);
            this.gbxNueva.Controls.Add(this.btnCrearReserva);
            this.gbxNueva.Controls.Add(this.btnConfirmarReserva);
            this.gbxNueva.Controls.Add(this.lblResultado);
            this.gbxNueva.Location = new System.Drawing.Point(12, 12);
            this.gbxNueva.Name = "gbxNueva";
            this.gbxNueva.Size = new System.Drawing.Size(460, 210);
            this.gbxNueva.TabIndex = 0;
            this.gbxNueva.TabStop = false;
            this.gbxNueva.Text = "Nueva reserva";
            //
            // lblHabitacion
            //
            this.lblHabitacion.AutoSize = true;
            this.lblHabitacion.Location = new System.Drawing.Point(10, 28);
            this.lblHabitacion.Name = "lblHabitacion";
            this.lblHabitacion.Size = new System.Drawing.Size(63, 13);
            this.lblHabitacion.TabIndex = 0;
            this.lblHabitacion.Text = "Habitación:";
            //
            // cboHabitacion
            //
            this.cboHabitacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHabitacion.FormattingEnabled = true;
            this.cboHabitacion.Location = new System.Drawing.Point(120, 25);
            this.cboHabitacion.Name = "cboHabitacion";
            this.cboHabitacion.Size = new System.Drawing.Size(180, 21);
            this.cboHabitacion.TabIndex = 1;
            //
            // lblHuesped
            //
            this.lblHuesped.AutoSize = true;
            this.lblHuesped.Location = new System.Drawing.Point(10, 58);
            this.lblHuesped.Name = "lblHuesped";
            this.lblHuesped.Size = new System.Drawing.Size(52, 13);
            this.lblHuesped.TabIndex = 2;
            this.lblHuesped.Text = "Huésped:";
            //
            // cboHuesped
            //
            this.cboHuesped.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHuesped.FormattingEnabled = true;
            this.cboHuesped.Location = new System.Drawing.Point(120, 55);
            this.cboHuesped.Name = "cboHuesped";
            this.cboHuesped.Size = new System.Drawing.Size(300, 21);
            this.cboHuesped.TabIndex = 3;
            //
            // lblTemporada
            //
            this.lblTemporada.AutoSize = true;
            this.lblTemporada.Location = new System.Drawing.Point(10, 88);
            this.lblTemporada.Name = "lblTemporada";
            this.lblTemporada.Size = new System.Drawing.Size(63, 13);
            this.lblTemporada.TabIndex = 4;
            this.lblTemporada.Text = "Temporada:";
            //
            // cboTemporada
            //
            this.cboTemporada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTemporada.FormattingEnabled = true;
            this.cboTemporada.Location = new System.Drawing.Point(120, 85);
            this.cboTemporada.Name = "cboTemporada";
            this.cboTemporada.Size = new System.Drawing.Size(120, 21);
            this.cboTemporada.TabIndex = 5;
            //
            // lblCheckIn
            //
            this.lblCheckIn.AutoSize = true;
            this.lblCheckIn.Location = new System.Drawing.Point(10, 118);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(52, 13);
            this.lblCheckIn.TabIndex = 6;
            this.lblCheckIn.Text = "Check-In:";
            //
            // dtpCheckIn
            //
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckIn.Location = new System.Drawing.Point(120, 114);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(120, 20);
            this.dtpCheckIn.TabIndex = 7;
            //
            // lblCheckOut
            //
            this.lblCheckOut.AutoSize = true;
            this.lblCheckOut.Location = new System.Drawing.Point(260, 118);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(63, 13);
            this.lblCheckOut.TabIndex = 8;
            this.lblCheckOut.Text = "Check-Out:";
            //
            // dtpCheckOut
            //
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckOut.Location = new System.Drawing.Point(330, 114);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(110, 20);
            this.dtpCheckOut.TabIndex = 9;
            //
            // btnCrearReserva
            //
            this.btnCrearReserva.Location = new System.Drawing.Point(120, 150);
            this.btnCrearReserva.Name = "btnCrearReserva";
            this.btnCrearReserva.Size = new System.Drawing.Size(110, 25);
            this.btnCrearReserva.TabIndex = 10;
            this.btnCrearReserva.Text = "Crear reserva";
            this.btnCrearReserva.UseVisualStyleBackColor = true;
            this.btnCrearReserva.Click += new System.EventHandler(this.btnCrearReserva_Click);
            //
            // btnConfirmarReserva
            //
            this.btnConfirmarReserva.Enabled = false;
            this.btnConfirmarReserva.Location = new System.Drawing.Point(240, 150);
            this.btnConfirmarReserva.Name = "btnConfirmarReserva";
            this.btnConfirmarReserva.Size = new System.Drawing.Size(110, 25);
            this.btnConfirmarReserva.TabIndex = 11;
            this.btnConfirmarReserva.Text = "Confirmar reserva";
            this.btnConfirmarReserva.UseVisualStyleBackColor = true;
            this.btnConfirmarReserva.Click += new System.EventHandler(this.btnConfirmarReserva_Click);
            //
            // lblResultado
            //
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(10, 185);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(0, 13);
            this.lblResultado.TabIndex = 12;
            //
            // gbxProximas
            //
            this.gbxProximas.Controls.Add(this.dgvProximas);
            this.gbxProximas.Location = new System.Drawing.Point(12, 230);
            this.gbxProximas.Name = "gbxProximas";
            this.gbxProximas.Size = new System.Drawing.Size(460, 180);
            this.gbxProximas.TabIndex = 1;
            this.gbxProximas.TabStop = false;
            this.gbxProximas.Text = "Reservas próximas (7 días)";
            //
            // dgvProximas
            //
            this.dgvProximas.AllowUserToAddRows = false;
            this.dgvProximas.AllowUserToDeleteRows = false;
            this.dgvProximas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProximas.Location = new System.Drawing.Point(3, 16);
            this.dgvProximas.Name = "dgvProximas";
            this.dgvProximas.ReadOnly = true;
            this.dgvProximas.Size = new System.Drawing.Size(454, 161);
            this.dgvProximas.TabIndex = 0;
            //
            // FrmReservas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 422);
            this.Controls.Add(this.gbxProximas);
            this.Controls.Add(this.gbxNueva);
            this.MaximizeBox = false;
            this.Name = "FrmReservas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HotelZormat - Reservas";
            this.gbxNueva.ResumeLayout(false);
            this.gbxNueva.PerformLayout();
            this.gbxProximas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximas)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox gbxNueva;
        private System.Windows.Forms.Label lblHabitacion;
        private System.Windows.Forms.ComboBox cboHabitacion;
        private System.Windows.Forms.Label lblHuesped;
        private System.Windows.Forms.ComboBox cboHuesped;
        private System.Windows.Forms.Label lblTemporada;
        private System.Windows.Forms.ComboBox cboTemporada;
        private System.Windows.Forms.Label lblCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.Label lblCheckOut;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.Button btnCrearReserva;
        private System.Windows.Forms.Button btnConfirmarReserva;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.GroupBox gbxProximas;
        private System.Windows.Forms.DataGridView dgvProximas;
    }
}
