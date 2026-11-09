using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saobracaj.Drumski;
using Syncfusion.Grouping;
using Syncfusion.GridHelperClasses;

namespace Saobracaj.Kapija
{
    public partial class frmPregledKamiona: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private SqlDataAdapter dataAdapter;
        private DataTable mainTable;

        public frmPregledKamiona()
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
            var select = "";

            select = $@"
                            SELECT  k.ID,
                                    CONVERT(varchar,k.DatumDolaska,104) AS DatumDolaska,
                                    st.Naziv AS Status, 
                                    Vozac ,
                                    RegistarskiBroj,
                                    Kontakt,
                                    RazlogDolaska,
                                    CONVERT(varchar,k.DatumZakazanogDolaska,104) AS DatumZakazanogDolaska,
                                    KontaktUnutarFirme,
                                    CONVERT(varchar,k.DatumOdlaska,104) AS DatumOdlaska
                            FROM Kapija k
                                 LEFT JOIN StatusKapija st ON st.ID = k.StatusID
                           ";


                SqlConnection myConnection = new SqlConnection(connection);
                var c = new SqlConnection(connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new System.Data.DataSet();
                dataAdapter.Fill(ds);
                // dataGridView1.ReadOnly = true;
                gridGroupingControl1.DataSource = ds.Tables[0];
                gridGroupingControl1.ShowGroupDropArea = true;
                this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
                foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
                {
                    column.AllowFilter = true;
                }

                GridDynamicFilter dynamicFilter = new GridDynamicFilter();

                //Wiring the Dynamic Filter to GridGroupingControl
                dynamicFilter.WireGrid(this.gridGroupingControl1);
            }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                // Uzimamo prvi selektovani red
                Record rec = gridGroupingControl1.Table.SelectedRecords[0].Record;

                if (rec != null)
                {
                    int ID = Convert.ToInt32(rec.GetValue("ID"));
                    frmKamionDetalji pnd = new frmKamionDetalji(ID);
                    pnd.FormClosed += pnd_FormClosed;
                    pnd.Show();
                }
            }
            else
            {
                frmKamionDetalji pnd = new frmKamionDetalji();
                pnd.FormClosed += pnd_FormClosed;
                pnd.Show();
            }
        }
        private void pnd_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            panelStatus.Visible = true;
            var stv = "select ID, Naziv from StatusKapija order by Naziv";
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


        private void btnSnimi_Click(object sender, EventArgs e)
        {
            int noviStatusId;
            if (cboStatus.SelectedValue != null && int.TryParse(cboStatus.SelectedValue.ToString(), out noviStatusId))
            {
                foreach (SelectedRecord record in gridGroupingControl1.Table.SelectedRecords)
                {
                    Record rec = record.Record;

                    var id = rec.GetValue("ID");
                    InsertKapija upd = new InsertKapija();
                    upd.UpdateStatusKapija(Convert.ToInt32(id), noviStatusId);
                }
            }
            panelStatus.Visible = false;
            RefreshGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panelStatus.Visible = false;
        }
    }
}
