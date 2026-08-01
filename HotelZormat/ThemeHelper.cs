using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelZormat
{
    // Cédula: 40232840757
    public static class ThemeHelper
    {
        public static readonly Color ColorBurgundy = Color.FromArgb(92, 32, 35);    // #5C2023 - Primario
        public static readonly Color ColorTerracota = Color.FromArgb(200, 78, 45);  // #C84E2D - Botones acción
        public static readonly Color ColorTeal = Color.FromArgb(58, 117, 120);      // #3A7578 - Botones limpiar/estados
        public static readonly Color ColorGold = Color.FromArgb(226, 159, 85);      // #E29F55 - Destacados
        public static readonly Color ColorBeige = Color.FromArgb(245, 242, 238);     // #F5F2EE - Fondo
        public static readonly Color ColorSlate = Color.FromArgb(138, 154, 155);    // #8A9A9B - Neutro/Inactivo
        public static readonly Color ColorDark = Color.FromArgb(43, 29, 32);        // #2B1D20 - Texto

        public static void AplicarTema(Form form)
        {
            form.BackColor = ColorBeige;
            form.ForeColor = ColorDark;

            // Recorrer y aplicar estilos a cada control recursivamente
            foreach (Control control in form.Controls)
            {
                StyleControl(control);
            }
        }

        private static void StyleControl(Control control)
        {
            if (control is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
                btn.ForeColor = Color.White;
                btn.Height = Math.Max(btn.Height, 23);

                string textLower = btn.Text.ToLower();
                if (textLower.Contains("guardar") || textLower.Contains("confirmar") || textLower.Contains("ingresar") || textLower.Contains("crear") || textLower.Contains("buscar"))
                {
                    btn.BackColor = ColorTerracota;
                }
                else if (textLower.Contains("limpiar") || textLower.Contains("nuevo"))
                {
                    btn.BackColor = ColorTeal;
                }
                else if (textLower.Contains("eliminar") || textLower.Contains("salir") || textLower.Contains("cancelar"))
                {
                    btn.BackColor = ColorBurgundy;
                }
                else
                {
                    btn.BackColor = ColorSlate;
                }
            }
            else if (control is GroupBox gbx)
            {
                gbx.ForeColor = ColorBurgundy;
                gbx.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
                foreach (Control subControl in gbx.Controls)
                {
                    StyleControl(subControl);
                }
            }
            else if (control is Label lbl)
            {
                if (lbl.Name == "lblPuntosClub")
                {
                    lbl.ForeColor = ColorBurgundy;
                    lbl.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold | FontStyle.Italic);
                }
                else
                {
                    lbl.ForeColor = ColorDark;
                    if (!(lbl.Parent is GroupBox))
                    {
                        lbl.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular);
                    }
                }
            }
            else if (control is Panel pnl)
            {
                // Si el panel es un encabezado superior
                if (pnl.Height < 100 && (pnl.Dock == DockStyle.Top || pnl.Name.ToLower().Contains("header") || pnl.Name.ToLower().Contains("top")))
                {
                    pnl.BackColor = ColorBurgundy;
                    pnl.ForeColor = Color.White;
                }
                else
                {
                    pnl.BackColor = ColorBeige;
                }

                foreach (Control subControl in pnl.Controls)
                {
                    StyleControl(subControl);
                }
            }
            else if (control is DataGridView dgv)
            {
                dgv.BackgroundColor = Color.White;
                dgv.GridColor = ColorBeige;
                dgv.BorderStyle = BorderStyle.None;
                
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorBurgundy;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);

                dgv.DefaultCellStyle.SelectionBackColor = ColorTeal;
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;
                dgv.DefaultCellStyle.BackColor = Color.White;
                dgv.DefaultCellStyle.ForeColor = ColorDark;
            }
            else if (control is MenuStrip ms)
            {
                ms.BackColor = ColorBurgundy;
                ms.ForeColor = Color.White;
                foreach (ToolStripItem item in ms.Items)
                {
                    item.ForeColor = Color.White;
                    if (item is ToolStripMenuItem tsmi)
                    {
                        tsmi.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
                        foreach (ToolStripItem subItem in tsmi.DropDownItems)
                        {
                            subItem.ForeColor = ColorDark;
                        }
                    }
                }
            }
            else
            {
                if (control.HasChildren)
                {
                    foreach (Control subControl in control.Controls)
                    {
                        StyleControl(subControl);
                    }
                }
            }
        }
    }
}
