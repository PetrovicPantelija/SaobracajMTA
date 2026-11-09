using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Potrebe za nabavkom: predlog (MD-R06), narudžbenice, pravila (MD-R07)
    public class frmSERNabavka : Form
    {
        private readonly SERKartice kartice;
        private readonly GridGroupingControl gridPredlog, gridNarudzbenice;
        private readonly SERTekst predlogZbir;
        private readonly SERChip chipOtvorene;
        private readonly Label pravila;
        private bool samoOtvorene = true;
        private int zaNarucivanje;          // delova u predlogu sa količinom većom od 0

        public frmSERNabavka()
        {
            SERStil.PripremiFormu(this, "Potrebe za nabavkom");

            gridPredlog = SERGrid.Novi();
            SERGrid.Kolone(gridPredlog,
                new SERKolona("Sifra", "Šifra", 100),
                new SERKolona("Naziv", "Deo", 240),
                new SERKolona("KriticanTekst", "Kritičan", 75),
                new SERKolona("Raspolozivo", "Raspoloživo", 95, "kolicina"),
                new SERKolona("Nedostaje", "Čeka na nalozima", 120, "kolicina"),
                new SERKolona("Naruceno", "Naručeno", 85, "kolicina"),
                new SERKolona("MinZaliha", "Min", 65, "kolicina"),
                new SERKolona("MaxZaliha", "Max", 65, "kolicina"),
                new SERKolona("PredlozenaKolicina", "Predlog", 85, "kolicina"),
                new SERKolona("JM", "JM", 50),
                new SERKolona("Cena", "Cena RSD", 100, "iznos"),
                new SERKolona("Iznos", "Iznos RSD", 115, "iznos"),
                new SERKolona("Dobavljac", "Dobavljač", 200));
            SERGrid.BojaTeksta(gridPredlog, (r, k) => k == "KriticanTekst" || (k == "Dobavljac" && Convert.ToString(r.GetValue("Dobavljac")) == "bez dobavljača")
                ? SavaTheme.Danger : (Color?)null);

            gridNarudzbenice = SERGrid.Novi();
            SERGrid.Kolone(gridNarudzbenice,
                new SERKolona("Broj", "Broj", 130),
                new SERKolona("Datum", "Datum", 95, "datum"),
                new SERKolona("Dobavljac", "Dobavljač", 240),
                new SERKolona("OcekivaniDatum", "Očekuje se", 100, "datum"),
                new SERKolona("BrojStavki", "Stavki", 65, "broj"),
                new SERKolona("Iznos", "Iznos RSD", 120, "iznos"),
                new SERKolona("StatusTekst", "Status", 160),
                new SERKolona("PotrebanNivoTekst", "Odobrava", 160),
                new SERKolona("OdobrioKorisnik", "Odobrio", 100));
            SERGrid.BojaTeksta(gridNarudzbenice, (r, k) => k == "StatusTekst" ? SERGrid.BojaStatusa("Narudzbenica", SERBaza.CeoBroj(r.GetValue("Status"))) : null);
            SERGrid.DuplikKlik(gridNarudzbenice, () => Otvori(SERGrid.IzabraniId(gridNarudzbenice)));

            // Predlog
            var tPredlog = new Panel { BackColor = SavaTheme.Surface };
            var trakaP = Traka();
            trakaP.Controls.Add(Dugme("Osveži", (s, e) => UcitajPredlog()));
            trakaP.Controls.Add(Dugme("Excel", (s, e) => SERGrid.IzvozUExcel(gridPredlog, "Predlog nabavke")));
            trakaP.Controls.Add(Dugme("Kreiraj narudžbenice", (s, e) => KreirajNarudzbenice(), "primary"));
            predlogZbir = new SERTekst { Margin = new Padding(16, 6, 0, 0), Font = SavaTheme.ButtonFontSemibold };
            trakaP.Controls.Add(predlogZbir);
            tPredlog.Controls.Add(Okvir(gridPredlog));
            tPredlog.Controls.Add(trakaP);

            // Narudžbenice
            var tNar = new Panel { BackColor = SavaTheme.Surface };
            var trakaN = Traka();
            chipOtvorene = new SERChip { Text = "Samo otvorene", Aktivan = true, Margin = new Padding(0, 0, 8, 0) };
            chipOtvorene.Click += (s, e) => { samoOtvorene = !samoOtvorene; UcitajNarudzbenice(); };
            trakaN.Controls.Add(chipOtvorene);
            trakaN.Controls.Add(Dugme("Osveži", (s, e) => UcitajNarudzbenice()));
            trakaN.Controls.Add(Dugme("+ Nova narudžbenica", (s, e) => NovaNarudzbenica(), "primary"));
            tNar.Controls.Add(Okvir(gridNarudzbenice));
            tNar.Controls.Add(trakaN);

            // Pravila
            var tPravila = new Panel { BackColor = SavaTheme.Surface, AutoScroll = true };
            pravila = new Label { Dock = DockStyle.Top, AutoSize = false, Height = 420, Font = SavaTheme.FieldFont, ForeColor = SavaTheme.Text };
            tPravila.Controls.Add(pravila);

            kartice = new SERKartice();
            kartice.Dodaj("Predlog nabavke", tPredlog);
            kartice.Dodaj("Narudžbenice", tNar);
            kartice.Dodaj("Pravila", tPravila);

            var naslov = SERStil.NaslovnaTraka(this, "Magacin › Potrebe za nabavkom");
            SERStil.StatusnaLinija(this).Text = "Deo ulazi u predlog kada je raspoloživo − čeka na nalozima + naručeno < minimum · Dupli klik otvara narudžbenicu";

            Controls.Add(kartice);
            kartice.BringToFront();
            SERStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) { UcitajPredlog(); UcitajNarudzbenice(); UcitajPravila(); } };
        }

        private static FlowLayoutPanel Traka()
        {
            return new FlowLayoutPanel { Dock = DockStyle.Top, Height = 46, BackColor = SavaTheme.Surface, WrapContents = false };
        }

        private static Button Dugme(string tekst, EventHandler klik, string uloga = null)
        {
            var b = SERStil.Dugme(tekst, uloga);
            b.Click += klik;
            return b;
        }

        private static Control Okvir(Control grid)
        {
            var okvir = SERStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid.Dock = DockStyle.Fill;
            okvir.Controls.Add(grid);
            return okvir;
        }

        private void UcitajPredlog()
        {
            DataTable dt;
            try
            {
                dt = SERBaza.Tabela(@"
                    SELECT DeoID AS ID, Sifra, Naziv, CASE WHEN Kritican = 1 THEN N'Da' ELSE N'' END AS KriticanTekst,
                           Raspolozivo, Nedostaje, Naruceno, MinZaliha, MaxZaliha, PredlozenaKolicina, JM, Cena, Iznos,
                           ISNULL(Dobavljac, N'bez dobavljača') AS Dobavljac
                    FROM dbo.vSERPredlogNabavke ORDER BY Kritican DESC, Sifra");
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
                return;
            }
            decimal ukupno = 0;
            zaNarucivanje = 0;
            foreach (DataRow r in dt.Rows)
            {
                ukupno += SERBaza.Decimalni(r["Iznos"]);
                if (SERBaza.Decimalni(r["PredlozenaKolicina"]) > 0)
                    zaNarucivanje++;
            }
            string nivo = "";
            try
            {
                var pNivo = SERBaza.Izlaz("@Nivo", SqlDbType.TinyInt);
                SERBaza.Procedura("dbo.SER_NivoOdobrenja", SERBaza.P("@Iznos", ukupno), pNivo);
                nivo = Convert.ToString(SERBaza.Skalar("SELECT Naziv FROM dbo.SERStatus WHERE Oblast = 'NivoOdobrenja' AND Status = @N", SERBaza.P("@N", pNivo.Value)));
            }
            catch
            {
                // nivo je samo informacija
            }
            predlogZbir.Text = dt.Rows.Count + " delova · ukupno " + SERBaza.Iznos(ukupno) + " RSD" + (nivo == "" ? "" : " · nivo: " + nivo);
            SERGrid.Napuni(gridPredlog, dt);
        }

        private void UcitajNarudzbenice()
        {
            chipOtvorene.Aktivan = samoOtvorene;
            try
            {
                SERGrid.Napuni(gridNarudzbenice, SERBaza.Tabela(@"
                    SELECT ID, Broj, Datum, ISNULL(Dobavljac, N'') AS Dobavljac, OcekivaniDatum, BrojStavki, Iznos, Status, StatusTekst,
                           ISNULL(PotrebanNivoTekst, N'') AS PotrebanNivoTekst, ISNULL(OdobrioKorisnik, N'') AS OdobrioKorisnik
                    FROM dbo.vSERNarudzbenica WHERE @Sve = 1 OR Status IN (1, 2, 3, 4)
                    ORDER BY Datum DESC, ID DESC", SERBaza.P("@Sve", samoOtvorene ? 0 : 1)));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
            }
        }

        private void UcitajPravila()
        {
            string n1 = "100.000", n2 = "500.000";
            try
            {
                foreach (DataRow r in SERBaza.Tabela("SELECT Kljuc, Vrednost FROM dbo.Podesavanja WHERE Kljuc IN (N'Servis.Delovi.NivoOdobrenja1', N'Servis.Delovi.NivoOdobrenja2')").Rows)
                {
                    if (!SERObrazac.ParsirajDecimalni(Convert.ToString(r["Vrednost"]).Replace(".", ""), out decimal d))
                        continue;
                    if (Convert.ToString(r["Kljuc"]).EndsWith("1")) n1 = SERBaza.Iznos(d); else n2 = SERBaza.Iznos(d);
                }
            }
            catch
            {
                // tabela Podesavanja ne postoji: važe podrazumevane granice
            }
            pravila.Text =
                "Predlog nabavke (MD-R06, MD-R09)\n" +
                "• Deo ulazi u predlog kada je raspoloživo − čeka na nalozima + naručeno < minimum.\n" +
                "• Predložena količina = maksimum − (raspoloživo − čeka na nalozima) − naručeno (ne manje od 0). Bez maksimuma se puni do minimuma.\n" +
                "• Naručeno je neisporučen ostatak narudžbenica u statusu Nacrt, Čeka odobrenje, Poručeno i Delimično isporučeno.\n" +
                "• „Kreiraj narudžbenice“ pravi po jedan nacrt za svakog dobavljača; delovi bez dobavljača ostaju u predlogu.\n\n" +
                "Odobravanje narudžbenica (MD-R07)\n" +
                "• do " + n1 + " RSD: šef tehničke službe\n" +
                "• do " + n2 + " RSD: tehnički direktor\n" +
                "• preko " + n2 + " RSD: direktor\n" +
                "Granice se menjaju u Podešavanjima sistema (Servis.Delovi.NivoOdobrenja1 i 2).\n\n" +
                "Prijem (MD-R05)\n" +
                "• Prijem se radi po narudžbenici; razlika u količini ili ceni se unosi na prijemu.\n" +
                "• Prijem po novoj ceni preračunava prosečnu ponderisanu cenu dela.\n" +
                "• Prijem bez narudžbenice je dozvoljen uz razlog.";
        }

        private void KreirajNarudzbenice()
        {
            if (!SERPrava.Proveri(this, SERPrava.Nabavka))
                return;
            if (zaNarucivanje == 0)
            {
                MessageBox.Show(this, "Nema delova za nabavku.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!SERDijalog.Potvrdi(this, "Napraviti nacrte narudžbenica iz predloga, po jednu za svakog dobavljača?", Text))
                return;
            DataSet ds = null;
            if (!SERDijalog.Pokusaj(this, Text, () =>
                {
                    using (var con = new System.Data.SqlClient.SqlConnection(SERBaza.Konekcija))
                    using (var cmd = new System.Data.SqlClient.SqlCommand("dbo.SER_NarudzbeniceIzPredloga", con) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.AddWithValue("@Datum", DateTime.Today);
                        cmd.Parameters.AddWithValue("@Korisnik", SERBaza.Korisnik);
                        ds = new DataSet();
                        SERBaza.Otvori(con);
                        using (var da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            da.Fill(ds);
                    }
                }))
                return;
            int nove = ds.Tables.Count > 0 ? ds.Tables[0].Rows.Count : 0;
            string poruka = nove == 0 ? "Nije napravljena nijedna narudžbenica." : "Napravljeno nacrta narudžbenica: " + nove + ".";
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                poruka += "\n\nDelovi bez dobavljača (ostaju u predlogu):";
                foreach (DataRow r in ds.Tables[1].Rows)
                    poruka += "\n• " + r["Sifra"] + " " + r["Naziv"];
            }
            MessageBox.Show(this, poruka, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            UcitajPredlog();
            UcitajNarudzbenice();
            kartice.Izaberi("Narudžbenice");
        }

        private void NovaNarudzbenica()
        {
            if (!SERPrava.Proveri(this, SERPrava.Nabavka))
                return;
            var polja = new List<SERPolje>
            {
                new SERPolje("DobavljacID", "Dobavljač", SERTip.Lista, true) { ListaUpit = SERListe.Partneri, Raspon = 2 },
                SERPolje.Datum("Datum", "Datum", true),
                SERPolje.Datum("OcekivaniDatum", "Očekivani datum isporuke"),
                SERPolje.Napomena()
            };
            int nova = 0;
            if (SERDijalog.Unos(this, "Nova narudžbenica", polja, new SERVrednosti { ["Datum"] = DateTime.Today }, v =>
                {
                    var id = SERBaza.Izlaz("@ID", SqlDbType.Int);
                    SERBaza.Procedura("dbo.SER_NarudzbenicaNova",
                        SERBaza.P("@DobavljacID", v.Uzmi("DobavljacID")), SERBaza.P("@Datum", v.Uzmi("Datum")),
                        SERBaza.P("@OcekivaniDatum", v.Uzmi("OcekivaniDatum")), SERBaza.P("@Napomena", SERBaza.Tekst(v.Tekst("Napomena"))),
                        SERBaza.P("@Korisnik", SERBaza.Korisnik), id, SERBaza.Izlaz("@Broj", SqlDbType.NVarChar, 20));
                    nova = SERBaza.CeoBroj(id.Value);
                }))
                Otvori(nova);
        }

        private void Otvori(int id)
        {
            if (id > 0)
                SERStil.Otvori(this, new frmSERNarudzbenica(id));
        }
    }
}
