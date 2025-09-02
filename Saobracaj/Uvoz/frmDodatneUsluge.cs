using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Grid;
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
using GMap.NET.MapProviders;
using Saobracaj.Izvoz;
using Syncfusion.Windows.Forms.Tools;
using Saobracaj.Dokumenta;
using System.Runtime.ConstrainedExecution;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Syncfusion.Windows.Forms;
using System.Drawing.Imaging;

namespace Saobracaj.Uvoz
{
    public partial class frmDodatneUsluge : Form
    {
        int usao = 1;
        int IzPomForme = 0;
        string OsnovPF = "";
        int OJPF = 0;

        private void ChangeTextBox()
        {
           

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
             
            {
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
                //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);

                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                // toolStripHeader.Visible = false;

                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                splitContainer1.Panel1.BackColor = Color.White;
                splitContainer1.Panel2.BackColor = Color.White;
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
                }


                foreach (Control control in splitContainer1.Panel1.Controls)
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
             
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        public frmDodatneUsluge()
        {
            InitializeComponent();
            ChangeTextBox();
        }


        public frmDodatneUsluge(string Osnov, int OJ)
        {
            InitializeComponent();
            IzPomForme = 1;
            OJPF = OJ;
            OsnovPF = Osnov;
            ChangeTextBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string DodatniAND = " AND 1=1 ";
            /*
            if (chkVOZ.Checked == true)
            {
                DodatniAND = DodatniAND + " AND VrstaManipulacije.Vozna = 1 ";
            }

            if (chkKamion.Checked == true)
            {
                DodatniAND = DodatniAND + " AND VrstaManipulacije.Kamionska = 1 ";

            }
            if (chkAdministrativna.Checked == true)
            {
                DodatniAND = DodatniAND + " AND VrstaManipulacije.Administrativna = 1 ";
            }

            if (chkFormiranTerminal.Checked == true)
            {
                DodatniAND = DodatniAND + " And FormiranTerminal = 1";


            }
            */



            var select = "";
            if (chkIzvoz.Checked == true)
            {
                select = "  Select RadniNalogInterni.ID as ID, IzvozKonacna.BrojKontejnera, RadniNalogInterni.BrojOsnov, RadniNalogInterni.IDManipulacijaJed, VrstaManipulacije.Naziv, RadniNalogInterni.KonkretaIDUsluge from RadniNalogInterni " +
            " inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
            " inner join VrstaManipulacije on VrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
            " where RadniNalogInterni.OJIzdavanja = 2 and Uradjen = 0 and VrstaManipulacije.Dodatna = 1 " +
            " Order by RadniNalogInterni.BrojOsnov desc ";
            }
            else if (chkUvoz.Checked == true)
            {
                select = "  Select RadniNalogInterni.ID as ID, UvozKonacna.BrojKontejnera, RadniNalogInterni.BrojOsnov, RadniNalogInterni.IDManipulacijaJed, VrstaManipulacije.Naziv,  RadniNalogInterni.KonkretaIDUsluge  from RadniNalogInterni " +
" inner join UvozKonacna on UvozKOnacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
" where RadniNalogInterni.OJIzdavanja = 1 and Uradjen = 0 and VrstaManipulacije.Dodatna = 1 " +
" Order by RadniNalogInterni.BrojOSnov desc";
            }
            else if (chkDodatne.Checked == true)
            {
                select = " Select KontejnerTekuce.Kontejner, TipKontenjera.Naziv as TipKontejnera , KontejnerStatus.Naziv as Status,  Skladista.Naziv as Skladiste, " +
               " Skladista.Kapacitet,  VizuelniNapomena, " +
               " Ostecenja as Ispravnost, Kvalitet, CIR, STATUSIzvoz , " +
               " UlazniBroj as KontejnerID_UVoz,  OperacijaUradjena as ZadnjaOperacija, Skladisnina, SkladisninaOd, SkladisninaDo " +
               " from KontejnerTekuce " +
               " inner join KontejnerStatus on KontejnerStatus.ID = KontejnerTekuce.StatusKontejnera " +
               " inner join Skladista on Skladista.Id = KontejnerTekuce.Skladiste " +
               " inner join TipKontenjera on TipKontenjera.ID = KontejnerTekuce.TipKontejnera ";
            }
            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = null;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;


          

            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void frmDodatneUsluge_Load(object sender, EventArgs e)
        {
            var select8 = "  Select Distinct ID, (Cast(BrVoza as nvarchar(10)) + '-' + Relacija) as IdVoza   From Voz where Dolazeci = 1";
            var s_connection8 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection8 = new SqlConnection(s_connection8);
            var c8 = new SqlConnection(s_connection8);
            var dataAdapter8 = new SqlDataAdapter(select8, c8);

            var commandBuilder8 = new SqlCommandBuilder(dataAdapter8);
            var ds8 = new System.Data.DataSet();
            dataAdapter8.Fill(ds8);
            cboBukingPrijema.DataSource = ds8.Tables[0];
            cboBukingPrijema.DisplayMember = "IdVoza";
            cboBukingPrijema.ValueMember = "ID";


            var select9 = "  Select Distinct ID,Naziv   From VrstaManipulacije where Dodatna = 1";
            var s_connection9 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new System.Data.DataSet();
            dataAdapter9.Fill(ds9);
            cboUsluga.DataSource = ds9.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";

            usao = 1;

            if (IzPomForme == 1)
            {

                UcitajPredpodatke();
            
            }
        }

        private void UcitajPredpodatke()
        {
            if (OsnovPF == null)
            {
                return;
            }
            var select = "";
            if (OJPF == 2)
            {
                chkIzvoz.Checked = true;
                select = " Select RadniNalogInterni.ID as ID,  " +
     " RadniNalogInterni.IDManipulacijaJed, VrstaManipulacije.Naziv, Uradjen , RadniNalogInterni.KonkretaIDUsluge from RadniNalogInterni " +
     " inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
     " inner join VrstaManipulacije on VrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
     " where RadniNalogInterni.OJIzdavanja = 2  and VrstaManipulacije.Dodatna = 1 " +
     " and RadniNalogInterni.BrojOsnov = " + OsnovPF;
            }
            else if (OJPF==1)
            {
                chkUvoz.Checked = true;
                select = " Select RadniNalogInterni.ID as ID, RadniNalogInterni.IDManipulacijaJed, VrstaManipulacije.Naziv,  RadniNalogInterni.KonkretaIDUsluge from RadniNalogInterni " +
                   " inner join UvozKonacna on UvozKOnacna.ID = RadniNalogInterni.BrojOsnov " +
     "  inner join VrstaManipulacije on VrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
     "  where RadniNalogInterni.OJIzdavanja = 1 and VrstaManipulacije.Dodatna = 1 " +
     "  and RadniNalogInterni.BrojOSnov =  " + OsnovPF;
            }
            else
            {

                chkDodatne.Checked = true;
            }
         
            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = null;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;




            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }



           
           


        }

        private void button4_Click(object sender, EventArgs e)
        {
            int pom = 0;
            int pomPotvrdiUradjen = 0;
            if (chkPotvrdiUradjen.Checked == true)
            {
                pomPotvrdiUradjen = 1;
            }
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            if (chkIzvoz.Checked == true)
            {
                try
                {
                    uvK.ZavrsiDodatnuUsluguIzvozZadata(Convert.ToInt32(txtNalogID.Text), Convert.ToInt32(txtKonkretnaMan.Text), Convert.ToDecimal(num1.Value), txtNapomena.Text, pomPotvrdiUradjen, Convert.ToDecimal(num2.Value));

                }
                catch
                {
                    MessageBox.Show("Nije uspela promena usluge");
                }


            }
            else if (chkUvoz.Checked == true)
            {
                try
                {
                   uvK.ZavrsiDodatnuUsluguUvozZadata(Convert.ToInt32(txtNalogID.Text), Convert.ToInt32(txtKonkretnaMan.Text), Convert.ToDecimal(num1.Value), txtNapomena.Text, pomPotvrdiUradjen, Convert.ToDecimal(num2.Value));

                }
                catch
                {
                    MessageBox.Show("Nije uspela promena usluge");
                }

            }
            else if (chkDodatne.Checked == true)
            {
               

                    string kor = Sifarnici.frmLogovanje.user;
                if (chkUvozniPosao.Checked == true )
                {

                    uvK.InsUbaciDodatnuUsluguUvoz(Convert.ToInt32(txtBrojOsnov.Text), Convert.ToInt32(cboUsluga.SelectedValue), Convert.ToDouble(num1.Value), txtNapomena.Text, kor, pomPotvrdiUradjen, Convert.ToDecimal(num2.Value));
                  

                }
                else
                {
                    uvK.InsUbaciDodatnuUsluguIzvoz(Convert.ToInt32(txtBrojOsnov.Text), Convert.ToInt32(cboUsluga.SelectedValue), Convert.ToDouble(num1.Value), txtNapomena.Text, kor, pomPotvrdiUradjen, Convert.ToDecimal(num2.Value));
                }
                   
          

            }
         
        }

        private void VratiPodatkeZaUslugu(int ID)
        {
            if (ID == 0)
            {
                return;
            }
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT ID,  Dodatna, PotvrdaUradio, Apstrakt1, Apstrakt2, JM, JM2 from VrstaManipulacije where ID=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lblKolicina1.Text = dr["Apstrakt1"].ToString();
                lblKolicina2.Text = dr["Apstrakt2"].ToString();
                lblJM.Text = dr["JM"].ToString();
                lblJM2.Text = dr["JM2"].ToString();
                // Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene
                if (dr["PotvrdaUradio"].ToString() == "1")
                {
                    chkPotvrdiUradjen.Visible = true;

                    lblKolicina1.Visible = false;
                    lblKolicina2.Visible = false;
                    lblJM.Visible = false;
                    lblJM2.Visible = false;
                    num1.Visible = false;
                    num2.Visible = false;
                    num1.Value = 1;
                    num2.Value = 1;
                }
                else
                {
                    chkPotvrdiUradjen.Visible = false;

                    lblKolicina1.Visible = true;
                    lblJM.Visible = true;
                  
                    num1.Visible = true;
                    if (lblKolicina2.Text == "")
                    {
                        lblKolicina2.Visible = false;
                        lblJM2.Visible = false;
                        num2.Visible = false;
                    }
                    else
                    {
                        lblKolicina2.Visible = true;
                        lblJM2.Visible = true;
                        num2.Visible = true;
                    }
                  //  lblKolicina2.Visible = true;
                    //num1.Visible = true;
                   // num2.Visible = true;
                    num1.Value = 1;
                    num2.Value = 1;
                }

                 
               
            }

            con.Close();
        }

        private void VratiBrojOsnovUvoz(string Kontejner)
        {
          
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT top 1 ID from UvozKonacna where BrojKontejnera= '" + Kontejner + "' Order By ID Desc", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtBrojOsnov.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void VratiBrojOsnovIzvoz(string Kontejner)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT top 1 ID from IzvozKonacna where BrojKontejnera= '" + Kontejner + "' Order By ID Desc", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtBrojOsnov.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    if (chkDodatne.Checked == true )
                    {
                        txtkontejner.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("Kontejner").ToString();
                        if ( chkUvozniPosao.Checked == true )
                        {
                            VratiBrojOsnovUvoz(txtkontejner.Text);
                        }
                        else
                        {
                            VratiBrojOsnovIzvoz(txtkontejner.Text);
                        }
                       // txtBrojOsnov.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojOsnov").ToString();
                        VratiPodatkeZaUslugu(Convert.ToInt32(cboUsluga.SelectedValue));
                    }
                    else
                    {
                        txtBrojOsnov.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojOsnov").ToString();
                        txtKonkretnaMan.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("KonkretaIDUsluge").ToString();
                        txtNalogID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                        cboUsluga.SelectedValue = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("IDManipulacijaJed").ToString());
                        VratiPodatkeZaUslugu(Convert.ToInt32(cboUsluga.SelectedValue));
                    }
                   
                }
           

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (chkIzvoz.Checked == true)
            {
                frmIzvozDokumenta dm = new frmIzvozDokumenta(txtKonkretnaMan.Text);
                dm.Show();

            }
            else
            {
                //Preraditi
                UvozDokumenta dm = new UvozDokumenta(txtKonkretnaMan.Text,txtBrojOsnov.Text, "1");
                dm.Show();
            }
         
        }

        private void cboUsluga_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           
        }

        private void cboUsluga_SelectedValueChanged(object sender, EventArgs e)
        {
        
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            VratiPodatkeZaUslugu(Convert.ToInt32(cboUsluga.SelectedValue));
        }
    }
}
