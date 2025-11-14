using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;


namespace Saobracaj.Drumski
{
    public partial class frmPregledNalogaDrumski : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private string _prethodnaVrednost = null;
        private SqlDataAdapter dataAdapter;
        private DataTable mainTable;
        int? Nalogodavac = null;
        string dan = null;
        private int? nalogID;
        public frmPregledNalogaDrumski()
        {
            InitializeComponent();
            ChangeTextBox();
            RefreshGrid();
        }

        public frmPregledNalogaDrumski(int NalogID, int? NalogodavacID, string Dan)
        {
            nalogID = NalogID;
            Nalogodavac = NalogodavacID;
            dan = Dan;
            InitializeComponent();
            ChangeTextBox();
            RefreshGrid();
            // vec imaju kreiran nalog id pa nema potrebe da budu vidljva sledeca dugmad
            btnFormiranjeNaloga.Visible = false;
            btnDopunaNaloga.Visible = false;
            btnDodeliKamion.Visible = false;
            this.Text = "Lista kontejnera naloga broj " + NalogID.ToString();

        }

        private void ChangeTextBox()
        {
            panelHeader.Visible = false;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
   
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                panelHeader.Visible = false;
    
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void RefreshGrid()
        {
            var s_connection = Sifarnici.frmLogovanje.connectionString;
            var connection = new SqlConnection(s_connection);
            List<string> statusi = new List<string>();
            using (connection)
            {
                connection.Open();

                // 1. Učitaj status vrednosti iz SistemskePostavke
                SqlCommand cmd1 = new SqlCommand("SELECT Vrednost FROM SistemskePostavke WHERE Naziv LIKE 'StatusKamiona%'", connection);
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statusi.Add(reader.GetString(0));
                    }
                }

                String conditionRadniNalogID = "";
                String conditionNalogodavac = " 1 = 1 ";
                if (nalogID !=null && nalogID > 0 )
                {
                    conditionRadniNalogID = " AND rn.NalogID  = @NalogID ";
                }
                if (Nalogodavac != null)
                {
                    conditionNalogodavac += $" AND  x.NalogodavacID = {Nalogodavac} ";

                    if (dan == "D")
                    {
                        conditionRadniNalogID += $" AND rn.DatumIstovara >= CAST(GetDate() AS DATE) AND rn.DatumIstovara < DATEADD(DAY, 1, CAST(GetDate() AS DATE))";
                        // Alternativno, ako je DatumIstovara samo datum:
                        // conditionRadniNalogID += $" AND rn.DatumIstovara = CAST(GetDate() AS DATE)";
                    }
                    else
                    {
                  
                        conditionRadniNalogID += $" AND rn.DatumIstovara >= DATEADD(DAY, 1, CAST(GetDate() AS DATE)) AND rn.DatumIstovara < DATEADD(DAY, 2, CAST(GetDate() AS DATE))";

                    }
                }

                // 2. Priprema statusa za upit
                string statusiZaUpit = string.Join(",", statusi
                    .Select(s => s.Trim())
                    .Where(s => int.TryParse(s, out _)));
                //samo oni koji imaju raspored voya
                var select = $@"
                    SELECT *
                    FROM (
                            SELECT rn.ID,
                                    pa.PaNaziv as Nalogodavac,
                                    ik.Klijent3 AS NalogodavacID,
                                    ik.BrojKontejnera,
                                    tk.SkNaziv AS TipKontejnera,
                                    rn.NalogID,
                                    CONVERT(varchar,rn.DatumKreiranjaNaloga,104) AS KreiranjeNaloga,
                                    rn.KamionID,
                                    a.RegBr,
                                    a.Vozac,
                                    rn.Status,   
                                    rn.Status AS StatusID, 
                                    CONVERT(varchar,rn.DatumPromeneStatusa,104) AS PromenaStatusa,
                                    rn.KontejnerID,  
                                    CASE  WHEN ISNULL(rn.KamionID, 0) > 0 THEN 'Raspoređen'
                                    ELSE 'Neraspoređen' END AS Trenutno,
                                    ri.ID AS RadniNalogInterniID
                            FROM RadniNalogDrumski rn
                            LEFT JOIN Automobili a ON rn.KamionID = a.ID
                            LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                            INNER JOIN IzvozKonacna ik ON ik.ID = rn.KontejnerID
                            LEFT JOIN Partnerji pa ON pa.PaSifra = ik.Klijent3
                            LEFT JOIN TipKontenjera tk ON ik.VrstaKontejnera = tk.ID
                            LEFT JOIN Radninaloginterni ri on ri.konkretaidusluge = rn.UKID
                            WHERE rn.Uvoz = 0  AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1  AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit})) {conditionRadniNalogID}

                            union all
                                    SELECT rn.ID,
                                    pa.PaNaziv as Nalogodavac,
                                    i.Klijent3 AS NalogodavacID,
                                    i.BrojKontejnera,
                                    tk.SkNaziv AS TipKontejnera,
                                    rn.NalogID,
                                    CONVERT(varchar, rn.DatumKreiranjaNaloga, 104) AS KreiranjeNaloga,
                                    rn.KamionID,
                                    a.RegBr,
                                    a.Vozac,
                                    rn.Status,   
                                    rn.Status AS StatusID, 
                                    CONVERT(varchar, rn.DatumPromeneStatusa, 104) AS PromenaStatusa,
                                    rn.KontejnerID, 
                                    CASE  WHEN ISNULL(rn.KamionID, 0) > 0 THEN 'Raspoređen'
                                    ELSE 'Neraspoređen' END AS Trenutno,
                                    ri.ID AS RadniNalogInterniID
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN Izvoz i ON i.ID = rn.KontejnerID
                        LEFT JOIN Partnerji pa ON pa.PaSifra = i.Klijent3
                        LEFT JOIN TipKontenjera tk ON i.VrstaKontejnera = tk.ID
                        LEFT JOIN Radninaloginterni ri on ri.konkretaidusluge = rn.UKID
                        WHERE rn.Uvoz = 0 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1  AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit})) {conditionRadniNalogID}

                        union all
                        SELECT rn.ID, 
                               pa.PaNaziv as Nalogodavac,
                               uk.Nalogodavac3 AS NalogodavacID,
                               uk.BrojKontejnera,
                               tk.SkNaziv AS TipKontejnera,
                               rn.NalogID,
                               CONVERT(varchar,rn.DatumKreiranjaNaloga,104) AS KreiranjeNaloga,
                               rn.KamionID,
                               a.RegBr,
                               a.Vozac,
                               rn.Status,   
                               rn.Status AS StatusID, 
                               CONVERT(varchar,rn.DatumPromeneStatusa,104) AS PromenaStatusa,
                               rn.KontejnerID, 
                               CASE  WHEN ISNULL(rn.KamionID, 0) > 0 THEN 'Raspoređen'
                               ELSE 'Neraspoređen' END AS Trenutno,
                               ri.ID AS RadniNalogInterniID
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN UvozKonacna uk ON uk.ID = rn.KontejnerID
                        LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3
                        LEFT JOIN TipKontenjera tk ON uk.TipKontejnera = tk.ID
                        LEFT JOIN Radninaloginterni ri on ri.konkretaidusluge = rn.UKID
                        WHERE rn.Uvoz = 1 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1  AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit})) {conditionRadniNalogID}

                        union all
                               SELECT rn.ID, 
                               pa.PaNaziv as Nalogodavac,
                               uk.Nalogodavac3 AS NalogodavacID,
                               uk.BrojKontejnera,
                               tk.SkNaziv AS TipKontejnera,
                               rn.NalogID,
                               CONVERT(varchar, rn.DatumKreiranjaNaloga, 104) AS KreiranjeNaloga,
                               rn.KamionID,
                               a.RegBr,
                               a.Vozac,
                               rn.Status,   
                               rn.Status AS StatusID, 
                               CONVERT(varchar, rn.DatumPromeneStatusa, 104) AS PromenaStatusa,
                               rn.KontejnerID, 
                               CASE  WHEN ISNULL(rn.KamionID, 0) > 0 THEN 'Raspoređen'
                               ELSE 'Neraspoređen' END AS Trenutno,
                               ri.ID AS RadniNalogInterniID
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN Uvoz uk ON uk.ID = rn.KontejnerID
                        LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3
                        LEFT JOIN TipKontenjera tk ON uk.TipKontejnera = tk.ID
                        LEFT JOIN Radninaloginterni ri on ri.konkretaidusluge = rn.UKID
                        WHERE rn.Uvoz = 1 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1  AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit})) {conditionRadniNalogID}

                        union all
                               SELECT rn.ID, 
                               pa.PaNaziv as Nalogodavac,
                               rn.Klijent ASNalogodavac,
                               rn.BrojKontejnera as BrojKontejnera,
                               tk.SkNaziv AS TipKontejnera,
                               rn.NalogID,
                               CONVERT(varchar, rn.DatumKreiranjaNaloga, 104) AS KreiranjeNaloga,
                               rn.KamionID,
                               a.RegBr,
                               a.Vozac,
                               rn.Status,   
                               rn.Status AS StatusID, 
                               CONVERT(varchar, rn.DatumPromeneStatusa, 104) AS PromenaStatusa,
                               rn.KontejnerID, 
                               CASE  WHEN ISNULL(rn.KamionID, 0) > 0 THEN 'Raspoređen'
                               ELSE 'Neraspoređen' END AS Trenutno,
                               ri.ID AS RadniNalogInterniID
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        LEFT JOIN Partnerji pa ON pa.PaSifra = rn.Klijent
                        LEFT JOIN TipKontenjera tk ON rn.TipKontejnera = tk.ID
                        LEFT JOIN Radninaloginterni ri on ri.konkretaidusluge = rn.UKID
                        WHERE rn.Uvoz in (-1,2, 3, 4, 5) AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1  AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit})) {conditionRadniNalogID}
                      ) x
                    WHERE {conditionNalogodavac}
                    ORDER BY ID DESC";

                dataAdapter = new SqlDataAdapter(select, connection);
                if (nalogID!=0 && nalogID > 0)
                {
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@NalogID", nalogID);
                }
                var commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Napuni glavnu tabelu
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                mainTable = ds.Tables[0];

                // Napuni status listu
                var stv = "SELECT ID, Naziv FROM StatusVozila ORDER BY Naziv";
                var stvAD = new SqlDataAdapter(stv, s_connection);
                var stvDS = new DataSet();
                stvAD.Fill(stvDS);
                var dtStatus = stvDS.Tables[0];

                // Očisti null vrednosti
                foreach (DataRow row in mainTable.Rows)
                {
                    if (row.IsNull("Status"))
                        row["Status"] = DBNull.Value;
                }

                // Veži glavnu tabelu
                gridGroupingControl1.DataSource = mainTable;

                // Postavi Syncfusion Visual Style (lepši izgled grida i kontrola)
                gridGroupingControl1.TableOptions.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2016Colorful;

                // Pristupi koloni Status i definiši njen izgled
                var statusCol = gridGroupingControl1.TableDescriptor.Columns["Status"];
                var cellStyle = statusCol.Appearance.AnyRecordFieldCell;

                // Podešavanje ComboBox-a
                cellStyle.CellType = "ComboBox";
                cellStyle.DataSource = dtStatus;
                cellStyle.DisplayMember = "Naziv";
                cellStyle.ValueMember = "ID";
                cellStyle.ExclusiveChoiceList = true;
                cellStyle.DropDownStyle = GridDropDownStyle.AutoComplete;

                // Ulepšavanje izgleda
                cellStyle.Font.Facename = "Segoe UI";
                cellStyle.Font.Size = 10;
                cellStyle.HorizontalAlignment = GridHorizontalAlignment.Left;
                cellStyle.VerticalAlignment = GridVerticalAlignment.Middle;
                cellStyle.BackColor = Color.White;
                cellStyle.TextColor = Color.Black;
                cellStyle.VerticalAlignment = GridVerticalAlignment.Middle;
                cellStyle.HorizontalAlignment = GridHorizontalAlignment.Left;
                cellStyle.TextMargins = new GridMarginsInfo(2, 2, 2, 2);
                cellStyle.Borders.All = new GridBorder(GridBorderStyle.Solid, Color.LightGray, GridBorderWeight.ExtraThin);

                // Ukloni kolone koje ne želiš da se vide
                var colsToRemove = new[] { "KontejnerID", "StatusID" , "RadniNalogInterniID", "NalogodavacID" }; // "Status" je Naziv
                foreach (var col in colsToRemove)
                {
                    if (gridGroupingControl1.TableDescriptor.VisibleColumns.Contains(col))
                    {
                        gridGroupingControl1.TableDescriptor.VisibleColumns.Remove(col);
                    }
                }

                // Filter bar
                gridGroupingControl1.ShowGroupDropArea = true;
                gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

                foreach (GridColumnDescriptor column in gridGroupingControl1.TableDescriptor.Columns)
                {
                    column.AllowFilter = true;
                }

                GridExcelFilter excelFilter = new GridExcelFilter();
                excelFilter.WireGrid(gridGroupingControl1);

                if (gridGroupingControl1.TableDescriptor.Columns.Contains("Status"))
                {
                    gridGroupingControl1.TableDescriptor.Columns["Status"].Width = 130;
                }
                gridGroupingControl1.TableControlCurrentCellAcceptedChanges -= GridGroupingControl1_TableControlCurrentCellAcceptedChanges;
                gridGroupingControl1.TableControlCurrentCellAcceptedChanges += GridGroupingControl1_TableControlCurrentCellAcceptedChanges;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (ActivateExistingForm("frmDrumski"))
            {
                // Ako je forma već otvorena i aktivirana, prekidamo izvršavanje.
                return;
            }

            if (gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                // Uzimamo prvi selektovani red
                Record rec = gridGroupingControl1.Table.SelectedRecords[0].Record;

                if (rec != null)
                {
                    int ID = Convert.ToInt32(rec.GetValue("ID"));
                    frmDrumski pnd = new frmDrumski(ID);
                    pnd.FormClosed += pnd_FormClosed;
                    pnd.Show();
                }
            }
            else if (nalogID > 0)
            {
                frmDrumski pnd = new frmDrumski("", nalogID);
                pnd.FormClosed += pnd_FormClosed;
                pnd.Show();
            }
            else
            {
                frmDrumski pnd = new frmDrumski();
                pnd.FormClosed += pnd_FormClosed;
                pnd.Show();
            }
        }

        private bool ActivateExistingForm(string formName)
        {
            // Prolazi kroz sve otvorene forme u aplikaciji
            foreach (Form frm in Application.OpenForms)
            {
                // Upoređujemo ime forme koju tražimo
                if (frm.Name == formName)
                {
                    // Forma je pronađena!

                    // 1. Dovedite je u prvi plan
                    frm.Activate();

                    // 2. Vratite je iz minimizovanog stanja, ako je minimizovana
                    if (frm.WindowState == FormWindowState.Minimized)
                    {
                        frm.WindowState = FormWindowState.Normal;
                    }

                    return true; // Vraćamo true jer je forma aktivirana
                }
            }
            return false; // Forma nije pronađena
        }

        private void pnd_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshGrid(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmPakovanjeKamiona kam = new frmPakovanjeKamiona();
            kam.Show();
            PakovanjeKamiona1 pk = new PakovanjeKamiona1();
            pk.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            panelStatus.Visible = true;
            var stv = "select ID, Naziv from StatusVozila order by Naziv";
            var stvAD = new SqlDataAdapter(stv, conn);
            var stvDS = new DataSet();
            stvAD.Fill(stvDS);

            System.Data.DataTable dt = stvDS.Tables[0];
            DataRow prazanRed = dt.NewRow();
            prazanRed["ID"] = DBNull.Value;
            prazanRed["Naziv"] = "";
            dt.Rows.InsertAt(prazanRed, 0);

            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "Naziv";
            cboStatus.ValueMember = "ID";
        }

        private void GridGroupingControl1_TableControlCurrentCellAcceptedChanges(object sender, GridTableControlCancelEventArgs e)
        {
            var cc = e.TableControl.CurrentCell;
            var style = e.TableControl.Model[cc.RowIndex, cc.ColIndex];
            var identity = style.CellIdentity as GridTableCellStyleInfoIdentity;

            if (identity?.DisplayElement is GridRecordRow recordRow)
            {
                var record = recordRow.ParentRecord;
                string kolona = identity.Column.MappingName;

                if (kolona == "Status")
                {
                    var red = record.GetData() as DataRowView;
                    var novaVrednost = red["Status"];
                    var id = red["ID"];

                    InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                    ins.UpdateStatusRadniNalogDrumski(Convert.ToInt32(id), novaVrednost == DBNull.Value ? null : (int?)novaVrednost);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panelStatus.Visible = false;
        }
        private int PostaviVrednostZaposleni()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int ulogovaniZaposleniID = 0;

            con.Open();

            SqlCommand cmd = new SqlCommand("Select  k.DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Zaposleni " +
                                            " FROM Korisnici k " +
                                            "INNER JOIN Delavci d ON k.DeSifra = d.DeSifra " +
                                            "where Trim(Korisnik) like '" + Saobracaj.Sifarnici.frmLogovanje.user.Trim() + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["ID"] != DBNull.Value)
                    ulogovaniZaposleniID = Convert.ToInt32(dr["ID"].ToString());
            }
            return ulogovaniZaposleniID;

        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            int noviStatusId;
            if (cboStatus.SelectedValue != null && int.TryParse(cboStatus.SelectedValue.ToString(), out noviStatusId))
            {
                // učitavanje statusa iz SistemskePostavke
                var statusi = new List<int>();
                var s_connection = Sifarnici.frmLogovanje.connectionString;
                using (var connection = new SqlConnection(s_connection))
                {
                    connection.Open();
                    var cmd1 = new SqlCommand("SELECT Vrednost FROM SistemskePostavke WHERE Naziv LIKE 'StatusKamiona%'", connection);

                    using (var reader = cmd1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (int.TryParse(reader.GetString(0).Trim(), out int parsed))
                            {
                                statusi.Add(parsed);
                            }
                        }
                    }
                }

                // sada se zna da li noviStatusId spada u tu listu
                bool trebaOkidatiInterni = statusi.Contains(noviStatusId);
                InsertRadniNalogDrumski upd = new InsertRadniNalogDrumski();
                InsertRadniNalogInterni updi = new InsertRadniNalogInterni();
                foreach (SelectedRecord record in gridGroupingControl1.Table.SelectedRecords)
                {
                    Record rec = record.Record;

                    var id = rec.GetValue("ID");

                    int? radniNalogInterniID = null;
                    var interniVal = rec.GetValue("RadniNalogInterniID");
                    if (interniVal != null && interniVal != DBNull.Value)
                    {
                        if (int.TryParse(interniVal.ToString(), out int parsedInterni))
                        {
                            radniNalogInterniID = parsedInterni;
                        }
                    }
                   
                    upd.UpdateStatusRadniNalogDrumski(Convert.ToInt32(id), noviStatusId);

                    if (trebaOkidatiInterni && radniNalogInterniID.HasValue)
                    {
                          updi.UpdRadniNalogInterniZavrsen(Convert.ToInt32(radniNalogInterniID.Value), Saobracaj.Sifarnici.frmLogovanje.user.Trim());
                    }
                }
            }
            panelStatus.Visible = false;
            RefreshGrid();
        }

        private void btnFormiranjeNaloga_Click(object sender, EventArgs e)
        {
            InsertRadniNalogDrumski isu = new InsertRadniNalogDrumski();

            List<int> stavke = new List<int>();

            foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
            {
                object nalogIdValue = selectedRecord.Record.GetValue("NalogID");
                if (nalogIdValue != null && int.TryParse(nalogIdValue.ToString(), out int nalogId) && nalogId != 0)
                {
                    MessageBox.Show("Radni nalog se može kreirati samo za stavke koje još nisu dodeljene nalogu.",
                                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int ID = Convert.ToInt32(selectedRecord.Record.GetValue("ID"));
                stavke.Add((ID));
            }

            if (stavke.Count > 0)
            {
                isu.PostaviNalogIDNaRedove(stavke);
                RefreshGrid();
            }
        }


        private void btnDopunaNaloga_Click_1(object sender, EventArgs e)
        {
            InsertRadniNalogDrumski isu = new InsertRadniNalogDrumski();
            List<int> stavkeBezNaloga = new List<int>();
            HashSet<int> nalogIds = new HashSet<int>();

            foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
            {
                object nalogIdValue = selectedRecord.Record.GetValue("NalogID");
                int nalogId = 0;
                if (nalogIdValue != null && int.TryParse(nalogIdValue.ToString(), out int parsedId))
                {
                    nalogId = parsedId;
                }

                if (nalogId != 0)
                {
                    nalogIds.Add(nalogId);
                }
                else
                {
                    int ID = Convert.ToInt32(selectedRecord.Record.GetValue("ID"));
                    stavkeBezNaloga.Add( ID);
                }
            }

            if (nalogIds.Count > 1)
            {
                MessageBox.Show("Stavke se mogu dodati samo u jedan nalog. Selektovano je više različitih naloga.",
                                "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nalogIds.Count == 1 && stavkeBezNaloga.Count > 0)
            {
                int nalogId = nalogIds.First();

                DialogResult result = MessageBox.Show($"Da li želite da dodate stavke u postojeći nalog ID: {nalogId}?",
                                                      "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    isu.UpdateNalogIDRadniNalogDrumski(stavkeBezNaloga, nalogId);
                    RefreshGrid();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (gridGroupingControl1.Table.SelectedRecords.Count == 0)
            {
                MessageBox.Show("Nije selektovan nijedan red.");
                return;
            }
            var result = MessageBox.Show(
                    $"Da li pravite kopiju za nalog {nalogID}?",
                    "Potvrda kopiranja",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

            // Ako korisnik zatvori dijalog (klik na X), ne radi ništa
            if (result == DialogResult.Cancel)
            {
                return;
            }

            int? duplikatNalogID;

            if (result == DialogResult.Yes)
                duplikatNalogID = nalogID;   // ako je korisnik kliknuo "Da"
            else
                duplikatNalogID = null;
            bool sveUspešno = true;
            InsertRadniNalogDrumski isu = new InsertRadniNalogDrumski();
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
            {
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                // Uzimanje ID-ja selektovanog reda
                int id = Convert.ToInt32(selectedRecord.Record.GetValue("ID"));

                con.Open();

                 SqlCommand cmd = new SqlCommand("SELECT	rn.ID ," +
                 " rn.Uvoz, rn.Status, rn.AutoDan,  rn.MestoPreuzimanjaKontejnera, rn.Ref, " +
                 " ik.Klijent3 AS Klijent, ik.MesoUtovara AS MestoUtovara,  (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,rn.DatumUtovara, rn.MestoIstovara AS MestoIstovara, rn.KontaktOsobaNaIstovaru,  rn.DatumIstovara, rn.AdresaIstovara,  " +
                 " rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz,  " +
                 " rn.Trosak, rn.Valuta, ik.BookingBrodara,   ik.BrodskaPlomba AS BrojPlombe,  '' AS BrodskaTeretnica,  " +
                 " ik.VGMBrod AS BTTKontejnetra, ik.BrutoRobe AS BTTRobe, " +
                 " ik.NapomenaZaRobu as NapomenaZaPozicioniranje , rn.Cena, cc.Naziv AS CarinjenjeIzvozno," +
                 "  '' AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski, " +
                 " ik.MestoCarinjenja as polaznaCarinarnica , 0 AS OdredisnaCarina, ik.Spedicija as polaznaSpedicija, 0 as OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt " +
                 "FROM    RadniNalogDrumski rn " +
                          "INNER JOIN IzvozKonacna ik ON rn.KontejnerID = ik.ID " +
                          "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = ik.MesoUtovara AND pko.PaKOZapSt = ik.KontaktOsoba " +
                          "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = ik.NapomenaReexport " +
                          "LEFT JOIN Carinarnice cc on cc.ID = ik.MestoCarinjenja " +
                          "LEFT JOIN IzvozKonacnaZaglavlje ukz ON ukz.ID = ik.IDNadredjena " +
                          "LEFT JOIN Voz v ON v.ID = ukz.IDVoza " +
                 "where rn.ID=" + id + " AND rn.Uvoz = 0 " +
                 "UNION " +
                 "SELECT	rn.ID ," +
                 " rn.Uvoz, rn.Status, rn.AutoDan,  rn.MestoPreuzimanjaKontejnera, rn.Ref, " +
                 " i.Klijent3 AS Klijent,  i.MesoUtovara AS MestoUtovara, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara, rn.DatumUtovara, rn.MestoIstovara AS MestoIstovara, rn.KontaktOsobaNaIstovaru, rn.DatumIstovara, rn.AdresaIstovara, " +
                 " rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz, " +
                 " rn.Trosak, rn.Valuta, i.BookingBrodara,  i.BrodskaPlomba AS BrojPlombe, '' AS BrodskaTeretnica,  " +
                 " i.VGMBrod AS BTTKontejnetra,  i.BrutoRobe AS BTTRobe, " +
                 " i.NapomenaZaRobu AS NapomenaZaPozicioniranje,  rn.Cena, cc.Naziv AS CarinjenjeIzvozno," +
                 " '' AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, '' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski, " +
                 " i.MestoCarinjenja as polaznaCarinarnica , 0 AS OdredisnaCarina, i.Spedicija as polaznaSpedicija, 0 as OdredisnaSpedicija,'' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt " +
                 "FROM    RadniNalogDrumski rn " +
                          "INNER JOIN  Izvoz i ON rn.KontejnerID = i.ID  " +
                          "LEFT JOIN partnerjiKontOsebaMU pko ON  pko.PaKOSifra = i.MesoUtovara AND pko.PaKOZapSt = i.KontaktOsoba " +
                          "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = i.NapomenaReexport " +
                          "LEFT JOIN Carinarnice cc on cc.ID = i.MestoCarinjenja " +
                 "where rn.ID=" + id + " AND rn.Uvoz = 0 " +
                 "UNION " +
                 "SELECT rn.ID ," +
                 " rn.Uvoz,rn.Status, rn.AutoDan, rn.MestoPreuzimanjaKontejnera, uk.Ref3 AS Ref, " +
                 " uk.Nalogodavac3 AS Klijent,  rn.MestoUtovara, rn.AdresaUtovara, rn.DatumUtovara, uk.MestoIstovara AS MestoIstovara,uk.KontaktOsobe as KontaktOsobaNaIstovaru, rn.DatumIstovara, (Rtrim(pko.PaKOOpomba)) AS AdresaIstovara, " +
                 " rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz, " +
                 " rn.Trosak,rn.Valuta,0 AS BookingBrodara,  '' AS BrojPlombe,  uk.BrodskaTeretnica, " +
                 " uk.BrutoKontejnera AS BTTKontejnetra, uk.BrutoRobe AS BTTRobe," +
                 " np.Naziv as NapomenaZaPozicioniranje, rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno,  " +
                 " rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta AS TipTransportaDrumski, " +
                 " 0 as polaznaCarinarnica, uk.OdredisnaCarina as OdredisnaCarina, 0 as polaznaSpedicija, uk.OdredisnaSpedicija as OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt " +
                 "FROM  RadniNalogDrumski rn " +
                        "INNER JOIN UvozKonacna uk ON rn.KontejnerID = uk.ID " +
                        "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = uk.MestoIstovara AND PaKOZapSt = uk.AdresaMestaUtovara " + /*AND PaKOSifra = mu.Naziv*/
                        "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = uk.NapomenaZaPozicioniranje " +
                        "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = uk.PostupakSaRobom " +
                        "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = uk.CarinskiPostupak " +
                        "LEFT JOIN Carinarnice c on c.ID = uk.OdredisnaCarina " +
                        "LEFT JOIN Partnerji p2 on p2.PaSifra = uk.OdredisnaSpedicija " +
                        "LEFT JOIN UvozKonacnaZaglavlje ukz ON ukz.ID = uk.IDNadredjeni " +
                        "LEFT JOIN Voz v ON v.ID = ukz.IDVoza " +
                 "where rn.ID= " + id + " AND rn.Uvoz = 1 " +
                 "UNION " +
                 "SELECT rn.ID ," +
                 " rn.Uvoz,rn.Status,rn.AutoDan, rn.MestoPreuzimanjaKontejnera, u.Ref3 AS Ref, " +
                 " u.Nalogodavac3 AS Klijent, rn.MestoUtovara, rn.AdresaUtovara, rn.DatumUtovara, u.MestoIstovara AS MestoIstovara,u.KontaktOsobe as KontaktOsobaNaIstovaru, rn.DatumIstovara,(Rtrim(pko.PaKOOpomba)) AS AdresaIstovara,  " +
                 " rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz, " +
                 " rn.Trosak,rn.Valuta,0 AS BookingBrodara,   '' AS BrojPlombe,  u.BrodskaTeretnica,  " +
                 " u.BrutoKontejnera AS BTTKontejnetra, u.BrutoRobe AS BTTRobe, " +
                 " np.Naziv as NapomenaZaPozicioniranje,  rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, " +
                 " rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV,'' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski," +
                 " 0 as polaznaCarinarnica, u.OdredisnaCarina as OdredisnaCarina, 0 as polaznaSpedicija, u.OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt " +
                 "FROM  RadniNalogDrumski rn " +
                        "INNER JOIN  Uvoz u ON rn.KontejnerID = u.ID " +
                        "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = u.MestoIstovara AND pko.PaKOZapSt = u.AdresaMestaUtovara " + /*AND PaKOSifra = mu.Naziv*/
                        "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = u.NapomenaZaPozicioniranje " +
                        "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = u.PostupakSaRobom " +
                        "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = u.CarinskiPostupak " +
                        "LEFT JOIN Carinarnice c on c.ID = u.OdredisnaCarina " +
                        "LEFT JOIN Partnerji p2 on p2.PaSifra = u.OdredisnaSpedicija " +
                 "where rn.ID= " + id + " AND rn.Uvoz = 1" +
                 "UNION " +
                 "SELECT rn.ID ," +
                 " rn.Uvoz,rn.Status,rn.AutoDan, rn.MestoPreuzimanjaKontejnera, rn.Ref AS Ref," +
                 " rn.Klijent, rn.MestoUtovara, rn.AdresaUtovara, rn.DatumUtovara, rn.MestoIstovara AS MestoIstovara,rn.KontaktOsobaNaIstovaru AS KontaktOsobaNaIstovaru, rn.DatumIstovara,rn.AdresaIstovara AS AdresaIstovara,  " +
                 " rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz, " +
                 " rn.Trosak,rn.Valuta,rn.BookingBrodara, rn.BrodskaPlomba AS BrojPlombe,   rn.BrodskaTeretnica, " +
                 " rn.BrutoKontejnera AS BTTKontejnetra, rn.BrutoRobe AS BTTRobe,  " +
                 " rn.NapomenaZaPozicioniranje as NapomenaZaPozicioniranje,  rn.Cena,'' as CarinjenjeIzvozno, " +
                 " rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV,rn.BrojVoza as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski," +
                 " rn.PolaznaCarinarnica, rn.OdredisnaCarinarnica as OdredisnaCarina, rn.PolaznaSpedicija ,rn.OdredisnaSpedicija, rn.PolaznaSpedicijaKontakt, rn.OdredisnaSpedicijaKontakt " +
                 "FROM  RadniNalogDrumski rn " +
                 "where rn.ID= " + id + " AND rn.Uvoz in ( 2,3, 4, 5)", con);

                SqlDataReader dr = cmd.ExecuteReader();
                try
                {

                        while (dr.Read())
                    {
                        int? uvoz = null;
                        int? autoDan = null;
                        int? mestoIstovara = null;
                        int? mestoUtovara = null;
                        int? klijent = null;
                        decimal? bttoKontejnera = null;
                        decimal? bttoRobe = null;
                        string brojVoza = null;
                        string brojKontejnera = null;
                        string brodskaPlomba = null;
                        string brodskaTeretnica = null;
                        DateTime? datumIstovara = null;
                        DateTime? datumUtovara = null;
                        DateTime? dtPreuzimanjaPKontejnera = null;
                        int? bookingBrodara = null;
                        string adresaIstovara = null;
                        string adresaUtovara = null;
                        int? napomenaPoz = null;
                        decimal? trosak = null;
                        decimal? cena = null;
                        int? status = null;
                        int? pdv = null;
                        int? tipTransporta = null;
                        string referenca = null;
                        int? polaznaCarinarnica = null;
                        int? odredisnaCarinarnica = null;
                        int? polaznaSpedicija = null;
                        int? odredisnaSpedicija = null;
                        string polaznaSpedicijaKontakt = null;
                        string odredisnaSpedicijaKontakt = null;

                        int uvozConverted = Convert.ToInt32(dr["Uvoz"].ToString());
                        if (uvozConverted == 1 || uvozConverted == 0)
                            uvoz = -1;
                        //else if (uvozConverted == 0)
                        //    uvoz = 3;
                        else
                            uvoz = uvozConverted;

                        if (dr["AutoDan"] != DBNull.Value && int.TryParse(dr["AutoDan"].ToString(), out int parseAutoDan))
                           autoDan  = parseAutoDan;

                        int? mestoPreuzimanja = dr["MestoPreuzimanjaKontejnera"] == DBNull.Value  ? (int?)null : Convert.ToInt32(dr["MestoPreuzimanjaKontejnera"]);

                        if (dr["Klijent"] != DBNull.Value && int.TryParse(dr["Klijent"].ToString(), out int parsedKlijentID))
                            klijent = parsedKlijentID;

                        if (dr["MestoUtovara"] != DBNull.Value && int.TryParse(dr["MestoUtovara"].ToString(), out int parsedMestoUtovaraID))
                            mestoUtovara =  parsedMestoUtovaraID;

                        adresaUtovara = dr["AdresaUtovara"] == DBNull.Value ? null : dr["AdresaUtovara"].ToString();

                        if (dr["DatumUtovara"] != DBNull.Value)
                           datumUtovara = Convert.ToDateTime(dr["DatumUtovara"].ToString());

                        if (dr["MestoIstovara"] != DBNull.Value && int.TryParse(dr["MestoIstovara"].ToString(), out int parsedMestoIstovaraID))
                            mestoIstovara = parsedMestoIstovaraID;

                        if (dr["DatumIstovara"] != DBNull.Value)
                            datumIstovara = Convert.ToDateTime(dr["DatumIstovara"].ToString());

                        adresaIstovara = dr["AdresaIstovara"] == DBNull.Value ? null : dr["AdresaIstovara"].ToString();

                        if (dr["DtPreuzimanjaPraznogKontejnera"] != DBNull.Value)
                            dtPreuzimanjaPKontejnera = Convert.ToDateTime(dr["DtPreuzimanjaPraznogKontejnera"].ToString());
                    
                        string granicniPrelaz = dr["GranicniPrelaz"] == DBNull.Value ? null : dr["GranicniPrelaz"].ToString();

                        referenca = dr["Ref"].ToString();

                        if (dr["Trosak"] != DBNull.Value)
                            trosak = Convert.ToDecimal(dr["Trosak"].ToString());

                        string valuta = dr["Valuta"] == DBNull.Value ? null : dr["Valuta"].ToString();

                        if (dr["Status"] != DBNull.Value && int.TryParse(dr["Status"].ToString(), out int parsedStatusID))
                            status = parsedStatusID;

                       string opis = dr["DodatniOpis"] == DBNull.Value ? null : dr["DodatniOpis"].ToString();

                        if (dr["Cena"] != DBNull.Value)
                            cena = Convert.ToDecimal(dr["Cena"].ToString());
                    
                        string kontaktOsobaNaIstovaru = dr["KontaktOsobaNaIstovaru"] == DBNull.Value ? null : dr["KontaktOsobaNaIstovaru"].ToString();

                        if (dr["PDV"] != DBNull.Value && int.TryParse(dr["PDV"].ToString(), out int parsePDV))
                            pdv = parsePDV;

                        if (dr["TipTransportaDrumski"] != DBNull.Value && int.TryParse(dr["TipTransportaDrumski"].ToString(), out int parsedTipTransportaDrumskiID))
                            tipTransporta  = parsedTipTransportaDrumskiID;

                        brojVoza = dr["NAzivVoza"] == DBNull.Value ? null : dr["NAzivVoza"].ToString();

                        if (dr["BTTKontejnetra"] != DBNull.Value)
                            bttoKontejnera = Convert.ToDecimal(dr["BTTKontejnetra"].ToString());
                        if (dr["BTTRobe"] != DBNull.Value)
                            bttoRobe = Convert.ToDecimal(dr["BTTRobe"].ToString());

                        if (dr["BookingBrodara"] != DBNull.Value && int.TryParse(dr["BookingBrodara"].ToString(), out int parsedBookingBrodara))
                            bookingBrodara = parsedBookingBrodara;

                        brodskaTeretnica = dr["BrodskaTeretnica"] == DBNull.Value ? null : dr["BrodskaTeretnica"].ToString();
                        brodskaPlomba = dr["BrojPlombe"] == DBNull.Value ? null : dr["BrojPlombe"].ToString();

                        if (dr["NapomenaZaPozicioniranje"] != DBNull.Value &&
                            int.TryParse(dr["NapomenaZaPozicioniranje"].ToString(), out int parsedValue))
                        {
                            napomenaPoz = parsedValue;
                        }
                        odredisnaSpedicijaKontakt = dr["OdredisnaSpedicijaKontakt"].ToString();
                        polaznaSpedicijaKontakt = dr["PolaznaSpedicijaKontakt"].ToString();

                        if (dr["OdredisnaSpedicija"] != DBNull.Value && int.TryParse(dr["OdredisnaSpedicija"].ToString(), out int parsedOdredisnaSpedicija))
                            odredisnaSpedicija= parsedOdredisnaSpedicija;
                      

                        if (dr["PolaznaSpedicija"] != DBNull.Value && int.TryParse(dr["PolaznaSpedicija"].ToString(), out int parsedPolaznaSpedicija))
                            polaznaSpedicija = parsedPolaznaSpedicija;
                        
                        if (dr["PolaznaCarinarnica"] != DBNull.Value && int.TryParse(dr["PolaznaCarinarnica"].ToString(), out int parsedPolaznaCarinarnica))
                        {
                            polaznaCarinarnica = parsedPolaznaCarinarnica;
                        }
                        if (dr["OdredisnaCarina"] != DBNull.Value && int.TryParse(dr["OdredisnaCarina"].ToString(), out int parsedOdredisnaCarina))
                        {
                            odredisnaCarinarnica = parsedOdredisnaCarina;
                        }
                        isu.DuplirajRadniNalogDrumski(duplikatNalogID, uvoz, autoDan, mestoPreuzimanja, klijent, mestoUtovara, adresaUtovara, datumUtovara, mestoIstovara, datumIstovara, adresaIstovara, dtPreuzimanjaPKontejnera, granicniPrelaz,
                            trosak, valuta, status, opis, cena, kontaktOsobaNaIstovaru, pdv, tipTransporta, brojVoza, bttoKontejnera, bttoRobe, bookingBrodara, brodskaTeretnica, brodskaPlomba, napomenaPoz, referenca,
                            polaznaCarinarnica, odredisnaCarinarnica, polaznaSpedicija, odredisnaSpedicija, polaznaSpedicijaKontakt, odredisnaSpedicijaKontakt );
                    }         
                }

                catch (Exception ex)
                {
                    sveUspešno = false;
                    MessageBox.Show("Greška tokom obrade: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break; // prekini dalje dupliranje
                }
            }
            RefreshGrid();
        }
    }
}
