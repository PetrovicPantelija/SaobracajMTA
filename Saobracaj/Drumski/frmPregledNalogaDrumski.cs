using Saobracaj.Uvoz;
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
        public frmPregledNalogaDrumski()
        {
            InitializeComponent();
            ChangeTextBox();
            RefreshGrid();
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
    
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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

                // 2. Priprema statusa za upit
                string statusiZaUpit = string.Join(",", statusi
                    .Select(s => s.Trim())
                    .Where(s => int.TryParse(s, out _)));
                //samo oni koji imaju raspored voya
                var select = $@"
                            SELECT rn.ID,
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
                                    pa.PaNaziv as Nalogodavac,
                                    rn.KontejnerID, 'KONACAN' as Trenutno
                            FROM RadniNalogDrumski rn
                            LEFT JOIN Automobili a ON rn.KamionID = a.ID
                            LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                            INNER JOIN IzvozKonacna ik ON ik.ID = rn.KontejnerID
                            LEFT JOIN Partnerji pa ON pa.PaSifra = ik.Klijent3
                            LEFT JOIN TipKontenjera tk ON ik.VrstaKontejnera = tk.ID
                            WHERE rn.Uvoz = 0  AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit}))

                            union all
                                    SELECT rn.ID,
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
                                    pa.PaNaziv as Nalogodavac,
                            rn.KontejnerID, 'NEODREDJEN' as Trenutno
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN Izvoz i ON i.ID = rn.KontejnerID
                        LEFT JOIN Partnerji pa ON pa.PaSifra = i.Klijent3
                        LEFT JOIN TipKontenjera tk ON i.VrstaKontejnera = tk.ID
                        WHERE rn.Uvoz = 0 AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit}))

                        union all
                        SELECT rn.ID, 
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
                               pa.PaNaziv as Nalogodavac,
                               rn.KontejnerID, 'KONACAN' as Trenutno
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN UvozKonacna uk ON uk.ID = rn.KontejnerID
                        LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3
                        LEFT JOIN TipKontenjera tk ON uk.TipKontejnera = tk.ID
                        WHERE rn.Uvoz = 1 AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit}))

                        union all
                               SELECT rn.ID, 
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
                               pa.PaNaziv as Nalogodavac,
                               rn.KontejnerID, 'NEODREDJEN' as Trenutno
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN Uvoz uk ON uk.ID = rn.KontejnerID
                        LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3
                        LEFT JOIN TipKontenjera tk ON uk.TipKontejnera = tk.ID
                        WHERE rn.Uvoz = 1 AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit}))

                        union all
                               SELECT rn.ID, 
                               rn.BrojKontejnera as BrojKontejnera,
                               '' AS TipKontejnera,
                               rn.NalogID,
                               CONVERT(varchar, rn.DatumKreiranjaNaloga, 104) AS KreiranjeNaloga,
                               rn.KamionID,
                               a.RegBr,
                               a.Vozac,
                               rn.Status,   
                               rn.Status AS StatusID, 
                               CONVERT(varchar, rn.DatumPromeneStatusa, 104) AS PromenaStatusa,
                               pa.PaNaziv as Nalogodavac,
                               rn.KontejnerID, 'NEODREDJEN' as Trenutno
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        LEFT JOIN Partnerji pa ON pa.PaSifra = rn.Klijent
                        WHERE rn.Uvoz in (-1,2, 3) AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit}))
                        ORDER BY ID DESC";

                dataAdapter = new SqlDataAdapter(select, connection);
                var commandBuilder = new SqlCommandBuilder(dataAdapter);

                var ds = new DataSet();
                dataAdapter.Fill(ds);
                mainTable = ds.Tables[0];

                // Napuni Status combo listu
                var stv = "SELECT ID, Naziv FROM StatusVozila ORDER BY Naziv";
                var stvAD = new SqlDataAdapter(stv, s_connection);
                var stvDS = new DataSet();
                stvAD.Fill(stvDS);
                var dtStatus = stvDS.Tables[0];

                // Osiguraj da StatusID kolona u glavnoj tabeli ima vrednosti ili je DBNull
                foreach (DataRow row in mainTable.Rows)
                {
                    if (row.IsNull("Status"))
                        row["Status"] = DBNull.Value;
                }

                // Veži za grid
                gridGroupingControl1.DataSource = mainTable;

                var statusCol = gridGroupingControl1.TableDescriptor.Columns["Status"];
                statusCol.Appearance.AnyRecordFieldCell.CellType = "ComboBox";
                statusCol.Appearance.AnyRecordFieldCell.DataSource = dtStatus;
                statusCol.Appearance.AnyRecordFieldCell.DisplayMember = "Naziv";
                statusCol.Appearance.AnyRecordFieldCell.ValueMember = "ID";
                statusCol.Appearance.AnyRecordFieldCell.ExclusiveChoiceList = true;
                statusCol.Appearance.AnyRecordFieldCell.DropDownStyle = GridDropDownStyle.AutoComplete;

                // Ukloni kolone koje ne želiš da se vide
                var colsToRemove = new[] { "KontejnerID", "StatusID" }; // "Status" je Naziv
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
            else
            {
                frmDrumski pnd = new frmDrumski();
                pnd.FormClosed += pnd_FormClosed;
                pnd.Show();
            }
        }

        private void pnd_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshGrid(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmPakovanjeKamiona kam = new frmPakovanjeKamiona();
            kam.Show();
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

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            int noviStatusId;
            if (cboStatus.SelectedValue != null && int.TryParse(cboStatus.SelectedValue.ToString(), out noviStatusId))
            {
                foreach (SelectedRecord record in gridGroupingControl1.Table.SelectedRecords)
                {
                    Record rec = record.Record;

                    var id = rec.GetValue("ID");
                    InsertRadniNalogDrumski upd = new InsertRadniNalogDrumski();
                    upd.UpdateStatusRadniNalogDrumski(Convert.ToInt32(id), noviStatusId);
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
                 " rn.Uvoz, rn.Status, rn.AutoDan,  rn.MestoPreuzimanjaKontejnera, " +
                 "ik.Klijent3 AS Klijent, ik.MesoUtovara AS MestoUtovara,  (Rtrim(pko.PaKOOpomba)) as AdresaUtovara, rn.MestoIstovara AS MestoIstovara, rn.KontaktOsobaNaIstovaru,  rn.DatumIstovara, rn.AdresaIstovara,  " +
                 "rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz,  " +
                 "rn.Trosak, rn.Valuta, ik.BookingBrodara,   ik.BrodskaPlomba AS BrojPlombe,  '' AS BrodskaTeretnica,  " +
                 " ik.VGMBrod AS BTTKontejnetra, ik.BrutoRobe AS BTTRobe, " +
                 "ik.NapomenaZaRobu as NapomenaZaPozicioniranje , rn.Cena, cc.Naziv AS CarinjenjeIzvozno," +
                 "  '' AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
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
                 " rn.Uvoz, rn.Status, rn.AutoDan,  rn.MestoPreuzimanjaKontejnera, " +
                 "i.Klijent3 AS Klijent,  i.MesoUtovara AS MestoUtovara, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,rn.MestoIstovara AS MestoIstovara, rn.KontaktOsobaNaIstovaru, rn.DatumIstovara, rn.AdresaIstovara, " +
                 "rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz, " +
                 "rn.Trosak, rn.Valuta, i.BookingBrodara,  i.BrodskaPlomba AS BrojPlombe, '' AS BrodskaTeretnica,  " +
                 " i.VGMBrod AS BTTKontejnetra,  i.BrutoRobe AS BTTRobe, " +
                 "i.NapomenaZaRobu AS NapomenaZaPozicioniranje,  rn.Cena, cc.Naziv AS CarinjenjeIzvozno," +
                 " '' AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, '' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
                 "FROM    RadniNalogDrumski rn " +
                          "INNER JOIN  Izvoz i ON rn.KontejnerID = i.ID  " +
                           "LEFT JOIN partnerjiKontOsebaMU pko ON  pko.PaKOSifra = i.MesoUtovara AND pko.PaKOZapSt = i.KontaktOsoba " +
                          "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = i.NapomenaReexport " +
                          "LEFT JOIN Carinarnice cc on cc.ID = i.MestoCarinjenja " +
                 "where rn.ID=" + id + " AND rn.Uvoz = 0 " +
                 "UNION " +
                 "SELECT rn.ID ," +
                 "rn.Uvoz,rn.Status, rn.AutoDan, rn.MestoPreuzimanjaKontejnera, " +
                 "uk.Nalogodavac3 AS Klijent,  rn.MestoUtovara, rn.AdresaUtovara,uk.MestoIstovara AS MestoIstovara,uk.KontaktOsobe as KontaktOsobaNaIstovaru, rn.DatumIstovara, (Rtrim(pko.PaKOOpomba)) AS AdresaIstovara, " +
                 "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz, " +
                 "rn.Trosak,rn.Valuta,0 AS BookingBrodara,  '' AS BrojPlombe,  uk.BrodskaTeretnica,  " +
                 " uk.BrutoKontejnera AS BTTKontejnetra, uk.BrutoRobe AS BTTRobe," +
                 " np.Naziv as NapomenaZaPozicioniranje, rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno,  " +
                 "  rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta AS TipTransportaDrumski " +
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
                 "rn.Uvoz,rn.Status,rn.AutoDan, rn.MestoPreuzimanjaKontejnera, " +
                 "u.Nalogodavac3 AS Klijent, rn.MestoUtovara, rn.AdresaUtovara,u.MestoIstovara AS MestoIstovara,u.KontaktOsobe as KontaktOsobaNaIstovaru, rn.DatumIstovara,(Rtrim(pko.PaKOOpomba)) AS AdresaIstovara,  " +
                 "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz, " +
                 "rn.Trosak,rn.Valuta,0 AS BookingBrodara,   '' AS BrojPlombe,  u.BrodskaTeretnica,   " +
                 "u.BrutoKontejnera AS BTTKontejnetra, u.BrutoRobe AS BTTRobe, " +
                 "np.Naziv as NapomenaZaPozicioniranje,  rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, " +
                 "rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV,'' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
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
                 "rn.Uvoz,rn.Status,rn.AutoDan, rn.MestoPreuzimanjaKontejnera, " +
                 "rn.Klijent, rn.MestoUtovara, rn.AdresaUtovara,rn.MestoIstovara AS MestoIstovara,rn.KontaktOsobaNaIstovaru AS KontaktOsobaNaIstovaru, rn.DatumIstovara,rn.AdresaIstovara AS AdresaIstovara,  " +
                 "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz, " +
                 "rn.Trosak,rn.Valuta,rn.BookingBrodara, rn.BrodskaPlomba AS BrojPlombe,   rn.BrodskaTeretnica,  " +
                 " rn.BrutoKontejnera AS BTTKontejnetra, rn.BrutoRobe AS BTTRobe,  " +
                 "rn.NapomenaZaPozicioniranje as NapomenaZaPozicioniranje,  rn.Cena,'' as CarinjenjeIzvozno, " +
                 "rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV,rn.BrojVoza as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
                 "FROM  RadniNalogDrumski rn " +
                 "where rn.ID= " + id + " AND rn.Uvoz in (-1,2,3)", con);

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
                        string napomenaPoz = null;
                        decimal? trosak = null;
                        decimal? cena = null;
                        int? status = null;
                        int? pdv = null;
                        int? tipTransporta = null;

                        int uvozConverted = Convert.ToInt32(dr["Uvoz"].ToString());
                        if (uvozConverted == 1 || uvozConverted == 0)
                            uvoz = -1;
                        //else if (uvozConverted == 0)
                        //    uvoz = 3;
                        else
                            uvoz = uvozConverted;

                        if (dr["AutoDan"] != DBNull.Value && int.TryParse(dr["AutoDan"].ToString(), out int parseAutoDan))
                           autoDan  = parseAutoDan;
 
                        string msetoPreuzimanja = dr["MestoPreuzimanjaKontejnera"] == DBNull.Value ? null : dr["MestoPreuzimanjaKontejnera"].ToString();

                        if (dr["Klijent"] != DBNull.Value && int.TryParse(dr["Klijent"].ToString(), out int parsedKlijentID))
                            klijent = parsedKlijentID;

                        if (dr["MestoUtovara"] != DBNull.Value && int.TryParse(dr["MestoUtovara"].ToString(), out int parsedMestoUtovaraID))
                            mestoUtovara =  parsedMestoUtovaraID;

                        adresaUtovara = dr["AdresaUtovara"] == DBNull.Value ? null : dr["AdresaUtovara"].ToString();

                        if (dr["MestoIstovara"] != DBNull.Value && int.TryParse(dr["MestoIstovara"].ToString(), out int parsedMestoIstovaraID))
                            mestoIstovara = parsedMestoIstovaraID;

                        if (dr["DatumIstovara"] != DBNull.Value)
                            datumIstovara = Convert.ToDateTime(dr["DatumIstovara"].ToString());

                        adresaIstovara = dr["AdresaIstovara"] == DBNull.Value ? null : dr["AdresaIstovara"].ToString();

                        if (dr["DtPreuzimanjaPraznogKontejnera"] != DBNull.Value)
                            dtPreuzimanjaPKontejnera = Convert.ToDateTime(dr["DtPreuzimanjaPraznogKontejnera"].ToString());
                    
                        string granicniPrelaz = dr["GranicniPrelaz"] == DBNull.Value ? null : dr["GranicniPrelaz"].ToString();

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
                        napomenaPoz = dr["NapomenaZaPozicioniranje"] == DBNull.Value ? null : dr["NapomenaZaPozicioniranje"].ToString();

                        isu.DuplirajRadniNalogDrumski( uvoz, autoDan, msetoPreuzimanja, klijent, mestoUtovara, adresaUtovara, mestoIstovara, datumIstovara, adresaIstovara, dtPreuzimanjaPKontejnera, granicniPrelaz,
                            trosak, valuta, status, opis, cena, kontaktOsobaNaIstovaru, pdv, tipTransporta, brojVoza, bttoKontejnera, bttoRobe, bookingBrodara, brodskaTeretnica, brodskaPlomba, napomenaPoz);
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
