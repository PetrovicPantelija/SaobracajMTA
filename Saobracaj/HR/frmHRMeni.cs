using Saobracaj.Theme;
using Syncfusion.WinForms.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Početni ekran HR modula u glavnom prozoru: pločice kao na ekranu Podešavanja sistema.
    // Svaka pločica otvara formu preko kartice (mainNoviKartice) i provere prava.
    public class frmHRMeni : Form
    {
        private SfButton zahteviGO;

        public frmHRMeni()
        {
            Text = "HR modul";
            BackColor = SavaTheme.Surface;
            AutoScaleMode = AutoScaleMode.None;
            Size = new Size(1200, 700);

            var naslov = new HRTekst { Text = "HR modul", Font = new Font("Segoe UI Semibold", 24f), ForeColor = SavaTheme.Text, Location = new Point(47, 24), Height = 48 };
            var ploce = new FlowLayoutPanel
            {
                Location = new Point(40, 90),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Size = new Size(ClientSize.Width - 60, ClientSize.Height - 100),
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            ploce.Controls.Add(Ploca("Zaposleni", "Zaposleni",() => new frmHRZaposleniLista()));
            ploce.Controls.Add(Ploca("Evidencija rada", HRPrava.EvidencijaRada, () => new frmHREvidencijaRada()));
            ploce.Controls.Add(zahteviGO = Ploca("Zahtevi za GO", HRPrava.OdobravanjeZahteva, () => new frmHRGOZahtevi()));
            ploce.Controls.Add(Ploca("Godišnji odmori", HRPrava.GodisnjiOdmori, () => new frmHRGOPravo()));
            ploce.Controls.Add(Ploca("Kontrolna tabla rokova", "Kontrolna tabla rokova", () => new frmHRKontrolnaTabla()));
            ploce.Controls.Add(Ploca("Zahtevi za nabavku", "Zahtevi za nabavku", () => new frmHRZahteviLista()));
            ploce.Controls.Add(Ploca("Pravila odobravanja", "Pravila odobravanja", () => new frmHRSifarnici(true)));
            ploce.Controls.Add(Ploca("Šifarnici HR", "Šifarnici HR", () => new frmHRSifarnici(false)));
            ploce.Controls.Add(Ploca("HR log", "HR log", () => new frmHRLog()));

            Controls.Add(ploce);
            Controls.Add(naslov);
            VisibleChanged += (s, e) => { if (Visible) OsveziBrojZahteva(); };
        }

        // Broj zahteva za GO koji čekaju prijavljenog šefa (dashboard)
        private void OsveziBrojZahteva()
        {
            try
            {
                int ja = HRPrava.TrenutniZaposleni();
                int n = ja <= 0 ? 0 : HRBaza.CeoBroj(HRBaza.Skalar(@"
                    SELECT COUNT(*) FROM dbo.GODopustStavke s JOIN dbo.GODopust d ON d.ID = s.DopustID
                    JOIN dbo.Zaposleni z ON z.ID = d.ZaposleniSifra
                    WHERE s.StatusID = 2 AND z.NadredjeniSifra = @Ja", HRBaza.P("@Ja", ja)));
                zahteviGO.Text = n > 0 ? "Zahtevi za GO (" + n + ")" : "Zahtevi za GO";
            }
            catch
            {
                // tabele faze 2 još ne postoje
            }
        }

        private SfButton Ploca(string tekst, string kartica, Func<Form> forma)
        {
            var b = new SfButton
            {
                Text = tekst,
                AllowWrapText = true,
                Size = new Size(288, 165),
                Margin = new Padding(7, 5, 14, 10),
                BackColor = Color.FromArgb(32, 61, 85),
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 18F),
                ImageSize = new Size(56, 64),
                ImageMargin = new Padding(20, 20, 20, 10),
                TextImageRelation = TextImageRelation.ImageAboveText,
                UseVisualStyleBackColor = false
            };
            b.Style.BackColor = Color.FromArgb(32, 61, 85);
            b.Style.ForeColor = Color.White;
            b.Style.Image = global::Saobracaj.Properties.Resources.PodešavanjeSistema;
            b.Click += (s, e) =>
            {
                var main = TopLevelControl as NewMain;
                if (main != null)
                    main.OtvoriFormuSaPravom(kartica ?? tekst, forma);
                else
                    forma().Show();
            };
            return b;
        }
    }
}
