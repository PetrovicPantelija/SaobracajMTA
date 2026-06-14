using Saobracaj.Drumski;
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
using System.Windows.Forms.VisualStyles;

namespace Saobracaj.DrumskiApp
{
    public partial class frmFormaZaVozace: Form
    {
        private int ID=0;
        private int korakPotvrde = 1;
        private int adr = 0;
        private int? carinskiPostupakUUnutrasnjemTranzitu = null;
        private int scenarioID = 0;
        private string tipNaloga ;
        public frmFormaZaVozace(int Id)
        {
            ID = Id;
            InitializeComponent();
            ChangeTextBox();
            VratiPodatkeSelect();
            ucitajVrednostiPoKoracima(korakPotvrde);
            PostaviVidljivostADR();
            PostaviVidljivostPanelaPoScenariju();
            btnDodajDokumentaciju.Visible = false;

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
                //this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                //this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
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

        private void PostaviVidljivostPanelaPoScenariju()
        {

            switch (scenarioID)
            {
                // GRUPA I
                case 13: // Scenario I
                case 26:
                    panel1.Visible = true;
                    if (korakPotvrde == 5)
                        btnPotvrda.Enabled = false;
                    break;
            }
        }

        private void PostaviVidljivostADR()
        {
            bool jeADR = (adr == 1);
            bool imaCP = (carinskiPostupakUUnutrasnjemTranzitu != null && carinskiPostupakUUnutrasnjemTranzitu > 0);

            // 1. Postavljanje vidljivosti kontrola
            lblADR.Visible = jeADR;
            txtADR.Visible = jeADR;

            // 2. Postavljanje visine redova (0 ako nije ADR, 30 ako jeste)
            float visinaReda = jeADR ? 30f : 0f;
            tableLayoutPanel2.RowStyles[10].SizeType = SizeType.Absolute;
            tableLayoutPanel2.RowStyles[10].Height = visinaReda;
            tableLayoutPanel2.RowStyles[11].SizeType = SizeType.Absolute;
            tableLayoutPanel2.RowStyles[11].Height = visinaReda;

            // 3. Dinamičko kreiranje teksta za labele
            string adrDeo = jeADR ? " ADR" : "";
            string cpDeo = imaCP ? " SA CP" : " BEZ CP";

  
            // Spajamo u finalni naziv scenarija
            lblScenarioNaziv.Text = $"DIREKTNO PUN{adrDeo}{cpDeo}";

            // Tip naloga sada uvek dinamički uzima vrednost iz promenljive 'tipNaloga' 
            // (bilo da je to "3PI ", "Izvoz " ili nešto treće) i lepi scenario
            lblTipNaloga.Text = $"{tipNaloga}{lblScenarioNaziv.Text}";
        }
        private void VratiPodatkeSelect()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            if (ID == 0) return;
            con.Open();

            SqlCommand cmd = new SqlCommand($@"
                                            SELECT  TOP 1 
                                                    mu.Naziv as MestoPreuzimanjaKontejnera, 
                                                    rn.DtPreuzimanjaPraznogKontejnera,
                                                    (ter.Naziv + ' - ' + ter.Oznaka) as MestoSpustanjaPunog,
                                                    rn.DtSpustanja as PlaniraniDatumSpustanja,
                                                    rn.BrojKontejnera,
                                                    tk.Naziv AS TipKontejnera,
                                                    ISNULL(rn.KorakPotvrdeVozaca,0) AS KorakPotvrdeVozaca,
                                                    ( vadr.UNKod +' - '+ vadr.Klasa + ' - ' + vadr.Naziv  ) as ADR,
                                                    IsNull(rn.ADR,0) AS adrINT,
                                                    rn.CarinskiPostupakUnutrasnji ,
                                                    rn.Scenario,
                                                    rn.NalogID,
                                                     CASE 
                                                        WHEN Uvoz = 1 THEN 'Uvoz'
                                                        WHEN Uvoz = 0 THEN 'Izvoz'
                                                        WHEN Uvoz = 2 THEN '3PU'
                                                        WHEN Uvoz = 3 THEN '3PI'
                                                        ELSE '' 
                                                    END AS TipNaloga

                                            FROM    [dbo].[RadniNalogDrumski] rn 
                                                    LEFT JOIN MestaUtovara mu on mu.ID = rn.MestoPreuzimanjaKontejnera
                                                    LEFT JOIN KontejnerskiTerminali ter on rn.MestoSpustanjaPunog = ter.ID
		                                            LEFT JOIN TipKontenjera tk on rn.TipKontejnera = tk.ID
                                                    LEFT JOIN VrstaRobeADR vadr on rn.ADR = vadr.ID
                                            WHERE   rn.ID  = {ID}", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtMestoPreuzimanjaKontejnera.Text = dr["MestoPreuzimanjaKontejnera"].ToString();
                dtPreuzimanjaPunogKontejnera.Text = dr["DtPreuzimanjaPraznogKontejnera"].ToString();
                txtMestoSpustanjaPunog.Text = dr["MestoSpustanjaPunog"].ToString();
                dtSpustanjaKontejnera.Text = dr["PlaniraniDatumSpustanja"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtVrstaKontejnera.Text = dr["TipKontejnera"].ToString();
                korakPotvrde = Convert.ToInt32(dr["KorakPotvrdeVozaca"].ToString()) ;
                txtADR.Text = dr["ADR"].ToString();
                adr = Convert.ToInt32(dr["adrINT"].ToString());
                carinskiPostupakUUnutrasnjemTranzitu = (dr["CarinskiPostupakUnutrasnji"] == DBNull.Value) ? (int?)null : Convert.ToInt32(dr["CarinskiPostupakUnutrasnji"]);
                scenarioID = Convert.ToInt32(dr["Scenario"].ToString());
                txtBrojNaloga.Text = dr["NalogID"].ToString();
                tipNaloga = (dr["TipNaloga"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   if (korakPotvrde <= 5)
            {
                InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                korakPotvrde = korakPotvrde + 1;
                ucitajVrednostiPoKoracima(korakPotvrde);
                ins.UpdKorakPotvrdeVozaca(ID);
            }
            else 
            {
                btnPotvrda.Enabled = false;
            }
        }

        private void vidljivostDugmetaDodajDokumentaciju(bool vidljivo)
        {
            if (!vidljivo)
            {
                btnDodajDokumentaciju.Visible = false;
                //tableLayoutPanel2.RowStyles[16] = new RowStyle(SizeType.Absolute, 0);
                //tableLayoutPanel2.RowStyles[17] = new RowStyle(SizeType.Absolute, 30);
            }
            else
            {
                btnDodajDokumentaciju.Visible = true;
                //tableLayoutPanel2.RowStyles[16] = new RowStyle(SizeType.Absolute, 30);
                //tableLayoutPanel2.RowStyles[17] = new RowStyle(SizeType.Absolute, 30);
            }
        }
        private void ucitajVrednostiPoKoracima(int korak)
        {
            bool imaCP = (carinskiPostupakUUnutrasnjemTranzitu != null && carinskiPostupakUUnutrasnjemTranzitu > 0);

            if (imaCP)
                koraciCP(korak);
            else
                koraciBezCP(korak);
        }

        private void btnDodajDokumentaciju_Click(object sender, EventArgs e)
        {
           

        }

        private void koraciCP(int korak)
        {

            switch (korak)
            {
                case 1:
                    lblKorak.Text = "KORAK 1: POTVRDA NALOGA";
                    vidljivostDugmetaDodajDokumentaciju(false);
                    break;
                case 2:
                    lblKorak.Text = "KORAK 2: PREUZIMANJE PUNOG KONTEJNERA";
                    vidljivostDugmetaDodajDokumentaciju(false);
                    break;
                case 3:
                    lblKorak.Text = "KORAK 3: KAMION NA CARINARNICI";
                    vidljivostDugmetaDodajDokumentaciju(false);
                    break;
                case 4:
                    lblKorak.Text = "KORAK 4: SPUŠTANJE PUNOG KONTEJNERA";
                    vidljivostDugmetaDodajDokumentaciju(false);
                    break;
                case 5:
                    lblKorak.Text = "KORAK 5: DODAVANJE DOKUMENTACIJE";
                    vidljivostDugmetaDodajDokumentaciju(true);
                    break;
            }
        }

        private void koraciBezCP(int korak)
        {

            switch (korak)
            {
                case 1:
                    lblKorak.Text = "KORAK 1: POTVRDA NALOGA";
                    vidljivostDugmetaDodajDokumentaciju(false);
                    break;
                case 2:
                    lblKorak.Text = "KORAK 2: PREUZIMANJE PUNOG KONTEJNERA";
                    vidljivostDugmetaDodajDokumentaciju(false);
                    break;
                case 3:
                    lblKorak.Text = "KORAK 3: SPUŠTANJE PUNOG KONTEJNERA";
                    vidljivostDugmetaDodajDokumentaciju(false);
                    break;
                case 4:
                    lblKorak.Text = "KORAK 4: DODAVANJE DOKUMENTACIJE";
                    vidljivostDugmetaDodajDokumentaciju(true);
                    break;
              
            }
        }
    }
}
