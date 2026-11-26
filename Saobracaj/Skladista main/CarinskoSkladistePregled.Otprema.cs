using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    // RAZ-11 (test 26.11.2026, izveštaj 4): nalog otpreme nije mogao da se napravi. Lista Otprema robe je prikazivala samo
    // naloge vezane za interni nalog RN20 (manipulacija 74) i već formirane, a nalog otpreme ne nastaje iz komercijalnog
    // naloga (u Otprema.cs je veza sa internim nalogom zakomentarisana). Na listi za Otpremu zato:
    //  - dugme „Novi nalog otpreme“ otvara praznu Otpremu za carinsko skladište (1008);
    //  - „Magacinski predmet“ se zove „Otvori nalog“, jer otvara izabrani nalog otpreme;
    //  - lista prikazuje i naloge bez internog naloga i neformirane (UpitListe, kolona Formiran);
    //  - lista se osvežava kad se korisnik vrati sa naloga (NewMain sakriva listu dok je nalog otvoren).
    public partial class CarinskoSkladistePregled
    {
        string tipListe;
        bool prviPrikaz = true;

        private void InitOtprema()
        {
            tipListe = Tip;
            if (tipListe != "Otprema")
                return;

            button24.Text = "Otvori nalog";

            var btnNovi = new Button
            {
                Text = "Novi nalog otpreme",
                AutoSize = true,
                Dock = DockStyle.Left,
                Font = btnViljuskarista.Font,
                ForeColor = btnViljuskarista.ForeColor,
                FlatStyle = btnViljuskarista.FlatStyle,
                Margin = btnViljuskarista.Margin,
                UseVisualStyleBackColor = true
            };
            btnNovi.FlatAppearance.BorderSize = 0;
            btnNovi.FlatAppearance.MouseDownBackColor = btnViljuskarista.FlatAppearance.MouseDownBackColor;
            btnNovi.FlatAppearance.MouseOverBackColor = btnViljuskarista.FlatAppearance.MouseOverBackColor;
            new ToolTip().SetToolTip(btnNovi, "Nov nalog otpreme za carinsko skladište: magacinski broj, izbor sa lagera i paleta, pa Snimi i Formiraj RN");
            btnNovi.Click += (s, e) => NoviNalogOtpreme();

            panel2.Controls.Add(btnNovi);
            panel2.Controls.SetChildIndex(btnNovi, 0);   // dokuje se poslednje, desno od „Radni nalozi“
            panel2.PerformLayout();
            NapraviMestoZaDugme();

            VisibleChanged += (s, e) =>
            {
                if (!Visible)
                    return;
                if (prviPrikaz) { prviPrikaz = false; return; }   // prvi prikaz: lista je već učitana u konstruktoru
                OsveziListu();
            };
        }

        // Protokol, Štampaj, Skeniraj, Završi proces i Refresh stoje na fiksnim mestima; pomeraju se udesno
        // koliko je potrebno da ne budu ispod dokovanih dugmadi
        private void NapraviMestoZaDugme()
        {
            var kontrole = panel2.Controls.Cast<Control>().ToList();
            var dokovane = kontrole.Where(c => c.Dock == DockStyle.Left).ToList();
            var slobodne = kontrole.Where(c => c.Dock == DockStyle.None).ToList();
            if (dokovane.Count == 0 || slobodne.Count == 0)
                return;

            int pomak = dokovane.Max(c => c.Right) + 12 - slobodne.Min(c => c.Left);
            if (pomak <= 0)
                return;
            foreach (var c in slobodne)
                c.Left += pomak;

            int desno = slobodne.Max(c => c.Right);
            if (desno > panel2.ClientSize.Width)
                panel2.Width += desno - panel2.ClientSize.Width + 4;
        }

        private void NoviNalogOtpreme()
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new Dokumenta.Otprema(0, "Direktni", "Carinsko", Sifarnici.frmLogovanje.user));
        }

        // ponovo puni listu; filteri su već povezani u VratiRN
        private void OsveziListu()
        {
            try
            {
                var ds = new DataSet();
                using (var da = new SqlDataAdapter(UpitListe(), connection))
                    da.Fill(ds);
                gridGroupingControl1.DataSource = ds.Tables[0];
                foreach (GridColumnDescriptor column in gridGroupingControl1.TableDescriptor.Columns)
                    column.AllowFilter = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lista naloga nije osvežena: " + ex.Message);
            }
        }
    }
}
