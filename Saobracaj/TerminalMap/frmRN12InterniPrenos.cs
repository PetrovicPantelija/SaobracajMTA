using Saobracaj.RadniNalozi;
using Saobracaj.TrackModal.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.TerminalMap
{
    public partial class frmRN12InterniPrenos : Form
    {
        string hangar = "";
        string user = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        int VrstaKontejnera = 0;
        int Brodar = 0;
        int PoljeSA= 0;
        int PozicijaSa = 0;
        int Kvalitet = 0;
        public frmRN12InterniPrenos()
        {
            InitializeComponent();
        }


        public frmRN12InterniPrenos(string polje)
        {
            InitializeComponent();
            ChangeTextBox();
            hangar = polje;
        }

        private void ChangeTextBox()
        {
           

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
              
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

    

        private void RefreshDataGrid()
        {
            var select = "";
         
                select = "Select Kontejner, KontejnerStatus.Naziv As StatusKontejnera , Zone.Oznaka as Zona, SkladistaGrupa.Oznaka as Lokacija, " +
                    "Skladista.SkNaziv as Polje,   TipKontenjera.SkNaziv as Vrsta,  Pozicija.Opis as Pozicija,  PArtnerji.PaNaziv as Brodar, " +
                    "    KontejnerTekuce.Pokret as Pokret,    uvKvalitetKontejnera.Naziv as Kvalitet   from KontejnerTekuce   " +
                    "inner join Skladista on KontejnerTekuce.Skladiste = Skladista.ID  " +
                    "  inner join Pozicija on Pozicija.Id = KontejnerTekuce.Pozicija  " +
                    " inner join TipKontenjera on TipKontenjera.ID = KontejnerTekuce.TipKontejnera  " +
                    "  inner join KontejnerStatus on KontejnerStatus.ID = StatusKontejnera " +
                    " inner join PArtnerji on KontejnerTekuce.Brodar = Partnerji.PaSifra  " +
                    " inner join PArtnerji p1 on KontejnerTekuce.Uvoznik = p1.PaSifra  " +
                    " inner join uvKvalitetKontejnera on KontejnerTekuce.Kvalitet = uvKvalitetKontejnera.ID " +
                    " inner join SkladistaGrupa on Skladista.GrupaPoljaID = SkladistaGrupa.ID  inner join Zone on Zone.ID = SkladistaGrupa.ZonaID " +
                    " where Skladista.SkNaziv = '" + hangar + "'";

             

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);
            /*
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 80;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Lok naz";
            dataGridView1.Columns[1].Width = 100;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Lozinka";
            dataGridView1.Columns[2].Width = 150;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Aktivna";
            dataGridView1.Columns[3].Width = 60;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Dizel";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Masa";
            dataGridView1.Columns[5].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Serija ID";
            dataGridView1.Columns[6].Width = 80;
            */

        }

        private void frmRN12InterniPrenos_Load(object sender, EventArgs e)
        {
            var select2 = " select ID, SkNaziv from Skladista order by SkNaziv";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboNaSklad.DataSource = ds2.Tables[0];
            cboNaSklad.DisplayMember = "SkNaziv";
            cboNaSklad.ValueMember = "ID";

            RefreshDataGrid();
        }

        private void VratiPodatke(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select Kontejner,  Skladista.ID as Polje,   TipKontenjera.ID as Vrsta, " +
   " Pozicija.ID as Pozicija, PArtnerji.PaSifra as Brodar, KontejnerTekuce.Kvalitet " +
  " from KontejnerTekuce inner join Skladista on KontejnerTekuce.Skladiste = Skladista.ID " +
" inner    join Pozicija on Pozicija.Id = KontejnerTekuce.Pozicija   inner    join TipKontenjera on TipKontenjera.ID = KontejnerTekuce.TipKontejnera " +
 " inner   join PArtnerji on KontejnerTekuce.Brodar = Partnerji.PaSifra " +
 " where Kontejner  = '" + ID + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                VrstaKontejnera = Convert.ToInt32(dr["Vrsta"].ToString()); 
                 Brodar = Convert.ToInt32(dr["Brodar"].ToString()); ;
                PoljeSA = Convert.ToInt32(dr["Polje"].ToString()); ;
                PozicijaSa = Convert.ToInt32(dr["Pozicija"].ToString()); ;
                Kvalitet = Convert.ToInt32(dr["Kvalitet"].ToString()); ;

            }
            con.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                RadniNalozi.InsertRN ir = new InsertRN();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        VratiPodatke(row.Cells[0].Value.ToString());
                        int PotrebanCIR = 0;
                      
                       
                        ir.InsRN12Medjuskladisni(Convert.ToDateTime(DateTime.Now), user, Convert.ToDateTime(DateTime.Now), Convert.ToInt32(PoljeSA), Convert.ToInt32(PozicijaSa), Convert.ToInt32(cboNaSklad.SelectedValue), Convert.ToInt32(cboNaPoz.SelectedValue), Convert.ToInt32(101), "", "Interni", row.Cells[0].Value.ToString(), Convert.ToInt32(VrstaKontejnera), Convert.ToInt32(Brodar), Convert.ToInt32(0), PotrebanCIR);
                       
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select2 = " Select Distinct Skladiste1, Skladista.SkNaziv from PredefinisanePozicije" +
                " inner join Skladista on PredefinisanePozicije.Skladiste1 = Skladista.ID" +
                " Where PredefinisanePozicije.Brodar = " + Brodar + " and  Kvalitet = " + Kvalitet + " and VrstaKontejnera =" + VrstaKontejnera;
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboNaSklad.DataSource = ds2.Tables[0];
            cboNaSklad.DisplayMember = "SkNaziv";
            cboNaSklad.ValueMember = "Skladiste1";


            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var select2 = " select ID, SkNaziv from Skladista order by SkNaziv";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboNaSklad.DataSource = ds2.Tables[0];
            cboNaSklad.DisplayMember = "SkNaziv";
            cboNaSklad.ValueMember = "ID";
        }
    }
}
