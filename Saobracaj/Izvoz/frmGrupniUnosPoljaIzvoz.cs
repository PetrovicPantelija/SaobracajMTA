using Microsoft.Ajax.Utilities;
using Microsoft.IdentityModel.Tokens;
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
        public frmGrupniUnosPoljaIzvoz(int BrojStavkePorudzbenice, List<int> ids,  int scenario, int _drumski )
        {
            InitializeComponent();
            ChangeTextBox();
            brojStavkePorudzbenice = BrojStavkePorudzbenice;
            noviIDs = ids;
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
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
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

  
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var bro = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
            var broAD = new SqlDataAdapter(bro, conn);
            var broDS = new DataSet();
            broAD.Fill(broDS);
            cboBrodar.DataSource = broDS.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";
            cboBrodar.SelectedIndex = -1;

            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            cboIzvoznik.DataSource = partDS.Tables[0];
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";
            cboIzvoznik.SelectedIndex = -1;


            var rl3 = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD3 = new SqlDataAdapter(rl3, conn);
            var rlSDS3 = new DataSet();
            rlSAD3.Fill(rlSDS3);
            cboOdlaznaMorskaLuka.DataSource = rlSDS3.Tables[0];
            cboOdlaznaMorskaLuka.DisplayMember = "Naziv";
            cboOdlaznaMorskaLuka.ValueMember = "ID";
            cboOdlaznaMorskaLuka.SelectedIndex = -1;

            var rl2 = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
            var rlSAD2 = new SqlDataAdapter(rl2, conn);
            var rlSDS2 = new DataSet();
            rlSAD2.Fill(rlSDS2);
            cboMestoSpustanjaPunogKontejnera.DataSource = rlSDS2.Tables[0];
            cboMestoSpustanjaPunogKontejnera.DisplayMember = "Naziv";
            cboMestoSpustanjaPunogKontejnera.ValueMember = "ID";
            cboMestoSpustanjaPunogKontejnera.SelectedIndex = -1;

            var carp = "Select ID, Naziv From Carinarnice order by Naziv";
            var carpAD = new SqlDataAdapter(carp, conn);
            var carpDS = new DataSet();
            carpAD.Fill(carpDS);
            cboPolaznaCarinarnica.DataSource = carpDS.Tables[0];
            cboPolaznaCarinarnica.DisplayMember = "Naziv";
            cboPolaznaCarinarnica.ValueMember = "ID";
            cboPolaznaCarinarnica.SelectedIndex = -1;

            //Spediter
            var partner3 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
            var partAD3 = new SqlDataAdapter(partner3, conn);
            var partDS3 = new DataSet();
            partAD3.Fill(partDS3);
            cboSpediterPolazna.DataSource = partDS3.Tables[0];
            cboSpediterPolazna.DisplayMember = "PaNaziv";
            cboSpediterPolazna.ValueMember = "PaSifra";
            cboSpediterPolazna.SelectedIndex = -1;

            var caro = "Select ID, Naziv From Carinarnice order by Naziv";
            var caroAD = new SqlDataAdapter(caro, conn);
            var caroDS = new DataSet();
            caroAD.Fill(caroDS);
            cboOdredisnaCarinarnica.DataSource = caroDS.Tables[0];
            cboOdredisnaCarinarnica.DisplayMember = "Naziv";
            cboOdredisnaCarinarnica.ValueMember = "ID";
            cboOdredisnaCarinarnica.SelectedIndex = -1;

            //Spediter
            var partner4 = "Select PaSifra,PaNaziv From Partnerji where Spediter =1 order by PaNaziv";
            var partAD4= new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpediterOdredisna.DataSource = partDS4.Tables[0];
            cboSpediterOdredisna.DisplayMember = "PaNaziv";
            cboSpediterOdredisna.ValueMember = "PaSifra";
            cboSpediterOdredisna.SelectedIndex = -1;

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

            var nalogodavacZaUsluve = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal2AD = new SqlDataAdapter(nalogodavacZaUsluve, conn);
            var nal2DS = new DataSet();
            nal2AD.Fill(nal2DS);
            cboNalogodavacZaUsluge.DataSource = nal2DS.Tables[0];
            cboNalogodavacZaUsluge.DisplayMember = "PaNaziv";
            cboNalogodavacZaUsluge.ValueMember = "PaSifra";
            cboNalogodavacZaUsluge.SelectedIndex = -1;

            var kvalitetKontejnera = "select ID, LTRIM(LTRIM(Naziv)) AS Naziv from uvKvalitetKontejnera order by ID";
            var kkAD = new SqlDataAdapter(kvalitetKontejnera, conn);
            var kkDS = new DataSet();
            kkAD.Fill(kkDS);

            cboKvalitetKontejnera.DataSource = kkDS.Tables[0];
            cboKvalitetKontejnera.DisplayMember = "Naziv";
            cboKvalitetKontejnera.ValueMember = "ID";
            cboKvalitetKontejnera.Width = 150;
            cboKvalitetKontejnera.SelectedIndex = -1;


            // var bro2 = "Select Sifra, Naziv From VrstePlombi order by Sifra";
            var bro2 = "Select PaSifra, PaNaziv From Partnerji where Brodar = 1  order by PaNaziv";
            var broAD2 = new SqlDataAdapter(bro2, conn);
            var broDS2 = new DataSet();
            broAD2.Fill(broDS2);
            cboVrstaPlombe.DataSource = broDS2.Tables[0];
            cboVrstaPlombe.DisplayMember = "PaNaziv";
            cboVrstaPlombe.ValueMember = "PaSifra";

            //carinski postupak
            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboCarinskiPUnutrasniTransport.DataSource = dirDS2.Tables[0];
            cboCarinskiPUnutrasniTransport.DisplayMember = "Naziv";
            cboCarinskiPUnutrasniTransport.ValueMember = "ID";


            var nhm  = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM order by Naziv";
           
            var nhmSAD = new SqlDataAdapter(nhm, conn);
            var nhmSDS = new DataSet();
            nhmSAD.Fill(nhmSDS);
            cboVrstaRobe.DataSource = nhmSDS.Tables[0];
            cboVrstaRobe.DisplayMember = "Naziv";
            cboVrstaRobe.ValueMember = "ID";


        }

        private void frmGrupniUnosPoljaIzvoz_Load(object sender, EventArgs e)
        {
            FillCombo();
            PodesiPoljaPoScenariju(scenarioID);
            VratiPodatkeSelect();
            dtpCutOffPort.Value = DateTime.Now;
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
           
        }
        private void VratiPodatkeSelect()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT [KvalitetKontejnera] ,[Brodar], [Link], [BukingNumber], [CuttOfPort], [Izvoznik] " +
                                            " FROM [dbo].[ProdajniNalogIzvozStavke] " +
                                            " INNER JOIN ProdajniNalogIzvoz on ProdajniNalogIzvozStavke.IDNAdredjenog = ProdajniNalogIzvoz.ID" +
                                            " inner join TipKontenjera on TipKontenjera.ID = ProdajniNalogIzvozStavke.TipKontejnera" +
                                            " where IDNAdredjenog =" + brojStavkePorudzbenice, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["KvalitetKontejnera"] != DBNull.Value)
                    cboKvalitetKontejnera.SelectedValue = Convert.ToInt32(dr["KvalitetKontejnera"].ToString());

                txtLink.Text = dr["Link"].ToString();

                if(dr["Brodar"]!= DBNull.Value)
                    cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());

                if (dr["Izvoznik"] != DBNull.Value)
                    cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());

                if (dr["CuttOfPort"] != DBNull.Value)
                    dtpCutOffPort.Value = Convert.ToDateTime(dr["CuttOfPort"].ToString());
                if (izabranaCerada == 1)
                    cboVrstaKamiona.SelectedItem = "CERADA";
                else if (izabranaCerada == 0)
                    cboVrstaKamiona.SelectedItem = "PLATFORMA";
           
            }
        }

        private bool ValidacijaSaIkonama()
        {
            bool uspesno = true;
            errorProvider1.Clear(); // Obavezno prvo očisti stare greške

            if (string.IsNullOrWhiteSpace(txtBoking.Text))
            {
                errorProvider1.SetError(txtBoking, "Polje Booking je obavezno!");
                uspesno = false;
            }

            if (cboIzvoznik.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboIzvoznik, "Morate izabrati izvoznika!");
                uspesno = false;
            }

            if (txtTaraKontejnera.Value <= 0)
            {
                errorProvider1.SetError(txtTaraKontejnera, "Tara kontejnera mora biti veća od 0!");
                uspesno = false;
            }
            
            
            if (cboOdlaznaMorskaLuka.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboOdlaznaMorskaLuka, "Morate izabrati neku vrednost!");
                uspesno = false;
            }

            if (cboMestoSpustanjaPunogKontejnera.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboMestoSpustanjaPunogKontejnera, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
            if (cboPolaznaCarinarnica.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboPolaznaCarinarnica, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
            if (cboSpediterPolazna.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboSpediterPolazna, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
            if (cboOdredisnaCarinarnica.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboOdredisnaCarinarnica, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
            if (cboSpediterOdredisna.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboSpediterOdredisna, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
             if (cboNalogodavacZaUsluge.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboNalogodavacZaUsluge, "Morate izabrati neku vrednost!");
                uspesno = false;
            }
            if (string.IsNullOrEmpty(txtRef2.Text))
            {
                errorProvider1.SetError(txtRef2, "Ovo polje je obavezno!");
                uspesno = false;
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
            if (cboVrstaKamiona.Text == "CERADA")
            {
                vrstaKamiona = 1;
            }
          
           
            decimal taraKontejnera = Convert.ToDecimal(txtTaraKontejnera.Value);
            string brodskaPlomba = null; /*string.IsNullOrWhiteSpace(txtBrodskaPlomba.Text) ? null : txtBrodskaPlomba.Text.Trim();*/

            string booking = string.IsNullOrWhiteSpace(txtBoking.Text) ? null : txtBoking.Text.Trim();

            int? brodar = null;
            if (cboBrodar.SelectedValue != null)
            {
                brodar = Convert.ToInt32(cboBrodar.SelectedValue);
            }
            else
            { 
            
            }
                int? izvoznik = null;
            if (cboIzvoznik.SelectedValue != null)
            {
                izvoznik=  Convert.ToInt32(cboIzvoznik.SelectedValue);
            }

            int? odlaznaMorskaLuka = null;
            if (cboOdlaznaMorskaLuka.SelectedValue != null)
            {
                odlaznaMorskaLuka =  Convert.ToInt32(cboOdlaznaMorskaLuka.SelectedValue);
            }

            int? mestoSpustanjaPunogKontejnera = null;
            if (cboMestoSpustanjaPunogKontejnera.SelectedValue != null)
            {
                mestoSpustanjaPunogKontejnera= Convert.ToInt32(cboMestoSpustanjaPunogKontejnera.SelectedValue);
            }

            string dodatneNapomene = string.IsNullOrWhiteSpace(txtDodatneNapomene.Text) ? null : txtDodatneNapomene.Text.Trim();

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

            //int? vrstaRobe = null;
            //if (cboVrstaRobe.SelectedValue != null)
            //{
            //    vrstaRobe = Convert.ToInt32(cboVrstaRobe.SelectedValue);
            //}
            string vrstaRobe = null; // doradi
            int? carinskiPostupak = null;// doradi

            int? polaznaCarinarnica = null;
            if (cboPolaznaCarinarnica.SelectedValue != null)
            {
                polaznaCarinarnica= Convert.ToInt32(cboPolaznaCarinarnica.SelectedValue);
            }

            int? spediterPolaznaC = null;
            if (cboSpediterPolazna.SelectedValue != null)
            {
                spediterPolaznaC = Convert.ToInt32(cboSpediterPolazna.SelectedValue);
            }
            int? kontaktOPolaznaC = null;
            if (cboKontaktOsobaPolazna.SelectedValue != null)
            {
                kontaktOPolaznaC = Convert.ToInt32(cboSpediterPolazna.SelectedValue);
            }

            int? odredisnaCarinarnica = null;
            if (cboOdredisnaCarinarnica.SelectedValue != null)
            {
                odredisnaCarinarnica = Convert.ToInt32(cboOdredisnaCarinarnica.SelectedValue);
            }

            int? spediterOdredisnaC = null;
            if (cboSpediterOdredisna.SelectedValue != null)
            {
                spediterOdredisnaC = Convert.ToInt32(cboSpediterOdredisna.SelectedValue);
            }
            int? kontaktOOdredisnaaC = null;
            if (cboKontaktOsobaOdrdisna.SelectedValue != null)
            {
                kontaktOOdredisnaaC = Convert.ToInt32(cboKontaktOsobaOdrdisna.SelectedValue);
            }

            int? inspekcijskiTretman = null;
            if (cboInspekciskiTretman.SelectedValue != null)
            {
                inspekcijskiTretman = Convert.ToInt32(cboInspekciskiTretman.SelectedValue);
            }
            
            int? nalogodavacZaUsluge = null;
            if (cboNalogodavacZaUsluge.SelectedValue != null)
            {
                nalogodavacZaUsluge = Convert.ToInt32(cboNalogodavacZaUsluge.SelectedValue);
            }
            string referencaFakturisanje = string.IsNullOrEmpty(txtRef2.Text)? null : txtRef2.Text.Trim();

            //foreach (int trenutniID in noviIDs)
            //{
            //    ins.UpdIzvoz(trenutniID, null, null, null,
            //    brodskaPlomba, Convert.ToInt32(txtBoking.Text), brodar, Convert.ToDateTime(dtpCutOffPort.Value),
            //    null, null, null, null,
            //    null, null, null, null,
            //    null, null, null,
            //    null, vrstaKamiona, null, null,
            //    kontaktOPolaznaC, polaznaCarinarnica, spediterPolaznaC,
            //    null, Napomena, null,
            //    null, inspekcijskiTretman, null,
            //    null, null, null, dodatneNapomene, pomVaganje, null,
            //    taraKontejnera, null, izvoznik,
            //    null, null, null,
            //    null, null,
            //    null, null,
            //    null, null,
            //    adr, null, null, null,
            //    null, null,
            //    null, vrstaRobe, null,
            //    null, null, null, null,
            //    null, null,
            //    mestoSpustanjaPunogKontejnera, odlaznaMorskaLuka);
            //}
        }

        private void PodesiPoljaPoScenariju(int scenarioID)
        {
            ResetujVidljivostPanela();

            switch (scenarioID)
            {
                // GRUPA I
                case 13: // Scenario I
                         // Podesi šta treba za čist I
                    izabranaCerada = 0;
                    adr = 0;
                    PostaviVidljivostPanel(1);
                    break;
                case 26: // Scenario I-L
                    izabranaCerada = 0;
                    adr = 1;
                    PostaviVidljivostPanel(1);
                    //  AktivirajLukaPolja();
                    break;
      

                // GRUPA II
                case 7: // Scenario II
                        // Specifičnosti za II
                    izabranaCerada = 0;
                    adr = 0;
                    PostaviVidljivostPanel(2);
                    break;
                case 23: // Scenario I-L
                         //  AktivirajLukaPolja();
                    izabranaCerada = 0;
                    adr = 1;
                    PostaviVidljivostPanel(2);
                    break;

                // GRUPA III
                case 8: // Scenario II
                        // Specifičnosti za II
                    izabranaCerada = 1;
                    adr = 0;
                    PostaviVidljivostPanel(3);
                    break;
                case 24: // Scenario I-L
                         //  AktivirajLukaPolja();
                    izabranaCerada = 1;
                    adr = 1;
                    PostaviVidljivostPanel(3);
                    break;
                // GRUPA IV
                case 9: // Scenario II
                        // Specifičnosti za II
                    izabranaCerada = 1;
                    adr = 0;
                    PostaviVidljivostPanel(3);
                    break;
                case 25: // Scenario I-L
                         //  AktivirajLukaPolja();
                    izabranaCerada = 1;
                    adr = 1;
                    PostaviVidljivostPanel(3);
                    break;
                // ... nastavi za sve ID-jeve ...

                default:
                    // Neki default ako ID nije prepoznat
                    break;
            }
        }

        private void ResetujVidljivostPanela()
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;         
        }

        private void PostaviVidljivostPanel(int grupa)
        {
            ResetujVidljivostPanela();

            switch (grupa)
            {
                case 1:
                    panel1.Visible = true;
                    PodesiUnutrasnjostGrupe1(scenarioID);
                    break;
                case 2:
                    panel2.Visible = true;
                    PodesiUnutrasnjostGrupe2(scenarioID);
                    break;
                case 3:
                    panel3.Visible = true;
                    //PodesiUnutrasnjostGrupe3(scenarioID);
                    break;
            }
        }

        private void PodesiUnutrasnjostGrupe1(int scenarioID)
        {
            // Default: sve vidljivo, pa gasimo specifično
            lblMestoSpustanjaPunogKontejnera.Visible = cboMestoSpustanjaPunogKontejnera.Visible = true;
  
            bool isUkljucenDrumski = ((scenarioID == 13 || scenarioID == 26) && drumski == 1);
            lblMestoPreuzimanjaPunog.Visible = cboMestoPreuzimanjaPunog.Visible = isUkljucenDrumski;
            lblPlaniraniDatum.Visible = dtpPlaniraniDatum.Visible = !isUkljucenDrumski;
        }

        private void PodesiUnutrasnjostGrupe2(int scenarioID)
        {
            // 
            bool isOsnovniIliA = ((scenarioID == 7 || scenarioID == 23) && drumski == 0); // II ili II-A

            lblPlaniraniDatumVreme.Visible = dtpPlaniraniDatumVreme.Visible = isOsnovniIliA;
            lblDatumRealizacije.Visible = dtpDatumRealizacije.Visible = isOsnovniIliA;
            lblPlaniranDatumSpustanja.Visible = dptPlaniranDatumSpustanja.Visible = isOsnovniIliA;
            lblNalogodavacZaDrumski.Visible = cboNalogodavacZaDrumski.Visible = isOsnovniIliA;
            lblRef3.Visible = txtRef3.Visible = isOsnovniIliA;
        }

       
    }
}
