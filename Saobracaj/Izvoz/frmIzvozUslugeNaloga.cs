using Saobracaj.Sifarnici;
using Saobracaj.Theme;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataRow = System.Data.DataRow;

namespace Saobracaj.Izvoz
{
    // Dodaj usluge iz Naloga za otpremu (frmGrupniUnosPoljaIzvoz). Levo su kontejneri naloga i katalog usluga, desno usluge
    // označenih kontejnera: terminalske, dodatne i administrativne. Korisnik označi jedan ili više kontejnera i jednu ili više
    // usluga, a Snimi upisuje svaku uslugu za svaki kontejner (IzvozVrstaManipulacije). Tabele su SfDataGrid sa filterom kao
    // u Excel-u (ikonica u zaglavlju kolone). Za frmIzvoz i frmIzvozKonacna usluge i dalje unosi frmIzvozUnosManipulacije.
    public partial class frmIzvozUslugeNaloga : Form
    {
        private readonly string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private readonly int kontejnerID;            // kontejner izabran u nalogu: označen je pri otvaranju
        private readonly List<int> kontejneriNaloga;
        private readonly int izvoznik;
        private readonly int terminal, scenarioGL, adr, punPrazan, zeleznina, repozicija, scenarioKonacan;

        // usluge označenih kontejnera; tabele se pune iznova pri svakom osvežavanju, pa filteri u gridovima ostaju
        private readonly DataTable terminalske = new DataTable();
        private readonly DataTable dodatne = new DataTable();
        private readonly DataTable administrativne = new DataTable();

        private bool kontejneriUcitani;
        private readonly Timer osvezavanje = new Timer { Interval = 250 };   // usluge se učitavaju kad se završi označavanje

        private GridColumn kolonaPokret, kolonaStatus, kolonaForma;   // katalog: vidljive samo za usluge scenarija

        private enum Katalog { Sve, Administrativne, PoGrupi, PoScenariju }

        public frmIzvozUslugeNaloga(int kontejnerID, IEnumerable<int> kontejneriNaloga, int izvoznik, int terminal, int scenarioGL,
            int adr, int punPrazan, int zeleznina, int repozicija, int scenarioKonacan)
        {
            InitializeComponent();
            this.kontejnerID = kontejnerID;
            this.kontejneriNaloga = (kontejneriNaloga ?? new int[0]).Concat(new[] { kontejnerID }).Where(id => id > 0).Distinct().ToList();
            this.izvoznik = izvoznik;
            this.terminal = terminal;
            this.scenarioGL = scenarioGL;
            this.adr = adr;
            this.punPrazan = punPrazan;
            this.zeleznina = zeleznina;
            this.repozicija = repozicija;
            this.scenarioKonacan = scenarioKonacan;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
                Icon = Saobracaj.Properties.Resources.LegetIconPNG;

            ThemeHelper.Apply(this);
            pnlFilteri.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, pnlFilteri.ClientRectangle, SavaTheme.Line, ButtonBorderStyle.Solid);
            PodesiKolone();

            osvezavanje.Tick += (s, e) =>
            {
                osvezavanje.Stop();
                OsveziUsluge();
            };
            Disposed += (s, e) => osvezavanje.Dispose();
        }

        private void frmIzvozUslugeNaloga_Load(object sender, EventArgs e)
        {
            try
            {
                ProveriZelezninuIRepoziciju();
                PopuniListe();
                UcitajKatalog(Katalog.Sve);
                UcitajKontejnere();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Podaci nisu učitani: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // SfDataGrid ume da pri prvom prikazu označi prvi red umesto kontejnera izabranog u nalogu
        private void frmIzvozUslugeNaloga_Shown(object sender, EventArgs e)
        {
            if (kontejneriUcitani && !OznaceniKontejneri().SequenceEqual(new[] { kontejnerID }))
                OznaciKontejner(kontejnerID);
        }

        // ---------------------------------------------------------------------------------
        // Kolone tabela
        // ---------------------------------------------------------------------------------
        private void PodesiKolone()
        {
            PodesiGrid(sfKontejneri);
            sfKontejneri.Columns.Add(new GridTextColumn { MappingName = "ID", HeaderText = "ID", Width = 60 });
            sfKontejneri.Columns.Add(new GridTextColumn { MappingName = "Kontejner", HeaderText = "Kontejner", AutoSizeColumnsMode = AutoSizeColumnsMode.Fill });

            PodesiGrid(sfUsluge);
            sfUsluge.Columns.Add(new GridTextColumn { MappingName = "ID", HeaderText = "ID", Width = 55 });
            sfUsluge.Columns.Add(new GridTextColumn { MappingName = "Naziv", HeaderText = "Usluga", Width = 230 });
            sfUsluge.Columns.Add(new GridTextColumn { MappingName = "JM", HeaderText = "JM", Width = 60 });
            sfUsluge.Columns.Add(Desno(new GridTextColumn { MappingName = "Cena", HeaderText = "Cena", Width = 80 }));
            sfUsluge.Columns.Add(new GridTextColumn { MappingName = "OJ", HeaderText = "Org. jedinica", Width = 120 });
            kolonaPokret = new GridTextColumn { MappingName = "Pokret", HeaderText = "Pokret", Width = 120 };
            kolonaStatus = new GridTextColumn { MappingName = "Status", HeaderText = "Status", Width = 120 };
            kolonaForma = new GridTextColumn { MappingName = "Forma", HeaderText = "Forma", Width = 100 };
            sfUsluge.Columns.Add(kolonaPokret);
            sfUsluge.Columns.Add(kolonaStatus);
            sfUsluge.Columns.Add(kolonaForma);

            foreach (SfDataGrid grid in new[] { sfTerminalske, sfDodatne, sfAdministrativne })
            {
                PodesiGrid(grid);
                grid.Columns.Add(new GridTextColumn { MappingName = "ID", HeaderText = "ID", Width = 60, AllowEditing = false });
                grid.Columns.Add(new GridTextColumn { MappingName = "Kontejner", HeaderText = "Kontejner", Width = 110, AllowEditing = false });
                grid.Columns.Add(new GridTextColumn { MappingName = "Usluga", HeaderText = "Usluga", Width = 240, AllowEditing = false });
                grid.Columns.Add(Desno(new GridTextColumn { MappingName = "Kolicina", HeaderText = "Količina", Width = 75, Format = "N2", AllowEditing = false }));
                grid.Columns.Add(Desno(new GridNumericColumn { MappingName = "Cena", HeaderText = "Cena", Width = 85, AllowEditing = false }));
                grid.Columns.Add(new GridCheckBoxColumn { MappingName = "SaPDV", HeaderText = "Sa PDV", Width = 70, AllowEditing = false });
                grid.Columns.Add(new GridTextColumn { MappingName = "Platilac", HeaderText = "Platilac", Width = 170, AllowEditing = false });
                grid.Columns.Add(new GridTextColumn { MappingName = "OJ", HeaderText = "Org. jedinica", Width = 120, AllowEditing = false });
                grid.Columns.Add(new GridTextColumn { MappingName = "Pokret", HeaderText = "Pokret", Width = 120, AllowEditing = false });
                grid.Columns.Add(new GridTextColumn { MappingName = "Status", HeaderText = "Status", Width = 120, AllowEditing = false });
                grid.Columns.Add(new GridTextColumn { MappingName = "Forma", HeaderText = "Forma", Width = 100, AllowEditing = false });
            }

            // u terminalskim uslugama se menjaju cena i PDV (kao u formi za jedan kontejner)
            sfTerminalske.AllowEditing = true;
            sfTerminalske.Columns["Cena"].AllowEditing = true;
            sfTerminalske.Columns["SaPDV"].AllowEditing = true;
        }

        private static void PodesiGrid(SfDataGrid grid)
        {
            grid.AutoGenerateColumns = false;
            grid.AllowFiltering = true;   // filter kao u Excel-u u zaglavlju kolone
            grid.AllowSorting = true;
            grid.AllowEditing = false;
            grid.AllowResizingColumns = true;
            grid.SelectionMode = GridSelectionMode.Extended;
            grid.ShowRowHeader = false;
            ThemeHelper.StyleGrid(grid);
        }

        private static GridColumn Desno(GridColumn kolona)
        {
            kolona.CellStyle.HorizontalAlignment = HorizontalAlignment.Right;
            return kolona;
        }

        // ---------------------------------------------------------------------------------
        // Liste: partner, grupa usluga, scenario
        // ---------------------------------------------------------------------------------
        private void PopuniListe()
        {
            cboPartner.DataSource = Ucitaj("SELECT PaSifra, PaNaziv FROM Partnerji ORDER BY PaNaziv");
            cboPartner.DisplayMember = "PaNaziv";
            cboPartner.ValueMember = "PaSifra";
            cboPartner.SelectedValue = izvoznik;

            cboGrupa.DataSource = Ucitaj("SELECT ID, Naziv FROM GrupaVrsteManipulacije ORDER BY ID");
            cboGrupa.DisplayMember = "Naziv";
            cboGrupa.ValueMember = "ID";

            List<int> scenariji = MoguciScenariji();
            if (scenariji.Count > 0)
            {
                cboScenario.DataSource = Ucitaj("SELECT ID, MIN(Naziv) AS Naziv FROM Scenario WHERE ID IN (" + Lista(scenariji) + ") GROUP BY ID ORDER BY ID");
                cboScenario.DisplayMember = "Naziv";
                cboScenario.ValueMember = "ID";
            }
        }

        // Scenariji koji se nude, kao u frmIzvozUnosManipulacije.FillCombo: konačan scenario kontejnera, terminal, ili po relaciji
        // (ScenarioGL), punom / praznom kontejneru i ADR-u
        private List<int> MoguciScenariji()
        {
            if (scenarioKonacan != 0)
                return new List<int> { scenarioKonacan };
            if (terminal == 1)
                return new List<int> { 15 };
            if (scenarioGL == 1 && punPrazan > 0 && adr == 0)
                return new List<int> { 7, 8, 9 };
            if (scenarioGL == 1 && punPrazan > 0 && adr >= 1)
                return new List<int> { 23, 24, 25 };
            if (scenarioGL == 1 && adr == 0 && punPrazan == 0)
                return new List<int> { 12, 29 };
            if (scenarioGL == 2 && adr == 0 && punPrazan > 1)
                return new List<int> { 13 };
            if (scenarioGL == 2 && adr >= 1 && punPrazan > 1)
                return new List<int> { 25, 26 };
            if (scenarioGL == 3 && adr == 0 && punPrazan == 0)
                return new List<int> { 14 };
            return new List<int>();
        }

        // ---------------------------------------------------------------------------------
        // Katalog usluga (levo)
        // ---------------------------------------------------------------------------------
        private void btnPoGrupi_Click(object sender, EventArgs e)
        {
            UcitajKatalogBezgreske(Katalog.PoGrupi);
        }

        private void btnAdministrativne_Click(object sender, EventArgs e)
        {
            UcitajKatalogBezgreske(Katalog.Administrativne);
        }

        // Usluge scenarija se odmah označavaju (Snimi ih upisuje sve)
        private void btnPoScenariju_Click(object sender, EventArgs e)
        {
            if (cboScenario.SelectedValue == null)
            {
                MessageBox.Show("Za ovaj kontejner nema ponuđenog scenarija.", "Po scenariju", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            UcitajKatalogBezgreske(Katalog.PoScenariju);
        }

        private void UcitajKatalogBezgreske(Katalog vrsta)
        {
            try
            {
                UcitajKatalog(vrsta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usluge nisu učitane: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UcitajKatalog(Katalog vrsta)
        {
            const string Kolone = "vm.ID, vm.Naziv, vm.JM, vm.Cena, oj.Naziv AS OJ";
            const string BezScenarija = ", CAST(N'/' AS nvarchar(50)) AS Pokret, 0 AS StatusID, CAST(N'' AS nvarchar(50)) AS Status, CAST(N'/' AS nvarchar(50)) AS Forma " +
                "FROM VrstaManipulacije vm INNER JOIN OrganizacioneJedinice oj ON oj.ID = vm.OrgJed ";

            DataTable tabela;
            switch (vrsta)
            {
                case Katalog.PoScenariju:
                    tabela = Ucitaj("SELECT " + Kolone + ", s.Pokret, s.StatusKontejnera AS StatusID, ks.Naziv AS Status, s.Forma " +
                        "FROM VrstaManipulacije vm " +
                        "INNER JOIN Scenario s ON s.Usluga = vm.ID " +
                        "INNER JOIN KontejnerStatus ks ON ks.ID = s.StatusKontejnera " +
                        "INNER JOIN OrganizacioneJedinice oj ON oj.ID = vm.OrgJed " +
                        "WHERE s.ID = @ID ORDER BY s.RB", new SqlParameter("@ID", Convert.ToInt32(cboScenario.SelectedValue)));
                    break;
                case Katalog.PoGrupi:
                    tabela = Ucitaj("SELECT " + Kolone + BezScenarija + "WHERE vm.GrupaVrsteManipulacijeID = @ID ORDER BY vm.Naziv",
                        new SqlParameter("@ID", Convert.ToInt32(cboGrupa.SelectedValue)));
                    break;
                case Katalog.Administrativne:
                    tabela = Ucitaj("SELECT " + Kolone + BezScenarija + "WHERE vm.Administrativna = 1 ORDER BY vm.Naziv");
                    break;
                default:
                    tabela = Ucitaj("SELECT " + Kolone + BezScenarija + "WHERE vm.Administrativna = 0 ORDER BY vm.Naziv");
                    break;
            }

            sfUsluge.DataSource = tabela;
            bool scenario = vrsta == Katalog.PoScenariju;
            kolonaPokret.Visible = scenario;
            kolonaStatus.Visible = scenario;
            kolonaForma.Visible = scenario;
            lblUsluge.Text = scenario ? "Usluge scenarija " + cboScenario.Text
                : vrsta == Katalog.PoGrupi ? "Usluge grupe " + cboGrupa.Text
                : vrsta == Katalog.Administrativne ? "Administrativne usluge"
                : "Usluge";
            if (scenario)
                sfUsluge.SelectAll();
        }

        // ---------------------------------------------------------------------------------
        // Kontejneri naloga
        // ---------------------------------------------------------------------------------
        private void UcitajKontejnere()
        {
            sfKontejneri.DataSource = Ucitaj("SELECT ID, BrojKontejnera AS Kontejner FROM Izvoz WHERE ID IN (" + Lista(kontejneriNaloga) + ") ORDER BY ID");
            OznaciKontejner(kontejnerID);
            kontejneriUcitani = true;
            OsveziUsluge();
        }

        private void OznaciKontejner(int id)
        {
            sfKontejneri.SelectedItems.Clear();
            if (sfKontejneri.View == null)
                return;
            foreach (var zapis in sfKontejneri.View.Records)
            {
                if (zapis.Data is DataRowView red && Convert.ToInt32(red["ID"]) == id)
                    sfKontejneri.SelectedItems.Add(zapis.Data);
            }
        }

        private List<int> OznaceniKontejneri()
        {
            return sfKontejneri.SelectedItems.OfType<DataRowView>()
                .Select(r => Convert.ToInt32(r["ID"]))
                .OrderBy(id => id)
                .ToList();
        }

        private void sfKontejneri_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!kontejneriUcitani)
                return;
            osvezavanje.Stop();
            osvezavanje.Start();
        }

        // ---------------------------------------------------------------------------------
        // Usluge označenih kontejnera (desno)
        // ---------------------------------------------------------------------------------
        private void OsveziUsluge()
        {
            try
            {
                string idjevi = Lista(OznaceniKontejneri());
                Napuni(sfTerminalske, terminalske, "vm.Dodatna = 0 AND vm.Administrativna = 0", idjevi);
                Napuni(sfDodatne, dodatne, "vm.Dodatna = 1 AND vm.Administrativna = 0", idjevi);
                Napuni(sfAdministrativne, administrativne, "vm.Administrativna = 1", idjevi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usluge kontejnera nisu učitane: " + ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Napuni(SfDataGrid grid, DataTable tabela, string uslov, string idjevi)
        {
            string sql =
                "SELECT ivm.ID, ivm.IDNadredjena AS KontejnerID, i.BrojKontejnera AS Kontejner, ivm.Kolicina, vm.ID AS UslugaID, " +
                "vm.Naziv AS Usluga, ivm.Cena, oj.ID AS OJID, oj.Naziv AS OJ, p.PaSifra AS PlatilacID, p.PaNaziv AS Platilac, SaPDV, " +
                "ivm.Pokret, ks.Naziv AS Status, ivm.Forma " +
                "FROM IzvozVrstaManipulacije ivm " +
                "INNER JOIN VrstaManipulacije vm ON vm.ID = ivm.IDVrstaManipulacije " +
                "INNER JOIN Partnerji p ON p.PaSifra = ivm.Platilac " +
                "INNER JOIN OrganizacioneJedinice oj ON oj.ID = ivm.OrgJed " +
                "INNER JOIN Izvoz i ON i.ID = ivm.IDNadredjena " +
                "LEFT JOIN KontejnerStatus ks ON ks.ID = ivm.StatusKontejnera " +
                "WHERE " + uslov + " AND ivm.IDNadredjena IN (" + idjevi + ") " +
                "ORDER BY i.BrojKontejnera, ivm.ID";

            using (var da = new SqlDataAdapter(sql, connection))
            {
                tabela.BeginLoadData();
                try
                {
                    tabela.Clear();
                    da.Fill(tabela);
                }
                finally
                {
                    tabela.EndLoadData();
                }
            }

            if (grid.DataSource == null)
                grid.DataSource = tabela;
        }

        private static List<int> OznaceneUsluge(SfDataGrid grid)
        {
            return grid.SelectedItems.OfType<DataRowView>().Select(r => Convert.ToInt32(r["ID"])).ToList();
        }

        // ---------------------------------------------------------------------------------
        // Snimi: svaka označena usluga za svaki označeni kontejner
        // ---------------------------------------------------------------------------------
        private void btnSnimi_Click(object sender, EventArgs e)
        {
            const string Naslov = "Snimi";
            List<int> kontejneri = OznaceniKontejneri();
            List<DataRowView> usluge = sfUsluge.SelectedItems.OfType<DataRowView>().ToList();
            if (kontejneri.Count == 0 || usluge.Count == 0)
            {
                MessageBox.Show("Označite jedan ili više kontejnera naloga i jednu ili više usluga.", Naslov,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // kao Snimi u formi za jedan kontejner: cena 0, količina 1, bez platioca; terminalska usluga nosi pokret,
            // status i formu iz scenarija, a dodatna i administrativna "/"
            var ins = new InsertIzvoz();
            var saTerminalskim = new HashSet<int>();
            int upisano = 0;
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (DataRowView usluga in usluge)
                {
                    int manipulacija = Convert.ToInt32(usluga["ID"]);
                    bool administrativna, dodatna;
                    int orgJed;
                    ProcitajUslugu(manipulacija, out administrativna, out dodatna, out orgJed);
                    bool terminalska = !administrativna && !dodatna;

                    string pokret = "/";
                    int status = 0;
                    string forma = "/";
                    if (terminalska)
                    {
                        pokret = Convert.ToString(usluga["Pokret"]);
                        int.TryParse(Convert.ToString(usluga["StatusID"]), out status);
                        forma = Convert.ToString(usluga["Forma"]);
                    }

                    foreach (int kontejner in kontejneri)
                    {
                        ins.InsUbaciUslugu(kontejner, manipulacija, 0, 1, orgJed, 0, 0, pokret, status, forma);
                        upisano++;
                        if (terminalska)
                            saTerminalskim.Add(kontejner);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usluge nisu upisane do kraja (upisano: " + upisano + "): " + ex.Message, Naslov,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            OsveziUsluge();
            ProveriScenario(saTerminalskim);
        }

        private void ProcitajUslugu(int id, out bool administrativna, out bool dodatna, out int orgJed)
        {
            administrativna = false;
            dodatna = false;
            orgJed = 0;
            DataTable t = Ucitaj("SELECT Administrativna, Dodatna, OrgJed FROM VrstaManipulacije WHERE ID = @ID", new SqlParameter("@ID", id));
            if (t.Rows.Count == 0)
                return;
            DataRow r = t.Rows[0];
            administrativna = r["Administrativna"] != DBNull.Value && Convert.ToInt32(r["Administrativna"]) == 1;
            dodatna = r["Dodatna"] != DBNull.Value && Convert.ToInt32(r["Dodatna"]) == 1;
            orgJed = r["OrgJed"] == DBNull.Value ? 0 : Convert.ToInt32(r["OrgJed"]);
        }

        // Kao provera scenarija u formi za jedan kontejner, ali za svaki kontejner kome su dodate terminalske usluge:
        // terminalske usluge kontejnera porede se sa uslugama scenarija; ako se poklapaju sa izabranim scenarijom, scenario
        // se upisuje kontejneru. Jedna poruka za sve kontejnere.
        private void ProveriScenario(ICollection<int> kontejneri)
        {
            if (kontejneri.Count == 0)
                return;

            int izabrani = Convert.ToInt32(cboScenario.SelectedValue);
            var upisan = new List<string>();
            var bezScenarija = new List<string>();
            try
            {
                var scenariji = new Dictionary<int, List<int>>();
                foreach (DataRow r in Ucitaj("SELECT ID, Usluga FROM Scenario").Rows)
                {
                    int id = Convert.ToInt32(r["ID"]);
                    if (!scenariji.ContainsKey(id))
                        scenariji[id] = new List<int>();
                    scenariji[id].Add(Convert.ToInt32(r["Usluga"]));
                }

                foreach (int kontejner in kontejneri)
                {
                    List<int> usluge = Ucitaj(
                        "SELECT ivm.IDVrstaManipulacije FROM IzvozVrstaManipulacije ivm " +
                        "INNER JOIN VrstaManipulacije vm ON vm.ID = ivm.IDVrstaManipulacije " +
                        "WHERE ivm.IDNadredjena = @ID AND vm.Dodatna = 0 AND vm.Administrativna = 0",
                        new SqlParameter("@ID", kontejner))
                        .Rows.Cast<DataRow>().Select(r => Convert.ToInt32(r[0])).ToList();

                    bool nasao = false;
                    bool upisano = false;
                    foreach (KeyValuePair<int, List<int>> scenario in scenariji.OrderBy(s => s.Key))
                    {
                        if (!IsteUsluge(usluge, scenario.Value))
                            continue;
                        nasao = true;
                        if (!upisano && scenario.Key == izabrani)
                        {
                            new InsertScenario().UpdScenarioKontejnera(scenario.Key, kontejner, 2, 0);
                            upisano = true;
                        }
                    }

                    if (upisano)
                        upisan.Add(NazivKontejnera(kontejner));
                    if (!nasao)
                        bezScenarija.Add(NazivKontejnera(kontejner));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Scenario nije proveren: " + ex.Message, "Scenario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var poruka = new List<string>();
            if (upisan.Count > 0)
                poruka.Add("Izabrali ste scenario " + izabrani + (kontejneri.Count > 1 ? " za kontejnere: " + string.Join(", ", upisan) : "") + ".");
            if (bezScenarija.Count > 0)
                poruka.Add("Ne postoji takav scenario" + (kontejneri.Count > 1 ? " za kontejnere: " + string.Join(", ", bezScenarija) : "") + "!");
            if (poruka.Count > 0)
                MessageBox.Show(string.Join(Environment.NewLine, poruka), "Scenario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Iste usluge, uključujući i broj ponavljanja iste usluge
        private static bool IsteUsluge(List<int> prve, List<int> druge)
        {
            if (prve.Count != druge.Count)
                return false;
            var broj = prve.GroupBy(u => u).ToDictionary(g => g.Key, g => g.Count());
            return druge.GroupBy(u => u).All(g => broj.TryGetValue(g.Key, out int n) && n == g.Count());
        }

        private string NazivKontejnera(int id)
        {
            DataRowView red = sfKontejneri.View == null ? null
                : sfKontejneri.View.Records.Select(z => z.Data).OfType<DataRowView>().FirstOrDefault(r => Convert.ToInt32(r["ID"]) == id);
            string broj = red == null ? "" : Convert.ToString(red["Kontejner"]).Trim();
            return broj.Length > 0 ? broj : "ID " + id;
        }

        // ---------------------------------------------------------------------------------
        // Cena i PDV terminalske usluge, platilac, brisanje
        // ---------------------------------------------------------------------------------
        private void sfTerminalske_CurrentCellEndEdit(object sender, CurrentCellEndEditEventArgs e)
        {
            if (e.DataColumn.GridColumn.MappingName != "Cena")
                return;
            var red = e.DataRow.RowData as DataRowView;
            if (red == null)
                return;

            try
            {
                double cena = red["Cena"] == DBNull.Value ? 0 : Convert.ToDouble(red["Cena"]);
                new InsertIzvoz().UpdCENAIzvozUsluga(Convert.ToInt32(red["ID"]), cena);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cena nije sačuvana: " + ex.Message, "Cena", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OsveziUsluge();
            }
        }

        private void sfTerminalske_CellCheckBoxClick(object sender, CellCheckBoxClickEventArgs e)
        {
            if (e.Column.MappingName != "SaPDV")
                return;
            var red = e.Record as DataRowView;
            if (red == null)
                return;

            try
            {
                new InsertIzvoz().UpdPDVIzvozUsluga(Convert.ToInt32(red["ID"]), e.NewValue == CheckState.Checked ? 1 : 0);
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show("PDV nije sačuvan: " + ex.Message, "Sa PDV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Platilac označenih terminalskih usluga postaje izabrani partner
        private void btnPromeniPlatioca_Click(object sender, EventArgs e)
        {
            const string Naslov = "Promeni platioca";
            if (cboPartner.SelectedValue == null)
            {
                MessageBox.Show("Izaberite partnera iz liste.", Naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<int> usluge = OznaceneUsluge(sfTerminalske);
            if (usluge.Count == 0)
            {
                MessageBox.Show("Označite terminalske usluge kojima se menja platilac.", Naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int partner = Convert.ToInt32(cboPartner.SelectedValue);
            var ins = new InsertIzvoz();
            try
            {
                foreach (int id in usluge)
                    ins.UpdPlatiocaIzvozUsluga(id, partner);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Platilac nije promenjen: " + ex.Message, Naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            OsveziUsluge();
        }

        // Briše označene usluge iz sve tri tabele desno
        private void btnObrisi_Click(object sender, EventArgs e)
        {
            const string Naslov = "Obriši selektovane";
            List<int> terminalskeID = OznaceneUsluge(sfTerminalske);
            List<int> dodatneID = OznaceneUsluge(sfDodatne);
            List<int> administrativneID = OznaceneUsluge(sfAdministrativne);
            List<int> sve = terminalskeID.Concat(dodatneID).Concat(administrativneID).ToList();
            if (sve.Count == 0)
            {
                MessageBox.Show("Označite usluge koje se brišu (terminalske, dodatne ili administrativne).", Naslov,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var opis = new List<string>();
            if (terminalskeID.Count > 0)
                opis.Add("terminalskih: " + terminalskeID.Count);
            if (dodatneID.Count > 0)
                opis.Add("dodatnih: " + dodatneID.Count);
            if (administrativneID.Count > 0)
                opis.Add("administrativnih: " + administrativneID.Count);
            if (MessageBox.Show("Obrisati označene usluge (" + string.Join(", ", opis) + ")?", Naslov,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            var ins = new InsertIzvoz();
            try
            {
                foreach (int id in sve)
                    ins.DelIzvozUsluga(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspelo brisanje: " + ex.Message, Naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            OsveziUsluge();
        }

        // ---------------------------------------------------------------------------------
        // Železnina i repozicija izračunate u nalogu (za kontejner izabran u nalogu)
        // ---------------------------------------------------------------------------------
        private void ProveriZelezninuIRepoziciju()
        {
            ProveriUsluguGrupe(zeleznina, 1, "železnina", "Provera železnine");
            ProveriUsluguGrupe(repozicija, 2, "repozicija", "Provera repozicije");
        }

        // Usluga grupe (1 = železnina, 2 = repozicija) se upisuje ako je kontejner nema; ako ima drugu, pita se za zamenu.
        // Postojeća usluga se traži u IzvozVrstaManipulacije (forma za jedan kontejner je železninu tražila u tabeli uvoza,
        // pa ju je upisivala pri svakom otvaranju).
        private void ProveriUsluguGrupe(int nova, int grupa, string naziv, string naslov)
        {
            if (nova <= 0 || kontejnerID <= 0)
                return;

            DataTable t = Ucitaj(
                "SELECT TOP 1 vm.ID FROM IzvozVrstaManipulacije ivm " +
                "INNER JOIN VrstaManipulacije vm ON vm.ID = ivm.IDVrstaManipulacije " +
                "WHERE vm.GrupaVrsteManipulacijeID = @Grupa AND ivm.IDNadredjena = @ID ORDER BY ivm.ID DESC",
                new SqlParameter("@Grupa", grupa), new SqlParameter("@ID", kontejnerID));
            int uneta = t.Rows.Count == 0 ? 0 : Convert.ToInt32(t.Rows[0][0]);
            if (uneta == nova)
                return;

            double cena = 0;
            DataTable c = Ucitaj("SELECT Cena FROM VrstaManipulacije WHERE ID = @ID", new SqlParameter("@ID", nova));
            if (c.Rows.Count > 0 && c.Rows[0][0] != DBNull.Value)
                double.TryParse(Convert.ToString(c.Rows[0][0]), out cena);

            if (uneta == 0)
            {
                new InsertIzvoz().InsUbaciUslugu(kontejnerID, nova, cena, 1, 1, 0, 0, "", 0, "");
                return;
            }

            if (MessageBox.Show("Promenjena je " + naziv + ". Da li da se postojeća usluga zameni novom?", naslov,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(
                "UPDATE IzvozVrstaManipulacije SET IDVrstaManipulacije = @Nova, Cena = @Cena WHERE IDVrstaManipulacije = @Stara AND IDNadredjena = @ID", conn))
            {
                cmd.Parameters.AddWithValue("@Nova", nova);
                cmd.Parameters.AddWithValue("@Cena", cena);
                cmd.Parameters.AddWithValue("@Stara", uneta);
                cmd.Parameters.AddWithValue("@ID", kontejnerID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // ---------------------------------------------------------------------------------
        // Pomoćne
        // ---------------------------------------------------------------------------------
        private DataTable Ucitaj(string sql, params SqlParameter[] parametri)
        {
            var tabela = new DataTable();
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddRange(parametri);
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(tabela);
            }
            return tabela;
        }

        // ID-jevi za IN (...); prazna lista daje 0, pa upit ne vraća ništa
        private static string Lista(IEnumerable<int> idjevi)
        {
            string lista = string.Join(",", idjevi);
            return lista.Length == 0 ? "0" : lista;
        }
    }
}
