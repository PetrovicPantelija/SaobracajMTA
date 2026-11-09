using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.HR
{
    // Zahtevi za godišnji odmor (specifikacija 8.2): šef vidi i odobrava zahteve svog tima, HR vidi sve.
    // Odobrenje u jednoj transakciji skida dane, dodeljuje broj rešenja i upisuje GO u evidenciju; zatim PDF i mail.
    public class frmHRGOZahtevi : Form
    {
        private readonly GridGroupingControl grid;
        private readonly TextBox pretraga;
        private readonly HRChip chipCeka, chipNaCekanju, chipMoji, chipSvi;
        private readonly ComboBox cboGodina;
        private readonly Timer odlozeno = new Timer { Interval = 300 };
        private readonly bool hr, mozeOdobravati;
        private readonly int ja;
        private readonly int samoZaposleni;   // 0 = svi koje korisnik vidi
        private DataTable podaci;
        private string filter;

        public frmHRGOZahtevi(int zaposleni = 0)
        {
            samoZaposleni = zaposleni;
            HRStil.PripremiFormu(this, "Zahtevi za GO");
            ja = HRPrava.TrenutniZaposleni();
            hr = HRPrava.JeHR();
            mozeOdobravati = hr || HRPrava.ImaPravo(HRPrava.OdobravanjeZahteva);

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 12, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = HRStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = HRGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            HRGrid.Kolone(grid,
                new HRKolona("Zaposleni", "Zaposleni", 200),
                new HRKolona("DatumOd", "Od", 95, "datum"),
                new HRKolona("DatumDo", "Do", 95, "datum"),
                new HRKolona("BrojDana", "Dana", 60, "broj"),
                new HRKolona("DatumPovratka", "Povratak", 95, "datum"),
                new HRKolona("Status", "Status", 110),
                new HRKolona("Odobrio", "Odobrio / odbio", 160),
                new HRKolona("BrojResenja", "Rešenje", 120),
                new HRKolona("Mail", "Mail", 60),
                new HRKolona("Stampano", "Štampano", 80),
                new HRKolona("KanalNaziv", "Podneto", 90),
                new HRKolona("DatumZahteva", "Datum zahteva", 120, "datumvreme"),
                new HRKolona("Napomena", "Napomena / razlog odbijanja", 260));
            HRGrid.BojaTeksta(grid, (r, k) =>
            {
                if (k != "Status")
                    return null;
                switch (HRBaza.CeoBroj(r.GetValue("StatusID")))
                {
                    case 2: return HRStil.StatusIstice;
                    case 3: return HRStil.StatusVazi;
                    case 4: return SavaTheme.Danger;
                    case 5: case 6: return SavaTheme.Label;
                    default: return (Color?)null;
                }
            });
            HRGrid.BojaReda(grid, r => HRBaza.CeoBroj(r.GetValue("CekaMene")) == 1 ? HRStil.PozadinaIstice : (Color?)null);

            var alati = HRStil.AlatnaTraka(this);
            pretraga = alati.Pretraga("Pretraga: zaposleni, broj rešenja…", 220);
            pretraga.TextChanged += (s, e) => { odlozeno.Stop(); odlozeno.Start(); };
            odlozeno.Tick += (s, e) => { odlozeno.Stop(); Filtriraj(); };
            cboGodina = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 80, Margin = new Padding(0, 2, 12, 0) };
            cboGodina.Items.Add("Sve");
            for (int g = DateTime.Today.Year + 1; g >= DateTime.Today.Year - 3; g--)
                cboGodina.Items.Add(g);
            cboGodina.SelectedItem = DateTime.Today.Year;
            cboGodina.SelectedIndexChanged += (s, e) => Filtriraj();
            alati.Levo.Controls.Add(cboGodina);
            chipCeka = alati.Chip("Čeka moje odobrenje", false, (s, e) => { filter = "ceka"; Filtriraj(); });
            chipNaCekanju = alati.Chip("Na čekanju", false, (s, e) => { filter = "nacekanju"; Filtriraj(); });
            chipMoji = alati.Chip("Moji", false, (s, e) => { filter = "moji"; Filtriraj(); });
            chipSvi = alati.Chip("Svi", false, (s, e) => { filter = "svi"; Filtriraj(); });
            chipCeka.Visible = mozeOdobravati;

            alati.Dugme("Odbij", (s, e) => { if (Izabran() > 0 && HRGO.Odbij(this, Izabran(), hr, ja)) Ucitaj(); }).Visible = mozeOdobravati;
            alati.Dugme("Izmeni", (s, e) => { if (Izabran() > 0 && HRGO.Izmeni(this, Izabran(), hr, ja)) Ucitaj(); });
            alati.Dugme("Otkaži", (s, e) => { if (Izabran() > 0 && HRGO.Otkazi(this, Izabran())) Ucitaj(); });
            alati.Dugme("Prekid (bolovanje)", (s, e) => { if (Izabran() > 0 && HRGO.Prekid(this, Izabran())) Ucitaj(); });
            alati.Dugme("Rešenje", (s, e) => Akcija(HRResenje.Otvori));
            alati.Dugme("Pošalji ponovo", (s, e) => PosaljiPonovo());
            alati.Dugme("Štampaj", (s, e) => Akcija(HRResenje.Stampaj));
            alati.Dugme("+ Novi zahtev", (s, e) => { if (HRGO.NoviZahtev(this, samoZaposleni, null, null, hr, ja)) Ucitaj(); });
            var odobri = alati.DugmeDesno("Odobri", (s, e) => { if (Izabran() > 0 && HRGO.Odobri(this, Izabran(), hr, ja)) Ucitaj(); });
            odobri.Visible = mozeOdobravati;

            var naslov = HRStil.NaslovnaTraka(this, samoZaposleni > 0 ? "Godišnji odmori › Stavke zaposlenog" : "Godišnji odmori › Zahtevi");
            naslov.DodajDugme("Osveži", false, (s, e) => Ucitaj());
            HRStil.StatusnaLinija(this).Text = "Istaknuti redovi čekaju vašu odluku · Odobravanje skida dane (prvo prošla godina do 30. juna), dodeljuje broj rešenja i upisuje GO u evidenciju rada";

            Controls.Add(telo);
            telo.BringToFront();
            HRStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private int Izabran()
        {
            int id = HRGrid.IzabraniId(grid, "ID");
            if (id <= 0)
                MessageBox.Show("Izaberite zahtev u tabeli.", "Zahtevi za GO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return id;
        }

        private void Akcija(Action<int> a)
        {
            int id = Izabran();
            if (id <= 0)
                return;
            try
            {
                a(id);
                Ucitaj();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Rešenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PosaljiPonovo()
        {
            int id = Izabran();
            if (id <= 0)
                return;
            try
            {
                string razlog = HRResenje.Posalji(id);
                MessageBox.Show(razlog ?? "Rešenje je poslato mailom.", "Rešenje", MessageBoxButtons.OK, razlog == null ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail nije poslat: " + ex.Message, "Rešenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Ucitaj();
        }

        private void Ucitaj()
        {
            try
            {
                HRBaza.Procedura("dbo.sp_GO_OznaciIskoriscene");
                string vidljivo = hr ? "1 = 1" : "(d.ZaposleniSifra = " + ja + " OR " + HRPrava.FilterTima("d.ZaposleniSifra", false, ja) + ")";
                if (samoZaposleni > 0)
                    vidljivo += " AND d.ZaposleniSifra = " + samoZaposleni;
                podaci = HRBaza.Tabela(@"
                    SELECT s.ID, d.ZaposleniSifra, d.Godina, LTRIM(RTRIM(ISNULL(z.Prezime, N'') + N' ' + ISNULL(z.Ime, N''))) AS Zaposleni,
                           s.DatumOd, s.DatumDo, s.BrojDana, s.DatumPovratka, s.StatusID, st.Naziv AS Status,
                           LTRIM(RTRIM(ISNULL(o.Prezime, N'') + N' ' + ISNULL(o.Ime, N''))) AS Odobrio,
                           ISNULL(s.BrojResenja, N'') AS BrojResenja,
                           CASE WHEN s.ResenjePoslatoMail = 1 THEN N'Da' ELSE N'' END AS Mail,
                           CASE WHEN s.ResenjeStampano = 1 THEN N'Da' ELSE N'' END AS Stampano,
                           CASE s.Kanal WHEN 'M' THEN N'Mobilna' ELSE N'Leget+' END AS KanalNaziv, s.DatumZahteva,
                           ISNULL(s.RazlogOdbijanja, s.Napomena) AS Napomena,
                           CASE WHEN s.StatusID = 2 AND z.NadredjeniSifra = @Ja THEN 1 ELSE 0 END AS CekaMene
                    FROM dbo.GODopustStavke s
                    JOIN dbo.GODopust d ON d.ID = s.DopustID
                    JOIN dbo.Zaposleni z ON z.ID = d.ZaposleniSifra
                    JOIN dbo.StatusZahteva st ON st.ID = s.StatusID
                    LEFT JOIN dbo.Zaposleni o ON o.ID = s.OdobrioSifra
                    WHERE " + vidljivo + @"
                    ORDER BY CASE WHEN s.StatusID = 2 THEN 0 ELSE 1 END, s.DatumOd DESC", HRBaza.P("@Ja", ja));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zahtevi nisu učitani. Proverite da li je na bazi pokrenuta skripta HR_Modul_Faza2_2026_10.sql.\n\n" + ex.Message,
                    "Zahtevi za GO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int ceka = podaci.Select("CekaMene = 1").Length;
            chipCeka.Text = "Čeka moje odobrenje" + (ceka > 0 ? " (" + ceka + ")" : "");
            chipCeka.BojaTeksta = ceka > 0 ? HRStil.StatusIstice : (Color?)null;
            if (filter == null)
                filter = samoZaposleni > 0 ? "svi" : ceka > 0 ? "ceka" : hr ? "nacekanju" : "svi";
            Filtriraj();
        }

        private void Filtriraj()
        {
            if (podaci == null)
                return;
            chipCeka.Aktivan = filter == "ceka";
            chipNaCekanju.Aktivan = filter == "nacekanju";
            chipMoji.Aktivan = filter == "moji";
            chipSvi.Aktivan = filter == "svi";
            var uslovi = new List<string>();
            if (filter == "ceka") uslovi.Add("CekaMene = 1");
            else if (filter == "nacekanju") uslovi.Add("StatusID = 2");
            else if (filter == "moji") uslovi.Add("ZaposleniSifra = " + ja);
            if (cboGodina.SelectedItem is int g)
                uslovi.Add("Godina = " + g);
            string t = pretraga.Text.Trim();
            if (t != "")
            {
                string e = t.Replace("'", "''").Replace("[", "[[]").Replace("*", "[*]").Replace("%", "[%]");
                uslovi.Add("(Zaposleni LIKE '%" + e + "%' OR BrojResenja LIKE '%" + e + "%')");
            }
            HRGrid.Napuni(grid, new DataView(podaci) { RowFilter = string.Join(" AND ", uslovi) }.ToTable());
        }
    }
}
