using Microsoft.ReportingServices.Diagnostics.Internal;
using Syncfusion.Windows.Forms;
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
using Testiranje.Sifarnici;

namespace Saobracaj.TrackModal.Sifarnici
{
    public partial class SkladisteGrupa : Form
    {
        public static string code = "frmZone";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string niz = "";

        string KorisnikCene;
        bool status = false;
        public SkladisteGrupa()
        {
            InitializeComponent();
            ChangeTextBox();
            
        }

        private void ChangeTextBox()
        {
            panelHeader.Visible = false;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
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
                meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            txtSifra.Text = "";
            txtOznaka.Text = "";
            txtNaziv.Text = "";
            txtAdresa.Text = "";
            txtGrad.Text = "";
            txtDrzava.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            int tmpAktivna = 0;

            if (txtSifra.Text == "")
            {
                status = true;
              
            }


            if (chkAktivna.Checked == true)
            { tmpAktivna = 1; };
            if (status == true)
            {
                InsertSkladistaGrupa ins = new InsertSkladistaGrupa();
                ins.InsLokacija(txtNaziv.Text,txtOznaka.Text,  Convert.ToInt32(cboZonaID.SelectedValue), txtDrzava.Text, txtGrad.Text, txtAdresa.Text, tmpAktivna, Convert.ToInt32(cboTipSkladista.SelectedValue));
                status = false;
            }
            else
            {
                if (txtSifra.Text == "")
                { return; }
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                InsertSkladistaGrupa upd = new InsertSkladistaGrupa();
                upd.UpdLokacija(Convert.ToInt32(txtSifra.Text), txtNaziv.Text, txtOznaka.Text, Convert.ToInt32(cboZonaID.SelectedValue), txtDrzava.Text, txtGrad.Text, txtAdresa.Text, tmpAktivna, Convert.ToInt32(cboTipSkladista.SelectedValue));
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete, brišu se i poljai pozicije?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertSkladistaGrupa upd = new InsertSkladistaGrupa();
                upd.DeleteLokacija(Convert.ToInt32(txtSifra.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT [SkladistaGrupa].[ID]      ,[SkladistaGrupa].[Oznaka]  as Oznaka     , [SkladistaGrupa].[Naziv]  as Naziv     ,[SkladistaGrupa].[ZonaID] " +
"  	  , Zone.Oznaka as Zona_Oznaka, Zone.NAziv as Zona_Naziv     ,[SkladistaGrupa].[Drzava]      ,[SkladistaGrupa].[Grad]      ,[SkladistaGrupa].[Adresa] " +
"      ,CASE WHEN SkladistaGrupa.Aktivna > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Aktivna, TipSkladista.Naziv as TipSkladista  FROM [dbo].[SkladistaGrupa] " +
"       inner join Zone on Zone.ID = [SkladistaGrupa].[ZonaID]" +
" inner join TipSkladista on SkladistaGrupa.TipSkladistaID = TipSkladista.ID  ";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(dataGridView1);
            }
            else
            {
                dataGridView1.BorderStyle = BorderStyle.None;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView1.BackgroundColor = Color.White;

                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            
            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Oznaka";
            dataGridView1.Columns[1].Width = 80;


            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Naziv";
            dataGridView1.Columns[2].Width = 200;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Zona ID";
            dataGridView1.Columns[3].Width = 40;
            dataGridView1.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Zona Oznaka";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Zona Naziv";
            dataGridView1.Columns[5].Width = 280;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Drzava";
            dataGridView1.Columns[6].Width = 150;


            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Grad";
            dataGridView1.Columns[7].Width = 150;

            DataGridViewColumn column9= dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Adresa";
            dataGridView1.Columns[8].Width = 250;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Aktivna";
            dataGridView1.Columns[9].Width = 80;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Tip skladista";
            dataGridView1.Columns[10].Width = 180;



        }

        private void VratiPodatke(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [SkladistaGrupa].[ID]      ,[SkladistaGrupa].[Naziv]  as Naziv     ,[SkladistaGrupa].[Oznaka]  as Oznaka     ,[SkladistaGrupa].[ZonaID]   ,[SkladistaGrupa].[Drzava]      ,[SkladistaGrupa].[Grad]      ,[SkladistaGrupa].[Adresa]      ,[SkladistaGrupa].[Aktivna], SkladistaGrupa.TipSkladistaID  FROM [dbo].[SkladistaGrupa] where ID=" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                // Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene

                txtNaziv.Text = dr["Naziv"].ToString();
                txtOznaka.Text = dr["Oznaka"].ToString();
                cboZonaID.SelectedValue = Convert.ToInt32(dr["ZonaID"].ToString());
                txtDrzava.Text = dr["Drzava"].ToString();
                txtGrad.Text = dr["Grad"].ToString();
                txtAdresa.Text = dr["Adresa"].ToString();
                if (dr["Aktivna"].ToString() == "1")
                {
                    chkAktivna.Checked = true;
                }
                else
                {
                    chkAktivna.Checked = false;
                }
                cboTipSkladista.SelectedValue = Convert.ToInt32(dr["TipSkladistaID"].ToString());
            }

            con.Close();
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var dir = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from Zone order by ID";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new System.Data.DataSet();
            dirAD.Fill(dirDS);
            cboZonaID.DataSource = dirDS.Tables[0];
            cboZonaID.DisplayMember = "Naziv";
            cboZonaID.ValueMember = "ID";


            var dir2 = "Select ID,  Naziv from TipSkladista order by ID";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new System.Data.DataSet();
            dirAD2.Fill(dirDS2);
            cboTipSkladista.DataSource = dirDS2.Tables[0];
            cboTipSkladista.DisplayMember = "Naziv";
            cboTipSkladista.ValueMember = "ID";

        }

        private void SkladisteGrupa_Load(object sender, EventArgs e)
        {
            FillCombo();
            
            RefreshDataGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txtSifra.Text);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                InsertSkladistaGrupa poz = new InsertSkladistaGrupa();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {

                        poz.ProglasiAktivnim(Convert.ToInt32(row.Cells[0].Value.ToString()), 0);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                InsertSkladistaGrupa poz = new InsertSkladistaGrupa();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {

                        poz.ProglasiNeAktivnim(Convert.ToInt32(row.Cells[0].Value.ToString()), 0);
                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
