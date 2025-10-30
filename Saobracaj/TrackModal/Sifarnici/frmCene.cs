using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
//PANTA
namespace Testiranje.Sifarnici
{
    public partial class frmCene : Form
    {
        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
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

        string KorisnikCene = Saobracaj.Sifarnici.frmLogovanje.user;
        public frmCene()
        {
            InitializeComponent();
            ChangeTextBox();

        }
        bool status = false;
        public frmCene(string Korisnik)
        {
            InitializeComponent();
            KorisnikCene = Korisnik;
            ChangeTextBox();

        }
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Text = "";
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,Cene.[Cena],Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik], VrstePostupakaUvoz.Naziv, p2.PaNaziv as UvoznikNazivCenovnika " +
     " , Razred      , OznakaManipulacije      , Cene.OrgJed FROM [dbo].[Cene] " +
            " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
            " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
            " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
            " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
            " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije order by ID desc";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Tip Cenovnika";
            dataGridView1.Columns[1].Width = 130;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Komitent";
            dataGridView1.Columns[2].Width = 250;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Cena JM/EUR";
            dataGridView1.Columns[3].Width = 80;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Cena JM2/EUR";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Vrsta manipulacije";
            dataGridView1.Columns[5].Width = 300;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Postupak sa robom";
            dataGridView1.Columns[8].Width = 100;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Uvoznik";
            dataGridView1.Columns[9].Width = 100;
        }

        private void RefreshDataGridPoCenovniku()
        {
            var select = " SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,Cene.[Cena],Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik] , VrstePostupakaUvoz.Naziv, p2.PaNaziv as UvoznikNazivCenovnika " +
      " , Razred      , OznakaManipulacije      , Cene.OrgJed FROM [dbo].[Cene] " +
            " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
            " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
            " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
             " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
            " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije " +
            " where Cene.TipCenovnika = " + cboTipCenovnika.SelectedValue +
            " order by ID desc";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
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
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Tip Cenovnika";
            dataGridView1.Columns[1].Width = 130;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Komitent";
            dataGridView1.Columns[2].Width = 250;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Cena JM/EUR";
            dataGridView1.Columns[3].Width = 80;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Cena JM2/EUR";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Vrsta manipulacije";
            dataGridView1.Columns[5].Width = 300;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Postupak sa robom";
            dataGridView1.Columns[8].Width = 100;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Uvoznik";
            dataGridView1.Columns[9].Width = 100;
        }

        private void RefreshDataGridPoPartneru()
        {
            var select = " SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,Cene.[Cena],Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik], VrstePostupakaUvoz.Naziv, p2.PaNaziv as Uvoznik, NazivCenovnika " +
      " , Razred      , OznakaManipulacije      , Cene.OrgJed FROM [dbo].[Cene] " +
            " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
            " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
             " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
             " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
            " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije where  Cene.Komitent = " + Convert.ToInt32(cboKomitent.SelectedValue) +
            "order by Cene.ID desc";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Tip Cenovnika";
            dataGridView1.Columns[1].Width = 130;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Komitent";
            dataGridView1.Columns[2].Width = 250;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Cena JM/EUR";
            dataGridView1.Columns[3].Width = 80;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Cena JM2/EUR";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Vrsta manipulacije";
            dataGridView1.Columns[5].Width = 300;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Postupak sa robom";
            dataGridView1.Columns[8].Width = 100;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Uvoznik";
            dataGridView1.Columns[9].Width = 100;
        }

        private void RefreshDataGridPoPartneruUvozniku()
        {
            var select = "SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,Cene.[Cena],Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik], VrstePostupakaUvoz.Naziv, p2.PaNaziv as Uvoznik, NazivCenovnika " +
      " , Razred      , OznakaManipulacije      , Cene.OrgJed FROM [dbo].[Cene] " +
            " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
            " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
             " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
             " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
            " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije where Cene.Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboKomitent.SelectedValue) +
            "order by Cene.ID desc";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Tip Cenovnika";
            dataGridView1.Columns[1].Width = 130;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Komitent";
            dataGridView1.Columns[2].Width = 250;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Cena JM/EUR";
            dataGridView1.Columns[3].Width = 80;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Cena JM2/EUR";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Vrsta manipulacije";
            dataGridView1.Columns[5].Width = 300;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Postupak sa robom";
            dataGridView1.Columns[8].Width = 100;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Uvoznik";
            dataGridView1.Columns[9].Width = 100;
        }


        private void tsSave_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            {
                status = true;
            }


            if (status == true)
            {
                InsertCene ins = new InsertCene();
                ins.InsCene(Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToDouble(txtCena2.Text), Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboPostupakSaRobom.SelectedValue), txtNazivCenovnika.Text, cboRazred.Text, txtOznakaManipulacije.Text, Convert.ToInt32(cboOrgJed.SelectedValue));
                status = false;
            }
            else
            {
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                InsertCene upd = new InsertCene();
                upd.UpdCene(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToDouble(txtCena2.Text), Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboPostupakSaRobom.SelectedValue), txtNazivCenovnika.Text, cboRazred.Text, txtOznakaManipulacije.Text, Convert.ToInt32(cboOrgJed.SelectedValue));
                status = false;

            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertCene upd = new InsertCene();
                upd.DeleteCene(Convert.ToInt32(txtSifra.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }

        private void VratiPodatke(string ID)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT ID , TipCenovnika , Komitent, Cena, VrstaManipulacije, Cena2, Uvoznik,PostupakSaRobom,NazivCenovnika ,Razred ,OznakaManipulacije ,OrgJed from Cene where ID=" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboTipCenovnika.SelectedValue = Convert.ToInt32(dr["TipCenovnika"].ToString());
                cboKomitent.SelectedValue = Convert.ToInt32(dr["Komitent"].ToString());
                cboVrstaManipulacije.SelectedValue = Convert.ToInt32(dr["VrstaManipulacije"].ToString());
                txtCena.Text = Convert.ToDecimal(dr["Cena"].ToString()).ToString();
                txtCena2.Text = Convert.ToDecimal(dr["Cena2"].ToString()).ToString();
                cboUvoznik.SelectedValue = Convert.ToInt32(dr["Uvoznik"].ToString());
                txtNazivCenovnika.Text = dr["NazivCenovnika"].ToString();
                cboRazred.Text = dr["Razred"].ToString();
                txtOznakaManipulacije.Text = dr["OznakaManipulacije"].ToString();
                cboOrgJed.SelectedValue = Convert.ToInt32(dr["OrgJed"].ToString());
            }

            con.Close();
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

        private void txtSifra_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmCene_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct PaSifra, PaNaziv  From Partnerji";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboKomitent.DataSource = ds.Tables[0];
            cboKomitent.DisplayMember = "PaNaziv";
            cboKomitent.ValueMember = "PaSifra";
            //
            var select3 = " select ID, Naziv from TipCenovnika";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboTipCenovnika.DataSource = ds3.Tables[0];
            cboTipCenovnika.DisplayMember = "Naziv";
            cboTipCenovnika.ValueMember = "ID";

            var select4 = " select ID, Naziv from VrstaManipulacije";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboVrstaManipulacije.DataSource = ds4.Tables[0];
            cboVrstaManipulacije.DisplayMember = "Naziv";
            cboVrstaManipulacije.ValueMember = "ID";


            // var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by Naziv";
            var select5 = " Select ID,Naziv from VrstePostupakaUvoz order by Naziv ";
            var s_connection5 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboPostupakSaRobom.DataSource = ds5.Tables[0];
            cboPostupakSaRobom.DisplayMember = "Naziv";
            cboPostupakSaRobom.ValueMember = "ID";


            var select6 = " Select PaSifra,PaNaziv from Partnerji order by PaNaziv ";
            var s_connection6 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboUvoznik.DataSource = ds6.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";

            var select2 = " SELECT ID, Naziv from OrganizacioneJedinice order by ID";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboOrgJed.DataSource = ds2.Tables[0];
            cboOrgJed.DisplayMember = "Naziv";
            cboOrgJed.ValueMember = "ID";





            RefreshDataGrid();
            /*
             if (KorisnikCene != "Kecman" && KorisnikCene != "Panta" && KorisnikCene != "Dušan Bašanović")
             {
                 //Dušan Bašanović
                 tsNew.Enabled = false;
                 tsSave.Enabled = false;
                 tsDelete.Enabled = false;
             }
            */
        }

        private void tsPrvi_Click(object sender, EventArgs e)
        {

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Min([ID]) as ID from Cene", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            VratiPodatke(txtSifra.Text);
            con.Close();

        }

        private void tsPoslednja_Click(object sender, EventArgs e)
        {


            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from Cene", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            VratiPodatke(txtSifra.Text);
            con.Close();



        }

        private void tsNazad_Click(object sender, EventArgs e)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int prvi = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from Cene where ID <" + Convert.ToInt32(txtSifra.Text) + " Order by ID desc", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                prvi = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = prvi.ToString();
            }

            con.Close();
            if ((Convert.ToInt32(txtSifra.Text) - 1) > prvi)
                VratiPodatke((Convert.ToInt32(txtSifra.Text) - 1).ToString());
            else
                VratiPodatke((Convert.ToInt32(prvi)).ToString());
        }

        private void VratiPodatkeMax()
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from Cene", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsNapred_Click(object sender, EventArgs e)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int zadnji = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID as ID from Cene where ID >" + Convert.ToInt32(txtSifra.Text) + " Order by ID", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                zadnji = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = zadnji.ToString();
            }

            con.Close();

            if ((Convert.ToInt32(txtSifra.Text) + 1) == zadnji)
                VratiPodatke((Convert.ToInt32(zadnji).ToString()));
            else
                VratiPodatke((Convert.ToInt32(txtSifra.Text) + 1).ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();

            // RefreshDataGridPoCenovniku();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    InsertCene ins = new InsertCene();
                    ins.KopirajCene(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(cboKomitent.SelectedValue));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoPartneru();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoPartneruUvozniku();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Saobracaj.TrackModal.Sifarnici.frmKopiranjeCenovnikaPoTipu kc = new Saobracaj.TrackModal.Sifarnici.frmKopiranjeCenovnikaPoTipu();
            kc.Show();
        }

        private void RefreshDataGridPoRazredu()
        {
            var select = " SELECT Cene.[ID] as ID ,[TipCenovnika].Naziv as TipCenovnika,[Partnerji].PaNaziv as Partner,Cene.[Cena],Cena2,[VrstaManipulacije].Naziv as VrstaManipulacije,Cene.[Datum],Cene.[Korisnik], VrstePostupakaUvoz.Naziv, p2.PaNaziv as Uvoznik, NazivCenovnika " +
      " , Razred      , OznakaManipulacije      , Cene.OrgJed FROM [dbo].[Cene] " +
               " inner join TipCenovnika on TipCenovnika.ID = Cene.TipCenovnika " +
               " inner join Partnerji on Partnerji.PaSifra = Cene.Komitent " +
                " inner join Partnerji p2 on p2.PaSifra = Cene.Uvoznik " +
                " inner join VrstePostupakaUvoz  on VrstePostupakaUvoz.ID = Cene.PostupakSaRobom " +
               " inner join VrstaManipulacije on VrstaManipulacije.Id = Cene.VrstaManipulacije where Cene.Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue) + " and Cene.Komitent = " + Convert.ToInt32(cboKomitent.SelectedValue) +
               "order by Cene.ID desc";
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Tip Cenovnika";
            dataGridView1.Columns[1].Width = 130;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Komitent";
            dataGridView1.Columns[2].Width = 250;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Cena JM/EUR";
            dataGridView1.Columns[3].Width = 80;


            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Cena JM2/EUR";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Vrsta manipulacije";
            dataGridView1.Columns[5].Width = 300;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Datum";
            dataGridView1.Columns[6].Width = 80;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Korisnik";
            dataGridView1.Columns[7].Width = 80;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Postupak sa robom";
            dataGridView1.Columns[8].Width = 100;


            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "Uvoznik";
            dataGridView1.Columns[9].Width = 100;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshDataGridPoRazredu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtNazivCenovnika.Text = cboUvoznik.Text + cboKomitent.Text;
        }
    
    
    }
}

