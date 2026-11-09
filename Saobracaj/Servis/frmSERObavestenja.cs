using Saobracaj.Theme;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Servis
{
    // Obaveštenja tehničkoj službi (MD-R08): kritičan deo ispod minimuma
    public class frmSERObavestenja : Form
    {
        private readonly SERChip chipNeprocitana;
        private readonly GridGroupingControl grid;
        private bool samoNeprocitana = true;

        public frmSERObavestenja()
        {
            SERStil.PripremiFormu(this, "Obaveštenja");

            var telo = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 16, 20, 0), BackColor = SavaTheme.Surface };
            var okvir = SERStil.BeliPanel();
            okvir.Dock = DockStyle.Fill;
            grid = SERGrid.Novi();
            okvir.Controls.Add(grid);
            telo.Controls.Add(okvir);
            SERGrid.Kolone(grid,
                new SERKolona("Vreme", "Vreme", 140, "datumvreme"),
                new SERKolona("Tekst", "Obaveštenje", 760),
                new SERKolona("Raspolozivo", "Raspoloživo sada", 130, "kolicina"),
                new SERKolona("StanjeTekst", "Status", 110),
                new SERKolona("ProcitaoKorisnik", "Pročitao", 100));
            SERGrid.BojaTeksta(grid, (r, k) => k == "StanjeTekst" && Convert.ToString(r.GetValue("StanjeTekst")) == "Nepročitano" ? SavaTheme.Danger : (Color?)null);
            SERGrid.DuplikKlik(grid, OtvoriDeo);
            SERGrid.TekstUTooltipu(grid, "Tekst");

            var alati = SERStil.AlatnaTraka(this);
            chipNeprocitana = alati.Chip("Nepročitana", true, (s, e) => { samoNeprocitana = !samoNeprocitana; Ucitaj(); });
            alati.Dugme("Osveži", (s, e) => Ucitaj());
            alati.Dugme("Otvori deo", (s, e) => OtvoriDeo());
            alati.DugmeDesno("Označi kao pročitano", (s, e) => Procitano());

            SERStil.NaslovnaTraka(this, "Magacin › Obaveštenja");
            SERStil.StatusnaLinija(this).Text = "Obaveštenje nastaje kad kritičan deo padne ispod minimuma · Novo stiže tek kad se prethodno označi kao pročitano";

            Controls.Add(telo);
            telo.BringToFront();
            SERStil.PrimeniTemu(this);
            VisibleChanged += (s, e) => { if (Visible) Ucitaj(); };
        }

        private void Ucitaj()
        {
            chipNeprocitana.Aktivan = samoNeprocitana;
            try
            {
                SERGrid.Napuni(grid, SERBaza.Tabela(@"
                    SELECT o.ID, o.DeoID, o.Vreme, o.Tekst, s.Raspolozivo,
                           CASE WHEN o.Procitano = 1 THEN N'Pročitano' ELSE N'Nepročitano' END AS StanjeTekst,
                           ISNULL(o.ProcitaoKorisnik, N'') AS ProcitaoKorisnik
                    FROM dbo.SERObavestenje o CROSS APPLY dbo.fnSER_Stanje(o.DeoID) s
                    WHERE @Sva = 1 OR o.Procitano = 0
                    ORDER BY o.Vreme DESC", SERBaza.P("@Sva", samoNeprocitana ? 0 : 1)));
            }
            catch (Exception ex)
            {
                SERDijalog.Greska(this, Text, ex);
            }
        }

        private void Procitano()
        {
            int id = SERGrid.IzabraniId(grid);
            if (id > 0 && SERDijalog.Pokusaj(this, Text, () => SERBaza.Izvrsi(@"
                    UPDATE dbo.SERObavestenje SET Procitano = 1, ProcitaoKorisnik = @K, DatumCitanja = sysdatetime()
                    WHERE ID = @ID AND Procitano = 0", SERBaza.P("@ID", id), SERBaza.P("@K", SERBaza.Korisnik))))
                Ucitaj();
        }

        private void OtvoriDeo()
        {
            int deo = SERBaza.CeoBroj(SERGrid.Vrednost(grid, "DeoID"));
            if (deo > 0)
                SERStil.Otvori(this, new frmSERDeo(deo));
        }
    }
}
