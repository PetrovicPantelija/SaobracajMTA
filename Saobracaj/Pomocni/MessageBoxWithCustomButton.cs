using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Pomocni
{
    class MessageBoxWithCustomButton
    {

        // Dodali smo parametre btnYesText i btnNoText
        public static DialogResult Show(string text, string btnYesText, string btnNoText, string caption = "")
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

                // Povećana visina jer su dugmad jedno ispod drugog
                int width = 1040;
                int height = 500;
                frm.ClientSize = new Size(width, height);

                // --- "✕" DUGME ---
                var btnClose = new Button();
                btnClose.Text = "✕";
                btnClose.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                btnClose.ForeColor = Color.FromArgb(32, 61, 85);
                btnClose.BackColor = Color.Azure;
                btnClose.FlatStyle = FlatStyle.Flat;
                btnClose.FlatAppearance.BorderSize = 0;
                btnClose.FlatAppearance.MouseOverBackColor = Color.Red;
                btnClose.SetBounds(frm.ClientSize.Width - 45, 0, 45, 45);
                btnClose.MouseEnter += (s, e) => { btnClose.ForeColor = Color.White; };
                btnClose.MouseLeave += (s, e) => { btnClose.ForeColor = Color.FromArgb(32, 61, 85); };
                btnClose.Click += (s, e) => { frm.DialogResult = DialogResult.Cancel; frm.Close(); };

                // --- LABELA ---
                var lbl = new Label();
                lbl.AutoSize = false;
                lbl.Text = text;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font("Segoe UI", 24F, FontStyle.Bold); // Malo manji font da stane naslov
                lbl.ForeColor = Color.Black;
                lbl.SetBounds(20, 50, frm.ClientSize.Width - 40, 100);

                // --- DIMENZIJE DUGMADI ---
                int btnWidth = 800; // Šira dugmad
                int btnHeight = 80; // Viša dugmad
                int spacing = 20;   // Razmak između dugmadi
                int startX = (frm.ClientSize.Width - btnWidth) / 2;

                // --- PRVO DUGME (DA) ---
                var btnYes = new Button();
                btnYes.Text = btnYesText;
                btnYes.Font = new Font("Segoe UI", 20F, FontStyle.Regular);
                btnYes.BackColor = Color.FromArgb(32, 61, 85);
                btnYes.ForeColor = Color.White;
                btnYes.FlatStyle = FlatStyle.Flat;
                btnYes.FlatAppearance.BorderSize = 0;
                // Pozicija: Ispod labele
                btnYes.SetBounds(startX, 180, btnWidth, btnHeight);
                btnYes.Click += (s, e) => { frm.DialogResult = DialogResult.Yes; frm.Close(); };

                // --- DRUGO DUGME (NE) ---
                var btnNo = new Button();
                btnNo.Text = btnNoText;
                btnNo.Font = new Font("Segoe UI", 20F, FontStyle.Regular);
                btnNo.BackColor = Color.FromArgb(32, 61, 85);
                btnNo.ForeColor = Color.White;
                btnNo.FlatStyle = FlatStyle.Flat;
                btnNo.FlatAppearance.BorderSize = 0;
                // Pozicija: Ispod prvog dugmeta
                btnNo.SetBounds(startX, 180 + btnHeight + spacing, btnWidth, btnHeight);
                btnNo.Click += (s, e) => { frm.DialogResult = DialogResult.No; frm.Close(); };

                // Dodavanje kontrola
                frm.Controls.Add(btnClose);
                frm.Controls.Add(lbl);
                frm.Controls.Add(btnYes);
                frm.Controls.Add(btnNo);

                frm.AcceptButton = btnYes;
                frm.CancelButton = btnNo;

                return frm.ShowDialog();
            }
        }
    }
}
