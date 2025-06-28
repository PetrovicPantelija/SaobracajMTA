using GMap.NET.MapProviders;
using Microsoft.Office.Interop.Excel;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Carinko;
using Saobracaj.Carinsko;
using Saobracaj.Pantheon_Export;
using Saobracaj.RadniNalozi;
using Saobracaj.Sifarnici;
using Saobracaj.Uvoz;
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
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;

namespace Saobracaj.Carinsko
{
    public partial class frmOtpremnicaCarinsko : Form
    {

        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        string OtpremnicaID = "0";

        bool status = false;

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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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

            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Helvetica", 12F, GraphicsUnit.Pixel);
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


        public frmOtpremnicaCarinsko()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        public frmOtpremnicaCarinsko(string Otpremnica)
        {
            InitializeComponent();
            ChangeTextBox();
            OtpremnicaID = Otpremnica;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                insertOtpremnicaCarinsko ins = new insertOtpremnicaCarinsko();
                ins.InsOtpremnicaCarinska(
                txtStatus.Text, DateTime.Now,
Kor, Convert.ToInt32(cboSkladisteID.SelectedValue), txtDokument.Text,
Convert.ToInt32(cboMagacinskiBroj.SelectedValue), 
Convert.ToInt32(cboVlasnik.SelectedValue), Convert.ToInt32(cboKorisnikRobe.SelectedValue), Convert.ToInt32(cboNalogodavac.SelectedValue),
Convert.ToInt32(cboKupac.SelectedValue), Convert.ToDouble(numIznos.Value),
txtPrevoznik.Text, txtBrojKamiona.Text,
txtNapomena1.Text, txtNapomena2.Text,
txtTransportNo.Text, Convert.ToDateTime(dtpOcekivanoVreme.Value), Convert.ToInt32(cboPrijemnicaId.SelectedValue));

                //RefreshDataGrid();
                //  RefrechDataGridT();
                status = false;
            }
            else
            {
                insertOtpremnicaCarinsko upd = new insertOtpremnicaCarinsko();
                upd.updOtpremnicaCarinska(Convert.ToInt32(txtID.Text), txtStatus.Text, DateTime.Now,
Kor, Convert.ToInt32(cboSkladisteID.SelectedValue), txtDokument.Text,
Convert.ToInt32(cboMagacinskiBroj.SelectedValue), 
Convert.ToInt32(cboVlasnik.SelectedValue), Convert.ToInt32(cboKorisnikRobe.SelectedValue), Convert.ToInt32(cboNalogodavac.SelectedValue),
Convert.ToInt32(cboKupac.SelectedValue), Convert.ToDouble(numIznos.Value),
txtPrevoznik.Text, txtBrojKamiona.Text,
txtNapomena1.Text, txtNapomena2.Text,
txtTransportNo.Text, Convert.ToDateTime(dtpOcekivanoVreme.Value), Convert.ToInt32(cboPrijemnicaId.SelectedValue));
                status = false;

               // RefreshDataGrid();
                // RefrechDataGridT();
            }
        }


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
   
            DataGridViewTextBoxColumn prijemnicastavkaid = new DataGridViewTextBoxColumn();
            prijemnicastavkaid.HeaderText = "PrijemnicaStavkaID";
            prijemnicastavkaid.Name = "PrijekmnicaStavkaID";
            prijemnicastavkaid.Width = 150;
            
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
            dataGridView1.Columns.Add(prijemnicastavkaid);

        }

        private void button22_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = false;


            status = true;
        }

        private void frmOtpremnicaCarinsko_Load(object sender, EventArgs e)
        {
            txtID.Text = OtpremnicaID;

            FillCombo();
            VratiPodatke(OtpremnicaID);
            DGVCombo();

            RefreshDataGrid();
          //  RefreshsfDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = "";
            select = @" SELECT [ID]      ,[IDNadredjena]      ,[Artikal]      ,[JM]      ,[Koleta]      ,[Bruto]      ,[Pozicija]      ,[Vrednost]      ,[Valuta]
      ,[BrojKontejnera]      ,[Paleta]      ,[VrstaPalete]      ,[Dimenzije], PrijemnicaStavkaID
  FROM [dbo].[OtpremnicaCarinskaStavke] where IDNadredjena =" + txtID.Text;

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
                dataGridView1.Rows[r].Cells[13].Value = ds.Tables[0].Rows[r].ItemArray[13];

            }

        }

        private void VratiPodatke(string PrijemnicaID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID]       , [Status]      , [Datum] " +
                 " , [Korisnik]      , [SkladisteID]       , [Dokument]      , [MBR] " +
                 " , [Vlasnik]            , [KorisnikROba]      , [Nalogodavac]      , [Kupac] " +
                 " , [Iznos]      , [Prevoznik]      , [BrojKamiona]      , [Napomena1] " +
                 " , [Napomena2]      , [TransportNo]      , [OcekivanoVreme]      , [PrijemnicaID] " +
              " FROM [dbo].[OtpremnicaCarinska] " +
             " where ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtStatus.Text = dr["Status"].ToString();
                dtpDatum.Value = Convert.ToDateTime(dr["Datum"].ToString());
                txtKorisnik.Text = dr["Korisnik"].ToString();
                cboSkladisteID.SelectedValue = Convert.ToInt32(dr["SkladisteID"].ToString());
                txtDokument.Text = dr["Dokument"].ToString();
                cboMagacinskiBroj.SelectedValue = Convert.ToInt32(dr["MBR"].ToString());
                cboVlasnik.SelectedValue = Convert.ToInt32(dr["Vlasnik"].ToString());
                cboKorisnikRobe.SelectedValue = Convert.ToInt32(dr["KorisnikROba"].ToString());
                cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                cboKupac.SelectedValue = Convert.ToInt32(dr["Kupac"].ToString());
                numIznos.Value = Convert.ToDecimal(dr["Iznos"].ToString());
                txtPrevoznik.Text = dr["Prevoznik"].ToString();
                txtBrojKamiona.Text = dr["BrojKamiona"].ToString();
                txtNapomena1.Text = dr["Napomena1"].ToString();
                txtNapomena2.Text = dr["Napomena2"].ToString();
                txtTransportNo.Text = dr["TransportNo"].ToString();
                dtpOcekivanoVreme.Value = Convert.ToDateTime(dr["OcekivanoVreme"].ToString());
                cboPrijemnicaId.SelectedValue = dr["PrijemnicaID"].ToString();
            }

            con.Close();

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


         

            var partner3 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
            var partAD3 = new SqlDataAdapter(partner3, conn);
            var partDS3 = new System.Data.DataSet();
            partAD3.Fill(partDS3);
            cboNalogodavac.DataSource = partDS3.Tables[0];
            cboNalogodavac.DisplayMember = "PaNaziv";
            cboNalogodavac.ValueMember = "PaSifra";
            //spedicija rtc luka leget

            var partner4 = "Select PaSifra,PaNaziv From Partnerji  order by PaNaziv";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new System.Data.DataSet();
            partAD4.Fill(partDS4);
            cboKupac.DataSource = partDS4.Tables[0];
            cboKupac.DisplayMember = "PaNaziv";
            cboKupac.ValueMember = "PaSifra";
            //odredisna spedicija
            var partner5 = "Select ID, Napomena from MagacinskiBrojevi order by ID Desc";
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new System.Data.DataSet();
            partAD5.Fill(partDS5);
            cboMagacinskiBroj.DataSource = partDS5.Tables[0];
            cboMagacinskiBroj.DisplayMember = "ID";
            cboMagacinskiBroj.ValueMember = "ID";

            var tipkontejnera = "Select ID From PrijemnicaCarinska";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new System.Data.DataSet();
            tkAD.Fill(tkDS);
            cboPrijemnicaId.DataSource = tkDS.Tables[0];
            cboPrijemnicaId.DisplayMember = "ID";
            cboPrijemnicaId.ValueMember = "ID";

            var adr = "Select ID, Naziv from Skladista  order by ID";
            var adrSAD = new SqlDataAdapter(adr, conn);
            var adrSDS = new System.Data.DataSet();
            adrSAD.Fill(adrSDS);
            cboSkladisteID.DataSource = adrSDS.Tables[0];
            cboSkladisteID.DisplayMember = "Naziv";
            cboSkladisteID.ValueMember = "ID";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int prStDokumenta = 0;
            string LOt = "";
            if (frmLogovanje.Firma == "Leget")
            {
                var query = "Select (Max(PrStDokumenta)+1) From Promet";
                SqlConnection conn = new SqlConnection(connection);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    prStDokumenta = Convert.ToInt32(dr[0].ToString());
                }
                conn.Close();

                InsertIsporuka ins = new InsertIsporuka();
                insertOtpremnicaCarinsko updst = new insertOtpremnicaCarinsko();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row != null && row.Cells[0].Value != null)
                    {
                        int skladisteno = 0;


                        //       string LOt = row.Cells["Lot"].Value.ToString();

                        int Tip = 2;
                        /*
                        SELECT[ID]      ,[IDNadredjena]      ,[Artikal] -2      ,[JM]  - 3     ,[Koleta] - 4      ,[Bruto]      ,[Pozicija]  - 6     ,[Vrednost]      ,[Valuta]
      ,[BrojKontejnera]   - 9    ,[Paleta]      ,[VrstaPalete]      ,[Dimenzije]
                        FROM[dbo].[PrijemnicaCarinskaStavke]

                        row.Cells[0].Value.ToString() -- PrijemnicaStavkaID
                            row.Cells[3].Value.ToString(), 
                            Convert.ToDouble(row.Cells[4].Value),
                        Convert.ToDouble(row.Cells[5].Value)
                        , Convert.ToInt32(row.Cells[6].Value), 
                            Convert.ToDouble(row.Cells[7].Value), 
                            row.Cells[8].Value.ToString(),
            */

                        ins.InsertPromet(Convert.ToDateTime(dtpDatum.Value), "OTP", prStDokumenta, row.Cells[9].Value.ToString(), "COT", 0,  Convert.ToDecimal(row.Cells[4].Value), 0, 0, Convert.ToInt32(cboSkladisteID.SelectedValue),
                            Convert.ToInt32(row.Cells[6].Value),  Convert.ToDateTime(DateTime.Now), Kor.Trim(), 0, 0, Convert.ToDateTime(dtpDatum.Value.ToString()), row.Cells[3].Value.ToString(),
                            row.Cells[13].Value.ToString(), Convert.ToInt32(txtID.Text), Convert.ToInt32(0), skladisteno, Tip);

                        updst.updetePrijemnicaCarinskaStavkaKolicina(Convert.ToInt32(row.Cells[13].Value), Convert.ToDouble(row.Cells[4].Value));
                        //Yaklapaju se po stavci prijemnice

                    }
                }

             
                updst.updeteOtpremnicaCarinskaStatus(Convert.ToInt32(txtID.Text), "PROKNJIZEN");
               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPrijemnicaStavke stav = new frmPrijemnicaStavke(txtID.Text);
            stav.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmOtpremnicaCarinskaStampa st = new frmOtpremnicaCarinskaStampa(txtID.Text);
            st.Show();
        }
    }
}
