using Microsoft.Office.Interop.Excel;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Skladista;
using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Grid.Grouping;
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

namespace Saobracaj.Skladista_main
{
    public partial class PregledKomercijalnihNaloga : Form
    {
        string Ulaz;
        string Korisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        public PregledKomercijalnihNaloga(string ulaz)
        {
            InitializeComponent();
            Ulaz= ulaz;   
            if(Ulaz=="Uvoz" || Ulaz == "Izvoz")
            {
                button24.Visible= false;
            }
        }
        private void PregledKomercijalnihNaloga_Load(object sender, EventArgs e)
        {
            FillGV();

            if(Ulaz=="Izvoz" || Ulaz == "Uvoz")
            {
                btnAktiviraj.Visible = true;
            }
            else
            {
                btnAktiviraj.Visible = false;
            }
        }
        private void FillGV()
        {
            var select = "";

            if (Ulaz == "Uvoz")
            {
                select = @"SELECT rn.[ID] as ID  ,UvozKonacna.BrojKontejnera, VrstaManipulacije.Naziv,[Uradjen],
(select Top 1 Naziv from Scenario  inner join UvozKonacna  on UvozKonacna.Scenario = Scenario.ID  where UvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, 
(select Top 1 stNapomene from UvozKonacnaNapomenePozicioniranja inner join UvozKonacna  on UvozKonacna.ID = UvozKonacnaNapomenePozicioniranja.IDNadredjena 
where UvozKonacna.ID = rn.BrojOsnov order by UvozKonacnaNapomenePozicioniranja.ID DEsc) as ScenarioNapomena,
(select Top 1 Voz.NAzivVoza as OznakaVoza from UvozKonacnaZaglavlje 
inner join Voz on Voz.ID = UvozKonacnaZaglavlje.IDVoza 
where UvozKonacnaZaglavlje.ID = rn.PlanID) as VozDolaska ,
TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv, rn.[StatusIzdavanja]  ,
(select Top 1 PaNaziv from Partnerji  inner join UvozKonacna  on UvozKonacna.Brodar = Partnerji.PaSifra  where UvozKonacna.ID = rn.BrojOsnov) as Brodar, 
[OJIzdavanja]      , o1.Naziv as Izdao 
,[OJRealizacije]       ,o2.Naziv as Realizuje  ,[DatumIzdavanja]      ,[DatumRealizacije]  ,rn.[Napomena]  , 
UvozKonacnaVrstaManipulacije.IDVrstaManipulacije ,[Osnov] , PlanID as PlanUtovara  ,
 [BrojOsnov] as BrojOsnov ,  VezniNalogID, [KorisnikIzdao]      ,[KorisnikZavrsio]       , uv.PaNaziv as Platilac  ,
  rn.Pokret,  rn.TipDokPrevoza,
 rn.BrojDokPrevoza, rn.TipRN, rn.BrojRN 
FROM [RadniNalogInterni] rn
 inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID  inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID 
 inner join UvozKonacna on UvozKonacna.ID = BrojOsnov
 inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge
 inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije 
 inner join Partnerji uv on uv.PaSifra = UvozKonacnaVrstaManipulacije.Platilac
 Inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera  Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera
where OJIzdavanja = 1 AND IDManipulacijaJED=74  order by rn.ID desc";
            }
            if (Ulaz == "Izvoz")
            {
                select = @"SELECT rn.[ID] as ID,IzvozKonacna.BrojKontejnera , VrstaManipulacije.Naziv, [Uradjen] ,
(select Top 1 Naziv 
from Scenario  
inner join IzvozKonacna  on IzvozKonacna.Scenario = Scenario.ID  
where IzvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv,
CASE(select Count(*) as Potvrdjen from RadniNalogInterniSkladistePotvrda where IDNaloga = rn.[ID]) 
WHEN 0 THEN 'NEAKTIVAN' 
WHEN 1 THEN 'AKTIVAN' 
END AS StatusKN, 
CASE Cirada 
WHEN 0 THEN 'PLATFORMA' 
WHEN 1 THEN 'CIRADA' 
END AS TipNaloga,
(select Top 1 Voz.NAzivVoza as OznakaVoza 
from IzvozKonacnaZaglavlje 
inner join Voz on Voz.ID = IzvozKonacnaZaglavlje.IDVoza 
where IzvozKonacnaZaglavlje.ID = rn.PlanID) as VozOdlaska , 
TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv, rn.[StatusIzdavanja],
(select Top 1 PaNaziv 
from Partnerji  
inner join IzvozKonacna  on IzvozKonacna.Brodar = Partnerji.PaSifra  
where izvozKonacna.ID = rn.BrojOsnov) as Brodar,
[OJIzdavanja], o1.Naziv as Izdao ,[OJRealizacije],o2.Naziv as Realizuje,[DatumIzdavanja],[DatumRealizacije]  ,rn.[Napomena], IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije, 
[Osnov], PlanID as PlanUtovara ,[BrojOsnov] as BrojOsnov ,  VezniNalogID ,[KorisnikIzdao],[KorisnikZavrsio], uv.PaNaziv as Platilac, rn.Pokret,  rn.TipDokPrevoza,
rn.BrojDokPrevoza, rn.TipRN, rn.BrojRN   
FROM RadniNalogInterni rn 
inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID 
inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID 
inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge
inner join IzvozKonacna on IzvozKonacna.ID = IzvozKonacnaVrstaManipulacije.IDNAdredjena 
inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije 
inner join Partnerji uv on uv.PaSifra = IzvozKonacnaVrstaManipulacije.Platilac 
Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera 
inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera
 where OJIzdavanja = 2 And IDManipulacijaJED=74  order by rn.ID desc";
            }
            if (Ulaz == "Direktni")
            {
                select = @"Select ID,RadniNalogSkladista.Datum as Datum,Korisnik,VrstaRN,TipRN,CarinskoSkladiste,RTRIM(p1.PaNaziv) as Nalogodavac,RTrim(p2.PaNaziv) as VlasnikRobe,
                OpisPosla,Napomena,Aktivan,Formiran 
                from RadniNalogSkladista 
                inner join Partnerji as p1 on RadniNalogSkladista.Nalogodavac=p1.PaSifra 
                inner join Partnerji as p2 on RadniNalogSkladista.VlasnikRobe=p2.PaSifra 
                WHere  Formiran=0";
            }

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Yellow;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Black;

            gcfd3.Expression = "[StatusKN] =  'AKTIVAN'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd3);

            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl1);

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);
        }
        private void button25_Click(object sender, EventArgs e)
        {
            FillGV();
        }
        int ID;
        int BrojRN;
        private void button24_Click(object sender, EventArgs e)
        {
            if (Ulaz == "Izvoz" || Ulaz == "Uvoz")
            {
               //Aktiviraj radni nalog
            }
            if (Ulaz == "Direktni")
            {
                var main = this.TopLevelControl as NewMain;

                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    ID = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString());
                    //ovde kada bude bio interni RN sad je ID isto sto i brojRN
                    //BrojRN= Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("BrojRN").ToString());
                    BrojRN= Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString());

                    if (BrojRN != 0)
                    {
                        var s_connection = Sifarnici.frmLogovanje.connectionString;
                        using (SqlConnection conn = new SqlConnection(s_connection))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(@"select VrstaRN,TipRN,Formiran From RadniNalogSkladista WHere ID=" + BrojRN, conn))
                            {
                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.Read())
                                    {
                                        var formiran = Convert.ToInt32(dr["Formiran"].ToString());
                                        if (formiran == 1)
                                        {
                                            MessageBox.Show("RN je već formiran!");
                                            return;
                                        }
                                        var vrstaRN = dr["VrstaRN"].ToString();
                                        var tipRN = dr["TipRN"].ToString();

                                        if (tipRN == "Prijem")
                                        {
                                            main.OtvoriFormuBezPrava(() => new Dokumenta.Prijem(ID, Ulaz, vrstaRN, Korisnik, BrojRN));
                                        }
                                        if (tipRN == "Otprema")
                                        {
                                            main.OtvoriFormuBezPrava(() => new Dokumenta.Otprema(ID, Ulaz, vrstaRN, Korisnik, BrojRN));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (main == null) return;

                        main.OtvoriFormuBezPrava(
                            () => new TipSkladista(ID, Ulaz)
                        );
                    }
                }
            }
           
        }

        string TipRN = "";
        int BrojRN1 = 0;
        int ProveriDaLijeVecGenerisanaOperacija(string Nalog)
        {

            int Uradjen = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 TipRN, BrojRN from RadniNalogInterni where ID = " + txtNALOGID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Uradjen = 1;
                TipRN = dr["TipRN"].ToString().TrimEnd();
                BrojRN1 = Convert.ToInt32(dr["BrojRN"].ToString().TrimEnd());
                if (BrojRN1 == 0)
                {
                    Uradjen = 0;
                }
            }
            con.Close();
            return Uradjen;

        }

        private void btnAktiviraj_Click(object sender, EventArgs e)
        {
            if (txtNALOGID.Text == "")
            {
                MessageBox.Show("Obelezite uslugu");
                return;
            }



            int i = 0;
            int j = 0;
            j = ProveriDaLijeVecGenerisanaOperacija(txtNALOGID.Text);
            if (j > 0)
            {
                MessageBox.Show("Za ovu uslugu već je generisan RN, " + TipRN + " broj :" + BrojRN);
                return;
            }
            /*
            i = ProveriDaLiJeUradjenaPredhodnaOperacija(txtNALOGID.Text);
            if (i == 0)
            {
                MessageBox.Show("Nije zavrsena predhodna usluga ne mozete generisati novu!!!");
                return;
            }
            */
            /*
            Saobracaj.Uvoz.InsertRadniNalogInterni ins = new Saobracaj.Uvoz.InsertRadniNalogInterni();
            ins.InsRadniNalogInterniIzvozPotvrda(Convert.ToInt32(txtNALOGID.Text));
            MessageBox.Show("Potvrdjen je Komercijalni nalog!!!");
            */
            //Scenario 1 test - Napravi PRI i RN4





            string Forma = VratiFormu();
            int KISUsl = 0;
            int OJ = VratiOJIzdavanja();
            if (Forma == "SKLADISNINA")
            {
                InsertSkladista ins = new InsertSkladista();

                var selektovaniID = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojOsnov");
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();
                //Podesi vrednosti koje vec imas za unos
                SqlCommand cmd;
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
                                  " where ik.ID =  " + selektovaniID, con);

                SqlDataReader dr = cmd.ExecuteReader();
                int Klijent1=0; string OpisPosla = "";int ADR = 0; string BrojKontejnera = "";
                int VrstaKontejnera=0; int Izvoznik = 0; int VrstaKamiona = 0; string Vozilo = "";
                string Vozac = ""; string BrLK = ""; string Telefon = ""; int NacinPakovanja = 0; string Napomena = "";
                int CarinskiPUnutrasniTransport = 0;
                int PolaznaCarinarnica = 0; int SpediterPolazna = 0; int OdredisnaCarinarnica = 0; int SpediterOdredisna = 0;
                string KontaktSpediteraOdredisna = ""; string KontaktSpeditera = "";
                int MestoUtovaraCerade = 0;
                DateTime DatumUtovaraCerade = DateTime.Now;
                DateTime NoviDatumUtovaraCerade = DateTime.Now;
                int MestoIstovaraCerade = 0;
                DateTime DatumIstovaraCerade = DateTime.Now;
                DateTime  NoviDatumIstovaraCerade = DateTime.Now;
                while (dr.Read())
                {
                    if (dr["Klijent1"] != DBNull.Value)
                    {Klijent1 = Convert.ToInt32(dr["Klijent1"].ToString()); }
                    OpisPosla = dr["OpisPosla"].ToString();
                    
                    if (dr["ADR"] != DBNull.Value)
                    {
                        ADR = Convert.ToInt32(dr["ADR"].ToString());
                    }
                  BrojKontejnera = dr["BrojKontejnera"].ToString();
                   VrstaKontejnera = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                   Izvoznik= Convert.ToInt32(dr["Izvoznik"].ToString());
                   VrstaKamiona = Convert.ToInt32(dr["Cirada"].ToString());
                   Vozilo = dr["Vozilo"].ToString().Trim();
                   Vozac = dr["Vozac"].ToString().Trim();
                   BrLK = dr["BrojLK"].ToString().Trim();
                   Telefon = dr["BrojTelefona"].ToString().Trim();
                   
                        if (dr["NacinPakovanja"] != DBNull.Value)
                    { NacinPakovanja = Convert.ToInt32(dr["NacinPakovanja"].ToString());}
                   Napomena = dr["NaslovSlanjaStatusa"].ToString().Trim();
                   
                    if (dr["CarinskiPostupakUnutrasnji"] != DBNull.Value)
                    {CarinskiPUnutrasniTransport = Convert.ToInt32(dr["CarinskiPostupakUnutrasnji"].ToString()); }

              
                    if (dr["MestoCarinjenja"] != DBNull.Value)
                    {PolaznaCarinarnica = Convert.ToInt32(dr["MestoCarinjenja"].ToString()); }
              
                    if (dr["Spedicija"] != DBNull.Value)
                    {SpediterPolazna = Convert.ToInt32(dr["Spedicija"].ToString()); }
                
                    if (dr["OdredisnaCarinarnica"] != DBNull.Value)
                    {OdredisnaCarinarnica = Convert.ToInt32(dr["OdredisnaCarinarnica"].ToString()); }
                   
                    if (dr["SpediterOdredisna"] != DBNull.Value)
                    {SpediterOdredisna = Convert.ToInt32(dr["SpediterOdredisna"].ToString()); }
                   KontaktSpediteraOdredisna = dr["KontaktSpediteraOdredisna"].ToString().Trim();
                   KontaktSpeditera = dr["KontaktSpeditera"].ToString().Trim();

                    if (dr["MestoIstovaraCerade"] != DBNull.Value)
                    {
                        MestoIstovaraCerade = Convert.ToInt32(dr["MestoIstovaraCerade"].ToString());
                    }
                    if (dr["PlaniraniDatumIstovaraCerade"] != DBNull.Value)
                    {
                        DatumIstovaraCerade = Convert.ToDateTime(dr["PlaniraniDatumIstovaraCerade"]);
                    }
                    if ( dr["NoviPlaniraniDatumIstovaraCerade"] != DBNull.Value)
                    {
                       NoviDatumIstovaraCerade= Convert.ToDateTime(dr["NoviPlaniraniDatumIstovaraCerade"]);
                    }
                   
                    if (dr["MestoUtovaraCerade"] != DBNull.Value)
                    {
                        MestoUtovaraCerade = Convert.ToInt32(dr["MestoUtovaraCerade"].ToString());
                    }
                    if (dr["PlaniraniDatumUtovaraCerade"] != DBNull.Value)
                    {
                        DatumUtovaraCerade= Convert.ToDateTime(dr["PlaniraniDatumUtovaraCerade"]);
                    }
                    if ( dr["NoviPlaniraniDatumUtovaraCerade"] != DBNull.Value)
                    {
                        NoviDatumUtovaraCerade = Convert.ToDateTime(dr["NoviPlaniraniDatumUtovaraCerade"]);
                    }
                    
                 


                }

                string SkladisteTip = "Komercijalno";

                if (CarinskiPUnutrasniTransport != 0)
                        {
                    SkladisteTip = "Carinsko";
                }

                //End of podesi vrednosti
                // Formiram Radni nalog skladiste - LO NAlog
                ins.InsertRadniNalog("Kreiran", 
                    DateTime.Now,
                    Korisnik, 
                    "Carinsko",
                    "Prijem", 
                    1008.ToString(),
                    Convert.ToInt32(0), //MB
                    Convert.ToInt32(Klijent1), // Nalogodavac
                    Convert.ToInt32(CarinskiPUnutrasniTransport),
                    OpisPosla,
                    Convert.ToInt32(Izvoznik),
                    "", // Vrsta robe
                   NacinPakovanja.ToString(), //NAcinPakovanja je inace sifarnik
                   Convert.ToInt32(0), //Ostala skladista
                   Convert.ToInt32(0), //PIB
                    Convert.ToInt32(1), 
                    Convert.ToInt32(VrstaKamiona),
                    Vozilo,
                    Vozac,
                    BrLK,
                    Telefon,
                    Convert.ToInt32(OdredisnaCarinarnica), 
                    Convert.ToInt32(SpediterOdredisna),
                    KontaktSpediteraOdredisna, 
                    Convert.ToInt32(MestoUtovaraCerade),  // MestoIstovaraOtprema
                    "", // Adresa otprema
                    "", // Kontakt osoba otprema
                    DatumUtovaraCerade, //Planirani datum otprema
                    NoviDatumUtovaraCerade,
                    BrojKontejnera,
                    Convert.ToInt32(0),
                    Convert.ToInt32(VrstaKamiona),
                      Vozilo,
                    Vozac,
                    BrLK,
                    Telefon,
                    Convert.ToInt32(PolaznaCarinarnica),
                    Convert.ToInt32(SpediterPolazna),
                    KontaktSpeditera,
                    Convert.ToInt32(MestoIstovaraCerade),
                    "", // KontaktOsobaPrijemaCerade
                    "",
                    DatumIstovaraCerade,
                    NoviDatumIstovaraCerade,
                    BrojKontejnera,
                    "", //PosebniUslovi
                    Convert.ToInt32(0), //GrtupaUslugaID
                    Napomena,
                    Convert.ToInt32(1),
                    Convert.ToInt32(1));


          
         

          

                string BrojRNMAX = "";
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (SqlCommand cmd1 = new SqlCommand("select Max(ID) as ID from RadniNalogSkladista", conn))
                    {
                        SqlDataReader dr1 = cmd1.ExecuteReader();
                        while (dr1.Read())
                        {
                            BrojRNMAX = dr1["ID"].ToString();
                        }
                    }
                    conn.Close();
                }

               
               ins.UpdateRNInterni(Convert.ToInt32(txtNALOGID.Text), Convert.ToInt32(BrojRNMAX));

                Saobracaj.Uvoz.InsertRadniNalogInterni ins1 = new Saobracaj.Uvoz.InsertRadniNalogInterni();
                ins1.InsRadniNalogInterniIzvozSkladistePotvrda(Convert.ToInt32(txtNALOGID.Text));
                MessageBox.Show("Potvrdjen je Komercijalni nalog!!!");


            }



           
        }

        int VratiOJIzdavanja()
        {
            int Konkretan = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select OJIzdavanja from RadniNalogInterni where ID = " + txtNALOGID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = Convert.ToInt32(dr["OJIzdavanja"].ToString().TrimEnd());
            }
            con.Close();
            return Konkretan;

        }

        string VratiFormu()
        {
            if (txtNALOGID.Text == "")
            {

                MessageBox.Show("Obelezite bar jednu stavku voza");
                return "";
            }
            else
            {
                string formica = "";
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("select Forma from RadniNalogInterni where ID = " + txtNALOGID.Text, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    formica = dr["Forma"].ToString().TrimEnd();



                }
                con.Close();
                return formica;
            }


        }
        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtNALOGID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (gridGroupingControl1.Table.CurrentRecord != null)
            {
                // 2. Dohvatamo vrednost polja "ID" iz selektovanog reda
                var selektovaniID = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojOsnov");


                if (selektovaniID != null)
                {
                    int idZaFormu = Convert.ToInt32(selektovaniID);

                    // 3. Sada imamo ID

                    RNI.frmScenarioSCI sc1 = new RNI.frmScenarioSCI(idZaFormu, "Izvoz");
                    sc1.Show();
                }
            }
            else
            {
                MessageBox.Show("Molimo vas da prvo izaberete kontejner u gornjoj tabeli.");
            }
        }
    }
}
