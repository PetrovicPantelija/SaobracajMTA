using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Grupni unos (kao Grupna promena u Terminalu privremenom): više zaposlenih, period od–do, vrsta, sati, samo radni dani
    public class frmHRGrupniUnos : Form
    {
        private readonly CheckedListBox lista;
        private readonly DateTimePicker dtpOd, dtpDo;
        private readonly ComboBox cboVrsta;
        private readonly TextBox txtSati;
        private readonly CheckBox chkRadni;
        private readonly Dictionary<string, HRVrstaRada> vrste;

        public Func<int, DateTime, decimal> FondDana;
        public Func<DateTime, bool> Praznik;
        public decimal PodrazumevaniFond = 8;

        public frmHRGrupniUnos(List<KeyValuePair<int, string>> zaposleni, List<int> izabrani, DateTime od, DateTime @do, Dictionary<string, HRVrstaRada> vrste)
        {
            this.vrste = vrste;
            Text = "Grupni unos";
            Size = new Size(760, 600);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = MinimizeBox = false;
            ShowInTaskbar = false;
            BackColor = SavaTheme.Surface;
            AutoScaleMode = AutoScaleMode.None;

            lista = new CheckedListBox { Dock = DockStyle.Fill, CheckOnClick = true, IntegralHeight = false, BorderStyle = BorderStyle.FixedSingle };
            foreach (var z in zaposleni)
                lista.Items.Add(new Stavka(z.Key, z.Value), izabrani.Contains(z.Key));
            var sve = new CheckBox { Text = "Označi sve", Dock = DockStyle.Top, Height = 30 };
            sve.CheckedChanged += (s, e) => { for (int i = 0; i < lista.Items.Count; i++) lista.SetItemChecked(i, sve.Checked); };
            var levo = new Panel { Dock = DockStyle.Left, Width = 320, Padding = new Padding(20, 12, 10, 12) };
            levo.Controls.Add(lista);
            levo.Controls.Add(sve);
            levo.Controls.Add(new Label { Text = "Zaposleni", Dock = DockStyle.Top, Height = 22 });

            var polja = new HRPoljaPanel(1) { Dock = DockStyle.Top };
            dtpOd = polja.Dodaj("Od", new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy", Value = od });
            dtpDo = polja.Dodaj("Do", new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd.MM.yyyy", Value = @do });
            cboVrsta = polja.Dodaj("Vrsta rada", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList });
            foreach (HRVrstaRada v in vrste.Values.Where(x => !x.SamoKrozZahtev))
                cboVrsta.Items.Add(new Stavka(v.ID, v.Oznaka + " – " + v.Naziv) { Oznaka = v.Oznaka });
            if (cboVrsta.Items.Count > 0)
                cboVrsta.SelectedIndex = 0;
            txtSati = polja.Dodaj("Sati (prazno = dnevni fond iz rasporeda)", new TextBox { TextAlign = HorizontalAlignment.Right });
            chkRadni = polja.Dodaj("Dani", new CheckBox { Text = "Samo radni dani (bez vikenda i praznika)", Checked = true, Height = 28 });
            var desno = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10, 12, 0, 12), AutoScroll = true };
            desno.Controls.Add(polja);

            var dno = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = SavaTheme.White, Padding = new Padding(20, 14, 20, 14) };
            var upisi = HRStil.Dugme("Upiši", "primary");
            var otkazi = HRStil.Dugme("Otkaži");
            upisi.Dock = otkazi.Dock = DockStyle.Right;
            dno.Controls.Add(otkazi);
            dno.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
            dno.Controls.Add(upisi);
            upisi.Click += (s, e) => Upisi();
            otkazi.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.Add(desno);
            Controls.Add(levo);
            Controls.Add(dno);
            ThemeHelper.Apply(this);
        }

        private class Stavka
        {
            public readonly int ID;
            public readonly string Tekst;
            public string Oznaka;
            public Stavka(int id, string tekst) { ID = id; Tekst = tekst; }
            public override string ToString() => Tekst;
        }

        private void Upisi()
        {
            var zaposleni = lista.CheckedItems.Cast<Stavka>().ToList();
            if (zaposleni.Count == 0 || !(cboVrsta.SelectedItem is Stavka vrsta))
            {
                MessageBox.Show(this, "Označite zaposlene i izaberite vrstu rada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime od = dtpOd.Value.Date, @do = dtpDo.Value.Date;
            if (@do < od || (@do - od).TotalDays > 366)
            {
                MessageBox.Show(this, "Period nije ispravan (najviše godinu dana).", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal? sati = null;
            if (txtSati.Text.Trim() != "")
            {
                if (!HRObrazac.ParsirajDecimalni(txtSati.Text, out decimal s) || s <= 0 || s > 24)
                {
                    MessageBox.Show(this, "Sati moraju biti broj između 0 i 24.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                sati = s;
            }
            HRVrstaRada v = vrste[vrsta.Oznaka];
            int upisano = 0;
            var greske = new List<string>();
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (Stavka z in zaposleni)
                    for (DateTime d = od; d <= @do; d = d.AddDays(1))
                    {
                        decimal fond = FondDana?.Invoke(z.ID, d) ?? 0;
                        bool praznik = Praznik?.Invoke(d) ?? false;
                        if (chkRadni.Checked && (fond <= 0 || praznik))
                            continue;
                        decimal h = sati ?? (fond > 0 ? fond : PodrazumevaniFond);
                        try
                        {
                            frmHREvidencijaRada.UpisiDan(z.ID, d, new HRUnosDana { Oznaka1 = v.Oznaka, Sati1 = h });
                            upisano++;
                        }
                        catch (SqlException ex)
                        {
                            greske.Add(z.Tekst + " " + d.ToString("dd.MM.") + ": " + ex.Message);
                        }
                    }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            MessageBox.Show(this, "Upisano dana: " + upisano + (greske.Count > 0 ? "\n\nNije upisano:\n" + string.Join("\n", greske.Take(10)) : ""),
                Text, MessageBoxButtons.OK, greske.Count > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
