using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Dijalog za unos i izmenu jednog zapisa; polja se prave iz opisa (HRPolje).
    // Snimanje radi pozivalac (Snimanje vraća poruku o grešci ili null), pa dijalog ostaje otvoren kad baza odbije zapis.
    public class frmHRStavka : Form
    {
        public readonly HRObrazac Obrazac;
        public HRVrednosti Vrednosti { get; private set; } = new HRVrednosti();
        public Func<HRVrednosti, string> Validacija;
        public Func<HRVrednosti, string> Snimanje;
        public event Action<frmHRStavka, string> Promena;

        public frmHRStavka(string naslov, IList<HRPolje> polja, HRVrednosti pocetne, int brojKolona = 2)
        {
            Text = naslov;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = SavaTheme.Surface;
            AutoScaleMode = AutoScaleMode.None;

            var naslovTekst = new HRTekst { Text = naslov, Dock = DockStyle.Top, Height = 48, Font = SavaTheme.FormTitleFont, ForeColor = SavaTheme.Text, Padding = new Padding(20, 0, 0, 0) };
            naslovTekst.Poravnanje = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            var dno = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = SavaTheme.White, Padding = new Padding(20, 14, 20, 14) };
            dno.Paint += (s, e) => e.Graphics.DrawLine(new Pen(SavaTheme.Line), 0, 0, dno.Width, 0);
            var sacuvaj = HRStil.Dugme("Sačuvaj", "primary");
            var otkazi = HRStil.Dugme("Otkaži");
            sacuvaj.Dock = DockStyle.Right;
            otkazi.Dock = DockStyle.Right;
            dno.Controls.Add(otkazi);
            dno.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
            dno.Controls.Add(sacuvaj);
            sacuvaj.Click += Sacuvaj_Click;
            otkazi.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            CancelButton = otkazi;

            Obrazac = new HRObrazac(polja, brojKolona);
            Obrazac.Promena += k => Promena?.Invoke(this, k);
            var telo = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(20, 4, 0, 10) };
            telo.Controls.Add(Obrazac.Panel);

            Controls.Add(telo);
            Controls.Add(dno);
            Controls.Add(naslovTekst);

            ThemeHelper.Apply(this);
            Obrazac.PostaviSve(pocetne);

            int sirina = brojKolona == 1 ? 460 : 780;
            Obrazac.Panel.Width = sirina - 40;
            ClientSize = new Size(sirina, Math.Min(Math.Max(Obrazac.Panel.PreferredSize.Height + 130, 240), 760));
            Shown += (s, e) =>
            {
                foreach (HRPolje p in polja)
                    Promena?.Invoke(this, p.Kolona);
            };
        }

        public Control Kontrola(string kolona) => Obrazac.Kontrola(kolona);
        public void Omoguci(string kolona, bool omoguceno, bool isprazni = false) => Obrazac.Omoguci(kolona, omoguceno, isprazni);
        public void PostaviOznaku(string kolona, string tekst) => Obrazac.PostaviOznaku(kolona, tekst);
        public void Postavi(string kolona, object v) => Obrazac.Postavi(kolona, v);
        public object Vrednost(string kolona) => Obrazac.Vrednost(kolona);

        private void Sacuvaj_Click(object sender, EventArgs e)
        {
            string greska = Obrazac.Ocitaj(out HRVrednosti v);
            if (greska == null)
                greska = Validacija?.Invoke(v);
            if (string.IsNullOrEmpty(greska) && Snimanje != null)
            {
                try
                {
                    greska = Snimanje(v);
                }
                catch (Exception ex)
                {
                    greska = ex.Message;
                }
            }
            if (!string.IsNullOrEmpty(greska))
            {
                MessageBox.Show(this, greska, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Vrednosti = v;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
