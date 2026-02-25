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

        public frmGrupniUnosPoljaIzvoz(int BrojStavkePorudzbenice,  int scenario, int _drumski )
        {
            InitializeComponent();
            ChangeTextBox();
            brojStavkePorudzbenice = BrojStavkePorudzbenice;
            scenarioID = scenario;
            drumski = _drumski;
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

        private void frmGrupniUnosPoljaIzvoz_Load(object sender, EventArgs e)
        {
            FillCombo();
            PostaviVidljivostPoljaADR();
            PostaviVidljivostFakturisabnjeDrumski();
            PostaviVidljivostNacinPakovanja();
             VratiPodatkeSelect();
            //InitializeDataGrid();
            //DGVCombo();
            PodesiDatagridView(dataGridView1);
            dtpCutOffPort.Value = DateTime.Now;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
           
        }
        private void VratiPodatkeSelect()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [KvalitetKontejnera] ,[Brodar], [Link], [BukingNumber], [CuttOfPort], [Izvoznik],[Nalogodavac], [OpisPosla], ProdajniNalogIzvoz.ID " +
                                            " FROM [dbo].[ProdajniNalogIzvozStavke] " +
                                            " INNER JOIN ProdajniNalogIzvoz on ProdajniNalogIzvozStavke.IDNAdredjenog = ProdajniNalogIzvoz.ID" +
                                            " inner join TipKontenjera on TipKontenjera.ID = ProdajniNalogIzvozStavke.TipKontejnera" +
                                            " where IDNAdredjenog =" + brojStavkePorudzbenice, con);

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
                    dtpCutOffPort.Value = Convert.ToDateTime(dr["CuttOfPort"].ToString());


                txtKorisnik.Text = tKorisnik;
                if (dr["Nalogodavac"] != DBNull.Value)
                    cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                txtopisPosla.Text = dr["OpisPosla"].ToString();

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

            // Tekstualne kolone
            AddTextColumn("BrojKontejnera", "Broj kontejnera", 120);
            AddTextColumn("OstalePlombe", "Ostale plombe", 120);

            // DateTime kolone

            //grupa I
            if (scenarioID == 13)
            {
                if (drumski == 0)
                {
                    AddTextColumn("SpustanjePunogNoviPlaniraniDt1", "MESTO SPUSTANJA PUNOG Novi plan. Datum/Vreme", 200);
                    AddTextColumn("SpustanjePunogDtRealizacije1", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                }

                else if (drumski == 1)
                {
                    AddTextColumn("SpustanjePunogDtRealizacije1", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                    AddTextColumn("PreuzimanjePraznogPlaniraniDt1", "MESTO PREUZIMANJA PUNOG Plan. Datum/Vreme", 200);
                    AddTextColumn("PreuzimanjePraznogDtRealizacije1", "MESTO PREUZIMANJA PUNOG Datum/Vreme realizacije", 200);
                }

            }

            else if (scenarioID == 26)
            {
                if (drumski == 0)
                {
                    AddTextColumn("SpustanjePunogNoviPlaniraniDt1", "MESTO SPUSTANJA PUNOG Novi plan. Datum/Vreme", 200);
                    AddTextColumn("SpustanjePunogDtRealizacije1", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                }

                else if (drumski == 1)
                {
                    AddTextColumn("SpustanjePunogDtRealizacije1", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                    AddTextColumn("PreuzimanjePraznogPlaniraniDt1", "MESTO PREUZIMANJA PUNOG Plan. Datum/Vreme", 200);

                }

            }

            else if ((scenarioID == 7 || scenarioID == 23))
            {
                if (drumski == 0)
                {
                    AddTextColumn("PreuzimanjePraznogNoviPlaniraniDt1", "MESTO PREUZIMANJA PRAZNOG Novi plan. Datum/Vreme", 200);
                    AddTextColumn("PreuzimanjePraznogDtRealizacije1", "MESTO PREUZIMANJA PRAZNOG Datum/Vreme realizacije", 200);
                    AddTextColumn("MestoUtovaraNoviPlaniraniDt1", "MESTO UTOVARA KON. Novi plan. Datum/Vreme", 200);
                    AddTextColumn("SpustanjePunogNoviPlaniraniDt1", "MESTO SPUSTANJA PUNOG Novi plan. Datum/Vreme", 200);
                    AddTextColumn("SpustanjePunogDtRealizacije1", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);
                }

                else if (drumski == 1)
                {
                    AddTextColumn("PreuzimanjePraznogNoviPlaniraniDt1", "MESTO PREUZIMANJA PRAZNOG Novi plan. Datum/Vreme", 200);
                    AddTextColumn("SpustanjePunogDtRealizacije1", "MESTO SPUSTANJA PUNOG Datum/Vreme realizacije", 200);


                }


            }

            else if (scenarioID == 8)
            {

                AddTextColumn("PreuzimanjePraznogDtRealizacije1", "MESTO PREUZIMANJA PRAZNOG Datum/Vreme realizacije", 200);

                // 
                AddTextColumn("IstovarCeradePlaniraniDt1", "MESTO ISTOVARA CERADE Novi plan. Datum/Vreme", 200);
                AddTextColumn("IstovarCeradeDtRealizacije1", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);
                AddTextColumn("UtovarKontejneraPlaniraniDt1", "MESTO UTOVARA KONTEJNERA Novi plan. Datum/Vreme", 200);
                AddTextColumn("UtovarKontejneraDtRealizacije1", "MESTO UTOVARA KONTEJNERA Datum/Vreme realizacije", 200);
                AddTextColumn("SpustanjePunogPlaniraniDt1", "MESTO SPUŠTANjA PUNOG KONTEJNERA Novi plan. Datum/Vreme", 230);
                AddTextColumn("SpustanjePunogDtRealizacije1", "MESTO SPUŠTANjA PUNOG KONTEJNERA Datum/Vreme realizacije", 230);

                if (drumski == 0)
                {
                    AddTextColumn("PreuzimanjePraznogNoviPlaniraniDt1", "MESTO PREUZIMANJA PRAZNOG Novi plan. Datum/Vreme", 200);
                }
                else if (drumski == 1)
                {
                    AddTextColumn("UtovarCeradeDtRealizacije", "MESTO UTOVARA CERADE Datum/Vreme realizacije", 200);
                    AddTextColumn("IstovarCeradePlaniraniDt1", "MESTO ISTOVARA CERADE Plan. Datum/Vreme", 200);
                }
            }

            else if (scenarioID == 24)
            {
                AddTextColumn("PreuzimanjePraznogNoviPlaniraniDt1", "MESTO PREUZIMANJA PRAZNOG Novi plan. Datum/Vreme", 200);
                AddTextColumn("PreuzimanjePraznogDtRealizacije1", "MESTO PREUZIMANJA PRAZNOG Datum/Vreme realizacije", 200);
                AddTextColumn("IstovarCeradeDtRealizacije1", "MESTO ISTOVARA CERADE Datum/Vreme realizacije", 200);
                AddTextColumn("UtovarKontejneraPlaniraniDt1", "MESTO UTOVARA KONTEJNERA Novi plan. Datum/Vreme", 200);
                AddTextColumn("UtovarKontejneraDtRealizacije1", "MESTO UTOVARA KONTEJNERA Datum/Vreme realizacije", 200);
                AddTextColumn("SpustanjePunogDtRealizacije1", "MESTO SPUŠTANjA PUNOG KONTEJNERA Datum/Vreme realizacije", 230);

                if (drumski == 0)
                {
                    AddTextColumn("IstovarCeradePlaniraniDt1", "MESTO ISTOVARA CERADE Novi plan. Datum/Vreme", 200);
                }
                else if (drumski == 1)
                {
                    AddTextColumn("UtovarCeradeDtRealizacije", "MESTO UTOVARA CERADE Datum/Vreme realizacije", 200);
                }

            }
                //grupa I
                //AddTextColumn("PlaniraniDatumVreme", "Novi plan. Datum/Vreme", 120);
                //AddTextColumn("NoviPlaniraniDatumVreme1", "Novi plan. Datum/Vreme1", 120);
                //AddTextColumn("NoviPlaniraniDatumVreme2", "Novi plan. Datum/Vreme2", 120);
                //AddTextColumn("NoviPlaniraniDatumVreme3", "Novi plan. Datum/Vreme3", 120);

                // Logistika - Vozilo, Vozač...
                AddTextColumn("Vozilo", "Vozilo", 100);
            AddTextColumn("Vozac", "Vozač", 120);
            AddTextColumn("BrojLK", "Broj LK", 100);
            AddTextColumn("BrojTelefona", "Telefon", 100);

            // Numeričke kolone
            AddTextColumn("BTTRobe", "BTT Robe", 80);
            AddTextColumn("NTTORobe", "NTTO Robe", 80);
            AddTextColumn("KoletaFakture", "Koleta", 80);
            AddTextColumn("CBMFaktura", "CBM", 80);
            AddTextColumn("VrednostRobe", "Vrednost", 80);

            
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

            if (drumski == 1)
            {
                dataGridView1.Columns["Vozilo"].Visible = false;
                dataGridView1.Columns["Vozac"].Visible = false;
                dataGridView1.Columns["BrojLK"].Visible = false;
                dataGridView1.Columns["BrojTelefona"].Visible = false;
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
            dataGridView1.AllowUserToAddRows = false; // Ne damo im da dodaju nove, samo edit onih 5
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
            // Proveravamo samo kolone koje su datumi (npr. kolone sa indeksom 3, 4, 5, 6)
            if (e.ColumnIndex >= 3 && e.ColumnIndex <= 6)
            {
                if (!string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    DateTime temp;
                    if (!DateTime.TryParse(e.FormattedValue.ToString(), out temp))
                    {
                        MessageBox.Show("Molimo unesite ispravan datum (npr. 2026-10-25 14:00)");
                        e.Cancel = true; // Zaustavlja korisnika da pređe u drugu ćeliju
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Provera da li je trenutna kolona numerička (npr. BTTRobe)
            string columnName = dataGridView1.CurrentCell.OwningColumn.Name;

            if (columnName == "BTTRobe" || columnName == "NTTORobe" ||  columnName == "KoletaFakture" || columnName == "CBMFaktura" ||  columnName == "VrednostRobe")
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress -= new KeyPressEventHandler(NumericColumn_KeyPress);
                    tb.KeyPress += new KeyPressEventHandler(NumericColumn_KeyPress);
                }
            }
            else
            {
                // Ako NIJE numerička kolona, skidamo handler da bi obična polja radila normalno
                TextBox tb = e.Control as TextBox;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidacijaSaIkonama())
            {
                MessageBox.Show("Molimo popunite označena polja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Prekida se izvršavanje ako nije validno
            }

            InsertIzvoz ins = new InsertIzvoz();
            int vrstaKamiona = 0;

                     
            decimal taraKontejnera = Convert.ToDecimal(txtTaraKontejnera.Value);
            int? brodskaPlomba = null; /*string.IsNullOrWhiteSpace(txtBrodskaPlomba.Text) ? null : txtBrodskaPlomba.Text.Trim();*/
            if (cboBrodar.SelectedValue != null)
            {
                brodskaPlomba = Convert.ToInt32(cboVrstaPlombe.SelectedValue);
            }

            string booking = string.IsNullOrWhiteSpace(txtBoking.Text) ? null : txtBoking.Text.Trim();

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


            string vrstaRobe = null; // doradi
            int? carinskiPostupak = null;// doradi
     

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


            string opisPosla = string.IsNullOrWhiteSpace(txtopisPosla.Text) ? null : txtopisPosla.Text.Trim(); 
            string link = string.IsNullOrWhiteSpace(txtLink.Text) ? null : txtLink.Text.Trim();

            int? kvalitetKontejnera = null;
            if (cboKvalitetKontejnera.SelectedValue != null)
            {
                kvalitetKontejnera = Convert.ToInt32(cboKvalitetKontejnera.SelectedValue);
            }

            if (noviIDs != null && noviIDs.Count > 0)
            {
               
                    ins.UpdateIzvozPorudzbenica(noviIDs, brodar, Convert.ToInt32(txtBoking.Text), vrstaKontejnera, izvoznik, brodskaPlomba, Napomena,
                                                  adr, nacinPakovanja, inspekcijskiTretman, Convert.ToDateTime(dtpCutOffPort.Value), taraKontejnera, pomVaganje, nalogodavacZaUsluge, referencaFakturisanje, nalogodavacZaDrumski, referencaDrumski, opisPosla, link, kvalitetKontejnera);
                  
            }
            else  // update
            {
                try
                {
                    noviIDs = ins.InsIzvozPorudzbenica(brojStavkePorudzbenice, scenarioID, tKorisnik, brojKontejnera, brodar, Convert.ToInt32(txtBoking.Text), vrstaKontejnera, izvoznik, brodskaPlomba, Napomena,
                                                                      adr, nacinPakovanja, inspekcijskiTretman, Convert.ToDateTime(dtpCutOffPort.Value), taraKontejnera, pomVaganje, nalogodavacZaUsluge, referencaFakturisanje, nalogodavacZaDrumski, referencaDrumski, opisPosla, link, kvalitetKontejnera);

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

        private void PostaviVidljivostNacinPakovanja()
        {

            bool isGrupa3 = (scenarioID == 8 || scenarioID == 24 );
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
               // return;
            }
            frmCarinskiPostupak cpo = new frmCarinskiPostupak(noviIDs, drumski, scenarioID);
            cpo.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (noviIDs.Count == 0)
            {
                MessageBox.Show("Morate prvo snimiti grupne podatke za kontejnere!");
                //return;
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

            int id = Convert.ToInt32(row.Cells["ID"].Value); 

            // Izvlačenje ostalih vrednosti iz ćelija
            string brojKontejnera = (row.Cells["BrojKontejnera"].Value == null || row.Cells["BrojKontejnera"].Value == DBNull.Value || string.IsNullOrWhiteSpace(row.Cells["BrojKontejnera"].Value.ToString())) ? null : row.Cells["BrojKontejnera"].Value.ToString().Trim(); 
            string ostalePlombe = (row.Cells["OstalePlombe"].Value == null || row.Cells["OstalePlombe"].Value == DBNull.Value || string.IsNullOrWhiteSpace(row.Cells["OstalePlombe"].Value.ToString()))  ? null  : row.Cells["OstalePlombe"].Value.ToString().Trim();
         
            decimal? bTTRobe = (row.Cells["BTTRobe"].Value == null || row.Cells["BTTRobe"].Value == DBNull.Value) ? (decimal?)null : Convert.ToDecimal(row.Cells["BTTRobe"].Value);
            decimal? nTTORobe = (row.Cells["NTTORobe"].Value == null || row.Cells["NTTORobe"].Value == DBNull.Value) ? (decimal?)null : Convert.ToDecimal(row.Cells["NTTORobe"].Value); 
            int? koletaFakture = (row.Cells["KoletaFakture"].Value == null || row.Cells["KoletaFakture"].Value == DBNull.Value) ? (int?)null : Convert.ToInt32(row.Cells["KoletaFakture"].Value);
            decimal? cBMFaktura = (row.Cells["CBMFaktura"].Value == null || row.Cells["CBMFaktura"].Value == DBNull.Value) ? (decimal?)null : Convert.ToDecimal(row.Cells["CBMFaktura"].Value);
            decimal? vrednostRobe = (row.Cells["VrednostRobe"].Value == null || row.Cells["VrednostRobe"].Value == DBNull.Value) ? (decimal?)null : Convert.ToDecimal(row.Cells["VrednostRobe"].Value); 
            int? napomenaPozicioniranje = null;
            if (row.Cells["NapomenaZaPozicioniranje"].Value != null &&  row.Cells["NapomenaZaPozicioniranje"].Value != DBNull.Value)
            {
                napomenaPozicioniranje = Convert.ToInt32(row.Cells["NapomenaZaPozicioniranje"].Value);
            }

            string vozilo = (row.Cells["Vozilo"].Value == null || row.Cells["Vozilo"].Value == DBNull.Value || string.IsNullOrWhiteSpace(row.Cells["Vozilo"].Value.ToString())) ? null : row.Cells["Vozilo"].Value.ToString().Trim();
            string vozac = (row.Cells["Vozac"].Value == null || row.Cells["Vozac"].Value == DBNull.Value || string.IsNullOrWhiteSpace(row.Cells["Vozac"].Value.ToString())) ? null : row.Cells["Vozac"].Value.ToString().Trim();
            string brojLK = (row.Cells["BrojLK"].Value == null || row.Cells["BrojLK"].Value == DBNull.Value || string.IsNullOrWhiteSpace(row.Cells["BrojLK"].Value.ToString())) ? null : row.Cells["BrojLK"].Value.ToString().Trim();
            string telefon = (row.Cells["BrojTelefona"].Value == null || row.Cells["BrojTelefona"].Value == DBNull.Value || string.IsNullOrWhiteSpace(row.Cells["BrojTelefona"].Value.ToString())) ? null : row.Cells["BrojTelefona"].Value.ToString().Trim();

            try
            {
                ins.UpdateIzvozPorudzbenicaPojedinacna(id, brojKontejnera, ostalePlombe, bTTRobe, nTTORobe, koletaFakture, cBMFaktura, vrednostRobe,  vozilo,  vozac, brojLK, telefon);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri čuvanju reda: " + ex.Message);
                row.DefaultCellStyle.BackColor = Color.Salmon;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            redJePromijenjen = true;
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                // Ovo prisiljava Grid da odmah registruje promjenu (i okine CellValueChanged)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
