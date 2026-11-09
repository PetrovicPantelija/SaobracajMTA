using Saobracaj.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Dijalog za unos i izmenu jednog zapisa; polja se prave iz opisa (SERPolje).
    // Snimanje radi pozivalac (Snimanje vraća poruku o grešci ili null), pa dijalog ostaje otvoren kad baza odbije zapis.
    public class frmSERStavka : Form
    {
        public readonly SERObrazac Obrazac;
        private readonly Button sacuvaj;
        public SERVrednosti Vrednosti { get; private set; } = new SERVrednosti();
        public Func<SERVrednosti, string> Validacija;
        public Func<SERVrednosti, string> Snimanje;
        public event Action<frmSERStavka, string> Promena;

        public frmSERStavka(string naslov, IList<SERPolje> polja, SERVrednosti pocetne, int brojKolona = 2)
        {
            Text = naslov;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = SavaTheme.Surface;
            AutoScaleMode = AutoScaleMode.None;

            var naslovTekst = new SERTekst { Text = naslov, Dock = DockStyle.Top, Height = 48, Font = SavaTheme.FormTitleFont, ForeColor = SavaTheme.Text, Padding = new Padding(20, 0, 0, 0) };
            naslovTekst.Poravnanje = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            var dno = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = SavaTheme.White, Padding = new Padding(20, 14, 20, 14) };
            dno.Paint += (s, e) => e.Graphics.DrawLine(new Pen(SavaTheme.Line), 0, 0, dno.Width, 0);
            sacuvaj = SERStil.Dugme("Sačuvaj", "primary");
            var otkazi = SERStil.Dugme("Otkaži");
            sacuvaj.Dock = DockStyle.Right;
            otkazi.Dock = DockStyle.Right;
            dno.Controls.Add(otkazi);
            dno.Controls.Add(new Panel { Dock = DockStyle.Right, Width = 8 });
            dno.Controls.Add(sacuvaj);
            sacuvaj.Click += Sacuvaj_Click;
            otkazi.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            CancelButton = otkazi;

            Obrazac = new SERObrazac(polja, brojKolona);
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
                foreach (SERPolje p in polja)
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
            string greska = Obrazac.Ocitaj(out SERVrednosti v);
            if (greska == null)
            {
                try
                {
                    greska = Validacija?.Invoke(v);
                }
                catch (SERPrekid)
                {
                    return;                      // korisnik je odustao od upozorenja: dijalog ostaje otvoren
                }
            }
            if (string.IsNullOrEmpty(greska) && Snimanje != null)
            {
                // za vreme snimanja: kursor čekanja, dugme „Obrađujem…“, dijalog ne prima klikove (nema dvostrukog snimanja)
                string tekstDugmeta = sacuvaj.Text;
                sacuvaj.Text = "Obrađujem…";
                using (new SERCekanje(this))
                {
                    try
                    {
                        greska = Snimanje(v);
                    }
                    catch (Exception ex)
                    {
                        greska = SERDijalog.Poruka(ex);
                    }
                }
                sacuvaj.Text = tekstDugmeta;
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
