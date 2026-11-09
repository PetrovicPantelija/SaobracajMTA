using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Pomocni
{
    public static class CustomOkMessageBox
    {
        public static DialogResult Show(string text, string caption = "")
        {
            using (var frm = new Form())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.MinimizeBox = false;
                frm.MaximizeBox = false;
                frm.ShowIcon = false;
                frm.ShowInTaskbar = false;
                frm.BackColor = Color.Azure;

                int width = 520;
                int height = 180;
                frm.ClientSize = new Size(width, height);
                frm.Text = caption;

                var lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = text;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
                lbl.ForeColor = Color.Black;
                lbl.BackColor = Color.Transparent;
                lbl.SetBounds(10, 10, frm.ClientSize.Width - 20, frm.ClientSize.Height - 80);

                var btnOk = new Button();
                btnOk.Text = "OK";
                btnOk.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
                btnOk.DialogResult = DialogResult.OK;
                btnOk.SetBounds((frm.ClientSize.Width - 100) / 2, frm.ClientSize.Height - 55, 100, 35);

                frm.Controls.Add(lbl);
                frm.Controls.Add(btnOk);
                frm.AcceptButton = btnOk;

                btnOk.Click += (s, e) => { frm.DialogResult = DialogResult.OK; frm.Close(); };

                return frm.ShowDialog();
            }
        }
    }
}
