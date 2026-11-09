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
using Syncfusion.WinForms.DataGrid;
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
        bool promenaUFakturi = false;
        private object staraVrednost;

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
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
        public frmPrijemnicaCarinsko()
        {
            InitializeComponent();
            this.FormClosing += frmPrijemnicaCarinsko_FormClosing;
        }
        public frmPrijemnicaCarinsko(string Prijemnica)
        {
            InitializeComponent();
            ChangeTextBox();
            PrijemnicaID = Prijemnica;
            this.FormClosing += frmPrijemnicaCarinsko_FormClosing;
        }

        private void frmPrijemnicaCarinsko_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ensure the form is allowed to close and resources are released.
            // If some other logic attempted to cancel closing, force the close.
            try
            {
                e.Cancel = false;
                // detach handlers if needed and dispose
                this.FormClosing -= frmPrijemnicaCarinsko_FormClosing;
                this.Dispose();
            }
            catch
            {
                // ignore errors during disposal
            }
        }

        private void PodesiDatagridView2(DataGridView dgv)
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

        private void button22_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = false;


            status = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.Rows.Clear();
        }

        private void RefreshDataGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Rows.Clear();

            bool idValid = !string.IsNullOrWhiteSpace(txtID.Text) &&
                           int.TryParse(txtID.Text, out int id) &&
                           id != 0;

            // 1) Ako ID NIJE validan nema SELECT, grid prazan i ceo read-only
            if (!idValid)
            {
                dataGridView1.ReadOnly = true;
                return;
            }


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
            System.Data.DataTable dt = ds.Tables[0];

            //int row = ds.Tables[0].Rows.Count - 1;

            foreach (System.Data.DataRow dr in ds.Tables[0].Rows)
            {
                int rowIndex = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowIndex];

                row.Cells["ID"].Value = dr["ID"];
                row.Cells["IDNadredjena"].Value = dr["IDNadredjena"];
                row.Cells["Artikal"].Value = dr["Artikal"];
                row.Cells["JM"].Value = dr["JM"];
                row.Cells["Koleta"].Value = dr["Koleta"];
                row.Cells["Bruto"].Value = dr["Bruto"];
                row.Cells["Pozicija"].Value = dr["Pozicija"];
                row.Cells["Vrednost"].Value = dr["Vrednost"];
                row.Cells["Valuta"].Value = dr["Valuta"];
                row.Cells["BrojKontejnera"].Value = dr["BrojKontejnera"];
                row.Cells["Paleta"].Value = dr["Paleta"];
                row.Cells["VrstaPalete"].Value = dr["VrstaPalete"];
                row.Cells["Dimenzije"].Value = dr["Dimenzije"];
            }
            dataGridView1.ReadOnly = false;
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["IDNadredjena"].ReadOnly = true;

            PodesiDatagridView(dataGridView1);

        }

        private void RefreshsfDataGrid()
        {
            var select = "";
            if (string.IsNullOrWhiteSpace(txtID.Text) || !int.TryParse(txtID.Text, out int id))
            {
                // Ako nije validno – očisti grid i prekini
                sfDataGrid1.DataSource = null;
                return;
            }
            select = @" SELECT [ID]      ,[IDNadredjena]      ,[Artikal]      ,JM      ,[Koleta]      ,[Bruto]      ,[Pozicija]      ,[Vrednost]      ,[Valuta]
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
            this.sfDataGrid1.DataSource = ds.Tables[0];



            var query21 = "SELECT MeSifra FROM MerskeEnote";
            SqlConnection conn21 = new SqlConnection(connection);
            SqlDataAdapter da21 = new SqlDataAdapter(query21, conn21);
            System.Data.DataSet ds21 = new System.Data.DataSet();
            da21.Fill(ds21);
            this.sfDataGrid1.Columns.Add(new GridComboBoxColumn() { MappingName = "JM2", HeaderText = "JM2" });
            (this.sfDataGrid1.Columns["JM2"] as GridComboBoxColumn).DataSource = ds21.Tables[0];
                ;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                status = true;
            }



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
txtTransportNo.Text, Convert.ToDateTime(dtpOcekivanoVreme.Value), Convert.ToInt32(cboNalogodavac.SelectedValue));

              
                //  RefrechDataGridT();
                status = false;
                RefreshDataGrid(); // da bi se postavila logika koje su kolone editabilne a koje nisu
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
txtTransportNo.Text, Convert.ToDateTime(dtpOcekivanoVreme.Value), Convert.ToInt32(cboNalogodavac.SelectedValue));
                status = false;

   
                // RefrechDataGridT();
            }
            RefreshDataGrid();
        }

        private void frmPrijemnicaCarinsko_Load(object sender, EventArgs e)
        {

            txtID.Text = PrijemnicaID;

            FillCombo();
            DGVCombo();
            VratiPodatke(PrijemnicaID);
            RefreshDataGrid();
            RefreshsfDataGrid();
        }

        private void VratiPodatke(string PrijemnicaID)
        {
            
            if (string.IsNullOrWhiteSpace(txtID.Text) || !int.TryParse(txtID.Text, out int id))
            {
                // Ako nije validno – očisti grid i prekini
                sfDataGrid1.DataSource = null;
                return;
            }
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID]      , [Status] " +
             " , [Datum]      , [Korisanik]      , [SkladisteID]      , [Dokument] " +
             " , [MBR]      , [VrstaSkladista]      , [Sektor]      , [Vlasnik] " +
             " , [Korisinik]      , [Posiljalac]      , [Primalac]      , [BrFakture] " +
             " , [Prevoznik]      , [BrojKamiona]      , [Napomena1]      , [Napomena2] " +
             " , [TransportNo]      , [OcekivanoVreme], Nalogodavac  FROM [dbo].[PrijemnicaCarinska]" +
             " where ID = " + txtID.Text, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtStatus.Text = dr["Status"].ToString();
                dtpDatum.Value = Convert.ToDateTime(dr["Datum"].ToString());
                txtKorisnik.Text = dr["Korisanik"].ToString();
                cboSkladisteID.SelectedValue = Convert.ToInt32(dr["SkladisteID"].ToString());
                txtDokument.Text = dr["Dokument"].ToString();
                cboMagacinskiBroj.SelectedValue = Convert.ToInt32(dr["MBR"].ToString());
                cboVrstaSkladista.Text = dr["VrstaSkladista"].ToString();
                cboSektor.Text = dr["Sektor"].ToString();
                cboVlasnik.SelectedValue = Convert.ToInt32(dr["Vlasnik"].ToString());
                cboKorisnikRobe.SelectedValue = Convert.ToInt32(dr["Korisinik"].ToString());
                cboPosiljalac.SelectedValue = Convert.ToInt32(dr["Posiljalac"].ToString());
                cboPrimalac.SelectedValue = Convert.ToInt32(dr["Primalac"].ToString());
                txtBrojFakture.Text = dr["BrFakture"].ToString();
                txtPrevoznik.Text = dr["Prevoznik"].ToString();
                txtBrojKamiona.Text = dr["BrojKamiona"].ToString();
                txtNapomena1.Text = dr["Napomena1"].ToString();
                txtNapomena2.Text = dr["Napomena2"].ToString();
                txtTransportNo.Text = dr["TransportNo"].ToString();
                dtpOcekivanoVreme.Value = Convert.ToDateTime(dr["OcekivanoVreme"].ToString());
                cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());

            }

            con.Close();

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

            var partner4 = "Select PaSifra,PaNaziv From Partnerji   order by PaNaziv";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new System.Data.DataSet();
            partAD4.Fill(partDS4);
            cboPrimalac.DataSource = partDS4.Tables[0];
            cboPrimalac.DisplayMember = "PaNaziv";
            cboPrimalac.ValueMember = "PaSifra";

            var partner41 = "Select PaSifra,PaNaziv From Partnerji   order by PaNaziv";
            var partAD41 = new SqlDataAdapter(partner41, conn);
            var partDS41 = new System.Data.DataSet();
            partAD41.Fill(partDS41);
            cboNalogodavac.DataSource = partDS41.Tables[0];
            cboNalogodavac.DisplayMember = "PaNaziv";
            cboNalogodavac.ValueMember = "PaSifra";



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
                    if (row.Cells[0].Value == null)
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
                    { brk = row.Cells[9].Value.ToString(); }


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
                double vrednost = 0;
                string valuta = "RSD";

                if (row.Cells[7].Value == null)
                {
                    vrednost = 0;
                }
                else
                {
                    vrednost = Convert.ToDouble(row.Cells[7].Value);
                }

                if (row.Cells[8].Value == null)
                {
                    valuta = "RSD";
                }
                else
                {
                    valuta = row.Cells[8].Value.ToString();
                }

                if (row.Cells[1].Value == null)
                {
                    ins.InsPrijemnicaCarinskaStavke(Convert.ToInt32(postojeciID), Convert.ToInt32(txtID.Text),
                        row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), Convert.ToDouble(row.Cells[4].Value),
                        Convert.ToDouble(row.Cells[5].Value)
                        , Convert.ToInt32(row.Cells[6].Value), vrednost, valuta,
                        brk, paleta, vrstp, dim);
                }

                
            }

            RefreshDataGrid();

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
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row != null && row.Cells[0].Value != null)
                    {
                        int skladisteno = 0;

                     
                        //       string LOt = row.Cells["Lot"].Value.ToString();

                        int Tip = 2;
                    

                        ins.InsertPromet(Convert.ToDateTime(dtpDatum.Value), "PRI", prStDokumenta, row.Cells[9].Value.ToString(), "CPR", Convert.ToDecimal(row.Cells[4].Value), 0, Convert.ToInt32(cboSkladisteID.SelectedValue),
                            Convert.ToInt32(row.Cells[6].Value), 0, 0, Convert.ToDateTime(DateTime.Now), Kor.Trim(), 0, 0, Convert.ToDateTime(dtpDatum.Value.ToString()), row.Cells[3].Value.ToString(),
                            row.Cells[0].Value.ToString(), Convert.ToInt32(txtID.Text), Convert.ToInt32(0), skladisteno, Tip);

                        // txtPrometID.Text = VratiIDPrometa().ToString();

                        //isporuka.InsertPrijemnicaPostav(Convert.ToInt32(row.Cells[0].Value), Convert.ToDecimal(row.Cells[1].Value), Convert.ToInt32(cbo_Skladiste.SelectedValue), cbo_Lokacija.SelectedValue.ToString(), cbo_MestoTroska.SelectedValue.ToString());
                        // progressBar1.Value = progressBar1.Value + 1;
                    }
                }

                InsertPrijemnicaCarinska updst = new InsertPrijemnicaCarinska();
                updst.updetePrijemnicaCarinskaStatus(Convert.ToInt32(txtID.Text), "PROKNJIZEN");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPrijemnicaCarinskaStampa st = new frmPrijemnicaCarinskaStampa(txtID.Text);
            st.Show();
        }

        private void button4_Click(object sender, EventArgs e)
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
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row != null && row.Cells[0].Value != null)
                    {
                        int skladisteno = 0;


                        //       string LOt = row.Cells["Lot"].Value.ToString();

                        int Tip = 2;


                        ins.DeletePromet(Convert.ToInt32(txtID.Text), "CPR");

                        // txtPrometID.Text = VratiIDPrometa().ToString();

                        //isporuka.InsertPrijemnicaPostav(Convert.ToInt32(row.Cells[0].Value), Convert.ToDecimal(row.Cells[1].Value), Convert.ToInt32(cbo_Skladiste.SelectedValue), cbo_Lokacija.SelectedValue.ToString(), cbo_MestoTroska.SelectedValue.ToString());
                        // progressBar1.Value = progressBar1.Value + 1;
                    }
                }

                InsertPrijemnicaCarinska updst = new InsertPrijemnicaCarinska();
                updst.updetePrijemnicaCarinskaStatus(Convert.ToInt32(txtID.Text), "OTVOREN");
            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frmPrijemnicaCarinskaStampa pcs = new frmPrijemnicaCarinskaStampa(txtID.Text);
            pcs.Show();
        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            staraVrednost = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var nova = dataGridView1[e.ColumnIndex, e.RowIndex].Value;

            if ((staraVrednost == null && nova != null) ||
                (staraVrednost != null && !staraVrednost.Equals(nova)))
            {
                promenaUFakturi = true;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Sprečava DataGridView da automatski briše red
                e.Handled = true;
                e.SuppressKeyPress = true;

                // Provjeri ima li odabranih redova
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    // Prikaži dijalog za potvrdu
                    DialogResult dr = MessageBox.Show("Jeste li sigurni da želite obrisati odabranu stavku?", "Potvrda brisanja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        // Uzmi ID retka koji se briše
                        int idZaBrisanje = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["ID"].Value);
                        int iDNadredjena = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells["IDNadredjena"].Value);
                        // Pozovi metodu za brisanje iz baze
                        InsertPrijemnicaCarinskaStavke ins = new InsertPrijemnicaCarinskaStavke();
                        ins.DelPrijemnicaCarinskaStavke(idZaBrisanje, iDNadredjena);

                        // Ukloni red iz DataGridView-a
                        this.dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);

                        MessageBox.Show("Stavka je uspješno obrisana.");
                    }
                }
                else
                {
                    MessageBox.Show("Nije odabrana nijedna stavka za brisanje.");
                }
            }
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            dataGridView1.EndEdit();

            if (!dataGridView1.IsCurrentRowDirty)
                return;

            // Provjeri je li bilo stvarnih promjena
            if (promenaUFakturi)
            {
                SaveRow(row);
                // Nakon uspješnog spremanja, resetiraj zastavicu
                promenaUFakturi = false;
            }
        }

        private void SaveRow(DataGridViewRow row)
        {
            // Provjeri je li red potpuno prazan
            bool isEmpty = true;
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.ReadOnly || cell.Value == null || cell.Value == DBNull.Value)
                    continue;

                if (!string.IsNullOrWhiteSpace(cell.Value.ToString()))
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                // Red je prazan, ne radi ništa.
                return;
            }

            // Učitaj ID
            var idCell = row.Cells["ID"].Value;
            bool isNew = (idCell == null || idCell == DBNull.Value || string.IsNullOrWhiteSpace(idCell.ToString()) || idCell.ToString() == "0");

            int id = 0;
            if (!isNew)
            {
                id = Convert.ToInt32(idCell);
            }
            if (string.IsNullOrWhiteSpace(txtID.Text) || !int.TryParse(txtID.Text, out int idNadredjena))
            {
                MessageBox.Show("Nije moguće snimiti stavku dok nije unet ID prijemnice.",
                                "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Očisti sva polja u formi (po želji dopuni šta sve brišeš)
                ClearGridFields();

                return;  // Prekini dalje snimanje
            }

            string artikal = row.Cells["Artikal"].Value?.ToString() ?? "";
            string jm = row.Cells["JM"].Value?.ToString() ?? "";
            string valuta = row.Cells["Valuta"].Value?.ToString() ?? "";
            string brojKontejnera = row.Cells["BrojKontejnera"].Value?.ToString() ?? "";
            string paleta = row.Cells["Paleta"].Value?.ToString() ?? "";
            string vrstaPalete = row.Cells["VrstaPalete"].Value?.ToString() ?? "";
            string dimenzije = row.Cells["Dimenzije"].Value?.ToString() ?? "";

            double? koleta = double.TryParse(row.Cells["Koleta"].Value?.ToString(), out var tmpKoleta) ? tmpKoleta : (double?)null;
            double? bruto = double.TryParse(row.Cells["Bruto"].Value?.ToString(), out var tmpBruto) ? tmpBruto : (double?)null;
            int? pozicija = int.TryParse(row.Cells["Pozicija"].Value?.ToString(), out var tmpPozicija) ? tmpPozicija : (int?)null;
            double? vrednost = double.TryParse(row.Cells["Vrednost"].Value?.ToString(), out var tmpVrednost) ? tmpVrednost : (double?)null;
            //   int prijemnicaStavkaID = int.TryParse(row.Cells["PrijemnicaStavkaID"].Value?.ToString(), out var tmpPrijemnica) ? tmpPrijemnica : 0;

            try
            {
                InsertPrijemnicaCarinskaStavke ins = new InsertPrijemnicaCarinskaStavke();

                if (isNew)
                {
                    // INSERT
                    //ins1.InsPrijemnicaCarinskaStavke(Convert.ToInt32(postojeciID), Convert.ToInt32(txtID.Text),
                    //   row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), Convert.ToDouble(row.Cells[4].Value),
                    //   Convert.ToDouble(row.Cells[5].Value)
                    //   , Convert.ToInt32(row.Cells[6].Value), Convert.ToDouble(row.Cells[7].Value), row.Cells[8].Value.ToString(),
                    //   brk, paleta, vrstp, dim);
                    int noviID = ins.InsPrijemnicaCarinskaStavke(0, idNadredjena, artikal, jm, koleta, bruto, pozicija, vrednost, valuta, brojKontejnera, paleta, vrstaPalete,
                                                                 dimenzije);
                    // Postavi novi ID u ćeliju
                    row.Cells["ID"].Value = noviID;
                    row.Cells["idNadredjena"].Value = idNadredjena;
                    row.ErrorText = "";
                }
                else
                {
                    // UPDATE
                    // Pozovi metodu za UPDATE
                    int rezultat = ins.InsPrijemnicaCarinskaStavke(id, idNadredjena, artikal, jm, koleta, bruto, pozicija, vrednost, valuta, brojKontejnera, paleta, vrstaPalete,
                                                                 dimenzije);
                }
            }
            catch (Exception ex)
            {
                row.ErrorText = ex.Message;
                MessageBox.Show("Greška pri snimanju: " + ex.Message);

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                     "Da li ste sigurni da želite da obrišete ovu prijemnicu?",
                     "Potvrda brisanja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                       );

            if (result == DialogResult.Yes)
            {
                InsertPrijemnicaCarinskaStavke ins = new InsertPrijemnicaCarinskaStavke();
                ins.DelPrijemnicaCarinsko(Convert.ToInt32(txtID.Text));
                ClearControls(this);
            }
        }

        private void ClearGridFields()
        {

            dataGridView1.Rows.Clear();
        }

        private void ClearControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is System.Windows.Forms.TextBox tb)
                    tb.Clear();
                else if (c is ComboBox cb)
                    cb.SelectedIndex = -1;
                else if (c is System.Windows.Forms.CheckBox chk)
                    chk.Checked = false;
                else if (c is DataGridView dgv)
                    dgv.DataSource = null; // ili gridGroupingControl1.DataSource = null
                else if (c.HasChildren)
                    ClearControls(c); // rekurzija za panel, groupbox itd.
            }

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.ClearSelection();
        }
    }
}

