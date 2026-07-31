namespace HotelZormat
{
    partial class FrmReportes
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
            this.gbxOcupacion = new System.Windows.Forms.GroupBox();
            this.dgvOcupacion = new System.Windows.Forms.DataGridView();
            this.btnRefrescarOcupacion = new System.Windows.Forms.Button();
            this.gbxIngresos = new System.Windows.Forms.GroupBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnGenerarIngresos = new System.Windows.Forms.Button();
            this.lblResultadoIngresos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOcupacion)).BeginInit();
            this.gbxOcupacion.SuspendLayout();
            this.gbxIngresos.SuspendLayout();
            this.SuspendLayout();
            //
            // gbxOcupacion
            //
            this.gbxOcupacion.Controls.Add(this.dgvOcupacion);
            this.gbxOcupacion.Controls.Add(this.btnRefrescarOcupacion);
            this.gbxOcupacion.Location = new System.Drawing.Point(12, 12);
            this.gbxOcupacion.Name = "gbxOcupacion";
            this.gbxOcupacion.Size = new System.Drawing.Size(500, 230);
            this.gbxOcupacion.TabIndex = 0;
            this.gbxOcupacion.TabStop = false;
            this.gbxOcupacion.Text = "Reporte 1: Ocupación del día";
            //
            // dgvOcupacion
            //
            this.dgvOcupacion.AllowUserToAddRows = false;
            this.dgvOcupacion.AllowUserToDeleteRows = false;
            this.dgvOcupacion.Location = new System.Drawing.Point(6, 20);
            this.dgvOcupacion.Name = "dgvOcupacion";
            this.dgvOcupacion.ReadOnly = true;
            this.dgvOcupacion.Size = new System.Drawing.Size(488, 170);
            this.dgvOcupacion.TabIndex = 0;
            //
            // btnRefrescarOcupacion
            //
            this.btnRefrescarOcupacion.Location = new System.Drawing.Point(6, 196);
            this.btnRefrescarOcupacion.Name = "btnRefrescarOcupacion";
            this.btnRefrescarOcupacion.Size = new System.Drawing.Size(110, 25);
            this.btnRefrescarOcupacion.TabIndex = 1;
            this.btnRefrescarOcupacion.Text = "Refrescar";
            this.btnRefrescarOcupacion.UseVisualStyleBackColor = true;
            this.btnRefrescarOcupacion.Click += new System.EventHandler(this.btnRefrescarOcupacion_Click);
            //
            // gbxIngresos
            //
            this.gbxIngresos.Controls.Add(this.lblDesde);
            this.gbxIngresos.Controls.Add(this.dtpDesde);
            this.gbxIngresos.Controls.Add(this.lblHasta);
            this.gbxIngresos.Controls.Add(this.dtpHasta);
            this.gbxIngresos.Controls.Add(this.btnGenerarIngresos);
            this.gbxIngresos.Controls.Add(this.lblResultadoIngresos);
            this.gbxIngresos.Location = new System.Drawing.Point(12, 250);
            this.gbxIngresos.Name = "gbxIngresos";
            this.gbxIngresos.Size = new System.Drawing.Size(500, 110);
            this.gbxIngresos.TabIndex = 1;
            this.gbxIngresos.TabStop = false;
            this.gbxIngresos.Text = "Reporte 2: Ingresos por rango de fecha";
            //
            // lblDesde
            //
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(10, 28);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(40, 13);
            this.lblDesde.TabIndex = 0;
            this.lblDesde.Text = "Desde:";
            //
            // dtpDesde
            //
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(70, 24);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(120, 20);
            this.dtpDesde.TabIndex = 1;
            //
            // lblHasta
            //
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(210, 28);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(33, 13);
            this.lblHasta.TabIndex = 2;
            this.lblHasta.Text = "Hasta:";
            //
            // dtpHasta
            //
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(260, 24);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(120, 20);
            this.dtpHasta.TabIndex = 3;
            //
            // btnGenerarIngresos
            //
            this.btnGenerarIngresos.Location = new System.Drawing.Point(390, 22);
            this.btnGenerarIngresos.Name = "btnGenerarIngresos";
            this.btnGenerarIngresos.Size = new System.Drawing.Size(100, 24);
            this.btnGenerarIngresos.TabIndex = 4;
            this.btnGenerarIngresos.Text = "Generar";
            this.btnGenerarIngresos.UseVisualStyleBackColor = true;
            this.btnGenerarIngresos.Click += new System.EventHandler(this.btnGenerarIngresos_Click);
            //
            // lblResultadoIngresos
            //
            this.lblResultadoIngresos.AutoSize = true;
            this.lblResultadoIngresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblResultadoIngresos.Location = new System.Drawing.Point(10, 65);
            this.lblResultadoIngresos.Name = "lblResultadoIngresos";
            this.lblResultadoIngresos.Size = new System.Drawing.Size(0, 16);
            this.lblResultadoIngresos.TabIndex = 5;
            //
            // FrmReportes
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 372);
            this.Controls.Add(this.gbxIngresos);
            this.Controls.Add(this.gbxOcupacion);
            this.MaximizeBox = false;
            this.Name = "FrmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HotelZormat - Reportes";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOcupacion)).EndInit();
            this.gbxOcupacion.ResumeLayout(false);
            this.gbxIngresos.ResumeLayout(false);
            this.gbxIngresos.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox gbxOcupacion;
        private System.Windows.Forms.DataGridView dgvOcupacion;
        private System.Windows.Forms.Button btnRefrescarOcupacion;
        private System.Windows.Forms.GroupBox gbxIngresos;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnGenerarIngresos;
        private System.Windows.Forms.Label lblResultadoIngresos;
    }
}
