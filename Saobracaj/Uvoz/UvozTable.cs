using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Data;
using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Grouping;

namespace Saobracaj.Uvoz
{
    public partial class UvozTable : Form
    {
        int Selektovani = 0;
        private Keys keyData;
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        public UvozTable()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }

        public string GetID()
        {
            return textBox1.Text;
        }

        private void UvozTable_Load(object sender, EventArgs e)
        {
            var select = "SELECT Uvoz.ID, [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,  DobijenBZ, Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz Left join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
" order by Uvoz.ID desc ";



            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
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
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    textBox1.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void UvozTable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                Close();
            }
        }
        private void PunjenjeVrednostiPolja()
        {
            /*
            
            ---- Datumska  Datum BZ, ATA broda u Luku Rijeka
             --- CHECK DobijenBZ, Prioritet
                ----- Text PIN, Broj kontejnera, BL, Ref za fakturisanje 1,Ref za fakturisanje 2,Ref za fakturisanje 3,Kontakt osobe
Naslov za slanje statusa vozila , Napomena 2,
E - mail za slanje statusa,
Broj plombe 1
Broj plombe 2
                --------- Combo Vrsta kontejnera, Relacija R / L / SRB, Dirigacija kontejnera za,
                ADR,Brodar,Vlasnik kontejnera,Nalogodavac za voz,Nalogdavac za usluge,Nalogodavac za drumski prevoz

Uvoznik,Inspekciski tretman,Špedicija - granica,Špedicija - RTC Leget,
 Carinski postupak
  Postupak sa robom / kontejnerom
Način pakovanja
Odredišna Špedicija
Carinarnica
Mesto istovara
Adresa utovara

---- NUMERIC NTTO robe
Tara kontejnera
BTTO kontejnera
BTTO robe
Koleta

*/
            SqlConnection conn = new SqlConnection(connection);
            string updatestring = "";
            switch (cboPolje.Text)
            {
                case "Datum BZ":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                  //  updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;
                case "ATA broda u Luku Rijeka":
                    dtpOpsti.Visible = true;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                   
                case "DobijenBZ":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    break;
                case "Prioritet":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    break;
                case "PIN":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Broj kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "BL":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ref za fakturisanje 1":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ref za fakturisanje 2":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ref za fakturisanje 3":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Kontakt osobe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Naslov za slanje statusa vozila":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Napomena 2":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "E - mail za slanje statusa":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
 
   
                case "Vrsta kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
                    var tkAD = new SqlDataAdapter(tipkontejnera, conn);
                    var tkDS = new DataSet();
                    tkAD.Fill(tkDS);
                    cboOpsti.DataSource = tkDS.Tables[0];
                    cboOpsti.DisplayMember = "SkNaziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Relacija R / L / SRB":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var rl = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
                    var rlSAD = new SqlDataAdapter(rl, conn);
                    var rlSDS = new DataSet();
                    rlSAD.Fill(rlSDS);
                    cboOpsti.DataSource = rlSDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Dirigacija kontejnera za":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var dir = "Select ID,Naziv from DirigacijaKontejneraZa order by Naziv";
                    var dirAD = new SqlDataAdapter(dir, conn);
                    var dirDS = new DataSet();
                    dirAD.Fill(dirDS);
                    cboOpsti.DataSource = dirDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "ADR":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var adr = "Select ID, (Naziv + ' - ' + UNKod) as Naziv From VrstaRobeADR order by (UNKod + ' ' + Naziv)";
                    var adrSAD = new SqlDataAdapter(adr, conn);
                    var adrSDS = new DataSet();
                    adrSAD.Fill(adrSDS);
                    cboOpsti.DataSource = adrSDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Brodar":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var bro = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
                    var broAD = new SqlDataAdapter(bro, conn);
                    var broDS = new DataSet();
                    broAD.Fill(broDS);
                    cboOpsti.DataSource = broDS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Vlasnik kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner = "Select PaSifra,PaNaziv From Partnerji where Vlasnik = 1  order by PaNaziv";
                    var partAD = new SqlDataAdapter(partner, conn);
                    var partDS = new DataSet();
                    partAD.Fill(partDS);
                    cboOpsti.DataSource = partDS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Nalogodavac za voz":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji where NalogodavacCH = 1 order by PaNaziv";
                    var nal1AD = new SqlDataAdapter(nalogodavac1, conn);
                    var nal1DS = new DataSet();
                    nal1AD.Fill(nal1DS);
                    cboOpsti.DataSource = nal1DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Nalogdavac za usluge":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var nalogodavac2 = "Select PaSifra,PaNaziv From Partnerji where NalogodavacCH = 1 order by PaNaziv";
                    var nal2AD = new SqlDataAdapter(nalogodavac2, conn);
                    var nal2DS = new DataSet();
                    nal2AD.Fill(nal2DS);
                    cboOpsti.DataSource = nal2DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Nalogodavac za drumski prevoz":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var nalogodavac3 = "Select PaSifra,PaNaziv From Partnerji where NalogodavacCH = 1 order by PaNaziv";
                    var nal3AD = new SqlDataAdapter(nalogodavac3, conn);
                    var nal3DS = new DataSet();
                    nal3AD.Fill(nal3DS);
                    cboOpsti.DataSource = nal3DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;

                case "Uvoznik":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner2 = "Select PaSifra,PaNaziv From Partnerji where UvoznikCH = 1 order by PaNaziv";
                    var partAD2 = new SqlDataAdapter(partner2, conn);
                    var partDS2 = new DataSet();
                    partAD2.Fill(partDS2);
                    cboOpsti.DataSource = partDS2.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    //spedicija na granici
                    break;
                case "Inspekciski tretman":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var it = "select ID, Naziv from InspekciskiTretman order by Naziv";
                    var itAD = new SqlDataAdapter(it, conn);
                    var itDS = new DataSet();
                    itAD.Fill(itDS);
                    cboOpsti.DataSource = itDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Špedicija -granica":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner3 = "Select PaSifra,PaNaziv From Partnerji where  Spediter = 1 order by PaNaziv";
                    var partAD3 = new SqlDataAdapter(partner3, conn);
                    var partDS3 = new DataSet();
                    partAD3.Fill(partDS3);
                    cboOpsti.DataSource = partDS3.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    //spedicija rtc luka leget
                    break;
                case "Špedicija - RTC Leget":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var partner4 = "Select PaSifra,PaNaziv From Partnerji  where Spediter = 1 order by PaNaziv";
                    var partAD4 = new SqlDataAdapter(partner4, conn);
                    var partDS4 = new DataSet();
                    partAD4.Fill(partDS4);
                    cboOpsti.DataSource = partDS4.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Carinski postupak":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //carinski postupak
                    var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
                    var dirAD2 = new SqlDataAdapter(dir2, conn);
                    var dirDS2 = new DataSet();
                    dirAD2.Fill(dirDS2);
                    cboOpsti.DataSource = dirDS2.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Postupak sa robom / kontejnerom":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by Naziv";
                    var dirAD3 = new SqlDataAdapter(dir3, conn);
                    var dirDS3 = new DataSet();
                    dirAD3.Fill(dirDS3);
                    cboOpsti.DataSource = dirDS3.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Odredišna Špedicija":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner5 = "Select PaSifra,PaNaziv From Partnerji where Spediter = 1 order by PaNaziv";
                    var partAD5 = new SqlDataAdapter(partner5, conn);
                    var partDS5 = new DataSet();
                    partAD5.Fill(partDS5);
                    cboOpsti.DataSource = partDS5.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Način pakovanja":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //nacin pakovanja
                    var dir4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
                    var dirAD4 = new SqlDataAdapter(dir4, conn);
                    var dirDS4 = new DataSet();
                    dirAD4.Fill(dirDS4);
                    cboOpsti.DataSource = dirDS4.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Carinarnica":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var car = "Select ID, Naziv From Carinarnice order by Naziv";
                    var carAD = new SqlDataAdapter(car, conn);
                    var carDS = new DataSet();
                    carAD.Fill(carDS);
                    cboOpsti.DataSource = carDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Mesto istovara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var mu = "select ID, Naziv from MestaUtovara order by Naziv";
                    var muAD = new SqlDataAdapter(mu, conn);
                    var muDS = new DataSet();
                    muAD.Fill(muDS);
                    cboOpsti.DataSource = muDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Adresa utovara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var ko = "select PaKoZapSt, (PaKoOpomba) as Naziv from partnerjiKontOsebaMU order by PaKOIme";
                    var koAD = new SqlDataAdapter(ko, conn);
                    var koDS = new DataSet();
                    koAD.Fill(koDS);
                    cboOpsti.DataSource = koDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "PaKoZapSt";
                    break;
                case "NTTO robe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Tara kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "BTTO kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "BTTO robe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Koleta":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                default:
                    Console.WriteLine("Nema podatka");
                    break;

            }













            /*

                        string updatestring = "";
                        switch (cboPolje.Text)
                        {
                            case "Naziv broda":
                               
                                break;
                            case "Napomena 1":
                                updatestring = " Update uvoz set Napomena1 = '" + txtNapomena1.Text + "'";
                                break;
                            case "Datum BZ":
                                updatestring = " Update uvoz set DobijeBZ = '" + txtBZ.Text.ToString().TrimEnd() + "'";
                                break;
                            case "PIN":
                                updatestring = " Update uvoz set PIN = '" + txtPIN.Text + "'";
                                break;
                            case "Vrsta kontejnera":
                                updatestring = " Update uvoz set TaraKontejnera = " + Convert.ToInt32(txtTipKont.SelectedValue);
                                break;
                            case "Relacija R\\L\\SRB":
                                updatestring = " Update uvoz set RLTerminali = " + Convert.ToInt32(cboRLTerminal.SelectedValue);
                                break;
                            case "BL":
                                updatestring = " Update uvoz set BrodskaTeretnica = '" + txtTeretnica.Text + "'";
                                break;
                            case "ADR":
                                updatestring = " Update uvoz set ADR = " + Convert.ToInt32(txtADR.SelectedValue);
                                break;
                            case "Brodar":
                                updatestring = " Update uvoz set Brodar = " + Convert.ToInt32(cboBrodar.SelectedValue);
                                break;
                            case "Vlasnik":
                                updatestring = " Update uvoz set Vlasnik = " + Convert.ToInt32(cbVlasnikKont.SelectedValue);
                                break;
                            case "Uvoznik":
                                updatestring = " Update uvoz set Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue);
                                break;
                            case "Nalogodavac 1":
                                updatestring = " Update uvoz set Nalogodavac1 = " + Convert.ToInt32(cboNalogodavac1.SelectedValue);
                                break;
                            case "Ref1":
                                updatestring = " Update uvoz set Ref1 = '" + txtRef1.Text + "'";
                                break;
                            case "Nalogodavac 2":
                                updatestring = " Update uvoz set Nalogodavac2 = " + Convert.ToInt32(cboNalogodavac2.SelectedValue);
                                break;
                            case "Ref2":
                                updatestring = " Update uvoz set Ref2 = '" + txtOpsti.Text + "'";
                                break;
                            case "Nalogodavac3":
                                updatestring = " Update uvoz set Nalogodavac3 = " + Convert.ToInt32(cboNalogodavac3.SelectedValue);
                                break;
                            case "Ref3":
                                updatestring = " Update uvoz set Ref3 = '" + txtRef3.Text + "'";
                                break;
                            case "VrstaPregleda":
                                updatestring = " Update uvoz set VrstaPregleda = '" + Convert.ToInt32(txtVrstaPregleda.SelectedValue) + "'";
                                break;
                            case "Špedicija - RTCLeget":
                                updatestring = " Update uvoz set SpedicijaRTC = " + Convert.ToInt32(cboSpedicijaRTC.SelectedValue);
                                break;
                            case "Špedicija granica":
                                updatestring = " Update uvoz set SpedicijaGranica = " + Convert.ToInt32(cboSpedicijaG.SelectedValue);
                                break;
                            case "Carinski postupak":
                                updatestring = " Update uvoz set CarinskiPostupak = " + Convert.ToInt32(cboCarinskiPostupak.SelectedValue);
                                break;
                            case "Postupak sa robom":
                                updatestring = " Update uvoz set PostupakSaRobom = " + Convert.ToInt32(cbPostupak.SelectedValue);
                                break;
                            case "Način pakovanja":
                                updatestring = " Update uvoz set NacinPakovanja = " + Convert.ToInt32(cbNacinPakovanja.SelectedValue);
                                break;
                            case "Napomena 2":
                                updatestring = " Update uvoz set Napomena2 = " + txtNapomena.Text;
                                break;
                            case "Odredišna špedicija":
                                updatestring = " Update uvoz set OdredisnaSpedicija = " + Convert.ToInt32(cbOspedicija.SelectedValue);
                                break;
                            case "Carinarnica":
                                updatestring = " Update uvoz set OdredisnaCarina = " + Convert.ToInt32(cbOcarina.SelectedValue);
                                break;
                            case "Mesto istovara":
                                updatestring = " Update uvoz set MestoIstovara = '" + Convert.ToInt32(txtMesto.SelectedValue) + "'";
                                break;
                            case "Kontakt osoba":
                                updatestring = " Update uvoz set KontaktOsoba = '" + Convert.ToInt32(txtKontaktOsoba.SelectedValue) + "'";
                                break;
                            case "EMail":
                                updatestring = " Update uvoz set Email = '" + txtMail.Text.ToString() + "'";
                                break;
                            default:
                                Console.WriteLine("Nema podatka");
                                break;

                        }


                        try
                        {
                            // 1. Create Command
                            // Sql Update Statement
                            string updateSql = updatestring + " where ID = " + IdZaPromenu;

                            SqlConnection conn = new SqlConnection(connection);
                            SqlCommand cmd = new SqlCommand(updateSql, conn);
                            conn.Open();
                            var q = cmd.ExecuteNonQuery();
                            conn.Close();


                        }

                        catch (SqlException ex)
                        {
                            // Display error
                            Console.WriteLine("Error: " + ex.ToString());
                        }



                        //FillGV();
                    }
                    private void button7_Click(object sender, EventArgs e)
                    {
                        int IDZaPromenu = 0;
                        try
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Selected)
                                {
                                    IDZaPromenu = Convert.ToInt32(row.Cells[0].Value.ToString());
                                    UpdateVrednostiPolja(IDZaPromenu);
                                }
                            }
                            // FillGV();

                        }
                        catch
                        {
                            MessageBox.Show("Nije uspela selekcija stavki");
                        }
            */
        }

        private void UpdateVrednostiPolja(int IdZaPromenu)
        {
            int temp = 0;
            SqlConnection conn = new SqlConnection(connection);
            string updatestring = "";
            switch (cboPolje.Text)
            {
                case "Datum BZ":
                   
                    break;
                case "ATA broda u Luku Rijeka":
                   
                    break;

                case "DobijenBZ":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set DobijenBZ = " + temp + " where ID ="  + IdZaPromenu;
                    break;
                case "Prioritet":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = true;
                    nmrOpsti.Visible = false;
                    temp = 0;
                    if (chkOpsti.Checked == true)
                    { temp = 1; }
                    updatestring = " Update uvoz set Prioritet = " + temp + " where ID =" + IdZaPromenu;
                    break;
                case "PIN":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Broj kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "BL":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ref za fakturisanje 1":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ref za fakturisanje 2":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Ref za fakturisanje 3":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Kontakt osobe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Naslov za slanje statusa vozila":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Napomena 2":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "E - mail za slanje statusa":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = false;
                    txtOpsti.Visible = true;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;


                case "Vrsta kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
                    var tkAD = new SqlDataAdapter(tipkontejnera, conn);
                    var tkDS = new DataSet();
                    tkAD.Fill(tkDS);
                    cboOpsti.DataSource = tkDS.Tables[0];
                    cboOpsti.DisplayMember = "SkNaziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Relacija R / L / SRB":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var rl = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by (Naziv + ' ' + Oznaka)";
                    var rlSAD = new SqlDataAdapter(rl, conn);
                    var rlSDS = new DataSet();
                    rlSAD.Fill(rlSDS);
                    cboOpsti.DataSource = rlSDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Dirigacija kontejnera za":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var dir = "Select ID,Naziv from DirigacijaKontejneraZa order by Naziv";
                    var dirAD = new SqlDataAdapter(dir, conn);
                    var dirDS = new DataSet();
                    dirAD.Fill(dirDS);
                    cboOpsti.DataSource = dirDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "ADR":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var adr = "Select ID, (Naziv + ' - ' + UNKod) as Naziv From VrstaRobeADR order by (UNKod + ' ' + Naziv)";
                    var adrSAD = new SqlDataAdapter(adr, conn);
                    var adrSDS = new DataSet();
                    adrSAD.Fill(adrSDS);
                    cboOpsti.DataSource = adrSDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Brodar":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var bro = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
                    var broAD = new SqlDataAdapter(bro, conn);
                    var broDS = new DataSet();
                    broAD.Fill(broDS);
                    cboOpsti.DataSource = broDS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Vlasnik kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner = "Select PaSifra,PaNaziv From Partnerji where Vlasnik = 1  order by PaNaziv";
                    var partAD = new SqlDataAdapter(partner, conn);
                    var partDS = new DataSet();
                    partAD.Fill(partDS);
                    cboOpsti.DataSource = partDS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Nalogodavac za voz":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji where NalogodavacCH = 1 order by PaNaziv";
                    var nal1AD = new SqlDataAdapter(nalogodavac1, conn);
                    var nal1DS = new DataSet();
                    nal1AD.Fill(nal1DS);
                    cboOpsti.DataSource = nal1DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Nalogdavac za usluge":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var nalogodavac2 = "Select PaSifra,PaNaziv From Partnerji where NalogodavacCH = 1 order by PaNaziv";
                    var nal2AD = new SqlDataAdapter(nalogodavac2, conn);
                    var nal2DS = new DataSet();
                    nal2AD.Fill(nal2DS);
                    cboOpsti.DataSource = nal2DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Nalogodavac za drumski prevoz":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var nalogodavac3 = "Select PaSifra,PaNaziv From Partnerji where NalogodavacCH = 1 order by PaNaziv";
                    var nal3AD = new SqlDataAdapter(nalogodavac3, conn);
                    var nal3DS = new DataSet();
                    nal3AD.Fill(nal3DS);
                    cboOpsti.DataSource = nal3DS.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;

                case "Uvoznik":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner2 = "Select PaSifra,PaNaziv From Partnerji where UvoznikCH = 1 order by PaNaziv";
                    var partAD2 = new SqlDataAdapter(partner2, conn);
                    var partDS2 = new DataSet();
                    partAD2.Fill(partDS2);
                    cboOpsti.DataSource = partDS2.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    //spedicija na granici
                    break;
                case "Inspekciski tretman":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var it = "select ID, Naziv from InspekciskiTretman order by Naziv";
                    var itAD = new SqlDataAdapter(it, conn);
                    var itDS = new DataSet();
                    itAD.Fill(itDS);
                    cboOpsti.DataSource = itDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Špedicija -granica":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner3 = "Select PaSifra,PaNaziv From Partnerji where  Spediter = 1 order by PaNaziv";
                    var partAD3 = new SqlDataAdapter(partner3, conn);
                    var partDS3 = new DataSet();
                    partAD3.Fill(partDS3);
                    cboOpsti.DataSource = partDS3.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    //spedicija rtc luka leget
                    break;
                case "Špedicija - RTC Leget":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var partner4 = "Select PaSifra,PaNaziv From Partnerji  where Spediter = 1 order by PaNaziv";
                    var partAD4 = new SqlDataAdapter(partner4, conn);
                    var partDS4 = new DataSet();
                    partAD4.Fill(partDS4);
                    cboOpsti.DataSource = partDS4.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Carinski postupak":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //carinski postupak
                    var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
                    var dirAD2 = new SqlDataAdapter(dir2, conn);
                    var dirDS2 = new DataSet();
                    dirAD2.Fill(dirDS2);
                    cboOpsti.DataSource = dirDS2.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Postupak sa robom / kontejnerom":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var dir3 = "Select ID,Naziv from VrstePostupakaUvoz order by Naziv";
                    var dirAD3 = new SqlDataAdapter(dir3, conn);
                    var dirDS3 = new DataSet();
                    dirAD3.Fill(dirDS3);
                    cboOpsti.DataSource = dirDS3.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Odredišna Špedicija":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var partner5 = "Select PaSifra,PaNaziv From Partnerji where Spediter = 1 order by PaNaziv";
                    var partAD5 = new SqlDataAdapter(partner5, conn);
                    var partDS5 = new DataSet();
                    partAD5.Fill(partDS5);
                    cboOpsti.DataSource = partDS5.Tables[0];
                    cboOpsti.DisplayMember = "PaNaziv";
                    cboOpsti.ValueMember = "PaSifra";
                    break;
                case "Način pakovanja":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    //nacin pakovanja
                    var dir4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
                    var dirAD4 = new SqlDataAdapter(dir4, conn);
                    var dirDS4 = new DataSet();
                    dirAD4.Fill(dirDS4);
                    cboOpsti.DataSource = dirDS4.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";
                    break;
                case "Carinarnica":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var car = "Select ID, Naziv From Carinarnice order by Naziv";
                    var carAD = new SqlDataAdapter(car, conn);
                    var carDS = new DataSet();
                    carAD.Fill(carDS);
                    cboOpsti.DataSource = carDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Mesto istovara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    var mu = "select ID, Naziv from MestaUtovara order by Naziv";
                    var muAD = new SqlDataAdapter(mu, conn);
                    var muDS = new DataSet();
                    muAD.Fill(muDS);
                    cboOpsti.DataSource = muDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "ID";

                    break;
                case "Adresa utovara":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;

                    var ko = "select PaKoZapSt, (PaKoOpomba) as Naziv from partnerjiKontOsebaMU order by PaKOIme";
                    var koAD = new SqlDataAdapter(ko, conn);
                    var koDS = new DataSet();
                    koAD.Fill(koDS);
                    cboOpsti.DataSource = koDS.Tables[0];
                    cboOpsti.DisplayMember = "Naziv";
                    cboOpsti.ValueMember = "PaKoZapSt";
                    break;
                case "NTTO robe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Tara kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "BTTO kontejnera":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "BTTO robe":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                case "Koleta":
                    dtpOpsti.Visible = false;
                    cboOpsti.Visible = true;
                    txtOpsti.Visible = false;
                    chkOpsti.Visible = false;
                    nmrOpsti.Visible = false;
                    break;
                default:
                    Console.WriteLine("Nema podatka");
                    break;

            }













            /*

                        string updatestring = "";
                        switch (cboPolje.Text)
                        {
                            case "Naziv broda":
                               
                                break;
                            case "Napomena 1":
                                updatestring = " Update uvoz set Napomena1 = '" + txtNapomena1.Text + "'";
                                break;
                            case "Datum BZ":
                                updatestring = " Update uvoz set DobijeBZ = '" + txtBZ.Text.ToString().TrimEnd() + "'";
                                break;
                            case "PIN":
                                updatestring = " Update uvoz set PIN = '" + txtPIN.Text + "'";
                                break;
                            case "Vrsta kontejnera":
                                updatestring = " Update uvoz set TaraKontejnera = " + Convert.ToInt32(txtTipKont.SelectedValue);
                                break;
                            case "Relacija R\\L\\SRB":
                                updatestring = " Update uvoz set RLTerminali = " + Convert.ToInt32(cboRLTerminal.SelectedValue);
                                break;
                            case "BL":
                                updatestring = " Update uvoz set BrodskaTeretnica = '" + txtTeretnica.Text + "'";
                                break;
                            case "ADR":
                                updatestring = " Update uvoz set ADR = " + Convert.ToInt32(txtADR.SelectedValue);
                                break;
                            case "Brodar":
                                updatestring = " Update uvoz set Brodar = " + Convert.ToInt32(cboBrodar.SelectedValue);
                                break;
                            case "Vlasnik":
                                updatestring = " Update uvoz set Vlasnik = " + Convert.ToInt32(cbVlasnikKont.SelectedValue);
                                break;
                            case "Uvoznik":
                                updatestring = " Update uvoz set Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue);
                                break;
                            case "Nalogodavac 1":
                                updatestring = " Update uvoz set Nalogodavac1 = " + Convert.ToInt32(cboNalogodavac1.SelectedValue);
                                break;
                            case "Ref1":
                                updatestring = " Update uvoz set Ref1 = '" + txtRef1.Text + "'";
                                break;
                            case "Nalogodavac 2":
                                updatestring = " Update uvoz set Nalogodavac2 = " + Convert.ToInt32(cboNalogodavac2.SelectedValue);
                                break;
                            case "Ref2":
                                updatestring = " Update uvoz set Ref2 = '" + txtOpsti.Text + "'";
                                break;
                            case "Nalogodavac3":
                                updatestring = " Update uvoz set Nalogodavac3 = " + Convert.ToInt32(cboNalogodavac3.SelectedValue);
                                break;
                            case "Ref3":
                                updatestring = " Update uvoz set Ref3 = '" + txtRef3.Text + "'";
                                break;
                            case "VrstaPregleda":
                                updatestring = " Update uvoz set VrstaPregleda = '" + Convert.ToInt32(txtVrstaPregleda.SelectedValue) + "'";
                                break;
                            case "Špedicija - RTCLeget":
                                updatestring = " Update uvoz set SpedicijaRTC = " + Convert.ToInt32(cboSpedicijaRTC.SelectedValue);
                                break;
                            case "Špedicija granica":
                                updatestring = " Update uvoz set SpedicijaGranica = " + Convert.ToInt32(cboSpedicijaG.SelectedValue);
                                break;
                            case "Carinski postupak":
                                updatestring = " Update uvoz set CarinskiPostupak = " + Convert.ToInt32(cboCarinskiPostupak.SelectedValue);
                                break;
                            case "Postupak sa robom":
                                updatestring = " Update uvoz set PostupakSaRobom = " + Convert.ToInt32(cbPostupak.SelectedValue);
                                break;
                            case "Način pakovanja":
                                updatestring = " Update uvoz set NacinPakovanja = " + Convert.ToInt32(cbNacinPakovanja.SelectedValue);
                                break;
                            case "Napomena 2":
                                updatestring = " Update uvoz set Napomena2 = " + txtNapomena.Text;
                                break;
                            case "Odredišna špedicija":
                                updatestring = " Update uvoz set OdredisnaSpedicija = " + Convert.ToInt32(cbOspedicija.SelectedValue);
                                break;
                            case "Carinarnica":
                                updatestring = " Update uvoz set OdredisnaCarina = " + Convert.ToInt32(cbOcarina.SelectedValue);
                                break;
                            case "Mesto istovara":
                                updatestring = " Update uvoz set MestoIstovara = '" + Convert.ToInt32(txtMesto.SelectedValue) + "'";
                                break;
                            case "Kontakt osoba":
                                updatestring = " Update uvoz set KontaktOsoba = '" + Convert.ToInt32(txtKontaktOsoba.SelectedValue) + "'";
                                break;
                            case "EMail":
                                updatestring = " Update uvoz set Email = '" + txtMail.Text.ToString() + "'";
                                break;
                            default:
                                Console.WriteLine("Nema podatka");
                                break;

                        }


                        try
                        {
                            // 1. Create Command
                            // Sql Update Statement
                            string updateSql = updatestring + " where ID = " + IdZaPromenu;

                            SqlConnection conn = new SqlConnection(connection);
                            SqlCommand cmd = new SqlCommand(updateSql, conn);
                            conn.Open();
                            var q = cmd.ExecuteNonQuery();
                            conn.Close();


                        }

                        catch (SqlException ex)
                        {
                            // Display error
                            Console.WriteLine("Error: " + ex.ToString());
                        }



                        //FillGV();
                    }
                    private void button7_Click(object sender, EventArgs e)
                    {
                        int IDZaPromenu = 0;
                        try
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Selected)
                                {
                                    IDZaPromenu = Convert.ToInt32(row.Cells[0].Value.ToString());
                                    UpdateVrednostiPolja(IDZaPromenu);
                                }
                            }
                            // FillGV();

                        }
                        catch
                        {
                            MessageBox.Show("Nije uspela selekcija stavki");
                        }
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            string updatestring = "";
            switch (cboPolje.Text)
            {

                case "Naziv broda":
                    updatestring = " Update uvoz set NazivBroda = " + cbBrod.SelectedValue;
                    break;
                case "Napomena 1":
                    updatestring = " Update uvoz set Napomena1 = '" + txtNapomena1.Text + "'";
                    break;
                case "Datum BZ":
                    updatestring = " Update uvoz set DobijeBZ = '" + txtBZ.Text.ToString().TrimEnd() + "'";
                    break;
                case "PIN":
                    updatestring = " Update uvoz set PIN = '" + txtPIN.Text + "'";
                    break;
                case "Vrsta kontejnera":
                    updatestring = " Update uvoz set TaraKontejnera = " + Convert.ToInt32(txtTipKont.SelectedValue);
                    break;
                case "Relacija R\\L\\SRB":
                    updatestring = " Update uvoz set RLTerminali = " + Convert.ToInt32(cboRLTerminal.SelectedValue);
                    break;
                case "BL":
                    updatestring = " Update uvoz set BrodskaTeretnica = '" + txtTeretnica.Text + "'";
                    break;
                case "ADR":
                    updatestring = " Update uvoz set ADR = " + Convert.ToInt32(txtADR.SelectedValue);
                    break;
                case "Brodar":
                    updatestring = " Update uvoz set Brodar = " + Convert.ToInt32(cboBrodar.SelectedValue);
                    break;
                case "Vlasnik":
                    updatestring = " Update uvoz set Vlasnik = " + Convert.ToInt32(cbVlasnikKont.SelectedValue);
                    break;
                case "Uvoznik":
                    updatestring = " Update uvoz set Uvoznik = " + Convert.ToInt32(cboUvoznik.SelectedValue);
                    break;
                case "Nalogodavac 1":
                    updatestring = " Update uvoz set Nalogodavac1 = " + Convert.ToInt32(cboNalogodavac1.SelectedValue);
                    break;
                case "Ref1":
                    updatestring = " Update uvoz set Ref1 = '" + txtRef1.Text + "'";
                    break;
                case "Nalogodavac 2":
                    updatestring = " Update uvoz set Nalogodavac2 = " + Convert.ToInt32(cboNalogodavac2.SelectedValue);
                    break;
                case "Ref2":
                    updatestring = " Update uvoz set Ref2 = '" + txtOpsti.Text + "'";
                    break;
                case "Nalogodavac3":
                    updatestring = " Update uvoz set Nalogodavac3 = " + Convert.ToInt32(cboNalogodavac3.SelectedValue);
                    break;
                case "Ref3":
                    updatestring = " Update uvoz set Ref3 = '" + txtRef3.Text + "'";
                    break;
                case "VrstaPregleda":
                    updatestring = " Update uvoz set VrstaPregleda = '" + Convert.ToInt32(txtVrstaPregleda.SelectedValue) + "'";
                    break;
                case "Špedicija - RTCLeget":
                    updatestring = " Update uvoz set SpedicijaRTC = " + Convert.ToInt32(cboSpedicijaRTC.SelectedValue);
                    break;
                case "Špedicija granica":
                    updatestring = " Update uvoz set SpedicijaGranica = " + Convert.ToInt32(cboSpedicijaG.SelectedValue);
                    break;
                case "Carinski postupak":
                    updatestring = " Update uvoz set CarinskiPostupak = " + Convert.ToInt32(cboCarinskiPostupak.SelectedValue);
                    break;
                case "Postupak sa robom":
                    updatestring = " Update uvoz set PostupakSaRobom = " + Convert.ToInt32(cbPostupak.SelectedValue);
                    break;
                case "Način pakovanja":
                    updatestring = " Update uvoz set NacinPakovanja = " + Convert.ToInt32(cbNacinPakovanja.SelectedValue);
                    break;
                case "Napomena 2":
                    updatestring = " Update uvoz set Napomena2 = " + txtNapomena.Text;
                    break;
                case "Odredišna špedicija":
                    updatestring = " Update uvoz set OdredisnaSpedicija = " + Convert.ToInt32(cbOspedicija.SelectedValue);
                    break;
                case "Carinarnica":
                    updatestring = " Update uvoz set OdredisnaCarina = " + Convert.ToInt32(cbOcarina.SelectedValue);
                    break;
                case "Mesto istovara":
                    updatestring = " Update uvoz set MestoIstovara = '" + Convert.ToInt32(txtMesto.SelectedValue) + "'";
                    break;
                case "Kontakt osoba":
                    updatestring = " Update uvoz set KontaktOsoba = '" + Convert.ToInt32(txtKontaktOsoba.SelectedValue) + "'";
                    break;
                case "EMail":
                    updatestring = " Update uvoz set Email = '" + txtMail.Text.ToString() + "'";
                    break;
                default:
                    Console.WriteLine("Nema podatka");
                    break;
            
            }
            */
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                int IDZaPromenu = 0;
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    IDZaPromenu = Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString());
                    UpdateVrednostiPolja(IDZaPromenu);



                   // Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), Convert.ToInt32(cboPlanUtovara.SelectedValue));
                    //To get the cell value of particular column of selected records   
                    //  string cellValue = selectedRecord.Record.GetValue("ID").ToString();
                    // MessageBox.Show(cellValue);
                }
            }
        }
    }
}
