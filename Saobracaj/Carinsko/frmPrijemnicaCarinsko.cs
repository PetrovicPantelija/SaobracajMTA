using GMap.NET.MapProviders;
using Microsoft.Office.Interop.Excel;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Carinsko;
using Saobracaj.Pantheon_Export;
using Saobracaj.RadniNalozi;
using Saobracaj.Sifarnici;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;

namespace Saobracaj.Carinko
{
    public partial class frmPrijemnicaCarinsko : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        string PrijemnicaID = "0";

        bool status = false;
        public frmPrijemnicaCarinsko()
        {
            InitializeComponent();
        }
        public frmPrijemnicaCarinsko(string Prijemnica)
        {
            InitializeComponent();
            PrijemnicaID = Prijemnica;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = false;
           

            status = true;
        }

        private void RefreshDataGrid()
        {
            var select = "";
            select = @" SELECT [ID]      ,[IDNadredjena]      ,[Artikal]      ,[JM]      ,[Koleta]      ,[Bruto]      ,[Pozicija]      ,[Vrednost]      ,[Valuta]
      ,[BrojKontejnera]      ,[Paleta]      ,[VrstaPalete]      ,[Dimenzije]
  FROM [dbo].[PrijemnicaCarinskaStavke] where IDNadredjena =" + txtID.Text;

            //  "  where  Aktivnosti.Masinovodja = 1 and Zaposleni = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by Aktivnosti.ID desc";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            int row = ds.Tables[0].Rows.Count - 1;

            for (int r = 0; r <= row; r++)
            {
                //dataGridView1.Rows.Add();

                //1 - NHM'), ('2 - LOT'), ('3 - ZBIRNI'


                dataGridView1.Rows[r].Cells[0].Value = ds.Tables[0].Rows[r].ItemArray[0];
                dataGridView1.Rows[r].Cells[1].Value = ds.Tables[0].Rows[r].ItemArray[1];
                dataGridView1.Rows[r].Cells[2].Value = ds.Tables[0].Rows[r].ItemArray[2];
                dataGridView1.Rows[r].Cells[3].Value = ds.Tables[0].Rows[r].ItemArray[3];
                dataGridView1.Rows[r].Cells[4].Value = ds.Tables[0].Rows[r].ItemArray[4];
                dataGridView1.Rows[r].Cells[5].Value = ds.Tables[0].Rows[r].ItemArray[5];
                dataGridView1.Rows[r].Cells[6].Value = ds.Tables[0].Rows[r].ItemArray[6];

                dataGridView1.Rows[r].Cells[7].Value = ds.Tables[0].Rows[r].ItemArray[7];
                dataGridView1.Rows[r].Cells[8].Value = ds.Tables[0].Rows[r].ItemArray[8];
                dataGridView1.Rows[r].Cells[9].Value = ds.Tables[0].Rows[r].ItemArray[9];
                dataGridView1.Rows[r].Cells[10].Value = ds.Tables[0].Rows[r].ItemArray[10];
                dataGridView1.Rows[r].Cells[11].Value = ds.Tables[0].Rows[r].ItemArray[11];
                dataGridView1.Rows[r].Cells[12].Value = ds.Tables[0].Rows[r].ItemArray[12];
             //   dataGridView1.Rows[r].Cells[13].Value = ds.Tables[0].Rows[r].ItemArray[13];

            }

        }

        private void RefreshsfDataGrid()
        {
            var select = "";
            select = @" SELECT [ID]      ,[IDNadredjena]      ,[Artikal]      ,[JM]      ,[Koleta]      ,[Bruto]      ,[Pozicija]      ,[Vrednost]      ,[Valuta]
      ,[BrojKontejnera]      ,[Paleta]      ,[VrstaPalete]      ,[Dimenzije]
  FROM [dbo].[PrijemnicaCarinskaStavke] where IDNadredjena =" + txtID.Text;

            //  "  where  Aktivnosti.Masinovodja = 1 and Zaposleni = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by Aktivnosti.ID desc";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            this.sfDataGrid1.DataSource =  ds.Tables[0];

        }

        private void button21_Click(object sender, EventArgs e)
        {
          
            if (status == true)
            {
                InsertPrijemnicaCarinska ins = new InsertPrijemnicaCarinska();
                ins.InsPrijemnicaCarinska( 
                txtStatus.Text, DateTime.Now,
Kor, Convert.ToInt32(cboSkladisteID.SelectedValue), txtDokument.Text,
Convert.ToInt32(cboMagacinskiBroj.SelectedValue), cboVrstaSkladista.Text, cboSektor.Text,
Convert.ToInt32(cboVlasnik.SelectedValue), Convert.ToInt32(cboKorisnikRobe.SelectedValue), Convert.ToInt32(cboPosiljalac.SelectedValue),
Convert.ToInt32(cboPrimalac.SelectedValue), txtBrojFakture.Text,
txtPrevoznik.Text, txtBrojKamiona.Text,
txtNapomena1.Text, txtNapomena2.Text,
txtTransportNo.Text, Convert.ToDateTime(dtpOcekivanoVreme.Value));

              RefreshDataGrid();
              //  RefrechDataGridT();
                status = false;
            }
            else
            {
                InsertPrijemnicaCarinska upd = new InsertPrijemnicaCarinska();
                upd.UpdPrijemnicaCarinska(Convert.ToInt32(txtID.Text), txtStatus.Text, DateTime.Now,
Kor, Convert.ToInt32(cboSkladisteID.SelectedValue), txtDokument.Text,
Convert.ToInt32(cboMagacinskiBroj.SelectedValue), cboVrstaSkladista.Text, cboSektor.Text,
Convert.ToInt32(cboVlasnik.SelectedValue), Convert.ToInt32(cboKorisnikRobe.SelectedValue), Convert.ToInt32(cboPosiljalac.SelectedValue),
Convert.ToInt32(cboPrimalac.SelectedValue), txtBrojFakture.Text,
txtPrevoznik.Text, txtBrojKamiona.Text,
txtNapomena1.Text, txtNapomena2.Text,
txtTransportNo.Text, Convert.ToDateTime(dtpOcekivanoVreme.Value));
                status = false;
               
                 RefreshDataGrid();
               // RefrechDataGridT();
            }
        }

        private void frmPrijemnicaCarinsko_Load(object sender, EventArgs e)
        {

            txtID.Text = PrijemnicaID;
            
            FillCombo();
            DGVCombo();
            RefreshDataGrid();
            RefreshsfDataGrid();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            frmMagacinskiBrojevi mag = new frmMagacinskiBrojevi();
            mag.Show();
        }


        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

          

            //where Vlasnik = 1
            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new System.Data.DataSet();
            partAD.Fill(partDS);
            cboVlasnik.DataSource = partDS.Tables[0];
            cboVlasnik.DisplayMember = "PaNaziv";
            cboVlasnik.ValueMember = "PaSifra";
            //uvoznik
            var partner2 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new System.Data.DataSet();
            partAD2.Fill(partDS2);
            cboKorisnikRobe.DataSource = partDS2.Tables[0];
            cboKorisnikRobe.DisplayMember = "PaNaziv";
            cboKorisnikRobe.ValueMember = "PaSifra";


            //Igranka

            //  cboUvoznik.DisplayMember = "PaNaziv";
            // cboUvoznik.ValueMember = "PaSifra";

            //
            //spedicija na granici

            var partner3 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
            var partAD3 = new SqlDataAdapter(partner3, conn);
            var partDS3 = new System.Data.DataSet();
            partAD3.Fill(partDS3);
            cboPosiljalac.DataSource = partDS3.Tables[0];
            cboPosiljalac.DisplayMember = "PaNaziv";
            cboPosiljalac.ValueMember = "PaSifra";
            //spedicija rtc luka leget

            var partner4 = "Select PaSifra,PaNaziv From Partnerji  where Spediter = 1 order by PaNaziv";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new System.Data.DataSet();
            partAD4.Fill(partDS4);
            cboPrimalac.DataSource = partDS4.Tables[0];
            cboPrimalac.DisplayMember = "PaNaziv";
            cboPrimalac.ValueMember = "PaSifra";
            //odredisna spedicija
            var partner5 = "Select ID, Napomena from MagacinskiBrojevi order by ID Desc";
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new System.Data.DataSet();
            partAD5.Fill(partDS5);
            cboMagacinskiBroj.DataSource = partDS5.Tables[0];
            cboMagacinskiBroj.DisplayMember = "ID";
            cboMagacinskiBroj.ValueMember = "ID";

            var tipkontejnera = "Select ID, Naziv From SkladistaGrupa";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new System.Data.DataSet();
            tkAD.Fill(tkDS);
            cboVrstaSkladista.DataSource = tkDS.Tables[0];
            cboVrstaSkladista.DisplayMember = "Naziv";
            cboVrstaSkladista.ValueMember = "ID";

            var adr = "Select ID, Naziv from Skladista  order by ID";
            var adrSAD = new SqlDataAdapter(adr, conn);
            var adrSDS = new System.Data.DataSet();
            adrSAD.Fill(adrSDS);
            cboSkladisteID.DataSource = adrSDS.Tables[0];
            cboSkladisteID.DisplayMember = "Naziv";
            cboSkladisteID.ValueMember = "ID";

        }


        DataGridViewComboBoxColumn cbo = new DataGridViewComboBoxColumn();

        private void DGVCombo()
        {
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            ID.HeaderText = "Stavka";
            ID.Name = "ID";
            ID.Width = 50;

            DataGridViewTextBoxColumn IDNadredjena = new DataGridViewTextBoxColumn();
            IDNadredjena.HeaderText = "ID";
            IDNadredjena.Name = "IDNadredjena";
            IDNadredjena.Width = 50;

            DataGridViewTextBoxColumn Artikal = new DataGridViewTextBoxColumn();
            Artikal.HeaderText = "Artikal";
            Artikal.Name = "Artikal";
            Artikal.Width = 450;

            DataGridViewComboBoxColumn JM = new DataGridViewComboBoxColumn();
            JM.HeaderText = "JM";
            JM.Name = "JM";
            var query21 = "SELECT MeSifra FROM MerskeEnote";
            SqlConnection conn21 = new SqlConnection(connection);
            SqlDataAdapter da21 = new SqlDataAdapter(query21, conn21);
            System.Data.DataSet ds21 = new System.Data.DataSet();
            da21.Fill(ds21);
            JM.DataSource = ds21.Tables[0];
            JM.DisplayMember = "MeSifra";
            JM.ValueMember = "MeSifra";

            DataGridViewTextBoxColumn Koleta = new DataGridViewTextBoxColumn();
            Koleta.ValueType = typeof(Decimal);
            Koleta.DefaultCellStyle.Format = "N2";
            Koleta.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Koleta.HeaderText = "Koleta";
            Koleta.Name = "Koleta";
            Koleta.Width = 150;

            DataGridViewTextBoxColumn Bruto = new DataGridViewTextBoxColumn();
            Bruto.HeaderText = "Bruto";
            Bruto.Name = "Bruto";
            Bruto.Width = 150;



            DataGridViewComboBoxColumn pozicija = new DataGridViewComboBoxColumn();
            pozicija.HeaderText = "Pozicija";
            pozicija.Name = "Pozicija";
            var query211 = "select ID, Oznaka from Pozicija";
            SqlConnection conn211 = new SqlConnection(connection);
            SqlDataAdapter da211 = new SqlDataAdapter(query211, conn211);
            System.Data.DataSet ds211 = new System.Data.DataSet();
            da211.Fill(ds211);
            pozicija.DataSource = ds211.Tables[0];
            pozicija.DisplayMember = "Oznaka";
            pozicija.ValueMember = "ID";

            DataGridViewTextBoxColumn vrednost = new DataGridViewTextBoxColumn();
            vrednost.HeaderText = "Vrednost";
            vrednost.Name = "Vrednost";
            vrednost.Width = 150;


            DataGridViewComboBoxColumn valuta = new DataGridViewComboBoxColumn();
            valuta.HeaderText = "Valuta";
            valuta.Name = "Valuta";
            var query212 = "select VaSifra from Valute";
            SqlConnection conn212 = new SqlConnection(connection);
            SqlDataAdapter da212 = new SqlDataAdapter(query212, conn212);
            System.Data.DataSet ds212 = new System.Data.DataSet();
            da212.Fill(ds212);
            valuta.DataSource = ds212.Tables[0];
            valuta.DisplayMember = "VaSifra";
            valuta.ValueMember = "VaSifra";


            DataGridViewTextBoxColumn brojkontejnera = new DataGridViewTextBoxColumn();
            brojkontejnera.HeaderText = "BrojKontejnera";
            brojkontejnera.Name = "BrojKontejnera";
            brojkontejnera.Width = 250;



            DataGridViewTextBoxColumn paleta = new DataGridViewTextBoxColumn();
            paleta.HeaderText = "Paleta";
            paleta.Name = "Paleta";
            paleta.Width = 150;

            DataGridViewTextBoxColumn vrstapalete = new DataGridViewTextBoxColumn();
            vrstapalete.HeaderText = "VrstaPalete";
            vrstapalete.Name = "VrstaPalete";
            vrstapalete.Width = 150;

            DataGridViewTextBoxColumn dimenzije = new DataGridViewTextBoxColumn();
            dimenzije.HeaderText = "Dimenzije";
            dimenzije.Name = "Dimenzije";
            dimenzije.Width = 150;

            dataGridView1.Columns.Add(ID);
            dataGridView1.Columns.Add(IDNadredjena);
            dataGridView1.Columns.Add(Artikal);
            dataGridView1.Columns.Add(JM);
            dataGridView1.Columns.Add(Koleta);
            dataGridView1.Columns.Add(Bruto);
            dataGridView1.Columns.Add(pozicija);
            dataGridView1.Columns.Add(vrednost);
            dataGridView1.Columns.Add(valuta);
            dataGridView1.Columns.Add(brojkontejnera);
            dataGridView1.Columns.Add(paleta);
            dataGridView1.Columns.Add(vrstapalete);
            dataGridView1.Columns.Add(dimenzije);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            InsertPrijemnicaCarinskaStavke ins = new InsertPrijemnicaCarinskaStavke();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string postojeciID = "0";
                if (row != null)
                {
                if (row.Cells[0].Value ==  null)
                {
                    postojeciID = "0";

                }
                else
                {
                    postojeciID = row.Cells[0].Value.ToString();
                }
                   


                }
                string brk = "";
                string paleta = "";
                string vrstp = "";
                string dim = "";
                if (row != null)
                {



               
                  

                if (row.Cells[9].Value == null)
                {
                    brk = " ";
                }
                else
                {  brk = row.Cells[9].Value.ToString();}


                if (row.Cells[10].Value == null)
                {
                    paleta = " ";
                }
                else
                { paleta = row.Cells[10].Value.ToString(); }

                if (row.Cells[11].Value == null)
                {
                    vrstp = " ";
                }
                else
                { vrstp = row.Cells[11].Value.ToString(); }


                if (row.Cells[12].Value == null)
                {
                    dim = " ";
                }
                else
                { dim = row.Cells[12].Value.ToString(); }


                }

                if (row != null)
                    {
                    ins.InsPrijemnicaCarinskaStavke(Convert.ToInt32(postojeciID), Convert.ToInt32(txtID.Text),
                        row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), Convert.ToDouble(row.Cells[4].Value),
                        Convert.ToDouble(row.Cells[5].Value)
                        , Convert.ToInt32(row.Cells[6].Value), Convert.ToDouble(row.Cells[7].Value), row.Cells[8].Value.ToString(),
                        brk, paleta, vrstp, dim);
                    }
            }
        }
    }
    }

