using Saobracaj.Uvoz;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
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
            //samo oni koji imaju raspored voya
            var select = @"
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
                               rn.Klijent as Nalogodavac,
                               rn.KontejnerID, 'KONACAN' as Trenutno
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN UvozKonacna uk ON uk.ID = rn.KontejnerID
                        LEFT JOIN TipKontenjera tk ON uk.TipKontejnera = tk.ID
                       
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
                               rn.Klijent as Nalogodavac,
                               rn.KontejnerID, 'NEODREDJEN' as Trenutno
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN Uvoz uk ON uk.ID = rn.KontejnerID
                        LEFT JOIN TipKontenjera tk ON uk.TipKontejnera = tk.ID
                        ORDER BY ID DESC";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            var connection = new SqlConnection(s_connection);
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
            var colsToRemove = new[] {  "KontejnerID", "StatusID" }; // "Status" je Naziv
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
            // Pretplata na promene
            gridGroupingControl1.TableControlCurrentCellAcceptedChanges -= GridGroupingControl1_TableControlCurrentCellAcceptedChanges;
            gridGroupingControl1.TableControlCurrentCellAcceptedChanges += GridGroupingControl1_TableControlCurrentCellAcceptedChanges;
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
                    pnd.Show();
                }
            }
            else
            {
                frmDrumski pnd = new frmDrumski();
                pnd.Show();
            }
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
    }
}
