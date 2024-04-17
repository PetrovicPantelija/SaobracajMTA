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

namespace Saobracaj.RadniNalozi
{
    public partial class frmDodelaSkladista : Syncfusion.Windows.Forms.Office2010Form
    {
        int TipRadnogNaloga = 0;
        public frmDodelaSkladista()
        {
            InitializeComponent();
        }

        public frmDodelaSkladista(string Prijem, int TipRN)
        {
            InitializeComponent();
            TipRadnogNaloga = TipRN;
            textBox1.Text = Prijem;
            if (TipRN == 1)
            {
                label5.Text = "GATE IN VOZ";
                chkGateInVoz.Checked = true;
            };
            if (TipRN == 2)
            {
                label5.Text = "GATE IN KAMION - RN4";
                chkGAteInKamion.Checked = true;

            }
            if (TipRN == 3)
            {
                label5.Text = "CIR";
                chkCIR.Checked = true;
            }
            if (TipRN == 4)
            {
                label5.Text = "GATE IN KAMION S1";
            };
            if (TipRN == 6)
            {
                label5.Text = "Otprema ID";
            };

            
        }
        private void FillGVSkladista()
        {
            var select = "  Select ID, Naziv, Kapacitet ,  " +
" (Select Count(*) from KontejnerTekuce where KontejnerTekuce.Skladiste = Skladista.ID) as TrenutnoKontejnera " +
" From Skladista ";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kapacitet";
            dataGridView1.Columns[2].Width = 100;

        }
        private void frmDodelaSkladista_Load(object sender, EventArgs e)
        {

            FillGVSkladista();
           

        }

        private void FillDGRN1()
        {
            var select = "select RNPrijemVoza.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, DatumRasporeda, NalogIzdao, Voz.BrVoza, NaSkladiste,Skladista.Naziv as Sklad,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, VrstaManipulacije.Naziv as Usliga, BrojPlombe, RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID,RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, Zavrsen  from RNPrijemVoza " +
   " inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
   " inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
   " inner join Skladista on Skladista.ID = NaSkladiste " +
   " inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
   " inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
   " inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge where RNPrijemVoza.PrijemID = " + textBox1.Text + 
   " order by RNPrijemVoza.ID  ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void FillDGRN2()
        {
            // Prijem platforme kalmarista
            var select = "select * from RnPrijemPlatforme " +
   " order by RnPrijemPlatforme.ID  ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void FillDGRN4()
        {
            //Prijem platforme Uvoz - scenario 1
            var select = "  select RNPrijemPlatforme.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, DatumRasporeda, NalogIzdao, " +
" RNPrijemPlatforme.Kamion, USkladiste,Skladista.Naziv as Sklad,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, " +
" VrstaManipulacije.Naziv as Usliga, BrojPlombe, RNPrijemPlatforme.Napomena, RNPrijemPlatforme.PrijemID,RNPrijemPlatforme.NalogID, DatumRealizacije, " +
" NalogRealizovao, RNPrijemPlatforme.Zavrsen from RNPrijemPlatforme " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemPlatforme.VrstaKontejnera " +
" inner join Skladista on Skladista.ID = USkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemPlatforme.Izvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemPlatforme.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge where RNPrijemPlatforme.PrijemID = " + textBox1.Text +
   " order by RNPrijemVoza.ID  ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void FillDGRN6()
        {
            var select = "SELECT [RNOtpremaPlatforme].[ID] " +
    " ,[DatumRasporeda]      ,[BrojKontejnera] " +
   "    ,[VrstaKontejnera]      ,[NalogIzdao] " +
   "    ,[DatumRealizacije]      ,[Uvoznik] " +
   "    ,[CarinskiPostupak]      , VrstaCarinskogPostupka.Naziv as CarinskiPostupak " +
     "  ,[SpedicijaRTC]	  , p3.PaNaziv as SpedicijaRTC " +
    "   ,[NazivBrodara]      ,[VrstaRobe] " +
    "   ,[SaSkladista]	  , Skladista.Naziv " +
     "  ,[SaPozicijeSklad]	  , Pozicija.Opis " +
     "  ,[IdUsluge]      ,[NalogRealizovao] " +
    "   ,[OtpremaID]      ,[Kamion] " +
    "   ,[Zavrsen]      ,[NalogID] " +
     "        FROM [dbo].[RNOtpremaPlatforme] " +
 "  inner join skladista on skladista.id = SaSkladista " +
 "  inner join Pozicija on Pozicija.ID = [SaPozicijeSklad] " +
" inner join Partnerji on Partnerji.PaSifra = [RNOtpremaPlatforme].Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = [RNOtpremaPlatforme].NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = [CarinskiPostupak] " +
" inner join Partnerji p3 on p3.PaSifra = [RNOtpremaPlatforme].[SpedicijaRTC]  where OtpremaID = " + textBox1.Text;

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (TipRadnogNaloga == 1)
            FillDGRN1();

            if (TipRadnogNaloga == 6)
                FillDGRN6();
        }

        private void btnUnesi_Click(object sender, EventArgs e)
        {
            if (TipRadnogNaloga == 1)
            {
                //Prijem voza
                foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN1Skladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN1();

            }
            // Dodeljujemo privremeno skladiste na koje Kalmarista spusta
            if (TipRadnogNaloga == 2)
            {
                //Prijem voza
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN4PrivremenoSkladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN2(); //

            }

            if (TipRadnogNaloga == 3)
            {
                //Prijem voza
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN4SkladisteIzCIRA(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN2(); //

            }


            if (TipRadnogNaloga == 4)
            {
                //Napisano za PrijemPlatvorme Uvoz
              
            foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN6Skladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN6();

            }




            if (TipRadnogNaloga == 6)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN6Skladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN6();
            }

            }

        private void FillGVSkladistaSuzeno()
        {
            var select = "  Select ID, Naziv, Kapacitet ,  " +
" (Select Count(*) from KontejnerTekuce where KontejnerTekuce.Skladiste = Skladista.ID) as TrenutnoKontejnera " +
" From Skladista where Skladista.Naziv like ('%"+ textBox2.Text +"%') order by Skladista.Naziv";
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kapacitet";
            dataGridView1.Columns[2].Width = 100;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            FillGVSkladistaSuzeno();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Saobracaj.Proba pr = new Proba();
            pr.Show();
        }
    }
}
