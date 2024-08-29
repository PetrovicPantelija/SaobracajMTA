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

namespace Saobracaj.Uvoz
{
    public partial class frmDodatneUsluge : Syncfusion.Windows.Forms.Office2010Form
    {
        int usao = 1;
        public frmDodatneUsluge()
        {
            InitializeComponent();
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
                select = " Select DISTINCT 0 as ID, BrojKontejnera, KontejnerID as BrojOsnov, 0 as  IDMAnipulacijaJed, '' as Naziv, 0 as KonkretaIDUsluge from PrijemKontejneraVozStavke   " +
" inner join PrijemKontejneraVoz on PrijemKontejneraVoz.ID = PrijemKontejneraVozStavke.IDNadredjenog " +
" where PrijemKontejneraVoz.IdVoza = " + cboBukingPrijema.SelectedValue +
" ";
            }
            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
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
            var ds8 = new DataSet();
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
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboUsluga.DataSource = ds9.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";

            usao = 1;
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
                    uvK.InsUbaciDodatnuUsluguUvoz(Convert.ToInt32(txtBrojOsnov.Text),Convert.ToInt32(cboUsluga.SelectedValue), Convert.ToDouble(num1.Value), txtNapomena.Text, kor, pomPotvrdiUradjen, Convert.ToDecimal(num2.Value));
          

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

            SqlCommand cmd = new SqlCommand("SELECT ID,  Dodatna, PotvrdaUradio, Apstrakt1, Apstrakt2 from VrstaManipulacije where ID=" + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                lblKolicina1.Text = dr["Apstrakt1"].ToString();
                lblKolicina2.Text = dr["Apstrakt2"].ToString();
                // Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene
                if (dr["PotvrdaUradio"].ToString() == "1")
                {
                    chkPotvrdiUradjen.Visible = true;

                    lblKolicina1.Visible = false;
                    lblKolicina2.Visible = false;
                    num1.Visible = false;
                    num2.Visible = false;
                    num1.Value = 1;
                    num2.Value = 1;
                }
                else
                {
                    chkPotvrdiUradjen.Visible = false;

                    lblKolicina1.Visible = true;
                    num1.Visible = true;
                    if (lblKolicina2.Text == "")
                    {
                        lblKolicina2.Visible = false;

                        num2.Visible = false;
                    }
                    else
                    {
                        lblKolicina2.Visible = true;
                      
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

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtBrojOsnov.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojOsnov").ToString();
                  
                    if (chkDodatne.Checked == true )
                    {
                        VratiPodatkeZaUslugu(Convert.ToInt32(cboUsluga.SelectedValue));
                    }
                    else
                    {
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (chkIzvoz.Checked == true)
            {
                frmDokumentaIzvozManipulacije dm = new frmDokumentaIzvozManipulacije(txtKonkretnaMan.Text);
                dm.Show();

            }
            else
            {
                frmDokumentaUvozneManipulacije dm = new frmDokumentaUvozneManipulacije(txtKonkretnaMan.Text);
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
