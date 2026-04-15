using Bunifu.UI.WinForms.Helpers.Transitions;
using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Office.Interop.Excel;
using Saobracaj.MainLeget.LegNew;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testiranje.Sifarnici;

namespace Saobracaj.Izvoz
{
    public partial class frmGrupniUnosPoljaIzvoz : Form
    {

        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        int brojStavkePorudzbenice = 0;
        int grupID = 0;
        int statusizmene = 0; // 0 je nova grupa ; 0 je update podataka postojece grupe
        List<int> noviIDs = new List<int>();
        int drumski = 0;
        int scenarioID = 0;
        bool redJePromijenjen = false;
        string ScenarioNaziv = "";
        List<PrivremeniNHM> privremenaListaNHM = new List<PrivremeniNHM>();
        List<PrivremeniNapomena> privremenaListaNapomena= new List<PrivremeniNapomena>();
        int kontejnerID = 0;
        int vrstaKamiona = 0;

        public frmGrupniUnosPoljaIzvoz(int BrojStavkePorudzbenice, int scenario, int _drumski, int VrstaKamiona)
        {
            InitializeComponent();
            ChangeTextBox();
            brojStavkePorudzbenice = BrojStavkePorudzbenice;
            scenarioID = scenario;
            drumski = _drumski;
            vrstaKamiona = VrstaKamiona;

            dataGridView1.ColumnHeadersHeightChanged += (s, e) =>
            {
                int maxHeight = 100; // Tvoj limit
                if (dataGridView1.ColumnHeadersHeight > maxHeight)
                {
                    // Isključi AutoSize da bi se moglo ručno vratiti na Max
                    dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    dataGridView1.ColumnHeadersHeight = maxHeight;
                }
            };
        }

        public frmGrupniUnosPoljaIzvoz(int ID, int BrojStavkePorudzbenice, int scenario, int _drumski , int GrupID)
        {
            InitializeComponent();
            ChangeTextBox();
            brojStavkePorudzbenice = BrojStavkePorudzbenice;
            grupID = GrupID;
            scenarioID = scenario;
            drumski = _drumski;
            kontejnerID = ID;
            txtBrojKontejnera.ReadOnly = true;

            dataGridView1.ColumnHeadersHeightChanged += (s, e) => {
                int maxHeight = 100; // Tvoj limit
                if (dataGridView1.ColumnHeadersHeight > maxHeight)
                {
                    // Isključi AutoSize da bi se moglo ručno vratiti na Max
                    dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    dataGridView1.ColumnHeadersHeight = maxHeight;
                }
            };
        }

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
           // meniHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                //meniHeader.Visible = true;
                //meniHeader.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                //foreach (Control control in groupBox1.Controls)
                //{
                //    if (control is System.Windows.Forms.Button buttons)
                //    {

                //        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                //        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                //        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                //        buttons.FlatStyle = FlatStyle.Flat;
                //    }
                //}


                foreach (System.Windows.Forms.Control control in this.Controls)
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
                //meniHeader.Visible = false;
                //meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);
            this.SuspendLayout();
            // 1. UČITAJ SVE PARTNERE SAMO JEDNOM
            var sqlPartneri = "Select PaSifra, PaNaziv, Brodar, Spediter From Partnerji order by PaNaziv";
            SqlDataAdapter daPart = new SqlDataAdapter(sqlPartneri, conn);
            System.Data.DataTable dtSviPartneri = new System.Data.DataTable();
            daPart.Fill(dtSviPartneri);

            // --- POPUNJAVANJE IZ MEMORIJE ---

            // Partneri (Svi)
            cboIzvoznik.DataSource = dtSviPartneri.Copy();
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";
            cboIzvoznik.SelectedIndex = -1;

            cboNalogodavacZaUsluge.DataSource = dtSviPartneri.Copy();
            cboNalogodavacZaUsluge.DisplayMember = "PaNaziv";
            cboNalogodavacZaUsluge.ValueMember = "PaSifra";

            cboNalogodavac.DataSource = dtSviPartneri.Copy();
            cboNalogodavac.DisplayMember = "PaNaziv";
            cboNalogodavac.ValueMember = "PaSifra";

            // Nalogodavac za drumski
            cboNalogodavacZaDrumski.DataSource = dtSviPartneri.Copy();
            cboNalogodavacZaDrumski.DisplayMember = "PaNaziv";
            cboNalogodavacZaDrumski.ValueMember = "PaSifra";

            // --- FILTRIRANJE U MEMORIJI (Bez odlaska na bazu) ---

            // Brodar (Filtriramo već učitane partnere gde je Brodar = 1)
            DataView dvBrodari = new DataView(dtSviPartneri);
            dvBrodari.RowFilter = "Brodar = 1";

            cboBrodar.DataSource = dvBrodari.ToTable(); // Kreira novu tabelu samo sa brodarima
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";

            cboVrstaPlombe.DataSource = dvBrodari.ToTable();
            cboVrstaPlombe.DisplayMember = "PaNaziv";
            cboVrstaPlombe.ValueMember = "PaSifra";


            //Novi sifarnik Inpekciski tretman
            var dir4 = "Select ID,Naziv from InspekciskiTretman order by Naziv";
            var dirAD4 = new SqlDataAdapter(dir4, conn);
            var dirDS4 = new DataSet();
            dirAD4.Fill(dirDS4);
            cboInspekciskiTretman.DataSource = dirDS4.Tables[0];
            cboInspekciskiTretman.DisplayMember = "Naziv";
            cboInspekciskiTretman.ValueMember = "ID";
            cboInspekciskiTretman.SelectedIndex = -1;

            var adr = "Select ID, (  UNKod +' - '+ Klasa + ' - ' + Naziv  ) as Naziv From VrstaRobeADR order by UNKod";
            var adrSAD = new SqlDataAdapter(adr, conn);
            var adrSDS = new DataSet();
            adrSAD.Fill(adrSDS);
            cboADR.DataSource = adrSDS.Tables[0];
            cboADR.DisplayMember = "Naziv";
            cboADR.ValueMember = "ID";
            cboADR.SelectedIndex = -1;
            ;

            var kvalitetKontejnera = "select ID, LTRIM(LTRIM(Naziv)) AS Naziv from uvKvalitetKontejnera order by ID";
            var kkAD = new SqlDataAdapter(kvalitetKontejnera, conn);
            var kkDS = new DataSet();
            kkAD.Fill(kkDS);

            cboKvalitetKontejnera.DataSource = kkDS.Tables[0];
            cboKvalitetKontejnera.DisplayMember = "Naziv";
            cboKvalitetKontejnera.ValueMember = "ID";
            cboKvalitetKontejnera.Width = 150;
            cboKvalitetKontejnera.SelectedIndex = -1;

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new DataSet();
            tkAD.Fill(tkDS);
            cboVrstaKontejnera.DataSource = tkDS.Tables[0];
            cboVrstaKontejnera.DisplayMember = "SkNaziv";
            cboVrstaKontejnera.ValueMember = "ID";


            var nhm = "Select top 100 ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM order by Naziv";

            var nhmSAD = new SqlDataAdapter(nhm, conn);
            var nhmSDS = new DataSet();
            nhmSAD.Fill(nhmSDS);
            cboVrstaRobe.DataSource = nhmSDS.Tables[0];
            cboVrstaRobe.DisplayMember = "Naziv";
            cboVrstaRobe.ValueMember = "ID";

            var np4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
            var npAD4 = new SqlDataAdapter(np4, conn);
            var npDS4 = new DataSet();
            npAD4.Fill(npDS4);
            cboNacinPakovanja.DataSource = npDS4.Tables[0];
            cboNacinPakovanja.DisplayMember = "Naziv";
            cboNacinPakovanja.ValueMember = "ID";

            var npoz4 = "Select CAST(ISNULL(ID,0) AS INT) AS ID,Naziv from NapomenaZaPozicioniranje order by Naziv";
            var npozAD4 = new SqlDataAdapter(npoz4, conn);
            var npozDS4 = new DataSet();
            npozAD4.Fill(npozDS4);
            cbNapomenaPoz.DataSource = npozDS4.Tables[0];
            cbNapomenaPoz.DisplayMember = "Naziv";
            cbNapomenaPoz.ValueMember = "ID";



            this.ResumeLayout();
        }


        private void OsveziGridNHM(System.Data.DataTable dt)
        {
            //// Ako txtID nije prazan, znači da gledamo postojeći kontejner u bazi
            //// Novi unos, čitamo iz privremene liste u memoriji
            //    DataTable dtPrivremeni = new DataTable();
            //    //dtPrivremeni.Columns.Add("ID"); // Ovde će biti 0 ili onaj negativni ID
            //    //dtPrivremeni.Columns.Add("Broj");
            //    dtPrivremeni.Columns.Add("IDNHM");
            //    dtPrivremeni.Columns.Add("Naziv");

            //    foreach (var stavka in privremenaListaNHM)
            //    {
            //        dtPrivremeni.Rows.Add(/*stavka.PrivremeniID, stavka.Broj,*/ stavka.IDNHM, stavka.Naziv);
            //    }
            //    dataGridView2.DataSource = dtPrivremeni;

            //    dataGridView2.Columns["IDNHM"].Width = 70; // dovoljno za 4 cifre
            //    dataGridView2.Columns["IDNHM"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            //    dataGridView2.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //    PodesiDatagridView(dataGridView2);
            //    //FormatirajKoloneNHM();

            dataGridView2.DataSource = dt;

            // Provera da li kolone postoje pre formatiranja (zbog sigurnosti)
            if (dataGridView2.Columns.Contains("IDNHM"))
            {
                dataGridView2.Columns["IDNHM"].Width = 70;
                dataGridView2.Columns["IDNHM"].HeaderText = "ID";
            }

            if (dataGridView2.Columns.Contains("Naziv"))
            {
                dataGridView2.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            PodesiDatagridView(dataGridView2);
        
        }

        private void OsveziGridNapomena(System.Data.DataTable dt)
        {
      

            dataGridView4.DataSource = dt;

            // Provera da li kolone postoje pre formatiranja (zbog sigurnosti)
            if (dataGridView4.Columns.Contains("IDNapomene"))
            {
                dataGridView4.Columns["IDNapomene"].Width = 70;
                dataGridView4.Columns["IDNapomene"].HeaderText = "ID";
            }

            if (dataGridView4.Columns.Contains("Naziv"))
            {
                dataGridView4.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            PodesiDatagridView(dataGridView4);

        }

        private void frmGrupniUnosPoljaIzvoz_Load(object sender, EventArgs e)
        {
            FillCombo();
            PostaviVidljivostPoljaADR();
            PostaviVidljivostGrupa4Specificna();
            PostaviVidljivostFakturisabnjeDrumski();
            PostaviVidljivostNacinPakovanja();
            PostaviVidljivostBrodskaPlomba();
            dtpCutOffPort.Value = DateTime.Now;
            if (grupID == 0)
                VratiPodatkeSelect();
            else
            {
                VratiPodatkeGrupeSelect();
                
                VratiListuKontejnera(brojStavkePorudzbenice, grupID);
                System.Data.DataTable dtIzBaze = VratiPodatkeIzBazeNHM();
                System.Data.DataTable dtIzBazeN = VratiPodatkeIzBazeNapomene();
                OsveziGridNHM(dtIzBaze);
                OsveziGridNapomena(dtIzBazeN);
                OsveziGridZaEditovanje();
            }
            //InitializeDataGrid();
            //DGVCombo();
            PodesiDatagridView(dataGridView1);
          
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // 1. Automatsko određivanje visine prema sadržaju
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;


        }


        private void VratiPodatkeSelect()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [KvalitetKontejnera] ,[Brodar], [Link], [BukingNumber], [CuttOfPort], [Izvoznik],[Nalogodavac], [OpisPosla], ProdajniNalogIzvoz.ID, TipKontenjera.Tara, ProdajniNalogIzvozStavke.TipKontejnera, (Scenario.Naziv) as ScenarioNaziv " +
                                            " FROM [dbo].[ProdajniNalogIzvozStavke] " +
                                            " INNER JOIN ProdajniNalogIzvoz on ProdajniNalogIzvozStavke.IDNAdredjenog = ProdajniNalogIzvoz.ID" +
                                            " LEFT join TipKontenjera on TipKontenjera.ID = ProdajniNalogIzvozStavke.TipKontejnera" +
                                            " LEFT join Scenario on ProdajniNalogIzvozStavke.Scenario =  Scenario.ID " +
                                            " where ProdajniNalogIzvozStavke.ID =" + brojStavkePorudzbenice, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtBrojDokumenta.Text = dr["ID"].ToString();

                if (dr["KvalitetKontejnera"] != DBNull.Value)
                    cboKvalitetKontejnera.SelectedValue = Convert.ToInt32(dr["KvalitetKontejnera"].ToString());

                txtLink.Text = dr["Link"].ToString();

                if (dr["Brodar"] != DBNull.Value)
                    cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());

                if (dr["Izvoznik"] != DBNull.Value)
                    cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());

                txtBoking.Text = dr["BukingNumber"].ToString();

                if (dr["CuttOfPort"] != DBNull.Value)
                {
                    dtpCutOffPort.Value = Convert.ToDateTime(dr["CuttOfPort"].ToString());
                    dtpCutOffPort.Tag = "IZMENJEN";
                }

                txtKorisnik.Text = tKorisnik;
                if (dr["Nalogodavac"] != DBNull.Value)
                    cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                txtopisPosla.Text = dr["OpisPosla"].ToString();

                if (dr["Tara"] != DBNull.Value)
                {
                    txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                }

                if (dr["TipKontejnera"] != DBNull.Value)
                {
                    cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                }

                if (dr["BukingNumber"] != DBNull.Value)
                {
                    txtBoking.Text = dr["BukingNumber"].ToString().Trim();
                }

                ScenarioNaziv = dr["ScenarioNaziv"].ToString().Trim();

            }
        }
        private void VratiPodatkeGrupeSelect()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

   
            SqlCommand cmd = new SqlCommand("SELECT pn.ID AS BrojDokumenta,i.Scenario, Klijent1, i.Brodar, BookingBrodara, VrstaKontejnera, i.Izvoznik, VrstaBrodskePlombe, BrodskaPlomba," +
                                            "NaslovSlanjaStatusa, ADR, NacinPakovanja, Inspekcija, CutOffPort," +
                                            "Vaganje, Tara,  DatumKreiranja, BrojStavkePorudzbenice, i.Scenario , " +
                                            " Klijent2, Napomena2REf, Klijent3, Napomena3REf, i.OpisPosla, i.Link, KvalitetKontejnera,i. Korisnik, ADR, Vaganje, NacinPakovanja " +
                                            " FROM Izvoz  i " +
                                            " INNER JOIN ProdajniNalogIzvoz pn on i.BrojStavkePorudzbenice = pn.ID  " +
                                            " where i.ID =  " + kontejnerID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtBrojDokumenta.Text = dr["BrojDokumenta"].ToString();
                //txtKorisnik.Text = dr["Korisnik"].ToString();

                if (dr["KvalitetKontejnera"] != DBNull.Value)
                    cboKvalitetKontejnera.SelectedValue = Convert.ToInt32(dr["KvalitetKontejnera"].ToString());

                txtLink.Text = dr["Link"].ToString();

                if (dr["Brodar"] != DBNull.Value)
                    cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());

                if (dr["Izvoznik"] != DBNull.Value)
                    cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());

                if (dr["VrstaBrodskePlombe"] != DBNull.Value)
                    cboVrstaPlombe.SelectedValue = Convert.ToInt32(dr["VrstaBrodskePlombe"].ToString());

                txtBrodskaPlombaBroj.Text = dr["BrodskaPlomba"].ToString().Trim();           

                if (dr["CutOffPort"] != DBNull.Value)
                {
                    dtpCutOffPort.Value = Convert.ToDateTime(dr["CutOffPort"].ToString());
                    dtpCutOffPort.Tag = "IZMENJEN";
                }

                txtKorisnik.Text = tKorisnik;
                if (dr["Klijent1"] != DBNull.Value)
                    cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Klijent1"].ToString());

                txtopisPosla.Text = dr["OpisPosla"].ToString();

                if (dr["Tara"] != DBNull.Value)
                {
                    txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                }
                if (dr["ADR"] != DBNull.Value)
                    cboADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());

                if (dr["VrstaKontejnera"] != DBNull.Value)
                {
                    cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                }
  
                if (dr["Inspekcija"] != DBNull.Value)
                {
                    cboInspekciskiTretman.SelectedValue = Convert.ToInt32(dr["Inspekcija"].ToString());
                }
                if (dr["BookingBrodara"] != DBNull.Value)
                {
                    txtBoking.Text = dr["BookingBrodara"].ToString().Trim();
                }
                
                if (dr["NaslovSlanjaStatusa"] != DBNull.Value)
                {
                    txtNapomena.Text = dr["NaslovSlanjaStatusa"].ToString().Trim();
                }
                
                if (dr["Vaganje"] != DBNull.Value && Convert.ToInt32(dr["Vaganje"]) == 1)
                    chkVaganje.Checked = true;

                if (dr["NacinPakovanja"] != DBNull.Value)
                    cboNacinPakovanja.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());


                if (dr["Klijent2"] != DBNull.Value)
                    cboNalogodavacZaUsluge.SelectedValue = Convert.ToInt32(dr["Klijent2"].ToString());
                txtRef2.Text = dr["Napomena2REf"].ToString().Trim();

                if (dr["Klijent3"] != DBNull.Value)
                    cboNalogodavacZaDrumski.SelectedValue = Convert.ToInt32(dr["Klijent3"].ToString());
                txtRef3.Text = dr["Napomena3REf"].ToString().Trim();

                ScenarioNaziv = dr["Scenario"].ToString().Trim();

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
            // dgv.ColumnHeadersHeight = 30;


        }

        private void AddTextColumn(string name, string header, int width, bool visible = true)
        {
            var col = new DataGridViewTextBoxColumn();
            col.Name = name;
            col.HeaderText = header;
            col.Width = width;
            col.Visible = visible;

            dataGridView1.Columns.Add(col);
        }

        public void AddDateColumn(string name, string header, int width, bool visible = true)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = name;
            col.DataPropertyName = name;
            col.HeaderText = header;
            col.Width = width;
            col.Visible = visible;
            col.ValueType = typeof(DateTime);
            if (name == "SpustanjePunogDtRealizacije")
            {
                col.ReadOnly = true;
            }
           
            // Postavljamo format prikaza 
            col.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

            dataGridView1.Columns.Add(col);
        }

        private void DGVCombo()
        {
            dataGridView1.Columns.Clear(); // Čistimo stare kolone
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;    // Isključuje prazan red na dnu za novi unos
            dataGridView1.AllowUserToDeleteRows = false; // Sprečava korisnika da briše redove tasterom Delete
            dataGridView1.ReadOnly = false;              // Dozvoljava editovanje ćelija 
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            //Panta

            // Skrivena ID kolona (bitna za update)
            DataGridViewTextBoxColumn id = new DataGridViewTextBoxColumn();
            id.DataPropertyName = "ID"; // Iz baze
            id.Name = "ID";
          //  id.Visible = false;
            dataGridView1.Columns.Add(id);

            if (!(scenarioID == 9 || scenarioID == 25))
            {
                // Tekstualne kolone
                AddTextColumn("BrojKontejnera", "Broj kontejnera", 120);
                AddTextColumn("OstalePlombe", "Ostale plombe", 120);
               
            }
            

                // DateTime kolone

                //grupa I
                if (scenarioID == 13)
            {
                if (drumski == 0)
                {
                    AddDateColumn("SpustanjePunogNoviPlaniraniDt", "MESTO SPUSTANJA PUNOG Novi plan.Datum/Vreme", 200);
                    AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                    //  Vozilo, Vozač...
                    AddTextColumn("Vozilo", "Vozilo", 100);
                    AddTextColumn("Vozac", "Vozač", 120);
                    AddTextColumn("BrojLK", "Broj LK", 100);
                    AddTextColumn("BrojTelefona", "Telefon", 100);


                }

                else if (drumski == 1)
                {

                    AddDateColumn("PreuzimanjePunogPlaniraniDt", "MESTO PREUZIMANJA PUNOG Plan. Datum/Vreme", 200);
                    AddDateColumn("PreuzimanjePunogNoviPlaniraniDt", "MESTO PREUZIMANJA PUNOG Novi planiran datum/Vreme", 200, false); // AUTO POPUNJAVANJE skrivena kolona
                    AddDateColumn("PreuzimanjePunogDtRealizacije", "MESTO PREUZIMANJA PUNOG Datum/Vreme realizacije", 200);
                    AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                }

                // Numeričke kolone
                AddTextColumn("BTTRobe", "BTT Robe", 80);
                AddTextColumn("NTTORobe", "NTTO Robe", 80);
                AddTextColumn("KoletaFakture", "Koleta", 80);
                AddTextColumn("CBMFaktura", "CBM Robe", 80);
                AddTextColumn("VrednostRobe", "Vrednost Robe", 80);
                AddTextColumn("BrodskaPlomba", "Brodska plomba broj", 120);

            }

            else if (scenarioID == 26)
            {
                if (drumski == 0)
                {
                    AddDateColumn("SpustanjePunogNoviPlaniraniDt", "MESTO SPUSTANJA PUNOG Novi plan. Datum/Vreme", 200);
                    AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                    //  Vozilo, Vozač...
                    AddTextColumn("Vozilo", "Vozilo", 100);
                    AddTextColumn("Vozac", "Vozač", 120);
                    AddTextColumn("BrojLK", "Broj LK", 100);
                    AddTextColumn("BrojTelefona", "Telefon", 100);
                }

                else if (drumski == 1)
                {
                    AddDateColumn("PreuzimanjePunogPlaniraniDt", "MESTO PREUZIMANJA PUNOG Plan. Datum/Vreme", 200);
                    AddDateColumn("PreuzimanjePunogNoviPlaniraniDt", "MESTO PREUZIMANJA PUNOG Novi plan. Datum/Vreme", 200, false);
                    AddDateColumn("PreuzimanjePunogDtRealizacije", "MESTO PREUZIMANJA PUNOG Datum/Vreme realizacij", 200, false);
                    AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);


                }
                // Numeričke kolone
                AddTextColumn("BTTRobe", "BTT Robe", 80);
                AddTextColumn("NTTORobe", "NTTO Robe", 80);
                AddTextColumn("KoletaFakture", "Koleta", 80);
                AddTextColumn("CBMFaktura", "CBM", 80);
                AddTextColumn("VrednostRobe", "Vrednost robe", 80);
                AddTextColumn("BrodskaPlomba", "Brodska plomba broj", 120);

            }

            else if ((scenarioID == 7 || scenarioID == 23))
            {
                if (drumski == 0)
                {
                    AddDateColumn("PreuzimanjePraznogNoviPlaniraniDt", "MESTO PREUZIMANJA PRAZNOG Novi plan. Datum/Vreme", 220);
                    AddDateColumn("PreuzimanjePraznogDtRealizacije", "MESTO PREUZIMANJA PRAZNOG Datum/Vreme realizacije", 220);
                    AddDateColumn("MestoUtovaraNoviPlaniraniDt", "MESTO UTOVARA KON. Novi plan. Datum/Vreme", 200);
                    AddDateColumn("SpustanjePunogNoviPlaniraniDt", "MESTO SPUSTANJA PUNOG Novi plan. Datum/Vreme", 200);
                    AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                    //  Vozilo, Vozač...
                    AddTextColumn("Vozilo", "Vozilo", 100);
                    AddTextColumn("Vozac", "Vozač", 120);
                    AddTextColumn("BrojLK", "Broj LK", 100);
                    AddTextColumn("BrojTelefona", "Telefon", 100);
                }

                else if (drumski == 1)
                {
                    AddDateColumn("PreuzimanjePraznogPlaniraniDt", "MESTO PREUZIMANJA PRAZNOG Plan. Datum/Vreme", 220);
                    if (scenarioID != 23)
                    {
                        AddDateColumn("PreuzimanjePraznogNoviPlaniraniDt", "MESTO PREUZIMANJA PRAZNOG Novi plan. Datum/Vreme", 220, false);// AUTO POPUNJAVANJE skrivena kolona

                    }

                    AddDateColumn("PreuzimanjePraznogDtRealizacije", "MESTO PREUZIMANJA PRAZNOG Datum/Vreme realizacije", 220, false);// AUTO POPUNJAVANJE skrivena kolona
                    AddDateColumn("MestoUtovaraNoviPlaniraniDt", "MESTO UTOVARA KON. Novi plan. Datum/Vreme", 200, false);// AUTO POPUNJAVANJE skrivena kolona
                    AddDateColumn("MestoUtovaraDtRealizacije", "MESTO UTOVARA KON. Datum/Vreme realizacije", 200, false);// AUTO POPUNJAVANJE skrivena kolona
                    AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUŠTANjA PUNOG Datum/Vreme realizacije", 200);


                }
                AddTextColumn("BTTRobe", "BTTO Robe", 80);
                if (!(scenarioID == 7 && drumski == 1))
                {
                    AddTextColumn("NTTORobe", "NTTO Robe", 80);
                    AddTextColumn("KoletaFakture", "Koleta", 80);
                    AddTextColumn("CBMFaktura", "CBM robe", 80);
                    AddTextColumn("VrednostRobe", "Vrednost robe", 80);
                    AddTextColumn("BrodskaPlomba", "Brodska plomba broj", 120);

                }
                

            }

            else if (scenarioID == 8)
            {

                if (drumski == 0)
                {
                    AddDateColumn("PreuzimanjePraznogNoviPlaniraniDt", "MESTO PREUZIMANJA PRAZNOG Novi plan. Datum/Vreme", 220);
                    AddDateColumn("PreuzimanjePraznogDtRealizacije", "MESTO PREUZIMANJA PRAZNOG Datum/Vreme realizacije", 220);
                    AddDateColumn("IstovarCeradeNoviPlaniraniDt", "MESTO ISTOVARA CERADE Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeDtRealizacije", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);
                    AddDateColumn("MestoUtovaraNoviPlaniraniDt", "MESTO UTOVARA KON. Novi plan. Datum/Vreme", 200);
                    AddDateColumn("MestoUtovaraDtRealizacije", "MESTO UTOVARA KON. Datum/Vreme realizacije", 200);
                    AddDateColumn("SpustanjePunogNoviPlaniraniDt", "MESTO SPUSTANJA PUNOG Novi plan. Datum/Vreme", 200);
                    AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                    //  Vozilo, Vozač...
                    AddTextColumn("Vozilo", "Vozilo", 100);
                    AddTextColumn("Vozac", "Vozač", 120);
                    AddTextColumn("BrojLK", "Broj LK", 100);
                    AddTextColumn("BrojTelefona", "Telefon", 100);
                }
                else if (drumski == 1)
                {
                    AddDateColumn("PreuzimanjePraznogDtRealizacije", "MESTO PREUZIMANJA PRAZNOG Datum/Vreme realizacije", 220);
                    AddDateColumn("UtovarCeradeNoviPlaniraniDt", "MESTO UTOVARA CERADE Novi plan. Datum/Vreme", 200, false); // AUTO POPUNJAVANJE skrivena kolona
                    AddDateColumn("UtovarCeradeDtRealizacije", "MESTO UTOVARA CERADE Datum/Vreme realizacije", 200);
                    AddDateColumn("IstovarCeradePlaniraniDt", "MESTO ISTOVARA CERADE Plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeNoviPlaniraniDt", "MESTO ISTOVARA CERADE Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeDtRealizacije", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);
                    AddDateColumn("MestoUtovaraNoviPlaniraniDt", "MESTO UTOVARA KON. Novi plan. Datum/Vreme", 200);
                    AddDateColumn("MestoUtovaraDtRealizacije", "MESTO UTOVARA KON. Datum/Vreme realizacije", 200);
                    AddDateColumn("SpustanjePunogNoviPlaniraniDt", "MESTO SPUSTANJA PUNOG Novi plan. Datum/Vreme", 200);
                    AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                }

                // Numeričke kolone
                AddTextColumn("BTTRobe", "BTT Robe", 80);
                AddTextColumn("NTTORobe", "NTTO Robe", 80);
                AddTextColumn("KoletaFakture", "Koleta", 80);
                AddTextColumn("CBMFaktura", "CBM Robe", 80);
                AddTextColumn("VrednostRobe", "Vrednost robe", 80);
                AddTextColumn("BrodskaPlomba", "Brodska plomba broj", 120);

            }

            else if (scenarioID == 24)
            {
                AddDateColumn("PreuzimanjePraznogNoviPlaniraniDt", "MESTO PREUZIMANJA PRAZNOG Novi plan. Datum/Vreme", 220);
                AddDateColumn("PreuzimanjePraznogDtRealizacije", "MESTO PREUZIMANJA PRAZNOG Datum/Vreme realizacije", 220);
                if (drumski == 1)
                {
                    AddDateColumn("UtovarCeradeNoviPlaniraniDt", "MESTO UTOVARA CERADE Novi plan. Datum/Vreme", 200, false); // AUTO POPUNJAVANJE skrivena kolona ima ih jos u ovom scenariju
                    AddDateColumn("UtovarCeradeDtRealizacije", "MESTO UTOVARA CERADE Datum/Vreme realizacije", 200);
                    AddDateColumn("IstovarCeradePlaniraniDt", "MESTO ISTOVARA CERADE Plan. Datum/Vreme", 200, false);
                    AddDateColumn("IstovarCeradeNoviPlaniraniDt", "MESTO ISTOVARA CERADE Novi plan. Datum/Vreme", 200, false);
                }
                else
                {
                    AddDateColumn("IstovarCeradeNoviPlaniraniDt", "MESTO ISTOVARA CERADE Novi plan. Datum/Vreme", 200);
                }
                AddDateColumn("IstovarCeradeDtRealizacije", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);
                AddDateColumn("MestoUtovaraNoviPlaniraniDt", "MESTO UTOVARA KONTEJNERA Novi plan. Datum/Vreme", 200);
                AddDateColumn("MestoUtovaraDtRealizacije", "MESTO UTOVARA KONTEJNERA Datum/Vreme realizacije", 200);
                AddDateColumn("SpustanjePunogNoviPlaniraniDt", "MESTO SPUSTANJA PUNOG Novi plan. Datum/Vreme", 200);
                AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);

                if (drumski == 0)
                {

                    //  Vozilo, Vozač...
                    AddTextColumn("Vozilo", "Vozilo", 100);
                    AddTextColumn("Vozac", "Vozač", 120);
                    AddTextColumn("BrojLK", "Broj LK", 100);
                    AddTextColumn("BrojTelefona", "Telefon", 100);
                }

                // Numeričke kolone
                AddTextColumn("BTTRobe", "BTT Robe", 80);
                AddTextColumn("NTTORobe", "NTTO Robe", 80);
                AddTextColumn("KoletaFakture", "Koleta", 80);
                AddTextColumn("CBMFaktura", "CBM Robe", 80);
                AddTextColumn("VrednostRobe", "Vrednost Robe", 80);
                AddTextColumn("BrodskaPlomba", "Brodska plomba broj", 120);

            }
            else if (scenarioID == 9)
            {
                if (drumski == 0)
                {

                    AddDateColumn("UtovarCeradeNoviPlaniraniDt", "MESTO UTOVARA CERADE Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeNoviPlaniraniDt", "MESTO ISTOVARA CERADE Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeDtRealizacije", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);
                    //  Vozilo, Vozač...
                    AddTextColumn("Vozilo", "Vozilo", 100);
                    AddTextColumn("Vozac", "Vozač", 120);
                    AddTextColumn("BrojLK", "Broj LK", 100);
                    AddTextColumn("BrojTelefona", "Telefon", 100);
                }
                else if (drumski == 1)
                {
                    AddDateColumn("UtovarCeradeNoviPlaniraniDt", "MESTO UTOVARA CERADE Novi plan. Datum/Vreme", 200, false);
                    AddDateColumn("UtovarCeradeDtRealizacije", "MESTO UTOVARA CERADE Datum/Vreme realizacije", 200);
                    AddDateColumn("IstovarCeradePlaniraniDt", "MESTO ISTOVARA CERADE Plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeNoviPlaniraniDt", "MESTO ISTOVARA CERADE  Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeDtRealizacije", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);

                    AddTextColumn("BTTRobe", "BTT Robe", 80);
                    AddTextColumn("NTTORobe", "NTTO Robe", 80);
                    AddTextColumn("KoletaFakture", "Koleta", 80);
                    AddTextColumn("CBMFaktura", "CBM Robe", 80);
                    AddTextColumn("VrednostRobe", "Vrednost Robe", 80);

                }

            }

            else if (scenarioID == 25)
            {
                if (drumski == 0)
                {

                    AddDateColumn("UtovarCeradePlaniraniDt", "MESTO UTOVARA CERADE Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeNoviPlaniraniDt", "MESTO ISTOVARA CERADE Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeDtRealizacije", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);

                    AddTextColumn("Vozilo", "Vozilo", 100);
                    AddTextColumn("Vozac", "Vozač", 120);
                    AddTextColumn("BrojLK", "Broj LK", 100);
                    AddTextColumn("BrojTelefona", "Telefon", 100);
                }
                else if (drumski == 1)
                {
                    AddDateColumn("UtovarCeradePlaniraniDt", "MESTO UTOVARA CERADE Novi plan. Datum/Vreme", 200, false);
                    AddDateColumn("UtovarCeradeDtRealizacije", "MESTO UTOVARA CERADE Datum/Vreme realizacije", 200);
                    AddDateColumn("IstovarCeradePlaniraniDt", "MESTO ISTOVARA CERADE Plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeNoviPlaniraniDt", "MESTO ISTOVARA CERADE  Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeDtRealizacije", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);
                }
                AddTextColumn("BTTRobe", "BTTO Robe", 80);
                AddTextColumn("NTTORobe", "NTTO Robe", 80);
                AddTextColumn("KoletaFakture", "Koleta", 80);
                AddTextColumn("CBMFaktura", "CBM  Robe", 80);
                AddTextColumn("VrednostRobe", "Vrednost Robe", 80);


            }

            if (drumski == 1)
            {
                DataGridViewComboBoxColumn napomena = new DataGridViewComboBoxColumn();
                napomena.HeaderText = "Napomena za pozicioniranje";
                napomena.DataPropertyName = "NapomenaZaPozicioniranje";
                napomena.Name = "NapomenaZaPozicioniranje";
                var query212 = "Select CAST(ISNULL(ID,0) AS INT) AS ID,Naziv from NapomenaZaPozicioniranje order by Naziv";
                SqlConnection conn212 = new SqlConnection(connection);
                SqlDataAdapter da212 = new SqlDataAdapter(query212, conn212);
                System.Data.DataSet ds212 = new System.Data.DataSet();
                da212.Fill(ds212);
                napomena.DataSource = ds212.Tables[0];
                napomena.DisplayMember = "Naziv";
                napomena.ValueMember = "ID";
                napomena.Width = 150;
                napomena.ReadOnly = false;
                napomena.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox; // Forsira izgled padajućeg menija uvek
                napomena.AutoComplete = true; // Pomaže kod editovanja
                napomena.FlatStyle = FlatStyle.Popup;
                dataGridView1.Columns.Add(napomena);
            }


        }

        private void OsveziGridZaEditovanje()
        {
            dataGridView1.DataSource = null; // Resetuj izvor
            dataGridView1.Columns.Clear();   // Obriši kolone
            dataGridView1.AutoGenerateColumns = false; // Isključi automatiku
            
            DGVCombo();
            System.Data.DataTable dt = VratiPodatkeIzBazePojedinacni();

            if (dt.Columns.Contains("NapomenaZaPozicioniranje"))
            {
                dt.Columns["NapomenaZaPozicioniranje"].ReadOnly = false;
            }

            dataGridView1.DataSource = dt;

            // Provera za svaki slučaj 
            if (dataGridView1.Columns.Contains("NapomenaZaPozicioniranje"))
            {
                dataGridView1.Columns["NapomenaZaPozicioniranje"].ReadOnly = false;
            }
        }

        private System.Data.DataTable VratiPodatkeIzBazePojedinacni()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            System.Data.DataTable dt = new System.Data.DataTable();

         
            string idsZaUpit = string.Join(",", noviIDs);

            using (SqlConnection con = new SqlConnection(s_connection))
            {
            
                string query = @"SELECT ID, BrojKontejnera, OstalePlombe, BrutoRobe as BTTRobe, NetoRobe as NTTORobe, 
                        BrojKoleta as KoletaFakture, CBM as CBMFaktura, VrednostRobeFaktura as VrednostRobe, 
                        BrojLK, BrojTelefona, Vozilo, Vozac, 
                        PlaniranDtSpustanjaPunog as SpustanjePunogNoviPlaniraniDt, 
		                DtRealizacijeSpustanjaPunog as SpustanjePunogDtRealizacije,  
		                PlaniranDtPreuzimanjaPraznog as PreuzimanjePraznogNoviPlaniraniDt, 
		                DtPreuzimanjaPraznog as PreuzimanjePraznogPlaniraniDt, 
		                DtRealizacijePreuzimanjaPraznog  as PreuzimanjePraznogDtRealizacije,  
		                DtPreuzimanjaPunog  as PreuzimanjePunogPlaniraniDt,  
		                PlaniranDtPreuzimanjaPunog  as PreuzimanjePunogNoviPlaniraniDt, 
		                DtRealizacijePreuzimanjaPunog  as PreuzimanjePunogDtRealizacije, 
		                PlaniranDtIstovaraCerade as IstovarCeradeNoviPlaniraniDt,  
		                DtIstovaraCerade  as IstovarCeradePlaniraniDt, 
		                DtRealizacijeIstovaraCerade  as IstovarCeradeDtRealizacije,  
		                PlaniranDtUtovaraKontejnera  as MestoUtovaraNoviPlaniraniDt,  
		                DtRealizacijeUtovaraKontejnera  as MestoUtovaraDtRealizacije, 
		                PlaniranDtUtovaraCerade  as UtovarCeradeNoviPlaniraniDt, 
		                DtRealizacijeUtovaraCerade  as UtovarCeradeDtRealizacije,
                        CAST(ISNULL((SELECT Top(1) IDNapomene FROM IzvozNapomenePozicioniranja where IDNadredjena = Izvoz.ID order by ID desc),0) AS INT) AS NapomenaZaPozicioniranje
                       
                        FROM Izvoz 
                        WHERE ID in ( " + idsZaUpit + " )"; 

                SqlCommand cmd = new SqlCommand(query, con);
             
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    dt.Load(dr);

                    foreach (DataColumn col in dt.Columns)
                    {
                        Console.WriteLine("Kolona: " + col.ColumnName + " | Tip: " + col.DataType);
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                  
                }
            } 

            return dt;


        }

        private System.Data.DataTable VratiPodatkeIzBazeNHM()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            System.Data.DataTable dt = new System.Data.DataTable();

            // Provera da li lista ima članova 
            if (noviIDs.Count == 0) return dt;

            string idsZaUpit = string.Join(",", noviIDs);

            using (SqlConnection con = new SqlConnection(s_connection))
            {
       
                string query = $@"SELECT DISTINCT  nm.ID as IDNHM, (RTRIM(nm.Naziv) + ' - ' + RTRIM(nm.Broj)) as Naziv
                          FROM IzvozNHM inhm 
                          INNER JOIN NHM nm on inhm.IDNHM = nm.ID
                          WHERE inhm.IDNadredjena IN ({idsZaUpit})";

                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    dt.Load(cmd.ExecuteReader());
                }
                catch (Exception ex) { /* log error */ }
            }
            return dt;

        }

        private System.Data.DataTable VratiPodatkeIzBazeNapomene()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            System.Data.DataTable dt = new System.Data.DataTable();

            // Provera da li lista ima članova 
            if (noviIDs.Count == 0) return dt;

            string idsZaUpit = string.Join(",", noviIDs);

            using (SqlConnection con = new SqlConnection(s_connection))
            {

                string query = $@"SELECT DISTINCT  np.ID as IDNapomene, (RTRIM(np.Naziv) ) as Naziv
                          FROM IzvozNapomenePozicioniranja inap
                          INNER JOIN NapomenaZaPozicioniranje np on inap.IDNapomene = np.ID
                          WHERE inap.IDNadredjena IN ({idsZaUpit})";

                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    dt.Load(cmd.ExecuteReader());
                }
                catch (Exception ex) { /* log error */ }
            }
            return dt;

        }

        private void InitializeDataGrid(List<int> noviIDs)
        {
            if (noviIDs.Count == 0) return;

            string idsZaUpit = string.Join(",", noviIDs);

            string query = $@"SELECT ID FROM Izvoz 
                     WHERE ID IN ({idsZaUpit})";

            SqlConnection conn = new SqlConnection(connection);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false; // Ne damo da dodaju nove, samo edit 
        }

        private void AddTextColumn(string propName, string header, int width)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = propName;
            col.HeaderText = header;
            col.Name = propName;
            col.Width = width;
            dataGridView1.Columns.Add(col);
        }
        // validacija da se upisuje datum u datumsku kolonu
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
           
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName.EndsWith("Dt") || colName.EndsWith("DtRealizacije"))
            {
                // 2. Uzmi ono što je trenutno u ćeliji
                string input = e.FormattedValue.ToString().Trim();

                // 3. SPREČAVANJE: Ako je polje prazno, dozvoli izlaz 
                if (string.IsNullOrEmpty(input))
                {
                    return;
                }
                DateTime temp;
                if (!DateTime.TryParse(input, out temp))
                {
                    // Dodatni osigurač: Validacija se okida samo ako korisnik pokuša da 
                    // klikne na drugu ćeliju ili pritisne Enter.

                    MessageBox.Show($"Unesite kompletan datum {DateTime.Now:dd-MM-yyyy HH:mm}", "Nepotpun unos");
                    e.Cancel = true;
                }
            }
        }


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Provera da li je trenutna kolona numerička (npr. BTTRobe)
            System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
            string columnName = dataGridView1.CurrentCell.OwningColumn.Name;

            if (columnName == "BTTRobe" || columnName == "NTTORobe" || columnName == "KoletaFakture" || columnName == "CBMFaktura" || columnName == "VrednostRobe")
            {

                if (tb != null)
                {
                    tb.KeyPress -= new KeyPressEventHandler(NumericColumn_KeyPress);
                    tb.KeyPress += new KeyPressEventHandler(NumericColumn_KeyPress);
                }
            }
            else if (columnName.EndsWith("Dt") || columnName.EndsWith("DtRealizacije") || columnName.EndsWith("Dt1"))
            {
                // Skidamo numerički handler ako je ostao
                tb.KeyPress -= NumericColumn_KeyPress;

                // Validacija će se okinuti TEK kad korisnik napusti TextBox (TAB/Enter)
                tb.CausesValidation = false;
            }
            else
            {
                // Ako NIJE numerička kolona, skidamo handler da bi obična polja radila normalno
           
                if (tb != null)
                {
                    tb.KeyPress -= NumericColumn_KeyPress;
                }
            }
        }

        private void NumericColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Dozvoli samo brojeve, kontrolne tastere (BackSpace) i zarez/tačku
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }
        private bool ValidacijaSaIkonama()
        {
            bool uspesno = true;
            errorProvider1.Clear(); //  prvo očistimo stare greške

            var allowedScenarios = new HashSet<int> { 13, 26, 7, 23, 8, 24 }; // grupa I, II, III

            if (allowedScenarios.Contains(scenarioID))
            {
                if (grupID == 0) // to znaci da je u pitanju insert i tada broj kontejnera ne sme biti prazan
                {
                    if (string.IsNullOrWhiteSpace(txtBrojKontejnera.Text) || !int.TryParse(txtBrojKontejnera.Text, out _))
                    {
                        errorProvider1.SetError(txtBrojKontejnera, "Polje je obavezno i očekuje broj!");
                        uspesno = false;
                    }
                }
                if (string.IsNullOrWhiteSpace(txtBoking.Text) || txtBoking.Text.Trim() == "0")
                {
                    errorProvider1.SetError(txtBoking, "Polje je obavezno!");
                    uspesno = false;
                }

                if (txtTaraKontejnera.Value <= 0)
                {
                    errorProvider1.SetError(txtTaraKontejnera, "Vrednost mora biti veća od 0!");
                    uspesno = false;
                }

                if (cboNalogodavacZaUsluge.SelectedIndex < 1)
                {
                    errorProvider1.SetError(cboNalogodavacZaUsluge, "Morate izabrati neku vrednost!");
                    uspesno = false;
                }
                if (string.IsNullOrEmpty(txtRef2.Text) || txtRef2.Text.Trim() == "0")
                {
                    errorProvider1.SetError(txtRef2, "Polje je obavezno!");
                    uspesno = false;
                }
                if (scenarioID == 23 || scenarioID == 24 || scenarioID == 26)
                {


                    if (cboADR.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboADR, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                }
                if (drumski == 1)
                {
                    if (cboNalogodavacZaDrumski.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboNalogodavacZaDrumski, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                    if (string.IsNullOrEmpty(txtRef3.Text) || txtRef3.Text.Trim() == "0")
                    {
                        errorProvider1.SetError(txtRef3, "Ovo polje je obavezno!");
                        uspesno = false;
                    }
                }

            }

            //grupa IV
            else
            {
                if (scenarioID == 25) // IVA,IVLA
                {
                    if (cboADR.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboADR, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                }
                if ((scenarioID == 9 && drumski == 1) || (scenarioID == 25)) // IVA,IVLA
                {
                    if (cboNalogodavacZaUsluge.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboNalogodavacZaUsluge, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                    if (string.IsNullOrEmpty(txtRef2.Text) || txtRef2.Text.Trim() == "0")
                    {
                        errorProvider1.SetError(txtRef2, "Polje je obavezno!");
                        uspesno = false;
                    }

                    if (drumski == 1)
                    {
                        if (cboNalogodavacZaDrumski.SelectedIndex < 1)
                        {
                            errorProvider1.SetError(cboNalogodavacZaDrumski, "Morate izabrati neku vrednost!");
                            uspesno = false;
                        }
                        if (string.IsNullOrEmpty(txtRef3.Text) || txtRef3.Text.Trim() == "0")
                        {
                            errorProvider1.SetError(txtRef3, "Ovo polje je obavezno!");
                            uspesno = false;
                        }
                    }

                }

            }

            return uspesno;
        }

        private bool ValidacijaGrida(DataGridViewRow row)
        {
            bool uspesno = true;

            dataGridView1.EndEdit(); // OBAVEZNO da commit-uje izmene
            dataGridView1.ClearSelection();

            row.ErrorText = ""; // očisti stare greške
            foreach (DataGridViewCell cell in row.Cells)
                cell.ErrorText = "";


            if (scenarioID != 9 && scenarioID != 25)
            {
                if (!ValidirajObaveznuKolonu(dataGridView1, row, "BrojKontejnera", "Broj kontejnera je obavezno polje!"))
                {
                    uspesno = false;
                }
                
                if (drumski == 0)
                {

                    if (!ValidirajObaveznuKolonu(dataGridView1, row, "Vozac", "Vozač je obavezno polje!"))
                    {
                        uspesno = false;
                    }

                    if (!ValidirajObaveznuKolonu(dataGridView1, row, "Vozilo", "Vozilo je obavezno polje!"))
                    {
                        uspesno = false;
                    }
                 
                    if (!ValidirajObaveznuKolonu(dataGridView1, row, "BrojLK", "Broj lične karte je obavezno polje!"))
                    {
                        uspesno = false;
                    }
                }
            }
            else if (scenarioID == 9 && drumski == 0)

            {
             
                if (!ValidirajObaveznuKolonu(dataGridView1, row, "Vozac", "Vozač je obavezno polje!"))
                {
                    uspesno = false;
                }

                if (!ValidirajObaveznuKolonu(dataGridView1, row, "Vozilo", "Vozilo je obavezno polje!"))
                {
                    uspesno = false;
                }

            }
            else if (scenarioID == 25 && drumski == 0)

            {
                if (!ValidirajObaveznuKolonu(dataGridView1, row, "Vozac", "Vozač je obavezno polje!"))
                {
                    uspesno = false;
                }

                if (!ValidirajObaveznuKolonu(dataGridView1, row, "Vozilo", "Vozilo je obavezno polje!"))
                {
                    uspesno = false;
                }
                if (!ValidirajObaveznuKolonu(dataGridView1, row, "BrojLK", "Broj lične karte je obavezno polje!"))
                {
                    uspesno = false;
                }

            }
            if ((scenarioID == 13 || scenarioID == 26)  )  //|| (scenarioID == 9 && drumski == 1)  IL i ILA
            {
                if (drumski == 1)
                {
                    if (!ValidirajObaveznuKolonu(dataGridView1, row, "PreuzimanjePunogPlaniraniDt", "Ovo polje je obavezno polje!"))
                    {
                        uspesno = false;
                    }
                }
                else if(drumski == 0)
                {
                    if (!ValidirajObaveznuKolonu(dataGridView1, row, "Vozac", "Vozač je obavezno polje!"))
                    {

                        uspesno = false;
                    }

                    if (!ValidirajObaveznuKolonu(dataGridView1, row, "Vozilo", "Vozilo je obavezno polje!"))
                    {
                        uspesno = false;
                    }
                    if (scenarioID == 26)
                    {
                        if (!ValidirajObaveznuKolonu(dataGridView1, row, "BrojLK", "Broj lične karte je obavezno polje!"))
                        {
                            uspesno = false;
                        }
                    }
                }


            }


            if ((scenarioID == 7 && drumski == 0) ) //|| (scenarioID == 9 && drumski == 1)
            {
                if (!ValidirajObaveznuKolonu(dataGridView1, row, "PreuzimanjePraznogDtRealizacije", "Ovo polje je obavezno polje!"))
                    {
                        uspesno = false;
                    }

                if (!ValidirajObaveznuKolonu(dataGridView1, row, "SpustanjePunogDtRealizacije", "Ovo polje je obavezno polje!"))
                {
                    uspesno = false;
                }
            }

            if ((scenarioID == 7 || scenarioID == 23) && drumski == 1) //
            {
                if (!ValidirajObaveznuKolonu(dataGridView1, row, "PreuzimanjePraznogPlaniraniDt", "Ovo polje je obavezno polje!"))
            {
                    uspesno = false;
                }
            }

            return uspesno;
        }
        private bool ValidirajObaveznuKolonu(DataGridView grid,
                                     DataGridViewRow row,
                                     string nazivKolone,
                                     string poruka)
        {
            // Proveri da li kolona postoji
            if (!grid.Columns.Contains(nazivKolone))
                return true; // ako ne postoji, preskoči validaciju

            var cell = row.Cells[nazivKolone];

            if (cell.Value == null ||
                string.IsNullOrWhiteSpace(cell.Value.ToString()))
            {
                cell.ErrorText = poruka;
                return false;
            }

            return true;
        }

        private void dptDatum_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Tag = "IZMENJEN";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidacijaSaIkonama())
            {
                MessageBox.Show("Molimo popunite označena polja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Prekida se izvršavanje ako nije validno
            }


            InsertIzvoz ins = new InsertIzvoz();
            DateTime? cutOffPort = null;
            decimal? bttRobe = null;
            decimal? nttoRobe = null;
            decimal? koleta = null;
            decimal? cbm = null;
            decimal? vrednostRobe = null;


            decimal taraKontejnera = Convert.ToDecimal(txtTaraKontejnera.Value);

            string brodskaPlombaBroj = string.IsNullOrWhiteSpace(txtBrodskaPlombaBroj.Text) ? null : txtBrodskaPlombaBroj.Text.Trim();
            int? brodskaPlomba = null; /*string.IsNullOrWhiteSpace(txtBrodskaPlomba.Text) ? null : txtBrodskaPlomba.Text.Trim();*/
            if (cboBrodar.SelectedValue != null)
            {
                brodskaPlomba = Convert.ToInt32(cboVrstaPlombe.SelectedValue);
            }

            string booking = string.IsNullOrWhiteSpace(txtBoking.Text) ? null : txtBoking.Text.Trim();
            int? bookingInt = null;
            if (!string.IsNullOrWhiteSpace(booking) && int.TryParse(booking, out var _btmp))
                bookingInt = _btmp;

            int? brodar = null;
            if (cboBrodar.SelectedValue != null)
            {
                brodar = Convert.ToInt32(cboBrodar.SelectedValue);
            }

            int? izvoznik = null;
            if (cboIzvoznik.SelectedValue != null)
            {
                izvoznik = Convert.ToInt32(cboIzvoznik.SelectedValue);
            }

            int? pomVaganje = null;
            if (chkVaganje.Checked == true)
            {
                pomVaganje = 1;
            }
            int? adr = null;
            if (cboADR.SelectedValue != null)
            {
                adr = Convert.ToInt32(cboADR.SelectedValue);
            }
            string Napomena = string.IsNullOrWhiteSpace(txtNapomena.Text) ? null : txtNapomena.Text.Trim();


            int? inspekcijskiTretman = null;
            if (cboInspekciskiTretman.SelectedValue != null)
            {
                inspekcijskiTretman = Convert.ToInt32(cboInspekciskiTretman.SelectedValue);
            }
            int? vrstaKontejnera = null;
            if (cboVrstaKontejnera.SelectedValue != null)
            {
                vrstaKontejnera = Convert.ToInt32(cboVrstaKontejnera.SelectedValue);
            }

            int? nalogodavacZaUsluge = null;
            if (cboNalogodavacZaUsluge.SelectedValue != null)
            {
                nalogodavacZaUsluge = Convert.ToInt32(cboNalogodavacZaUsluge.SelectedValue);
            }

            int? referencaFakturisanje = null;

            if (!string.IsNullOrWhiteSpace(txtRef2.Text))
            {
                if (int.TryParse(txtRef2.Text.Trim(), out int rezultat))
                {
                    referencaFakturisanje = rezultat;
                }
            }

            int? nalogodavacZaDrumski = null;
            if (cboNalogodavacZaDrumski.SelectedValue != null)
            {
                nalogodavacZaDrumski = Convert.ToInt32(cboNalogodavacZaDrumski.SelectedValue);
            }

            int? porucilac = null;
            if (cboNalogodavac.SelectedValue != null)
            {
                porucilac = Convert.ToInt32(cboNalogodavac.SelectedValue);
            }

            int? referencaDrumski = null;

            if (!string.IsNullOrWhiteSpace(txtRef3.Text))
            {
                if (int.TryParse(txtRef3.Text.Trim(), out int rezultat))
                {
                    referencaDrumski = rezultat;
                }
            }

            int? nacinPakovanja = null;
            if (cboNacinPakovanja.SelectedValue != null)
            {
                nacinPakovanja = Convert.ToInt32(cboNacinPakovanja.SelectedValue);
            }
            if (dtpCutOffPort.Tag == "IZMENJEN")
                cutOffPort = GetVisibleDateTimeValue(dtpCutOffPort);


            int brojKontejnera = 0;
            if (noviIDs == null || noviIDs.Count == 0)
            {
                if (!string.IsNullOrWhiteSpace(txtBrojKontejnera.Text))
                {
                    if (int.TryParse(txtBrojKontejnera.Text.Trim(), out int rezultat))
                    {
                        brojKontejnera = rezultat;
                    }
                    else
                    {
                        MessageBox.Show("Pogrešno unet podatak za broj kontejnera! ");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Niste uneli broj koliko kontejnera želite da napravite ");
                    return;
                }
            }



            int vrstaRobe = 0;
            if (cboVrstaRobe.SelectedValue != null)
            {
                vrstaRobe = Convert.ToInt32(cboVrstaRobe.SelectedValue);
            }
            string opisPosla = string.IsNullOrWhiteSpace(txtopisPosla.Text) ? null : txtopisPosla.Text.Trim();
            string link = string.IsNullOrWhiteSpace(txtLink.Text) ? null : txtLink.Text.Trim();

            int? kvalitetKontejnera = null;
            if (cboKvalitetKontejnera.SelectedValue != null)
            {
                kvalitetKontejnera = Convert.ToInt32(cboKvalitetKontejnera.SelectedValue);
            }

            // Normalize parameters: ints -> 0 if null, strings -> single space if null/empty
            int safePorucilac = porucilac ?? 0;
            int safeBrojKontejnera = brojKontejnera;
            int safeBrodar = brodar ?? 0;
            int safeBooking = bookingInt ?? 0;
            int safeVrstaKontejnera = vrstaKontejnera ?? 0;
            int safeIzvoznik = izvoznik ?? 0;
            int safeBrodskaPlomba = brodskaPlomba ?? 0;
            string safeBrodskaPlombaBroj = string.IsNullOrWhiteSpace(brodskaPlombaBroj) ? " " : brodskaPlombaBroj;
            string safeNapomena = string.IsNullOrWhiteSpace(Napomena) ? " " : Napomena;
            int safeAdr = adr ?? 0;
            int safeNacinPakovanja = nacinPakovanja ?? 0;
            int safeInspekcija = inspekcijskiTretman ?? 0;
            DateTime? safeCutOffPort = cutOffPort; // keep nullable
            decimal safeTaraKontejnera = taraKontejnera;
            int safeVaganje = pomVaganje ?? 0;
            int safeKlijent2 = nalogodavacZaUsluge ?? 0;
            int safeNapomena2Ref = referencaFakturisanje ?? 0;
            int safeKlijent3 = nalogodavacZaDrumski ?? 0;
            int safeNapomena3Ref = referencaDrumski ?? 0;
            string safeOpisPosla = string.IsNullOrWhiteSpace(opisPosla) ? " " : opisPosla;
            string safeLink = string.IsNullOrWhiteSpace(link) ? " " : link;
            int safeKvalitet = kvalitetKontejnera ?? 0;
            int safeVrstaRobe = vrstaRobe;

            if (noviIDs != null && noviIDs.Count > 0)
            {
                ins.UpdateIzvozPorudzbenica(noviIDs,
                                            safePorucilac,
                                            safeBrodar,
                                            safeBooking,
                                            safeVrstaKontejnera,
                                            safeIzvoznik,
                                            safeBrodskaPlomba,
                                            safeBrodskaPlombaBroj,
                                            safeNapomena,
                                            safeAdr,
                                            safeNacinPakovanja,
                                            safeInspekcija,
                                            safeCutOffPort,
                                            safeTaraKontejnera,
                                            safeVaganje,
                                            safeKlijent2,
                                            safeNapomena2Ref,
                                            safeKlijent3,
                                            safeNapomena3Ref,
                                            safeOpisPosla,
                                            safeLink,
                                            safeKvalitet,
                                            safeVrstaRobe);
                InsertIzvoz uvK = new InsertIzvoz();
                InsertIzvozKonacna izk = new InsertIzvozKonacna();
                foreach (int kontejnerID in noviIDs)
                {

                    foreach (var stavka in privremenaListaNapomena)
                    {
                        izk.InsIzvozNapomenePozicioniranja(kontejnerID, stavka.IDNapomene, stavka.Naziv);
                    }
                }

                foreach (int kontejnerID in noviIDs)
                {

                    foreach (var stavka in privremenaListaNHM)
                    {
                        uvK.InsIzvozNHM(kontejnerID, stavka.IDNHM);
                    }
                }

                /*  foreach (int kontejnerID in noviIDs)
                  {
                      int sc = VratiScenarioSelektovanog(kontejnerID);
                      UnosManipulacija(kontejnerID, sc);
                  }*/
            }
            else  // insert
            {
                try
                {
                    noviIDs = ins.InsIzvozPorudzbenica(brojStavkePorudzbenice,
                                                       scenarioID,
                                                       tKorisnik,
                                                       safePorucilac,
                                                       safeBrojKontejnera,
                                                       safeBrodar,
                                                       safeBooking,
                                                       safeVrstaKontejnera,
                                                       safeIzvoznik,
                                                       safeBrodskaPlomba,
                                                       safeBrodskaPlombaBroj,
                                                       safeNapomena,
                                                       safeAdr,
                                                       safeNacinPakovanja,
                                                       safeInspekcija,
                                                       safeCutOffPort,
                                                       safeTaraKontejnera,
                                                       safeVaganje,
                                                       safeKlijent2,
                                                       safeNapomena2Ref,
                                                       safeKlijent3,
                                                       safeNapomena3Ref,
                                                       safeOpisPosla,
                                                       safeLink,
                                                       safeKvalitet,
                                                       safeVrstaRobe,
                                                       drumski,
                                                       vrstaKamiona);

                    InsertIzvoz uvK = new InsertIzvoz();

                    foreach (int kontejnerID in noviIDs)
                    {

                        foreach (var stavka in privremenaListaNHM)
                        {
                            uvK.InsIzvozNHM(kontejnerID, stavka.IDNHM);
                        }
                    }

                    InsertIzvozKonacna izk = new InsertIzvozKonacna();
                    foreach (int kontejnerID in noviIDs)
                    {

                        foreach (var stavka in privremenaListaNapomena)
                        {
                            izk.InsIzvozNapomenePozicioniranja(kontejnerID, stavka.IDNapomene, stavka.Naziv);
                        }
                    }

                    //Unos manipulacija terminalskih
                    //Za Svaki kontejner - Vrati scenario i insertuj manipulacije koje su vezane za taj scenario
                    foreach (int kontejnerID in noviIDs)
                    {
                        int sc = VratiScenarioSelektovanog(kontejnerID);
                        UnosManipulacija(kontejnerID, sc);
                    }

                    MessageBox.Show("Uspešno formirano!");
                    InitializeDataGrid(noviIDs);
                    DGVCombo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void UnosManipulacija(int kontejnerID, int sc)
        {
            int pomID = 0;
            int pomManupulacija = 0;
            double pomCena = 0;
            double pomkolicina = 1;
            string pomPokret = "";
            int pomStatusKontejnera = 0;
            int pomPlatilac = 0;
            string pomForma = "";
            try
            {


                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("Select Usluga, Pokret, Forma, Statuskontejnera from Scenario where ID = " + sc + " order by RB", con);
                SqlDataReader dr = cmd.ExecuteReader();



                while (dr.Read())
                {
                    pomManupulacija = Convert.ToInt32(dr["Usluga"].ToString());
                    pomPokret = dr["Pokret"].ToString();
                    pomStatusKontejnera = Convert.ToInt32(dr["Statuskontejnera"].ToString());
                    pomForma = dr["Forma"].ToString();
                    pomCena = 0;
                    pomkolicina = 1;
                    pomPlatilac = 0;
                    // pomOrgJed = VratiOrgJed(pomManupulacija);
                    UbaciStavkuUsluge(kontejnerID, pomManupulacija, pomCena, pomkolicina, 4, pomPlatilac, pomPokret, pomStatusKontejnera, pomForma);
                }           
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }



        }

        private void UbaciStavkuUsluge(int ID, int Manipulacija, double Cena, double Kolicina, int OrgJed, int Platilac, string PomPokret, int PomStatusKOntejnera, string PomForma)
        {
            // if (txtNadredjeni.Text != "0")
            // {
            //  InsertIzvoz uvK = new InsertIzvoz();
            // uvK.InsUbaciUsluguKonacna(ID, Manipulacija, Cena, Kolicina, OrgJed, Platilac, 0, PomPokret, PomStatusKOntejnera, PomForma);

            // }
            // else
            // {

            InsertIzvoz uvK = new InsertIzvoz();
            uvK.InsUbaciUslugu(ID, Manipulacija, Cena, Kolicina, OrgJed, Platilac, 0, PomPokret, PomStatusKOntejnera, PomForma);


            // }


        }

        private DateTime? GetVisibleDateTimeValue(DateTimePicker dtp)
        {
            // Ako panel uopšte nije vidljiv, ili je vidljiv ali ništa nije izabrano
            if (!dtp.Visible)
            {
                return null;
            }

            if (DateTime.TryParse(dtp.Value.ToString(), out DateTime rezultat))
            {
                return rezultat;
            }

            return null;
        }
        private void PostaviVidljivostPoljaADR()
        {
            bool isADR = (scenarioID == 23 || scenarioID == 24 || scenarioID == 25 || scenarioID == 26); // II ili II-A
            {
                cboADR.Visible = isADR;
                lblAdr.Visible = isADR;
            }
            if (isADR == false)
            {
                cboADR.SelectedValue = -1;
            }
        }
        private void PostaviVidljivostFakturisabnjeDrumski()
        {
            if (scenarioID == 9 && drumski == 0)
            {

                lblNalogodavacZaDrumski.Visible = cboNalogodavacZaDrumski.Visible = false;
                lblRef3.Visible = txtRef3.Visible = false;
                lblNalogodavacZaUsluge.Visible = cboNalogodavacZaUsluge.Visible = false;
                lblRef2.Visible = txtRef2.Visible = false;

            }
            else
            {
                if (drumski == 1)
                {
                    cboNalogodavacZaDrumski.Visible = true;
                    lblNalogodavacZaDrumski.Visible = true;
                    txtRef3.Visible = true;
                    lblRef3.Visible = true;
                }
                else
                {
                    cboNalogodavacZaDrumski.Visible = false;
                    lblNalogodavacZaDrumski.Visible = false;
                    txtRef3.Visible = false;
                    lblRef3.Visible = false;
                }
            }
        }

        private void PostaviVidljivostGrupa4Specificna()
        {
            if (scenarioID == 9 || scenarioID == 25)
            {
                lblKvalitetKontejnera.Visible = cboKvalitetKontejnera.Visible = false;
                lblTaraKontejnera.Visible = txtTaraKontejnera.Visible = false;
                lblVrstaPlombe.Visible = cboVrstaPlombe.Visible = false;
            }

            if (scenarioID == 9 && drumski == 0) // dodatno samo za Scenario IV -ako nema ni adr ni drumski
            {
                lblLink.Visible = txtLink.Visible = false;
                lblCutOffPort.Visible = dtpCutOffPort.Visible = false;
            }
            else if (scenarioID == 9 && drumski == 1)
            {
                lblLink.Visible = txtLink.Visible = true;
                lblCutOffPort.Visible = dtpCutOffPort.Visible = true;
            }
        }


        private void PostaviVidljivostBrodskaPlomba()
        {

            if ((scenarioID == 7 && drumski == 1) || scenarioID == 9 || scenarioID == 25)
            {
                lblBrodskaPlombaBroj.Visible = false;
                txtBrodskaPlombaBroj.Visible = false;
            }

        }
        private void PostaviVidljivostNacinPakovanja()
        {

            bool isGrupa3 = (scenarioID == 8 || scenarioID == 24 || scenarioID == 9 || scenarioID == 25);
            lblNacinPakovanja.Visible = isGrupa3;
            cboNacinPakovanja.Visible = isGrupa3;

        }
        private void PodesiUnutrasnjostGrupe2(int scenarioID)
        {
            // 
            bool isOsnovniIliA = ((scenarioID == 7 || scenarioID == 23) && drumski == 0); // II ili II-A

            lblNalogodavacZaDrumski.Visible = cboNalogodavacZaDrumski.Visible = isOsnovniIliA;
            lblRef3.Visible = txtRef3.Visible = isOsnovniIliA;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (noviIDs.Count == 0)
            {
                MessageBox.Show("Morate prvo snimiti grupne podatke za kontejnere!");
                return;
            }
            frmCarinskiPostupak cpo = new frmCarinskiPostupak(noviIDs, drumski, scenarioID);
            cpo.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (noviIDs.Count == 0)
            {
                MessageBox.Show("Morate prvo snimiti grupne podatke za kontejnere!");
                return;
            }
            frmRelacija cpo = new frmRelacija(noviIDs, drumski, scenarioID);
            cpo.Show();
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (redJePromijenjen)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Preskoči ako je u pitanju prazni red na dnu za novi unos
                if (row.IsNewRow) return;

                SnimiIzmeneReda(row);
                redJePromijenjen = false;
            }
        }
        private void SnimiIzmeneReda(DataGridViewRow row)
        {


            InsertIzvoz ins = new InsertIzvoz();
            //  Provera da li je red nov ili prazan (opciono)
            if (row.IsNewRow) return;

            if (row.Cells["ID"].Value == null || row.Cells["ID"].Value == DBNull.Value) return;

            if (!ValidacijaGrida(row))
                return;


            int id = Convert.ToInt32(row.Cells["ID"].Value);
            decimal? bTTRobe = null;
            decimal? nTTORobe = null;
            int? koletaFakture = null;
            decimal? cBMFaktura = null;
            decimal? vrednostRobe = null;
            string brojKontejnera = null;
            string ostalePlombe = null;

            // Izvlačenje ostalih vrednosti iz ćelija

            if (row.DataGridView.Columns.Contains("BrojKontejnera") && row.Cells["BrojKontejnera"].Value != null && row.Cells["BrojKontejnera"].Value != DBNull.Value)
            {
                brojKontejnera = (row.Cells["BrojKontejnera"].Value == null || row.Cells["BrojKontejnera"].Value == DBNull.Value || string.IsNullOrWhiteSpace(row.Cells["BrojKontejnera"].Value.ToString())) ? null : row.Cells["BrojKontejnera"].Value.ToString().Trim();

            }
            if (row.DataGridView.Columns.Contains("OstalePlombe") && row.Cells["OstalePlombe"].Value != null && row.Cells["OstalePlombe"].Value != DBNull.Value)
            {
                ostalePlombe = (row.Cells["OstalePlombe"].Value == null || row.Cells["OstalePlombe"].Value == DBNull.Value || string.IsNullOrWhiteSpace(row.Cells["OstalePlombe"].Value.ToString())) ? null : row.Cells["OstalePlombe"].Value.ToString().Trim();

            }
            if (row.DataGridView.Columns.Contains("BTTRobe") && row.Cells["BTTRobe"].Value != null && row.Cells["BTTRobe"].Value != DBNull.Value)
            {
                bTTRobe = Convert.ToDecimal(row.Cells["BTTRobe"].Value);
            }
            if (row.DataGridView.Columns.Contains("NTTORobe") && row.Cells["NTTORobe"].Value != null && row.Cells["NTTORobe"].Value != DBNull.Value)
            {
                nTTORobe = Convert.ToDecimal(row.Cells["NTTORobe"].Value);
            }
            if (row.DataGridView.Columns.Contains("KoletaFakture") && row.Cells["KoletaFakture"].Value != null && row.Cells["KoletaFakture"].Value != DBNull.Value)
            {
                koletaFakture = Convert.ToInt32(row.Cells["KoletaFakture"].Value);
            }
            if (row.DataGridView.Columns.Contains("CBMFaktura") && row.Cells["CBMFaktura"].Value != null && row.Cells["CBMFaktura"].Value != DBNull.Value)
            {
                cBMFaktura = Convert.ToDecimal(row.Cells["CBMFaktura"].Value);
            }
            if (row.DataGridView.Columns.Contains("VrednostRobe") && row.Cells["VrednostRobe"].Value != null && row.Cells["VrednostRobe"].Value != DBNull.Value)
            {
                vrednostRobe = Convert.ToDecimal(row.Cells["VrednostRobe"].Value);
            }
            int napomenaPozicioniranje = -1;
            string napomeneZaPozTekst = null;
            if (row.DataGridView.Columns.Contains("NapomenaZaPozicioniranje") && row.Cells["NapomenaZaPozicioniranje"].Value != null && row.Cells["NapomenaZaPozicioniranje"].Value != DBNull.Value)
            {
                napomenaPozicioniranje = Convert.ToInt32(row.Cells["NapomenaZaPozicioniranje"].Value);
                napomeneZaPozTekst = row.Cells["NapomenaZaPozicioniranje"].FormattedValue?.ToString();
            }

            bool autoValue = false;
            if ((scenarioID == 9 || scenarioID == 8 || scenarioID == 25) && drumski == 1) // u ovim slucajevima automatski popuni vrednost dt polja ako nije upisana vrednost
            {
                autoValue = true;
            }
            DateTime? planiranDtSpustanjaPunog = GetDateValue(row, "SpustanjePunogNoviPlaniraniDt", false);
            DateTime? dtRealizacijeSpustanjaPunog = GetDateValue(row, "SpustanjePunogDtRealizacije", true);
            DateTime? planiranDtPreuzimanjaPraznog = GetDateValue(row, "PreuzimanjePraznogNoviPlaniraniDt", false);
            DateTime? dtPreuzimanjaPraznog = GetDateValue(row, "PreuzimanjePraznogPlaniraniDt", false);
            DateTime? dtRealizacijePreuzimanjaPraznog = GetDateValue(row, "PreuzimanjePraznogDtRealizacije", true);
            DateTime? dtPreuzimanjaPunog = GetDateValue(row, "PreuzimanjePunogPlaniraniDt", false);
            DateTime? planiranDtPreuzimanjaPunog = GetDateValue(row, "PreuzimanjePunogNoviPlaniraniDt", false);
            DateTime? dtRealizacijePreuzimanjaPunog = GetDateValue(row, "PreuzimanjePunogDtRealizacije", true);
            DateTime? planiranDtIstovaraCerade = GetDateValue(row, "IstovarCeradeNoviPlaniraniDt", autoValue);
            DateTime? dtIstovaraCerade = GetDateValue(row, "IstovarCeradePlaniraniDt", autoValue);
            DateTime? dtRealizacijeIstovaraCerade = GetDateValue(row, "IstovarCeradeDtRealizacije", true);
            DateTime? planiranDtUtovaraKontejnera = GetDateValue(row, "MestoUtovaraNoviPlaniraniDt", false);
            DateTime? dtRealizacijeUtovaraKontejnera = GetDateValue(row, "MestoUtovaraDtRealizacije", true);
            DateTime? planiranDtUtovaraCerade = GetDateValue(row, "UtovarCeradeNoviPlaniraniDt", false);
            DateTime? dtRealizacijeUtovaraCerade = GetDateValue(row, "UtovarCeradeDtRealizacije", true);


            string vozilo = null;

            if (row.DataGridView.Columns.Contains("Vozilo"))
            {
                var cellValue = row.Cells["Vozilo"].Value;
                vozilo = cellValue?.ToString().Trim();

                if (string.IsNullOrWhiteSpace(vozilo))
                    vozilo = null;
            }


            string vozac = null;
            if (row.DataGridView.Columns.Contains("Vozac"))
            {
                var cellValue = row.Cells["Vozac"].Value;
                vozac = cellValue?.ToString().Trim();

                if (string.IsNullOrWhiteSpace(vozac))
                    vozac = null;
            }

            string brojLK = null;
            if (row.DataGridView.Columns.Contains("brojLK"))
            {
                var cellValue = row.Cells["brojLK"].Value;
                brojLK = cellValue?.ToString().Trim();

                if (string.IsNullOrWhiteSpace(brojLK))
                    brojLK = null;
            }

            string telefon = null;
            if (row.DataGridView.Columns.Contains("BrojTelefona"))
            {
                var cellValue = row.Cells["BrojTelefona"].Value;
                telefon = cellValue?.ToString().Trim();

                if (string.IsNullOrWhiteSpace(telefon))
                    telefon = null;
            }



            InsertIzvozKonacna uvK = new InsertIzvozKonacna();

            try
            {
                ins.UpdateIzvozPorudzbenicaPojedinacna(id, brojKontejnera, ostalePlombe, bTTRobe, nTTORobe, koletaFakture, cBMFaktura, vrednostRobe, vozilo, vozac, brojLK, telefon,
                     planiranDtSpustanjaPunog, dtRealizacijeSpustanjaPunog, planiranDtPreuzimanjaPraznog, dtPreuzimanjaPraznog, dtRealizacijePreuzimanjaPraznog, dtPreuzimanjaPunog, planiranDtPreuzimanjaPunog, dtRealizacijePreuzimanjaPunog,
                     planiranDtIstovaraCerade, dtIstovaraCerade, dtRealizacijeIstovaraCerade, planiranDtUtovaraKontejnera, dtRealizacijeUtovaraKontejnera, planiranDtUtovaraCerade, dtRealizacijeUtovaraCerade);

                if (napomenaPozicioniranje > -1)
                    uvK.InsIzvozNapomenePozicioniranja(id, napomenaPozicioniranje, napomeneZaPozTekst);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri čuvanju reda: " + ex.Message);
                row.DefaultCellStyle.BackColor = Color.Salmon;
            }
        }

        private DateTime? GetDateValue(DataGridViewRow row, string columnName, bool autoValue)
        {
            if (!row.DataGridView.Columns.Contains(columnName))
                return null;

            var column = row.DataGridView.Columns[columnName];

            if (!column.Visible)
                return DateTime.Today;

            var value = row.Cells[columnName].Value;

            if (value != null && value != DBNull.Value && DateTime.TryParse(value.ToString(), out DateTime parsed))
                return parsed;

            return autoValue ? DateTime.Today : (DateTime?)null;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            redJePromijenjen = true;
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            string colName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;

            // Ako NIJE datumska kolona, radi CommitEdit
            if (!colName.EndsWith("Dt") && !colName.EndsWith("DtRealizacije") && !colName.EndsWith("Dt1"))
            {
                if (dataGridView1.IsCurrentCellDirty)
                {
                    // Ovo prisiljava Grid da odmah registruje promjenu (i okine CellValueChanged)
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
                // Ako JESTE datum, čekamo da korisnik završi (izađe iz ćelije)
                // Ne zovemo CommitEdit ovde za datume!
            }
        }
        string pickUp = "";
        string pickUp2 = "";
        string pickUp3 = "";
        int pickupValue = 0;
        int pickupValue2 = 0;
        int pickupValue3 = 0;

        int pp = 0;

        private void btnUsluge_Click(object sender, EventArgs e)
        {
            ///Ovde se mora proveriti

            if (drumski == 1 || chkVaganje.Checked)
            {
                string idsZaUpit = string.Join(", ", noviIDs);

                string query = $"SELECT * FROM IzvozVrstaManipulacije WHERE IDNadredjena IN ({idsZaUpit})";
                SqlConnection conn = new SqlConnection(connection);
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                System.Data.DataTable dtManipulacije = new System.Data.DataTable();
                da.Fill(dtManipulacije);

                InsertIzvoz uvK = new InsertIzvoz();

                string pomPokret = "/";
                int pomStatusKontejnera = Convert.ToInt32(0);
                string pomForma = "/";
                double pomCena = 0;
                double pomkolicina = 1;
                int pomPlatilac = 0;
                int pomOrgJed84 = 0;
                int pomOrgJed102 = 0;

                bool drumskiAktivan = (drumski == 1);

                pomOrgJed84 = VratiOrgJed(84);
                pomOrgJed102 = VratiOrgJed(102);

                foreach (int trenutniID in noviIDs)
                {
                    // ---USLOV 1: PROVERA ZA DRUMSKI (84) ---
                    if (drumski == 1)
                    {
                        // Proveravamo da li u izvučenim podacima postoji red sa ovim ID-jem i manipulacijom 84
                        bool postoji84 = dtManipulacije.AsEnumerable().Any(r =>
                            r.Field<int>("IDNadredjena") == trenutniID &&
                            r.Field<int>("idVrstaManipulacije") == 84);

                        if (!postoji84)
                        {

                            uvK.InsUbaciUslugu(trenutniID, 84, pomCena, pomkolicina, pomOrgJed84, pomPlatilac, 0, pomPokret, pomStatusKontejnera, pomForma);
                        }
                    }

                    // // --- USLOV 2: Vaganje i manipulacija 102 ---
                    if (chkVaganje.Checked)
                    {
                        // Proveravamo da li postoji red sa ovim ID-jem i manipulacijom 102
                        bool postoji102 = dtManipulacije.AsEnumerable().Any(r =>
                            r.Field<int>("IDNadredjena") == trenutniID &&
                            r.Field<int>("idVrstaManipulacije") == 102);

                        if (!postoji102)
                        {

                            uvK.InsUbaciUslugu(trenutniID, 102, pomCena, pomkolicina, pomOrgJed102, pomPlatilac, 0, pomPokret, pomStatusKontejnera, pomForma);
                        }
                    }
                }
            }

            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("Morate izabrati / označiti red u gridu!", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            txtID.Text = selectedRow.Cells[0].Value?.ToString();
            int Scenario = VratiScenarioSelektovanog(Convert.ToInt32(txtID.Text));

            int terminal = 0;
            if (txtID.Text == "")
            { txtID.Text = "0"; }
            //if (chkTerminal.Checked) { terminal = 1; }
            //string pickUp = cboPPCNT.Text.ToString().TrimEnd();
            //string pickUp2 = cboPPCNT2.Text.ToString().TrimEnd();
            //string pickUp3 = cboPPCNT3.Text.ToString().TrimEnd();
            VratiPikup(Convert.ToInt32(txtID.Text));

            pp = ProveriPrazanPun();

            if (cboVrstaKontejnera.Text.Length > 3)
            {
                if (pp == 0)
                {
                    //PRAZAN
                    VratiRepoziciju(pickupValue, pickupValue2, pickupValue3, cboVrstaKontejnera.Text.Substring(0, 3));
                }
                else
                {
                    //PUN
                    VratiZelezninu(pickupValue, pickupValue2, pickupValue3, cboVrstaKontejnera.Text.Substring(0, 3));

                }
            }


            int ADR = Convert.ToInt32(cboADR.SelectedValue);

            MoguciScenario();

            //

            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            frmIzvozUnosManipulacije um = new frmIzvozUnosManipulacije(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboNalogodavacZaUsluge.SelectedValue), Convert.ToInt32(cboNalogodavacZaDrumski.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue), terminal, pickUp, ScenarioGL, ADR, pp, Zeleznina, Repozicija, Scenario);
            um.Show();

        }

        int VratiScenarioSelektovanog(int Kont)
        {
            int SC = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Scenario from Izvoz " +
                " where ID = " + Kont, con);
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {

                SC = Convert.ToInt32(dr["Scenario"].ToString());

            }
            con.Close();

            return SC;

        }

        int VratiOrgJed(int Manipulacija)
        {
            int pomOJ = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select OrgJed from VrstaManipulacije  where ID= " + Manipulacija, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                pomOJ = Convert.ToInt32(dr["OrgJed"].ToString());
            }
            con.Close();
            return pomOJ;

        }

        List<int> VratiListuKontejnera(int BrojStavkePorudzbenice, int GrupID)
        {
      
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select ID from Izvoz  where BrojStavkePorudzbenice= " + BrojStavkePorudzbenice + " AND GrupID = " + GrupID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                //Izmenjeno
                // txtSopstvenaMasa2.Value = Convert.ToDecimal(dr["SopM"].ToString());
                noviIDs.Add(Convert.ToInt32(dr["ID"].ToString()));
            }
            con.Close();
            return noviIDs;

        }

        int Repozicija = 0;
        int ProveriPrazanPun()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int idnhm = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 IDNHM from IzvozNHM where IDNadredjena = " + Convert.ToInt32(txtID.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                idnhm = Convert.ToInt32(dr["IDNHM"].ToString());

            }

            con.Close();

            return idnhm;
        }

        private void VratiPikup(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select i.ID, i.MestoPreuzimanja, (kt.Naziv + ' - ' + kt.Oznaka) as MestoPreuzimanjaNaziv,i.MestoPreuzimanja2, (kt2.Naziv + ' - ' + kt2.Oznaka) as MestoPreuzimanjaNaziv2," +
                "i.MestoPreuzimanja3,(kt3.Naziv + ' - ' + kt3.Oznaka) as MestoPreuzimanjaNaziv3  " +
                "from Izvoz i  " +
                " LEFT JOIN KontejnerskiTerminali kt on kt.ID = i.MestoPreuzimanja " +
                " LEFT JOIN KontejnerskiTerminali kt2 on kt2.ID = i.MestoPreuzimanja2 " +
                " LEFT JOIN KontejnerskiTerminali kt3 on kt3.ID = i.MestoPreuzimanja3 " +
                " where i.ID = " + ID, con);
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {
                pickUp = dr["MestoPreuzimanja"].ToString();
                pickUp2 = dr["MestoPreuzimanja2"].ToString();
                pickUp3 = dr["MestoPreuzimanja3"].ToString();
                pickupValue = (dr["MestoPreuzimanja"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MestoPreuzimanja"]);
                pickupValue2 = (dr["MestoPreuzimanja2"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MestoPreuzimanja2"]);
                pickupValue3 = (dr["MestoPreuzimanja3"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["MestoPreuzimanja3"]);


            }
            con.Close();


        }

        private void VratiRepoziciju(int pickUp, int pickUp2, int pickUp3, string TipKOntejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select VrstaManipulacije.ID, Relacija from VrstaManipulacije  " +
                " inner join TipKontenjera on VrstaManipulacije.TipKontejnera = TipKontenjera.ID " +
                " where GrupaVrsteManipulacijeID = 2 and Substring(TipKontenjera.SkNaziv,1,3) = '" + TipKOntejnera + "'' AND RLTerminali = " +
                pickUp + " and RLTerminali2 = " + pickUp2 + " AND RLTerminali3 = " + pickUp3, con);
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {

                Repozicija = Convert.ToInt32(dr["ID"].ToString());
                //  relacija = dr["Relacija"].ToString();
            }
            con.Close();


        }
        int Zeleznina = 0;
        string relacija = "";
        private void VratiZelezninu(int pickUp, int pickUp2, int pickUp3, string TipKOntejnera)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select VrstaManipulacije.ID, Relacija from VrstaManipulacije  " +
                " inner join TipKontenjera on VrstaManipulacije.TipKontejnera = TipKontenjera.ID" +
                " where GrupaVrsteManipulacijeID = 1 and Substring(TipKontenjera.SkNaziv,1,3) = '" + TipKOntejnera + "'' AND RLTerminali = " +
                pickUp + " and RLTerminali2 = " + pickUp2 + " AND RLTerminali3 = " + pickUp3, con);
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {

                Zeleznina = Convert.ToInt32(dr["ID"].ToString());
                relacija = dr["Relacija"].ToString();
            }
            con.Close();


        }


        int ScenarioGL = 0;
        private void MoguciScenario()
        {
            string Moguce = "";
            if (pickUp == "Leget -" && pickUp2 == "Leget -" && pickUp3 != "Leget -")
            {
                ScenarioGL = 1; ///Leget - Leget - Krajnja destinacija

            }
            else if (pickUp != "Leget -" && pickUp3 != "Leget -")
            {
                ScenarioGL = 2; //Drugi terminal - Leget - Krajnja destinacija
            }
            else if (pickUp != "Leget -" && pickUp3 == "Leget -")
            {
                ScenarioGL = 3; //Drugi terminal - Leget - Krajnja destinacija
            }

            else
            {
                MessageBox.Show("Relacije ne pripadaju ni jednom scenariju");
                return;
            }


            //Provera SCENARIJA UKLJUCITI ADR
            if (ScenarioGL == 1 && Convert.ToInt32(cboADR.SelectedValue) == 0 && pp > 1)
            {
                Moguce = "7,8,9"; // Leget - Leget - NestoDrugo - BEZ ADR - pun
            }
            else if (ScenarioGL == 1 && Convert.ToInt32(cboADR.SelectedValue) > 1 && pp > 1)
            {
                Moguce = "23,24,25";  // PUN
            }
            else if (ScenarioGL == 2 && Convert.ToInt32(cboADR.SelectedValue) == 0 && pp > 1)
            {
                Moguce = "13"; // PUN Ostaje na terminalu \"11,12,13,14,29
            }
            else if (ScenarioGL == 1 && Convert.ToInt32(cboADR.SelectedValue) == 0 && pp == 0)
            {
                Moguce = "12,29"; // PRAYAN
            }
            else if (ScenarioGL == 2 && Convert.ToInt32(cboADR.SelectedValue) > 1 && pp > 1)
            {
                Moguce = "25,26";
            }

            else if (ScenarioGL == 3 && Convert.ToInt32(cboADR.SelectedValue) == 0 && pp == 0)
            {
                Moguce = "14";
            }


            int poklapase = 0;
            string[] split = Moguce.Split(',');
            foreach (string item in split)
            {
                if (item == scenarioID.ToString())
                {
                    poklapase = 1;
                }

            }

            if (poklapase == 0)
            {
                int k = ProveriImaUsluga(txtID.Text);
                if (k == 0)
                {
                    //Kada jos nisu definisane usluge
                }
                else
                {
                    DialogResult result = MessageBox.Show("Trenutni scenario je" + ScenarioNaziv + " moguci scenariji " + Moguce + " se ne poklapaju sa njim, da li želite brisanje terminalskih usluge?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        InsertIzvozKonacna uvK = new InsertIzvozKonacna();
                        uvK.DelIzvozUslugaTerminalskih(Convert.ToInt32(txtID.Text));
                    }
                    else
                    {
                        //...
                    }
                }


            }
        }
        int ProveriImaUsluga(string UvozID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT Count(*)  as Broj" +
        "  FROM [IzvozVrstaManipulacije] where IDNAdredjena=" + UvozID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                return Convert.ToInt32(dr["Broj"].ToString());
            }
            con.Close();
            return 0;

        }

        private void btnUnesiNHM_Click(object sender, EventArgs e)
        {
            if (cboVrstaRobe.SelectedValue == null) return;

            // Kreiramo novi objekat 
            PrivremeniNHM novaStavka = new PrivremeniNHM
            {
                IDNHM = Convert.ToInt32(cboVrstaRobe.SelectedValue),
                Naziv = cboVrstaRobe.Text
            };

            // Dodajemo u listu
            privremenaListaNHM.Add(novaStavka);


            //OsveziGridNHM();
            System.Data.DataTable dtPrivremeni = new System.Data.DataTable();
            dtPrivremeni.Columns.Add("IDNHM");
            dtPrivremeni.Columns.Add("Naziv");

            foreach (var stavka in privremenaListaNHM)
            {
                dtPrivremeni.Rows.Add(stavka.IDNHM, stavka.Naziv);
            }

            OsveziGridNHM(dtPrivremeni);

        }

        private void ProveriRelacijuGrupe1(int scenarioID)
        {

            // Vratiti da li su popunjena polja
            // cboMestoSpustanjaPunogKontejnera.Visible = true;

            bool isUkljucenDrumski = ((scenarioID == 13 || scenarioID == 26) && drumski == 1);
          // cboMestoPreuzimanjaPunog.Visible = isUkljucenDrumski;
          //  dptPlaniranDatumSpustanja.Visible = !isUkljucenDrumski;
           // txtDodatneNapomeneDrumski1.Visible = isUkljucenDrumski;

        }

        private void ProveriRelacijuGrupe2(int scenarioID)
        {
            /*
            bool isOsnovniIliA = ((scenarioID == 7 || scenarioID == 23) && drumski == 0); // II ili II-A

            dtpPlaniraniDatumVremePreuzimanja2.Visible = isOsnovniIliA;
           dtpDatumRealizacijeUtovaraKontejnera2.Visible = isOsnovniIliA;
            dptPlaniranDatumSpustanja2.Visible = isOsnovniIliA;
            txtDodatneNapomeneDrumski2.Visible = !isOsnovniIliA;



            if (scenarioID == 7 && drumski == 1)
            {
                cboOdlaznaMorskaLuka2.Visible = false;

            }
            else if (scenarioID == 7 && drumski == 0)
            {
                cboOdlaznaMorskaLuka2.Visible = true;

            }
            */

        }

        private void ProveriRelacijuGrupe3(int scenarioID)
        {
            /*
            bool isDrumski = ((scenarioID == 8 || scenarioID == 24) && drumski == 1); // IIIL ili IIILA

            lblMestoUtovaraCerade3.Visible = cboMestoUtovaraCerade3.Visible = isDrumski;
            lblAdresaUtovaraCerade3.Visible = cboAdresaUtovaraCerade3.Visible = isDrumski;
            lblKontaktUtovaraCerade3.Visible = cboKontaktUtovaraCerade3.Visible = isDrumski;
            lblDatumIstovaraCerade3.Visible = dptDatumIstovaraCerade3.Visible = !isDrumski;

            if (scenarioID == 24 && drumski == 1) // ako je IIILA iskljuci sledeca polja
            {
               

            }
            else if (scenarioID == 24 && drumski == 0)
            {
                lblOdlaznaMorskaLuka2.Visible = cboOdlaznaMorskaLuka2.Visible = true;
                lblMestoIstovaraCerada3.Visible = cboMestoIstovaraCerada3.Visible = true;
                lblAdresaIstovaraCerade3.Visible = cboAdresaIstovaraCerade3.Visible = true;
                lblKontaktOIstovarCerade3.Visible = cboKontaktOIstovarCerade3.Visible = true;
                lblPlaniraniDatumUtovaraKontejnera3.Visible = dptPlaniraniDatumUtovaraKontejnera3.Visible = true;
                lblDatumUtovaraCerade3.Visible = dptDatumUtovaraCerade3.Visible = false;
                lblDodatneNapomenDrumski.Visible = txtDodatneNapomeneDrumski.Visible = false;
            }

            // ako je IIIL
            if (scenarioID == 8 && drumski == 1)
            {
                lblDatumUtovaraCerade3.Visible = dptDatumUtovaraCerade3.Visible = true;
                lblDodatneNapomenDrumski.Visible = txtDodatneNapomeneDrumski.Visible = true;
            }
            else if (scenarioID == 8 && drumski == 0)
            {
                lblDatumUtovaraCerade3.Visible = dptDatumUtovaraCerade3.Visible = false;
                lblDodatneNapomenDrumski.Visible = txtDodatneNapomeneDrumski.Visible = false;
            }
            */

        }


        private void ProveriRelacijuGrupe4(int scenarioID)
        {
            /*
            bool isVisible = (scenarioID == 9 || scenarioID == 25); // IIIL ili IIILA

            lblMestoUtovaraCerade4.Visible = cboMestoUtovaraCerade4.Visible = isVisible;
            lblDatumUtovaraCerade4.Visible = dptDatumUtovaraCerade4.Visible = isVisible;
            lblMestoIstovaraCerada4.Visible = cboMestoIstovaraCerada4.Visible = isVisible;
            lblAdresaIstovaraCerade4.Visible = cboAdresaIstovaraCerade4.Visible = isVisible;
            lblKontaktOIstovarCerade4.Visible = lblKontaktOIstovarCerade4.Visible = isVisible;


            if (scenarioID == 25) // ako je IVLA iskljuci sledeca polja
            {
                if (drumski == 0)
                {
                    lblAdresaUtovaraCerade4.Visible = cboAdresaUtovaraCerade4.Visible = !isVisible;
                    lblKontaktUtovaraCerade4.Visible = cboKontaktUtovaraCerade4.Visible = !isVisible;
                    lblDodatneNapomeneDrumski4.Visible = txtDodatneNapomeneDrumski4.Visible = !isVisible;

                }
                // nova pozicija za DatumUtovaraCerade4
                //lblDatumUtovaraCerade4.Location = new Point(cboMestoUtovaraCerade4.Location.X+10, cboMestoUtovaraCerade4.Location.Y + 36);
                //dptDatumUtovaraCerade4.Location = new Point(lblDatumUtovaraCerade4.Location.X - 2, lblDatumUtovaraCerade4.Location.Y + 21);
                if (drumski == 1)
                {
                    lblDatumIstovaraCerade4.Visible = dptDatumIstovaraCerade4.Visible = !isVisible;
                }
            }
            else if (scenarioID == 9)
            {
                if (drumski == 0)
                {
                    lblKontaktUtovaraCerade4.Visible = cboKontaktUtovaraCerade4.Visible = !isVisible;
                    lblDodatneNapomeneDrumski4.Visible = txtDodatneNapomeneDrumski4.Visible = !isVisible;


                }
                else if (drumski == 1)
                {
                    lblDatumIstovaraCerade4.Visible = dptDatumIstovaraCerade4.Visible = !isVisible;
                }

            }
            */
        }

        private void ProveriPodatkeDaLiSuPripreljeni(int KontejnerID)
        {

            //Proveri Carinsko
            //Proveri relacije
            switch (scenarioID)
            {
                // GRUPA I
                case 13: // Scenario I
                         // Podesi šta treba za čist I
                    ProveriRelacijuGrupe1(scenarioID);
                    break;
                case 26: // Scenario I-L
                    ProveriRelacijuGrupe1(scenarioID);
                    break;
                // GRUPA II
                case 7: // Scenario II
                        // Specifičnosti za II
                    ProveriRelacijuGrupe2(scenarioID);
                    break;
                case 23: // Scenario I-L
                         //  AktivirajLukaPolja();
                    ProveriRelacijuGrupe2(scenarioID);
                    break;

                // GRUPA III
                case 8: // Scenario II
                    // Specifičnosti za II
                    ProveriRelacijuGrupe3(scenarioID);
                    break;
                case 24: // Scenario I-L
                    //  AktivirajLukaPolja();
                    ProveriRelacijuGrupe3(scenarioID);
                    break;
                // GRUPA IV
                case 9: // Scenario II
                    // Specifičnosti za II
                    ProveriRelacijuGrupe4(scenarioID);
                    break;
                case 25: // Scenario I-L
                    //  AktivirajLukaPolja();
                    ProveriRelacijuGrupe4(scenarioID);
                    break;


                default:
                    // Neki default ako ID nije prepoznat
                    break;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertIzvozKonacna ins = new InsertIzvozKonacna();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // if (row.Selected)
                // {
                ProveriPodatkeDaLiSuPripreljeni(Convert.ToInt32(row.Cells[0].Value.ToString()));

                ins.PrenesiUPlanUtovaraIzvoz(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(40));


                // }
            }
            string kor = Sifarnici.frmLogovanje.user;

            Uvoz.InsertRadniNalogInterni rn = new Uvoz.InsertRadniNalogInterni();
            //ins.InsRadniNalogInterni(Convert.ToInt32(1), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), "", Convert.ToInt32(0), "PlanUtovara", Convert.ToInt32(txtNadredjeni.Text), KorisnikTekuci, "");
            rn.InsRadniNalogInterniIzvoz(Convert.ToInt32(2), Convert.ToInt32(4), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime("1.1.1900. 00:00:00"), " ", Convert.ToInt32(0), "PlanUtovaraIZ", Convert.ToInt32(40), kor, " ");


            // Izvoz.frmFormiranjePlanaIzvoz fpi = new Izvoz.frmFormiranjePlanaIzvoz();
            // fpi.Show();

        }
        public class PrivremeniNHM
        {
            //public int PrivremeniID { get; set; } 
            public int IDNHM { get; set; }
            //public string Broj { get; set; }
            public string Naziv { get; set; }
        }

        public class PrivremeniNapomena
        {
            //public int PrivremeniID { get; set; } 
            public int IDNapomene { get; set; }
            //public string Broj { get; set; }
            public string Naziv { get; set; }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (cbNapomenaPoz.SelectedValue == null) return;

            // Kreiramo novi objekat 

             PrivremeniNapomena  novaStavka = new PrivremeniNapomena
             {

                 IDNapomene = Convert.ToInt32(cbNapomenaPoz.SelectedValue),
                Naziv = cbNapomenaPoz.Text
            };

            // Dodajemo u listu
            privremenaListaNapomena.Add(novaStavka);
 


            //OsveziGridNHM();
            System.Data.DataTable dtPrivremeni = new System.Data.DataTable();
            dtPrivremeni.Columns.Add("IDNapomene");
            dtPrivremeni.Columns.Add("Naziv");

            foreach (var stavka in privremenaListaNapomena)
            {
                dtPrivremeni.Rows.Add(stavka.IDNapomene, stavka.Naziv);
            }
            //PPP
            OsveziGridNapomena(dtPrivremeni);



            /*
            InsertIzvozKonacna uvK = new InsertIzvozKonacna();

            uvK.InsIzvozNapomenePozicioniranja(Convert.ToInt32(txtID.Text), Convert.ToInt32(cbNapomenaPoz.SelectedValue), cbNapomenaPoz.Text);
            FillDG4();
            */
        }

        private void FillDG4()
        {
            var select = "select IzvozNapomenePozicioniranja.ID, IDNapomene, stNapomene from IzvozNapomenePozicioniranja " +
"  where IzvozNapomenePozicioniranja.IdNadredjena = " + Convert.ToInt32(txtID.Text) + " order by IzvozNapomenePozicioniranja.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView4.ReadOnly = true;
            dataGridView4.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView4);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView4.Columns[0];
            dataGridView4.Columns[0].HeaderText = "ID";
            dataGridView4.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView4.Columns[1];
            dataGridView4.Columns[1].HeaderText = "NapomenaID";
            dataGridView4.Columns[1].Width = 20;

            DataGridViewColumn column3 = dataGridView4.Columns[2];
            dataGridView4.Columns[2].HeaderText = "Napomena";
            dataGridView4.Columns[2].Width = 160;

        }
    }
}


