using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Testiranje.Dokumeta
{
    public partial class frmCIRPregled : Form
    {
        public frmCIRPregled()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            ChangeTextBox();
        }

        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;



                foreach (Control control in this.Controls)
                {

                }


                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

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

        private void frmCIRPregled_Load(object sender, EventArgs e)
        {
            RefreshGV();

        }

        private void RefreshGV()
        {
            var select = "    SELECT top 4000 [ID] ,[BrKontejnera] ,[Datum] as DatumIzrade, CAST(CASE Interni WHEN 1 THEN 1 ELSE 0 END AS BIT) as Interni " +
            " ,CAST(CASE Prijem WHEN 1 THEN 1 ELSE 0 END AS BIT) as IzPrijema,[Dokument] " +
            " ,[TiKontejnera] " +
            " ,[Pun],[Tezina],[Plomba1] " +
            " ,[Plomba2],[DatumIn],[Vagon],[TruckNo] " +
            " ,CAST(CASE [Damaged] WHEN 1 THEN 1 ELSE 0 END AS BIT) as Ostecen,CAST(CASE ISpravan WHEN 1 THEN 1 ELSE 0 END AS BIT) as Ispravan,[Prevoz],[Containerresponsible] " +
            " ,[primedbe],[Received],[Inspected],[Delivery] " +
            " ,[Size] " +
            " ,[Duzina],[Sirina],[Visina],[sPlomba] " +
            " ,[sPlomba2], Nosivost , [Korisnik]  FROM [dbo].[CIR] order by ID desc";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
        }

        private void RefreshDataGrid()
        {
            var select = "  SELECT top 4000 [ID] ,[BrKontejnera] ,[Datum] as DatumIzrade, CAST(CASE Interni WHEN 1 THEN 1 ELSE 0 END AS BIT) " +
" ,[Prijem],[Dokument] " +
" ,[TiKontejnera],[MaterijalCelik],[MaterijalAlumini],[incoming] " +
" ,[Pun],[Tezina],[Plomba1] " +
" ,[Plomba2],[DatumIn],[Vagon],[TruckNo] " +
" ,[Damaged],[Ispravan],[Prevoz],[Containerresponsible] " +
" ,[primedbe],[Received],[Inspected],[Delivery] " +
" ,[Korisnik],[Size] " +
" ,[Duzina],[Sirina],[Visina],[sPlomba] " +
" ,[sPlomba2]  FROM[dbo].[CIR] order by ID desc";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];





        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    textBox1.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            RefreshGV();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Niste izabrali stavku za koju otvarate CIR");
                return;
            }
            Saobracaj.Dokumenta.frmCIR cir = new Saobracaj.Dokumenta.frmCIR(Convert.ToInt32(textBox1.Text));
            cir.Show();
        }
    }
}
