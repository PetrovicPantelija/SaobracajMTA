using Microsoft.Ajax.Utilities;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmUnosManipulacija : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int pIDPlana = 0;
        int pID = 0;
        int pNalogodavac1 = 0;
        int pNalogodavac2 = 0;
        int pNalogodavac3 = 0;
        int pUvoznik = 0;
        int pomOrgJed = 0;
        int Usao = 0;
        string KorisnikTekuci = "";
        int terminal;
        string relacija;
        int ZelezninaUneta = 0;
        int UnetihManipulacija = 0;
        int ADRSC = 0;
        int ScenarioGL = 0;
        int PunPrazan = 0;

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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                }
            }
            else
            {
                meniHeader.Visible = true;
                panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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


        public frmUnosManipulacija()
        {
            InitializeComponent();
            ChangeTextBox();
            FillDG6(1);
            Usao = 0;
        }

        public frmUnosManipulacija(int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3, int Uvoznik, string Korisnik,int Terminal,string Relacija, int Zeleznina, int ADR, int ScenarioGLsf, int pp)
        {
            InitializeComponent();
            ChangeTextBox();
            pIDPlana = IDPlana;
            pID = ID;
            txtID.Text = pID.ToString();
            pNalogodavac1 = Nalogodavac1;
            pNalogodavac2 = Nalogodavac2;
            pNalogodavac3 = Nalogodavac3;
            pUvoznik = Uvoznik;
            txtNadredjeni.Text = pIDPlana.ToString();
          
            KorisnikTekuci = Korisnik;
            Usao = 0;
            terminal = Terminal;
            relacija = Relacija;
            ScenarioGL = ScenarioGLsf;
            UnetihManipulacija = VratiBrojManipulacija(ID);
            ZelezninaUneta = VratiUnetuZelezninu(ID);
            if (Zeleznina != ZelezninaUneta)
            {
                var result = System.Windows.Forms.MessageBox.Show("Promenjena je železnina", "Provera železnine", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    UbaciZelezninu(Zeleznina, ZelezninaUneta, pID);
                }

            }
            if (ADR != 0)
            {
                ADRSC = 1;
            }
            PunPrazan = pp;
            FillDG6(1);
            FillDG8();
            FillDG8Dodatne();
            FillDG8Administracija();

        }

        private void UbaciZelezninu(int ZelezninaNova, int ZelezninaStara, int pID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Cena from VrstaManipulacije where  ID = " + ZelezninaNova, con);
            SqlDataReader dr = cmd.ExecuteReader();
            double pomCena = 0;
            while (dr.Read())
            {
                //Izmenjeno
                if (dr["Cena"].ToString() == "")
                        { pomCena = 0; }
                else
                {
                    pomCena = Convert.ToDouble(dr["Cena"].ToString());

                }
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());

                // pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                // Nece se analizirati podrazumevano da nije cekirano
                // pomSaPDV = Convert.ToInt32(dr["SaPDV"].ToString());
            }

            con.Close();


            if (ZelezninaStara == 0)
            {

                UbaciStavkuUsluge(pID, ZelezninaNova, pomCena, 1, 1, 0, "", 0, "");

            }
            else
            {
                var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con2 = new SqlConnection(s_connection2);

                con2.Open();

                SqlCommand cmd2 = new SqlCommand("update UvozVrstaManipulacije set IDVrstaManipulacije = " + ZelezninaNova + ", Cena = " + pomCena + " where IDVrstaManipulacije = " + ZelezninaStara + " and IDNAdredjena = " + pID , con2);
                SqlDataReader dr2 = cmd2.ExecuteReader();
         
              

                con2.Close();

            }
          

        }

        private void button11_Click(object sender, EventArgs e)
        {
            FillDG6(1);
        }


        private void FillDG6(int TipUsluge)
        {
            if (TipUsluge == 1)
            {
                //Administrativne
                var select = "SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
   " VrstaManipulacije.[JM] " +
  " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
  " ,VrstaManipulacije.[Cena] ,'',0, '/',  VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM[VrstaManipulacije] " +
  "  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where Administrativna = 0  order by VrstaManipulacije.[Naziv] ";
                SqlConnection conn = new SqlConnection(connection);
                var da = new SqlDataAdapter(select, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView6.ReadOnly = false;
                dataGridView6.DataSource = ds.Tables[0];


                PodesiDatagridView(dataGridView6);

                  //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
                  DataGridViewColumn column = dataGridView6.Columns[0];
                dataGridView6.Columns[0].HeaderText = "ID";
                dataGridView6.Columns[0].Width = 20;

                DataGridViewColumn column2 = dataGridView6.Columns[1];
                dataGridView6.Columns[1].HeaderText = "Naziv";
                dataGridView6.Columns[1].Width = 180;

                DataGridViewColumn column3 = dataGridView6.Columns[2];
                dataGridView6.Columns[2].HeaderText = "JM";
                dataGridView6.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView6.Columns[3];
                dataGridView6.Columns[3].HeaderText = "Tip manipulacije";
                dataGridView6.Columns[3].Width = 50;
                dataGridView6.Columns[3].Visible = false;


                DataGridViewColumn column5 = dataGridView6.Columns[4];
                dataGridView6.Columns[4].HeaderText = "OJ";
                dataGridView6.Columns[4].Width = 30;

                DataGridViewColumn column6 = dataGridView6.Columns[5];
                dataGridView6.Columns[5].HeaderText = "OJ naziv";
                dataGridView6.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView6.Columns[6];
                dataGridView6.Columns[6].HeaderText = "Cena";
                dataGridView6.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView6.Columns[7];
                dataGridView6.Columns[7].HeaderText = "Pokret";
                dataGridView6.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView6.Columns[8];
                dataGridView6.Columns[8].HeaderText = "StatusID";
                dataGridView6.Columns[8].Width = 30;

                DataGridViewColumn column10 = dataGridView6.Columns[9];
                dataGridView6.Columns[9].HeaderText = "Status";
                dataGridView6.Columns[9].Width = 130;
            }
            else if (TipUsluge == 2)
            {
                //Po grupi Manipulacije
                var select = "SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
      " VrstaManipulacije.[JM] " +
     " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
     " ,VrstaManipulacije.[Cena] ,'',0, '/',  VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM [VrstaManipulacije] " +
     "  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where VrstaManipulacije.GrupaVrsteManipulacijeID = " + Convert.ToInt32(cboGrupaVrsteManipulacije.SelectedValue) + "  order by VrstaManipulacije.[Naziv] ";
                SqlConnection conn = new SqlConnection(connection);
                var da = new SqlDataAdapter(select, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView6.ReadOnly = false;
                dataGridView6.DataSource = ds.Tables[0];


                PodesiDatagridView(dataGridView6);

                //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
                DataGridViewColumn column = dataGridView6.Columns[0];
                dataGridView6.Columns[0].HeaderText = "ID";
                dataGridView6.Columns[0].Width = 20;

                DataGridViewColumn column2 = dataGridView6.Columns[1];
                dataGridView6.Columns[1].HeaderText = "Naziv";
                dataGridView6.Columns[1].Width = 180;

                DataGridViewColumn column3 = dataGridView6.Columns[2];
                dataGridView6.Columns[2].HeaderText = "JM";
                dataGridView6.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView6.Columns[3];
                dataGridView6.Columns[3].HeaderText = "Tip manipulacije";
                dataGridView6.Columns[3].Width = 50;
                dataGridView6.Columns[3].Visible = false;


                DataGridViewColumn column5 = dataGridView6.Columns[4];
                dataGridView6.Columns[4].HeaderText = "OJ";
                dataGridView6.Columns[4].Width = 30;

                DataGridViewColumn column6 = dataGridView6.Columns[5];
                dataGridView6.Columns[5].HeaderText = "OJ naziv";
                dataGridView6.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView6.Columns[6];
                dataGridView6.Columns[6].HeaderText = "Cena";
                dataGridView6.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView6.Columns[7];
                dataGridView6.Columns[7].HeaderText = "Pokret";
                dataGridView6.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView6.Columns[8];
                dataGridView6.Columns[8].HeaderText = "StatusID";
                dataGridView6.Columns[8].Width = 30;

                DataGridViewColumn column10 = dataGridView6.Columns[9];
                dataGridView6.Columns[9].HeaderText = "Status";
                dataGridView6.Columns[9].Width = 130;
            }

            else if (TipUsluge == 3)
            {
                //Po grupi Manipulacije
                var select = "SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
      " VrstaManipulacije.[JM] " +
     " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
     " ,VrstaManipulacije.[Cena] ,'',0, '/',  VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM [VrstaManipulacije] " +
     "  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where VrstaManipulacije.Dodatna = 1  order by VrstaManipulacije.[Naziv] ";
                SqlConnection conn = new SqlConnection(connection);
                var da = new SqlDataAdapter(select, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView6.ReadOnly = false;
                dataGridView6.DataSource = ds.Tables[0];


                PodesiDatagridView(dataGridView6);

                //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
                DataGridViewColumn column = dataGridView6.Columns[0];
                dataGridView6.Columns[0].HeaderText = "ID";
                dataGridView6.Columns[0].Width = 20;

                DataGridViewColumn column2 = dataGridView6.Columns[1];
                dataGridView6.Columns[1].HeaderText = "Naziv";
                dataGridView6.Columns[1].Width = 180;

                DataGridViewColumn column3 = dataGridView6.Columns[2];
                dataGridView6.Columns[2].HeaderText = "JM";
                dataGridView6.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView6.Columns[3];
                dataGridView6.Columns[3].HeaderText = "Tip manipulacije";
                dataGridView6.Columns[3].Width = 50;
                dataGridView6.Columns[3].Visible = false;


                DataGridViewColumn column5 = dataGridView6.Columns[4];
                dataGridView6.Columns[4].HeaderText = "OJ";
                dataGridView6.Columns[4].Width = 30;

                DataGridViewColumn column6 = dataGridView6.Columns[5];
                dataGridView6.Columns[5].HeaderText = "OJ naziv";
                dataGridView6.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView6.Columns[6];
                dataGridView6.Columns[6].HeaderText = "Cena";
                dataGridView6.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView6.Columns[7];
                dataGridView6.Columns[7].HeaderText = "Pokret";
                dataGridView6.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView6.Columns[8];
                dataGridView6.Columns[8].HeaderText = "StatusID";
                dataGridView6.Columns[8].Width = 30;

                DataGridViewColumn column10 = dataGridView6.Columns[9];
                dataGridView6.Columns[9].HeaderText = "Status";
                dataGridView6.Columns[9].Width = 130;
            }
            else
            {
                var select = "SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
   " VrstaManipulacije.[JM] " +
  " ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
  " ,VrstaManipulacije.[Cena] ,'',0, '/',  VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM[VrstaManipulacije] " +
  "  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where Administrativna = 1  order by VrstaManipulacije.[Naziv] ";
                SqlConnection conn = new SqlConnection(connection);
                var da = new SqlDataAdapter(select, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView6.ReadOnly = false;
                dataGridView6.DataSource = ds.Tables[0];


                PodesiDatagridView(dataGridView6);

                DataGridViewColumn column = dataGridView6.Columns[0];
                dataGridView6.Columns[0].HeaderText = "ID";
                dataGridView6.Columns[0].Width = 20;

                DataGridViewColumn column2 = dataGridView6.Columns[1];
                dataGridView6.Columns[1].HeaderText = "Naziv";
                dataGridView6.Columns[1].Width = 180;

                DataGridViewColumn column3 = dataGridView6.Columns[2];
                dataGridView6.Columns[2].HeaderText = "JM";
                dataGridView6.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView6.Columns[3];
                dataGridView6.Columns[3].HeaderText = "Tip manipulacije";
                dataGridView6.Columns[3].Width = 50;
                dataGridView6.Columns[3].Visible = false;


                DataGridViewColumn column5 = dataGridView6.Columns[4];
                dataGridView6.Columns[4].HeaderText = "OJ";
                dataGridView6.Columns[4].Width = 30;

                DataGridViewColumn column6 = dataGridView6.Columns[5];
                dataGridView6.Columns[5].HeaderText = "OJ naziv";
                dataGridView6.Columns[5].Width = 100;

                DataGridViewColumn column7 = dataGridView6.Columns[6];
                dataGridView6.Columns[6].HeaderText = "Cena";
                dataGridView6.Columns[6].Width = 50;

                DataGridViewColumn column8 = dataGridView6.Columns[7];
                dataGridView6.Columns[7].HeaderText = "Pokret";
                dataGridView6.Columns[7].Width = 150;

                DataGridViewColumn column9 = dataGridView6.Columns[8];
                dataGridView6.Columns[8].HeaderText = "StatusID";
                dataGridView6.Columns[8].Width = 30;

                DataGridViewColumn column10 = dataGridView6.Columns[9];
                dataGridView6.Columns[9].HeaderText = "Status";
                dataGridView6.Columns[9].Width = 130;
            }
        }

        private void FillDG6Scenario()
        {

            var select = " SELECT VrstaManipulacije.[ID]      ,VrstaManipulacije.[Naziv] ,  " +
 " VrstaManipulacije.[JM] " +
" ,VrstaManipulacije.[TipManipulacije]      ,VrstaManipulacije.[OrgJed]      ,OrganizacioneJedinice.Naziv as OJ " +
" ,VrstaManipulacije.[Cena] ,Scenario.Pokret,Scenario.StatusKontejnera,KontejnerStatus.Naziv, Scenario.Forma, " +
" VrstaManipulacije.[Datum] ,VrstaManipulacije.[Korisnik] FROM [VrstaManipulacije] " +
" inner join Scenario on Scenario.Usluga = VrstaManipulacije.ID " +
" inner join kontejnerStatus on KontejnerStatus.ID = Scenario.statusKOntejnera " +
"  inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID where Scenario.ID = " + Convert.ToInt32(cboScenario.SelectedValue) + " order by Scenario.RB asc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Naziv";
            dataGridView6.Columns[1].Width = 180;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "JM";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "Tip manipulacije";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;


            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "OJ";
            dataGridView6.Columns[4].Width = 30;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OJ naziv";
            dataGridView6.Columns[5].Width = 100;

            DataGridViewColumn column7 = dataGridView6.Columns[6];
            dataGridView6.Columns[6].HeaderText = "Cena";
            dataGridView6.Columns[6].Width = 50;

            DataGridViewColumn column8 = dataGridView6.Columns[7];
            dataGridView6.Columns[7].HeaderText = "Pokret";
            dataGridView6.Columns[7].Width = 150;

            DataGridViewColumn column9 = dataGridView6.Columns[8];
            dataGridView6.Columns[8].HeaderText = "StatusID";
            dataGridView6.Columns[8].Width = 30;

            DataGridViewColumn column10 = dataGridView6.Columns[9];
            dataGridView6.Columns[9].HeaderText = "Status";
            dataGridView6.Columns[9].Width = 130;

            DataGridViewColumn column11 = dataGridView6.Columns[10];
            dataGridView6.Columns[10].HeaderText = "Otvara formu";
            dataGridView6.Columns[10].Width = 100;

        }
        private void FillDN1()
        {
            //Cenovnik po Nalogodavcu broj 1
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
              "inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
            " where  Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }
        private void FillDGN2()
        {
            //Cenovnik po Nalogodavcu broj 2
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed  from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
             "inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
           " where  Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;


            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }
        private void FillDGN3()
        {
            //Cenovnik po Nalogodavcu broj 2
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed  from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
           " where  Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }


        private void FillDN1BU()
        {
            //Cenovnik po Nalogodavcu broj 1
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
              "inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
            " where  Uvoznik = " + Convert.ToInt32(0) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }
        private void FillDGN2BU()
        {
            //Cenovnik po Nalogodavcu broj 2
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed  from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
             "inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
           " where  Uvoznik = " + Convert.ToInt32(0) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView6);
            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;


            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }
        private void FillDGN3BU()
        {
            //Cenovnik po Nalogodavcu broj 2
            var select = "select Cene.ID, VrstaManipulacije.Naziv, Cene.Cena, VrstaManipulacije.ID, 1 as Kolicina, VrstaManipulacije.OrgJed  from Cene " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = Cene.VrstaManipulacije " +
            " inner join OrganizacioneJedinice on VrstaManipulacije.OrgJed = OrganizacioneJedinice.ID " +
           " where  Uvoznik = " + Convert.ToInt32(0) + " and Cene.Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "  order by VrstaManipulacije.Naziv ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView6.ReadOnly = false;
            dataGridView6.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView6);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView6.Columns[0];
            dataGridView6.Columns[0].HeaderText = "ID";
            dataGridView6.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView6.Columns[1];
            dataGridView6.Columns[1].HeaderText = "Man";
            dataGridView6.Columns[1].Width = 120;

            DataGridViewColumn column3 = dataGridView6.Columns[2];
            dataGridView6.Columns[2].HeaderText = "Cena";
            dataGridView6.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView6.Columns[3];
            dataGridView6.Columns[3].HeaderText = "IDVM";
            dataGridView6.Columns[3].Width = 50;
            dataGridView6.Columns[3].Visible = false;

            DataGridViewColumn column5 = dataGridView6.Columns[4];
            dataGridView6.Columns[4].HeaderText = "Količina";
            dataGridView6.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView6.Columns[5];
            dataGridView6.Columns[5].HeaderText = "OrgJed";
            dataGridView6.Columns[5].Width = 50;
        }




        private void button13_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Pretrazuje cenovnik sa parametrima Nalogodavci 1 i Izvoznik");

            // FillDG7();
            FillDN1();
        }

        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena, double Kolicina, int OrgJed, int pomPlatilac, string pomPokret, int pomStatusKontejnera, string pomForma)
        {
           
            if (txtNadredjeni.Text != "0")
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.InsUbaciUsluguKonacna(ID, Manipulacija, Cena, Kolicina, OrgJed, pomPlatilac, 0, pomPokret, pomStatusKontejnera, KorisnikTekuci, pomForma);
                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
            }
            else
            {
                InsertUvozKonacna uvK = new InsertUvozKonacna();
                uvK.InsUbaciUslugu(ID, Manipulacija, Cena, Kolicina, OrgJed, pomPlatilac, 0, pomPokret, pomStatusKontejnera, KorisnikTekuci, pomForma);
                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();

            }
           

        }
        int VratiManipulaciju(int ID)
        {
            int pomBZ = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
            if (txtNadredjeni.Text != "0")
            {
                Komanda = "select IDVrstaManipulacije from UvozKonacnaVrstaManipulacije  where ID =  " + ID;
            }
            else
            {
                Komanda = "select IDVrstaManipulacije from UvozVrstaManipulacije  where ID =  " + ID;
            }

            SqlCommand cmd = new SqlCommand(Komanda + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomBZ = Convert.ToInt32(dr["IDVrstaManipulacije"].ToString());
            }
            con.Close();
            return pomBZ;

        }



        //Panta
        int VratiBrojManipulacija(int ID)
        {
            int pomBZ = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
            if (txtNadredjeni.Text != "0")
            {
                Komanda = "select Count(*) as Broj from UvozKonacnaVrstaManipulacije  inner join vrstamanipulacije on VrstaManipulacije.ID = IDVrstaManipulacije where Dodatna = 0 and Administrativna = 0 and IDNadredjena= ";
            }
            else
            {
                Komanda = "select Count(*) as Broj from UvozVrstaManipulacije  inner join vrstamanipulacije on VrstaManipulacije.ID = IDVrstaManipulacije where Dodatna = 0 and Administrativna = 0and IDNadredjena= ";
            }

                SqlCommand cmd = new SqlCommand(Komanda + txtID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomBZ = Convert.ToInt32(dr["Broj"].ToString());
            }
            con.Close();
            return pomBZ;

        }

        int VratiUnetuZelezninu(int ID)
        {
            int pomBZ = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
            if (txtNadredjeni.Text != "0")
            {
                Komanda = "select VrstaManipulacije.ID from UvozKonacnaVrstaManipulacije  inner join vrstamanipulacije on VrstaManipulacije.ID = IDVrstaManipulacije where GrupaVrsteManipulacijeID = 1 and IDNadredjena= ";
            }
            else
            {
                Komanda = "select VrstaManipulacije.ID from UvozVrstaManipulacije  inner join vrstamanipulacije on VrstaManipulacije.ID = IDVrstaManipulacije where GrupaVrsteManipulacijeID = 1 and IDNadredjena= ";
            }

            SqlCommand cmd = new SqlCommand(Komanda + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomBZ = Convert.ToInt32(dr["ID"].ToString());
            }
            con.Close();
            return pomBZ;

        }

        int VratiBrojScenario(int ID)
        {
            int pomBZ = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Count(*) as Broj from Scenario  where ID= " + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomBZ = Convert.ToInt32(dr["Broj"].ToString());
            }
            con.Close();
            return pomBZ;

        }

        int ProveriDaLiSuIsteManipulacije(int ID, int SC)
        {
            int postoji = 0;
            int konacan = 1;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select usluga from Scenario  where ID= " + SC, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                postoji = VratiPostojiUsluga(Convert.ToInt32(dr["usluga"].ToString()),Convert.ToInt32(txtID.Text));
                if (postoji == 0)
                {
                    konacan = 0;
                }
            }
            con.Close();
            return konacan;

        }
        int VratiPostojiUsluga(int usluga, int kontejnerid)
        {
            int postoji = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            if (txtNadredjeni.Text != "0")
            {
                Komanda = "select Count(*) as Broj from UvozKonacnaVrstaManipulacije where IDNadredjena = " + kontejnerid + " and IDVrstaManipulacije = " + usluga;
            }
            else
            {
                Komanda = "select Count(*) as Broj from UvozVrstaManipulacije where IDNadredjena = " + kontejnerid + " and IDVrstaManipulacije = " + usluga;
            }

            SqlCommand cmd = new SqlCommand(Komanda, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                if (Convert.ToInt32(dr["Broj"].ToString()) == 1)
                {
                    postoji = 1;
                }
            }
            con.Close();
            return postoji;


        }

        private void ProveriScenario(int ID)
        {
            //Panta 2
            // broj Zapisa za kontejner
            //Izab raniScenario
            int IzabraniScenario = 0;
            int BrojZapisaKontejnera = VratiBrojManipulacija(ID);
            int BSC1 = VratiBrojScenario(1);
            int BSC2 = VratiBrojScenario(2);
            int BSC3 = VratiBrojScenario(3);
            int BSC4 = VratiBrojScenario(4);
            int BSC5 = VratiBrojScenario(5);
            int BSC6 = VratiBrojScenario(6);

            int BSC18 = VratiBrojScenario(18);
            int BSC19 = VratiBrojScenario(19);
            int BSC20 = VratiBrojScenario(20);
            int BSC21 = VratiBrojScenario(21);
            int BSC22 = VratiBrojScenario(22);
            int BSC27 = VratiBrojScenario(27);
            int BSC28 = VratiBrojScenario(28);
            int BSC30 = VratiBrojScenario(30);
            int BSC31 = VratiBrojScenario(31);
            //  int BSC6 = VratiBrojScenario(6);

            int rasporedjen = 0;
            if (txtNadredjeni.Text != "0")
            {
                rasporedjen = 1;
            }
            else
            {
                rasporedjen = 0;
            }
            if (BrojZapisaKontejnera == BSC1 && ADRSC == 0 && PunPrazan > 0)
            {
                IzabraniScenario =  ProveriDaLiSuIsteManipulacije(ID,1);
                if (IzabraniScenario == 1 && ADRSC == 0 && PunPrazan>0)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(1, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 1");
                }
            }
            if (BrojZapisaKontejnera == BSC2 && ADRSC == 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 2);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(2, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 2");
                }
            }

            if (BrojZapisaKontejnera == BSC3 && ADRSC == 0 && PunPrazan > 0)   
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 3);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(3, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 3");
                }
            }

            if (BrojZapisaKontejnera == BSC4 && ADRSC == 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 4);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(4, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 4");
                }
            }

            if (BrojZapisaKontejnera == BSC5 && ADRSC == 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 5);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(5, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 5");
                }
            }

            if (BrojZapisaKontejnera == BSC6 && ADRSC == 0 && PunPrazan == 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 6);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(6, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 6");
                }
            }


            if (BrojZapisaKontejnera == BSC18 && ADRSC > 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 18);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(18, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 18");
                }
            }

            if (BrojZapisaKontejnera == BSC19 && ADRSC > 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 19);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(19, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 19");
                }
            }

            if (BrojZapisaKontejnera == BSC20 && ADRSC > 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 20);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(20, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 20");
                }
            }

            if (BrojZapisaKontejnera == BSC21 && ADRSC > 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 21);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(21, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 21");
                }
            }

            if (BrojZapisaKontejnera == BSC22 && ADRSC > 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 22);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(22, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 22");
                }
            }

            if (BrojZapisaKontejnera == BSC27 && ADRSC == 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 27);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(27, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 27");
                }
            }

            if (BrojZapisaKontejnera == BSC28 && ADRSC == 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 28);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(28, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 28");
                }
            }

            if (BrojZapisaKontejnera == BSC30 && ADRSC > 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 29);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(30, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 30");
                }
            }

            if (BrojZapisaKontejnera == BSC31 && ADRSC > 0 && PunPrazan > 0)
            {
                IzabraniScenario = ProveriDaLiSuIsteManipulacije(ID, 31);
                if (IzabraniScenario == 1)
                {
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(31, ID, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario 31");
                }
            }
            if (IzabraniScenario == 0)
            {
                InsertScenario isc = new InsertScenario();
                isc.UpdScenarioKontejnera(0, ID, 1, rasporedjen);
                System.Windows.Forms.MessageBox.Show("Ne postoji scenario koji odgovara preostalim uslugama");
            
            }

            // Proveri da li je isti broj 
            // Proveri da lii su iste manipulacije

        }

        private void FillDG8()
        {
            var select = "";


            if (txtNadredjeni.Text == "0")
            {
                select = "select  UvozVrstaManipulacije.ID as ID, UvozVrstaManipulacije.IDNadredjena as KontejnerID, Uvoz.BrojKontejnera, " +
" UvozVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV, Pokret, KontejnerStatus.Naziv, Forma " +
" from UvozVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije " +
" inner " +
" join PArtnerji on UvozVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozVrstaManipulacije.OrgJed " +
" inner " +
" join Uvoz on UvozVrstaManipulacije.IDNadredjena = Uvoz.ID" +
" left join KontejnerStatus on  UvozVrstaManipulacije.StatusKontejnera = KontejnerStatus.ID " +
" where Uvoz.ID = " + Convert.ToInt32(txtID.Text) + "  and Dodatna = 0 and Administrativna = 0 order by UvozVrstaManipulacije.ID";


            }
            else
            {
                select = "select  UvozKonacnaVrstaManipulacije.ID as ID, UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, UvozKonacna.BrojKontejnera, " +
" UvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV, UvozKonacnaVrstaManipulacije.Pokret, KontejnerStatus.Naziv, Forma " +
" from UvozKonacnaVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
" inner " +
" join PArtnerji on UvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozKonacnaVrstaManipulacije.OrgJed " +
" inner " +
" join UvozKonacna on UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID  " +
" inner " +
" join KontejnerStatus on UvozKonacnaVrstaManipulacije.StatusKOntejnera = KontejnerStatus.ID  " +
"where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text) + " and Dodatna = 0 and Administrativna = 0 order by UvozKonacnaVrstaManipulacije.ID";
            }

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView7.ReadOnly = false;
            dataGridView7.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView7);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView7.Columns[0];
            dataGridView7.Columns[0].HeaderText = "ID";
            dataGridView7.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView7.Columns[1];
            dataGridView7.Columns[1].HeaderText = "USL ID";
            dataGridView7.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView7.Columns[2];
            dataGridView7.Columns[2].HeaderText = "Kontejner";
            dataGridView7.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView7.Columns[3];
            dataGridView7.Columns[3].HeaderText = "Kol";
            dataGridView7.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView7.Columns[4];
            dataGridView7.Columns[4].HeaderText = "VRU";
            dataGridView7.Columns[4].Width = 30;

            DataGridViewColumn column6 = dataGridView7.Columns[5];
            dataGridView7.Columns[5].HeaderText = "Usluga";
            dataGridView7.Columns[5].Width = 250;

            DataGridViewColumn column7 = dataGridView7.Columns[6];
            dataGridView7.Columns[6].HeaderText = "Cena";
            dataGridView7.Columns[6].Width = 80;


            DataGridViewColumn column8 = dataGridView7.Columns[7];
            dataGridView7.Columns[7].HeaderText = "OJID";
            dataGridView7.Columns[7].Width = 20;

            DataGridViewColumn column9 = dataGridView7.Columns[8];
            dataGridView7.Columns[8].HeaderText = "Org Jed";
            dataGridView7.Columns[8].Width = 120;

            DataGridViewColumn column10 = dataGridView7.Columns[9];
            dataGridView7.Columns[9].HeaderText = "PLID";
            dataGridView7.Columns[9].Width = 40;

            DataGridViewColumn column11 = dataGridView7.Columns[10];
            dataGridView7.Columns[10].HeaderText = "Platilac";
            dataGridView7.Columns[10].Width = 140;

            //dataGridView7.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView7.SelectAll();
            /*
                        DataGridViewColumn column12 = dataGridView7.Columns[11];
                        dataGridView7.Columns[11].HeaderText = "SaPDV";
                        dataGridView7.Columns[11].Width = 140;
            */
        }

        private void FillDG8Dodatne()
        {
            var select = "";


            if (txtNadredjeni.Text == "0")
            {
                select = "select  UvozVrstaManipulacije.ID as ID, UvozVrstaManipulacije.IDNadredjena as KontejnerID, Uvoz.BrojKontejnera, " +
" UvozVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV, Pokret, KontejnerStatus.Naziv, Forma " +
" from UvozVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije " +
" inner " +
" join PArtnerji on UvozVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozVrstaManipulacije.OrgJed " +
" inner " +
" join Uvoz on UvozVrstaManipulacije.IDNadredjena = Uvoz.ID" +
" left join KontejnerStatus on  UvozVrstaManipulacije.StatusKontejnera = KontejnerStatus.ID " +
" where Uvoz.ID = " + Convert.ToInt32(txtID.Text) + "  and Dodatna = 1 and Administrativna = 0 order by UvozVrstaManipulacije.ID";


            }
            else
            {
                select = "select  UvozKonacnaVrstaManipulacije.ID as ID, UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, UvozKonacna.BrojKontejnera, " +
" UvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV, UvozKonacnaVrstaManipulacije.Pokret, KontejnerStatus.Naziv, Forma " +
" from UvozKonacnaVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
" inner " +
" join PArtnerji on UvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozKonacnaVrstaManipulacije.OrgJed " +
" inner " +
" join UvozKonacna on UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID  " +
" inner " +
" join KontejnerStatus on UvozKonacnaVrstaManipulacije.StatusKOntejnera = KontejnerStatus.ID  " +
"where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text) + " and Dodatna = 1 and Administrativna = 0 order by UvozKonacnaVrstaManipulacije.ID";
            }

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView2);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "USL ID";
            dataGridView2.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "Kontejner";
            dataGridView2.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Kol";
            dataGridView2.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView2.Columns[4];
            dataGridView2.Columns[4].HeaderText = "VRU";
            dataGridView2.Columns[4].Width = 30;

            DataGridViewColumn column6 = dataGridView2.Columns[5];
            dataGridView2.Columns[5].HeaderText = "Usluga";
            dataGridView2.Columns[5].Width = 250;

            DataGridViewColumn column7 = dataGridView2.Columns[6];
            dataGridView2.Columns[6].HeaderText = "Cena";
            dataGridView2.Columns[6].Width = 80;


            DataGridViewColumn column8 = dataGridView2.Columns[7];
            dataGridView2.Columns[7].HeaderText = "OJID";
            dataGridView2.Columns[7].Width = 20;

            DataGridViewColumn column9 = dataGridView2.Columns[8];
            dataGridView2.Columns[8].HeaderText = "Org Jed";
            dataGridView2.Columns[8].Width = 120;

            DataGridViewColumn column10 = dataGridView2.Columns[9];
            dataGridView2.Columns[9].HeaderText = "PLID";
            dataGridView2.Columns[9].Width = 40;

            DataGridViewColumn column11 = dataGridView2.Columns[10];
            dataGridView2.Columns[10].HeaderText = "Platilac";
            dataGridView2.Columns[10].Width = 140;

            //dataGridView7.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView7.SelectAll();
            /*
                        DataGridViewColumn column12 = dataGridView7.Columns[11];
                        dataGridView7.Columns[11].HeaderText = "SaPDV";
                        dataGridView7.Columns[11].Width = 140;
            */
        }

        private void FillDG8Administracija()
        {
            var select = "";


            if (txtNadredjeni.Text == "0")
            {
                select = "select  UvozVrstaManipulacije.ID as ID, UvozVrstaManipulacije.IDNadredjena as KontejnerID, Uvoz.BrojKontejnera, " +
" UvozVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV, Pokret, KontejnerStatus.Naziv, Forma " +
" from UvozVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije " +
" inner " +
" join PArtnerji on UvozVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozVrstaManipulacije.OrgJed " +
" inner " +
" join Uvoz on UvozVrstaManipulacije.IDNadredjena = Uvoz.ID" +
" left join KontejnerStatus on  UvozVrstaManipulacije.StatusKontejnera = KontejnerStatus.ID " +
" where Uvoz.ID = " + Convert.ToInt32(txtID.Text) + "  and Dodatna = 0 and Administrativna = 1 order by UvozVrstaManipulacije.ID";


            }
            else
            {
                select = "select  UvozKonacnaVrstaManipulacije.ID as ID, UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, UvozKonacna.BrojKontejnera, " +
" UvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" UvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
" Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac, SaPDV, UvozKonacnaVrstaManipulacije.Pokret, KontejnerStatus.Naziv, Forma " +
" from UvozKonacnaVrstaManipulacije " +
" Inner    join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
" inner " +
" join PArtnerji on UvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra " +
" inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozKonacnaVrstaManipulacije.OrgJed " +
" inner " +
" join UvozKonacna on UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID  " +
" inner " +
" join KontejnerStatus on UvozKonacnaVrstaManipulacije.StatusKOntejnera = KontejnerStatus.ID  " +
"where UvozKonacna.IDNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text) + " and Dodatna = 0 and Administrativna = 1 order by UvozKonacnaVrstaManipulacije.ID";
            }

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = false;
            dataGridView3.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView3);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "USL ID";
            dataGridView3.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView3.Columns[2];
            dataGridView3.Columns[2].HeaderText = "Kontejner";
            dataGridView3.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView3.Columns[3];
            dataGridView3.Columns[3].HeaderText = "Kol";
            dataGridView3.Columns[3].Width = 50;

            DataGridViewColumn column5 = dataGridView3.Columns[4];
            dataGridView3.Columns[4].HeaderText = "VRU";
            dataGridView3.Columns[4].Width = 30;

            DataGridViewColumn column6 = dataGridView3.Columns[5];
            dataGridView3.Columns[5].HeaderText = "Usluga";
            dataGridView3.Columns[5].Width = 250;

            DataGridViewColumn column7 = dataGridView3.Columns[6];
            dataGridView3.Columns[6].HeaderText = "Cena";
            dataGridView3.Columns[6].Width = 80;


            DataGridViewColumn column8 = dataGridView3.Columns[7];
            dataGridView3.Columns[7].HeaderText = "OJID";
            dataGridView3.Columns[7].Width = 20;

            DataGridViewColumn column9 = dataGridView3.Columns[8];
            dataGridView3.Columns[8].HeaderText = "Org Jed";
            dataGridView3.Columns[8].Width = 120;

            DataGridViewColumn column10 = dataGridView3.Columns[9];
            dataGridView3.Columns[9].HeaderText = "PLID";
            dataGridView3.Columns[9].Width = 40;

            DataGridViewColumn column11 = dataGridView3.Columns[10];
            dataGridView3.Columns[10].HeaderText = "Platilac";
            dataGridView3.Columns[10].Width = 140;

            //dataGridView7.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView7.SelectAll();
            /*
                        DataGridViewColumn column12 = dataGridView7.Columns[11];
                        dataGridView7.Columns[11].HeaderText = "SaPDV";
                        dataGridView7.Columns[11].Width = 140;
            */
        }

        int PretraziPoNalogodavciIIzvozniku(int PomManipulacija, int Nalogodavac, int Uvoznik)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int tmp = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) as Broj from Cene where Komitent = " + Nalogodavac + " and Uvoznik = " + Uvoznik + " and ID = " + PomManipulacija, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {


                tmp = Convert.ToInt32(dr["Broj"].ToString());

            }

            con.Close();
            if (tmp > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        int PretraziPoNalogodavci(int PomManipulacija, int Nalogodavac, int Uvoznik)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int tmp = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(*) as Broj from Cene where Komitent = " + Nalogodavac + " and Uvoznik = " + Uvoznik + " and ID = " + PomManipulacija, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {


                tmp = Convert.ToInt32(dr["Broj"].ToString());

            }

            con.Close();
            if (tmp > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        int ProveriDodatna(int ID)
        {
            int pomBZ = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            Komanda = "select Dodatna from VrstaManipulacije  where ID = ";


            SqlCommand cmd = new SqlCommand(Komanda + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomBZ = Convert.ToInt32(dr["Dodatna"].ToString());
            }
            con.Close();
            return pomBZ;

        }
        private void button12_Click(object sender, EventArgs e)
        {
            int CenaNadjenaTip1 = 0;
            int CenaNadjenaTip2 = 0;
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            int pomPlatilac = 0;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        int idMP = Convert.ToInt32(row.Cells[0].Value.ToString());
                        List<int> gv7 = new List<int>();
                        foreach (DataGridViewRow r in dataGridView7.Rows)
                        {
                            if (int.TryParse(r.Cells[4].Value.ToString(), out int value))
                            {
                                gv7.Add(value);
                            }
                        }
                        bool uneto = gv7.Contains(idMP);
                        if (uneto)
                        {
                            System.Windows.Forms.MessageBox.Show("Manipulacija ID:" + idMP + " je već dodata!");
                            return;
                        }
                        else
                        {
                            pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                            CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                            CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                            int Adm = ProveriAdministrativna(pomManupulacija);
                            int Dod = ProveriDodatna(pomManupulacija);
                            if (Adm == 1 || Dod == 1)
                            {
                                pomPokret = "/";
                                pomStatusKontejnera = Convert.ToInt32(0);
                                pomForma = "/";
                               

                            }
                            else
                            {
                                pomPokret = row.Cells[7].Value.ToString();
                                pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                                pomForma = row.Cells[10].Value.ToString();
                            }
                            if (CenaNadjenaTip1 == 1)
                            {
                                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                                SqlConnection con = new SqlConnection(s_connection);

                                con.Open();

                                SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManupulacija + " Uvoznik = " + cboUvoznik.SelectedValue + " and  Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "", con);
                                SqlDataReader dr = cmd.ExecuteReader();

                                while (dr.Read())
                                {

                                    pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                    pomkolicina = 1;


                                }

                                con.Close();
                                pomOrgJed = VratiOrgJed(pomManupulacija);
                                pomPlatilac = Convert.ToInt32(cboNalogodavac1.SelectedValue);
                            }

                            if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
                            {
                                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                                SqlConnection con = new SqlConnection(s_connection);

                                con.Open();

                                SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where Uvoznik = 0 and VrstaManipulacije = " + pomManupulacija + " and  Komitent = " + Convert.ToInt32(cboNalogodavac1.SelectedValue) + "", con);
                                SqlDataReader dr = cmd.ExecuteReader();

                                while (dr.Read())
                                {
                                    //Izmenjeno
                                    // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                    pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                    pomkolicina = 1;


                                }

                                con.Close();
                                pomPlatilac = Convert.ToInt32(cboNalogodavac1.SelectedValue);
                                pomOrgJed = VratiOrgJed(pomManupulacija);
                            }

                            if (CenaNadjenaTip2 == 0 && CenaNadjenaTip2 == 0)
                            {
                                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                                SqlConnection con = new SqlConnection(s_connection);

                                con.Open();

                                SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where TipCenovnika =1 and VrstaManipulacije = " + pomManupulacija, con);
                                SqlDataReader dr = cmd.ExecuteReader();

                                while (dr.Read())
                                {
                                    //Izmenjeno
                                    // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                    pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                    pomkolicina = 1;


                                }

                                con.Close();
                                pomPlatilac = Convert.ToInt32(cboNalogodavac1.SelectedValue);
                                pomOrgJed = VratiOrgJed(pomManupulacija);
                                foreach (DataGridViewRow row2 in dataGridView1.Rows)
                                {
                                    if (row2.Selected)
                                    {
                                        pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                        UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                                    }
                                }

                            }
                        }

                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());


                    }
                }

                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
                ProveriScenario(Convert.ToInt32(txtID.Text));
               
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Unos nije uspeo.Proverite da li imate definisanu cenu u Cenovniku!!!");
            }
        }
        int brojKontejnera;
        int rasporedjen = 0;
        private void ProveriScenario()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    brojKontejnera = Convert.ToInt32(row.Cells[0].Value);
                }
            }
            bool nasao = false;
            int count = dataGridView7.RowCount;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection conn = new SqlConnection(s_connection);

            List<int> usluge = new List<int>();
            foreach (DataGridViewRow row in dataGridView7.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    usluge.Add(Convert.ToInt32(row.Cells[4].Value));
                }
            }

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select ID From Scenario Group by ID Having Count(usluga)=" + count, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            List<int> scenarija = new List<int>();
            while (dr.Read())
            {
                scenarija.Add(dr.GetInt32(0));
            }
            conn.Close();

            foreach (int id in scenarija)
            {
                List<int> uslugeScenario = new List<int>();
                conn.Open();
                SqlCommand cmd2 = new SqlCommand("Select Usluga From Scenario Where ID=" + id, conn);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    uslugeScenario.Add(Convert.ToInt32(dr2[0]));
                }
                if (ProveriListe(usluge, uslugeScenario))
                {
                    nasao = true;
                  
                    if (txtNadredjeni.Text != "0")
                    {
                        rasporedjen = 1;
                    }
                    else
                    {
                        rasporedjen = 0;
                    }
                    InsertScenario isc = new InsertScenario();
                    isc.UpdScenarioKontejnera(id, brojKontejnera, 1, rasporedjen);
                    System.Windows.Forms.MessageBox.Show("Izabrali ste scenario " + id);
                }
                else
                {
                    uslugeScenario.Clear();
                }
                conn.Close();
            }
            if (nasao == false)
            {
                System.Windows.Forms.MessageBox.Show("Ne postoji takav scenario!");
            }
        }
        private bool ProveriListe(List<int> list1, List<int> list2)
        {
            var countMap1 = GetCountMap(list1);
            var countMap2 = GetCountMap(list2);

            // Check if both dictionaries have the same elements and counts
            if (countMap1.Count != countMap2.Count)
            {
                return false;
            }

            foreach (var kvp in countMap1)
            {
                if (!countMap2.TryGetValue(kvp.Key, out int count) || count != kvp.Value)
                {
                    return false;
                }
            }

            return true;
        }
        private Dictionary<int, int> GetCountMap(List<int> list)
        {
            var countMap = new Dictionary<int, int>();
            foreach (var item in list)
            {
                if (countMap.ContainsKey(item))
                {
                    countMap[item]++;
                }
                else
                {
                    countMap[item] = 1;
                }
            }
            return countMap;
        }
        private void FillGVUvozKonacnaPoPlanu()
        {
            var select = "    SELECT UvozKonacna.ID, BrojKontejnera,   CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet ,  CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) " +
          " ELSE Cast(0 as BIT) END as DobijenBZ ,TipKontenjera.Naziv as Vrsta_Kontejnera, DobijeBZ as DatumBZ ,  p1.PaNaziv as Uvoznik,   Brodovi.Naziv as Brod,n1.PaNaziv as Nalogodavac1, " +
          " Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,BrodskaTeretnica as BL,  " +
          " Napomena1 as Napomena1, PIN,    BrodskaTeretnica,KontejnerskiTerminali.Naziv as R_L_SRB,  VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, pv.PaNaziv as VlasnikKontejnera,  " +
          " pp1.Naziv as Dirigacija_Kontejnera_Za,                VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
          " VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,                         Carinarnice.Naziv as Carinarnica,   " +
          " p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, AdresaMestaUtovara, KontaktOsobe,  Email,  " +
          " BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta FROM UvozKonacna inner join Partnerji on PaSifra = VlasnikKontejnera " +
          " inner join Partnerji p1 on p1.PaSifra = Uvoznik  inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC  inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
          " inner join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe   inner join NHM on NHM.ID = NHMBroj  inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
          " inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina   inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
          " inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali   inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
          " inner join Partnerji n2 on n2.PaSifra = Nalogodavac2   inner join Partnerji n3 on n3.PaSifra = Nalogodavac3   inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
          " inner join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa     inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda    inner join VrstaRobeADR on VrstaRobeADR.ID = ADR     inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom   " +
          "  inner join MestaUtovara on UvozKOnacna.MestoIstovara = MestaUtovara.ID  " +
          " inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
          " inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera " +
            " where UvozKonacna.ID = " + pID + "  order by UvozKonacna.ID desc ";



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;
            /*
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;
            */
            RefreshDataGridColor();
            dataGridView1.SelectAll();

        }

        private void FillGVUvozKonacnaPoPlanuSVI()
        {
            var select = "    SELECT UvozKonacna.ID, BrojKontejnera,   CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet ,  CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) " +
           " ELSE Cast(0 as BIT) END as DobijenBZ ,TipKontenjera.Naziv as Vrsta_Kontejnera, DobijeBZ as DatumBZ ,  p1.PaNaziv as Uvoznik,   Brodovi.Naziv as Brod,n1.PaNaziv as Nalogodavac1, " +
           " Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,BrodskaTeretnica as BL,  " +
           " Napomena1 as Napomena1, PIN,    BrodskaTeretnica,KontejnerskiTerminali.Naziv as R_L_SRB,  VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, pv.PaNaziv as VlasnikKontejnera,  " +
           " pp1.Naziv as Dirigacija_Kontejnera_Za,                VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
           " VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,                         Carinarnice.Naziv as Carinarnica,   " +
           " p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, AdresaMestaUtovara, KontaktOsobe,  Email,  " +
           " BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta FROM UvozKonacna inner join Partnerji on PaSifra = VlasnikKontejnera " +
           " inner join Partnerji p1 on p1.PaSifra = Uvoznik  inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC  inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
           " inner join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe   inner join NHM on NHM.ID = NHMBroj  inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
           " inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina   inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak " +
           " inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali   inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
           " inner join Partnerji n2 on n2.PaSifra = Nalogodavac2   inner join Partnerji n3 on n3.PaSifra = Nalogodavac3   inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar " +
           " inner join DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa     inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda    inner join VrstaRobeADR on VrstaRobeADR.ID = ADR     inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom   " +
           "  inner join MestaUtovara on UvozKOnacna.MestoIstovara = MestaUtovara.ID  " +
           " inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
           " inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera " +
            " where UvozKonacna.IdNadredjeni = " + Convert.ToInt32(txtNadredjeni.Text) + "  order by UvozKonacna.ID desc ";



            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;
            /*
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;
            */
            RefreshDataGridColor();

        }

        private void FillGVUvoz()
        {
            /*
            var select = "SELECT Uvoz.ID, [BrojKontejnera], BrodskaTeretnica as BL, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda, Brodovi.Naziv as Brod,Napomena1 as Napomena1, DobijeBZ as DatumBZ ,PIN, " +
 " [BrojKontejnera], TipKontenjera.Naziv as Vrsta_Kontejnera,  KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
 "   BrodskaTeretnica, VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, " +
  "      p1.PaNaziv as Uvoznik,   " +
  "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM UvozVrstaManipulacije " +
   "       inner join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije   where UvozVrstaManipulacije.IDNadredjena = Uvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
   "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM UvozVrstaRobeHS " +
   "       inner join VrstaRobeHS on UvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where UvozVrstaRobeHS.IDNadredjena = Uvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
   "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
  "            FROM UvozNHM  inner join NHM on UvozNHM.IDNHM = NHM.ID  where UvozNHM.IDNadredjena = Uvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
   "              VrstaPregleda as VrstaPregleda,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,      " +
   " VrstaCarinskogPostupka.Naziv as CarinskiPostupak,   " +
   "                  VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,  " +
   "                      (Carinarnice.CINaziv + ' ' + Carinarnice.CIOznaka + ' ' + CIEmail + ' ' + CITelefon + ' / ' + p3.PaNaziv) as Carinarnica,   " +
   "                               p4.PaNaziv as OdredisnaSpedicija, MestoIstovara, KontaktOsoba, Email,        BrojPlombe1, BrojPlombe2,   " +
" PredefinisanePoruke.Naziv as NapomenaZaPozicioniranje,  " +
 " NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, " +
 " Koleta, green " +
 " FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
 " inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
 " inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
 "  inner join VrstaRobeHS on VrstaRobeHS.ID = Uvoz.NazivRobe " +
"  inner join NHM on NHM.ID = NHMBroj " +
 " inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
 "  inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
 "  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
 " inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
 "  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
 "  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
 "  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
 "  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
 "  inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
  " inner join PredefinisanePoruke pp1 on pp1.ID = DirigacijaKontejeraZa   " +
 "  inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
                              "   inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
                              "    inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom    inner join uvNacinPakovanja " +
 " on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija  order by Uvoz.ID desc ";
            */

            var select = "SELECT Uvoz.ID, [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,  DobijenBZ, Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz Left join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
" where Uvoz.ID = " + pID + " order by Uvoz.ID desc ";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;
            /*
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;
            */
            RefreshDataGridColor();
            dataGridView1.SelectAll();



        }

        private void FillGVUvozSVI()
        {
            /*
            var select = "SELECT Uvoz.ID, [BrojKontejnera], BrodskaTeretnica as BL, DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda, Brodovi.Naziv as Brod,Napomena1 as Napomena1, DobijeBZ as DatumBZ ,PIN, " +
 " [BrojKontejnera], TipKontenjera.Naziv as Vrsta_Kontejnera,  KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,  " +
 "   BrodskaTeretnica, VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar, n1.PaNaziv as Nalogodavac1, Ref1 as Ref1, n2.PaNaziv as Nalogodavac2, Ref2 as Ref2, n3.PaNaziv as Nalogodavac3, Ref3 as Ref3, " +
  "      p1.PaNaziv as Uvoznik,   " +
  "  (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))   FROM UvozVrstaManipulacije " +
   "       inner join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije   where UvozVrstaManipulacije.IDNadredjena = Uvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga,   " +
   "     (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaRobeHS.HSKod as nvarchar(20))   FROM UvozVrstaRobeHS " +
   "       inner join VrstaRobeHS on UvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID   where UvozVrstaRobeHS.IDNadredjena = Uvoz.ID " +
   "        FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,   " +
   "    (SELECT  STUFF((SELECT distinct    '/' + Cast(NHM.Broj as nvarchar(20)) " +
  "            FROM UvozNHM  inner join NHM on UvozNHM.IDNHM = NHM.ID  where UvozNHM.IDNadredjena = Uvoz.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
   "              VrstaPregleda as VrstaPregleda,p2.PaNaziv as SpedicijaRTC,  p3.PaNaziv as SpedicijaGranica,      " +
   " VrstaCarinskogPostupka.Naziv as CarinskiPostupak,   " +
   "                  VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, Napomena as Napomena2,  " +
   "                      (Carinarnice.CINaziv + ' ' + Carinarnice.CIOznaka + ' ' + CIEmail + ' ' + CITelefon + ' / ' + p3.PaNaziv) as Carinarnica,   " +
   "                               p4.PaNaziv as OdredisnaSpedicija, MestoIstovara, KontaktOsoba, Email,        BrojPlombe1, BrojPlombe2,   " +
" PredefinisanePoruke.Naziv as NapomenaZaPozicioniranje,  " +
 " NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera, " +
 " Koleta, green " +
 " FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
 " inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
 " inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
 "  inner join VrstaRobeHS on VrstaRobeHS.ID = Uvoz.NazivRobe " +
"  inner join NHM on NHM.ID = NHMBroj " +
 " inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
 "  inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
 "  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
 " inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
 "  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
 "  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
 "  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
 "  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
 "  inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
  " inner join PredefinisanePoruke pp1 on pp1.ID = DirigacijaKontejeraZa   " +
 "  inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
                              "   inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
                              "    inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom    inner join uvNacinPakovanja " +
 " on uvNacinPakovanja.ID = NacinPakovanja  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija  order by Uvoz.ID desc ";
            */

            var select = "SELECT Uvoz.ID, [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,  DobijenBZ, Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz Left join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
"  order by Uvoz.ID desc ";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "BrojKontejnera";
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].Width = 100;
            /*
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "BL";
            dataGridView1.Columns[2].Frozen = true;
            dataGridView1.Columns[2].Width = 90;

            DataGridViewColumn column3 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Dobijen_Nalog_Brodara";
            // dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[3].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "ATABroda";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column5 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Brod";
            dataGridView1.Columns[5].Width = 100;

            DataGridViewColumn column6 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Napomena";
            dataGridView1.Columns[6].Width = 100;

            DataGridViewColumn column7 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "DatumBZ";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PIN";
            dataGridView1.Columns[8].Width = 60;

            DataGridViewColumn column9 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "BrojKontejnera";
            //   dataGridView1.Columns[7].Frozen = true;
            dataGridView1.Columns[9].Width = 100;

            DataGridViewColumn column10 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "Vrsta kontejnera";
            dataGridView1.Columns[10].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[11];
            dataGridView1.Columns[11].HeaderText = "R_L_SRB";
            dataGridView1.Columns[11].Width = 120;

            DataGridViewColumn column12 = dataGridView1.Columns[12];
            dataGridView1.Columns[12].HeaderText = "Dirigacija_Kontejnera_Za";
            dataGridView1.Columns[12].Width = 100;

            DataGridViewColumn column13 = dataGridView1.Columns[13];
            dataGridView1.Columns[13].HeaderText = "BL";
            // dataGridView1.Columns[13].Frozen = true;
            dataGridView1.Columns[13].Width = 90;
            */
            RefreshDataGridColor();
            dataGridView1.SelectAll();

        }

        private void RefreshDataGridColor()
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {


                if (row.Cells[25].Value.ToString() == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.SelectionBackColor = Color.Green;
                }
                else
                {

                }
            }

            dataGridView1.Refresh();

        }
        private void frmUnosManipulacija_Load(object sender, EventArgs e)
        {
            FillCombo();

            txtNadredjeni.Text = pIDPlana.ToString();
            txtID.Text = pID.ToString();
            cboNalogodavac1.SelectedValue = pNalogodavac1;
            cboNalogodavac2.SelectedValue = pNalogodavac2;
            cboNalogodavac3.SelectedValue = pNalogodavac3;
            cboUvoznik.SelectedValue = pUvoznik;
            if (txtNadredjeni.Text == "0")
            {
                FillGVUvoz();
            }
            else
            {
                FillGVUvozKonacnaPoPlanu();

            }
            Usao = 1;
        }

        private void FillCombo()
        {
            
            SqlConnection conn = new SqlConnection(connection);

            var partner8 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD8 = new SqlDataAdapter(partner8, conn);
            var partDS8 = new DataSet();
            partAD8.Fill(partDS8);
            cboNalogodavac1.DataSource = partDS8.Tables[0];
            cboNalogodavac1.DisplayMember = "PaNaziv";
            cboNalogodavac1.ValueMember = "PaSifra";

            var partner9 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD9 = new SqlDataAdapter(partner9, conn);
            var partDS9 = new DataSet();
            partAD9.Fill(partDS9);
            cboNalogodavac2.DataSource = partDS9.Tables[0];
            cboNalogodavac2.DisplayMember = "PaNaziv";
            cboNalogodavac2.ValueMember = "PaSifra";

            var partner10 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD10 = new SqlDataAdapter(partner10, conn);
            var partDS10 = new DataSet();
            partAD9.Fill(partDS10);
            cboNalogodavac3.DataSource = partDS10.Tables[0];
            cboNalogodavac3.DisplayMember = "PaNaziv";
            cboNalogodavac3.ValueMember = "PaSifra";

            var partner2 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";
            //Provera SCENARIJA UKLJUCITI ADR
            /*
          
         
         
            else if (ScenarioGl == 2 && Convert.ToInt32(txtADR.SelectedValue) > 1 && pp == 1)
            {
                Moguce = "20,21,31";

            }
            else if (ScenarioGl == 3 && Convert.ToInt32(txtADR.SelectedValue) == 0 && pp == 1)
            {
                Moguce = "5"; // Ostaje na terminalu
            }
            else if (ScenarioGl == 3 && Convert.ToInt32(txtADR.SelectedValue) > 1 && pp == 1)
            {
                Moguce = "22"; // Ostaje na terminalu
            }

            */



            if (terminal == 1)
            {
                var partner22 = "SELECT ID, Min(Naziv) as Naziv FROM Scenario Where OJIzdavanje=4 group by ID order by ID";
                var partAD22 = new SqlDataAdapter(partner22, conn);
                var partDS22 = new DataSet();
                partAD22.Fill(partDS22);
                cboScenario.DataSource = partDS22.Tables[0];
                cboScenario.DisplayMember = "Naziv";
                cboScenario.ValueMember = "ID";
            }
            else if (ScenarioGL == 1  && ADRSC == 0 && PunPrazan > 0 )
            {
                var partner22 = " SELECT ID, Min(Naziv) as Naziv FROM Scenario Where ID in (1,2,27) group by ID order by ID";
                var partAD22 = new SqlDataAdapter(partner22, conn);
                var partDS22 = new DataSet();
                partAD22.Fill(partDS22);
                cboScenario.DataSource = partDS22.Tables[0];
                cboScenario.DisplayMember = "Naziv";
                cboScenario.ValueMember = "ID";
            }
            else if (ScenarioGL == 1  && ADRSC == 1 && PunPrazan > 0)
            {
                var partner22 = " SELECT ID, Min(Naziv) as Naziv FROM Scenario Where ID in (18,19,30) group by ID order by ID";
                var partAD22 = new SqlDataAdapter(partner22, conn);
                var partDS22 = new DataSet();
                partAD22.Fill(partDS22);
                cboScenario.DataSource = partDS22.Tables[0];
                cboScenario.DisplayMember = "Naziv";
                cboScenario.ValueMember = "ID";
            }

            else if (ScenarioGL == 2 && ADRSC == 0 && PunPrazan > 0)
            {
                var partner22 = " SELECT ID, Min(Naziv) as Naziv FROM Scenario Where ID in (3,4,28) group by ID order by ID";
                var partAD22 = new SqlDataAdapter(partner22, conn);
                var partDS22 = new DataSet();
                partAD22.Fill(partDS22);
                cboScenario.DataSource = partDS22.Tables[0];
                cboScenario.DisplayMember = "Naziv";
                cboScenario.ValueMember = "ID";


            }
            else if (ScenarioGL == 2 && ADRSC == 0 && PunPrazan == 0)
            {
                var partner22 = " SELECT ID, Min(Naziv) as Naziv FROM Scenario Where ID in (6) group by ID order by ID";
                var partAD22 = new SqlDataAdapter(partner22, conn);
                var partDS22 = new DataSet();
                partAD22.Fill(partDS22);
                cboScenario.DataSource = partDS22.Tables[0];
                cboScenario.DisplayMember = "Naziv";
                cboScenario.ValueMember = "ID";
            }
            else if (ScenarioGL == 2 && ADRSC == 1 && PunPrazan > 0)
            {
                var partner22 = " SELECT ID, Min(Naziv) as Naziv FROM Scenario Where ID in (20,21,31) group by ID order by ID";
                var partAD22 = new SqlDataAdapter(partner22, conn);
                var partDS22 = new DataSet();
                partAD22.Fill(partDS22);
                cboScenario.DataSource = partDS22.Tables[0];
                cboScenario.DisplayMember = "Naziv";
                cboScenario.ValueMember = "ID";
            }

        
            else if (ScenarioGL == 3  && ADRSC == 0 && PunPrazan > 0)
            {
                var partner22 = " SELECT ID, Min(Naziv) as Naziv FROM Scenario Where ID in (5) group by ID order by ID";
                var partAD22 = new SqlDataAdapter(partner22, conn);
                var partDS22 = new DataSet();
                partAD22.Fill(partDS22);
                cboScenario.DataSource = partDS22.Tables[0];
                cboScenario.DisplayMember = "Naziv";
                cboScenario.ValueMember = "ID";
            }
            else if (ScenarioGL == 3 && ADRSC == 1 && PunPrazan > 0)
            {
                var partner22 = " SELECT ID, Min(Naziv) as Naziv FROM Scenario Where ID in (22) group by ID order by ID";
                var partAD22 = new SqlDataAdapter(partner22, conn);
                var partDS22 = new DataSet();
                partAD22.Fill(partDS22);
                cboScenario.DataSource = partDS22.Tables[0];
                cboScenario.DisplayMember = "Naziv";
                cboScenario.ValueMember = "ID";
            }


            var select3 = " SELECT ID, Naziv from GrupaVrsteManipulacije order by ID";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboGrupaVrsteManipulacije.DataSource = ds3.Tables[0];
            cboGrupaVrsteManipulacije.DisplayMember = "Naziv";
            cboGrupaVrsteManipulacije.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillDGN2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FillDGN3();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    FillDG8();
                    FillDG8Dodatne();
                    FillDG8Administracija();
                    //  VratiPodatkeSelect(Convert.ToInt32(txtID.Text));

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillDN1BU();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillDGN2BU();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FillDGN3BU();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int CenaNadjenaTip1 = 0;
            int CenaNadjenaTip2 = 0;
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            int pomPlatilac = 0;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac2.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        int Adm = ProveriAdministrativna(pomManupulacija);

                        int Dod = ProveriDodatna(pomManupulacija);
                        if (Adm == 1 || Dod == 1)
                        {
                            pomPokret = "/";
                            pomStatusKontejnera = Convert.ToInt32(0);
                            pomForma = "/";
                        }
                        else
                        {
                            pomPokret = row.Cells[7].Value.ToString();
                            pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                            pomForma = row.Cells[10].Value.ToString();
                        }
                        if (CenaNadjenaTip1 == 1)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManupulacija + " Uvoznik = " + cboUvoznik.SelectedValue + " and  Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {

                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac2.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                        }

                        if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where Uvoznik = 0 and VrstaManipulacije = " + pomManupulacija + " and  Komitent = " + Convert.ToInt32(cboNalogodavac2.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac2.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                        }

                        if (CenaNadjenaTip2 == 0 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where TipCenovnika =1 and VrstaManipulacije = " + pomManupulacija, con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac2.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                            foreach (DataGridViewRow row2 in dataGridView1.Rows)
                            {
                                if (row2.Selected)
                                {
                                    pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                    UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                                }
                            }
                        }

                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                       


                    }
                }
                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
                ProveriScenario(Convert.ToInt32(txtID.Text));

            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Unos nije uspeo.Proverite da li imate definisanu cenu u Cenovniku!!!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int CenaNadjenaTip1 = 0;
            int CenaNadjenaTip2 = 0;
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            int pomPlatilac = 0;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac3.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                        int Adm = ProveriAdministrativna(pomManupulacija);
                        int Dod = ProveriDodatna(pomManupulacija);
                        if (Adm == 1 || Dod == 1)
                        {
                            pomPokret = "/";
                            pomStatusKontejnera = Convert.ToInt32(0);
                            pomForma = "/";
                        }
                        else
                        {
                            pomPokret = row.Cells[7].Value.ToString();
                            pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                            pomForma = row.Cells[10].Value.ToString();
                        }
                        if (CenaNadjenaTip1 == 1)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManupulacija + " Uvoznik = " + cboUvoznik.SelectedValue + " and  Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {

                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac3.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                        }

                        if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where Uvoznik = 0 and VrstaManipulacije = " + pomManupulacija + " and  Komitent = " + Convert.ToInt32(cboNalogodavac3.SelectedValue) + "", con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac3.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                        }

                        if (CenaNadjenaTip2 == 0 && CenaNadjenaTip2 == 0)
                        {
                            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                            SqlConnection con = new SqlConnection(s_connection);

                            con.Open();

                            SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where TipCenovnika =1 and VrstaManipulacije = " + pomManupulacija, con);
                            SqlDataReader dr = cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                //Izmenjeno
                                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                                pomCena = Convert.ToDouble(dr["Cena"].ToString());
                                pomkolicina = 1;


                            }

                            con.Close();
                            pomPlatilac = Convert.ToInt32(cboNalogodavac3.SelectedValue);
                            pomOrgJed = VratiOrgJed(pomManupulacija);
                            foreach (DataGridViewRow row2 in dataGridView1.Rows)
                            {
                                if (row2.Selected)
                                {
                                    pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                    UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                                }
                            }
                        }

                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                       


                    }
                }
                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
                ProveriScenario(Convert.ToInt32(txtID.Text));

            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Unos nije uspeo.Proverite da li imate definisanu cenu u Cenovniku!!!");
            }
        }

        int ProveriDaliPostojiIzdatRNI(int pom)
        {
            int Odgovor = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

                Komanda = "select count(*) as Broj from RadniNalogInterni where OJIzdavanja = 1 and KonketaIDUsluge = " + pom;
          

            SqlCommand cmd = new SqlCommand(Komanda, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                if (Convert.ToInt32(dr["Broj"].ToString()) == 1)
                {
                    Odgovor = 1;
                }
            }
            con.Close();

            //Proveri da li je već izdat radni nalog

            con.Open();

            Komanda = "select count(*) as Broj from RadniNalogInterni where BrojRN <> 0 and KonketaIDUsluge = " + pom;


            SqlCommand cmd2 = new SqlCommand(Komanda, con);
            SqlDataReader dr2 = cmd.ExecuteReader();

            while (dr2.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                if (Convert.ToInt32(dr2["Broj"].ToString()) == 1)
                {
                    Odgovor = 2;
                }
            }
            con.Close();

            // Proveri da li je izdat dokument prijema
            con.Open();

            Komanda = " select count(*) as Broj from RadniNalogInterni where BrojDokPrevoza <> 0 and KonketaIDUsluge = " + pom;


            SqlCommand cmd3 = new SqlCommand(Komanda, con);
            SqlDataReader dr3 = cmd.ExecuteReader();

            while (dr3.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                if (Convert.ToInt32(dr3["Broj"].ToString()) == 1)
                {
                    Odgovor = 2;
                }
            }
            con.Close();

           

            //  select count(*) from RadniNalogInterni where OJIzdavanja = 1 and KonketaIDUsluge =
            return Odgovor;


        }

        private void button8_Click(object sender, EventArgs e)
        {
            int pom = 0;
            InsertUvozKonacna uvK = new InsertUvozKonacna();

            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                                                                  //Ovde treba proveriti da li za manipulacije postoji izdat interni radni nalog
                                                                                  // Da li je za radni nalog izdata Prijemnica i RN
                            int i = ProveriDaliPostojiIzdatRNI(pom);

                            if (i == 0)
                            {
                                uvK.DelUvozKonacnaUsluga(pom);
                                ProveriScenario(Convert.ToInt32(txtID.Text));


                            }
                            else if (i == 1)
                            {
                                System.Windows.Forms.MessageBox.Show("Za ovu uslugu generisali ste nalog Terminalu ali nije dat u dalju izradu. Kontektiraje terminal");
                            }
                            else if (i == 2)
                            {
                                System.Windows.Forms.MessageBox.Show("Za ovu uslugu generisali ste nalog Terminalu dat je u dalju izradu. Kontektirajte terminal");

                            }
                          
                          
                        
                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.DelUvozUsluga(pom);
                            ProveriScenario(Convert.ToInt32(txtID.Text));
                        }
                    }
                }
                foreach (DataGridViewRow row2 in dataGridView2.Rows)
                {
                    if (row2.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                                                                  //Ovde treba proveriti da li za manipulacije postoji izdat interni radni nalog
                                                                                  // Da li je za radni nalog izdata Prijemnica i RN
                            int i = ProveriDaliPostojiIzdatRNI(pom);

                            if (i == 0)
                            {
                                uvK.DelUvozKonacnaUsluga(pom);

                            }
                            else if (i == 1)
                            {
                                System.Windows.Forms.MessageBox.Show("Za ovu uslugu generisali ste nalog Terminalu ali nije dat u dalju izradu. Kontektiraje terminal");
                            }
                            else if (i == 2)
                            {
                                System.Windows.Forms.MessageBox.Show("Za ovu uslugu generisali ste nalog Terminalu dat je u dalju izradu. Kontektirajte terminal");

                            }



                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.DelUvozUsluga(pom);
                        }
                    }
                }

                foreach (DataGridViewRow row3 in dataGridView3.Rows)
                {
                    if (row3.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                                                                                  //Ovde treba proveriti da li za manipulacije postoji izdat interni radni nalog
                                                                                  // Da li je za radni nalog izdata Prijemnica i RN
                            int i = ProveriDaliPostojiIzdatRNI(pom);

                            if (i == 0)
                            {
                                uvK.DelUvozKonacnaUsluga(pom);

                            }
                            else if (i == 1)
                            {
                                System.Windows.Forms.MessageBox.Show("Za ovu uslugu generisali ste nalog Terminalu ali nije dat u dalju izradu. Kontektiraje terminal");
                            }
                            else if (i == 2)
                            {
                                System.Windows.Forms.MessageBox.Show("Za ovu uslugu generisali ste nalog Terminalu dat je u dalju izradu. Kontektirajte terminal");

                            }



                        }
                        else
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                            uvK.DelUvozUsluga(pom);
                        }
                    }
                }


                FillDG8();
              
                FillDG8Dodatne();
                 
                FillDG8Administracija();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Nije uspelo brisanje");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int pom = 0;
            InsertUvozKonacna uvK = new InsertUvozKonacna();

            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
                        }
                    }
                }


                foreach (DataGridViewRow row3 in dataGridView2.Rows)
                {
                    if (row3.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
                        }
                    }
                }

                foreach (DataGridViewRow row4 in dataGridView3.Rows)
                {
                    if (row4.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row4.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row4.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboUvoznik.SelectedValue));
                        }
                    }
                }

                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Nije uspelo brisanje");
            }
        }

        int ProveriAdministrativna(int ID)
        {
            int pomBZ = 0;
            string Komanda = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
          
            Komanda = "select Administrativna from VrstaManipulacije  where ID = ";
           

            SqlCommand cmd = new SqlCommand(Komanda + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomBZ = Convert.ToInt32(dr["Administrativna"].ToString());
            }
            con.Close();
            return pomBZ;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            int pomPlatilac = 0;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected == true)
                    {
                        pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                        int Adm = ProveriAdministrativna(pomManupulacija);
                        int Dod = ProveriDodatna(pomManupulacija);
                        if (Adm == 1 || Dod == 1)
                        {
                            pomPokret = "/";
                            pomStatusKontejnera = Convert.ToInt32(0);
                            pomForma = "/";
                        }
                        else
                        {
                            pomPokret = row.Cells[7].Value.ToString();
                            pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                            pomForma = row.Cells[10].Value.ToString();
                        }
                   
                        var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
                        SqlConnection con = new SqlConnection(s_connection);

                        con.Open();

                        SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where TipCenovnika =1 and VrstaManipulacije = " + pomManupulacija, con);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            //Izmenjeno
                            // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                            pomCena = Convert.ToDouble(dr["Cena"].ToString());
                            pomkolicina = 1;
                            // pomOrgJed = Convert.ToInt32(dr["OrgJed"].ToString());

                            // Nece se analizirati podrazumevano da nije cekirano
                            // pomSaPDV = Convert.ToInt32(dr["SaPDV"].ToString());
                        }

                        con.Close();

                        pomOrgJed = VratiOrgJed(pomManupulacija);
                        pomPlatilac = Convert.ToInt32(cboUvoznik.SelectedValue);



                        //  pomCena = Convert.ToDouble(row.Cells[2].Value.ToString());
                        // pomkolicina= Convert.ToDouble(row.Cells[4].Value.ToString());
                        //   pomOrgJed = Convert.ToInt32(row.Cells[5].Value.ToString());
                        foreach (DataGridViewRow row2 in dataGridView1.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                UbaciStavkuUsluge(pomID, pomManupulacija, pomCena, pomkolicina, pomOrgJed, pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                            }
                        }


                    }
                    
                }
                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
                ProveriScenario(Convert.ToInt32(txtID.Text));


            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Unos nije uspeo.Proverite da li imate definisanu cenu u Cenovniku!!!");
            }
        }
     

        int VratiOrgJed(int Manipulacija)
        {
            int pomOJ = 0;
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select OrgJed from VrstaManipulacije  where ID= " + Manipulacija, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomOJ = Convert.ToInt32(dr["OrgJed"].ToString());
            }
            con.Close();
            return pomOJ;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            FillDG6(1);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FillDG6(0);
        }

        private void dataGridView7_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (Usao == 1)
            {

                int columnIndex = dataGridView7.CurrentCell.ColumnIndex;
                int rowIndex = dataGridView7.CurrentCell.RowIndex;



                if (columnIndex == 6)
                {
                    string value = dataGridView7.Rows[rowIndex].Cells[0].EditedFormattedValue.ToString();
                    string value1 = dataGridView7.Rows[rowIndex].Cells[6].EditedFormattedValue.ToString();
                    string value2 = dataGridView7.Rows[rowIndex].Cells[6].Value.ToString();
                    if (txtNadredjeni.Text != "0")
                    {
                        InsertUvoz uvK = new InsertUvoz();
                        uvK.UpdCENAUvozKonacnaUsluga(Convert.ToInt32(value), Convert.ToDouble(value1));
                        // FillDG8();
                    }
                    else
                    {
                        InsertUvoz uvK = new InsertUvoz();
                        uvK.UpdCENAUvozUsluga(Convert.ToInt32(value), Convert.ToDouble(value1));
                        //FillDG8();

                    }


                }

            }
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Usao == 1)
            {
                int columnIndex = dataGridView7.CurrentCell.ColumnIndex;
                int rowIndex = dataGridView7.CurrentCell.RowIndex;
                int tmp = 0;

                if (columnIndex == 11)
                {
                    string value = dataGridView7.Rows[rowIndex].Cells[0].Value.ToString();
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView7.Rows[rowIndex].Cells[11];
                    if (chk.EditedFormattedValue.ToString() == "True")
                    {
                        chk.Selected = false;
                        tmp = 0;
                        //Update PDV
                    }
                    else
                    {
                        chk.Selected = true;
                        tmp = 1;
                        //Update PDV
                    }

                    if (txtNadredjeni.Text != "0")
                    {
                        InsertUvoz uvK = new InsertUvoz();
                        uvK.UpdPDVUvozKonacnaUsluga(Convert.ToInt32(value), tmp);
                        // FillDG8();
                    }
                    else
                    {
                        InsertUvoz uvK = new InsertUvoz();
                        uvK.UpdPDVUvozUsluga(Convert.ToInt32(value), tmp);
                        // FillDG8();

                    }

                    // string value1 = dataGridView7.Rows[rowIndex].Cells[6].EditedFormattedValue.ToString();
                    // string value2 = dataGridView7.Rows[rowIndex].Cells[6].Value.ToString();
                    //Promeni cenu
                }


            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (txtNadredjeni.Text == "0")
            {
                FillGVUvozSVI();
            }
            else
            {
                FillGVUvozKonacnaPoPlanuSVI();

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (txtNadredjeni.Text == "0")
            {
                FillGVUvoz();
            }
            else
            {
                FillGVUvozKonacnaPoPlanu();

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            FillDG6Scenario();
            dataGridView6.SelectAll();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            int UslugaKamion = 0;
            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        UslugaKamion = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta

                    }
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Nije uspelo brisanje");
            }


            frmVoziloUsluga vu = new frmVoziloUsluga(UslugaKamion, 0);
            vu.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FillDG6(2); // Po grupi manipulacije
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FillDG6(3);
        }

        private void frmUnosManipulacija_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        decimal VratiCenu(int IDVM, int TipoPlatioc)
        {
            int CenaNadjenaTip1 = 0;
            int CenaNadjenaTip2 = 0;
            decimal pomCena = 0;
            int pomPlatioc = 0;
            int pomManipulacija = 0;

            pomManipulacija = VratiManipulaciju(IDVM);
    

            if (TipoPlatioc == 1)
            {
                pomPlatioc = Convert.ToInt32(cboNalogodavac1.SelectedValue);


            }
            else if (TipoPlatioc == 2)
            {
                pomPlatioc = Convert.ToInt32(cboNalogodavac2.SelectedValue);
            }

            else if (TipoPlatioc == 3)
            {
                pomPlatioc = Convert.ToInt32(cboNalogodavac3.SelectedValue);
            }
            else if (TipoPlatioc == 4)
            {
                pomPlatioc = Convert.ToInt32(cboUvoznik.SelectedValue);
            }

            CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManipulacija, pomPlatioc, Convert.ToInt32(cboUvoznik.SelectedValue));
            CenaNadjenaTip2 = PretraziPoNalogodavci(pomManipulacija, pomPlatioc, Convert.ToInt32(cboUvoznik.SelectedValue));
            if (CenaNadjenaTip1 == 1)
            {
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where VrstaManipulacije = " + pomManipulacija + "  and  Komitent = " + pomPlatioc+ "", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    pomCena = Convert.ToDecimal(dr["Cena"].ToString());



                }

                con.Close();

            }

            if (CenaNadjenaTip2 == 1 && CenaNadjenaTip2 == 0)
            {
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("select ID, Cena, OrgJed from Cene where Uvoznik = 0 and VrstaManipulacije = " + pomManipulacija + " and  Komitent = " + pomPlatioc + "", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    //Izmenjeno
                    // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                    pomCena = Convert.ToDecimal(dr["Cena"].ToString());



                }

                con.Close();
               

            }
            return pomCena;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int pom = 0;
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            decimal cena = 0;
            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboNalogodavac3.SelectedValue));
                            cena = VratiCenu(pom, 3);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboNalogodavac3.SelectedValue));
                            cena = VratiCenu(pom, 3);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                    }
                }


                foreach (DataGridViewRow row3 in dataGridView2.Rows)
                {
                    if (row3.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboNalogodavac3.SelectedValue));
                            cena = VratiCenu(pom, 3);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboNalogodavac3.SelectedValue));
                            cena = VratiCenu(pom, 3);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                    }
                }

                foreach (DataGridViewRow row4 in dataGridView2.Rows)
                {
                    if (row4.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row4.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboNalogodavac3.SelectedValue));
                            cena = VratiCenu(pom, 3);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row4.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboNalogodavac3.SelectedValue));
                            cena = VratiCenu(pom, 3);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));

                        }
                    }
                }

                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Nije uspelo brisanje");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            int pom = 0;
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            decimal cena = 0;
            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboNalogodavac2.SelectedValue));
                            cena = VratiCenu(pom, 2);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboNalogodavac2.SelectedValue));
                            cena = VratiCenu(pom, 2);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                    }
                }


                foreach (DataGridViewRow row3 in dataGridView2.Rows)
                {
                    if (row3.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboNalogodavac2.SelectedValue));
                            cena = VratiCenu(pom, 2);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboNalogodavac2.SelectedValue));
                            cena = VratiCenu(pom, 2);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                    }
                }

                foreach (DataGridViewRow row4 in dataGridView3.Rows)
                {
                    if (row4.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row4.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboNalogodavac2.SelectedValue));
                            cena = VratiCenu(pom, 3);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row4.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboNalogodavac2.SelectedValue));
                            cena = VratiCenu(pom, 3);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                    }
                }

                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Nije uspelo brisanje");
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            int pom = 0;
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            decimal cena = 0;
            try
            {

                foreach (DataGridViewRow row2 in dataGridView7.Rows)
                {
                    if (row2.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboNalogodavac1.SelectedValue));
                            cena = VratiCenu(pom, 1);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboNalogodavac1.SelectedValue));
                            cena = VratiCenu(pom, 1);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                    }
                }


                foreach (DataGridViewRow row3 in dataGridView2.Rows)
                {
                    if (row3.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboNalogodavac1.SelectedValue));
                            cena = VratiCenu(pom, 1);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row3.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboNalogodavac1.SelectedValue));
                            cena = VratiCenu(pom, 1);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                    }
                }

                foreach (DataGridViewRow row4 in dataGridView2.Rows)
                {
                    if (row4.Selected)
                    {
                        if (txtNadredjeni.Text != "0")
                        {
                            pom = Convert.ToInt32(row4.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozKonacnaUsluga(pom, Convert.ToInt32(cboNalogodavac1.SelectedValue));
                            cena = VratiCenu(pom, 1);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                        else
                        {
                            pom = Convert.ToInt32(row4.Cells[0].Value.ToString());//Panta
                            uvK.UpdPlatiocaUvozUsluga(pom, Convert.ToInt32(cboNalogodavac1.SelectedValue));
                            cena = VratiCenu(pom, 1);
                            uvK.UpdCenaUvozKonacnaUsluga(pom, Convert.ToDecimal(cena));
                        }
                    }
                }

                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Nije uspelo brisanje");
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
     
            int pomID = 0;
            int pomManupulacija = 0;
            double pomkolicina = 1;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            string pomForma = "";
            try
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Selected)
                    {
                        int idMP = Convert.ToInt32(row.Cells[0].Value.ToString());
                        List<int> gv7 = new List<int>();
                        foreach (DataGridViewRow r in dataGridView7.Rows)
                        {
                            if (int.TryParse(r.Cells[4].Value.ToString(), out int value))
                            {
                                gv7.Add(value);
                            }
                        }
                        bool uneto = gv7.Contains(idMP);
                        if (uneto)
                        {
                            System.Windows.Forms.MessageBox.Show("Manipulacija ID:" + idMP + " je već dodata!");
                            return;
                        }
                        else
                        {
                            pomManupulacija = Convert.ToInt32(row.Cells[0].Value.ToString());
                           // CenaNadjenaTip1 = PretraziPoNalogodavciIIzvozniku(pomManupulacija, Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                           // CenaNadjenaTip2 = PretraziPoNalogodavci(pomManupulacija, Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboUvoznik.SelectedValue));
                            int Adm = ProveriAdministrativna(pomManupulacija);
                            int Dod = ProveriDodatna(pomManupulacija);
                            if (Adm == 1 || Dod == 1)
                            {
                                pomPokret = "/";
                                pomStatusKontejnera = Convert.ToInt32(0);
                                pomForma = "/";
                            }
                            else
                            {
                                pomPokret = row.Cells[7].Value.ToString();
                                pomStatusKontejnera = Convert.ToInt32(row.Cells[8].Value.ToString());
                                pomForma = row.Cells[10].Value.ToString();
                            }
                                pomOrgJed = VratiOrgJed(pomManupulacija);
                                foreach (DataGridViewRow row2 in dataGridView1.Rows)
                                {
                                    if (row2.Selected)
                                    {
                                        pomID = Convert.ToInt32(row2.Cells[0].Value.ToString());//Panta
                                        UbaciStavkuUsluge(pomID, pomManupulacija, 0, pomkolicina, pomOrgJed, 0, pomPokret, pomStatusKontejnera, pomForma);
                                    }
                                }

                           
                        }
                    }
                }

                FillDG8();
                FillDG8Dodatne();
                FillDG8Administracija();
                ProveriScenario(Convert.ToInt32(txtID.Text));
                //ProveriScenario(pomID);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Unos nije uspeo.Proverite da li imate definisanu cenu u Cenovniku!!!");
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (txtNadredjeni.Text == "0")
            {
                frmUvozKopirajUslugeKontejnera kuk = new frmUvozKopirajUslugeKontejnera(txtID.Text, 1);
                kuk.Show();
            }
            else
            {
                frmUvozKopirajUslugeKontejnera kuk = new frmUvozKopirajUslugeKontejnera(txtID.Text, 2);
                kuk.Show();

            }
           
        }
    }
}
