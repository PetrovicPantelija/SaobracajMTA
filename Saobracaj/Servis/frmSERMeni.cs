using Saobracaj.Theme;
using Syncfusion.WinForms.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Početni ekran modula Održavanje u glavnom prozoru (hub „Magacin rezervnih delova“).
    // Pločice otvaraju forme preko kartice „Delovi“ (pravo pregleda); akcije u formama traže svoja prava.
    public class frmSERMeni : Form
    {
        private readonly SfButton obavestenja, korekcije, nabavka;

        public frmSERMeni()
        {
            Text = "Održavanje";
            BackColor = SavaTheme.Surface;
            AutoScaleMode = AutoScaleMode.None;
            Size = new Size(1200, 700);

            var naslov = new SERTekst { Text = "Magacin rezervnih delova", Font = new Font("Segoe UI Semibold", 24f), ForeColor = SavaTheme.Text, Location = new Point(47, 24), Height = 48 };
            var ploce = new FlowLayoutPanel
            {
                Location = new Point(40, 90),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Size = new Size(ClientSize.Width - 60, ClientSize.Height - 100),
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            ploce.Controls.Add(Ploca("Zalihe i šifarnik", () => new frmSERZalihe()));
            ploce.Controls.Add(Ploca("Izdatnice i prijemi", () => new frmSERPromet()));
            ploce.Controls.Add(nabavka = Ploca("Potrebe za nabavkom", () => new frmSERNabavka()));
            ploce.Controls.Add(Ploca("Popis zaliha", () => new frmSERPopis()));
            ploce.Controls.Add(Ploca("Radni nalozi", () => new frmSERNalozi()));
            ploce.Controls.Add(korekcije = Ploca("Korekcije zaliha", () => new frmSERKorekcije()));
            ploce.Controls.Add(obavestenja = Ploca("Obaveštenja", () => new frmSERObavestenja()));
            ploce.Controls.Add(Ploca("Sredstva i plan održavanja", () => new frmSERSifarnici()));

            Controls.Add(ploce);
            Controls.Add(naslov);
            VisibleChanged += (s, e) => { if (Visible) OsveziBrojeve(); };
        }

        // Brojevi na pločicama: nepročitana obaveštenja, korekcije koje čekaju, delovi u predlogu nabavke
        private void OsveziBrojeve()
        {
            try
            {
                var r = SERBaza.Red(@"
                    SELECT (SELECT COUNT(*) FROM dbo.SERObavestenje WHERE Procitano = 0) AS Obavestenja,
                           (SELECT COUNT(*) FROM dbo.SERKorekcija WHERE Status = 1) AS Korekcije,
                           (SELECT COUNT(*) FROM dbo.vSERPredlogNabavke WHERE PredlozenaKolicina > 0) AS Predlog");
                Broj(obavestenja, "Obaveštenja", SERBaza.CeoBroj(r["Obavestenja"]));
                Broj(korekcije, "Korekcije zaliha", SERBaza.CeoBroj(r["Korekcije"]));
                Broj(nabavka, "Potrebe za nabavkom", SERBaza.CeoBroj(r["Predlog"]));
            }
            catch
            {
                // skripta SER_MagacinDelova_2026_11.sql još nije puštena
            }
        }

        private static void Broj(SfButton b, string tekst, int n)
        {
            b.Text = n > 0 ? tekst + " (" + n + ")" : tekst;
        }

        private SfButton Ploca(string tekst, Func<Form> forma)
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
                    main.OtvoriFormuSaPravom(SERPrava.Pregled, forma);
                else
                    forma().Show();
            };
            return b;
        }
    }
}
