using Saobracaj.RadniNalozi;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saobracaj.Izvoz;

namespace Saobracaj.Dokumenta
{
    public partial class frmPovezivanjeKontejneraIVagona : Form
    {
        string OtpremnicaO = "0";
        int dragRow = -1;
        Label dragLabel = null;

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
           
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
               
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                splitContainer1.Panel1.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);




                foreach (Control control in splitContainer1.Panel1.Controls)
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
            
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
       
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxPanel2()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);

            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {

                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                splitContainer1.Panel2.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);




                foreach (Control control in splitContainer1.Panel2.Controls)
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

        public frmPovezivanjeKontejneraIVagona()
        {
            InitializeComponent();
            ChangeTextBox();
            ChangeTextBoxPanel2();
        }

        public frmPovezivanjeKontejneraIVagona(string Otpremnica)
        {
            InitializeComponent();
            OtpremnicaO = Otpremnica;
            ChangeTextBox();
            ChangeTextBoxPanel2();

        }

        public frmPovezivanjeKontejneraIVagona(int PlanIDIzvoza)
        {
            InitializeComponent();
            txtIzvozPlanID.Text = PlanIDIzvoza.ToString();
            ChangeTextBox();
            ChangeTextBoxPanel2();

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmPovezivanjeKontejneraIVagona_Load(object sender, EventArgs e)

        {

            var select8 = "  Select Distinct ID, (NazivVoza + '-' + Cast(BrVoza as nvarchar(10)) + '-' + Relacija) as IdVoza, BrVoza   From Voz order by BrVoza desc";
            var s_connection8 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection8 = new SqlConnection(s_connection8);
            var c8 = new SqlConnection(s_connection8);
            var dataAdapter8 = new SqlDataAdapter(select8, c8);

            var commandBuilder8 = new SqlCommandBuilder(dataAdapter8);
            var ds8 = new DataSet();
            dataAdapter8.Fill(ds8);
            cboBukingPrijema.DataSource = ds8.Tables[0];
            cboBukingPrijema.DisplayMember = "IdVoza";
            cboBukingPrijema.ValueMember = "ID";
            if (txtIzvozPlanID.Text == "0")
            {
                RefreshDataGridKontejneri();
            }
          
        }

        private void RefreshDataGridKontejneri()
        {

            var select = " SELECT OtpremaKontejneraVozStavke.ID, OtpremaKontejneraVozStavke.RB, OtpremaKontejneraVozStavke.IDNadredjenog, " +
 " OtpremaKontejneraVozStavke.BrojKontejnera, OtpremaKontejneraVozStavke.BrojVagona, OtpremaKontejneraVozStavke.Granica,  " +
 " OtpremaKontejneraVozStavke.BrojOsovina, OtpremaKontejneraVozStavke.SopstvenaMasa, OtpremaKontejneraVozStavke.Tara, OtpremaKontejneraVozStavke.Neto, " +
 " Partnerji.PaNaziv AS Posiljalac, Komitenti_1.PaNaziv AS primalac,  Komitenti_2.PaNaziv AS Vlasnikkontejnera,  Komitenti_3.PaNaziv AS Organizator, " +
 " TipKontenjera.Naziv AS TipKontejnera, VrstaRobe.Naziv AS VrstaRobe, OtpremaKontejneraVozStavke.Buking , " +
 " OtpremaKontejneraVozStavke.StatusKontejnera,  OtpremaKontejneraVozStavke.BrojPlombe, OtpremaKontejneraVozStavke.BrojPlombe2, " +
 " OtpremaKontejneraVozStavke.PlaniraniLager, OtpremaKontejneraVozStavke.Datum, OtpremaKontejneraVozStavke.Korisnik, OtpremaKontejneraVozStavke.NapomenaS FROM  Partnerji " +
 " INNER JOIN OtpremaKontejneraVozStavke ON Partnerji.Pasifra = OtpremaKontejneraVozStavke.Posiljalac " +
 " INNER JOIN  Partnerji AS Komitenti_1 ON OtpremaKontejneraVozStavke.Primalac = Komitenti_1.PaSifra INNER JOIN  Partnerji AS Komitenti_2 ON OtpremaKontejneraVozStavke.VlasnikKontejnera = Komitenti_2.PaSifra INNER JOIN  Partnerji AS Komitenti_3 ON OtpremaKontejneraVozStavke.Organizator = Komitenti_3.PaSifra " +
 " INNER JOIN TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
 " INNER JOIN  VrstaRobe ON OtpremaKontejneraVozStavke.VrstaRobe = VrstaRobe.ID  " +
                        " where IdNadredjenog = " + OtpremnicaO + " order by RB";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);


            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 40;
            dataGridView2.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "RB";
            dataGridView2.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Br otp";
            dataGridView2.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Broj kontejnera";
            dataGridView2.Columns[3].Width = 90;



            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "Vagon";
            dataGridView2.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Granica";
            dataGridView2.Columns[5].Width = 40;

            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Broj osovina";
            dataGridView2.Columns[6].Width = 20;

            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "Sopstvena masa";
            dataGridView2.Columns[7].Width = 30;

            DataGridViewColumn column9 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Tara";
            dataGridView2.Columns[8].Width = 50;

            DataGridViewColumn column10 = dataGridView2.Columns[9];
            dataGridView2.Columns[9].HeaderText = "Neto";
            dataGridView2.Columns[9].Width = 50;



            DataGridViewColumn column11 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "Posiljalac";
            dataGridView2.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView2.Columns[11];
            dataGridView2.Columns[11].HeaderText = "Primalac";

            dataGridView2.Columns[11].Width = 50;

            DataGridViewColumn column13 = dataGridView2.Columns[12];
            dataGridView2.Columns[12].HeaderText = "Vlasnik kontejnera";
            dataGridView2.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView2.Columns[13];
            dataGridView2.Columns[13].HeaderText = "Organizator";
            dataGridView2.Columns[13].Width = 40;



            DataGridViewColumn column15 = dataGridView2.Columns[14];
            dataGridView2.Columns[14].HeaderText = "Tip kontejnera";
            dataGridView2.Columns[14].Width = 30;

            DataGridViewColumn column16 = dataGridView2.Columns[15];
            dataGridView2.Columns[15].HeaderText = "Vrsta robe";
            dataGridView2.Columns[15].Width = 30;

            DataGridViewColumn column17 = dataGridView2.Columns[16];
            dataGridView2.Columns[16].HeaderText = "Buking";
            dataGridView2.Columns[16].Width = 30;


            DataGridViewColumn column18 = dataGridView2.Columns[17];
            dataGridView2.Columns[17].HeaderText = " Status Kontejnera";
            dataGridView2.Columns[17].Width = 90;

            DataGridViewColumn column19 = dataGridView2.Columns[18];
            dataGridView2.Columns[18].HeaderText = "Broj plombe";
            dataGridView2.Columns[18].Width = 90;

            DataGridViewColumn column20 = dataGridView2.Columns[19];
            dataGridView2.Columns[19].HeaderText = "Br plombe 2";
            dataGridView2.Columns[19].Width = 70;

            DataGridViewColumn column21 = dataGridView2.Columns[20];
            dataGridView2.Columns[20].HeaderText = "Pl lager";
            dataGridView2.Columns[20].Width = 70;



            DataGridViewColumn column22 = dataGridView2.Columns[21];
            dataGridView2.Columns[21].HeaderText = "Datum";
            dataGridView2.Columns[21].Width = 70;

            DataGridViewColumn column23 = dataGridView2.Columns[22];
            dataGridView2.Columns[22].HeaderText = "Korisnik";
            dataGridView2.Columns[22].Width = 70;

            DataGridViewColumn column24 = dataGridView2.Columns[23];
            dataGridView2.Columns[23].HeaderText = "Napomena";
            dataGridView2.Columns[23].Width = 70;

        }

        private void RefreshDataGridView1Postojeci()
        {

            var select = " select Distinct BrojVagona from PrijemKontejneraVozStavke ";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "VAGON";
            dataGridView1.Columns[0].Width = 240;
          //  dataGridView2.Columns[0].Visible = false;

           

        }


        private void RefreshDataGridPostojeciIzIzvoza()
        {

            var select = " SELECT IzvozKonacna.ID, IzvozKonacna.BrojKontejnera, BrojVagona from IzvozKonacna " +
                       " where IdNadredjena = " + txtIzvozPlanID.Text ;


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);


          



        }

        private void chkPostojeci_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPostojeci.Checked == true)
            {
                RefreshDataGridView1Postojeci();
            }
            else
            {
                RefreshDataGridPostojeciIzIzvoza();
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            
        
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            dragRow = e.RowIndex;
            if (dragLabel == null) dragLabel = new Label();
            dragLabel.Text = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
            txtIzabran.Text = dragLabel.Text;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            dragRow = e.RowIndex;
            if (dragLabel == null) dragLabel = new Label();
            dataGridView2[e.ColumnIndex, e.RowIndex].Value = txtIzabran.Text;
           
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var select = "  select Distinct BrojVagona, PrijemKontejneraVozStavke.BrojKontejnera from PrijemKontejneraVozStavke " +
                " inner join PrijemKontejneraVoz on PrijemKontejneraVoz.ID = PrijemKontejneraVozStavke.IDNadredjenog where PrijemKontejneraVoz.IdVoza = " + Convert.ToInt32(cboBukingPrijema.SelectedValue);


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "VAGON";
            dataGridView1.Columns[0].Width = 240;



           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string IDS = "0";
            string BrojVagona = "";
            InsertOtpremaKontejneraStavke ins = new InsertOtpremaKontejneraStavke();
            InsertIzvozKonacna ik = new InsertIzvozKonacna();
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                  
                        IDS = row.Cells[0].Value.ToString();
                        if (txtIzvozPlanID.Text == "0")
                        {
                        BrojVagona = row.Cells[4].Value.ToString();
                        ins.UpdOtpremaKontejneraVozStavVagoni(Convert.ToInt32(IDS), BrojVagona);

                        }
                    else
                    {
                        BrojVagona = row.Cells[2].Value.ToString();
                        ik.UpdateIzvozKonacnaVagon(Convert.ToInt32(IDS), BrojVagona);
                    }
                        
               

                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }

        
        }
    }
}
