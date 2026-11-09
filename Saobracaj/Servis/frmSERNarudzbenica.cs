using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Narudžbenica: Nacrt → Čeka odobrenje → Poručeno → (Delimično) isporučeno; odobravanje po nivou iznosa (MD-R07)
    public class frmSERNarudzbenica : Form
    {
        private readonly int id;
        private readonly SERNaslovnaTraka naslov;
        private readonly FlowLayoutPanel bedzevi;
        private readonly SERObrazac zaglavlje;
        private readonly GridGroupingControl grid;
        private readonly StavkeTraka alatiStavke;
        private readonly Label statusna;
        private int status;
        private string broj = "";

        public frmSERNarudzbenica(int id)
        {
            this.id = id;
            SERStil.PripremiFormu(this, "Narudžbenica");

            zaglavlje = new SERObrazac(new List<SERPolje>
            {
                new SERPolje("DobavljacID", "Dobavljač", SERTip.Lista, true) { ListaUpit = SERListe.Partneri, Raspon = 2 },
                SERPolje.Datum("Datum", "Datum", true),
                SERPolje.Datum("OcekivaniDatum", "Očekivani datum isporuke"),
                new SERPolje("Napomena", "Napomena", SERTip.Tekst, false, 500) { Raspon = 4 }
            }, 4);
            var gore = new Panel { Dock = DockStyle.Top, Height = 170, Padding = new Padding(20, 14, 20, 0), BackColor = SavaTheme.Surface };
            gore.Controls.Add(zaglavlje.Panel);

            grid = SERGrid.Novi();
            SERGrid.Kolone(grid,
                new SERKolona("Sifra", "Šifra", 100),
                new SERKolona("Naziv", "Deo", 280),
                new SERKolona("Kolicina", "Količina", 90, "kolicina"),
                new SERKolona("JM", "JM", 50),
                new SERKolona("Cena", "Cena RSD", 110, "iznos"),
                new SERKolona("Iznos", "Iznos RSD", 120, "iznos"),
                new SERKolona("Primljeno", "Primljeno", 90, "kolicina"),
                new SERKolona("Preostalo", "Preostalo", 90, "kolicina"),
                new SERKolona("Napomena", "Napomena", 200));
            SERGrid.DuplikKlik(grid, () => Stavka(SERGrid.IzabraniId(grid)));

            alatiStavke = new StavkeTraka();
            alatiStavke.Dodaj(SERStil.Dugme("Dodaj stavku"), (s, e) => Stavka(0));
            alatiStavke.Dodaj(SERStil.Dugme("Izmeni stavku"), (s, e) => Stavka(SERGrid.IzabraniId(grid)));
            alatiStavke.Dodaj(SERStil.Dugme("Ukloni stavku", "danger"), (s, e) => UkloniStavku());
            alatiStavke.Dodaj(SERStil.Dugme("Sačuvaj zaglavlje"), (s, e) => SacuvajZaglavlje());

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 0, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = SERStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid.Dock = DockStyle.Fill;
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            telo.Controls.Add(alatiStavke);

            bedzevi = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(20, 10, 20, 0), BackColor = SavaTheme.White, WrapContents = false };
            naslov = SERStil.NaslovnaTraka(this, "Narudžbenica");
            statusna = SERStil.StatusnaLinija(this);

            Controls.Add(telo);
            Controls.Add(gore);
            Controls.Add(bedzevi);
            // dock ide od poslednje kontrole ka prvoj: statusna linija, naslovna traka, bedževi, zaglavlje, stavke
            Controls.SetChildIndex(telo, 0);
            Controls.SetChildIndex(gore, 1);
            Controls.SetChildIndex(bedzevi, 2);
            Controls.SetChildIndex(naslov, 3);
            Controls.SetChildIndex(statusna.Parent, 4);
            SERStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        // Traka sa dugmadima iznad tabele stavki
        private class StavkeTraka : FlowLayoutPanel
        {
            public StavkeTraka()
            {
                Dock = DockStyle.Top;
                Height = 46;
                BackColor = SavaTheme.Surface;
                WrapContents = false;
            }

            public void Dodaj(Button b, EventHandler klik)
            {
                b.Click += klik;
                Controls.Add(b);
            }
        }

        private void Ucitaj()
        {
            DataRow r;
            try
            {
                r = SERBaza.Red("SELECT * FROM dbo.vSERNarudzbenica WHERE ID = @ID", SERBaza.P("@ID", id));
                if (r == null)
                    return;
                SERGrid.Napuni(grid, SERBaza.Tabela(@"
                    SELECT ID, DeoID, Sifra, Naziv, Kolicina, JM, Cena, Iznos, Primljeno, Preostalo, ISNULL(Napomena, N'') AS Napomena
                    FROM dbo.vSERNarudzbenicaStavka WHERE NarudzbenicaID = @ID ORDER BY Sifra", SERBaza.P("@ID", id)));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
                return;
            }
            status = SERBaza.CeoBroj(r["Status"]);
            broj = Convert.ToString(r["Broj"]);
            Text = broj;
            naslov.Naslov.Text = "Nabavka › " + broj + " · " + r["Dobavljac"];
            zaglavlje.PostaviSve(new SERVrednosti(SERBaza.RedUMapu(r)));
            bool nacrt = status == 1;
            foreach (SERPolje p in zaglavlje.Polja)
                zaglavlje.Omoguci(p.Kolona, nacrt);
            alatiStavke.Visible = nacrt;

            bedzevi.Controls.Clear();
            Color boja = SERGrid.BojaStatusa("Narudzbenica", status) ?? SavaTheme.Primary;
            bedzevi.Controls.Add(new SERBedz(Convert.ToString(r["StatusTekst"]), SavaTheme.SelectedRow, boja));
            bedzevi.Controls.Add(new SERBedz("Iznos " + SERBaza.Iznos(SERBaza.Decimalni(r["Iznos"])) + " RSD", SavaTheme.White, SavaTheme.Text));
            if (r["PotrebanNivoTekst"] != DBNull.Value)
                bedzevi.Controls.Add(new SERBedz("Odobrava: " + r["PotrebanNivoTekst"], SavaTheme.White, SavaTheme.Text));
            if (r["OdobrioKorisnik"] != DBNull.Value)
                bedzevi.Controls.Add(new SERBedz("Odobrio " + r["OdobrioKorisnik"] + " " + SERBaza.DatumTekst(r["DatumOdobrenja"]), SERStil.PozadinaVazi, SERStil.StatusVazi));

            naslov.ObrisiDugmad();
            if (status >= 1 && status <= 4)
                naslov.DodajDugme("Otkaži", false, (s, e) => Otkazi());
            if (status == 1)
                naslov.DodajDugme("Pošalji na odobrenje", true, (s, e) => Posalji());
            else if (status == 2)
            {
                naslov.DodajDugme("Vrati u nacrt", false, (s, e) => Odobri(false));
                naslov.DodajDugme("Odobri", true, (s, e) => Odobri(true));
            }
            else if (status == 3 || status == 4)
                naslov.DodajDugme("Prijem", true, (s, e) => Prijem());

            statusna.Text = "Kreirao " + r["KreiraoKorisnik"] + " " + SERBaza.DatumTekst(r["DatumKreiranja"])
                + (nacrt ? " · Stavke i zaglavlje se menjaju samo u nacrtu" : "");
        }

        private void Stavka(int stavkaID)
        {
            if (status != 1)
                return;
            if (!SERPrava.Proveri(this, SERPrava.Nabavka))
                return;
            var polja = new List<SERPolje>
            {
                new SERPolje("DeoID", "Deo", SERTip.Lista, true) { ListaUpit = SERListe.AktivniDelovi, Raspon = 2, SamoCitanje = stavkaID > 0 },
                SERPolje.Kolicina("Kolicina", "Količina"),
                SERPolje.Decimalni("Cena", "Cena RSD", true),
                SERPolje.Napomena()
            };
            SERVrednosti pocetne = null;
            if (stavkaID > 0)
                pocetne = new SERVrednosti(SERBaza.RedUMapu(SERBaza.Red(
                    "SELECT DeoID, Kolicina, Cena, Napomena FROM dbo.SERNarudzbenicaStavka WHERE ID = @ID", SERBaza.P("@ID", stavkaID))));
            // za nov deo se predlaže prosečna cena
            Action<frmSERStavka, string> promena = (f, k) =>
            {
                if (stavkaID == 0 && k == "DeoID" && f.Vrednost("DeoID") != null && f.Vrednost("Cena") == null)
                    f.Postavi("Cena", SERBaza.Skalar("SELECT ProsecnaCena FROM dbo.SERDeo WHERE ID = @ID", SERBaza.P("@ID", f.Vrednost("DeoID"))));
            };
            if (SERDijalog.Unos(this, stavkaID > 0 ? "Izmena stavke" : "Nova stavka", polja, pocetne, v =>
                    SERBaza.Procedura("dbo.SER_NarudzbenicaStavkaSnimi",
                        SERBaza.P("@NarudzbenicaID", id), SERBaza.P("@DeoID", v.Uzmi("DeoID")), SERBaza.P("@Kolicina", v.Uzmi("Kolicina")),
                        SERBaza.P("@Cena", v.Uzmi("Cena")), SERBaza.P("@Napomena", SERBaza.Tekst(v.Tekst("Napomena"))),
                        SERBaza.P("@Korisnik", SERBaza.Korisnik), SERBaza.Izlaz("@StavkaID", SqlDbType.Int)), 2, promena))
                Ucitaj();
        }

        private void UkloniStavku()
        {
            int s = SERGrid.IzabraniId(grid);
            if (s <= 0 || !SERPrava.Proveri(this, SERPrava.Nabavka))
                return;
            if (SERDijalog.Potvrdi(this, "Ukloniti izabranu stavku iz nacrta?", Text)
                && SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_NarudzbenicaStavkaObrisi",
                       SERBaza.P("@StavkaID", s), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Ucitaj();
        }

        private void SacuvajZaglavlje()
        {
            if (!SERPrava.Proveri(this, SERPrava.Nabavka))
                return;
            string greska = zaglavlje.Ocitaj(out SERVrednosti v);
            if (greska != null)
            {
                MessageBox.Show(this, greska, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_NarudzbenicaZaglavlje",
                    SERBaza.P("@ID", id), SERBaza.P("@DobavljacID", v.Uzmi("DobavljacID")), SERBaza.P("@Datum", v.Uzmi("Datum")),
                    SERBaza.P("@OcekivaniDatum", v.Uzmi("OcekivaniDatum")), SERBaza.P("@Napomena", SERBaza.Tekst(v.Tekst("Napomena"))),
                    SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Ucitaj();
        }

        private void Posalji()
        {
            if (!SERPrava.Proveri(this, SERPrava.Nabavka))
                return;
            // zbir se računa po svim stavkama, bez obzira na filter u tabeli
            int brojStavki = 0;
            decimal iznos = 0;
            if (grid.Table != null)
                foreach (Syncfusion.Grouping.Record r in grid.Table.Records)
                {
                    brojStavki++;
                    iznos += SERBaza.Decimalni(r.GetValue("Iznos"));
                }
            if (brojStavki == 0)
            {
                MessageBox.Show(this, "Dodajte bar jednu stavku pre slanja na odobrenje.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (iznos <= 0)
            {
                MessageBox.Show(this, "Iznos narudžbenice je 0 RSD. Unesite cene stavki pre slanja na odobrenje.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var nivo = SERBaza.Izlaz("@Nivo", SqlDbType.TinyInt);
            if (SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_NarudzbenicaPosalji",
                    SERBaza.P("@ID", id), SERBaza.P("@Korisnik", SERBaza.Korisnik), nivo)))
                Ucitaj();
        }

        private void Odobri(bool odobri)
        {
            int nivo = SERPrava.NivoOdobravanja();
            if (nivo == 0)
            {
                SERPrava.ProveriOdobravanje(this);
                return;
            }
            string komentar = SERDijalog.Razlog(this, odobri ? "Odobri narudžbenicu" : "Vrati u nacrt", "Komentar", !odobri);
            if (komentar == null)
                return;
            if (SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_NarudzbenicaOdobri",
                    SERBaza.P("@ID", id), SERBaza.P("@Odobri", odobri), SERBaza.P("@NivoKorisnika", nivo),
                    SERBaza.P("@Komentar", SERBaza.Tekst(komentar)), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Ucitaj();
        }

        private void Otkazi()
        {
            if (!SERPrava.Proveri(this, SERPrava.Nabavka))
                return;
            string razlog = SERDijalog.Razlog(this, "Otkaži narudžbenicu", "Razlog otkazivanja", true);
            if (razlog != null && SERDijalog.Pokusaj(this, Text, () => SERBaza.Procedura("dbo.SER_NarudzbenicaOtkazi",
                    SERBaza.P("@ID", id), SERBaza.P("@Razlog", razlog), SERBaza.P("@Korisnik", SERBaza.Korisnik))))
                Ucitaj();
        }

        private void Prijem()
        {
            if (!SERPrava.Proveri(this, SERPrava.Magacioner))
                return;
            using (var f = new frmSERPrijem(id))
                if (f.ShowDialog(this) == DialogResult.OK)
                    Ucitaj();
        }
    }
}
