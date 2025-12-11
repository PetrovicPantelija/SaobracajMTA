using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Pomocni
{
    public static class CustomMessageBox
    {
        public static DialogResult Show(string text, string caption = "")
        {
            using (var frm = new Form())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.MinimizeBox = false;
                frm.MaximizeBox = false;
                frm.ShowIcon = false;
                frm.ShowInTaskbar = false;
                frm.BackColor = Color.Azure;

                // Double the original size
                int width = 1040; // 520 * 2
                int height = 360; // 180 * 2
                frm.ClientSize = new Size(width, height);
                frm.Text = caption;

                var lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = text;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font("Segoe UI", 28F, FontStyle.Regular, GraphicsUnit.Point);
                lbl.ForeColor = Color.Black;
                lbl.BackColor = Color.Transparent;

                // Reserve space for larger buttons at bottom
                int btnWidth = 200; // 100 * 2
                int btnHeight = 70; // 35 * 2
                int btnMarginBottom = 25;

                lbl.SetBounds(10, 10, frm.ClientSize.Width - 20, frm.ClientSize.Height - btnHeight - btnMarginBottom - 30);

                var btnYes = new Button();
                btnYes.Text = "Da"; // Yes in local language
                btnYes.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
                btnYes.DialogResult = DialogResult.Yes;
                int yesX = (frm.ClientSize.Width / 2) - btnWidth - 10;
                int btnY = frm.ClientSize.Height - btnHeight - btnMarginBottom;
                btnYes.SetBounds(yesX, btnY, btnWidth, btnHeight);
                // style colors
                btnYes.BackColor = Color.FromArgb(32, 61, 85);
                btnYes.ForeColor = Color.White;
                btnYes.FlatStyle = FlatStyle.Flat;
                btnYes.FlatAppearance.BorderSize = 0;

                var btnNo = new Button();
                btnNo.Text = "Ne"; // No in local language
                btnNo.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
                btnNo.DialogResult = DialogResult.No;
                int noX = (frm.ClientSize.Width / 2) + 10;
                btnNo.SetBounds(noX, btnY, btnWidth, btnHeight);
                // style colors
                btnNo.BackColor = Color.FromArgb(32, 61, 85);
                btnNo.ForeColor = Color.White;
                btnNo.FlatStyle = FlatStyle.Flat;
                btnNo.FlatAppearance.BorderSize = 0;

                frm.Controls.Add(lbl);
                frm.Controls.Add(btnYes);
                frm.Controls.Add(btnNo);

                frm.AcceptButton = btnYes;
                frm.CancelButton = btnNo;

                // Ensure buttons return correct DialogResult
                btnYes.Click += (s, e) => { frm.DialogResult = DialogResult.Yes; frm.Close(); };
                btnNo.Click += (s, e) => { frm.DialogResult = DialogResult.No; frm.Close(); };

                return frm.ShowDialog();
            }
        }
    }
}
