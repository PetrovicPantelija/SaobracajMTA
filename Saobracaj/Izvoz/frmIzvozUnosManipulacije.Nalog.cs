using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Saobracaj.Sifarnici;

namespace Saobracaj.Izvoz
{
    // Dodaj usluge iz Naloga za otpremu (frmGrupniUnosPoljaIzvoz): forma dobija sve kontejnere naloga. Korisnik označi jedan ili
    // više kontejnera i jednu ili više usluga, a SNIMI upisuje svaku označenu uslugu za svaki označeni kontejner. Gridovi
    // Terminalske, Dodatne i Administrativne prikazuju usluge označenih kontejnera.
    // Otvorena iz drugih formi (frmIzvoz, frmIzvozKonacna) forma radi kao do sada: kontejneriNaloga je tada null.
    public partial class frmIzvozUnosManipulacije
    {
        private readonly IList<int> kontejneriNaloga;
        private bool kontejneriUcitani;   // do učitavanja kontejnera gridovi usluga prikazuju kontejner izabran u nalogu
        private Timer osvezavanjeUsluga;  // usluge se učitavaju kad korisnik završi označavanje, ne posle svakog reda

        private void PrilagodiZaNalog()
        {
            // precrtano na zahtevu: meni (Prikaži sve / selektovani kontejner, Prenesi uvozne usluge, Kopiraj usluge) i
            // nalogodavci za voz, usluge i drumski sa svojim 'Promeni platioca'; nalogodavci se i dalje prenose iz naloga
            panelHeader.Visible = false;
            foreach (Control c in new Control[] { label55, cboNalogodavac1, button19, label53, cboNalogodavac2, button20, label57, cboNalogodavac3, button21 })
                c.Visible = false;

            // Partner sa svojim 'Promeni platioca' prelazi na mesto prvog nalogodavca
            label2.Location = label55.Location;
            cboUvoznik.Location = cboNalogodavac1.Location;
            button9.Location = button19.Location;

            // kontejneri naloga levo od usluga
            const int SirinaKontejnera = 200;
            const int Razmak = 6;
            var lblKontejneri = new Label
            {
                AutoSize = true,
                Text = "Kontejneri naloga",
                Location = new Point(dataGridView6.Left - 3, label58.Top),
                ForeColor = label58.ForeColor,
                Font = label58.Font
            };
            Controls.Add(lblKontejneri);

            dataGridView1.Location = dataGridView6.Location;
            dataGridView1.Size = new Size(SirinaKontejnera, dataGridView6.Height);
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dataGridView1.MultiSelect = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Visible = true;

            dataGridView6.Left += SirinaKontejnera + Razmak;
            dataGridView6.Width -= SirinaKontejnera + Razmak;
            dataGridView6.MultiSelect = true;
            dataGridView6.SelectionMode = DataGridViewSelectionMode.FullRowSelect;   // klik na uslugu označava ceo red

            osvezavanjeUsluga = new Timer { Interval = 250 };
            osvezavanjeUsluga.Tick += (s, e) =>
            {
                osvezavanjeUsluga.Stop();
                OsveziUslugeKontejnera();
            };
            dataGridView1.SelectionChanged += (s, e) =>
            {
                if (!kontejneriUcitani)
                    return;
                osvezavanjeUsluga.Stop();
                osvezavanjeUsluga.Start();
            };
            Disposed += (s, e) => osvezavanjeUsluga.Dispose();

            // grid pri prvom prikazu forme ume da označi prvi red umesto kontejnera izabranog u nalogu
            Shown += (s, e) =>
            {
                if (kontejneriUcitani && !OznaceniKontejneri().SequenceEqual(new[] { pID }))
                    OznaciKontejnerIzNaloga();
            };
        }

        // Svi kontejneri naloga; označen je kontejner izabran u nalogu
        private void PrikaziKontejnereNaloga()
        {
            List<int> idjevi = kontejneriNaloga.Concat(new[] { pID }).Where(id => id > 0).Distinct().ToList();
            string select = "SELECT Izvoz.ID, Izvoz.BrojKontejnera FROM Izvoz WHERE Izvoz.ID IN ("
                + (idjevi.Count == 0 ? "0" : string.Join(",", idjevi)) + ") ORDER BY Izvoz.ID";

            var tabela = new DataTable();
            using (var da = new SqlDataAdapter(select, connection))
                da.Fill(tabela);

            dataGridView1.DataSource = tabela;
            PodesiDatagridView(dataGridView1);
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 55;
            dataGridView1.Columns[1].HeaderText = "Kontejner";
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            OznaciKontejnerIzNaloga();
            kontejneriUcitani = true;
            OsveziUslugeKontejnera();
        }

        private void OznaciKontejnerIzNaloga()
        {
            dataGridView1.ClearSelection();
            foreach (DataGridViewRow red in dataGridView1.Rows)
            {
                if (Convert.ToInt32(red.Cells[0].Value) == pID)
                {
                    dataGridView1.CurrentCell = red.Cells[0];
                    red.Selected = true;
                }
            }
        }

        private void OsveziUslugeKontejnera()
        {
            FillDG8();
            FillDG8Dodatna();
            FillDG8Administrativna();
        }

        // ID-jevi kontejnera za upite gridova usluga: van naloga jedan kontejner (kao do sada), u nalogu označeni kontejneri
        private string KontejneriZaPrikaz(int podrazumevani)
        {
            if (kontejneriNaloga == null || !kontejneriUcitani)
                return podrazumevani.ToString();

            List<int> oznaceni = OznaceniKontejneri();
            return oznaceni.Count == 0 ? "0" : string.Join(",", oznaceni);
        }

        private List<int> OznaceniKontejneri()
        {
            return dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                .Select(r => Convert.ToInt32(r.Cells[0].Value))
                .OrderBy(id => id)
                .ToList();
        }

        // SNIMI iz naloga: svaka označena usluga za svaki označeni kontejner, sa istim vrednostima kao SNIMI za jedan kontejner
        // (cena 0, količina 1, bez platioca). Scenario se proverava samo za kontejnere kojima su dodate terminalske usluge.
        private void SnimiZaNalog()
        {
            const string Naslov = "Dodaj usluge";
            List<int> kontejneri = OznaceniKontejneri();
            List<DataGridViewRow> usluge = dataGridView6.Rows.Cast<DataGridViewRow>().Where(r => r.Selected).ToList();
            if (kontejneri.Count == 0 || usluge.Count == 0)
            {
                MessageBox.Show("Označite jedan ili više kontejnera naloga i jednu ili više usluga.", Naslov,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ins = new InsertIzvoz();
            var saTerminalskim = new HashSet<int>();
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (DataGridViewRow usluga in usluge)
                {
                    int manipulacija = Convert.ToInt32(usluga.Cells[0].Value);
                    bool terminalska = ProveriAdministrativna(manipulacija) == 0 && ProveriDodatna(manipulacija) == 0;

                    // terminalska usluga nosi pokret, status i formu iz scenarija (kolone 7, 8 i 10 grida 'Po scenariju')
                    string pokret = "/";
                    int status = 0;
                    string forma = "/";
                    if (terminalska)
                    {
                        pokret = Convert.ToString(usluga.Cells[7].Value);
                        int.TryParse(Convert.ToString(usluga.Cells[8].Value), out status);
                        forma = Convert.ToString(usluga.Cells[10].Value);
                    }
                    int orgJed = VratiOrgJed(manipulacija);

                    foreach (int kontejner in kontejneri)
                    {
                        ins.InsUbaciUslugu(kontejner, manipulacija, 0, 1, orgJed, 0, 0, pokret, status, forma);
                        if (terminalska)
                            saTerminalskim.Add(kontejner);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nije uspela selekcija stavki: " + ex.Message, Naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            OsveziUslugeKontejnera();
            ProveriScenarioKontejnera(saTerminalskim);
        }

        // Kao ProveriScenario(), ali za svaki kontejner posebno: terminalske usluge kontejnera (bez dodatnih i administrativnih)
        // porede se sa uslugama scenarija; ako se poklapaju sa izabranim scenarijom, scenario se upisuje kontejneru. Jedna poruka
        // za sve kontejnere.
        private void ProveriScenarioKontejnera(ICollection<int> kontejneri)
        {
            if (kontejneri.Count == 0)
                return;

            int izabrani = Convert.ToInt32(cboScenario.SelectedValue);
            var upisan = new List<string>();
            var bezScenarija = new List<string>();

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();

                var scenariji = new Dictionary<int, List<int>>();
                using (var cmd = new SqlCommand("SELECT ID, Usluga FROM Scenario", conn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = Convert.ToInt32(dr[0]);
                        if (!scenariji.ContainsKey(id))
                            scenariji[id] = new List<int>();
                        scenariji[id].Add(Convert.ToInt32(dr[1]));
                    }
                }

                foreach (int kontejner in kontejneri)
                {
                    var usluge = new List<int>();
                    using (var cmd = new SqlCommand(
                        "SELECT ivm.IDVrstaManipulacije FROM IzvozVrstaManipulacije ivm " +
                        "INNER JOIN VrstaManipulacije vm ON vm.ID = ivm.IDVrstaManipulacije " +
                        "WHERE ivm.IDNadredjena = @ID AND vm.Dodatna = 0 AND vm.Administrativna = 0", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", kontejner);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                            while (dr.Read())
                                usluge.Add(Convert.ToInt32(dr[0]));
                    }

                    bool nasao = false;
                    bool upisano = false;
                    foreach (KeyValuePair<int, List<int>> scenario in scenariji.OrderBy(s => s.Key))
                    {
                        if (scenario.Value.Count != usluge.Count || !ProveriListe(usluge, scenario.Value))
                            continue;
                        nasao = true;
                        if (!upisano && scenario.Key == izabrani)
                        {
                            new InsertScenario().UpdScenarioKontejnera(scenario.Key, kontejner, 2, 0);
                            upisano = true;
                        }
                    }

                    string naziv = NazivKontejnera(kontejner);
                    if (upisano)
                        upisan.Add(naziv);
                    if (!nasao)
                        bezScenarija.Add(naziv);
                }
            }

            var poruka = new List<string>();
            if (upisan.Count > 0)
                poruka.Add("Izabrali ste scenario " + izabrani + (kontejneri.Count > 1 ? " za kontejnere: " + string.Join(", ", upisan) : "") + ".");
            if (bezScenarija.Count > 0)
                poruka.Add("Ne postoji takav scenario" + (kontejneri.Count > 1 ? " za kontejnere: " + string.Join(", ", bezScenarija) : "") + "!");
            if (poruka.Count > 0)
                MessageBox.Show(string.Join(Environment.NewLine, poruka), "Scenario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string NazivKontejnera(int id)
        {
            foreach (DataGridViewRow red in dataGridView1.Rows)
            {
                if (Convert.ToInt32(red.Cells[0].Value) == id)
                {
                    string broj = Convert.ToString(red.Cells[1].Value).Trim();
                    return broj.Length > 0 ? broj : "ID " + id;
                }
            }
            return "ID " + id;
        }
    }
}
