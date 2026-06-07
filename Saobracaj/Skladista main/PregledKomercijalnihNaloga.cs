using Saobracaj.Skladista;
using Syncfusion.GridHelperClasses;
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

namespace Saobracaj.Skladista_main
{
    public partial class PregledKomercijalnihNaloga : Form
    {
        string Ulaz;
        string Korisnik = Saobracaj.Sifarnici.frmLogovanje.user;

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
CASE(select Count(*) as Potvrdjen from RadniNalogInterniPotvrda where IDNaloga = rn.[ID]) 
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
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
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

        private void btnAktiviraj_Click(object sender, EventArgs e)
        {
            if (Ulaz == "Izvoz")
            {
                
            }
        }
    }
}
