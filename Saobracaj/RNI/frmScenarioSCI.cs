
using Microsoft.Ajax.Utilities;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Saobracaj.RNI
{
    public partial class frmScenarioSCI : Form
    {
        private int ID;
        private string IzdaoOJ;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        private int ScenarioID;
        private int ADR = 0;
        private int Drumski = 0;
        public frmScenarioSCI( int id, string izdaoOJ)
        {
            ID = id;
            IzdaoOJ = izdaoOJ;
            InitializeComponent();
            ChangeTextBox();
            FillCombo();
            txtBrojDokumenta.Text = id.ToString();
           
            
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

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            // panelGrupa1 

            // 1. Učitaj terminale SAMO JEDNOM
            var sqlTerminali = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by Naziv, Oznaka";
            SqlDataAdapter daTerminali = new SqlDataAdapter(sqlTerminali, conn);
            DataTable dtTerminali = new DataTable();
            daTerminali.Fill(dtTerminali);

            // 1. UČITAJ SVE PARTNERE SAMO JEDNOM
            var sqlPartneri = "Select PaSifra, PaNaziv, Brodar, Spediter From Partnerji order by PaNaziv";
            SqlDataAdapter daPart = new SqlDataAdapter(sqlPartneri, conn);
            System.Data.DataTable dtSviPartneri = new System.Data.DataTable();
            daPart.Fill(dtSviPartneri);


            // 2. Učitaj mesta utovara SAMO JEDNOM
            var sqlMesta = "Select ID, Naziv from MestaUtovara order by Naziv";
            SqlDataAdapter daMesta = new SqlDataAdapter(sqlMesta, conn);
            DataTable dtMesta = new DataTable();
            daMesta.Fill(dtMesta);

            cboNalogodavac.DataSource = dtSviPartneri.Copy();
            cboNalogodavac.DisplayMember = "PaNaziv";
            cboNalogodavac.ValueMember = "PaSifra";

            var kvalitetKontejnera = "select ID, LTRIM(LTRIM(Naziv)) AS Naziv from uvKvalitetKontejnera order by ID";
            var kkAD = new SqlDataAdapter(kvalitetKontejnera, conn);
            var kkDS = new DataSet();
            kkAD.Fill(kkDS);

            cboKvalitetKontejnera.DataSource = kkDS.Tables[0];
            cboKvalitetKontejnera.DisplayMember = "Naziv";
            cboKvalitetKontejnera.ValueMember = "ID";
            cboKvalitetKontejnera.Width = 150;
            cboKvalitetKontejnera.SelectedIndex = -1;

            // Partneri (Svi)
            cboIzvoznik.DataSource = dtSviPartneri.Copy();
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";
            cboIzvoznik.SelectedIndex = -1;

            cboIzvoznik4.DataSource = dtSviPartneri.Copy();
            cboIzvoznik4.DisplayMember = "PaNaziv";
            cboIzvoznik4.ValueMember = "PaSifra";
            cboIzvoznik4.SelectedIndex = -1;

            // Brodar (Filtriramo već učitane partnere gde je Brodar = 1)
            DataView dvBrodari = new DataView(dtSviPartneri);
            dvBrodari.RowFilter = "Brodar = 1";

            cboBrodar.DataSource = dvBrodari.ToTable(); // Kreira novu tabelu samo sa brodarima
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";

            cboVrstaPlombe.DataSource = dvBrodari.ToTable();
            cboVrstaPlombe.DisplayMember = "PaNaziv";
            cboVrstaPlombe.ValueMember = "PaSifra";


            cboOdlaznaMorskaLuka.DataSource = dtTerminali.Copy();
            cboOdlaznaMorskaLuka.DisplayMember = "Naziv";
            cboOdlaznaMorskaLuka.ValueMember = "ID";
            cboOdlaznaMorskaLuka.SelectedIndex = -1;

            cboMestoSpustanjaPunogKontejnera.DataSource = dtMesta.Copy();
            cboMestoSpustanjaPunogKontejnera.DisplayMember = "Naziv";
            cboMestoSpustanjaPunogKontejnera.ValueMember = "ID";
            cboMestoSpustanjaPunogKontejnera.SelectedIndex = -1;

            cboMestoSpustanjaPunogKontejnera2.DataSource = dtMesta.Copy();
            cboMestoSpustanjaPunogKontejnera2.DisplayMember = "Naziv";
            cboMestoSpustanjaPunogKontejnera2.ValueMember = "ID";
            cboMestoSpustanjaPunogKontejnera2.SelectedIndex = -1;

            cboMestoSpustanjaPunogKontejnera3.DataSource = dtMesta.Copy();
            cboMestoSpustanjaPunogKontejnera3.DisplayMember = "Naziv";
            cboMestoSpustanjaPunogKontejnera3.ValueMember = "ID";
            cboMestoSpustanjaPunogKontejnera3.SelectedIndex = -1;


            cboMestoPreuzimajnjaPraznogK2.DataSource = dtMesta.Copy();
            cboMestoPreuzimajnjaPraznogK2.DisplayMember = "Naziv";
            cboMestoPreuzimajnjaPraznogK2.ValueMember = "ID";

            //isti source kao i cboMestoPreuzimanjaPunog
            cboMestoPreuzimajnjaPraznogK3.DataSource = dtMesta.Copy();
            cboMestoPreuzimajnjaPraznogK3.DisplayMember = "Naziv";
            cboMestoPreuzimajnjaPraznogK3.ValueMember = "ID";

            var tpv = $" select ID, LTRIM(RTRIM(Naziv)) as Naziv from VrstaVozila ";
            var tpvAD = new SqlDataAdapter(tpv, conn);
            var tpvDS = new DataSet();
            tpvAD.Fill(tpvDS);

            System.Data.DataTable dt2 = tpvDS.Tables[0];

            DataRow prazanRed2 = dt2.NewRow();
            prazanRed2["ID"] = 0;
            prazanRed2["Naziv"] = "";
            dt2.Rows.InsertAt(prazanRed2, 0);


            cboVrstaKamiona.DataSource = dt2;
            cboVrstaKamiona.DisplayMember = "Naziv";
            cboVrstaKamiona.ValueMember = "ID";

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
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpediterOdredisna.DataSource = partDS4.Tables[0];
            cboSpediterOdredisna.DisplayMember = "PaNaziv";
            cboSpediterOdredisna.ValueMember = "PaSifra";
            cboSpediterOdredisna.SelectedIndex = -1;



            //carinski postupak
            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboCarinskiPUnutrasniTransport.DataSource = dirDS2.Tables[0];
            cboCarinskiPUnutrasniTransport.DisplayMember = "Naziv";
            cboCarinskiPUnutrasniTransport.ValueMember = "ID";

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            DataTable dtTipKontenjera = new DataTable();
            tkAD.Fill(dtTipKontenjera);

            cboVrstaKontejnera.DataSource = dtTipKontenjera.Copy();
            cboVrstaKontejnera.DisplayMember = "SkNaziv";
            cboVrstaKontejnera.ValueMember = "ID";

            cboVrstaKontejnera4.DataSource = dtTipKontenjera.Copy();
            cboVrstaKontejnera4.DisplayMember = "SkNaziv";
            cboVrstaKontejnera4.ValueMember = "ID";

            var adr = "Select ID, (  UNKod +' - '+ Klasa + ' - ' + Naziv  ) as Naziv From VrstaRobeADR order by UNKod";
            var adrSAD = new SqlDataAdapter(adr, conn);
            DataTable dtADR = new DataTable();
            adrSAD.Fill(dtADR);

            cboADR.DataSource = dtADR.Copy();
            cboADR.DisplayMember = "Naziv";
            cboADR.ValueMember = "ID";
            cboADR.SelectedIndex = -1;

            cboADR4.DataSource = dtADR.Copy();
            cboADR4.DisplayMember = "Naziv";
            cboADR4.ValueMember = "ID";
            cboADR4.SelectedIndex = -1;

            var dir4 = "Select ID,Naziv from InspekciskiTretman order by Naziv";
            var dirAD4 = new SqlDataAdapter(dir4, conn);
            var dirDS4 = new DataSet();
            dirAD4.Fill(dirDS4);
            cboInspekciskiTretman.DataSource = dirDS4.Tables[0];
            cboInspekciskiTretman.DisplayMember = "Naziv";
            cboInspekciskiTretman.ValueMember = "ID";
            cboInspekciskiTretman.SelectedIndex = -1;

            cboMestoUtovaraKontejnera3.DataSource = dtMesta.Copy();
            cboMestoUtovaraKontejnera3.DisplayMember = "Naziv";
            cboMestoUtovaraKontejnera3.ValueMember = "ID";

            cboMestoIstovaraCerada3.DataSource = dtMesta.Copy();
            cboMestoIstovaraCerada3.DisplayMember = "Naziv";
            cboMestoIstovaraCerada3.ValueMember = "ID";

            cboMestoIstovaraCerada4.DataSource = dtMesta.Copy();
            cboMestoIstovaraCerada4.DisplayMember = "Naziv";
            cboMestoIstovaraCerada4.ValueMember = "ID";
                
            cboMestoUtovaraCerada4.DataSource = dtMesta.Copy();
            cboMestoUtovaraCerada4.DisplayMember = "Naziv";
            cboMestoUtovaraCerada4.ValueMember = "ID";

            var np4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
            var npAD4 = new SqlDataAdapter(np4, conn);
            var npDS4 = new DataSet();
            npAD4.Fill(npDS4);
            cboNacinPakovanja4.DataSource = npDS4.Tables[0];
            cboNacinPakovanja4.DisplayMember = "Naziv";
            cboNacinPakovanja4.ValueMember = "ID";

        }

        private void VratiPodatkeGrupeSelect()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
            //"SELECT i.ID AS BrojDokumenta,i.Scenario, Klijent1,i.OpisPosla,KvalitetKontejnera, i.Brodar, BookingBrodara, CutOffPort," +
            //                                    " BrojKontejnera, VrstaKontejnera, OstalePlombe, BrutoRobe as BTTRobe, NetoRobe as NTTORobe," +
            //                                    " VrstaBrodskePlombe, BrodskaPlomba, i.Izvoznik, " +
            //                                    " NaslovSlanjaStatusa, ADR, NacinPakovanja, Inspekcija, Cirada," +
            //                                    " Vaganje, Tara, i.Scenario ,BrojLK, BrojTelefona, Vozilo, Vozac, " +
            //                                    " Klijent2, Napomena2REf, Klijent3, Napomena3REf, i. Korisnik, ADR, Vaganje, NacinPakovanja," +
            //                                    " MestoPreuzimanja3 AS OdlaznaMorskaLuka, MestoPreuzimanja2 AS MestoSpustanjaPunogKontejnera," +
            //                                    " CarinskiPostupakUnutrasnji, MestoCarinjenja, Spedicija, KontaktSpeditera, OdredisnaCarinarnica, SpediterOdredisna, KontaktSpediteraOdredisna," +
            //                                    " MestoPreuzimanja2 AS MestoSpustanjaPunogKontejnera,PlaniraniDtSpustanjaKontejnera AS PlaniranDatSpustanjaKontejnera, PlaniranDtSpustanjaPunog AS NoviPlaniranDatSpustanjaKontejnera,DtRealizacijeSpustanjaPunog as DtRealizacijeSpustanja," +
            //                                    " PlaniraniDtPreuzimanja as DtPreuzimanjaPraznog, PlaniranDtPreuzimanjaPraznog as NoviDtPreuzimanjaPraznog, i.DtRealizacijePreuzimanjaPraznog as DtRealizacijePreuzimanjaKontejnera, " +  //PlaniraniDtPreuzimanja
            //                                    " MestoPreuzimanja AS MestoPreuzimanjaPunogPraznog, PlaniraniDatumUtovara AS PlaniranDatUtovaraKontejnera,PlaniranDtUtovaraKontejnera AS NoviPlaniranDatUtovaraKontejnera, DtRealizacijeUtovaraKontejnera, MesoUtovara AS MestoUtovaraKontejnera,  " + //Ispravljeno: MestoUtovara AS
            //                                    " KontaktOsoba AS KontaktOUtovaraKontejnera,  MestoUtovaraCerade AS MestoUtovaraCerade, KontaktOsobaUtovaraCerade AS KontaktOUtovaraCerade," +
            //                                    " PlaniraniDtUtovaraCerade AS PlaniraniDatumUtovaraCerade, PlaniranDtUtovaraCerade As  NoviPlaniraniDatumUtovaraCerade, MestoIstovaraCerade AS MestoIstovaraCerade,KontaktOsobaIstovaraCerade AS KontaktOIstovaraCerade," +
            //                                    " PlaniraniDtIstovaraCerade AS PlaniraniDatumIstovaraCerade, PlaniranDtIstovaraCerade AS NoviPlaniraniDatumIstovaraCerade,DtRealizacijeIstovaraCerade, DtRealizacijeUtovaraCerade , Scenario" + //Ispravljeno: PlaniraniDtRealizacijaDtUtovara AS RealizacijaDatUtovaraKontejnera,

            //                                    " FROM Izvoz  i " +
            //                                    " INNER JOIN ProdajniNalogIzvoz pn on i.BrojStavkePorudzbenice = pn.ID  " +
            //                                    " where i.ID =  " + ID +
            //                                    " UNION " +
            SqlCommand cmd;
            if (IzdaoOJ == "Izvoz")
            {
                cmd = new SqlCommand(
                                    " SELECT ik.ID AS BrojDokumenta,ik.Scenario,ik.Korisnik AS NalogKreiraoKorisnik, Klijent1, ik.OpisPosla,  KvalitetKontejnera,ik.Brodar, BookingBrodara, CutOffPort," +
                                    " BrojKontejnera, VrstaKontejnera, OstalePlombe, BrutoRobe as BTTRobe, NetoRobe as NTTORobe," +
                                    " VrstaBrodskePlombe, BrodskaPlomba, ik.Izvoznik, " +
                                    " NaslovSlanjaStatusa, ADR, NacinPakovanja, Inspekcija, Cirada, " +
                                    " Vaganje, Tara, ik.Scenario , BrojLK, BrojTelefona, Vozilo, Vozac," +
                                    " Klijent2, Napomena2REf, Klijent3, Napomena3REf,   " +
                                    " MestoPreuzimanja3 AS OdlaznaMorskaLuka, MestoPreuzimanja2 AS MestoSpustanjaPunogKontejnera," +
                                    " CarinskiPostupakUnutrasnji, MestoCarinjenja, Spedicija, KontaktSpeditera, OdredisnaCarinarnica, SpediterOdredisna, KontaktSpediteraOdredisna ," +
                                    " MestoPreuzimanja2 AS MestoSpustanjaPunogKontejnera,PlaniraniDtSpustanjaKontejnera AS PlaniranDatSpustanjaKontejnera, PlaniranDtSpustanjaPunog AS NoviPlaniranDatSpustanjaKontejnera,DtRealizacijeSpustanjaPunog as DtRealizacijeSpustanja, " +
                                    " PlaniraniDtPreuzimanja as DtPreuzimanjaPraznog, PlaniranDtPreuzimanjaPraznog as NoviDtPreuzimanjaPraznog, ik.DtRealizacijePreuzimanjaPraznog as DtRealizacijePreuzimanjaKontejnera ," +  
                                    " MestoPreuzimanja AS MestoPreuzimanjaPunogPraznog, PlaniraniDatumUtovara AS PlaniranDatUtovaraKontejnera,PlaniranDtUtovaraKontejnera AS NoviPlaniranDatUtovaraKontejnera, DtRealizacijeUtovaraKontejnera,MesoUtovara AS MestoUtovaraKontejnera,  " + 
                                    " KontaktOsoba AS KontaktOUtovaraKontejnera,  MestoUtovaraCerade AS MestoUtovaraCerade, KontaktOsobaUtovaraCerade AS KontaktOUtovaraCerade," +
                                    " PlaniraniDtUtovaraCerade AS PlaniraniDatumUtovaraCerade, PlaniranDtUtovaraCerade As  NoviPlaniraniDatumUtovaraCerade, MestoIstovaraCerade AS MestoIstovaraCerade,KontaktOsobaIstovaraCerade AS KontaktOIstovaraCerade," +
                                    " PlaniraniDtIstovaraCerade AS PlaniraniDatumIstovaraCerade, PlaniranDtIstovaraCerade AS NoviPlaniraniDatumIstovaraCerade, DtRealizacijeIstovaraCerade, DtRealizacijeUtovaraCerade, Scenario, Drumski " + 

                                    " FROM IzvozKonacna  ik " +
                                    " LEFT JOIN ProdajniNalogIzvoz pn on ik.BrojStavkePorudzbenice = pn.ID " +
                                    " where ik.ID =  " + ID, con);
            }
            else
            {
                return;
            }

            // Tek sada kreirate komandu koristeći izabrani string
           

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ScenarioID = Convert.ToInt32(dr["Scenario"].ToString());

                PodesiPoljaPoScenariju(ScenarioID);

                txtKorisnik.Text = tKorisnik;

                txtBrojDokumenta.Text = dr["BrojDokumenta"].ToString();
                txtKorisnikKreirao.Text = dr["NalogKreiraoKorisnik"].ToString();

                if (dr["Klijent1"] != DBNull.Value)
                    cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Klijent1"].ToString());

                txtopisPosla.Text = dr["OpisPosla"].ToString();

                if (dr["KvalitetKontejnera"] != DBNull.Value)
                    cboKvalitetKontejnera.SelectedValue = Convert.ToInt32(dr["KvalitetKontejnera"].ToString());
                
                if (dr["Brodar"] != DBNull.Value)
                    cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());

                if (dr["BookingBrodara"] != DBNull.Value)
                {
                    txtBoking.Text = dr["BookingBrodara"].ToString().Trim();
                }

                if (dr["CutOffPort"] != DBNull.Value)
                {
                    dtpCutOffPort.Value = Convert.ToDateTime(dr["CutOffPort"].ToString());
                }

                if (dr["ADR"] != DBNull.Value)
                    if (panel1.Visible)
                    {
                        cboADR.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                    }
                    else if (panel4.Visible)
                    {
                        cboADR4.SelectedValue = Convert.ToInt32(dr["ADR"].ToString());
                    }
               
                if (dr["Drumski"] != DBNull.Value  && int.TryParse(dr["Drumski"].ToString(), out int parsedDrumski))
                {
                    Drumski = parsedDrumski;
                }
 
                

                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();

                if (dr["VrstaKontejnera"] != DBNull.Value)
                {
                    if (panel1.Visible)
                    {
                        cboVrstaKontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                    }
                    else if (panel4.Visible)
                    {
                        cboVrstaKontejnera4.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                    }
                }
                if (dr["Tara"] != DBNull.Value)
                {
                    txtTaraKontejnera.Value = Convert.ToDecimal(dr["Tara"].ToString());
                }

                txtBrodskaPlombaBroj.Text = dr["BrodskaPlomba"].ToString().Trim();

                if (dr["VrstaBrodskePlombe"] != DBNull.Value)
                    cboVrstaPlombe.SelectedValue = Convert.ToInt32(dr["VrstaBrodskePlombe"].ToString());

                txtOstalePlombe.Text = dr["OstalePlombe"].ToString().Trim();

                if (dr["Izvoznik"] != DBNull.Value)
                    if (panel1.Visible)
                    {
                        cboIzvoznik.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                    }
                    else if (panel4.Visible)
                    {
                        cboIzvoznik4.SelectedValue = Convert.ToInt32(dr["Izvoznik"].ToString());
                    }

                

                if (dr["OdlaznaMorskaLuka"] != DBNull.Value)
                {
                    cboOdlaznaMorskaLuka.SelectedValue = Convert.ToInt32(dr["OdlaznaMorskaLuka"].ToString());
                }
            
                if (dr["MestoSpustanjaPunogKontejnera"] != DBNull.Value)
                {
                    cboMestoSpustanjaPunogKontejnera.SelectedValue = Convert.ToInt32(dr["MestoSpustanjaPunogKontejnera"].ToString());
                }

                if (dr["Cirada"] != DBNull.Value)
                    cboVrstaKamiona.SelectedValue = Convert.ToInt32(dr["Cirada"].ToString());
      
                if (panel1.Visible)
                {
                    txtVozilo.Text = dr["Vozilo"].ToString().Trim();
                    txtVozac.Text = dr["Vozac"].ToString().Trim();
                    txtBrLK.Text = dr["BrojLK"].ToString().Trim();
                    txtTelefon.Text = dr["BrojTelefona"].ToString().Trim();
                }
                else if (panel4.Visible)
                {
                    txtVozilo4.Text = dr["Vozilo"].ToString().Trim();
                    txtVozac4.Text = dr["Vozilo"].ToString().Trim();
                    txtBrLK4.Text = dr["BrojLK"].ToString().Trim();
                    txtTelefon4.Text = dr["BrojTelefona"].ToString().Trim();

                    if (dr["NacinPakovanja"] != DBNull.Value)
                        cboNacinPakovanja4.SelectedValue = Convert.ToInt32(dr["NacinPakovanja"].ToString());
                }

                       

                if (dr["Vaganje"] != DBNull.Value && Convert.ToInt32(dr["Vaganje"]) == 1)
                    if (panel1.Visible)
                    {
                        chkVaganje.Checked = true;
                    }
                    else if (panel4.Visible)
                    {
                        chkVaganje4.Checked = true;
                    }


                if (dr["NaslovSlanjaStatusa"] != DBNull.Value)
                {
                    if (panel1.Visible)
                    {
                        txtNapomena.Text = dr["NaslovSlanjaStatusa"].ToString().Trim();
                    }
                    else if (panel4.Visible)
                    {
                        txtNapomena4.Text = dr["NaslovSlanjaStatusa"].ToString().Trim();
                    }
                    
                }

                

                if (dr["CarinskiPostupakUnutrasnji"] != DBNull.Value)
                    cboCarinskiPUnutrasniTransport.SelectedValue = Convert.ToInt32(dr["CarinskiPostupakUnutrasnji"].ToString());

                if (dr["MestoCarinjenja"] != DBNull.Value)
                    cboPolaznaCarinarnica.SelectedValue = Convert.ToInt32(dr["MestoCarinjenja"].ToString());

                if (dr["Spedicija"] != DBNull.Value)
                    cboSpediterPolazna.SelectedValue = Convert.ToInt32(dr["Spedicija"].ToString());

                if (dr["OdredisnaCarinarnica"] != DBNull.Value)
                    cboOdredisnaCarinarnica.SelectedValue = Convert.ToInt32(dr["OdredisnaCarinarnica"].ToString());

                if (dr["SpediterOdredisna"] != DBNull.Value)
                    cboSpediterOdredisna.SelectedValue = Convert.ToInt32(dr["SpediterOdredisna"].ToString());

                txtKontaktSpediteraOdredisna.Text = dr["KontaktSpediteraOdredisna"].ToString().Trim();

                txtKontaktSpeditera.Text = dr["KontaktSpeditera"].ToString().Trim(); 

                if (dr["Inspekcija"] != DBNull.Value)
                {
                    cboInspekciskiTretman.SelectedValue = Convert.ToInt32(dr["Inspekcija"].ToString());
                }
               

                if (panelGrupa1.Visible == true)
                {
       
                    if (cboMestoSpustanjaPunogKontejnera.Visible && dr["MestoSpustanjaPunogKontejnera"] != DBNull.Value)
                    {
                        cboMestoSpustanjaPunogKontejnera.SelectedValue = Convert.ToInt32(dr["MestoSpustanjaPunogKontejnera"].ToString());
                    }

                    if (dptPlaniranDatumSpustanja.Visible && dr["PlaniranDatSpustanjaKontejnera"] != DBNull.Value)
                    {
                        dptPlaniranDatumSpustanja.Value = Convert.ToDateTime(dr["PlaniranDatSpustanjaKontejnera"]);
                    }

                    if (dptNoviPlaniranDatumSpustanja.Visible && dr["NoviPlaniranDatSpustanjaKontejnera"] != DBNull.Value)
                    {
                        dptNoviPlaniranDatumSpustanja.Value = Convert.ToDateTime(dr["NoviPlaniranDatSpustanjaKontejnera"]);
                    }
                }

                else if (panelGrupa2.Visible == true)
                {                   
                    if (cboMestoSpustanjaPunogKontejnera2.Visible && dr["MestoSpustanjaPunogKontejnera"] != DBNull.Value)
                    {
                        cboMestoSpustanjaPunogKontejnera2.SelectedValue = Convert.ToInt32(dr["MestoSpustanjaPunogKontejnera"].ToString());
                    }                   

                    if (dptPlaniranDatumSpustanja2.Visible && dr["PlaniranDatSpustanjaKontejnera"] != DBNull.Value)
                    {
                        dptPlaniranDatumSpustanja2.Value = Convert.ToDateTime(dr["PlaniranDatSpustanjaKontejnera"]);
                    }

                    if (dptNoviPlaniranDatumSpustanja2.Visible && dr["NoviPlaniranDatSpustanjaKontejnera"] != DBNull.Value)
                    {
                        dptNoviPlaniranDatumSpustanja2.Value = Convert.ToDateTime(dr["NoviPlaniranDatSpustanjaKontejnera"]);
                    }

                    if (cboMestoPreuzimajnjaPraznogK2.Visible && dr["MestoPreuzimanjaPunogPraznog"] != DBNull.Value)
                    {
                        cboMestoPreuzimajnjaPraznogK2.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanjaPunogPraznog"].ToString());
                    }
                    if (dptPlaniraniDtPreuzimanja2.Visible && dr["DtPreuzimanjaPraznog"] != DBNull.Value)
                    {
                        dptPlaniraniDtPreuzimanja2.Value = Convert.ToDateTime(dr["DtPreuzimanjaPraznog"]);
                    }

                    if (dptNoviPlaniraniDtPreuzimanja2.Visible && dr["NoviDtPreuzimanjaPraznog"] != DBNull.Value)
                    {
                        dptNoviPlaniraniDtPreuzimanja2.Value = Convert.ToDateTime(dr["NoviDtPreuzimanjaPraznog"]);
                    }

                }
                else if (panelGrupa3.Visible == true)
                {

                    if (cboMestoPreuzimajnjaPraznogK3.Visible && dr["MestoPreuzimanjaPunogPraznog"] != DBNull.Value)
                    {
                        cboMestoPreuzimajnjaPraznogK3.SelectedValue = Convert.ToInt32(dr["MestoPreuzimanjaPunogPraznog"].ToString());
                    }
                    if (dptPlaniraniDtPreuzimanja3.Visible && dr["DtPreuzimanjaPraznog"] != DBNull.Value)
                    {
                        dptPlaniraniDtPreuzimanja3.Value = Convert.ToDateTime(dr["DtPreuzimanjaPraznog"]);
                    }

                    if (dptNoviPlaniraniDtPreuzimanja3.Visible && dr["NoviDtPreuzimanjaPraznog"] != DBNull.Value)
                    {
                        dptNoviPlaniraniDtPreuzimanja3.Value = Convert.ToDateTime(dr["NoviDtPreuzimanjaPraznog"]);
                    }

                    if (dptRealDtPreuzimanja3.Visible && dr["DtRealizacijePreuzimanjaKontejnera"] != DBNull.Value)
                    {
                        dptRealDtPreuzimanja3.Value = Convert.ToDateTime(dr["DtRealizacijePreuzimanjaKontejnera"]);
                    }
                    if (cboMestoUtovaraKontejnera3.Visible && dr["MestoUtovaraKontejnera"] != DBNull.Value)
                    {
                        cboMestoUtovaraKontejnera3.SelectedValue = Convert.ToInt32(dr["MestoUtovaraKontejnera"].ToString());
                    }

                    if (dptDatumUtovaraKontejnera3.Visible && dr["PlaniranDatUtovaraKontejnera"] != DBNull.Value)
                    {
                        dptDatumUtovaraKontejnera3.Value = Convert.ToDateTime(dr["PlaniranDatUtovaraKontejnera"]);
                    }
                    if (dptNoviDatumUtovaraKontejnera3.Visible && dr["NoviPlaniranDatUtovaraKontejnera"] != DBNull.Value)
                    {
                        dptNoviDatumUtovaraKontejnera3.Value = Convert.ToDateTime(dr["NoviPlaniranDatUtovaraKontejnera"]);
                    }
                    if (dptDatumRealUtovaraKontejnera3.Visible && dr["DtRealizacijeUtovaraKontejnera"] != DBNull.Value)
                    {
                        dptDatumRealUtovaraKontejnera3.Value = Convert.ToDateTime(dr["DtRealizacijeUtovaraKontejnera"]);
                    }

                    PopuniAdresu(cboMestoUtovaraKontejnera3, cboAdresaUtovaraKontejnera3);
                    PopuniKontaktOsobu(cboMestoUtovaraKontejnera3, cboKontaktUtovaraKontejnera3);

                    if (cboMestoIstovaraCerada3.Visible && dr["MestoIstovaraCerade"] != DBNull.Value)
                    {
                        cboMestoIstovaraCerada3.SelectedValue = Convert.ToInt32(dr["MestoIstovaraCerade"].ToString());
                    }
                    if (dptDatumIstovaraCerade3.Visible && dr["PlaniraniDatumIstovaraCerade"] != DBNull.Value)
                    {
                        dptDatumIstovaraCerade3.Value = Convert.ToDateTime(dr["PlaniraniDatumIstovaraCerade"]);
                    }
                    if (dptNoviDatumIstovaraCerade3.Visible && dr["NoviPlaniraniDatumIstovaraCerade"] != DBNull.Value)
                    {
                        dptNoviDatumIstovaraCerade3.Value = Convert.ToDateTime(dr["NoviPlaniraniDatumIstovaraCerade"]);
                    }
                    if (dptDatumRealIstovaraCerade3.Visible && dr["DtRealizacijeIstovaraCerade"] != DBNull.Value)
                    {
                        dptDatumRealIstovaraCerade3.Value = Convert.ToDateTime(dr["DtRealizacijeIstovaraCerade"]);
                    }
                    PopuniAdresu(cboMestoIstovaraCerada3, cboAdresaIstovaraCerade3);
                    PopuniKontaktOsobu(cboMestoIstovaraCerada3, cboKontaktIstovaraCerade3);

                    if (cboMestoSpustanjaPunogKontejnera3.Visible && dr["MestoSpustanjaPunogKontejnera"] != DBNull.Value)
                    {
                        cboMestoSpustanjaPunogKontejnera3.SelectedValue = Convert.ToInt32(dr["MestoSpustanjaPunogKontejnera"].ToString());
                    }

                    if (dptPlaniranDatumSpustanja3.Visible && dr["PlaniranDatSpustanjaKontejnera"] != DBNull.Value)
                    {
                        dptPlaniranDatumSpustanja3.Value = Convert.ToDateTime(dr["PlaniranDatSpustanjaKontejnera"]);
                    }

                    if (dptNoviPlaniranDatumSpustanja3.Visible && dr["NoviPlaniranDatSpustanjaKontejnera"] != DBNull.Value)
                    {
                        dptNoviPlaniranDatumSpustanja3.Value = Convert.ToDateTime(dr["NoviPlaniranDatSpustanjaKontejnera"]);
                    }
                    if (dptDatumRealSpustanja3.Visible && dr["DtRealizacijeSpustanja"] != DBNull.Value)
                    {
                        dptDatumRealSpustanja3.Value = Convert.ToDateTime(dr["DtRealizacijeSpustanja"]);
                    }
                }
                else if (panelGrupa4.Visible == true)
                {

                    if (cboMestoUtovaraCerada4.Visible && dr["MestoUtovaraCerade"] != DBNull.Value)
                    {
                        cboMestoUtovaraCerada4.SelectedValue = Convert.ToInt32(dr["MestoUtovaraCerade"].ToString());
                    }
                    if (dptDatumUtovaraCerade4.Visible && dr["PlaniraniDatumUtovaraCerade"] != DBNull.Value)
                    {
                        dptDatumUtovaraCerade4.Value = Convert.ToDateTime(dr["PlaniraniDatumUtovaraCerade"]);
                    }
                    if (dptNoviDatumUtovaraCerade4.Visible && dr["NoviPlaniraniDatumUtovaraCerade"] != DBNull.Value)
                    {
                        dptNoviDatumUtovaraCerade4.Value = Convert.ToDateTime(dr["NoviPlaniraniDatumUtovaraCerade"]);
                    }
                    if (dptDatumRealUtovaraCerade4.Visible && dr["DtRealizacijeUtovaraCerade"] != DBNull.Value)
                    {
                        dptDatumRealUtovaraCerade4.Value = Convert.ToDateTime(dr["DtRealizacijeUtovaraCerade"]);
                    }
                    PopuniAdresu(cboMestoUtovaraCerada4, cboAdresaUtovaraCerade4);
                    PopuniKontaktOsobu(cboMestoUtovaraCerada4, cboKontaktUtovaraCerade4);
                    if (cboMestoIstovaraCerada4.Visible && dr["MestoIstovaraCerade"] != DBNull.Value)
                    {
                        cboMestoIstovaraCerada4.SelectedValue = Convert.ToInt32(dr["MestoIstovaraCerade"].ToString());
                    }
                    if (dptDatumIstovaraCerade4.Visible && dr["PlaniraniDatumIstovaraCerade"] != DBNull.Value)
                    {
                        dptDatumIstovaraCerade4.Value = Convert.ToDateTime(dr["PlaniraniDatumIstovaraCerade"]);
                    }
                    if (dptNoviDatumIstovaraCerade4.Visible && dr["NoviPlaniraniDatumIstovaraCerade"] != DBNull.Value)
                    {
                        dptNoviDatumIstovaraCerade4.Value = Convert.ToDateTime(dr["NoviPlaniraniDatumIstovaraCerade"]);
                    }
                    if (dptDatumRealIstovaraCerade4.Visible && dr["DtRealizacijeIstovaraCerade"] != DBNull.Value)
                    {
                        dptDatumRealIstovaraCerade4.Value = Convert.ToDateTime(dr["DtRealizacijeIstovaraCerade"]);
                    }
                    PopuniAdresu(cboMestoIstovaraCerada4, cboAdresaIstovaraCerade4);
                    PopuniKontaktOsobu(cboMestoIstovaraCerada4, cboKontaktIstovaraCerade4);

                   
                }
            }
        }

        private void PodesiPoljaPoScenariju(int scenarioID)
        {

            switch (scenarioID)
            {
                // GRUPA I
                case 13: // Scenario I
                         // Podesi šta treba za čist I
                    panelGrupa1.Visible = true;
                    panelGrupa2.Visible = false;
                    panelGrupa3.Visible = false;
                    panelGrupa4.Visible = false;
                    panel4.Visible = false;
                    ADR = 0;
                    PostaviVidljivostPoljaADR();
                    PostaviVidljivostPoljaNapomena();
               //     PodesiUnutrasnjostGrupe1(scenario);


                    break;
                case 26: // Scenario I-L
                    panelGrupa1.Visible = true;
                    panelGrupa2.Visible = false;
                    panelGrupa3.Visible = false;
                    panelGrupa4.Visible = false;
                    panel4.Visible = false;
                    //    PodesiUnutrasnjostGrupe1(scenario);
                    ADR = 1;
                    PostaviVidljivostPoljaADR();
                    PostaviVidljivostPoljaNapomena();
                    break;


                // GRUPA II
                case 7: // Scenario II
                        // Specifičnosti za II
                    panelGrupa1.Visible = false;
                    panelGrupa2.Visible = true;
                    panelGrupa3.Visible = false;
                    panelGrupa4.Visible = false;
                    panel4.Visible = false;
                    ADR = 0;
                    PostaviVidljivostPoljaADR();
                    PostaviVidljivostPoljaNapomena();
                    //   PodesiUnutrasnjostGrupe2(scenario);

                    break;
                case 23: // Scenario I-L
                         //  AktivirajLukaPolja();
                    panelGrupa1.Visible = false;
                    panelGrupa2.Visible = true;
                    panelGrupa3.Visible = false;
                    panelGrupa4.Visible = false;
                    panel4.Visible = false;
                    ADR = 1;
                    PostaviVidljivostPoljaADR();
                    PostaviVidljivostPoljaNapomena();
                    //    PodesiUnutrasnjostGrupe2(scenario);

                    break;

                // GRUPA III
                case 8: // Scenario II
                        // Specifičnosti za II
                    panelGrupa1.Visible = false;
                    panelGrupa2.Visible = false;
                    panelGrupa3.Visible = true;
                    panelGrupa4.Visible = false;
                    panel4.Visible = false;
                    ADR = 0;
                    PostaviVidljivostPoljaADR();
                    PostaviVidljivostPoljaNapomena();
                    //  PodesiUnutrasnjostGrupe3(scenario);
                    break;
                case 24: // Scenario I-L
                         //  AktivirajLukaPolja();
                    panelGrupa1.Visible = false;
                    panelGrupa2.Visible = false;
                    panelGrupa3.Visible = true;
                    panelGrupa4.Visible = false;
                    panel4.Visible = false;
                    ADR = 1;
                    PostaviVidljivostPoljaADR();
                    PostaviVidljivostPoljaNapomena();
                    PodesiUnutrasnjostGrupe3(scenarioID);
                    break;
                // GRUPA IV
                case 9: // Scenario II
                        // Specifičnosti za II
                    panelGrupa1.Visible = false;
                    panelGrupa2.Visible = false;
                    panelGrupa3.Visible = false;
                    panelGrupa4.Visible = true;
                    panel4.Visible = true;
                    panel1.Visible = false;
                    ADR = 0;
                    PostaviVidljivostPoljaADR();
                    PostaviVidljivostPoljaCutOffPort();
                    PostaviVidljivostPoljaNapomena();
                    //panel6.Visible = true;


                    break;
                case 25: // Scenario I-L‚‚‚‚
                         // 
                    panelGrupa1.Visible = false;
                    panelGrupa2.Visible = false;
                    panelGrupa3.Visible = false;
                    panelGrupa4.Visible = true;
                    panel4.Visible = true;
                    panel1.Visible = false;
                    ADR = 1;
                    PostaviVidljivostPoljaADR();
                    PostaviVidljivostPoljaCutOffPort();
                    PostaviVidljivostPoljaNapomena();
                    //  PodesiUnutrasnjostGrupe4(scenario);

                    break;


                default:
                    // Neki default ako ID nije prepoznat
                    break;
            }
        }

        private void PostaviVidljivostPoljaADR()
        {
            bool isADR = (ADR == 1); // 
            {
                if (ScenarioID == 9 && ScenarioID == 25)
                {
                    cboADR4.Visible = isADR;
                    lblADR4.Visible = isADR;
                }
                else
                {
                    cboADR.Visible = isADR;
                    lblAdr.Visible = isADR;
                }
               
            }
            if (isADR == false)
            {
                if (ScenarioID == 9 && ScenarioID == 25)
                    cboADR4.SelectedValue = -1;
                else
                    cboADR.SelectedValue = -1;
            }
        }

        private void PostaviVidljivostPoljaNapomena()
        {
            bool napomenaVisible = (ScenarioID == 7 && ScenarioID == 23); // 
            {
                dataGridView4.Visible = napomenaVisible;
                lblNapomenaPoz.Visible = napomenaVisible;
            }
           
        }

        private void PostaviVidljivostPoljaCutOffPort()
        {
            bool cutOffPortVisible = (ScenarioID == 9 && ScenarioID == 25); // 
            {
                dtpCutOffPort.Visible = cutOffPortVisible;
                lblCutOffPort.Visible = cutOffPortVisible;
            }

        }
       
        private void PodesiUnutrasnjostGrupe3(int scenario)
        {
            if (scenario == 24 || scenario == 8)
            {
                lblCutOffPort.Visible = dtpCutOffPort.Visible = false;
                lblBoking.Visible = txtBoking.Visible = false;
                lblBrodskaPlombaBroj.Visible = txtBrodskaPlombaBroj.Visible = false;
                lblOstalePlombe.Visible = txtOstalePlombe.Visible = false;
                lblVrstaPlombe.Visible = cboVrstaPlombe.Visible = false;
            }
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
                        cboCilj.SelectedIndex = 0;
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
                            cboKontakt.SelectedIndex = 0;
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


        private void frmScenarioSCI_Load(object sender, EventArgs e)
        {
            VratiPodatkeGrupeSelect();
            System.Data.DataTable dtIzBaze = VratiPodatkeIzBazeNHM();
            System.Data.DataTable dtIzBazeN = VratiPodatkeIzBazeNapomene();
            PostaviVidljivostGrupe4Drumski();
            OsveziGridNHM(dtIzBaze);
            OsveziGridNapomena(dtIzBazeN);
            this.Text = $"Po scenariju {ScenarioID}";
        }
        
       private void PostaviVidljivostGrupe4Drumski()
        {
            bool isVisible = (Drumski == 1); // 
            {
                if (ScenarioID == 9 || ScenarioID == 25)
                {
                    lblAdresaUtovaraCerade4.Visible = cboAdresaUtovaraCerade4.Visible = isVisible;
                    lblKontaktOUtovarCerade4.Visible = cboKontaktUtovaraCerade4.Visible = isVisible;
                    lblDatumRealUtovaraCerade4.Visible = dptDatumRealUtovaraCerade4.Visible = isVisible;
                }
            }
            
        }

        private void OsveziGridNHM(System.Data.DataTable dt)
        {
          

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
            dataGridView2.Enabled = false;
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

        private System.Data.DataTable VratiPodatkeIzBazeNHM()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            System.Data.DataTable dt = new System.Data.DataTable();



            using (SqlConnection con = new SqlConnection(s_connection))
            {

                string query = $@"SELECT DISTINCT  nm.ID as IDNHM, (RTRIM(nm.Naziv) + ' - ' + RTRIM(nm.Broj)) as Naziv
                          FROM IzvozKonacnaNHM inhm 
                          INNER JOIN NHM nm on inhm.IDNHM = nm.ID
                          WHERE inhm.IDNadredjena IN ({ID})";

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



            using (SqlConnection con = new SqlConnection(s_connection))
            {

                string query = $@"SELECT DISTINCT  np.ID as IDNapomene, (RTRIM(np.Naziv) ) as Naziv
                          FROM IzvozNapomenePozicioniranja inap
                          INNER JOIN NapomenaZaPozicioniranje np on inap.IDNapomene = np.ID
                          WHERE inap.IDNadredjena IN ({ID})";

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

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
          
            if (!dataGridView2.Enabled)
            {
                e.CellStyle.BackColor = SystemColors.Control;
                e.CellStyle.ForeColor = SystemColors.GrayText;
                e.CellStyle.SelectionBackColor = SystemColors.Control;
                e.CellStyle.SelectionForeColor = SystemColors.GrayText;
            }
        }

        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!dataGridView4.Enabled)
            {
                e.CellStyle.BackColor = SystemColors.Control;
                e.CellStyle.ForeColor = SystemColors.GrayText;
                e.CellStyle.SelectionBackColor = SystemColors.Control;
                e.CellStyle.SelectionForeColor = SystemColors.GrayText;
            }
        }
    }
}
