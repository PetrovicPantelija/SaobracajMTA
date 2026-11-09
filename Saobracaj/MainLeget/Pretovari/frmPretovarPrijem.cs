using Saobracaj.Izvoz;
using Saobracaj.MainLeget.PrijemIOtpremaKamiona;
using Saobracaj.RadniNalozi;
using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Grid.Grouping;
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

namespace Saobracaj.MainLeget.Pretovari
{
    public partial class frmPretovarPrijem: Form
    {
        string Kor = Sifarnici.frmLogovanje.user;
        public frmPretovarPrijem()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            VratiPodatke();
        }
        private void VratiPodatke()
        {
            var select = "";


            select = "     SELECT RadniNalogInterni.[ID] as KomNalID, RadniNalogInterni.BrojOsnov as KontID," +
            " DatumPrijema as DatumPrijema, " +
            "  CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status,  " +
            "  REgBrKamiona, ImeVozaca, " +
            "  n1.VremeDolaska as VremeDol,  " +
            "  n1.[Datum] ,n1.[Korisnik] ,  RadniNalogInterniPotvrda.KapijaUlaz, " +
             "  (SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
             "  FROM PrijemKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
            "  FOR XML PATH('')), 1, 1, ''  ) As Skupljen)  " +
             " as Kontejner , RadniNalogInterni.BrojOsnov AS KontejrID, OrganizacioneJedinice.Naziv as Modul,  " +
             " CASE WHEN n1.Poreklo = 0 THEN 'PLATFORMA' ELSE 'CIRADA' END as POREKLO, RadniNalogInterniPotvrda.Scenario, " +
             " RadniNalogInterni.IDManipulacijaJed, RadniNalogInterniPotvrda.Vaganje,IzvozKonacna.Vaganje as PotrebnoVaganje, RadniNalogInterniPotvrda.FazaUsluge  " +
            "  FROM[dbo].[PrijemKontejneraVoz] as n1 " +
            "  inner join organizacioneJedinice on OrganizacioneJedinice.ID = n1.Modul " +
            "  inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.IDNadredjenog = n1.ID " +
            "  inner join RadniNalogInterni on RadniNalogInterni.ID = PrijemKontejneraVozStavke.NajavaID " +
            "  inner join IzvozKonacna on RadniNalogInterni.BrojOsnov = IzvozKonacna.ID " +
            "  inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
            "  where Vozom = 0  and RadniNalogInterniPotvrda.Kamion = 0   AND DozvolaIzlaz = 0 AND n1.Poreklo = 1 " +
                  "  and (( RadniNalogInterniPotvrda.FazaUsluge > 0 AND IDManipulacijaJed = 69) OR (RadniNalogInterniPotvrda.FazaUsluge = 1 AND IDManipulacijaJed = 81) " +
                  "  OR (IDManipulacijaJed in (81) AND  (  " +  
                  "   EXISTS ( " +
                  "     SELECT 1  " +
                  "  FROM radninaloginterni sub " +
                  "  INNER JOIN RadniNalogInterniPotvrda subP on sub.ID = subP.IDNaloga " +
                  "  WHERE sub.BrojOsnov = radninaloginterni.BrojOsnov " +
                  " AND sub.IDManipulacijaJED = 102 " +
                  "  AND subP.FazaUsluge = 0 " +
                " )))" +
                ")" +
            "  order by n1.ID desc";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            this.gridGroupingControl2.Table.Records.DeleteAll();

            gridGroupingControl2.DataSource = ds.Tables[0];
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("Scenario");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("IDManipulacijaJed");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("KontejrID");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("Vaganje");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("PotrebnoVaganje");
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("FazaUsluge");
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            // 1. Deo: scenario 3 i usluga praznog
            GridConditionalFormatDescriptor gcfdPretovar = new GridConditionalFormatDescriptor();
            gcfdPretovar.Appearance.AnyRecordFieldCell.BackColor = Color.Yellow;
            gcfdPretovar.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            gcfdPretovar.Expression = "[Scenario] = 3 AND [KapijaUlaz] = 2 AND [IDManipulacijaJed] = 69";

            GridConditionalFormatDescriptor gcfdPretovar2 = new GridConditionalFormatDescriptor();
            gcfdPretovar2.Appearance.AnyRecordFieldCell.BackColor = Color.Yellow;
            gcfdPretovar2.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            gcfdPretovar2.Expression = "[Scenario] = 3 AND [KapijaUlaz] = 0 AND [IDManipulacijaJed] = 81";

            // Dodavanje u grid
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfdPretovar);
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfdPretovar2);
            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {


                // ČISTO REŠENJE: Sinhronizacija i čišćenje selekcije
                if (gridGroupingControl2.Table.CurrentRecord != null)
                {
                    // 1. Prvo čistimo sve stare redove da se ne bi gomilali u memoriji!
                    gridGroupingControl2.Table.SelectedRecords.Clear();

                    // 2. Dodajemo isključivo i samo ovaj trenutno kliknuti red
                    gridGroupingControl2.Table.SelectedRecords.Add(gridGroupingControl2.Table.CurrentRecord);
                }

            }
            catch (Exception ex) { }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            // kreira medjuskladišni rn i završava uslugu praznog kontejnera i aktivira uslugu pretovara
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                int idUsluge = Convert.ToInt32(selectedRecord.Record.GetValue("IDManipulacijaJed")?.ToString());
                if (idUsluge != 69)
                {
                    MessageBox.Show("Radni nalog kalmaru je moguće napraviti samo za uslugu praznim kontejnerom.");
                    return;
                }
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                int usluga = Convert.ToInt32(selectedRecord.Record.GetValue("KomNalID").ToString());   //Convert.ToInt16(txtID.Text) 
                string kontejner = selectedRecord.Record.GetValue("Kontejner").ToString(); //txtKontejner.Text
                int kontejnerID = Convert.ToInt32(selectedRecord.Record.GetValue("KontID").ToString());
                string napomena = ""; // txtNapomena.Text

               if(PostojiRadniNalogZaUslugu(usluga))
                {
                    MessageBox.Show("Već je napravljen radni nalog za ovu uslugu.", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RN12MedjuskladisniKontejnera rn12 = new RN12MedjuskladisniKontejnera(usluga, kontejner, napomena);
                rn12.ShowDialog();

                //proveri da li je zaista i napravljen radni nalog i ako jeste zavrsi uslugu i aktiviraj uslugu pretovara
                if (PostojiRadniNalogZaUslugu(usluga))
                {
                    //string Kor = Sifarnici.frmLogovanje.user;
                    //InsertPretovari ins = new InsertPretovari();
                    //ins.UpdateStatusUsluge(usluga, Kor);

                    Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz prijemplat = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(Kor, 0, usluga.ToString(), 1, 2);
                    prijemplat.ShowDialog();

                    if (prijemplat.noviPrijemKontejneraVozID > 0)
                    {
                        int idUslugePretovar = vratiUsluguPretovar(usluga);
                        InsertUvozKonacna ins3 = new InsertUvozKonacna();
                        // modul = 2, poreklo = 1 za ceradu
                        ins3.PrenesiKontejnerIzPlanaNaPrijemnicuIzvozIDNadredjenog(kontejnerID, idUslugePretovar, prijemplat.noviPrijemKontejneraVozID, 2, 1);
                        //  ins3.PrenesiKontejnerIzPlanaNaPrijemnicuIzvoz(kontejnerId,nalogId );
                    }
                    else
                    {
                        // Korisnik je odustao ili snimanje na formi nije uspelo
                        MessageBox.Show("Prijem nije snimljen, prenos kontejnera je otkazan.");
                    }
                    VratiPodatke();
                }
            }
        }


        private bool PostojiRadniNalogZaUslugu(int uslugaId)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            string query = @"SELECT TOP 1 1 
                     FROM RadniNalogInterni 
                     INNER JOIN RNMedjuskladisni ON RadniNalogInterni.BrojRN = RNMedjuskladisni.ID 
                     WHERE RadniNalogInterni.id = @UslugaID";

            using (SqlConnection con = new SqlConnection(s_connection))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UslugaID", uslugaId);

                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value;
                }
            }
        }

        private int vratiUsluguPretovar(int uslugaId)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            int idNalogaPretovar = 0;

            string query = @"SELECT TOP 1 p.ID
                            FROM RadniNalogInterni p
                            INNER JOIN RadniNalogInterni prazan ON p.BrojOsnov = prazan.BrojOsnov
                            INNER JOIN RadniNalogInterniPotvrda prazanPotvrda ON prazan.ID = prazanPotvrda.IDNaloga
                            WHERE prazan.ID = @ID 
                              AND prazanPotvrda.Scenario = 3 
                              AND prazan.IDManipulacijaJed = 69
                              AND p.IDManipulacijaJed in (81)
                            ORDER BY p.ID DESC";

            using (SqlConnection con = new SqlConnection(s_connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", uslugaId);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idNalogaPretovar = Convert.ToInt32(result);
                    }
                }
            }

            return idNalogaPretovar;
        }


        private int vratiUsluguPun(int uslugaId)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            int idNalogaPun = 0;

            string query = @"SELECT TOP 1 p.ID
                            FROM RadniNalogInterni p
                            INNER JOIN RadniNalogInterni prazan ON p.BrojOsnov = prazan.BrojOsnov
                            INNER JOIN RadniNalogInterniPotvrda prazanPotvrda ON prazan.ID = prazanPotvrda.IDNaloga
                            WHERE prazan.ID = @ID 
                              AND prazanPotvrda.Scenario = 3 
                              AND prazan.IDManipulacijaJed = 81
                              AND p.IDManipulacijaJed in (70,71)
                            ORDER BY p.ID DESC";

            using (SqlConnection con = new SqlConnection(s_connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ID", uslugaId);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idNalogaPun = Convert.ToInt32(result);
                    }
                }
            }

            return idNalogaPun;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // kreira prijem, otpremu i vrsi pretovar i zavrsava uslugu 2 (pretvar) i aktivira uslugu 3 (pun kontejner)
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            using (SqlConnection con = new SqlConnection(s_connection))
            {
                con.Open();

                foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
                {
                    
                    int idUsluge = Convert.ToInt32(selectedRecord.Record.GetValue("IDManipulacijaJed")?.ToString());
                    if (idUsluge != 81)
                    {
                        MessageBox.Show("Radni nalog pretovar je moguće napraviti samo za uslugu pretovar.");
                        return;
                    }
                    string usluga = selectedRecord.Record.GetValue("KomNalID")?.ToString() ?? "";
                    string kontejner = selectedRecord.Record.GetValue("Kontejner")?.ToString() ?? "";
                    int kontejnerID = Convert.ToInt32(selectedRecord.Record.GetValue("KontejnerID")?.ToString());

                    // Provera da li imamo validan ID usluge
                    if (!string.IsNullOrEmpty(usluga) && int.TryParse(usluga, out int uslugaId))
                    {
                        int cboSaSklad = 0;
                        int cboSaPoz = 0;

                        string query = @"SELECT RNMedjuskladisni.SaSkladista, RNMedjuskladisni.SaPozicijeSklad 
                             FROM  RNMedjuskladisni 
                             INNER JOIN RadniNalogInterni prazan ON prazan.BrojRN = RNMedjuskladisni.ID 
							 INNER JOIN RadniNalogInterni pretovar on pretovar.BrojOsnov  = prazan.BrojOsnov AND pretovar.IDManipulacijaJED= 81
                             WHERE pretovar.id = @UslugaID";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@UslugaID", uslugaId);

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    cboSaSklad = dr["SaSkladista"] != DBNull.Value ? Convert.ToInt32(dr["SaSkladista"]) : 0;
                                    cboSaPoz = dr["SaPozicijeSklad"] != DBNull.Value ? Convert.ToInt32(dr["SaPozicijeSklad"]) : 0;
                                }
                            }
                        }
                        InsertPretovari ins = new InsertPretovari();
                        // Pozivamo formu Prijemnica sa dinamičkim vrednostima iz baze
                        Prijemnica frm = new Prijemnica(uslugaId, kontejner.TrimEnd(), cboSaSklad, cboSaPoz);
                        frm.ShowDialog();

                        if(frm.novaPrijenicaID > -1)
                            ins.UpdateRadniNalogInterniTipNaloga(uslugaId, "Prij", frm.novaPrijenicaID);
                   
                        Otpremnica fot = new Otpremnica(uslugaId, kontejner.TrimEnd(), null, null);
                        fot.ShowDialog();
           

                        string Kor = Sifarnici.frmLogovanje.user;
                      
                        ins.UpdateStatusUsluge(uslugaId, Kor);

                        int idUslugePun = vratiUsluguPun(uslugaId);  //Kontejner, NalogID.ToString(), Korisnik, 0, OJ, "TxtBrojKontejnera"
                        string napomena = "";

                        RN12MedjuskladisniKontejnera rn12 = new RN12MedjuskladisniKontejnera(idUslugePun, kontejner, napomena);
                        rn12.ShowDialog();

                        //int OJ = VratiOJIzdavanja(uslugaId.ToString());
                        //frmOtpremaVozaIzPlana ovizpl = new frmOtpremaVozaIzPlana(idUslugePun.ToString());
                        //ovizpl.Show();

                        //Saobracaj.Izvoz.frmOtpremaKontejneraKamionomIzKontejnera okk = new Saobracaj.Izvoz.frmOtpremaKontejneraKamionomIzKontejnera(kontejnerID.ToString(), idUslugePun.ToString(), Kor, 1, 2, kontejner);
                        //okk.ShowDialog();

                        //int txtSifraOtpreme = VratiMaxOtpremaKontejneraID();
                        //InsertIzvozKonacna ins2 = new InsertIzvozKonacna();
                        ////Prakticno napravi stavku
                        //ins2.PrenesiKontejnerUOtpremuKamionomIzvoz(txtSifraOtpreme, kontejnerID, idUslugePun);
                        VratiPodatke();

                    }
                }
            }
        }

       


        int VratiOJIzdavanja(string usluga)
        {
            int Konkretan = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select OJIzdavanja from RadniNalogInterni where ID = " + usluga, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = Convert.ToInt32(dr["OJIzdavanja"].ToString().TrimEnd());
            }
            con.Close();
            return Konkretan;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                int idUsluge = Convert.ToInt32(selectedRecord.Record.GetValue("IDManipulacijaJed")?.ToString());
                if (idUsluge != 69)
                {
                    MessageBox.Show("Moguće je samo dozvoliti izlaz uslugama praznog kontejera.");
                    return;
                }
                string usluga = (selectedRecord.Record.GetValue("KomNalID").ToString());
                string kontejner = selectedRecord.Record.GetValue("Kontejner").ToString();
                InsertRadniNalogInterni ir = new InsertRadniNalogInterni();
                ir.DozvoliIzlaz(Convert.ToInt32(usluga));

                //   RadniNalozi.Otpremnica frm = new Otpremnica(Convert.ToInt32(usluga), kontejner.TrimEnd(), txtReg.Text, txtVozac.Text);
                //    RadniNalozi.Otpremnica frm = new Otpremnica(Convert.ToInt32(usluga), kontejner.TrimEnd(), null, null);
                //   frm.Show();
            }
            VratiPodatke();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                int usluga = Convert.ToInt32(selectedRecord.Record.GetValue("KomNalID").ToString());

                InsertRadniNalogInterni ir = new InsertRadniNalogInterni();
                ir.PromeniStatusKalmar(usluga);

                ///menja na Pretovareno = 1
                ir.PromeniStatusPretovareno(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalID").ToString()));

                string Kor = Sifarnici.frmLogovanje.user;
                InsertPretovari ins = new InsertPretovari();
                ins.UpdateStatusUsluge(usluga, Kor);


            }
            VratiPodatke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
            {
                int uslugaPun = Convert.ToInt32(selectedRecord.Record.GetValue("KomNalID").ToString());
                int idUsluge = Convert.ToInt32(selectedRecord.Record.GetValue("IDManipulacijaJed")?.ToString());
                int vaganje = Convert.ToInt32(selectedRecord.Record.GetValue("Vaganje")?.ToString());
                int potrebnoVaganje = Convert.ToInt32(selectedRecord.Record.GetValue("PotrebnoVaganje")?.ToString());
                int fazaUsluge = Convert.ToInt32(selectedRecord.Record.GetValue("FazaUsluge")?.ToString());
                if ((idUsluge != 81) || potrebnoVaganje == 0)
                {
                    MessageBox.Show("Za ovu uslugu nije dozvoljeno vaganje.");
                    return;
                }
                else if (vaganje > 1)
                {
                    MessageBox.Show("Nalog za vaganje je već napravljen.");
                    return;
                }
                    


                InsertPretovari ins = new InsertPretovari();
                ins.UpdateStatusUslugeVaganje(uslugaPun);

                MessageBox.Show("Usluga vaganja je uspešno aktivirana.");

            }

        }
    }
    
}
