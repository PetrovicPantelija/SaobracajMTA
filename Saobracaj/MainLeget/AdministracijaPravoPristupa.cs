using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.MainLeget
{
    public partial class AdministracijaPravoPristupa : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string Korisnik;

        private ContextMenuStrip _treeMenu;

        public AdministracijaPravoPristupa(string korisnik)
        {
            InitializeComponent();
            Korisnik = korisnik;

            // Desni klik da selektuje čvor
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;

            // Context meni za dodavanje / brisanje forme (i dodelu cele grane)
            // samo za korisnika "test"
            if (string.Equals(Korisnik, "test", StringComparison.OrdinalIgnoreCase))
            {
                InitTreeContextMenu();
            }

            // evente za combo i dugme obavezno nakačiti u Designer-u:
            // cboKorisnik.SelectionChangeCommitted += cboKorisnik_SelectionChangeCommitted;
            // button1.Click += button1_Click;
        }

        private void FillComboBox()
        {
            SqlConnection conn = new SqlConnection(connection);

            var select = "Select RTrim(Korisnik) as Korisnik from Korisnici";
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboKorisnik.DataSource = ds.Tables[0];
            cboKorisnik.DisplayMember = "Korisnik";
            cboKorisnik.ValueMember = "Korisnik";
        }

        private void AdministracijaPravoPristupa_Load(object sender, EventArgs e)
        {
            FillComboBox();
            FillTreeView();
        }

        // model za red iz mainNoviKartice
        private class Kartice
        {
            public int Id { get; set; }
            public int MainId { get; set; }
            public int? ParentId { get; set; }
            public string Naziv { get; set; }
        }

        // info koji čuvamo u Tag svakog TreeNode-a
        private class NodeInfo
        {
            public string Tip { get; set; }   // "Modul" ili "Kartica"
            public int Id { get; set; }       // ID iz MainNovi ili mainNoviKartice
            public int MainId { get; set; }   // MainID modula
        }

        #region TreeView punjenje iz baze

        private void FillTreeView()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            var main = new List<(int ID, string Naziv)>();
            var kartice = new List<Kartice>();

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();

                // Učitavanje modula
                using (var cmd = new SqlCommand("SELECT ID, Naziv FROM MainNovi ORDER BY ID", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        main.Add((reader.GetInt32(0), reader.GetString(1).Trim()));
                    }
                }

                // Učitavanje kartica / stavki
                using (var cmd = new SqlCommand("SELECT ID, MainID, Parent, Naziv FROM mainNoviKartice ORDER BY MainID, ID", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        kartice.Add(new Kartice
                        {
                            Id = reader.GetInt32(0),
                            MainId = reader.GetInt32(1),
                            ParentId = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                            Naziv = reader.GetString(3).Trim()
                        });
                    }
                }
            }

            // Grupisanje kartica po modu
            var modul = kartice
                .GroupBy(k => k.MainId)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Kreiranje stabla
            foreach (var m in main)
            {
                var mainNode = new TreeNode(m.Naziv)
                {
                    Tag = new NodeInfo
                    {
                        Tip = "Modul",
                        Id = m.ID,
                        MainId = m.ID
                    }
                };
                treeView1.Nodes.Add(mainNode);

                if (modul.TryGetValue(m.ID, out var karticeZaModul))
                {
                    AddNode(mainNode, null, karticeZaModul);
                }
            }

            treeView1.CollapseAll();
            treeView1.EndUpdate();
        }

        private void AddNode(TreeNode parentNode, int? parentId, List<Kartice> karticeZaModul)
        {
            var deca = karticeZaModul
                .Where(k => k.ParentId == parentId)
                .OrderBy(k => k.Id);

            foreach (var item in deca)
            {
                var node = new TreeNode(item.Naziv)
                {
                    Tag = new NodeInfo
                    {
                        Tip = "Kartica",
                        Id = item.Id,
                        MainId = item.MainId
                    }
                };
                parentNode.Nodes.Add(node);

                // rekurzivno dodaj naredni nivo
                AddNode(node, item.Id, karticeZaModul);
            }
        }

        #endregion

        #region Context meni (Dodaj / Obriši / Dodeli celu granu) – samo za user "test"

        private void InitTreeContextMenu()
        {
            _treeMenu = new ContextMenuStrip();

            var miDodaj = new ToolStripMenuItem("Dodaj formu");
            miDodaj.Click += MiDodajFormu_Click;

            var miObrisi = new ToolStripMenuItem("Obriši formu");
            miObrisi.Click += MiObrisiFormu_Click;

            var miDodeliGranu = new ToolStripMenuItem("Dodeli pravo za celu granu");
            miDodeliGranu.Click += MiDodeliGranu_Click;

            _treeMenu.Items.Add(miDodaj);
            _treeMenu.Items.Add(miObrisi);
            _treeMenu.Items.Add(new ToolStripSeparator());
            _treeMenu.Items.Add(miDodeliGranu);

            treeView1.ContextMenuStrip = _treeMenu;
        }

        // desni klik selektuje čvor
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
            }
        }

        // mali input dialog da se unese naziv nove forme
        private static string PromptNaziv(string title, string label)
        {
            using (Form f = new Form())
            {
                f.Text = title;
                f.StartPosition = FormStartPosition.CenterParent;
                f.FormBorderStyle = FormBorderStyle.FixedDialog;
                f.MinimizeBox = false;
                f.MaximizeBox = false;
                f.ShowInTaskbar = false;
                f.Width = 400;
                f.Height = 150;

                Label lbl = new Label()
                {
                    Left = 10,
                    Top = 10,
                    Text = label,
                    AutoSize = true
                };

                TextBox txt = new TextBox()
                {
                    Left = 10,
                    Top = 35,
                    Width = 360
                };

                Button btnOk = new Button()
                {
                    Text = "OK",
                    Left = 210,
                    Width = 75,
                    Top = 70,
                    DialogResult = DialogResult.OK
                };

                Button btnCancel = new Button()
                {
                    Text = "Otkaži",
                    Left = 295,
                    Width = 75,
                    Top = 70,
                    DialogResult = DialogResult.Cancel
                };

                f.Controls.Add(lbl);
                f.Controls.Add(txt);
                f.Controls.Add(btnOk);
                f.Controls.Add(btnCancel);

                f.AcceptButton = btnOk;
                f.CancelButton = btnCancel;

                if (f.ShowDialog() == DialogResult.OK)
                {
                    return txt.Text.Trim();
                }

                return null;
            }
        }

        // klik na "Dodaj formu" u context meniju
        private void MiDodajFormu_Click(object sender, EventArgs e)
        {
            // sigurnost: samo za korisnika "test"
            if (!string.Equals(Korisnik, "test", StringComparison.OrdinalIgnoreCase))
                return;

            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null) return;

            var info = selectedNode.Tag as NodeInfo;
            if (info == null) return;

            string naziv = PromptNaziv("Dodaj formu", "Naziv forme:");
            if (string.IsNullOrWhiteSpace(naziv))
                return;

            int mainId;
            int? parentId;

            if (info.Tip == "Modul")
            {
                // Kliknuto na modul → nova stavka je 2. nivo
                mainId = info.MainId;
                parentId = null;
            }
            else
            {
                // Kliknuto na karticu → nova stavka je dete te kartice
                mainId = info.MainId;
                parentId = info.Id;
            }

            int newId;

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();

                string sql = @"
                    INSERT INTO mainNoviKartice (MainID, Parent, Naziv)
                    OUTPUT INSERTED.ID
                    VALUES (@MainID, @Parent, @Naziv);";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MainID", mainId);

                    if (parentId.HasValue)
                        cmd.Parameters.AddWithValue("@Parent", parentId.Value);
                    else
                        cmd.Parameters.AddWithValue("@Parent", DBNull.Value);

                    cmd.Parameters.AddWithValue("@Naziv", naziv);

                    object result = cmd.ExecuteScalar();
                    newId = Convert.ToInt32(result);
                }
            }

            // Ubaci novi čvor u TreeView da se odmah vidi
            var newNode = new TreeNode(naziv)
            {
                Tag = new NodeInfo
                {
                    Tip = "Kartica",
                    Id = newId,
                    MainId = mainId
                }
            };

            selectedNode.Nodes.Add(newNode);
            selectedNode.Expand();
        }

        // klik na "Obriši formu" u context meniju
        private void MiObrisiFormu_Click(object sender, EventArgs e)
        {
            // sigurnost: samo za korisnika "test"
            if (!string.Equals(Korisnik, "test", StringComparison.OrdinalIgnoreCase))
                return;

            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null) return;

            var info = selectedNode.Tag as NodeInfo;
            if (info == null) return;

            // dozvoljavamo brisanje samo kartica (forma), ne modula
            if (info.Tip != "Kartica")
            {
                MessageBox.Show("Možete obrisati samo formu (stavku), ne i glavni modul.",
                    "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // ne dozvoljavamo brisanje čvora koji ima decu
            if (selectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("Ne možete obrisati stavku koja ima podstavke. Prvo obrišite podstavke.",
                    "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dr = MessageBox.Show(
                "Da li ste sigurni da želite da obrišete ovu formu?",
                "Potvrda brisanja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            int karticaId = info.Id;

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // prvo obriši sva prava na ovu formu
                        string delPrava = "DELETE FROM MainNoviPrava WHERE KarticaID = @KarticaID";
                        using (var cmdPrava = new SqlCommand(delPrava, conn, tran))
                        {
                            cmdPrava.Parameters.AddWithValue("@KarticaID", karticaId);
                            cmdPrava.ExecuteNonQuery();
                        }

                        // zatim obriši formu iz mainNoviKartice
                        string delKartica = "DELETE FROM mainNoviKartice WHERE ID = @Id";
                        using (var cmdKartica = new SqlCommand(delKartica, conn, tran))
                        {
                            cmdKartica.Parameters.AddWithValue("@Id", karticaId);
                            cmdKartica.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Greška pri brisanju forme: " + ex.Message,
                            "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // ukloni čvor iz TreeView-a
            selectedNode.Remove();
        }

        // klik na "Dodeli pravo za celu granu"
        private void MiDodeliGranu_Click(object sender, EventArgs e)
        {
            if (!string.Equals(Korisnik, "test", StringComparison.OrdinalIgnoreCase))
                return;

            var selectedNode = treeView1.SelectedNode;
            if (selectedNode == null) return;

            // Čekiraj izabrani čvor i sve njegove potomke
            SetCheckedRecursive(selectedNode, true);

            // Ažuriraj roditelje (da i oni budu čekirani ako treba – kozmetika)
            UpdateParentCheckUpwards(selectedNode.Parent);
        }

        #endregion

        #region Pomoćne metode za obilazak TreeView-a

        private IEnumerable<TreeNode> GetAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                yield return n;
                foreach (TreeNode child in GetAllNodes(n.Nodes))
                {
                    yield return child;
                }
            }
        }

        private void ClearAllChecks()
        {
            foreach (TreeNode n in GetAllNodes(treeView1.Nodes))
            {
                n.Checked = false;
            }
        }

        // rekurzivno setovanje Checked za čvor + decu
        private void SetCheckedRecursive(TreeNode node, bool isChecked)
        {
            node.Checked = isChecked;
            foreach (TreeNode child in node.Nodes)
            {
                SetCheckedRecursive(child, isChecked);
            }
        }

        // ažurira roditelje da budu čekirani ako bilo koje dete jeste
        private void UpdateParentCheckUpwards(TreeNode node)
        {
            while (node != null)
            {
                bool anyChildChecked = node.Nodes.Cast<TreeNode>().Any(c => c.Checked);
                if (anyChildChecked)
                    node.Checked = true;

                node = node.Parent;
            }
        }

        #endregion

        #region Učitavanje i snimanje prava (MainNoviPrava)

        // Poziva se na SelectionChangeCommitted od cboKorisnik
        private void cboKorisnik_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var korisnik = cboKorisnik.SelectedValue?.ToString().Trim();
            if (string.IsNullOrEmpty(korisnik))
                return;

            LoadRightsForUser(korisnik);
        }

        private void LoadRightsForUser(string korisnik)
        {
            // 1) Skloni sve check-ove
            ClearAllChecks();

            // 2) Učitaj iz MainNoviPrava prava za korisnika
            var allowedKartice = new HashSet<int>();   // KarticaID > 0
            var allowedModuli = new HashSet<int>();    // MainID za koje je KarticaID = 0

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();

                string sql = "SELECT MainID, KarticaID FROM MainNoviPrava WHERE Korisnik = @Korisnik";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Korisnik", korisnik);

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            int mainId = rd.GetInt32(0);
                            int kartId = rd.GetInt32(1);

                            if (kartId == 0)
                                allowedModuli.Add(mainId);
                            else
                                allowedKartice.Add(kartId);
                        }
                    }
                }
            }

            // 3) Prođi kroz TreeView i checkiraj odgovarajuće čvorove
            foreach (TreeNode node in GetAllNodes(treeView1.Nodes))
            {
                var info = node.Tag as NodeInfo;
                if (info == null) continue;

                if (info.Tip == "Kartica")
                {
                    node.Checked = allowedKartice.Contains(info.Id);
                }
                else if (info.Tip == "Modul")
                {
                    node.Checked = allowedModuli.Contains(info.Id);
                }
            }

            // 4) Dodatno: ako neki modul nema direktno pravo,
            //    ali neka njegova forma ima check, označi i njega (kozmetika)
            foreach (TreeNode root in treeView1.Nodes)
            {
                UpdateParentCheckState(root);
            }
        }

        // propagira check stanje na roditelje (root module), čisto kozmetički
        private void UpdateParentCheckState(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                UpdateParentCheckState(child);
            }

            if (node.Nodes.Count > 0)
            {
                bool anyChildChecked = node.Nodes.Cast<TreeNode>().Any(c => c.Checked);
                if (anyChildChecked)
                    node.Checked = true;
            }
        }

        // Klik na button1 – snimanje prava
        private void button1_Click(object sender, EventArgs e)
        {
            var korisnik = cboKorisnik.SelectedValue?.ToString().Trim();
            if (string.IsNullOrEmpty(korisnik))
            {
                MessageBox.Show("Nije izabran korisnik.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 1) Obriši sva postojeća prava za korisnika
                        string deleteSql = "DELETE FROM MainNoviPrava WHERE Korisnik = @Korisnik";
                        using (var cmdDel = new SqlCommand(deleteSql, conn, tran))
                        {
                            cmdDel.Parameters.AddWithValue("@Korisnik", korisnik);
                            cmdDel.ExecuteNonQuery();
                        }

                        // 2) Upiši sva nova prava (moduli + sve checkirane kartice)
                        string insertSql = @"INSERT INTO MainNoviPrava (MainID, KarticaID, Korisnik)
                                             VALUES (@MainID, @KarticaID, @Korisnik)";

                        using (var cmdIns = new SqlCommand(insertSql, conn, tran))
                        {
                            var pMain = cmdIns.Parameters.Add("@MainID", SqlDbType.Int);
                            var pKartica = cmdIns.Parameters.Add("@KarticaID", SqlDbType.Int);
                            var pKorisnik = cmdIns.Parameters.Add("@Korisnik", SqlDbType.NVarChar, 50);

                            pKorisnik.Value = korisnik;

                            foreach (TreeNode node in GetAllNodes(treeView1.Nodes))
                            {
                                var info = node.Tag as NodeInfo;
                                if (info == null) continue;

                                // prvo upis prava na modul
                                if (info.Tip == "Modul" && node.Checked)
                                {
                                    pMain.Value = info.Id;   // ID modula iz MainNovi
                                    pKartica.Value = 0;      // 0 označava "samo modul"
                                    cmdIns.ExecuteNonQuery();
                                }

                                // zatim prava na konkretne forme (kartice)
                                if (info.Tip == "Kartica" && node.Checked)
                                {
                                    pMain.Value = info.MainId;  // MainID iz mainNoviKartice
                                    pKartica.Value = info.Id;   // ID kartice
                                    cmdIns.ExecuteNonQuery();
                                }
                            }
                        }

                        tran.Commit();
                        MessageBox.Show("Prava su uspešno sačuvana.", "Informacija",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Greška pri snimanju prava: " + ex.Message,
                            "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion
    }
}
