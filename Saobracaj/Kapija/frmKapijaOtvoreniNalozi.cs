using Microsoft.Office.Interop.Excel;
using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Saobracaj.Kapija
{
    public partial class frmKapijaOtvoreniNalozi : Form
    {
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        public frmKapijaOtvoreniNalozi()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = "";
            /*
                 select = "  select Distinct RadniNalogInterni.PlanID, UvozKonacna.BrojKontejnera, Scenario.Naziv, 'Uvozni' as OJ from RadniNalogInterni " +
               "  inner join UvozKonacna on RadniNalogInterni.BrojOsnov = UvozKonacna.ID inner join Scenario on UvozKonacna.Scenario = Scenario.ID " +
               "  where Uradjen not in (1, 2) " +
                " union " +
               "  select Distinct RadniNalogInterni.PlanID, IzvozKonacna.BrojKontejnera, Scenario.Naziv , 'Izvozni' as OJ  from RadniNalogInterni " +
               "  inner join IzvozKonacna on RadniNalogInterni.BrojOsnov = IzvozKonacna.ID " +
               "  inner   join Scenario on IzvozKonacna.Scenario = Scenario.ID " +
               "  where Uradjen not in (1, 2)";
            */
          
                select = " select RadniNalogInterni.ID as KomNalogID, 'Izvoz' as Izvor, 'DOLAZAK' as Smer,  KorisnikIzdao, IZvozKonacna.BrojKontejnera, " +
" TipKontenjera.SkNaziv as VrstaKontejnera, " +
" (Select Top 1 Scenario.Naziv from Scenario where Scenario.ID = IzvozKonacna.Scenario) as SC, " +
"  CASE Drumski  WHEN 0 THEN ' '  WHEN 1 THEN 'L'  END AS Drumski,   " +
" CASE Cirada " +
"  WHEN 1 THEN 'PLATFORMA' " +
" WHEN 2 THEN 'CIRADA' " +
" END AS TipNaloga,  " +
" Vozilo, Vozac, BrojLK, BrojTelefona, PlaniraniDtSpustanjaKontejnera as PlaniraniDatum, PlaniranDtSpustanjaPunog as NoviDatum, GETDATE() as Datum, " +
" BrojStavkePorudzbenice, KapijaUlaz, RadniNalogInterniPotvrda.Scenario  AS ScenarioPotvrde,IZvozKonacna.ID AS KontejnerID " +
" from RadniNalogInterni " +
" inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
" inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
/*" where KapijaUlaz = 0 or KapijaUlaz = 10" +  */
" WHERE (KapijaUlaz = 0 OR KapijaUlaz = 10) AND RadniNalogInterniPotvrda.FazaUsluge = 1 " +
"    union  select RadniNalogInterni.ID as KomNalogID, 'Izvoz' as Izvor, 'ODLAZAK',  KorisnikIzdao, IZvozKonacna.BrojKontejnera, " +
" TipKontenjera.SkNaziv as VrstaKontejnera, " +
" (Select Top 1 Scenario.Naziv from Scenario where Scenario.ID = IzvozKonacna.Scenario) as SC, " +
" CASE Drumski " +
" WHEN 0 THEN ' ' " +
" WHEN 1 THEN 'L' " +
" END AS Drumski,  " +
" CASE Cirada " +
"  WHEN 1 THEN 'PLATFORMA' " +
" WHEN 2 THEN 'CIRADA' " +
" END AS TipNaloga,  " +
 "       Vozilo, Vozac, BrojLK, BrojTelefona,  DtPreuzimanjaPunog, PlaniranDtPreuzimanjaPunog, GETDATE() as Datum, " +
 " BrojStavkePorudzbenice, KapijaUlaz , RadniNalogInterniPotvrda.Scenario  AS ScenarioPotvrde, izvozkonacna.id  AS KontejnerID " +
 " from RadniNalogInterni " +
 "       inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
 "       inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
 "       inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
            /* "        where Kalmar = 1 and KapijaIzlaz = 0"; */
 " WHERE Kalmar = 1 AND KapijaIzlaz = 0 AND RadniNalogInterniPotvrda.FazaUsluge = 1 "+
 " AND ( (RadniNalogInterniPotvrda.Scenario = 1 AND RadniNalogInterniPotvrda.Kalmar = 1) " +
 " OR (RadniNalogInterniPotvrda.Scenario = 2 AND ( ( radninaloginterni.idmanipulacijajed = 69 AND radninaloginternipotvrda.dozvolaizlaz = 1) OR   ( radninaloginterni.idmanipulacijajed = 70 AND radninaloginternipotvrda.kalmar = 1)) )) ";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            this.gridGroupingControl2.Table.Records.DeleteAll();

            gridGroupingControl2.DataSource = ds.Tables[0];
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("KontejnerID");
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Red;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[KapijaUlaz] = '10'";
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd3);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);

        }

        private void button27_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.Enabled = false;
            string kor = Sifarnici.frmLogovanje.user;
            string vozac = "";
            string registarskibroj = "";
            string kontakt = "";
            string kontaktunutarfirme = "";
            int NalogID = 0;
            if (gridGroupingControl2.Table.CurrentRecord != null)
                {
                vozac = gridGroupingControl2.Table.CurrentRecord.GetValue("Vozac").ToString();
                registarskibroj = gridGroupingControl2.Table.CurrentRecord.GetValue("Vozilo").ToString();
                kontakt = gridGroupingControl2.Table.CurrentRecord.GetValue("BrojTelefona").ToString();
                NalogID = Convert.ToInt32(gridGroupingControl2.Table.CurrentRecord.GetValue("KomNalogID").ToString());
            }



            InsertKapija ins = new InsertKapija();
                int noviID = ins.InsKapija(1, vozac, registarskibroj, kontakt, "Izvoz", null, kontakt, kor, NalogID);
                txtID.Text = noviID.ToString();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                MessageBox.Show("Izaberite zapis");
                return;
            }
            InsertKapija ukn = new InsertKapija();
            ukn.UpdeteKapijaNeslaganje(Convert.ToInt32(txtID.Text));
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            if (gridGroupingControl2.Table.CurrentRecord != null)
            {
                txtID.Text = gridGroupingControl2.Table.CurrentRecord.GetValue("KomNalogID").ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Kor = Sifarnici.frmLogovanje.user;
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                int scenario = Convert.ToInt32(selectedRecord.Record.GetValue("ScenarioPotvrde").ToString());
                InsertRadniNalogInterni ir = new InsertRadniNalogInterni();
                ir.PromeniStatusKapijaOdlazak(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()), Kor);
                UpisiLog(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()));

                if (scenario == 2)
                {
                    string kontejnerID = selectedRecord.Record.GetValue("KontejnerID").ToString();
                    //            SELECT TOP 1 p.ID
                    //FROM RadniNalogInterni p
                    //INNER JOIN RadniNalogInterni prazan ON p.BrojOsnov = prazan.BrojOsnov
                    //INNER JOIN RadniNalogInterniPotvrda prazanPotvrda ON prazan.ID = prazanPotvrda.IDNaloga
                    //WHERE prazan.ID = @ID
                    //  AND prazanPotvrda.Scenario = 2
                    //  AND prazan.IDManipulacijaJed = 69
                    //  AND p.IDManipulacijaJed = 70
                    //ORDER BY p.ID DESC
                    //            FormirajRadniNalog(2, Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()), kontejnerID);

                    //        }
                    
                    //int noviNalogID = 0;

                    //string query = @"
                    //            SELECT TOP 1 p.ID
                    //            FROM RadniNalogInterni p
                    //            INNER JOIN RadniNalogInterni prazan ON p.BrojOsnov = prazan.BrojOsnov
                    //            INNER JOIN RadniNalogInterniPotvrda prazanPotvrda ON prazan.ID = prazanPotvrda.IDNaloga
                    //            WHERE prazan.ID = @ID
                    //              AND prazanPotvrda.Scenario = 2
                    //              AND prazan.IDManipulacijaJed = 69
                    //              AND p.IDManipulacijaJed = 70
                    //            ORDER BY p.ID DESC";

                    //using (SqlConnection conn = new SqlConnection(konekcijaString))
                    //{
                    //    using (SqlCommand cmd = new SqlCommand(query, conn))
                    //    {
                    //        cmd.Parameters.AddWithValue("@ID", id);
                    //        conn.Open();

                    //        object result = cmd.ExecuteScalar();
                    //        if (result != null && result != DBNull.Value)
                    //        {
                    //            noviNalogID = Convert.ToInt32(result);
                    //        }
                    //    }
                    //}

                    //// Provera da li je nalog pronađen pre poziva funkcije
                    //if (noviNalogID > 0)
                    //{
                    //    FormirajRadniNalog(2, noviNalogID, kontejnerID);
                    //}
                int id = Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString());
                    int noviNalogID = 0;

                    var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                    SqlConnection con = new SqlConnection(s_connection);

                    con.Open();

                    string query = @"
                                    SELECT TOP 1 p.ID
                                    FROM RadniNalogInterni p
                                    INNER JOIN RadniNalogInterni prazan ON p.BrojOsnov = prazan.BrojOsnov
                                    INNER JOIN RadniNalogInterniPotvrda prazanPotvrda ON prazan.ID = prazanPotvrda.IDNaloga
                                    WHERE prazan.ID = @ID
                                      AND prazanPotvrda.Scenario = 2
                                      AND prazan.IDManipulacijaJed = 69
                                      AND p.IDManipulacijaJed = 70
                                    ORDER BY p.ID DESC";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        noviNalogID = Convert.ToInt32(dr["ID"].ToString());
                    }

                    dr.Close();
                    con.Close();
                    if(noviNalogID > 0 )
                        FormirajRadniNalog(2, noviNalogID, kontejnerID);
                }
            }

        }

        private void FormirajRadniNalog(int ScenarioZaBazu, int NalogID, string Kontejner)
        {
            string Korisnik = Sifarnici.frmLogovanje.user;
            Saobracaj.Uvoz.InsertRadniNalogInterni ins = new Saobracaj.Uvoz.InsertRadniNalogInterni();

          //  string Kotejner = KontejnerID;
            // ako je scenario 2 pored inserta manipulacije praznim kontejnerom treba automatski odraditi insert manipulacije punim kontejnerom gde se fazaUsluge postavlja na 0
            // dakle ne obradjuje se dok se ne zavrsi prethodna usluga
            //if (scenarioZaBazu == 2)
            //{
            //    int rnBrojPunog = vratiNalogIDManipulacijePunogKontejnera(Convert.ToInt32(txtNALOGID.Text));
            //    if (rnBrojPunog > 0)
            //        ins.InsRadniNalogInterniIzvozPotvrda(rnBrojPunog, scenarioZaBazu, 0);
            //}
            //MessageBox.Show("Potvrdjen je Komercijalni nalog!!!");

            //Scenario 1 test - Napravi PRI i RN4





            string Forma = VratiFormu(NalogID);
            int KISUsl = 0;
            int OJ = VratiOJIzdavanja(NalogID);
            if (Forma == "GATE IN VOZ")
            {
                MessageBox.Show("Formirate GATE IN VOZ");
                frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana(NalogID, 0, OJ);
                rd1.Show();
            }
            if (Forma == "GATE OUT KAMION" || Forma == "GATE OUT KAMION TERMINAL" || Forma == "GATE OUT KAMION IZVOZ")
            {

                MessageBox.Show("Formirate GATE OUT KAMION Platforma");
                KISUsl = VratiKonkretanIDUsluge(NalogID);
                Saobracaj.Izvoz.frmOtpremaKontejneraKamionomIzKontejnera okk = new Izvoz.frmOtpremaKontejneraKamionomIzKontejnera(Kontejner, NalogID.ToString(), Korisnik, 0, OJ, "TxtBrojKontejnera");
                okk.Show();




            }

            if (Forma == "GATE IN KAMION" || Forma == "GATE IN KAMION IZVOZ" || Forma == "GATE IN KAMION TERMINAL")
            {
               
                    //ZAdnja nula je Uvoz
                    if (OJ == 4)
                    {
                        //
                        MessageBox.Show("Formirate GATE IN Platforma TERMINAL");
                        //OVDE TREBA DA URADIM TERMINALSKI GATE IN KAMION
                        frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana(NalogID, 1, OJ);
                        rd1.Show();
                        // Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz prijemplat = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(Korisnik, 0, txtNALOGID.Text, 0,4);
                        //  prijemplat.Show();
                    }
                    else if (OJ == 2)
                    {

                        // MessageBox.Show("Formirate Prijem kamionom Platforma Izvoz");
                        DialogResult result = MessageBox.Show(
                                "Formirate GATE IN Platforma Izvoz, da li želite da sistem sam pripremi podatke?", // Message
                                "Potvrdite automatiku", // Title
                                MessageBoxButtons.YesNo, // Buttons
                                MessageBoxIcon.Question // Icon
                                );

                        // Handle the user's response
                        if (result == DialogResult.Yes)
                        {

                            Dokumeta.InsertPrijemKontejneraVoz insV = new Dokumeta.InsertPrijemKontejneraVoz();
                            insV.InsertPrijemKontVoz(Convert.ToDateTime(DateTime.Now), Convert.ToInt32(1), Convert.ToInt32(0), Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(DateTime.Now), Korisnik, "", "", 0, "Scenario II", Convert.ToInt32(0), Convert.ToInt32(0), 0, 0, Convert.ToInt32(0), 2);
                            InsertUvozKonacna insk = new InsertUvozKonacna();
                            int pr = VratiPodatkeMaxPrijemnica();
                            insk.PrenesiPlanUtovaraUPrijemVozIzvoz(Convert.ToInt32(pr), NalogID);

                            RadniNalozi.InsertRN ir = new InsertRN();
                            ir.InsRNPrijemPlatformeKamIzvoz(Convert.ToDateTime(DateTime.Now), Korisnik, Convert.ToDateTime(DateTime.Now), Convert.ToInt32(0), Convert.ToInt32(1), Convert.ToInt32(1), Convert.ToInt32(70), "", "Automatska napomena", Convert.ToInt32(pr), "Kamion", NalogID, 1, 0);

                        }
                        else if (result == DialogResult.No)
                        {
                            // Action for 'No'
                            frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana(NalogID, 1, OJ);
                            rd1.Show();
                        }









                        // Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz prijemplat = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz(Korisnik, 0, txtNALOGID.Text,0, 2);
                        // prijemplat.Show();
                    }
                    else if (OJ == 1)
                    {
                        //Prijem platforme //Uvoz SC1
                        MessageBox.Show("Formirate GATE IN Platforma Uvoz");
                        frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana(NalogID, 1, OJ);
                        rd1.Show();
                        //   Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz prijemplat = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(Korisnik, 0, txtNALOGID.Text, 0, 1);
                        // prijemplat.Show();

                    }

            




            }
        }

        int VratiPodatkeMaxPrijemnica()
        {
            int pr = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from PrijemKontejneraVoz", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pr = Convert.ToInt32(dr["ID"].ToString());
            }

            con.Close();
            return pr;
        }

        int VratiKonkretanIDUsluge(int NalogID)
        {
            int Konkretan = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select KonkretaIDUsluge from RadniNalogInterni where ID = " + NalogID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = Convert.ToInt32(dr["KonkretaIDUsluge"].ToString().TrimEnd());



            }
            con.Close();
            return Konkretan;

        }

        int VratiOJIzdavanja(int NalogID)
        {
            int Konkretan = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select OJIzdavanja from RadniNalogInterni where ID = " + NalogID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = Convert.ToInt32(dr["OJIzdavanja"].ToString().TrimEnd());
            }
            con.Close();
            return Konkretan;

        }


        string VratiFormu(int NalogID)
        {
          
            string formica = "";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Forma from RadniNalogInterni where ID = " + NalogID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                formica = dr["Forma"].ToString().TrimEnd();



            }
            con.Close();
            return formica;
            


        }


        private void UpisiLog(int uslugaidID)
        {

            InsertIzvoz ink = new InsertIzvoz();



            System.Data.DataTable dtPodaci = VratiPodatkeZaLog(uslugaidID);
            // ink.InsertKontejnerLog(Convert.ToInt32(textBox1.Text), poruka1, vreme1, lokacija, tKorisnik);

            if (dtPodaci == null || dtPodaci.Rows.Count == 0)
            {
                return;
            }

            int scenario = Convert.ToInt32(dtPodaci.Rows[0]["Scenario"].ToString());
            switch (scenario)
            {
                // GRUPA I
                case 13:
                case 26:
                    string poruka = "Kamion je na napustio kompleks";

                    DateTime? vreme = null;

                    string lokacija = string.Empty;

                    int kontejnerID = Convert.ToInt32(dtPodaci.Rows[0]["ID"].ToString());

                    // Čitanje lokacije
                    if (dtPodaci.Rows[0]["MestoSpustanjaPunogKontejnera"] != DBNull.Value)
                    {
                        lokacija = dtPodaci.Rows[0]["MestoSpustanjaPunogKontejnera"].ToString();
                    }


                    // Datum dolaska kamiona na kapiju
                    if (dtPodaci.Rows[0]["DatumOdlaska"] != DBNull.Value)
                    {
                        vreme = Convert.ToDateTime(dtPodaci.Rows[0]["DatumOdlaska"]);
                    }



                    ink.InsertKontejnerLog(kontejnerID, poruka, vreme, lokacija, tKorisnik);

                    break;
            }


        }

        private System.Data.DataTable VratiPodatkeZaLog(int? kontejnerID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            System.Data.DataTable dt = new System.Data.DataTable();


            using (SqlConnection con = new SqlConnection(s_connection))
            {

                string query = @"SELECT i.ID, Scenario,
                        LTRIM(RTRIM(mu.Naziv)) AS MestoSpustanjaPunogKontejnera,
                        ka.DatumOdlaska
                        FROM IzvozKonacna i
                             LEFT JOIN MestaUtovara mu ON i.MestoPreuzimanja2 = mu.ID
                             LEFT JOIN RadniNalogInterni rni on rni.BrojOsnov =  i.ID
							 LEFT JOIN Kapija ka on ka.NalogID =  rni.ID
                        WHERE rni.ID = " + kontejnerID;

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

        private void frmKapijaOtvoreniNalozi_Load(object sender, EventArgs e)
        {

        }
    }
}
