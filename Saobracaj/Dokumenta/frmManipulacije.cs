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
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Security.Cryptography;
using Saobracaj.Sifarnici;

//Panta
namespace Saobracaj.Dokumenta
{
    public partial class frmManipulacije : Syncfusion.Windows.Forms.Office2010Form
    {
        public static string code = "frmManipulacije";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        string KorisnikCene;
        int pomPrijemnica = 0;
        int pomVozom = 0;
        int IzPrijemnice = 0;
        bool usao = false;
        //  bool status = false;
        public frmManipulacije()
        {
            InitializeComponent();

        }

        public frmManipulacije(string Korisnik)
        {
            KorisnikCene = Korisnik;
            InitializeComponent();

        }

        public frmManipulacije(string Korisnik, int PrijemnicaOtpremnica, int Vozom, int IzPrijema)
        {
            if (IzPrijema == 0)
            {
                IzPrijemnice = 0; //Podatak dosao iz Prijema
            }
            else
            {
                IzPrijemnice = 1; // Podatak Dosao iz Otpreme
            }
            pomPrijemnica = PrijemnicaOtpremnica;
            pomVozom = Vozom;



            KorisnikCene = Korisnik;
            InitializeComponent();

            if (pomVozom == 1)
            { chkVoz.Checked = true; }

            // RefreshDataGrid3();
        }


        private void frmManipulacije_Load(object sender, EventArgs e)
        {
            dtpVremeOd.Value = DateTime.Now;
            dtpVremeDo.Value = DateTime.Now;

            var select3 = " Select Distinct ID, Naziv From Skladista";
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

            var select4 = " Select Distinct ID, Naziv From Skladista";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboSkladIz.DataSource = ds4.Tables[0];
            cboSkladIz.DisplayMember = "Naziv";
            cboSkladIz.ValueMember = "ID";

            var selectp = " Select ID, Oznaka From Pozicija ";
            var s_connectionp = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnectionp = new SqlConnection(s_connectionp);
            var cp = new SqlConnection(s_connectionp);
            var dataAdapterp = new SqlDataAdapter(selectp, cp);

            var commandBuilderp = new SqlCommandBuilder(dataAdapterp);
            var dsp = new DataSet();
            dataAdapterp.Fill(dsp);
            cboPozIz.DataSource = dsp.Tables[0];
            cboPozIz.DisplayMember = "Oznaka";
            cboPozIz.ValueMember = "ID";
            usao = true;




            // var select = " Select ID From PrijemKontejneraVoz";
            if (IzPrijemnice == 1)
            {
                if (pomVozom == 1)
                {
                    var select = "SELECT PrijemKontejneraVoz.[ID], (CAst(PrijemKontejneraVoz.[ID] as nvarchar(5)) + '-' + Cast(Voz.BrVoza as nvarchar(6)) + ' ' + Voz.Relacija +  ' ' +  CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5)) as Naziv " +
                            " FROM [dbo].[PrijemKontejneraVoz]    inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza  where PrijemKontejneraVoz.[ID] = " + pomPrijemnica + "  order by  PrijemKontejneraVoz.[DatumPrijema] desc, PrijemKontejneraVoz.[ID] ";
                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    cboPrijemVozom.DataSource = ds.Tables[0];
                    cboPrijemVozom.DisplayMember = "Naziv";
                    cboPrijemVozom.ValueMember = "ID";
                    chkPrijem.Checked = true;
                    cboPrijemKamionom.Enabled = false;
                    chkVoz.Checked = true;

                    PretraziKontejnereVozomIzPrijemnice();
                    // Ovde treba refreh kontejnera

                }
                else
                {
                    var select2 = "SELECT PrijemKontejneraVoz.[ID], (CAst(PrijemKontejneraVoz.[ID] as nvarchar(5)) + '-' + RegBrKamiona + ' ' + ImeVozaca +  ' ' +   CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5)) as Naziv " +
                    " FROM [dbo].[PrijemKontejneraVoz]  where Vozom = 0 and PrijemKontejneraVoz.ID = " + pomPrijemnica + "   order by  PrijemKontejneraVoz.[DatumPrijema] desc, PrijemKontejneraVoz.[ID] ";

                    var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection2 = new SqlConnection(s_connection2);
                    var c2 = new SqlConnection(s_connection2);
                    var dataAdapter2 = new SqlDataAdapter(select2, c2);

                    var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                    var ds2 = new DataSet();
                    dataAdapter2.Fill(ds2);
                    cboPrijemKamionom.DataSource = ds2.Tables[0];
                    cboPrijemKamionom.DisplayMember = "Naziv";
                    cboPrijemKamionom.ValueMember = "ID";

                    chkPrijem.Checked = true;
                    cboPrijemVozom.Enabled = false;

                    chkVoz.Checked = false;
                    PretraziKontejnereKamionomIzPrijemnice();
                }
            }
            else if (IzPrijemnice == 0)
            {
                //Panta
                if (pomVozom == 1)
                {
                    var select = "SELECT OtpremaKontejnera.[ID], (CAst(OtpremaKontejnera.[ID] as nvarchar(5)) + '-' + Cast(Voz.BrVoza as nvarchar(6)) + ' ' + Voz.Relacija + ' ' +  " +
" CONVERT(varchar, OtpremaKontejnera.[DatumOtpreme], 104) + ' ' + " +
" SUBSTRING(CONVERT(varchar, OtpremaKontejnera.[DatumOtpreme], 108), 1, 5)) as Naziv " +
" FROM [dbo].[OtpremaKontejnera]    inner join Voz on Voz.ID = OtpremaKontejnera.IdVoza  where OtpremaKontejnera.[ID] = " + pomPrijemnica + " and otpremakontejnera.NacinOtpreme = 1 order by OtpremaKontejnera.[DatumOtpreme] desc, OtpremaKontejnera.[ID]";
                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    cboPrijemVozom.DataSource = ds.Tables[0];
                    cboPrijemVozom.DisplayMember = "Naziv";
                    cboPrijemVozom.ValueMember = "ID";
                    cboPrijemVozom.SelectedValue = pomPrijemnica;
                    PretraziKontejnereVozomIzOtpremnice();
                }
                else
                {
                    var select2 = "SELECT OtpremaKontejnera.[ID], (CAst(OtpremaKontejnera.[ID] as nvarchar(5)) + '-' + (CAst(OtpremaKontejnera.[ID] as nvarchar(5)) + '-' + OtpremaKontejnera.RegBrKamiona + ' ' + OtpremaKontejnera.ImeVozaca +  ' ' + " +
                   " CONVERT(varchar, OtpremaKontejnera.[DatumOtpreme], 104) + ' ' + " +
                   " SUBSTRING(CONVERT(varchar, OtpremaKontejnera.[DatumOtpreme], 108), 1, 5))) as Naziv " +
                   " FROM [dbo].[OtpremaKontejnera]     where OtpremaKontejnera.[ID] = " + pomPrijemnica + " and otpremakontejnera.NacinOtpreme = 0 order by OtpremaKontejnera.[DatumOtpreme] desc, OtpremaKontejnera.[ID]";

                    var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection2 = new SqlConnection(s_connection2);
                    var c2 = new SqlConnection(s_connection2);
                    var dataAdapter2 = new SqlDataAdapter(select2, c2);

                    var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                    var ds2 = new DataSet();
                    dataAdapter2.Fill(ds2);
                    cboPrijemKamionom.DataSource = ds2.Tables[0];
                    cboPrijemKamionom.DisplayMember = "Naziv";
                    cboPrijemKamionom.ValueMember = "ID";
                    cboPrijemKamionom.SelectedValue = pomPrijemnica;
                    PretraziKontejnereKamionomIzOtpremnice();
                }


                //

                //Kod za ulazak a nije iz prijemnice tj sa otpremnice
                /*
                 var select = "SELECT PrijemKontejneraVoz.[ID], (CAst(PrijemKontejneraVoz.[ID] as nvarchar(5)) + '-' + Cast(Voz.BrVoza as nvarchar(6)) + ' ' + Voz.Relacija +  ' ' +  CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5)) as Naziv " +
                 " FROM [dbo].[PrijemKontejneraVoz]    inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza  where Vozom = 1 order by  PrijemKontejneraVoz.[DatumPrijema] desc, PrijemKontejneraVoz.[ID] ";
                 var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                 SqlConnection myConnection = new SqlConnection(s_connection);
                 var c = new SqlConnection(s_connection);
                 var dataAdapter = new SqlDataAdapter(select, c);

                 var commandBuilder = new SqlCommandBuilder(dataAdapter);
                 var ds = new DataSet();
                 dataAdapter.Fill(ds);
                 cboPrijemVozom.DataSource = ds.Tables[0];
                 cboPrijemVozom.DisplayMember = "Naziv";
                 cboPrijemVozom.ValueMember = "ID";
                 var select2 = "SELECT PrijemKontejneraVoz.[ID], (CAst(PrijemKontejneraVoz.[ID] as nvarchar(5)) + '-' + RegBrKamiona + ' ' + ImeVozaca +  ' ' +   CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5)) as Naziv " +
       " FROM [dbo].[PrijemKontejneraVoz]  where Vozom = 0  order by  PrijemKontejneraVoz.[DatumPrijema] desc, PrijemKontejneraVoz.[ID] ";

                 var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                 SqlConnection myConnection2 = new SqlConnection(s_connection2);
                 var c2 = new SqlConnection(s_connection2);
                 var dataAdapter2 = new SqlDataAdapter(select2, c2);

                 var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
                 var ds2 = new DataSet();
                 dataAdapter2.Fill(ds2);
                 cboPrijemKamionom.DataSource = ds2.Tables[0];
                 cboPrijemKamionom.DisplayMember = "Naziv";
                 cboPrijemKamionom.ValueMember = "ID";
                */

            }
            var select44 = " Select Distinct PaSifra, PaNaziv From Partnerji order by PaNaziv";
            var s_connection44 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection444 = new SqlConnection(s_connection44);
            var c44 = new SqlConnection(s_connection44);
            var dataAdapter44 = new SqlDataAdapter(select44, c44);

            var commandBuilder44 = new SqlCommandBuilder(dataAdapter44);
            var ds44 = new DataSet();
            dataAdapter44.Fill(ds44);
            cboPlatilac.DataSource = ds44.Tables[0];
            cboPlatilac.DisplayMember = "PaNaziv";
            cboPlatilac.ValueMember = "PaSifra";

            RefreshManipulacije();
            RefreshDataGrid3();
            cboSkladIz.SelectedValue = Sifarnici.frmLogovanje.Skladiste;
            cboPozIz.SelectedValue = Sifarnici.frmLogovanje.Lokacija;

        }

        private void PretraziKontejnereVozomIzPrijemnice()
        {
            chkPrijem.Checked = true;
            chkVoz.Checked = true;
            var select = "  Select BrojKontejnera, IDNAdredjenog as Dokument   From PrijemKontejneraVozStavke where IDNadredjenog = " + Convert.ToInt32(cboPrijemVozom.SelectedValue);
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[0].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[0].Width = 100;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Dokument prijema voza";
            dataGridView1.Columns[1].Width = 100;


        }

        private void PretraziKontejnereKamionomIzPrijemnice()
        {

            chkVoz.Checked = false;
            var select = "  Select BrojKontejnera, IDNAdredjenog as Dokument   From PrijemKontejneraVozStavke where IDNadredjenog = " + Convert.ToInt32(cboPrijemKamionom.SelectedValue);
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[0].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[0].Width = 100;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Dokument prijema";
            dataGridView1.Columns[1].Width = 100;

        }

        private void PretraziKontejnereVozomIzOtpremnice()
        {
            chkPrijem.Checked = false;
            chkOtprema.Checked = true;
            chkVoz.Checked = true;
            cboPrijemKamionom.Enabled = false;
            var select = "  Select BrojKontejnera, IDNAdredjenog as Dokument   From OtpremaKontejneraVozStavke where IDNadredjenog = " + Convert.ToInt32(cboPrijemVozom.SelectedValue);
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[0].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[0].Width = 100;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Dokument prijema voza";
            dataGridView1.Columns[1].Width = 100;


        }

        private void PretraziKontejnereKamionomIzOtpremnice()
        {
            cboPrijemVozom.Enabled = false;
            chkOtprema.Checked = true;
            chkPrijem.Checked = false;
            chkVoz.Checked = false;
            var select = "  Select BrojKontejnera, IDNAdredjenog as Dokument   From OtpremaKontejneraVozStavke where IDNadredjenog = " + Convert.ToInt32(cboPrijemKamionom.SelectedValue);
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
            dataGridView1.Columns[0].HeaderText = "Broj kontejnera";
            dataGridView1.Columns[0].Width = 100;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Dokument prijema";
            dataGridView1.Columns[1].Width = 100;

        }

        private void btnPretrazi_Click(object sender, EventArgs e)
        {
            if (chkPrijem.Checked == true)
            {
                chkVoz.Checked = true;
                var select = "  Select BrojKontejnera, IDNAdredjenog as Dokument   From PrijemKontejneraVozStavke where IDNadredjenog = " + Convert.ToInt32(cboPrijemVozom.SelectedValue);
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                dataGridView1.Columns[0].HeaderText = "Broj kontejnera";
                dataGridView1.Columns[0].Width = 100;

                DataGridViewColumn column1 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Dokument prijema voza";
                dataGridView1.Columns[1].Width = 100;
            }
            else
            {
                chkVoz.Checked = true;
                var select = "  Select BrojKontejnera, IDNAdredjenog as Dokument   From OtpremaKontejneraVozStavke where IDNadredjenog = " + Convert.ToInt32(cboPrijemVozom.SelectedValue);
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                dataGridView1.Columns[0].HeaderText = "Broj kontejnera";
                dataGridView1.Columns[0].Width = 100;

                DataGridViewColumn column1 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Dokument otpreme voza";
                dataGridView1.Columns[1].Width = 100;

            }

            RefreshDataGrid3();
        }

        private void PovuciPodatkeSaPrijema()
        {
            if (chkVoz.Checked == true)
            {
                var select = "   select  NaruceneManipulacije.IDPrijemaVoza, NaruceneManipulacije.BrojKontejnera, VrstaManipulacije.ID, VrstaManipulacije.Naziv, " +
 " CASE WHEN NaruceneManipulacije.Uradjeno > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Uradjeno, " +
 " NaruceneManipulacije.DatumOd,NaruceneManipulacije.DatumDo, NaruceneManipulacije.Datum, NaruceneManipulacije.Korisnik,  NaruceneManipulacije.ID, Partnerji.PaNaziv As Platilac" +
 " , Skladista.Naziv as Skladiste, Pozicija.Oznaka from NaruceneManipulacije " +
" inner join VrstaManipulacije on NaruceneManipulacije.VrstaManipulacije = VrstaManipulacije.ID " +
" inner join Partnerji on NaruceneManipulacije.Platilac = Partnerji.PaSifra " +
" inner join Skladista on Skladista.ID = NaruceneManipulacije.Sklad inner join Pozicija on Pozicija.ID = NaruceneManipulacije.Poz " +
               " where IDPrijemaVoza = " + Convert.ToInt32(cboPrijemVozom.SelectedValue);

                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView3.ReadOnly = false;
                dataGridView3.DataSource = ds.Tables[0];

                DataGridViewColumn column = dataGridView3.Columns[0];
                dataGridView3.Columns[0].HeaderText = "Prijem";
                dataGridView3.Columns[0].Width = 40;
                // dataGridView2.Columns[0].Visible = false;

                DataGridViewColumn column2 = dataGridView3.Columns[1];
                dataGridView3.Columns[1].HeaderText = "Broj kontejnera";
                dataGridView3.Columns[1].Width = 100;

                DataGridViewColumn column3 = dataGridView3.Columns[2];
                dataGridView3.Columns[2].HeaderText = "Man ID";
                dataGridView3.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView3.Columns[3];
                dataGridView3.Columns[3].HeaderText = "Manipulacija";
                dataGridView3.Columns[3].Width = 150;



                DataGridViewColumn column5 = dataGridView3.Columns[4];
                dataGridView3.Columns[4].HeaderText = "Urađeno";
                dataGridView3.Columns[4].Width = 50;

                DataGridViewColumn column6 = dataGridView3.Columns[5];
                dataGridView3.Columns[5].HeaderText = "Datum od";
                dataGridView3.Columns[5].Width = 80;

                DataGridViewColumn column7 = dataGridView3.Columns[6];
                dataGridView3.Columns[6].HeaderText = "Datum do";
                dataGridView3.Columns[6].Width = 80;

                DataGridViewColumn column8 = dataGridView3.Columns[7];
                dataGridView3.Columns[7].HeaderText = "Datum";
                dataGridView3.Columns[7].Width = 80;

                DataGridViewColumn column9 = dataGridView3.Columns[8];
                dataGridView3.Columns[8].HeaderText = "Korisnik";
                dataGridView3.Columns[8].Width = 80;

                DataGridViewColumn column10 = dataGridView3.Columns[9];
                dataGridView3.Columns[9].HeaderText = "ID";
                dataGridView3.Columns[9].Width = 70;




            }
            else
            {
                var select = "  select NaruceneManipulacije.IDPrijemaKamionom, NaruceneManipulacije.BrojKontejnera, VrstaManipulacije.ID, VrstaManipulacije.Naziv, " +
                    " NaruceneManipulacije.Uradjeno, NaruceneManipulacije.DatumOd,NaruceneManipulacije.DatumDo, NaruceneManipulacije.Datum, NaruceneManipulacije.Korisnik, " +
                    " NaruceneManipulacije.ID, Partnerji.PaNaziv  as Platilac" +
                    " , Skladista.Naziv as Skladiste, Pozicija.Oznaka from NaruceneManipulacije " +
            " inner join VrstaManipulacije on NaruceneManipulacije.VrstaManipulacije = VrstaManipulacije.ID " +
            " inner join Partnerji on NaruceneManipulacije.Platilac = Partnerji.PaSifra" +
            "  inner join Skladista on Skladista.ID = NaruceneManipulacije.Sklad inner join Pozicija on Pozicija.ID = NaruceneManipulacije.Poz  " +
             " where IDPrijemaKamionom = " + Convert.ToInt32(cboPrijemKamionom.SelectedValue);

                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView3.ReadOnly = false;
                dataGridView3.DataSource = ds.Tables[0];



                DataGridViewColumn column = dataGridView3.Columns[0];
                dataGridView3.Columns[0].HeaderText = "Prijem kamionom";
                dataGridView3.Columns[0].Width = 40;
                // dataGridView2.Columns[0].Visible = false;

                DataGridViewColumn column2 = dataGridView3.Columns[1];
                dataGridView3.Columns[1].HeaderText = "Broj kontejnera";
                dataGridView3.Columns[1].Width = 100;

                DataGridViewColumn column3 = dataGridView3.Columns[2];
                dataGridView3.Columns[2].HeaderText = "Man ID";
                dataGridView3.Columns[2].Visible = false;
                dataGridView3.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView3.Columns[3];
                dataGridView3.Columns[3].HeaderText = "Manipulacija";
                dataGridView3.Columns[3].Width = 150;

                DataGridViewColumn column5 = dataGridView3.Columns[4];
                dataGridView3.Columns[4].HeaderText = "Urađeno";
                dataGridView3.Columns[4].Width = 50;

                DataGridViewColumn column6 = dataGridView3.Columns[5];
                dataGridView3.Columns[5].HeaderText = "Datum od";
                dataGridView3.Columns[5].Width = 80;

                DataGridViewColumn column7 = dataGridView3.Columns[6];
                dataGridView3.Columns[6].HeaderText = "Datum do";
                dataGridView3.Columns[6].Width = 80;

                DataGridViewColumn column8 = dataGridView3.Columns[7];
                dataGridView3.Columns[7].HeaderText = "Datum";
                dataGridView3.Columns[7].Width = 80;

                DataGridViewColumn column9 = dataGridView3.Columns[8];
                dataGridView3.Columns[8].HeaderText = "Korisnik";
                dataGridView3.Columns[8].Width = 80;

                DataGridViewColumn column10 = dataGridView3.Columns[8];
                dataGridView3.Columns[9].HeaderText = "ID";
                dataGridView3.Columns[9].Width = 80;

            }
        }

        private void PovuciPodatkeSaOtpreme()
        {
            if (chkVoz.Checked == true)
            {
                var select = "   select  NaruceneManipulacije.IDPrijemaVoza, NaruceneManipulacije.BrojKontejnera, VrstaManipulacije.ID, VrstaManipulacije.Naziv, " +
 " CASE WHEN NaruceneManipulacije.Uradjeno > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Uradjeno, " +
 " NaruceneManipulacije.DatumOd,NaruceneManipulacije.DatumDo, NaruceneManipulacije.Datum, NaruceneManipulacije.Korisnik,  NaruceneManipulacije.ID, " +
 " Partnerji.PaNaziv as Platilac  , Skladista.Naziv as Skladiste, Pozicija.Oznaka from NaruceneManipulacije " +
" inner join VrstaManipulacije on NaruceneManipulacije.VrstaManipulacije = VrstaManipulacije.ID " +
" inner join Partnerji on NaruceneManipulacije.Platilac = Partnerji.PaSifra " +
  "  inner join Skladista on Skladista.ID = NaruceneManipulacije.Sklad inner join Pozicija on Pozicija.ID = NaruceneManipulacije.Poz  " +
               " where IDPrijemaVoza = " + Convert.ToInt32(cboPrijemVozom.SelectedValue);

                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView3.ReadOnly = false;
                dataGridView3.DataSource = ds.Tables[0];

                DataGridViewColumn column = dataGridView3.Columns[0];
                dataGridView3.Columns[0].HeaderText = "Prijem";
                dataGridView3.Columns[0].Width = 40;
                // dataGridView2.Columns[0].Visible = false;

                DataGridViewColumn column2 = dataGridView3.Columns[1];
                dataGridView3.Columns[1].HeaderText = "Broj kontejnera";
                dataGridView3.Columns[1].Width = 100;

                DataGridViewColumn column3 = dataGridView3.Columns[2];
                dataGridView3.Columns[2].HeaderText = "Man ID";
                dataGridView3.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView3.Columns[3];
                dataGridView3.Columns[3].HeaderText = "Manipulacija";
                dataGridView3.Columns[3].Width = 50;



                DataGridViewColumn column5 = dataGridView3.Columns[4];
                dataGridView3.Columns[4].HeaderText = "Urađeno";
                dataGridView3.Columns[4].Width = 50;

                DataGridViewColumn column6 = dataGridView3.Columns[5];
                dataGridView3.Columns[5].HeaderText = "Datum od";
                dataGridView3.Columns[5].Width = 80;

                DataGridViewColumn column7 = dataGridView3.Columns[6];
                dataGridView3.Columns[6].HeaderText = "Datum do";
                dataGridView3.Columns[6].Width = 80;

                DataGridViewColumn column8 = dataGridView3.Columns[7];
                dataGridView3.Columns[7].HeaderText = "Datum";
                dataGridView3.Columns[7].Width = 80;

                DataGridViewColumn column9 = dataGridView3.Columns[8];
                dataGridView3.Columns[8].HeaderText = "Korisnik";
                dataGridView3.Columns[8].Width = 80;

                DataGridViewColumn column10 = dataGridView3.Columns[9];
                dataGridView3.Columns[9].HeaderText = "ID";
                dataGridView3.Columns[9].Width = 70;




            }
            else
            {
                var select = "  select NaruceneManipulacije.IDPrijemaKamionom, NaruceneManipulacije.BrojKontejnera, VrstaManipulacije.ID, " +
                "VrstaManipulacije.Naziv, NaruceneManipulacije.Uradjeno, NaruceneManipulacije.DatumOd,NaruceneManipulacije.DatumDo, " +
                "NaruceneManipulacije.Datum, NaruceneManipulacije.Korisnik, NaruceneManipulacije.ID, PaNaziv As Platilac" +
                "  , Skladista.Naziv as Skladiste, Pozicija.Oznaka from NaruceneManipulacije " +
            " inner join VrstaManipulacije on NaruceneManipulacije.VrstaManipulacije = VrstaManipulacije.ID " +
                " inner join Partnerji on NaruceneManipulacije.Platilac = Partnerji.PaSifra " +
                  "  inner join Skladista on Skladista.ID = NaruceneManipulacije.Sklad inner join Pozicija on Pozicija.ID = NaruceneManipulacije.Poz  " +
             " where IDPrijemaKamionom = " + Convert.ToInt32(cboPrijemKamionom.SelectedValue);

                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView3.ReadOnly = false;
                dataGridView3.DataSource = ds.Tables[0];



                DataGridViewColumn column = dataGridView3.Columns[0];
                dataGridView3.Columns[0].HeaderText = "Otprema kamionom";
                dataGridView3.Columns[0].Width = 40;
                // dataGridView2.Columns[0].Visible = false;

                DataGridViewColumn column2 = dataGridView3.Columns[1];
                dataGridView3.Columns[1].HeaderText = "Broj kontejnera";
                dataGridView3.Columns[1].Width = 100;

                DataGridViewColumn column3 = dataGridView3.Columns[2];
                dataGridView3.Columns[2].HeaderText = "Man ID";
                dataGridView3.Columns[2].Width = 50;

                DataGridViewColumn column4 = dataGridView3.Columns[3];
                dataGridView3.Columns[3].HeaderText = "Manipulacija";
                dataGridView3.Columns[3].Width = 50;

                DataGridViewColumn column5 = dataGridView3.Columns[4];
                dataGridView3.Columns[4].HeaderText = "Urađeno";
                dataGridView3.Columns[4].Width = 50;

                DataGridViewColumn column6 = dataGridView3.Columns[5];
                dataGridView3.Columns[5].HeaderText = "Datum od";
                dataGridView3.Columns[5].Width = 80;

                DataGridViewColumn column7 = dataGridView3.Columns[6];
                dataGridView3.Columns[6].HeaderText = "Datum do";
                dataGridView3.Columns[6].Width = 80;

                DataGridViewColumn column8 = dataGridView3.Columns[7];
                dataGridView3.Columns[7].HeaderText = "Datum";
                dataGridView3.Columns[7].Width = 80;

                DataGridViewColumn column9 = dataGridView3.Columns[8];
                dataGridView3.Columns[8].HeaderText = "Korisnik";
                dataGridView3.Columns[8].Width = 80;

                DataGridViewColumn column10 = dataGridView3.Columns[8];
                dataGridView3.Columns[9].HeaderText = "ID";
                dataGridView3.Columns[9].Width = 80;

            }


        }


        private void RefreshDataGrid3()
        {
            if (chkPrijem.Checked == true)
            {

                PovuciPodatkeSaPrijema();


            }
            else
            {
                PovuciPodatkeSaOtpreme();

            }



        }

        private void RefreshManipulacije()
        {
            var select = "  SELECT ID, Naziv, JM, CASE WHEN UticeSkladisno > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as UticeSkladisno from VrstaManipulacije";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];



            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 40;
            // dataGridView2.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Manipulacija";
            dataGridView2.Columns[1].Width = 250;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "JM";
            dataGridView2.Columns[2].Width = 80;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "Skladišno";
            dataGridView2.Columns[3].Width = 80;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chkPrijem.Checked == true)
            {
                chkVoz.Checked = false;
                var select = "  Select BrojKontejnera, IDNAdredjenog as Dokument   From PrijemKontejneraVozStavke where IDNadredjenog = " + Convert.ToInt32(cboPrijemKamionom.SelectedValue);
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                dataGridView1.Columns[0].HeaderText = "Broj kontejnera";
                dataGridView1.Columns[0].Width = 100;

                DataGridViewColumn column1 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Dokument prijema";
                dataGridView1.Columns[1].Width = 100;
            }
            else
            {
                chkVoz.Checked = false;
                var select = "  Select BrojKontejnera, IDNAdredjenog as Dokument   From OtpremaKontejneraVozStavke where IDNadredjenog = " + Convert.ToInt32(cboPrijemKamionom.SelectedValue);
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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
                dataGridView1.Columns[0].HeaderText = "Broj kontejnera";
                dataGridView1.Columns[0].Width = 100;

                DataGridViewColumn column1 = dataGridView1.Columns[1];
                dataGridView1.Columns[1].HeaderText = "Dokument otpreme";
                dataGridView1.Columns[1].Width = 100;
            }

            RefreshDataGrid3();
        }

        private void btnUnesi_Click(object sender, EventArgs e)
        {
            if (txtSifra.Text == "")
            { MessageBox.Show("Morate oformiti dokument"); return; }
            int IzPrijema = 0;
            int Direktna = 0;
            int PunPrazan = 0;

            if (chkPunPrazan.Checked == true)
            {
                PunPrazan = 1;

            }


            if (chkPrijem.Checked == true)
            { IzPrijema = 1; }
            else
            {
                IzPrijema = 0;
            }
            if (chkDirektna.Checked == true)
            {
                Direktna = 1;
            }
            else
            {
                Direktna = 0;
            }


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                InsertNaruceneManipulacije ins = new InsertNaruceneManipulacije();

                if (row.Selected == true)
                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        if (chkVoz.Checked == true && row2.Selected == true)
                        {
                            ins.InsertNarManipulacije(Convert.ToInt32(cboPrijemVozom.SelectedValue), 0, row.Cells[0].Value.ToString(), Convert.ToInt32(row2.Cells[0].Value.ToString()), 0, Convert.ToDateTime(dtpVremeOd.Text), Convert.ToDateTime(dtpVremeDo.Text), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToInt32(txtSifra.Text), Convert.ToInt32(cboPlatilac.SelectedValue), IzPrijema, Direktna, PunPrazan, Convert.ToInt32(cboSkladiste.SelectedValue), Convert.ToInt32(cboPozicija.SelectedValue));
                        }
                        else if (chkVoz.Checked == false && row2.Selected == true)
                        {
                            ins.InsertNarManipulacije(0, Convert.ToInt32(cboPrijemKamionom.SelectedValue), row.Cells[0].Value.ToString(), Convert.ToInt32(row2.Cells[0].Value.ToString()), 0, Convert.ToDateTime(dtpVremeOd.Text), Convert.ToDateTime(dtpVremeDo.Text), Convert.ToDateTime(DateTime.Now), KorisnikCene, Convert.ToInt32(txtSifra.Text), Convert.ToInt32(cboPlatilac.SelectedValue), IzPrijema, Direktna, PunPrazan, Convert.ToInt32(cboSkladiste.SelectedValue), Convert.ToInt32(cboPozicija.SelectedValue));
                        }
                    }

                // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
            }
            RefreshDataGrid3();
            //RefreshDataGrid3();
            //RefreshDataGridIma();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void dtpVremeOd_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpVremeDo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void VratiPodatkeMaxSledeci()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select (Max([Broj]) + 1) as ID from NaruceneManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            VratiPodatkeMaxSledeci();
            dtpVremeOd.Value = DateTime.Now;
            dtpVremeDo.Value = DateTime.Now;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmNajavaKomitemtaDokument frmd3 = new frmNajavaKomitemtaDokument(txtSifra.Text, KorisnikCene);
            frmd3.Show();
        }

        private void tsPrvi_Click(object sender, EventArgs e)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Min([Broj]) as ID from NaruceneManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            RefreshDataGrid3();
            con.Close();

        }

        private void tsPoslednja_Click(object sender, EventArgs e)
        {


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([Broj]) as ID from NaruceneManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }
            RefreshDataGrid3();
            con.Close();



        }

        private void tsNazad_Click(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int prvi = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select Max(Broj) as ID from NaruceneManipulacije where Broj <" + Convert.ToInt32(txtSifra.Text) + " Order by ID desc", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                prvi = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = prvi.ToString();
            }

            con.Close();
            RefreshDataGrid3();

        }

        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([Broj]) as ID from NaruceneManipulacije", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
            }

            con.Close();
        }

        private void tsNapred_Click(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int zadnji = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select Min(Broj) as ID from NaruceneManipulacije where Broj >" + Convert.ToInt32(txtSifra.Text) + " Order by ID", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                zadnji = Convert.ToInt32(dr["ID"].ToString());
                txtSifra.Text = zadnji.ToString();
            }

            con.Close();
            RefreshDataGrid3();
            /*
                        if ((Convert.ToInt32(txtSifra.Text) + 1) == zadnji)
                          //  VratiPodatke((Convert.ToInt32(zadnji).ToString()));
                        else
                           // VratiPodatke((Convert.ToInt32(txtSifra.Text) + 1).ToString());
                        */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertNaruceneManipulacije ins = new InsertNaruceneManipulacije();
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected == true)
                    {

                        ins.DeleteNarManipulacija(Convert.ToInt32(row.Cells[9].Value));

                    }
                }
                RefreshDataGrid3();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid3();
        }

        private void RefreshComboPrijem()
        {
            var select = "SELECT PrijemKontejneraVoz.[ID], (CAst(PrijemKontejneraVoz.[ID] as nvarchar(5)) + '-' + Cast(Voz.BrVoza as nvarchar(6)) + ' ' + Voz.Relacija +  ' ' +  CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5)) as Naziv " +
      " FROM [dbo].[PrijemKontejneraVoz]    inner join Voz on Voz.ID = PrijemKontejneraVoz.IdVoza  where Vozom = 1 order by  PrijemKontejneraVoz.[DatumPrijema] desc, PrijemKontejneraVoz.[ID] ";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboPrijemVozom.DataSource = ds.Tables[0];
            cboPrijemVozom.DisplayMember = "Naziv";
            cboPrijemVozom.ValueMember = "ID";
            var select2 = "SELECT PrijemKontejneraVoz.[ID], (CAst(PrijemKontejneraVoz.[ID] as nvarchar(5)) + '-' + RegBrKamiona + ' ' + ImeVozaca +  ' ' +   CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],104)      + ' '      + SUBSTRING(CONVERT(varchar,PrijemKontejneraVoz.[DatumPrijema],108),1,5)) as Naziv " +
  " FROM [dbo].[PrijemKontejneraVoz]  where Vozom = 0  order by  PrijemKontejneraVoz.[DatumPrijema] desc, PrijemKontejneraVoz.[ID] ";

            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboPrijemKamionom.DataSource = ds2.Tables[0];
            cboPrijemKamionom.DisplayMember = "Naziv";
            cboPrijemKamionom.ValueMember = "ID";

            var select3 = " Select Distinct PaSifra, PaNaziv From Partnerji order by PaNaziv";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPlatilac.DataSource = ds3.Tables[0];
            cboPlatilac.DisplayMember = "PaNaziv";
            cboPlatilac.ValueMember = "PaSifra";

            RefreshManipulacije();


        }

        private void RefreshComboOtprema()
        {
            var select = "SELECT OtpremaKontejnera.[ID], (CAst(OtpremaKontejnera.[ID] as nvarchar(5)) + '-' + Cast(Voz.BrVoza as nvarchar(6)) + ' ' + Voz.Relacija + ' ' +  " +
" CONVERT(varchar, OtpremaKontejnera.[DatumOtpreme], 104) + ' ' + " +
" SUBSTRING(CONVERT(varchar, OtpremaKontejnera.[DatumOtpreme], 108), 1, 5)) as Naziv " +
 " FROM [dbo].[OtpremaKontejnera]    inner join Voz on Voz.ID = OtpremaKontejnera.IdVoza  where otpremakontejnera.NacinOtpreme = 1 order by OtpremaKontejnera.[DatumOtpreme] desc, OtpremaKontejnera.[ID]";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            cboPrijemVozom.DataSource = ds.Tables[0];
            cboPrijemVozom.DisplayMember = "Naziv";
            cboPrijemVozom.ValueMember = "ID";
            cboPrijemVozom.SelectedValue = pomPrijemnica;
            var select2 = "SELECT OtpremaKontejnera.[ID], (CAst(OtpremaKontejnera.[ID] as nvarchar(5)) + '-' + (CAst(OtpremaKontejnera.[ID] as nvarchar(5)) + '-' + OtpremaKontejnera.RegBrKamiona + ' ' + OtpremaKontejnera.ImeVozaca +  ' ' + " +
 " CONVERT(varchar, OtpremaKontejnera.[DatumOtpreme], 104) + ' ' + " +
 " SUBSTRING(CONVERT(varchar, OtpremaKontejnera.[DatumOtpreme], 108), 1, 5))) as Naziv " +
   " FROM[dbo].[OtpremaKontejnera]     where otpremakontejnera.NacinOtpreme = 0 order by OtpremaKontejnera.[DatumOtpreme] desc, OtpremaKontejnera.[ID]";

            var s_connection2 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection2 = new SqlConnection(s_connection2);
            var c2 = new SqlConnection(s_connection2);
            var dataAdapter2 = new SqlDataAdapter(select2, c2);

            var commandBuilder2 = new SqlCommandBuilder(dataAdapter2);
            var ds2 = new DataSet();
            dataAdapter2.Fill(ds2);
            cboPrijemKamionom.DataSource = ds2.Tables[0];
            cboPrijemKamionom.DisplayMember = "Naziv";
            cboPrijemKamionom.ValueMember = "ID";
            cboPrijemKamionom.SelectedValue = pomPrijemnica;
            var select3 = " Select Distinct PaSifra, PaNaziv From Partnerji order by PaNaziv";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboPlatilac.DataSource = ds3.Tables[0];
            cboPlatilac.DisplayMember = "PaNaziv";
            cboPlatilac.ValueMember = "PaSifra";

            RefreshManipulacije();




        }

        private void chkOtprema_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtprema.Checked == true)
            {
                chkPrijem.Checked = false;
                RefreshComboOtprema();
            }
            else
            {
                chkPrijem.Checked = true;
                RefreshComboPrijem();
            }
        }

        private void chkPrijem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrijem.Checked == true)
            {
                chkOtprema.Checked = false;
                RefreshComboPrijem();
            }
            else
            {
                chkOtprema.Checked = true;
                RefreshComboOtprema();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cboSkladiste_SelectedValueChanged(object sender, EventArgs e)
        {
            if (usao == true)
            {
                var select = " Select ID, Oznaka From Pozicija where Skladiste = " + Convert.ToInt32(cboSkladiste.SelectedValue);
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                cboPozicija.DataSource = ds.Tables[0];
                cboPozicija.DisplayMember = "Oznaka";
                cboPozicija.ValueMember = "ID";
            }
        }

        private void VratiPodatke(int ID)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            //VR SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen, PredefinisanaPorukaID from PrijemKontejneraVoz where ID=" + ID, con);

            SqlCommand cmd = new SqlCommand("SELECT [IDPrijemaVoza]      ,[IDPrijemaKamionom]   " +
                "  ,[BrojKontejnera]     ,[VrstaManipulacije]     ,[Uradjeno]     ,[DatumOd]      ,[DatumDo]     ,[Datum]" +
                "     ,[Korisnik]      ,[ID]     ,[Platilac]     ,[Broj]      ,[DatumUradjeno]     ,[IzPrijema]      ,[Direktna]  " +
                "    ,[PunPrazan]     ,[sklad]      ,[poz]  FROM [dbo].[NaruceneManipulacije] where ID=" + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["Broj"].ToString();
                cboPlatilac.SelectedValue = Convert.ToInt32(dr["Platilac"].ToString());
                dtpVremeOd.Value = Convert.ToDateTime(dr["DatumOd"].ToString());
                dtpVremeDo.Value = Convert.ToDateTime(dr["DatumDo"].ToString());
                cboSkladiste.SelectedValue = Convert.ToInt32(dr["sklad"].ToString());
                cboPozicija.SelectedValue = Convert.ToInt32(dr["Poz"].ToString());

                if (dr["PunPrazan"].ToString() == "false")
                {
                    chkPunPrazan.Checked = false;
                }
                else
                {
                    chkPunPrazan.Checked = true;
                }

                if (Convert.ToInt32(dr["Direktna"].ToString()) == 0)
                {
                    chkDirektna.Checked = false;
                }
                else
                {
                    chkDirektna.Checked = true;
                }

                if (Convert.ToInt32(dr["Uradjeno"].ToString()) == 0)
                {
                    chkUradjen.Checked = false;
                }
                else
                {
                    chkUradjen.Checked = true;
                }


            }

            con.Close();

        }

        private void VratiTrenutnuPoziciju(string BrojKont)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            //VR SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen, PredefinisanaPorukaID from PrijemKontejneraVoz where ID=" + ID, con);

            SqlCommand cmd = new SqlCommand("select top 1 SkladisteU, Skladista.Naziv, Pozicija.Oznaka , LokacijaU from Promet " +
" inner join Skladista on Skladista.ID = Promet.SkladisteU " +
" inner join Pozicija on Pozicija.ID = Promet.LokacijaU  where BrojKontejnera = '" + BrojKont + "' and Zatvoren = 0 order by Promet.ID DESC "
, con);

            SqlDataReader dr = cmd.ExecuteReader();

           

                while (dr.Read())
                {
                if (dr["SkladisteU"] != DBNull.Value)
                {
                    cboSkladIz.SelectedValue = Convert.ToInt32(dr["SkladisteU"].ToString());
                    cboPozIz.SelectedValue = Convert.ToInt32(dr["LokacijaU"].ToString());
                }
                else
                {
                    cboSkladIz.SelectedValue = Sifarnici.frmLogovanje.Skladiste;
                    cboPozIz.SelectedValue = Sifarnici.frmLogovanje.Lokacija;
                }
                    
                }

                con.Close();
       
         

           

        }

        private void VratiTrenutnuPozicijuNArudzbine(string BrojKont)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            //VR SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen, PredefinisanaPorukaID from PrijemKontejneraVoz where ID=" + ID, con);

            SqlCommand cmd = new SqlCommand("select top 1 sklad, poz from NaruceneManipulacije  where BrojKontejnera = '" + BrojKont + "' order by ID DESC "
, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                cboSkladIz.SelectedValue = Convert.ToInt32(dr["Sklad"].ToString());
                cboPozIz.SelectedValue = Convert.ToInt32(dr["Poz"].ToString());
            }

            con.Close();

        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {

                if (row.Selected == true)
                {
                   
                    VratiTrenutnuPoziciju(row.Cells[1].Value.ToString());
                    VratiPodatke(Convert.ToInt32(row.Cells[9].Value.ToString()));

                }
            }



        }

        private void PotvrdiUradjenaManipulacija()
        {
 InsertNaruceneManipulacije ins = new InsertNaruceneManipulacije();
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Selected == true)
                {

                    ins.UpdateUradjeno(Convert.ToInt32(row.Cells[9].Value) , DateTime.Now);

                }
}


}
        int VratiOznakuSledivosti(string Prijemnica, string BrojKontejnera)
        {
            int BrojStavkePrijemnice = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            //VR SqlCommand cmd = new SqlCommand("select [ID] ,[DatumPrijema],[StatusPrijema],[IdVoza],[VremeDolaska],RegBrKamiona, ImeVozaca, NajavaEmail, PrijemEmail, Napomena, CIRUradjen, PredefinisanaPorukaID from PrijemKontejneraVoz where ID=" + ID, con);

            SqlCommand cmd = new SqlCommand("Select top 1 ID from PrijemKontejneraVozStavke where IDNadredjenog = "  + Prijemnica + " and BrojKontejnera =  '" + BrojKontejnera + "'  "
            , con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                BrojStavkePrijemnice =  Convert.ToInt32(dr["ID"].ToString());
            
            }

            con.Close();

            return BrojStavkePrijemnice;
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Panta
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                Saobracaj.Dokumenta.InsertPromet ins = new Saobracaj.Dokumenta.InsertPromet();
                int pom1 = 1;
                //int pom2 = 0;
                int pom3 = 1;
                string s1 = "MSP";
                string s2 = "MSP";
                int OznSled = 0;

                //Vrati trenutno skladiste

                if (row.Selected == true)
                {
                    string poms = row.Cells[2].Value.ToString();
                    int pozicija = Convert.ToInt32(cboPozicija.SelectedValue);
                    OznSled = VratiOznakuSledivosti(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                    ins.InsProm(Convert.ToDateTime(dtpVremeOd.Value), s1,Convert.ToInt32(txtSifra.Text), row.Cells[1].Value.ToString(), s2, pom3, pom1, Convert.ToInt32(cboSkladiste.SelectedValue), Convert.ToInt32(cboPozicija.SelectedValue), Convert.ToInt32(cboSkladIz.SelectedValue), Convert.ToInt32(cboPozIz.SelectedValue), OznSled.ToString(), Convert.ToDateTime(DateTime.Now), KorisnikCene, 0, 0, Convert.ToDateTime(dtpVremeDo.Value));

                    //ins.UpdateZatvoren(Convert.ToInt32(row.Cells[1].Value.ToString()));
                    PotvrdiUradjenaManipulacija();

                }
            }
            RefreshDataGrid3();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PotvrdiUradjenaManipulacija();
            RefreshDataGrid3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.Selected == true)
                {

                    VratiTrenutnuPoziciju(row.Cells[0].Value.ToString());
                   
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (row.Selected == true)
                {
                    VratiTrenutnuPozicijuNArudzbine(row.Cells[0].Value.ToString());

                   // VratiTrenutnuPozicijuNarudzbina

                }
            }
        }
    }
}
