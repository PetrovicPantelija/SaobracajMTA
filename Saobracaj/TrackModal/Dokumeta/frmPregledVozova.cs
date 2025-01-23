

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using Syncfusion.Windows.Forms;
using System.Drawing.Imaging;


namespace Testiranje.Dokumeta
{
    public partial class frmPregledVozova : Form
    {
        public static string code = "frmPregledVozova";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        string KorisnikCene = Saobracaj.Sifarnici.frmLogovanje.user.ToString();

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


        }

        public frmPregledVozova()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            ProveriFirmu();
            ChangeTextBox();


        }
        public frmPregledVozova(string Korisnik)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

            InitializeComponent();
            KorisnikCene = Korisnik;

            ProveriFirmu();
            ChangeTextBox();
        }

        public void ProveriFirmu()
        {
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        toolStripButton2.Text = "Vozovi uvoza";
                        toolStripButton4.Text = "Vozovi izvoza";
                        button24.Text = "Vozovi uvoza";
                        button25.Text = "Vozovi izvoza";
                        return;

                    }
                default:
                    {

                        
                        return;

                    }
                    break;
            }

        }
        
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void RefreshDataGrid()
        {
            var select = "SELECT [ID] ,[BrVoza],[Relacija] " +
         " ,[VremePolaska],[VremeDolaska],[MaksimalnaBruto],[MaksimalnaDuzina] " +
         " ,[MaksimalanBrojKola]" +
         " ,[Napomena],[Dolazeci] " +
         " ,[VremeDolaskaO],[PostNaTerminalD],[KontrolniPregledD],[VremeIstovaraD]" +
         " ,[VremePrimopredajeD],[PostNaTerminalO],[VremeUtovaraO],[VremeKontrolnogO] " +
         " ,[VremeIzvlacenjaO] ,[VremePolaskaO],[Datum] ,[Korisnik]" + 
         " FROM [dbo].[Voz] ";
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

             DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Br voza";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 150;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vr polaska";
            dataGridView1.Columns[3].Width = 70;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Vr dolaska";
            dataGridView1.Columns[4].Width = 70;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Max bruto";
            dataGridView1.Columns[5].Width = 70;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Max duž";
            dataGridView1.Columns[6].Width = 70;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Max br kola";
            dataGridView1.Columns[7].Width = 70;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Napomena";
            dataGridView1.Columns[8].Width = 100;
       
       
        }

        private void PrijemVozomPregled_Load(object sender, EventArgs e)
        {
          //  RefreshDataGrid();
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

                    }
                }


            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (txtSifra.Text != "")
            {
                frmVoz ter = new frmVoz(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                ter.Show();
            }
         
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            var select = "";
            string Company = Saobracaj.Sifarnici.frmLogovanje.Firma;
            switch (Company)
            {
                case "Leget":
                    {
                        select = " SELECT[Voz].[ID] ,UvozKonacnaZaglavlje.[ID] as PLANID,[Relacija], p1.PaNAziv as OperaterSRB, p2.PaNaziv as OperaterHR," +
                        " PristizanjaUSid as DV_PristizanjaUSid, Sazeta as DV_SAzeta, " +
                        " (SELECT  Count(*) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrojSpakovanihKonterjnera, " +
                        " (SELECT  SUM(UvozKonacna.TaraKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as TaraSpakovanihKonterjnera, " +
                        " (SELECT  SUM(UvozKonacna.BrutoKontejnera) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoSpakovanihKonterjnera, " +
                        " (SELECT  SUM(UvozKonacna.BrutoRobe) from UvozKonacna where UvozKonacna.IDNadredjeni = UvozKonacnaZaglavlje.ID and IDNadredjeni = UvozKonacnaZaglavlje.[ID]  ) as BrutoRobeSpakovanihKonterjnera, " +
                        " [MaksimalnaBruto],[MaksimalnaDuzina] " +
                        " ,[MaksimalanBrojKola] " +
                        " ,[Voz].Napomena " +
                        " ,Voz.[Datum] ,[Korisnik],[Dolazeci] " +
                        "  FROM [dbo].[Voz] " +
                         " Left join UvozKonacnaZaglavlje On Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
                         " inner join Partnerji p1 on p1.PaSifra = OperaterSrbija " +
                          " inner join Partnerji p2 on p2.PaSifra = OperaterHR " +
                         " where Dolazeci = 1 Order by [Voz].[ID] desc";
                        break;

                    }
                default:
                    {
                        select = "SELECT [ID] ,[BrVoza],[Relacija], " +
             " CONVERT(varchar,VremePolaska,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremePolaska,108),1,5) as VremePolaska, " +
             " CONVERT(varchar,[VremeDolaska],104)      + ' '      + SUBSTRING(CONVERT(varchar,[VremeDolaska],108),1,5)  as VremeDolaska, " +
             " [MaksimalnaBruto],[MaksimalnaDuzina] " +
             " ,[MaksimalanBrojKola] " +
             " ,[Napomena]" +
             " ,[PostNaTerminalD] as Postavka,[KontrolniPregledD] as Kontrolni ,[VremePrimopredajeD] as Primopredaja,[VremeIstovaraD] as Istovar" +
             " ,[Datum] ,[Korisnik],[Dolazeci] " +
             " FROM [dbo].[Voz] where Dolazeci = 1 ";

                       break;

                    }
                   
            }



            
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

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Voz_ID";
            dataGridView1.Columns[0].Width = 30;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Plan_ID";
            dataGridView1.Columns[1].Width = 30;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Relacija";
            dataGridView1.Columns[2].Width = 230;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var select = "SELECT [ID] ,[BrVoza],[Relacija] , NazivVoza, " +
             " CONVERT(varchar,VremePolaskaO,104)      + ' '      + SUBSTRING(CONVERT(varchar,VremePolaskaO,108),1,5) as VremePolaska, " +
              " CONVERT(varchar,[VremeDolaskaO],104)      + ' '      + SUBSTRING(CONVERT(varchar,[VremeDolaskaO],108),1,5)  as VremeDolaska, " +
           " [MaksimalnaBruto],[MaksimalnaDuzina] " +
           " ,[MaksimalanBrojKola] " +
           " ,[Napomena]   " +
           " ,[PostNaTerminalO] as Postavka, [VremeUtovaraO] as Utovar, [VremeKontrolnogO] as Kontrolni " +
           " ,[VremeIzvlacenjaO] as Izvlacenje ,[Datum] ,[Korisnik],[Dolazeci] " +
           " FROM [dbo].[Voz] where Dolazeci = 0 order by ID Desc";
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
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            InsertVoz ins = new InsertVoz();
            ins.PrekopirajVoz(Convert.ToInt32(txtSifra.Text));
        }
    }
}


