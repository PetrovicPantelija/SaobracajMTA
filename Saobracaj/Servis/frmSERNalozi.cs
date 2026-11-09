using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Radni nalozi servisa sa troškom delova
    public class frmSERNalozi : Form
    {
        private readonly SERTekst info;
        private readonly TextBox pretraga;
        private readonly SERChip chipOtvoreni, chipCekaju;
        private readonly GridGroupingControl grid;
        private readonly Timer odlozenaPretraga = new Timer { Interval = 300 };
        private DataTable podaci;
        private bool samoOtvoreni = true, samoCekaju;

        public frmSERNalozi()
        {
            SERStil.PripremiFormu(this, "Radni nalozi");

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = SERStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = SERGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            SERGrid.Kolone(grid,
                new SERKolona("Broj", "Broj", 120),
                new SERKolona("Datum", "Datum", 95, "datum"),
                new SERKolona("Sredstvo", "Sredstvo", 220),
                new SERKolona("Servis", "Servis iz plana", 200),
                new SERKolona("Opis", "Opis", 260),
                new SERKolona("StatusTekst", "Status", 120),
                new SERKolona("TrosakDelova", "Trošak delova RSD", 140, "iznos"));
            SERGrid.BojaTeksta(grid, (r, k) => k == "StatusTekst" ? SERGrid.BojaStatusa("RadniNalog", SERBaza.CeoBroj(r.GetValue("Status"))) : null);
            SERGrid.DuplikKlik(grid, () => { int id = SERGrid.IzabraniId(grid); if (id > 0) SERStil.Otvori(this, new frmSERNalog(id)); });

            var alati = SERStil.AlatnaTraka(this);
            pretraga = alati.Pretraga("Pretraga: broj, sredstvo, opis…", 260);
            pretraga.TextChanged += (s, e) => { odlozenaPretraga.Stop(); odlozenaPretraga.Start(); };
            odlozenaPretraga.Tick += (s, e) => { odlozenaPretraga.Stop(); Filtriraj(); };
            chipOtvoreni = alati.Chip("Samo otvoreni", true, (s, e) => { samoOtvoreni = !samoOtvoreni; Filtriraj(); });
            chipCekaju = alati.Chip("Čekaju delove", false, (s, e) => { samoCekaju = !samoCekaju; Filtriraj(); });
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            alati.DugmeDesno("+ Novi nalog", (s, e) => NoviNalog());

            var naslov = SERStil.NaslovnaTraka(this, "Servis › Radni nalozi");
            info = naslov.DodajInfo("");
            SERStil.StatusnaLinija(this).Text = "Dupli klik otvara nalog · Nalog iz plana odmah rezerviše delove iz normativa (MD-R09)";

            Controls.Add(telo);
            telo.BringToFront();
            SERStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private void Ucitaj()
        {
            try
            {
                podaci = SERBaza.Tabela(@"
                    SELECT t.RadniNalogID AS ID, t.Broj, t.Datum, s.Oznaka + N' – ' + s.Naziv AS Sredstvo, ISNULL(p.Naziv, N'') AS Servis,
                           ISNULL(rn.Opis, N'') AS Opis, t.Status, t.StatusTekst, t.TrosakDelova
                    FROM dbo.vSERRadniNalogTrosak t
                    JOIN dbo.SERRadniNalog rn ON rn.ID = t.RadniNalogID
                    JOIN dbo.SERSredstvo s ON s.ID = rn.SredstvoID
                    LEFT JOIN dbo.SERPlanStavka p ON p.ID = rn.PlanStavkaID
                    ORDER BY t.Datum DESC, t.RadniNalogID DESC");
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
                podaci = new DataTable();
                return;
            }
            Filtriraj();
        }

        private void Filtriraj()
        {
            if (podaci == null || podaci.Columns.Count == 0)
                return;
            chipOtvoreni.Aktivan = samoOtvoreni;
            chipCekaju.Aktivan = samoCekaju;
            var uslovi = new List<string>();
            if (samoOtvoreni)
                uslovi.Add("Status IN (1, 2, 3)");
            if (samoCekaju)
                uslovi.Add("Status = 3");
            string t = pretraga.Text.Trim();
            if (t != "")
            {
                string e = t.Replace("'", "''").Replace("[", "[[]").Replace("*", "[*]").Replace("%", "[%]");
                uslovi.Add("(Broj LIKE '%" + e + "%' OR Sredstvo LIKE '%" + e + "%' OR Opis LIKE '%" + e + "%' OR Servis LIKE '%" + e + "%')");
            }
            DataTable prikaz = new DataView(podaci) { RowFilter = string.Join(" AND ", uslovi) }.ToTable();
            info.Text = prikaz.Rows.Count + " naloga";
            SERGrid.Napuni(grid, prikaz);
        }

        private void NoviNalog()
        {
            var polja = new List<SERPolje>
            {
                new SERPolje("SredstvoID", "Sredstvo", SERTip.Lista, true) { ListaUpit = SERListe.Sredstva, Raspon = 2 },
                SERPolje.Datum("Datum", "Datum", true),
                new SERPolje("PlanStavkaID", "Redovni servis iz plana (prazno = vanredni nalog)", SERTip.Lista) { ListaUpit = ServisiZaSredstvo(null) },
                new SERPolje("Opis", "Opis", SERTip.VelikiTekst, false, 500) { Raspon = 2 }
            };
            // servisi iz plana su samo oni koji odgovaraju vrsti izabranog sredstva (server odbija nepodudaranje)
            Action<frmSERStavka, string> promena = (f, kolona) =>
            {
                if (kolona != "SredstvoID")
                    return;
                foreach (SERPolje p in f.Obrazac.Polja)
                    if (p.Kolona == "PlanStavkaID")
                        p.ListaUpit = ServisiZaSredstvo(f.Vrednost("SredstvoID"));
                f.Obrazac.OsveziListu("PlanStavkaID");
            };
            int novi = 0;
            if (!SERDijalog.Unos(this, "Novi radni nalog", polja, new SERVrednosti { ["Datum"] = DateTime.Today }, v =>
                {
                    var id = SERBaza.Izlaz("@ID", SqlDbType.Int);
                    SERBaza.Procedura("dbo.SER_RadniNalogNovi",
                        SERBaza.P("@Datum", v.Uzmi("Datum")), SERBaza.P("@SredstvoID", v.Uzmi("SredstvoID")), SERBaza.P("@PlanStavkaID", v.Uzmi("PlanStavkaID")),
                        SERBaza.P("@Opis", SERBaza.Tekst(v.Tekst("Opis"))), SERBaza.P("@Korisnik", SERBaza.Korisnik),
                        id, SERBaza.Izlaz("@Broj", SqlDbType.NVarChar, 20));
                    novi = SERBaza.CeoBroj(id.Value);
                }, 2, promena))
                return;
            SERStil.Otvori(this, new frmSERNalog(novi));
        }

        // Upit za listu servisa iz plana: samo vrsta izabranog sredstva; bez sredstva lista je prazna
        private static string ServisiZaSredstvo(object sredstvoID)
        {
            int id = SERBaza.CeoBroj(sredstvoID);
            return id <= 0
                ? "SELECT CAST(NULL AS int) AS ID, CAST(NULL AS nvarchar(100)) AS Naziv WHERE 1 = 0"
                : "SELECT p.ID, p.Naziv FROM dbo.SERPlanStavka p WHERE p.Aktivno = 1 AND p.VrstaSredstvaID = "
                  + "(SELECT VrstaSredstvaID FROM dbo.SERSredstvo WHERE ID = " + id + ") ORDER BY p.Naziv";
        }
    }
}
