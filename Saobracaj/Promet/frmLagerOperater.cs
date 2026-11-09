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
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;
using Syncfusion.Windows.Forms;

namespace Saobracaj.Promet
{
    public partial class frmLagerOperater : Form
    {
        public static string code = "frmLagerOperater";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        public frmLagerOperater()
        {
            InitializeComponent();
            ChangeTextBox();

        }

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            //  panelHeader.Visible = false;


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

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


        }


        private void btnPregled_Click(object sender, EventArgs e)
        {
            

  var select = "  Select BrojKontejnera, Sum(Kol) as Kolicina, X1.SkladisteR, Skladista.Naziv as Skladiste, Lokacija, Pozicija.Oznaka as Pozicija from " +
 " (select PrPrimKol as Kol, BrojKontejnera, SkladisteU as SkladisteR, LokacijaU as Lokacija " +
 " from Promet " +
 " where Zatvoren = 0 " +
 " union  select  PrIzdKol as Kol, BrojKontejnera, SkladisteIz as SkladisteR, LokacijaIz as Lokacija " +
 " from Promet  where Zatvoren = 0) as X1 " +
 "  inner join Skladista on Skladista.ID = SkladisteR " +
 "   inner join Pozicija on Pozicija.ID = Lokacija " +
 " group by BrojKontejnera, SkladisteR, Skladista.Naziv, Lokacija, Pozicija.Oznaka having Sum(Kol) > 0 and SkladisteR = " + Convert.ToInt32(cboSkladiste.SelectedValue);




            /*
                        var select = " SELECT DISTINCT Promet.[Id], Promet.[DatumTransakcije], Promet.[VrstaDokumenta], " +
                     " Promet.[PrSifVrstePrometa],Promet.[BrojKontejnera] " +
                     " ,Promet.[PrPrimKol] , Skladista.Naziv as Skladiste  , Pozicija.Oznaka " +
                     " , Promet.[PrOznSled]  ,Promet.[Datum] ,Promet.[Korisnik]  FROM [dbo].[Promet] inner join Skladista on Promet.SkladisteU = Skladista.ID " +
                     " inner join Pozicija on Pozicija.ID = Promet.LokacijaU " +
                     " inner join skladista as skladista1 on skladista1.ID = promet.SkladisteIz " +
                     " inner join pozicija as pozicija1 on Pozicija1.ID = Promet.LokacijaIz " +
                     " where Zatvoren = 0 and Promet.SkladisteU  = " + Convert.ToInt32(cboSkladiste.SelectedValue);

                            */

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            dataGridView1.Columns[0].HeaderText = "Kontejner";
            dataGridView1.Columns[0].Width = 140;
          

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Kol";
            dataGridView1.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Sklad ID";
            dataGridView1.Columns[2].Width = 50;

         

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Skladiste";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Poz id";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Pozicija";
            dataGridView1.Columns[5].Width = 100;

        }

        private void frmLagerOperater_Load(object sender, EventArgs e)
        {
            var select = " Select Distinct ID, (Rtrim(Broj) + '-' + Naziv) as NHM  From NHM ORDER BY ID";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboVrstaRobe.DataSource = ds.Tables[0];
            cboVrstaRobe.DisplayMember = "NHM";
            cboVrstaRobe.ValueMember = "ID";

            var select2 = " Select Distinct PaSifra, PaNaziv From Partnerji  order by PaNaziv";
            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboVlasnikKontejnera.DataSource = ds2.Tables[0];
            cboVlasnikKontejnera.DisplayMember = "PaNaziv";
            cboVlasnikKontejnera.ValueMember = "PaSifra";

            var select3 = " Select Distinct ID, Naziv   From Skladista";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboSkladiste.DataSource = ds3.Tables[0];
            cboSkladiste.DisplayMember = "Naziv";
            cboSkladiste.ValueMember = "ID";

            var select4 = " Select Distinct ID, Naziv From TipKontenjera order by Naziv";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboTipKontejnera.DataSource = ds4.Tables[0];
            cboTipKontejnera.DisplayMember = "Naziv";
            cboTipKontejnera.ValueMember = "ID";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var select = " Select x1.BrojKontejnera, Sum(Kol) as Kolicina, X1.SkladisteR, Skladista.Naziv as Skladiste, Lokacija, Pozicija.Oznaka as Pozicija, X1.PrOznSled from " +
" (select PrPrimKol as Kol, BrojKontejnera, SkladisteU as SkladisteR, LokacijaU as Lokacija, Promet.PrOznSled  from Promet  where Zatvoren = 0 " +
" union  select  PrIzdKol as Kol, BrojKontejnera, SkladisteIz as SkladisteR, LokacijaIz as Lokacija, Promet.PrOznSled  from Promet  where Zatvoren = 0) as X1 " +
" inner join Skladista on Skladista.ID = SkladisteR  inner join Pozicija on Pozicija.ID = Lokacija " +
" inner join PrijemKontejneraVozStavke on X1.PrOznSled = PrijemKontejneraVozStavke.ID " +
" group by x1.BrojKontejnera, SkladisteR, Skladista.Naziv, Lokacija, Pozicija.Oznaka, PrOznSled, PrijemKontejneraVozStavke.VrstaRobe having Sum(Kol) > 0 and SkladisteR = " + Convert.ToInt32(cboSkladiste.SelectedValue) + " and PrijemKontejneraVozStavke.VrstaRobe = " + Convert.ToInt32(cboVrstaRobe.SelectedValue);


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
          
            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            dataGridView1.Columns[0].HeaderText = "Kontejner";
            dataGridView1.Columns[0].Width = 140;


            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Kol";
            dataGridView1.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Sklad ID";
            dataGridView1.Columns[2].Width = 50;



            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Skladiste";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Poz id";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Pozicija";
            dataGridView1.Columns[5].Width = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var select = " Select x1.BrojKontejnera, Sum(Kol) as Kolicina, X1.SkladisteR, Skladista.Naziv as Skladiste, Lokacija, Pozicija.Oznaka as Pozicija, X1.PrOznSled from " +
" (select PrPrimKol as Kol, Promet.BrojKontejnera, SkladisteU as SkladisteR, LokacijaU as Lokacija, Promet.PrOznSled  from Promet  where Zatvoren = 0 " +
" union  select  PrIzdKol as Kol, Promet.BrojKontejnera, SkladisteIz as SkladisteR, LokacijaIz as Lokacija, Promet.PrOznSled  from Promet  where Zatvoren = 0) as X1 " +
" inner join Skladista on Skladista.ID = SkladisteR  inner join Pozicija on Pozicija.ID = Lokacija " +
" inner join PrijemKontejneraVozStavke on X1.PrOznSled = PrijemKontejneraVozStavke.ID " +
" group by x1.BrojKontejnera, SkladisteR, Skladista.Naziv, Lokacija, Pozicija.Oznaka, X1.PrOznSled, PrijemKontejneraVozStavke.TipKontejnera  having Sum(Kol) > 0 and SkladisteR = " + Convert.ToInt32(cboSkladiste.SelectedValue) + " and PrijemKontejneraVozStavke.TipKontejnera = " + Convert.ToInt32(cboTipKontejnera.SelectedValue)  ;

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            dataGridView1.Columns[0].HeaderText = "Kontejner";
            dataGridView1.Columns[0].Width = 140;


            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Kol";
            dataGridView1.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Sklad ID";
            dataGridView1.Columns[2].Width = 50;



            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Skladiste";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Poz id";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Pozicija";
            dataGridView1.Columns[5].Width = 100;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var select = " Select x1.BrojKontejnera, Sum(Kol) as Kolicina, X1.SkladisteR, Skladista.Naziv as Skladiste, Lokacija, Pozicija.Oznaka as Pozicija, X1.PrOznSled from " +
       " (select PrPrimKol as Kol, BrojKontejnera, SkladisteU as SkladisteR, LokacijaU as Lokacija, Promet.PrOznSled  from Promet  where Zatvoren = 0 " +
       " union  select  PrIzdKol as Kol, BrojKontejnera, SkladisteIz as SkladisteR, LokacijaIz as Lokacija, Promet.PrOznSled  from Promet  where Zatvoren = 0) as X1 " +
       " inner join Skladista on Skladista.ID = SkladisteR  inner join Pozicija on Pozicija.ID = Lokacija " +
       " inner join PrijemKontejneraVozStavke on X1.PrOznSled = PrijemKontejneraVozStavke.ID " +
       " group by x1.BrojKontejnera, SkladisteR, Skladista.Naziv, Lokacija, Pozicija.Oznaka, PrOznSled, PrijemKontejneraVozStavke.VlasnikKontejnera  having Sum(Kol) > 0 and SkladisteR = " + Convert.ToInt32(cboSkladiste.SelectedValue) + " and PrijemKontejneraVozStavke.VlasnikKontejnera = " + Convert.ToInt32(cboVlasnikKontejnera.SelectedValue); ;

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            dataGridView1.Columns[0].HeaderText = "Kontejner";
            dataGridView1.Columns[0].Width = 140;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Kol";
            dataGridView1.Columns[1].Width = 70;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Sklad ID";
            dataGridView1.Columns[2].Width = 50;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Skladiste";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Poz id";
            dataGridView1.Columns[4].Width = 50;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Pozicija";
            dataGridView1.Columns[5].Width = 100;
        }
    }
}
