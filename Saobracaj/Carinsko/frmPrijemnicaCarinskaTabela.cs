using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Carinko
{
    public partial class frmPrijemnicaCarinskaTabela : Form
    {
        public frmPrijemnicaCarinskaTabela()
        {
            InitializeComponent();
            ChangeTextBox();
        }


        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
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

        private void RefreshDataGrid()
        {
            var select = " SELECT     PrijemnicaCarinska.ID, PrijemnicaCarinska.Status, Partnerji.PaNaziv AS Vlasnik, PrijemnicaCarinska.ID AS Expr1, PrijemnicaCarinska.Status, PrijemnicaCarinska.Datum," +
                " PrijemnicaCarinska.Korisanik, PrijemnicaCarinska.SkladisteID, PrijemnicaCarinska.Dokument, PrijemnicaCarinska.MBR, " +
                "  PrijemnicaCarinska.VrstaSkladista, PrijemnicaCarinska.Vlasnik AS VlasnikRobe, PrijemnicaCarinska.Korisinik, PrijemnicaCarinska.Posiljalac,  " +
                "  PrijemnicaCarinska.Sektor, Partnerji_1.PaNaziv AS KorisnikRobe, Partnerji_2.PaNaziv AS Primalac,  " +
                "    Skladista.Naziv AS Skladiste " +
                " FROM        PrijemnicaCarinska INNER JOIN " +
                "  Skladista ON PrijemnicaCarinska.SkladisteID = Skladista.ID INNER JOIN " +
                "  Partnerji ON PrijemnicaCarinska.Vlasnik = Partnerji.PaSifra INNER JOIN " +
                " Partnerji AS Partnerji_1 ON PrijemnicaCarinska.Korisinik = Partnerji_1.PaSifra INNER JOIN " +
                "  Partnerji AS Partnerji_2 ON PrijemnicaCarinska.Posiljalac = Partnerji_2.PaSifra ";


            // var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            //  SqlConnection myConnection = new SqlConnection(s_connection);
            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
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

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);


        }

        private void button23_Click(object sender, EventArgs e)
        {
            frmPrijemnicaCarinsko car = new frmPrijemnicaCarinsko();
            car.FormClosed += car_FormClosed;
            car.Show();
        }

        private void car_FormClosed(object sender, FormClosedEventArgs e)
        {
            RefreshDataGrid();
        }


        private void button25_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            frmPrijemnicaCarinsko pc = new frmPrijemnicaCarinsko(txtSifra.Text);
            pc.FormClosed += car_FormClosed;
            pc.Show();
        }

        private void frmPrijemnicaCarinskaTabela_Load(object sender, EventArgs e)
        {

        }
    }
}
