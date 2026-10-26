using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Lista zahteva za nabavku (izgled formi, strana 8). Istaknuti su redovi koji čekaju odluku prijavljenog korisnika.
    public class frmHRZahteviLista : Form
    {
        private readonly TextBox pretraga;
        private readonly HRChip chipMoji, chipCeka, chipSvi;
        private readonly GridGroupingControl grid;
        private readonly Timer odlozenaPretraga = new Timer { Interval = 300 };
        private readonly HRTekst korisnik;
        private DataTable podaci;
        private string filter;            // "moji", "ceka", "svi"
        private int ja;

        public frmHRZahteviLista()
        {
            HRStil.PripremiFormu(this, "Zahtevi za nabavku");

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = HRStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = HRGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            HRGrid.Kolone(grid,
                new HRKolona("BrojZahteva", "Broj", 130),
                new HRKolona("Datum", "Datum", 100, "datum"),
                new HRKolona("PodnosilacNaziv", "Podnosilac", 200),
                new HRKolona("Vrsta", "Vrsta", 180),
                new HRKolona("Svrha", "Svrha", 260),
                new HRKolona("ProcenjenaVrednost", "Iznos", 120, "iznos"),
                new HRKolona("Valuta", "Valuta", 60),
                new HRKolona("StatusNaziv", "Status", 190));
            HRGrid.BojaTeksta(grid, (r, k) => k == "StatusNaziv" ? HRGrid.BojaStatusaZahteva(HRBaza.CeoBroj(r.GetValue("Status"))) : null);
            HRGrid.BojaReda(grid, r => HRBaza.CeoBroj(r.GetValue("CekaMene")) == 1 ? HRStil.PozadinaIstice : (Color?)null);
            HRGrid.DuplikKlik(grid, () =>
            {
                int z = HRGrid.IzabraniId(grid);
                if (z > 0)
                    HRStil.Otvori(this, new frmHRZahtev(z));
            });

            var alati = HRStil.AlatnaTraka(this);
            pretraga = alati.Pretraga("Pretraga: broj, opis…", 260);
            pretraga.TextChanged += (s, e) => { odlozenaPretraga.Stop(); odlozenaPretraga.Start(); };
            odlozenaPretraga.Tick += (s, e) => { odlozenaPretraga.Stop(); Filtriraj(); };
            chipMoji = alati.Chip("Moji zahtevi", false, (s, e) => { filter = "moji"; Filtriraj(); });
            chipCeka = alati.Chip("Čeka moje odobrenje", false, (s, e) => { filter = "ceka"; Filtriraj(); });
            chipSvi = alati.Chip("Svi", false, (s, e) => { filter = "svi"; Filtriraj(); });
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            alati.DugmeDesno("+ Novi zahtev", (s, e) => HRStil.Otvori(this, new frmHRZahtev(0)));

            var naslov = HRStil.NaslovnaTraka(this, "Nabavka › Zahtevi za nabavku");
            korisnik = naslov.DodajInfo("");
            HRStil.StatusnaLinija(this).Text = "Istaknuti redovi čekaju odluku prijavljenog korisnika · Status pokazuje i nivo odobravanja (npr. 2/3) · Dupli klik otvara zahtev";

            Controls.Add(telo);
            telo.BringToFront();
            HRStil.PrimeniTemu(this);

            // prvo otvaranje i povratak sa forme zahteva
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private void Ucitaj()
        {
            try
            {
                ja = HRPrava.TrenutniZaposleni();
                object ime = ja > 0 ? HRBaza.Skalar("SELECT PrezimeIme FROM dbo.vZaposleniTrenutno WHERE Zaposleni = @Z", HRBaza.P("@Z", ja)) : null;
                korisnik.Text = "korisnik: " + (ime != null ? Convert.ToString(ime) : HRBaza.Korisnik);

                podaci = HRBaza.Tabela(@"
                    SELECT z.Sifra, z.BrojZahteva, z.Datum, z.Podnosilac, z.KreiraoKorisnik,
                           LTRIM(RTRIM(ISNULL(p.Prezime, N'') + N' ' + ISNULL(p.Ime, N''))) AS PodnosilacNaziv,
                           v.Naziv AS Vrsta, z.Svrha, z.ProcenjenaVrednost, z.Valuta, z.Status,
                           s.Naziv + CASE WHEN z.Status = 3 THEN N' (' + CAST(n.Trenutni AS nvarchar(5)) + N'/' + CAST(n.Ukupno AS nvarchar(5)) + N')' ELSE N'' END AS StatusNaziv,
                           CASE WHEN EXISTS (SELECT 1 FROM dbo.ZahtevZaNabavkuOdobrenja o WHERE o.Zahtev = z.Sifra AND o.Odluka IS NULL AND o.Odobravalac = @Ja) THEN 1 ELSE 0 END AS CekaMene,
                           ISNULL(STUFF((SELECT N' ' + st.Opis FROM dbo.ZahtevZaNabavkuStavke st WHERE st.Zahtev = z.Sifra FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 1, N''), N'') AS OpisStavki
                    FROM dbo.ZahteviZaNabavku z
                    JOIN dbo.Zaposleni p ON p.ID = z.Podnosilac
                    JOIN dbo.VrsteNabavke v ON v.Sifra = z.VrstaNabavke
                    JOIN dbo.StatusiZahtevaZaNabavku s ON s.Sifra = z.Status
                    OUTER APPLY (SELECT CASE WHEN z.Status = 3 THEN (SELECT COUNT(*) FROM dbo.fnHR_ZahtevPlanOdobravanja(z.Sifra) f WHERE f.Preskocen = 0) END AS Ukupno,
                                        CASE WHEN z.Status = 3 THEN (SELECT COUNT(*) FROM dbo.fnHR_ZahtevPlanOdobravanja(z.Sifra) f WHERE f.Preskocen = 0 AND f.Nivo <= z.TrenutniNivoOdobravanja) END AS Trenutni) n
                    ORDER BY z.Datum DESC, z.Sifra DESC", HRBaza.P("@Ja", ja));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zahtevi nisu učitani. Proverite da li je na bazi pokrenuta skripta HR_Modul_2026_10.sql.\n\n" + ex.Message,
                    "Zahtevi za nabavku", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                podaci = new DataTable();
                return;
            }
            int ceka = podaci.Select("CekaMene = 1").Length;
            chipCeka.Text = "Čeka moje odobrenje" + (ceka > 0 ? " (" + ceka + ")" : "");
            chipCeka.BojaTeksta = ceka > 0 ? HRStil.StatusIstice : (Color?)null;
            if (filter == null)
                filter = ceka > 0 ? "ceka" : "svi";
            Filtriraj();
        }

        private void Filtriraj()
        {
            if (podaci == null)
                return;
            chipMoji.Aktivan = filter == "moji";
            chipCeka.Aktivan = filter == "ceka";
            chipSvi.Aktivan = filter == "svi";

            var uslovi = new List<string>();
            if (filter == "moji")
                uslovi.Add("(Podnosilac = " + ja + " OR KreiraoKorisnik = '" + HRBaza.Korisnik.Replace("'", "''") + "')");
            else if (filter == "ceka")
                uslovi.Add("CekaMene = 1");
            string t = pretraga.Text.Trim();
            if (t != "")
            {
                string e = t.Replace("'", "''").Replace("[", "[[]").Replace("*", "[*]").Replace("%", "[%]");
                uslovi.Add("(BrojZahteva LIKE '%" + e + "%' OR Svrha LIKE '%" + e + "%' OR OpisStavki LIKE '%" + e + "%' OR PodnosilacNaziv LIKE '%" + e + "%')");
            }
            var dv = new DataView(podaci) { RowFilter = string.Join(" AND ", uslovi) };
            HRGrid.Napuni(grid, dv.ToTable());
        }
    }
}
