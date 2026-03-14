using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
using Saobracaj.MainLeget.LegNew;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Chart;
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
using Testiranje.Sifarnici;

namespace Saobracaj.Izvoz
{
    public partial class frmGrupniUnosPoljaIzvoz: Form
    {

        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        int brojStavkePorudzbenice = 0;
        List<int> noviIDs = new List<int>();
        int izabranaCerada = 0;
        int adr = 0;
        int drumski = 0;
        int scenarioID = 0;
        bool redJePromijenjen = false;
        string ScenarioNaziv = "";
        List<PrivremeniNHM> privremenaListaNHM = new List<PrivremeniNHM>();

        public frmGrupniUnosPoljaIzvoz(int BrojStavkePorudzbenice,  int scenario, int _drumski )
        {
            InitializeComponent();
            ChangeTextBox();
            brojStavkePorudzbenice = BrojStavkePorudzbenice;
            scenarioID = scenario;
            drumski = _drumski;

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


                foreach (Control control in Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); //
                        buttons.ForeColor = Color.White;  //51; 51; 54  -
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  //
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

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
                        label.ForeColor = Color.FromArgb(110, 110, 115); // 
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  //

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // 
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); //
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

  
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);
            this.SuspendLayout();
            // 1. UČITAJ SVE PARTNERE SAMO JEDNOM
            var sqlPartneri = "Select PaSifra, PaNaziv, Brodar, Spediter From Partnerji order by PaNaziv";
            SqlDataAdapter daPart = new SqlDataAdapter(sqlPartneri, conn);
            DataTable dtSviPartneri = new DataTable();
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


            var nhm  = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM order by Naziv";
           
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

            this.ResumeLayout();
        }


        private void OsveziGridNHM()
        {
        // Ako txtID nije prazan, znači da gledamo postojeći kontejner u bazi
        // Novi unos, čitamo iz privremene liste u memoriji
            DataTable dtPrivremeni = new DataTable();
            //dtPrivremeni.Columns.Add("ID"); // Ovde će biti 0 ili onaj negativni ID
            //dtPrivremeni.Columns.Add("Broj");
            dtPrivremeni.Columns.Add("IDNHM");
            dtPrivremeni.Columns.Add("Naziv");

            foreach (var stavka in privremenaListaNHM)
            {
                dtPrivremeni.Rows.Add(/*stavka.PrivremeniID, stavka.Broj,*/ stavka.IDNHM, stavka.Naziv);
            }
            dataGridView2.DataSource = dtPrivremeni;

            dataGridView2.Columns["IDNHM"].Width = 70; // dovoljno za 4 cifre
            dataGridView2.Columns["IDNHM"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dataGridView2.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            PodesiDatagridView(dataGridView2);
            //FormatirajKoloneNHM();
        }

        private void frmGrupniUnosPoljaIzvoz_Load(object sender, EventArgs e)
        {
            FillCombo();
            PostaviVidljivostPoljaADR();
            PostaviVidljivostGrupa4Specificna();
            PostaviVidljivostFakturisabnjeDrumski();
            PostaviVidljivostNacinPakovanja();
            PostaviVidljivostBrodskaPlomba();
            VratiPodatkeSelect();
            //InitializeDataGrid();
            //DGVCombo();
            PodesiDatagridView(dataGridView1);
            dtpCutOffPort.Value = DateTime.Now;
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

                if(dr["Brodar"]!= DBNull.Value)
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
            col.HeaderText = header;
            col.Width = width;
            col.Visible = visible;
            col.ValueType = typeof(DateTime);

            // Postavljamo format prikaza 
            col.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

            dataGridView1.Columns.Add(col);
        }

        private void DGVCombo()
        {
            dataGridView1.Columns.Clear(); // Čistimo stare kolone
            dataGridView1.AutoGenerateColumns = false;

            // Skrivena ID kolona (bitna za update)
            DataGridViewTextBoxColumn id = new DataGridViewTextBoxColumn();
            id.DataPropertyName = "ID"; // Iz baze
            id.Name = "ID";
            id.Visible = false;
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
                AddTextColumn("CBMFaktura", "CBM", 80);
                AddTextColumn("VrednostRobe", "Vrednost", 80);

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
                    AddDateColumn("PreuzimanjePraznogPlaniraniDt", "MESTO PREUZIMANJA PUNOG Plan. Datum/Vreme", 200);
                    AddDateColumn("PreuzimanjePraznogNoviPlaniraniDt", "MESTO PREUZIMANJA PUNOG Novi plan. Datum/Vreme", 200, false);
                    AddDateColumn("PreuzimanjePraznogDtRealizacije", "MESTO PREUZIMANJA PUNOG Datum/Vreme realizacij", 200, false);
                    AddDateColumn("SpustanjePunogDtRealizacije", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                    

                }
                // Numeričke kolone
                AddTextColumn("BTTRobe", "BTT Robe", 80);
                AddTextColumn("NTTORobe", "NTTO Robe", 80);
                AddTextColumn("KoletaFakture", "Koleta", 80);
                AddTextColumn("CBMFaktura", "CBM", 80);
                AddTextColumn("VrednostRobe", "Vrednost", 80);

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
                AddTextColumn("BTTRobe", "BTT Robe", 80);
                if (!(scenarioID == 7 && drumski == 1))
                {
                    AddTextColumn("NTTORobe", "NTTO Robe", 80);
                    AddTextColumn("KoletaFakture", "Koleta", 80);
                    AddTextColumn("CBMFaktura", "CBM", 80);
                    AddTextColumn("VrednostRobe", "Vrednost", 80);

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
                AddTextColumn("CBMFaktura", "CBM", 80);
                AddTextColumn("VrednostRobe", "Vrednost", 80);

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
                AddTextColumn("CBMFaktura", "CBM", 80);
                AddTextColumn("VrednostRobe", "Vrednost", 80);

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
                    AddTextColumn("CBMFaktura", "CBM", 80);
                    AddTextColumn("VrednostRobe", "Vrednost", 80);
                }

            }

            else if (scenarioID == 25)
            {
                if (drumski == 0)
                {

                    AddDateColumn("UtovarCeradePlaniraniDt1", "MESTO UTOVARA CERADE Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradePlaniraniDt1", "MESTO ISTOVARA CERADE Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeDtRealizacije", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);

                    AddTextColumn("Vozilo", "Vozilo", 100);
                    AddTextColumn("Vozac", "Vozač", 120);
                    AddTextColumn("BrojLK", "Broj LK", 100);
                    AddTextColumn("BrojTelefona", "Telefon", 100);
                }
                else if (drumski == 1)
                {
                    AddDateColumn("UtovarCeradeDtRealizacije", "MESTO UTOVARA CERADE Datum/Vreme realizacije", 200, false);
                    AddDateColumn("IstovarCeradePlanirani", "MESTO ISTOVARA CERADE Plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradePlanirani", "MESTO ISTOVARA CERADE  Novi plan. Datum/Vreme", 200);
                    AddDateColumn("IstovarCeradeDtRealizacije", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);
                }
                AddTextColumn("BTTRobe", "BTT Robe", 80);
                AddTextColumn("NTTORobe", "NTTO Robe", 80);
                AddTextColumn("KoletaFakture", "Koleta", 80);
                AddTextColumn("CBMFaktura", "CBM", 80);
                AddTextColumn("VrednostRobe", "Vrednost", 80);

            }

            if (drumski == 1)
            {
                DataGridViewComboBoxColumn napomena = new DataGridViewComboBoxColumn();
                napomena.HeaderText = "Napomena za pozicioniranje";
                napomena.Name = "NapomenaZaPozicioniranje";
                var query212 = "Select ID,Naziv from PredefinisanePoruke order by Naziv";
                SqlConnection conn212 = new SqlConnection(connection);
                SqlDataAdapter da212 = new SqlDataAdapter(query212, conn212);
                System.Data.DataSet ds212 = new System.Data.DataSet();
                da212.Fill(ds212);
                napomena.DataSource = ds212.Tables[0];
                napomena.DisplayMember = "Naziv";
                napomena.ValueMember = "ID";
                napomena.Width = 150;
                dataGridView1.Columns.Add(napomena);
            }


        }
        private void InitializeDataGrid(List<int> noviIDs)
        {
            if (noviIDs.Count == 0) return;

            string idsZaUpit = string.Join(",", noviIDs);

            string query = $@"SELECT ID FROM Izvoz 
                     WHERE ID IN ({idsZaUpit})";

            SqlConnection conn = new SqlConnection(connection);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
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
            //    // Proveravamo samo kolone koje su datumi (npr. kolone sa indeksom 3, 4, 5, 6)
            //    if (e.ColumnIndex >= 3 && e.ColumnIndex <= 6)
            //        {
            //        if (!string.IsNullOrEmpty(e.FormattedValue.ToString()))
            //        {
            //            DateTime temp;
            //            if (!DateTime.TryParse(e.FormattedValue.ToString(), out temp))
            //            {
            //                MessageBox.Show($"Molimo unesite ispravan datum (npr. {DateTime.Now:dd-MM-yyyy HH:mm})");
            //                e.Cancel = true; // Zaustavlja korisnika da pređe u drugu ćeliju
            //            }
            //        }
            //    }
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
            TextBox tb = e.Control as TextBox;
            string columnName = dataGridView1.CurrentCell.OwningColumn.Name;

            if (columnName == "BTTRobe" || columnName == "NTTORobe" ||  columnName == "KoletaFakture" || columnName == "CBMFaktura" ||  columnName == "VrednostRobe")
            {
             
                if (tb != null)
                {
                    tb.KeyPress -= new KeyPressEventHandler(NumericColumn_KeyPress);
                    tb.KeyPress += new KeyPressEventHandler(NumericColumn_KeyPress);
                }
            }
            else if (columnName.EndsWith("Dt") || columnName.EndsWith("DtRealizacije"))
            {
                // Skidamo numerički handler ako je ostao
                tb.KeyPress -= NumericColumn_KeyPress;

                // OVDE JE TRIK: Isključujemo uzbunu dok je polje u fokusu
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
            errorProvider1.Clear(); // Obavezno prvo očisti stare greške

            var allowedScenarios = new HashSet<int> { 13, 26, 7, 23, 8, 24 }; // grupa I, II, III

            if (allowedScenarios.Contains(scenarioID))
            {
                if (string.IsNullOrWhiteSpace(txtBrojKontejnera.Text) || !int.TryParse(txtBrojKontejnera.Text, out _))
                {
                    errorProvider1.SetError(txtBrojKontejnera, "Polje je obavezno i očekuje broj!");
                    uspesno = false;
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
            if (scenarioID == 13 && drumski == 1) //|| (scenarioID == 9 && drumski == 1)
            {
                if (!ValidirajObaveznuKolonu(dataGridView1, row, "PreuzimanjePunogPlaniraniDt", "Ovo polje je obavezno polje!"))
                {
                    uspesno = false;
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
            int vrstaKamiona = 0;
            DateTime? cutOffPort = null;
            decimal? bttRobe = null;
            decimal? nttoRobe= null;
            decimal? koleta= null;
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
                izvoznik=  Convert.ToInt32(cboIzvoznik.SelectedValue);
            }

            int? pomVaganje =null;
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
            if(dtpCutOffPort.Tag=="IZMENJEN")
                cutOffPort = GetVisibleDateTimeValue(dtpCutOffPort);


            int brojKontejnera = 0;
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
                                                       safeVrstaRobe);

                    InsertIzvoz uvK = new InsertIzvoz();
                    foreach (int kontejnerID in noviIDs)
                    {

                        foreach (var stavka in privremenaListaNHM)
                        {
                            uvK.InsIzvozNHM(kontejnerID, stavka.IDNHM);
                        }
                    }
                    MessageBox.Show("Uspešno formirano!");
                    InitializeDataGrid(noviIDs);
                    DGVCombo();
                }
                catch (Exception ex)
                {
                    // Ovde hvatamo onu grešku iz SQL-a 
                    MessageBox.Show(ex.Message, "Pažnja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private DateTime? GetVisibleDateTimeValue(DateTimePicker dtp)
        {
            // Ako panel uopšte nije vidljiv, ili je vidljiv ali ništa nije izabrano
            if (!dtp.Visible)
            {
                return null;
            }

            // Pokušaj konverzije u int
            if (DateTime.TryParse(dtp.Value.ToString(), out DateTime rezultat))
            {
                return rezultat;
            }

            return null;
        }
        private void PostaviVidljivostPoljaADR()
        {
            bool isADR = ( scenarioID == 23 || scenarioID == 24 || scenarioID == 25 || scenarioID == 26) ; // II ili II-A
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
            else if(scenarioID == 9 && drumski == 1)
            {
                lblLink.Visible = txtLink.Visible = true;
                lblCutOffPort.Visible = dtpCutOffPort.Visible = true;
            }
        }
        

        private void PostaviVidljivostBrodskaPlomba()
        {

           if((scenarioID == 7 && drumski == 1) || scenarioID == 9 || scenarioID == 25)
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
            if (row.DataGridView.Columns.Contains("BTTRobe") && row.Cells["BTTRobe"].Value != null &&   row.Cells["BTTRobe"].Value != DBNull.Value)
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
            if (row.DataGridView.Columns.Contains("NapomenaZaPozicioniranje") && row.Cells["NapomenaZaPozicioniranje"].Value != null &&  row.Cells["NapomenaZaPozicioniranje"].Value != DBNull.Value)
            {
                napomenaPozicioniranje = Convert.ToInt32(row.Cells["NapomenaZaPozicioniranje"].Value);
                napomeneZaPozTekst = row.Cells["NapomenaZaPozicioniranje"].FormattedValue?.ToString();
            }

            bool autoValue = false;
            if ((scenarioID == 9 || scenarioID == 8 || scenarioID == 25) && drumski == 1) // u ovim slucajevima automatski popuni vrednost dt polja ako nije upisana vrednost
            {
                autoValue = true;
            }
            DateTime? planiranDtSpustanjaPunog = GetDateValue(row, "SpustanjePunogNoviPlaniraniDt",false);
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
                ins.UpdateIzvozPorudzbenicaPojedinacna(id, brojKontejnera, ostalePlombe, bTTRobe, nTTORobe, koletaFakture, cBMFaktura, vrednostRobe,  vozilo,  vozac, brojLK, telefon,
                     planiranDtSpustanjaPunog, dtRealizacijeSpustanjaPunog, planiranDtPreuzimanjaPraznog, dtPreuzimanjaPraznog, dtRealizacijePreuzimanjaPraznog, dtPreuzimanjaPunog, planiranDtPreuzimanjaPunog, dtRealizacijePreuzimanjaPunog,
                     planiranDtIstovaraCerade, dtIstovaraCerade, dtRealizacijeIstovaraCerade, planiranDtUtovaraKontejnera, dtRealizacijeUtovaraKontejnera, planiranDtUtovaraCerade, dtRealizacijeUtovaraCerade);
               
                if(napomenaPozicioniranje > -1)
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
            if (!colName.EndsWith("Dt") && !colName.EndsWith("DtRealizacije"))
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
    

            if (drumski == 1 || chkVaganje.Checked)
            {
                string idsZaUpit = string.Join(", ", noviIDs);

                string query = $"SELECT * FROM IzvozVrstaManipulacije WHERE IDNadredjena IN ({idsZaUpit})";
                SqlConnection conn = new SqlConnection(connection);
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dtManipulacije = new DataTable();
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

            // int IDPlana, int ID, int Nalogodavac1, int Nalogodavac2, int Nalogodavac3
            frmIzvozUnosManipulacije um = new frmIzvozUnosManipulacije(Convert.ToInt32(0), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboNalogodavacZaUsluge.SelectedValue), Convert.ToInt32(cboNalogodavacZaDrumski.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue), terminal, pickUp, ScenarioGL, ADR, pp, Zeleznina, Repozicija);
            um.Show();
         
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
                " where i.ID = " + ID , con);
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

            // Kreiramo novi objekat na osnovu klase koju smo dole definisali
            PrivremeniNHM novaStavka = new PrivremeniNHM
            {
                // Generišemo negativni ID (-1, -2, -3...)
                //PrivremeniID = (privremenaListaNHM.Count + 1) * -1,
                IDNHM = Convert.ToInt32(cboVrstaRobe.SelectedValue),
                //Broj = "", // Možeš dopuniti ako treba
                Naziv = cboVrstaRobe.Text
            };

            // Dodajemo u listu
            privremenaListaNHM.Add(novaStavka);

            // Pozivamo tvoju metodu za osvežavanje grida
            OsveziGridNHM();
        }

     
    }
    public class PrivremeniNHM
    {
        //public int PrivremeniID { get; set; } 
        public int IDNHM { get; set; }      
        //public string Broj { get; set; }
        public string Naziv { get; set; }
    }
}
