using Microsoft.Ajax.Utilities;
using Saobracaj.MainLeget.LegNew;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmRelacija: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int drumski = 0;
        int scenario = 0;
        int vrstaKamiona = 0;
        List<int> noviIDs;
        int panelVisible = 1;
        public frmRelacija(List<int> ids, int _drumski, int _scenarioID)
        {
            InitializeComponent();
            ChangeTextBox();
            drumski = _drumski;
            scenario = _scenarioID;
            noviIDs = ids;
            FillCombo();
        }

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);



            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                //  meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                // meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            // panel1 

            // 1. Učitaj terminale SAMO JEDNOM
            var sqlTerminali = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by Naziv, Oznaka";
            SqlDataAdapter daTerminali = new SqlDataAdapter(sqlTerminali, conn);
            DataTable dtTerminali = new DataTable();
            daTerminali.Fill(dtTerminali);

            // 2. Učitaj mesta utovara SAMO JEDNOM
            var sqlMesta = "Select ID, Naziv from MestaUtovara order by Naziv";
            SqlDataAdapter daMesta = new SqlDataAdapter(sqlMesta, conn);
            DataTable dtMesta = new DataTable();
            daMesta.Fill(dtMesta);

            cboOdlaznaMorskaLuka1.DataSource = dtTerminali.Copy();
            cboOdlaznaMorskaLuka1.DisplayMember = "Naziv";
            cboOdlaznaMorskaLuka1.ValueMember = "ID";
            cboOdlaznaMorskaLuka1.SelectedIndex = -1;

            cboMestoSpustanjaPunogKontejnera.DataSource = dtTerminali.Copy();
            cboMestoSpustanjaPunogKontejnera.DisplayMember = "Naziv";
            cboMestoSpustanjaPunogKontejnera.ValueMember = "ID";
            cboMestoSpustanjaPunogKontejnera.SelectedIndex = -1;

            cboMestoPreuzimanjaPunog.DataSource = dtTerminali.Copy();
            cboMestoPreuzimanjaPunog.DisplayMember = "Naziv";
            cboMestoPreuzimanjaPunog.ValueMember = "ID";

            cboOdlaznaMorskaLuka2.DataSource = dtTerminali.Copy();
            cboOdlaznaMorskaLuka2.DisplayMember = "Naziv";
            cboOdlaznaMorskaLuka2.ValueMember = "ID";
            cboOdlaznaMorskaLuka2.SelectedIndex = -1;

            //isti source kao i cboMestoPreuzimanjaPunog
            cboMestoPreuzimanjaPraznog2.DataSource = dtTerminali.Copy();
            cboMestoPreuzimanjaPraznog2.DisplayMember = "Naziv";
            cboMestoPreuzimanjaPraznog2.ValueMember = "ID";

            cboMestoUtovaraKontejnera2.DataSource = dtMesta.Copy();
            cboMestoUtovaraKontejnera2.DisplayMember = "Naziv";
            cboMestoUtovaraKontejnera2.ValueMember = "ID";

            //isto kao cboMestoSpustanjaPunogKontejnera
            cboMestoSpustanjaPunogKontejnera2.DataSource = dtTerminali.Copy();
            cboMestoSpustanjaPunogKontejnera2.DisplayMember = "Naziv";
            cboMestoSpustanjaPunogKontejnera2.ValueMember = "ID";

            // panel3
            //isti source kao i cboMestoPreuzimanjaPunog
            cboMestoPreuzimanjaPraznog3.DataSource = dtTerminali.Copy();
            cboMestoPreuzimanjaPraznog3.DisplayMember = "Naziv";
            cboMestoPreuzimanjaPraznog3.ValueMember = "ID";

            cboMestoUtovaraCerade3.DataSource = dtMesta.Copy();
            cboMestoUtovaraCerade3.DisplayMember = "Naziv";
            cboMestoUtovaraCerade3.ValueMember = "ID";


            cboMestoIstovaraCerada3.DataSource = dtMesta.Copy();
            cboMestoIstovaraCerada3.DisplayMember = "Naziv";
            cboMestoIstovaraCerada3.ValueMember = "ID";


            cboMestoUtovaraKontejnera3.DataSource = dtMesta.Copy(); 
            cboMestoUtovaraKontejnera3.DisplayMember = "Naziv";
            cboMestoUtovaraKontejnera3.ValueMember = "ID";

            cboMestoSpustanjaPunogKontejnera3.DataSource = dtTerminali.Copy();
            cboMestoSpustanjaPunogKontejnera3.DisplayMember = "Naziv";
            cboMestoSpustanjaPunogKontejnera3.ValueMember = "ID";
            cboMestoSpustanjaPunogKontejnera3.SelectedIndex = -1;


            cboOdlaznaMorskaLuka3.DataSource = dtTerminali.Copy();
            cboOdlaznaMorskaLuka3.DisplayMember = "Naziv";
            cboOdlaznaMorskaLuka3.ValueMember = "ID";
            cboOdlaznaMorskaLuka3.SelectedIndex = -1;

            var tpv = $" select ID, LTRIM(RTRIM(Naziv)) as Naziv from VrstaVozila ";
            var tpvAD = new SqlDataAdapter(tpv, conn);
            var tpvDS = new DataSet();
            tpvAD.Fill(tpvDS);

            System.Data.DataTable dt2 = tpvDS.Tables[0];
         
            DataRow prazanRed2 = dt2.NewRow();
            prazanRed2["ID"] = DBNull.Value;
            prazanRed2["Naziv"] = "";
            dt2.Rows.InsertAt(prazanRed2, 0);


            cboVrstaKamiona.DataSource = dt2;
            cboVrstaKamiona.DisplayMember = "Naziv";
            cboVrstaKamiona.ValueMember = "ID";

            cboVrstaKamiona2.DataSource = dt2.Copy();
            cboVrstaKamiona2.DisplayMember = "Naziv";
            cboVrstaKamiona2.ValueMember = "ID";


            cboVrstaKamiona3.DataSource = dt2.Copy();
            cboVrstaKamiona3.DisplayMember = "Naziv";
            cboVrstaKamiona3.ValueMember = "ID";


        }

        private void PopuniAdresu(ComboBox cboIzvor, ComboBox cboCilj)
        {
            // Provera da li je vrednost validan broj (da izbegnemo grešku pri konverziji)
            if (cboIzvor.SelectedValue == null || !int.TryParse(cboIzvor.SelectedValue.ToString(), out int sifra))
            {
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // Upit sa parametrom @Sifra
                    string sql = "SELECT PaKoZapSt, (Rtrim(PaKOOpomba)) as Naziv FROM partnerjiKontOsebaMU WHERE PaKOSifra = @Sifra ORDER BY PaKOIme";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Sifra", sifra);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                     if (dt.Rows.Count > 0)
                    {
                        cboCilj.DataSource = dt;
                        cboCilj.DisplayMember = "Naziv";
                        cboCilj.ValueMember = "PaKoZapSt";
                    }
                    else
                    {
                        // Ako nema rezultata null
                        cboCilj.DataSource = null;
                        cboCilj.Items.Clear();
                        cboCilj.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        private void PopuniKontaktOsobu(ComboBox cboMesto, ComboBox cboKontakt)
        {
            // Provera selekcije
            if (cboMesto.SelectedValue == null || cboMesto.SelectedValue == DBNull.Value)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
 
                    string sql = @"SELECT PaKoZapSt, 
                           (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv 
                           FROM partnerjiKontOsebaMU 
                           WHERE PaKOSifra = @Sifra 
                           ORDER BY PaKOIme";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Sifra", Convert.ToInt32(cboMesto.SelectedValue));

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            cboKontakt.DataSource = dt;
                            cboKontakt.DisplayMember = "Naziv";
                            cboKontakt.ValueMember = "PaKoZapSt";
                        }
                        else
                        {
                            // Ako nema rezultata null
                            cboKontakt.DataSource = null;
                            cboKontakt.Items.Clear();
                            cboKontakt.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška kod kontakta: " + ex.Message);
            }
        }

        private void PodesiUnutrasnjostGrupe1(int scenarioID)
        {
            // Default: sve vidljivo, pa gasimo specifično
            lblMestoSpustanjaPunogKontejnera.Visible = cboMestoSpustanjaPunogKontejnera.Visible = true;

            bool isUkljucenDrumski = ((scenarioID == 13 || scenarioID == 26) && drumski == 1);
            lblMestoPreuzimanjaPunog.Visible = cboMestoPreuzimanjaPunog.Visible = isUkljucenDrumski;
            lblPlaniraniDatumSpustanja.Visible = dptPlaniranDatumSpustanja.Visible = !isUkljucenDrumski;

            if (scenarioID == 13 && drumski == 0)
            {
                lblOdlaznaMorskaLuka1.Visible = cboOdlaznaMorskaLuka1.Visible = false;

            }
            else 
            {
                lblOdlaznaMorskaLuka1.Visible = cboOdlaznaMorskaLuka1.Visible = true;
            }


        }

        private void PodesiUnutrasnjostGrupe2(int scenarioID)
        {
            // 
            bool isOsnovniIliA = ((scenarioID == 7 || scenarioID == 23) && drumski == 0); // II ili II-A

            lblPlaniraniDatumVreme.Visible = dtpPlaniraniDatumVremePreuzimanja2.Visible = isOsnovniIliA;
            lblDatumRealizacije.Visible = dtpDatumRealizacijeUtovaraKontejnera2.Visible = isOsnovniIliA;
            lblPlaniranDatumSpustanja.Visible = dptPlaniranDatumSpustanja2.Visible = isOsnovniIliA;
           

            if (scenarioID == 7 && drumski == 0)
            {
                lblOdlaznaMorskaLuka2.Visible = cboOdlaznaMorskaLuka2.Visible = false;

            }
            else
            {
                lblOdlaznaMorskaLuka2.Visible = cboOdlaznaMorskaLuka2.Visible = true;

            }

        }

        private void PodesiUnutrasnjostGrupe3(int scenarioID)
        {
            // 
            bool isDrumski = ((scenarioID == 8 || scenarioID == 24) && drumski == 1); // IIIL ili IIILA

            lblMestoUtovaraCerade3.Visible = cboMestoUtovaraCerade3.Visible = isDrumski;
            lblAdresaUtovaraCerade3.Visible = cboAdresaUtovaraCerade3.Visible = isDrumski;
            lblKontaktUtovaraCerade3.Visible = cboKontaktUtovaraCerade3.Visible = isDrumski;
            lblDatumIstovaraCerade3.Visible = dptDatumIstovaraCerade3.Visible = !isDrumski;

            if (scenarioID == 24 && drumski == 1) // ako je IIILA iskljuci sledeca polja
            {
                lblOdlaznaMorskaLuka3.Visible = cboOdlaznaMorskaLuka3.Visible = false;
                lblMestoIstovaraCerada3.Visible = cboMestoIstovaraCerada3.Visible = false;
                lblAdresaIstovaraCerade3.Visible = cboAdresaIstovaraCerade3.Visible = false;
                lblKontaktOIstovarCerade3.Visible = cboKontaktOIstovarCerade3.Visible = false;
                lblPlaniraniDatumUtovaraKontejnera3.Visible = dptPlaniraniDatumUtovaraKontejnera3.Visible = false;
            }
            else
            {
                lblOdlaznaMorskaLuka2.Visible = cboOdlaznaMorskaLuka2.Visible = true;
                lblMestoIstovaraCerada3.Visible = cboMestoIstovaraCerada3.Visible = true;
                lblAdresaIstovaraCerade3.Visible = cboAdresaIstovaraCerade3.Visible = true;
                lblKontaktOIstovarCerade3.Visible = cboKontaktOIstovarCerade3.Visible = true;
                lblPlaniraniDatumUtovaraKontejnera3.Visible = dptPlaniraniDatumUtovaraKontejnera3.Visible = true;
            }

            // ako je IIIL
            if (scenarioID == 8 && drumski == 1)
            {
                lblDatumUtovaraCerade3.Visible = dptDatumUtovaraCerade3.Visible = true;
                lblDodatneNapomenDrumski.Visible = txtDodatneNapomeneDrumski.Visible = true;
            }
            else 
            {
                lblDatumUtovaraCerade3.Visible = dptDatumUtovaraCerade3.Visible = false;
                lblDodatneNapomenDrumski.Visible = txtDodatneNapomeneDrumski.Visible = false;
            }


        }

        private void frmRelacija_Load(object sender, EventArgs e)
        {
            dptPlaniranDatumSpustanja.Value = DateTime.Now;
            dtpPlaniraniDatumVremePreuzimanja2.Value = DateTime.Now;
            dptPlaniraniDatumUtovaraKontejnera2.Value = DateTime.Now;
            dtpDatumRealizacijeUtovaraKontejnera2.Value = DateTime.Now;
            dptPlaniranDatumSpustanja2.Value = DateTime.Now;
            dtpPlaniraniDatumVremePreuzimanja3.Value = DateTime.Now;
            dptDatumUtovaraCerade3.Value = DateTime.Now;
            dptDatumIstovaraCerade3.Value = DateTime.Now;
            dptPlaniraniDatumUtovaraKontejnera3.Value = DateTime.Now;
            dptPlaniranDatumSpustanja3.Value = DateTime.Now;
            PodesiPoljaPoScenariju(scenario);
            VratiPodatkeSelect();
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void PodesiPoljaPoScenariju(int scenarioID)
        {
            ResetujVidljivostPanela();

            switch (scenarioID)
            {
                // GRUPA I
                case 13: // Scenario I
                         // Podesi šta treba za čist I
                    panel1.Visible = true;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    vrstaKamiona = 0;
                    PodesiUnutrasnjostGrupe1(scenario);
                   

                    break;
                case 26: // Scenario I-L
                    panel1.Visible = true;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    vrstaKamiona =0;
                    PodesiUnutrasnjostGrupe1(scenario);
                    break;


                // GRUPA II
                case 7: // Scenario II
                        // Specifičnosti za II
                    panel1.Visible = false;
                    panel2.Visible = true;
                    panel3.Visible = false;
                    vrstaKamiona = 0;
                    PodesiUnutrasnjostGrupe2(scenario);

                    break;
                case 23: // Scenario I-L
                         //  AktivirajLukaPolja();
                    panel1.Visible = false;
                    panel2.Visible = true;
                    panel3.Visible = false;
                    vrstaKamiona = 0;
                    PodesiUnutrasnjostGrupe2(scenario);

                    break;

                // GRUPA III
                case 8: // Scenario II
                        // Specifičnosti za II
                    panel1.Visible = false;
                    panel2.Visible = false;
                    panel3.Visible = true;
                    vrstaKamiona = 1;
                    PodesiUnutrasnjostGrupe3(scenario);
                    break;
                case 24: // Scenario I-L
                         //  AktivirajLukaPolja();
                    panel1.Visible = false;
                    panel2.Visible = false;
                    panel3.Visible = true;
                    vrstaKamiona = 1;
                    PodesiUnutrasnjostGrupe3(scenario);
                    break;
                // GRUPA IV
                case 9: // Scenario II
                        // Specifičnosti za II
                    panel1.Visible = false;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    vrstaKamiona = 1;

                    break;
                case 25: // Scenario I-L
                         //  AktivirajLukaPolja();
                    panel1.Visible = false;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    vrstaKamiona = 1;

                    break;
  

                default:
                    // Neki default ako ID nije prepoznat
                    break;
            }
        }

        private void ResetujVidljivostPanela()
        {

            //panel3.Visible = false;         
        }


        private void cboMestoIstovaraCerada3_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoIstovaraCerada3, cboAdresaIstovaraCerade3);
            PopuniKontaktOsobu(cboMestoIstovaraCerada3, cboKontaktOIstovarCerade3);
        }

        private void cboMestoUtovaraCerade3_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoUtovaraCerade3, cboAdresaUtovaraCerade3);
            PopuniKontaktOsobu(cboMestoUtovaraCerade3, cboKontaktUtovaraCerade3);
        }

        private int? GetVisibleComboValue(Panel pnl, ComboBox cbo)
        {
            // Ako panel uopšte nije vidljiv, ili je vidljiv ali ništa nije izabrano
            if (!pnl.Visible || cbo.SelectedValue == null || cbo.SelectedIndex == -1)
            {
                return null;
            }

            // Pokušaj konverzije u int
            if (int.TryParse(cbo.SelectedValue.ToString(), out int rezultat))
            {
                return rezultat;
            }

            return null;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (!ValidacijaSaIkonama())
            {
                MessageBox.Show("Molimo popunite označena polja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Prekida se izvršavanje ako nije validno
            }
            InsertIzvoz ins = new InsertIzvoz();
            int? odlaznaMorskaLuka = null;
            int? mestoSpustanjaPunogKontejnera = null;
            int? mestoPreuzimanjaPunogPraznog = null;
            int? mestoUtovaraKontejnera = null;
            int? mestoUtovaraCerade = null;
            int? mestoIstovaraCerade = null;
            int? adresaUtovaraKontejnera = null;
            int? kontaktOUtovaraKontejnera = null;
            int? kontaktOUtovaraCerade = null;
            int? kontaktOIstovaraCerade = null;
            DateTime? planiranDatSpustanjaKontejnera = null;
            DateTime? planiranDatPreuzimanjaKontejnera = null;
            DateTime? planiranDatUtovaraKontejnera = null;
            DateTime? realizacijaDatUtovaraKontejnera = null;
            DateTime? planiraniDatumUtovaraCerade = null;
            DateTime? planiraniDatumIstovaraCerade = null;
            string dodatnaNapomenaDrumski = null;
            int? vrstaKamiona = 0;

            if (panel1.Visible)
            {
                odlaznaMorskaLuka = GetVisibleComboValue(panel1, cboOdlaznaMorskaLuka1);
                mestoSpustanjaPunogKontejnera = GetVisibleComboValue(panel1, cboMestoSpustanjaPunogKontejnera);
                mestoPreuzimanjaPunogPraznog = GetVisibleComboValue(panel1, cboMestoPreuzimanjaPunog);
                planiranDatSpustanjaKontejnera = (dptPlaniranDatumSpustanja.Tag?.ToString() == "IZMENJEN") ? dptPlaniranDatumSpustanja.Value : (DateTime?)null;

                if (int.TryParse(cboVrstaKamiona.ToString(), out int rezultat))
                {
                    vrstaKamiona = rezultat;
                }

            }
            else if (panel2.Visible)
            {
                odlaznaMorskaLuka = GetVisibleComboValue(panel2, cboOdlaznaMorskaLuka2);
                mestoSpustanjaPunogKontejnera = GetVisibleComboValue(panel2, cboMestoSpustanjaPunogKontejnera2);
                mestoPreuzimanjaPunogPraznog = GetVisibleComboValue(panel2, cboMestoPreuzimanjaPraznog2);
                mestoUtovaraKontejnera = GetVisibleComboValue(panel2, cboMestoUtovaraKontejnera2);
                if (mestoUtovaraKontejnera != null)
                {
                    adresaUtovaraKontejnera = GetVisibleComboValue(panel2, cboAdresaUtovaraKontejnera2);
                    kontaktOUtovaraKontejnera = GetVisibleComboValue(panel2, cboKontaktUtovaraKontejnera2);
                }
                planiranDatSpustanjaKontejnera = (dptPlaniranDatumSpustanja2.Tag?.ToString() == "IZMENJEN") ? dptPlaniranDatumSpustanja2.Value : (DateTime?)null;
                planiranDatPreuzimanjaKontejnera= (dtpPlaniraniDatumVremePreuzimanja2.Tag?.ToString() == "IZMENJEN") ? dtpPlaniraniDatumVremePreuzimanja2.Value : (DateTime?)null;
                planiranDatUtovaraKontejnera = (dptPlaniraniDatumUtovaraKontejnera2.Tag?.ToString() == "IZMENJEN") ? dptPlaniraniDatumUtovaraKontejnera2.Value : (DateTime?)null;
                realizacijaDatUtovaraKontejnera = (dtpDatumRealizacijeUtovaraKontejnera2.Tag?.ToString() == "IZMENJEN") ? dtpDatumRealizacijeUtovaraKontejnera2.Value : (DateTime?)null;

                if (int.TryParse(cboVrstaKamiona2.ToString(), out int rezultat))
                {
                    vrstaKamiona = rezultat;
                }
            }
            else if (panel3.Visible)
            {
                odlaznaMorskaLuka = GetVisibleComboValue(panel3, cboOdlaznaMorskaLuka3);
                mestoSpustanjaPunogKontejnera = GetVisibleComboValue(panel3, cboMestoSpustanjaPunogKontejnera3);
                mestoPreuzimanjaPunogPraznog = GetVisibleComboValue(panel3, cboMestoPreuzimanjaPraznog3);
                mestoUtovaraKontejnera = GetVisibleComboValue(panel3, cboMestoUtovaraKontejnera3);
                
                if (mestoUtovaraKontejnera != null)
                {
                    adresaUtovaraKontejnera = GetVisibleComboValue(panel3, cboAdresaUtovaraKontejnera3);
                    kontaktOUtovaraKontejnera = GetVisibleComboValue(panel3, cboKontaktUtovaraKontejnera3);
                }
                mestoUtovaraCerade = GetVisibleComboValue(panel3, cboMestoUtovaraCerade3);

                if (mestoUtovaraCerade != null)
                {
                    kontaktOUtovaraCerade = GetVisibleComboValue(panel3, cboKontaktUtovaraCerade3);
                }

                mestoIstovaraCerade = GetVisibleComboValue(panel3, cboMestoIstovaraCerada3);

                if (mestoIstovaraCerade != null)
                {
                    kontaktOIstovaraCerade = GetVisibleComboValue(panel3, cboKontaktOIstovarCerade3);
                }
                planiranDatSpustanjaKontejnera = (dptPlaniranDatumSpustanja3.Tag?.ToString() == "IZMENJEN") ? dptPlaniranDatumSpustanja3.Value : (DateTime?)null;
                planiranDatPreuzimanjaKontejnera = (dtpPlaniraniDatumVremePreuzimanja3.Tag?.ToString() == "IZMENJEN") ? dtpPlaniraniDatumVremePreuzimanja3.Value : (DateTime?)null;
                planiranDatUtovaraKontejnera = (dptPlaniraniDatumUtovaraKontejnera3.Tag?.ToString() == "IZMENJEN") ? dptPlaniraniDatumUtovaraKontejnera3.Value : (DateTime?)null;
                planiraniDatumUtovaraCerade = (dptDatumUtovaraCerade3.Tag?.ToString() == "IZMENJEN") ? dptDatumUtovaraCerade3.Value : (DateTime?)null;
                planiraniDatumIstovaraCerade = (dptDatumIstovaraCerade3.Tag?.ToString() == "IZMENJEN") ? dptDatumIstovaraCerade3.Value : (DateTime?)null;

                dodatnaNapomenaDrumski = string.IsNullOrWhiteSpace(txtDodatneNapomeneDrumski.Text) ? null : txtDodatneNapomeneDrumski.Text.Trim();

                if (int.TryParse(cboVrstaKamiona3.ToString(), out int rezultat))
                {
                    vrstaKamiona= rezultat;
                }
             
            }

         
            ins.UpdateIzvozPorudzbenicaRelacija(noviIDs, odlaznaMorskaLuka, mestoSpustanjaPunogKontejnera, mestoPreuzimanjaPunogPraznog, mestoUtovaraKontejnera,
            adresaUtovaraKontejnera, kontaktOUtovaraKontejnera, planiranDatSpustanjaKontejnera, planiranDatPreuzimanjaKontejnera, planiranDatUtovaraKontejnera,
            realizacijaDatUtovaraKontejnera, mestoIstovaraCerade, kontaktOIstovaraCerade, planiraniDatumIstovaraCerade, mestoUtovaraCerade, kontaktOUtovaraCerade, planiraniDatumUtovaraCerade, dodatnaNapomenaDrumski, vrstaKamiona);

        }

        private void cboMestoUtovaraKontejnera3_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoUtovaraKontejnera3, cboAdresaUtovaraKontejnera3);
            PopuniKontaktOsobu(cboMestoUtovaraKontejnera3, cboKontaktUtovaraKontejnera3);
        }

        private void cboMestoUtovaraKontejnera2_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoUtovaraKontejnera2, cboAdresaUtovaraKontejnera2);
            PopuniKontaktOsobu(cboMestoUtovaraKontejnera2, cboKontaktUtovaraKontejnera2);
        }

        private void dptDatum_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Tag = "IZMENJEN";
        }


        private void VratiPodatkeSelect()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            if (noviIDs.Count == 0) return;

            string idsZaUpit = string.Join(",", noviIDs);



            con.Open();

            SqlCommand cmd = new SqlCommand($@"
                                            SELECT TOP 1 
                                                MestoPreuzimanja3 AS OdlaznaMorskaLuka, 
                                                MestoPreuzimanja2 AS MestoSpustanjaPunogKontejnera, 
                                                PlaniraniDtSpustanjaKontejnera AS PlaniranDatSpustanjaKontejnera, 
                                                PlaniraniDtPreuzimanja, 
                                                MestoPreuzimanja AS MestoPreuzimanjaPunogPraznog, 
                                                PlaniraniDatumUtovara AS PlaniranDatUtovaraKontejnera, 
                                                MesoUtovara AS MestoUtovaraKontejnera, -- Ispravljeno: MestoUtovara AS
                                                KontaktOsoba AS KontaktOUtovaraKontejnera, 
                                                MestoUtovaraCerade AS MestoUtovaraCerade, 
                                                KontaktOsobaUtovaraCerade AS KontaktOUtovaraCerade, 
                                                PlaniraniDtUtovaraCerade AS PlaniraniDatumUtovaraCerade, 
                                                MestoIstovaraCerade AS MestoIstovaraCerade, 
                                                KontaktOsobaIstovaraCerade AS KontaktOIstovaraCerade, 
                                                PlaniraniDtIstovaraCerade AS PlaniraniDatumIstovaraCerade, -- Ispravljeno: PlaniraniDt
                                                RealizacijaDtUtovara AS RealizacijaDatUtovaraKontejnera, 
                                                DodatneNapomeneDrumski AS DodatnaNapomenaDrumski
                                            FROM [dbo].[Izvoz] 
                                            WHERE ID IN ({idsZaUpit})", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {


                if (panel1.Visible == true)
                {
                    //if (dr["OdlaznaMorskaLuka"] != DBNull.Value)
                    //    cboOdlaznaMorskaLuka1.SelectedValue = Convert.ToInt32(dr["OdlaznaMorskaLuka"].ToString());

                    //if (dr["MestoSpustanjaPunogKontejnera"] != DBNull.Value)
                    //    cboMestoSpustanjaPunogKontejnera.SelectedValue = Convert.ToInt32(dr["MestoSpustanjaPunogKontejnera"].ToString());

                    //if (dr["PlaniranDatSpustanjaKontejnera"] != DBNull.Value)
                    //    dptPlaniranDatumSpustanja.Value = Convert.ToDateTime(dr["PlaniranDatSpustanjaKontejnera"].ToString());

                    //if (dr["MestoPreuzimanjaPunogPraznog"] != DBNull.Value)
                    //    cboMestoPreuzimanjaPunog.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanjaPunogPraznog"].ToString());

                    SetVisibleComboValue(panel1, cboOdlaznaMorskaLuka1, dr["OdlaznaMorskaLuka"]);
                    SetVisibleComboValue(panel1, cboMestoSpustanjaPunogKontejnera, dr["MestoSpustanjaPunogKontejnera"]);
                    SetVisibleComboValue(panel1, cboMestoPreuzimanjaPunog, dr["MestoPreuzimanjaPunogPraznog"]);
                    SetVisibleDateValue(panel1, dptPlaniranDatumSpustanja, dr["PlaniranDatSpustanjaKontejnera"]);

                    if (vrstaKamiona == 1)
                    { cboVrstaKamiona.Text = "CERADA"; }
                    else if (vrstaKamiona == 0)
                    { cboVrstaKamiona.Text = "PLATFORMA"; }
                    dptPlaniranDatumSpustanja.Tag = null;
                }
                else if (panel2.Visible == true)
                {
                    SetVisibleComboValue(panel2, cboOdlaznaMorskaLuka2, dr["OdlaznaMorskaLuka"]);
                    SetVisibleComboValue(panel2, cboMestoSpustanjaPunogKontejnera2, dr["MestoSpustanjaPunogKontejnera"]);
                    SetVisibleDateValue(panel2, dptPlaniranDatumSpustanja2, dr["PlaniranDatSpustanjaKontejnera"]);
                    SetVisibleComboValue(panel2, cboMestoPreuzimanjaPraznog2, dr["MestoPreuzimanjaPunogPraznog"]);
                    SetVisibleDateValue(panel2, dtpPlaniraniDatumVremePreuzimanja2, dr["PlaniraniDtPreuzimanja"]);
                    SetVisibleComboValue(panel2, cboMestoUtovaraKontejnera2, dr["MestoUtovaraKontejnera"]);
                    SetVisibleComboValue(panel2, cboMestoUtovaraKontejnera2, dr["MestoUtovaraKontejnera"], () => {
                        PopuniAdresu(cboMestoUtovaraKontejnera2, cboAdresaUtovaraKontejnera2);
                        PopuniKontaktOsobu(cboMestoUtovaraKontejnera2, cboKontaktUtovaraKontejnera2);
                    });
                    SetVisibleComboValue(panel2, cboKontaktUtovaraKontejnera2, dr["KontaktOUtovaraKontejnera"]);
                    SetVisibleDateValue(panel2, dptPlaniraniDatumUtovaraKontejnera2, dr["PlaniranDatUtovaraKontejnera"]);
                    SetVisibleDateValue(panel2, dtpDatumRealizacijeUtovaraKontejnera2, dr["MestoIstovaraCerade"]);
                    SetVisibleDateValue(panel2, dtpDatumRealizacijeUtovaraKontejnera2, dr["MestoIstovaraCerade"]);


                    if (vrstaKamiona == 1)
                    { cboVrstaKamiona2.Text = "CERADA"; }
                    else if (vrstaKamiona == 0)
                    { cboVrstaKamiona2.Text = "PLATFORMA"; }

                    dptPlaniranDatumSpustanja2.Tag = null;
                    dtpPlaniraniDatumVremePreuzimanja2.Tag = null;
                    dptPlaniraniDatumUtovaraKontejnera2.Tag = null;
                    dtpDatumRealizacijeUtovaraKontejnera2.Tag = null;
                }
                else if (panel3.Visible == true)
                {
                    SetVisibleComboValue(panel3, cboOdlaznaMorskaLuka3, dr["OdlaznaMorskaLuka"]);
                    SetVisibleComboValue(panel3, cboMestoPreuzimanjaPraznog3, dr["MestoPreuzimanjaPunogPraznog"]);
                    SetVisibleDateValue(panel3, dtpPlaniraniDatumVremePreuzimanja3, dr["PlaniraniDtPreuzimanja"]);
                    SetVisibleComboValue(panel3, cboMestoIstovaraCerada3, dr["MestoIstovaraCerade"]);
                    SetVisibleComboValue(panel3, cboMestoIstovaraCerada3, dr["MestoIstovaraCerade"], () => {
                        PopuniAdresu(cboMestoIstovaraCerada3, cboAdresaIstovaraCerade3);
                        PopuniKontaktOsobu(cboMestoIstovaraCerada3, cboKontaktOIstovarCerade3);
                    });
                    SetVisibleComboValue(panel3, cboKontaktUtovaraCerade3, dr["KontaktOUtovaraCerade"]);
                    SetVisibleDateValue(panel3, dptDatumUtovaraCerade3, dr["PlaniraniDatumUtovaraCerade"]);
                    SetVisibleComboValue(panel3, cboMestoUtovaraCerade3, dr["KontaktOUtovaraCerade"]);
                    SetVisibleComboValue(panel3, cboMestoUtovaraCerade3, dr["MestoUtovaraCerade"], () => {
                        PopuniAdresu(cboMestoUtovaraCerade3, cboAdresaUtovaraCerade3);
                        PopuniKontaktOsobu(cboMestoUtovaraCerade3, cboKontaktUtovaraCerade3);
                    });
                    SetVisibleComboValue(panel3, cboKontaktOIstovarCerade3, dr["KontaktOIstovaraCerade"]);
                    SetVisibleDateValue(panel3, dptDatumIstovaraCerade3, dr["PlaniraniDatumIstovaraCerade"]); 
                    SetVisibleComboValue(panel3, cboMestoUtovaraKontejnera3, dr["MestoUtovaraKontejnera"],() => {
                        PopuniAdresu(cboMestoUtovaraKontejnera3, cboAdresaUtovaraKontejnera3);
                        PopuniKontaktOsobu(cboMestoUtovaraKontejnera3, cboKontaktUtovaraKontejnera3);
                    });
                    SetVisibleComboValue(panel3, cboKontaktUtovaraKontejnera3, dr["KontaktOUtovaraKontejnera"]);
                    SetVisibleDateValue(panel3, dptPlaniraniDatumUtovaraKontejnera3, dr["PlaniranDatUtovaraKontejnera"]);
                    SetVisibleComboValue(panel3, cboMestoSpustanjaPunogKontejnera3, dr["MestoSpustanjaPunogKontejnera"]);
                    SetVisibleDateValue(panel3, dptPlaniranDatumSpustanja3, dr["PlaniranDatSpustanjaKontejnera"]);
                    txtDodatneNapomeneDrumski.Text = dr["DodatnaNapomenaDrumski"].ToString();

                
                    if (vrstaKamiona == 1)
                    { cboVrstaKamiona3.Text = "CERADA"; }
                    else if (vrstaKamiona == 0)
                    { cboVrstaKamiona3.Text = "PLATFORMA"; }

                    dptPlaniraniDatumUtovaraKontejnera3.Tag = null;
                    dptDatumUtovaraCerade3.Tag = null;
                    dptDatumIstovaraCerade3.Tag = null;
                    dptPlaniranDatumSpustanja3.Tag = null;
                    dtpPlaniraniDatumVremePreuzimanja3.Tag = null;
                }

            }
        }
        private void SetVisibleComboValue(Control parent, ComboBox cbo, object dbValue, Action postUpdateAction = null)
        {
            if (parent.Visible && cbo.Visible && dbValue != DBNull.Value)
            {
                cbo.SelectedValue = Convert.ToInt32(dbValue);
                // Ako smo prosledili neku akciju (npr. popunjavanje adrese)
                postUpdateAction?.Invoke();
            }
        }

        // Za DateTimePicker (datum vrednosti)
        private void SetVisibleDateValue(Control parent, DateTimePicker dtp, object dbValue)
        {
            if (parent.Visible && dtp.Visible && dbValue != DBNull.Value)
            {
                dtp.Value = Convert.ToDateTime(dbValue);
            }
        }


        private bool ValidacijaSaIkonama()
        {
            bool uspesno = true;
            errorProvider1.Clear(); // Obavezno prvo očisti stare greške

            var allowedScenarios = new HashSet<int> { 13, 26, 7, 23, 8, 24 }; // grupa I, II, III


            if (panel1.Visible == true)
            {

                if (cboMestoSpustanjaPunogKontejnera.SelectedIndex < 1)
                {
                    errorProvider1.SetError(cboMestoSpustanjaPunogKontejnera, "Morate izabrati neku vrednost!");
                    uspesno = false;
                }


                if ((scenario == 26 || scenario == 16) )
                {
                    if (drumski == 0)
                    {
                        if (dptPlaniranDatumSpustanja.Tag == null)
                        {
                            errorProvider1.SetError(dptPlaniranDatumSpustanja, "Morate izabrati neku vrednost!");
                            uspesno = false;
                        }
                    }
                    else if (drumski == 1)
                    {
                        if (cboMestoPreuzimanjaPunog.SelectedIndex < 1)
                        {
                            errorProvider1.SetError(cboMestoPreuzimanjaPunog, "Morate izabrati neku vrednost!");
                            uspesno = false;
                        }
                    }

                }
            }
            else if (panel2.Visible == true)
            {
                if (cboMestoSpustanjaPunogKontejnera2.SelectedIndex < 1)
                {
                    errorProvider1.SetError(cboMestoSpustanjaPunogKontejnera2, "Morate izabrati neku vrednost!");
                    uspesno = false;
                }

                if (cboMestoPreuzimanjaPraznog2.SelectedIndex < 1)
                {
                    errorProvider1.SetError(cboMestoPreuzimanjaPraznog2, "Morate izabrati neku vrednost!");
                    uspesno = false;
                }

                if ((scenario == 7 || scenario == 23))
                {
                    if (drumski == 0)
                    {
                        if (dtpPlaniraniDatumVremePreuzimanja2.Tag == null)
                        {
                            errorProvider1.SetError(dtpPlaniraniDatumVremePreuzimanja2, "Morate izabrati neku vrednost!");
                            uspesno = false;
                        }

                        if (dptPlaniranDatumSpustanja2.Tag == null)
                        {
                            errorProvider1.SetError(dptPlaniranDatumSpustanja2, "Morate izabrati neku vrednost!");
                            uspesno = false;
                        }
                    }
                    else if (drumski == 1)
                    {
                        if (cboMestoUtovaraKontejnera2.SelectedIndex < 1)
                        {
                            errorProvider1.SetError(cboMestoUtovaraKontejnera2, "Morate izabrati neku vrednost!");
                            uspesno = false;
                        }

                        if (cboAdresaUtovaraKontejnera2.SelectedIndex < 1)
                        {
                            errorProvider1.SetError(cboAdresaUtovaraKontejnera2, "Morate izabrati neku vrednost!");
                            uspesno = false;
                        }

                        if (cboKontaktUtovaraKontejnera2.SelectedIndex < 1)
                        {
                            errorProvider1.SetError(cboKontaktUtovaraKontejnera2, "Morate izabrati neku vrednost!");
                            uspesno = false;
                        }
                    }
                }

            }

            else if (panel3.Visible == true)
            {
                if (cboMestoSpustanjaPunogKontejnera3.SelectedIndex < 1)
                {
                    errorProvider1.SetError(cboMestoSpustanjaPunogKontejnera3, "Morate izabrati neku vrednost!");
                    uspesno = false;
                }

                if (dptPlaniranDatumSpustanja3.Tag == null)
                {
                    errorProvider1.SetError(dptPlaniranDatumSpustanja3, "Morate izabrati neku vrednost!");
                    uspesno = false;
                }

                if (cboMestoPreuzimanjaPraznog3.SelectedIndex < 1)
                {
                    errorProvider1.SetError(cboMestoPreuzimanjaPraznog3, "Morate izabrati neku vrednost!");
                    uspesno = false;
                }
                if (dtpPlaniraniDatumVremePreuzimanja3.Tag == null)
                {
                    errorProvider1.SetError(dtpPlaniraniDatumVremePreuzimanja3, "Morate izabrati neku vrednost!");
                    uspesno = false;
                }
                if (scenario == 8 && drumski == 0) //III
                {
                    if (cboMestoUtovaraKontejnera3.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboMestoUtovaraKontejnera3, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                    if (cboAdresaUtovaraKontejnera3.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboAdresaUtovaraKontejnera3, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                    if (cboKontaktUtovaraKontejnera3.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboKontaktUtovaraKontejnera3, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                    if (cboMestoIstovaraCerada3.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboMestoIstovaraCerada3, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                    if (cboAdresaIstovaraCerade3.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboAdresaIstovaraCerade3, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                    if (cboKontaktOIstovarCerade3.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboKontaktOIstovarCerade3, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }

                }

                if ((scenario == 8 || scenario == 24) && drumski == 1)
                {
                    if (cboMestoUtovaraCerade3.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboMestoUtovaraCerade3, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                    if (cboAdresaUtovaraCerade3.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboAdresaUtovaraCerade3, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }
                    if (cboKontaktUtovaraCerade3.SelectedIndex < 1)
                    {
                        errorProvider1.SetError(cboKontaktUtovaraCerade3, "Morate izabrati neku vrednost!");
                        uspesno = false;
                    }

                }
            }
             
                return uspesno;
        }



    }
}
