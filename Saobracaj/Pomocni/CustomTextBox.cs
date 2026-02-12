using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Pomocni
{
    public static class CustomTextBox
    {
        public static DialogResult Show(string text, out decimal input, decimal maxVrednost, string caption = "")
        {
            input = 0; // Podrazumevana vrednost    
            decimal tempOutput = 0;

            using (var frm = new Form())
            {
                // --- PODEŠAVANJE FORME ---
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.BackColor = Color.Azure;

                int width = 520;
                int height = 250;
                frm.ClientSize = new Size(width, height);

                // --- KONTROLE ---

                // Labela 
                var lbl = new Label();
                lbl.Text = text;
                lbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold); 
                lbl.SetBounds(25, 40, 280, 70);
                lbl.AutoSize = false;
                lbl.TextAlign = ContentAlignment.MiddleLeft;

                // TextBox 
                var txtInput = new TextBox();
                txtInput.Font = new Font("Segoe UI", 18F);
                txtInput.SetBounds(320, 50, 150, 45);
                txtInput.TextAlign = HorizontalAlignment.Center; 

                // Dugmići - Dimenzije i Y pozicija
                int btnWidth = 120;
                int btnHeight = 45;
                int btnY = height - btnHeight - 30;

                var btnOk = new Button();
                btnOk.Text = "OK";
                btnOk.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                btnOk.BackColor = Color.FromArgb(32, 61, 85);
                btnOk.ForeColor = Color.White;
                btnOk.FlatStyle = FlatStyle.Flat;
                btnOk.SetBounds((width / 2) - btnWidth - 10, btnY, btnWidth, btnHeight);

                var btnCancel = new Button();
                btnCancel.Text = "Odustani";
                btnCancel.Font = new Font("Segoe UI", 12F);
                btnCancel.SetBounds((width / 2) + 10, btnY, btnWidth, btnHeight);

                // Dozvoli samo brojeve i zarez
                txtInput.KeyPress += (s, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
                    {
                        e.Handled = true;
                    }
                };

                // OK klik - Validacija
                btnOk.Click += (s, e) =>
                {
                    if (decimal.TryParse(txtInput.Text, out decimal unetaVrednost))
                    {
                        if (unetaVrednost <= maxVrednost)
                        {
                            tempOutput = unetaVrednost;
                            frm.DialogResult = DialogResult.OK;
                            frm.Close();
                        }
                        else
                        {
                            MessageBox.Show($"Greška: Maksimalna dozvoljena količina je {maxVrednost}!", "Prekoračenje");
                            txtInput.Text = "";
                            txtInput.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Molimo unesite validan broj (koristite zapetu za decimale).", "Neispravan unos");
                        txtInput.Focus();
                    }
                };

                btnCancel.Click += (s, e) => {
                    frm.DialogResult = DialogResult.Cancel;
                    frm.Close();
                };

                frm.Controls.AddRange(new Control[] { lbl, txtInput, btnOk, btnCancel });

                // Omogućava Enter i Esc tastere
                frm.AcceptButton = btnOk;
                frm.CancelButton = btnCancel;

                frm.ActiveControl = txtInput;


                DialogResult result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    input = tempOutput; // Ovde dodeljujemo vrednost onome što je korisnik uneo
                }
                else
                {
                    input = 0; // Ili ostavite prethodnu vrednost
                }

                return result;
            }
        }
    }
}