using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            RefreshGrid();
        }

        private void RefreshGrid()
        {
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
                               rn.KontejnerID
                        FROM RadniNalogDrumski rn
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        LEFT JOIN UvozKonacna uk ON uk.ID = rn.KontejnerID
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
