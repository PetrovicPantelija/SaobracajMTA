using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmOtpremaVozaIzPlana : Form
    {
        bool status = false;
        string KorisnikCene = "Panta";
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int IzRNI = 0;
        int trnI = 0;


        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                meniHeader.Visible = false;
                panelHeader.Visible = true;
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
                meniHeader.Visible = true;
                panelHeader.Visible = false;
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
        public frmOtpremaVozaIzPlana()
        {
            InitializeComponent();
            ChangeTextBox();

        }

        public frmOtpremaVozaIzPlana(string NAlogID)
        {
            InitializeComponent();
            IzRNI = 1;
            trnI = Convert.ToInt32(NAlogID);
            txtNalogID.Text = NAlogID;
            ChangeTextBox();
        }

        private void frmOtpremaVozaIzPlana_Load(object sender, EventArgs e)
        {
            var planutovara = "select IzvozKonacnaZaglavlje.ID,(Cast(IzvozKonacnaZaglavlje.ID as nvarchar(5)) + '-' + NazivVoza + '-' + Cast(BrVoza as nvarchar(15)) + ' '  + Relacija) as Naziv from IzvozKonacnaZaglavlje " +
            " inner join Voz on Voz.Id = IzvozKonacnaZaglavlje.IdVoza order by IzvozKonacnaZaglavlje.ID desc";
            var planutovaraSAD = new SqlDataAdapter(planutovara, connection);
            var planutovaraSDS = new DataSet();
            planutovaraSAD.Fill(planutovaraSDS);
            cboPlanUtovara.DataSource = planutovaraSDS.Tables[0];
            cboPlanUtovara.DisplayMember = "Naziv";
            cboPlanUtovara.ValueMember = "ID";


            var select8 = "  Select Distinct ID, (Cast(BrVoza as nvarchar(10)) + '-' + Relacija) as IdVoza   From Voz ";
            var s_connection8 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection8 = new SqlConnection(s_connection8);
            var c8 = new SqlConnection(s_connection8);
            var dataAdapter8 = new SqlDataAdapter(select8, c8);

            var commandBuilder8 = new SqlCommandBuilder(dataAdapter8);
            var ds8 = new DataSet();
            dataAdapter8.Fill(ds8);
            cboVozBuking.DataSource = ds8.Tables[0];
            cboVozBuking.DisplayMember = "IdVoza";
            cboVozBuking.ValueMember = "ID";


            var select4 = "  select PaSifra, PaNaziv from Partnerji order by PaNaziv";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboOperater.DataSource = ds4.Tables[0];
            cboOperater.DisplayMember = "PaNaziv";
            cboOperater.ValueMember = "PaSifra";


            if (IzRNI == 1)
            {
                int voz = VratiPlan();
                cboPlanUtovara.SelectedValue = voz;
                VratiVozIzPlana();
            }
        }

        int VratiPlan()
        {
            int PlanID = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select PlanID from RadniNalogInterni where OjIzdavanja = 2 and ID = " + trnI, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                PlanID = Convert.ToInt32(dr["PlanID"].ToString());
            }
            con.Close();
            return PlanID;


        }

        private void tsNew_Click(object sender, EventArgs e)
        {

            status = true;
            txtSifra.Enabled = false;
            
            dtpDatumOtpreme.Value = DateTime.Now;
            dtpDatumOtpreme.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VratiVozIzPlana();
        }

        private void VratiVozIzPlana()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select Top 1 IDVoza, Operater, Voz.Napomena from IzvozKonacnaZaglavlje " +
" inner join Voz on IzvozKonacnaZaglavlje.IDVoza = Voz.ID " +
" where IzvozKonacnaZaglavlje.ID = " + Convert.ToInt32(cboPlanUtovara.SelectedValue));



          
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboVozBuking.SelectedValue = Convert.ToInt32(dr["IDVoza"].ToString());
                cboOperater.SelectedValue = Convert.ToInt32(dr["Operater"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
            }

            con.Close();

        }

        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from OtpremaKontejnera", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                /// ,  string RegBrKamiona,   string ImeVozaca,   int Vozom
                Dokumenta.InsertOtprema ins = new Dokumenta.InsertOtprema();
                ins.InsertOtp(Convert.ToDateTime(dtpDatumOtpreme.Text), Convert.ToInt32(cboStatusOtpreme.SelectedIndex), Convert.ToInt32(cboVozBuking.SelectedValue), txtRegBrKamiona.Text, txtImeVozaca.Text, Convert.ToDateTime(dtpVremeOdlaska.Value), 1, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtNapomena.Text, 0, 0, 0, 0);
                status = false;
                VratiPodatkeMax();
            }
            else
            {
                if (txtSifra.Text == "")
                {
                    MessageBox.Show("Prvo formirati dokument - novi");
                    return;
                }
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                Dokumenta.InsertOtprema upd = new Dokumenta.InsertOtprema();
                upd.UpdOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumOtpreme.Text), Convert.ToInt32(cboStatusOtpreme.SelectedIndex), Convert.ToInt32(cboVozBuking.SelectedValue), txtRegBrKamiona.Text, txtImeVozaca.Text, Convert.ToDateTime(dtpVremeOdlaska.Value), 1, Convert.ToDateTime(DateTime.Now), KorisnikCene, txtNapomena.Text, 0, 0, 0, 0);
                status = false;
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

        }
        int VratiUsluguPoNalogu(string NalogID)
        {
            int Manipulacija = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT IDManipulacijaJed  from RadniNalogInterni " +
            "  where ID =" + NalogID, con); ;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Manipulacija = Convert.ToInt32(dr["IDManipulacijaJed"].ToString());

            }
            con.Close();
            return Manipulacija;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Usluga = VratiUsluguPoNalogu(txtNalogID.Text);
            if (txtSifra.Text == "")
            { return; }
            else
            {
                InsertIzvozKonacna ins = new InsertIzvozKonacna();
                ins.PrenesiIzPlanUtovaraUOtpremaVoz(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(cboPlanUtovara.SelectedValue));
                VratiPodatkeMax();
                RefreshDataGrid();

            }
            MessageBox.Show("Uspešno ste formirali GATE OUT VOZ za Plan");

            DialogResult dialogResult = MessageBox.Show("Da li želite da formirate RN za otpremu voza GATE OUT VOZ", "Radni nalozi?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                RadniNalozi.RN2OtpremaVoza rnpv = new RadniNalozi.RN2OtpremaVoza(KorisnikCene, cboVozBuking.SelectedValue.ToString(), Usluga.ToString(), txtSifra.Text);
                rnpv.Show();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }


        }

        private void RefreshDataGrid()
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
                       " where IdNadredjenog = " + txtSifra.Text + " order by RB";

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

            //Panta refresh

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "RB";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Br Dok";
            dataGridView1.Columns[2].Width = 30;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[3].Width = 110;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Broj Vagona";
            dataGridView1.Columns[4].Width = 110;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Granica";
            dataGridView1.Columns[5].Width = 50;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Br os";
            dataGridView1.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Sops masa";
            dataGridView1.Columns[7].Width = 50;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Tara";
            dataGridView1.Columns[8].Width = 70;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Neto";
            dataGridView1.Columns[9].Width = 70;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Posiljalac";
            dataGridView1.Columns[10].Width = 50;

            DataGridViewColumn column12 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "Primalac";
            dataGridView1.Columns[11].Width = 50;

            DataGridViewColumn column13 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Vlasnik";
            dataGridView1.Columns[12].Width = 40;

            DataGridViewColumn column14 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "Tip kontejnera";
            dataGridView1.Columns[13].Width = 70;

            DataGridViewColumn column15 = dataGridView1.Columns[14];
            dataGridView1.Columns[14].HeaderText = "NHM";
            dataGridView1.Columns[14].Width = 40;

            DataGridViewColumn column16 = dataGridView1.Columns[15];
            dataGridView1.Columns[15].HeaderText = "Buking broldar";
            dataGridView1.Columns[15].Width = 70;

            DataGridViewColumn column17 = dataGridView1.Columns[16];
            dataGridView1.Columns[16].HeaderText = "Status Kontejnera";
            dataGridView1.Columns[16].Width = 20;

            DataGridViewColumn column18 = dataGridView1.Columns[17];
            dataGridView1.Columns[17].HeaderText = "Br plombe";
            dataGridView1.Columns[17].Width = 90;

            DataGridViewColumn column19 = dataGridView1.Columns[18];
            dataGridView1.Columns[18].HeaderText = "Br plombe2";
            dataGridView1.Columns[18].Width = 9;

            DataGridViewColumn column20 = dataGridView1.Columns[19];
            dataGridView1.Columns[19].HeaderText = "Plan lager";
            dataGridView1.Columns[19].Width = 30;

            DataGridViewColumn column21 = dataGridView1.Columns[20];
            dataGridView1.Columns[20].HeaderText = "Vreme dolaska";
            dataGridView1.Columns[20].Width = 70;

            DataGridViewColumn column22 = dataGridView1.Columns[21];
            dataGridView1.Columns[21].HeaderText = "Vreme prip";
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[21].Width = 70;

            DataGridViewColumn column23 = dataGridView1.Columns[22];
            dataGridView1.Columns[22].HeaderText = "Vreme odlaska";
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[22].Width = 70;

            DataGridViewColumn column24 = dataGridView1.Columns[23];
            dataGridView1.Columns[23].HeaderText = "Datum";
            dataGridView1.Columns[23].Width = 70;
            /*
            DataGridViewColumn column25 = dataGridView1.Columns[24];
            dataGridView1.Columns[24].HeaderText = "Korisnik";
            dataGridView1.Columns[24].Width = 70;

            DataGridViewColumn column26 = dataGridView1.Columns[25];
            dataGridView1.Columns[25].HeaderText = "Napomena stav";
            dataGridView1.Columns[25].Width = 70;
            */

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        Saobracaj.Dokumenta.frmOtpremaKontejneraLegetIZVOZ otpr = new Saobracaj.Dokumenta.frmOtpremaKontejneraLegetIZVOZ(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                        otpr.Show();
                        return;

                    }
                default:
                    {
                        Saobracaj.Dokumeta.frmOtpremaKontejnera otpr = new Saobracaj.Dokumeta.frmOtpremaKontejnera(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                        otpr.Show();
                        return;

                    }
            }
        }

        string VratiKontejner(string NalogID)
        {/*
            select
    CASE
WHEN RadniNalogInterni.OJIzdavanja = 1 THEN(Select BrojKontejnera from UvozKOnacna inner join RadniNalogInterni on UvozKOnacna.ID = RadniNalogInterni.BrojOsnov where RadniNalogInterni.ID = 222)
WHEN RadniNalogInterni.OJIzdavanja = 2 THEN(Select BrojKontejnera from IzvozKonacna inner join RadniNalogInterni on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov where RadniNalogInterni.ID = 222)
else 'NEMA'
END AS BrojKontejnera
from RadniNalogInterni where RadniNalogInterni.ID = 222
            */

            string Konkretan = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select   CASE WHEN RadniNalogInterni.OJIzdavanja = 1 THEN(Select BrojKontejnera from UvozKOnacna inner join RadniNalogInterni on UvozKOnacna.ID = RadniNalogInterni.BrojOsnov where RadniNalogInterni.ID = " + txtNalogID.Text + ") WHEN RadniNalogInterni.OJIzdavanja = 2 THEN(Select BrojKontejnera from IzvozKonacna inner join RadniNalogInterni on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov where RadniNalogInterni.ID = " + txtNalogID.Text + ") else 'NEMA' END AS BrojKontejnera from RadniNalogInterni where RadniNalogInterni.ID =  " + txtNalogID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = dr["BrojKontejnera"].ToString().TrimEnd();



            }
            con.Close();
            return Konkretan;

        }

        private void txtNalogID_TextChanged(object sender, EventArgs e)
        {
            VratiKontejner(txtNalogID.Text);
        }
    }
}
