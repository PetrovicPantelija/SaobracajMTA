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

namespace Saobracaj.RadniNalozi
{
    public partial class frmAnalizaRadnihNaloga :  Syncfusion.Windows.Forms.Office2010Form
    {
        public frmAnalizaRadnihNaloga()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = "";
            select = "   select ID, TipRN, IDRadnogNaloga, IDUsluge, BrojKontejnera, Uradjen from TerminalRadniNalozi";
           
            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "select TerminalRadniNalozi.BrojKontejnera, RNPrijemVoza.Zavrsen , TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis, RNPrijemVoza.NaPozicijuSklad ,p1.PaNaziv as Brodar, UvozKonacna.BukingBrodara,  Voz.BrVoza,Voz.Relacija, TerminalRadniNalozi.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
" PArtnerji.PaNaziv as Uvoznik, RNPrijemVoza.DatumRasporeda, RNPrijemVoza.NalogIzdao, " +
" RNPrijemVoza.DatumRealizacije, RNPrijemVoza.NalogRealizovao, RNPrijemVoza.Napomena as RNNapomena, Zavrsen " +
" , p2.PaNaziv as NalogodavacZaVoz, p3.PaNaziv as NalogavacUsluge,  p4.PaNaziv as NalogavacDrumski, UvozKonacna.BrojPlombe1, UvozKonacna.BrojPlombe2, UvozKonacna.Napomena, UvozKonacna.Napomena1 " +
" from TerminalRadniNalozi " +
" inner join RNPrijemVoza on RNPrijemVoza.ID = TerminalRadniNalozi.IDRadnogNaloga " +
" inner join Skladista on Skladista.ID = RNPrijemVoza.NaSkladiste " +
" inner join Pozicija on Pozicija.ID = RNPrijemVoza.NaPozicijuSklad " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
" inner join PrijemKontejneraVoz on RNPrijemVoza.PrijemID = PrijemKontejneraVoz.ID " +
" inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.ID " +
" inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.BrojKontejnera = TerminalRadniNalozi.BrojKontejnera " +
" inner join UvozKonacna on UvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID " +
" inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
" inner join PArtnerji On PArtnerji.PaSifra = UvozKonacna.Uvoznik " +
" inner join PArtnerji as P1 On P1.PaSifra = UvozKonacna.Brodar " +
" inner join PArtnerji as P2 On P2.PaSifra = UvozKonacna.Nalogodavac1 " +
" inner join PArtnerji as P3 On P3.PaSifra = UvozKonacna.Nalogodavac2 " +
" inner join PArtnerji as P4 On P4.PaSifra = UvozKonacna.Nalogodavac3" +
" where TerminalRadniNalozi.TIPRN = '1-Prijem kontejnera VOZ'";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "select TerminalRadniNalozi.BrojKontejnera, TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis, RNPrijemVoza.NaPozicijuSklad ,p1.PaNaziv as Brodar, UvozKonacna.BukingBrodara,  Voz.BrVoza,Voz.Relacija, TerminalRadniNalozi.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
" PArtnerji.PaNaziv as Uvoznik, RNPrijemVoza.DatumRasporeda, RNPrijemVoza.NalogIzdao, " +
" RNPrijemVoza.DatumRealizacije, RNPrijemVoza.NalogRealizovao, RNPrijemVoza.Napomena as RNNapomena, Uradjen " +
" , p2.PaNaziv as NalogodavacZaVoz, p3.PaNaziv as NalogavacUsluge,  p4.PaNaziv as NalogavacDrumski, UvozKonacna.BrojPlombe1, UvozKonacna.BrojPlombe2, UvozKonacna.Napomena, UvozKonacna.Napomena1 " +
" from TerminalRadniNalozi " +
" inner join RNPrijemVoza on RNPrijemVoza.ID = TerminalRadniNalozi.IDRadnogNaloga " +
" inner join Skladista on Skladista.ID = RNPrijemVoza.NaSkladiste " +
" inner join Pozicija on Pozicija.ID = RNPrijemVoza.NaPozicijuSklad " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
" inner join PrijemKontejneraVoz on RNPrijemVoza.PrijemID = PrijemKontejneraVoz.ID " +
" inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.ID " +
" inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.BrojKontejnera = TerminalRadniNalozi.BrojKontejnera " +
" inner join UvozKonacna on UvozKonacna.ID = PrijemKontejneraVozStavke.KontejnerID " +
" inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
" inner join PArtnerji On PArtnerji.PaSifra = UvozKonacna.Uvoznik " +
" inner join PArtnerji as P1 On P1.PaSifra = UvozKonacna.Brodar " +
" inner join PArtnerji as P2 On P2.PaSifra = UvozKonacna.Nalogodavac1 " +
" inner join PArtnerji as P3 On P3.PaSifra = UvozKonacna.Nalogodavac2 " +
" inner join PArtnerji as P4 On P4.PaSifra = UvozKonacna.Nalogodavac3" +
" where TerminalRadniNalozi.TIPRN = '2-Prijem Voz pretovar'";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "select TerminalRadniNalozi.BrojKontejnera, TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis, Pozicija.Opis as Pozicija ,p1.PaNaziv as Brodar, IzvozKonacna.BookingBrodara,  Voz.BrVoza,Voz.Relacija, TerminalRadniNalozi.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
 " PArtnerji.PaNaziv as Izvoznik, RNOtpremaVoza.DatumRasporeda, RNOtpremaVoza.NalogIzdao, " +
" RNOtpremaVoza.DatumRealizacije, RNOtpremaVoza.NalogRealizovao, RNOtpremaVoza.Napomena as RNNapomena, Uradjen " +
" , p2.PaNaziv as NalogodavacZaVoz, p3.PaNaziv as NalogavacUsluge,  p4.PaNaziv as NalogavacDrumski, IzvozKonacna.OstalePlombe, IzvozKonacna.BrodskaPlomba, IzvozKonacna.NapomenaZaRobu " +
" from TerminalRadniNalozi " +
"  inner join RNOtpremaVoza on RNOtpremaVoza.ID = TerminalRadniNalozi.IDRadnogNaloga " +
"  inner join Skladista on Skladista.ID = RNOtpremaVoza.SaSkladista " +
"  inner join Pozicija on Pozicija.ID = RNOtpremaVoza.SaPozicijeSklad " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
" inner join OtpremaKontejnera on RNOtpremaVoza.OtpremaID = OtpremaKontejnera.ID " +
" inner join Voz on OtpremaKontejnera.IdVoza = Voz.ID " +
"  inner join OtpremaKontejneraVozStavke on OtpremaKontejneraVozStavke.BrojKontejnera = TerminalRadniNalozi.BrojKontejnera " +
"  inner join IzvozKonacna on IzvozKonacna.ID = OtpremaKontejneraVozStavke.KontejnerID " +
"  inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
"  inner join PArtnerji On PArtnerji.PaSifra = IzvozKonacna.Izvoznik " +
"  inner join PArtnerji as P1 On P1.PaSifra = IzvozKonacna.Brodar " +
"  inner join PArtnerji as P2 On P2.PaSifra = IzvozKonacna.Klijent1 " +
"  inner join PArtnerji as P3 On P3.PaSifra = IzvozKonacna.Klijent2 " +
"  inner join PArtnerji as P4 On P4.PaSifra = IzvozKonacna.Klijent3 " +
"  where TerminalRadniNalozi.TIPRN = '3-Otprema voza - IZ'";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "select TerminalRadniNalozi.BrojKontejnera, TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis,  " + 
" p1.PaNaziv as Brodar, Izvoz.BookingBrodara,  RNPrijemPlatforme.Kamion, TerminalRadniNalozi.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
" PArtnerji.PaNaziv as Uvoznik, RNPrijemPlatforme.DatumRasporeda, RNPrijemPlatforme.NalogIzdao, " +
 " RNPrijemPlatforme.DatumRealizacije, RNPrijemPlatforme.NalogRealizovao, RNPrijemPlatforme.Napomena as RNNapomena, Uradjen " +
" , p2.PaNaziv as NalogodavacZaVoz, p3.PaNaziv as NalogavacUsluge,  p4.PaNaziv as NalogavacDrumski, Izvoz.BrodskaPlomba, Izvoz.OstalePlombe, " +
" Izvoz.NapomenaZaRobu, Izvoz.DodatneNapomeneDrumski " +
 " from TerminalRadniNalozi " +
" inner join RNPrijemPlatforme on RNPrijemPlatforme.ID = TerminalRadniNalozi.IDRadnogNaloga " +
" inner join Skladista on Skladista.ID = RNPrijemPlatforme.USkladiste " +
" inner join Pozicija on Pozicija.ID = RNPrijemPlatforme.UPozicijaSklad " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
" inner join PrijemKontejneraVoz on RNPrijemPlatforme.PrijemID = PrijemKontejneraVoz.ID " +
" inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.BrojKontejnera = TerminalRadniNalozi.BrojKontejnera " +
" inner join Izvoz on Izvoz.ID = PrijemKontejneraVozStavke.KontejnerID " +
" inner join TipKontenjera on TipKontenjera.ID = Izvoz.VrstaKontejnera " +
" inner join PArtnerji On PArtnerji.PaSifra = Izvoz.Izvoznik " +
" inner join PArtnerji as P1 On P1.PaSifra = Izvoz.Brodar" +
" inner join PArtnerji as P2 On P2.PaSifra = Izvoz.Klijent1 " +
" inner join PArtnerji as P3 On P3.PaSifra = Izvoz.Klijent2 " +
" inner join PArtnerji as P4 On P4.PaSifra = Izvoz.Klijent3 " +
" where TerminalRadniNalozi.TIPRN = '4-Prijem platforma kam IZ'";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "select TerminalRadniNalozi.BrojKontejnera, TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis, Pozicija.Opis as Pozicija ,p1.PaNaziv as Brodar, " +
" UvozKonacna.BukingBrodara,   RNOtpremaPlatforme.Kamion, RNOtpremaPlatforme.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
" PArtnerji.PaNaziv as Izvoznik, RNOtpremaPlatforme.DatumRasporeda, RNOtpremaPlatforme.NalogIzdao, " +
 " RNOtpremaPlatforme.DatumRealizacije, RNOtpremaPlatforme.NalogRealizovao,  Uradjen " +
 " , p2.PaNaziv as NalogodavacZaVoz, p3.PaNaziv as NalogavacUsluge,  p4.PaNaziv as NalogavacDrumski, UvozKonacna.CarinskiPostupak, UvozKonacna.DirigacijaKontejeraZa " +
 " from TerminalRadniNalozi " +
 "  inner join RNOtpremaPlatforme on RNOtpremaPlatforme.ID = TerminalRadniNalozi.IDRadnogNaloga " +
 " inner join Skladista on Skladista.ID = RNOtpremaPlatforme.SaSkladista " +
 "  inner join Pozicija on Pozicija.ID = RNOtpremaPlatforme.SaPozicijeSklad " +
 " inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
 " inner join OtpremaKontejnera on RNOtpremaPlatforme.OtpremaID = OtpremaKontejnera.ID " +
  " inner join OtpremaKontejneraVozStavke on OtpremaKontejneraVozStavke.BrojKontejnera = TerminalRadniNalozi.BrojKontejnera " +
  " inner join UvozKonacna on UvozKonacna.ID = OtpremaKontejneraVozStavke.KontejnerID " +
  " inner join PArtnerji On PArtnerji.PaSifra = UvozKonacna.Uvoznik " +
  " inner join PArtnerji as P1 On P1.PaSifra = UvozKonacna.Brodar " +
  " inner join PArtnerji as P2 On P2.PaSifra = UvozKonacna.Nalogodavac1 " +
  " inner join PArtnerji as P3 On P3.PaSifra = UvozKonacna.Nalogodavac1 " +
  " inner join PArtnerji as P4 On P4.PaSifra = UvozKonacna.Nalogodavac1 " +
  " left JOIN TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
 " where TerminalRadniNalozi.TIPRN = '6-Otprema Platforme UV'";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "select TerminalRadniNalozi.BrojKontejnera, TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis, Pozicija.Opis as Pozicija , " +
  " RNOtpremaPlatforme2.Kamion, RNOtpremaPlatforme2.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
 " RNOtpremaPlatforme2.DatumRasporeda, RNOtpremaPlatforme2.NalogIzdao, " +
"  RNOtpremaPlatforme2.DatumRealizacije, RNOtpremaPlatforme2.NalogRealizovao,  Uradjen " +
 " from TerminalRadniNalozi " +
 "  inner join RNOtpremaPlatforme2 on RNOtpremaPlatforme2.ID = TerminalRadniNalozi.IDRadnogNaloga " +
 " inner join Skladista on Skladista.ID = RNOtpremaPlatforme2.SaSkladista " +
 "  inner join Pozicija on Pozicija.ID = RNOtpremaPlatforme2.SaPozicijeSklad " +
 " inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
 " inner join OtpremaKontejnera on RNOtpremaPlatforme2.OtpremaID = OtpremaKontejnera.ID " +
 "  inner join OtpremaKontejneraVozStavke on OtpremaKontejneraVozStavke.BrojKontejnera = TerminalRadniNalozi.BrojKontejnera " +
 "  left JOIN TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
 " where TerminalRadniNalozi.TIPRN = '7-Otprema Platforme BRODAR'";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "select TerminalRadniNalozi.BrojKontejnera, TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis, Pozicija.Opis as Pozicija , "+
" RNOtpremaCirade.Kamion, RNOtpremaCirade.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
" RNOtpremaCirade.DatumRasporeda, RNOtpremaCirade.NalogIzdao, " +
" RNOtpremaCirade.DatumRealizacije, RNOtpremaCirade.NalogRealizovao,  Uradjen " +
" from TerminalRadniNalozi " +
" inner join RNOtpremaCirade on RNOtpremaCirade.ID = TerminalRadniNalozi.IDRadnogNaloga " +
" inner join Skladista on Skladista.ID = RNOtpremaCirade.SaSkladista " +
" inner join Pozicija on Pozicija.ID = RNOtpremaCirade.SaPozicijeSklad " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
" inner join OtpremaKontejnera on RNOtpremaCirade.OtpremaID = OtpremaKontejnera.ID " +
" inner join OtpremaKontejneraVozStavke on OtpremaKontejneraVozStavke.BrojKontejnera = TerminalRadniNalozi.BrojKontejnera " +
" left JOIN TipKontenjera ON OtpremaKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
" where TerminalRadniNalozi.TIPRN = '8-Otprema Cirade'";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "select TerminalRadniNalozi.BrojKontejnera, TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis, Pozicija.Opis as Pozicija , " +
" RNPrijemCirade.Kamion, RNPrijemCirade.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
" RNPrijemCirade.DatumRasporeda, RNPrijemCirade.NalogIzdao, " +
" RNPrijemCirade.DatumRealizacije, RNPrijemCirade.NalogRealizovao,  Uradjen " +
" from TerminalRadniNalozi " +
" inner join RNPrijemCirade on RNPrijemCirade.ID = TerminalRadniNalozi.IDRadnogNaloga " +
" inner join Skladista on Skladista.ID = RNPrijemCirade.SaSkladista " +
" inner join Pozicija on Pozicija.ID = RNPrijemCirade.SaPozicijeSklad " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
" inner join PrijemKontejneraVoz on RNPrijemCirade.PrijemID = PrijemKontejneraVoz.ID " +
" inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.ID " +
" Inner JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
" where TerminalRadniNalozi.TIPRN = '9-Prijem Cirade'";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {

            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = "select TerminalRadniNalozi.BrojKontejnera, TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis, Pozicija.Opis as Pozicija , " +
" RNPregledIspraznjenogKontejnera.Kamion, RNPregledIspraznjenogKontejnera.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
" RNPregledIspraznjenogKontejnera.DatumRasporeda, RNPregledIspraznjenogKontejnera.NalogIzdao, " +
" RNPregledIspraznjenogKontejnera.DatumRealizacije, RNPregledIspraznjenogKontejnera.NalogRealizovao,  Uradjen " +
" from TerminalRadniNalozi " +
" inner join RNPregledIspraznjenogKontejnera on RNPregledIspraznjenogKontejnera.ID = TerminalRadniNalozi.IDRadnogNaloga " +
" inner join Skladista on Skladista.ID = RNPregledIspraznjenogKontejnera.SaSkladista " +
" inner join Pozicija on Pozicija.ID = RNPregledIspraznjenogKontejnera.SaPozicijeSklad " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
" inner join PrijemKontejneraVoz on RNPrijemCirade.PrijemID = PrijemKontejneraVoz.ID " +
" inner join PrijemKontejneraVozStavke on PrijemKontejneraVozStavke.IDNadredjenog = PrijemKontejneraVoz.ID " +
" Inner JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
" where TerminalRadniNalozi.TIPRN = '10-PREGLED ISPRAŽNJENOG KONTEJNERA'";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            gridGroupingControl2.DataSource = null;
            gridGroupingControl2.ResetTableDescriptor();
            gridGroupingControl2.Refresh();
            var select = "";
            select = " select TerminalRadniNalozi.BrojKontejnera, TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv, Pozicija.Opis, Pozicija.Opis as Pozicija , " +
" RNPregledIpostavkaKontejnera.Kamion, RNPregledIpostavkaKontejnera.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
" RNPregledIpostavkaKontejnera.DatumRasporeda, RNPregledIpostavkaKontejnera.NalogIzdao, " +
" RNPregledIpostavkaKontejnera.DatumRealizacije, RNPregledIpostavkaKontejnera.NalogRealizovao,  Uradjen " +
" from TerminalRadniNalozi " +
" inner join RNPregledIpostavkaKontejnera on RNPregledIpostavkaKontejnera.ID = TerminalRadniNalozi.IDRadnogNaloga " +
" inner join Skladista on Skladista.ID = RNPregledIpostavkaKontejnera.SaSkladista " +
" inner join Pozicija on Pozicija.ID = RNPregledIpostavkaKontejnera.SaPozicijeSklad " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
" inner join OtpremaKOntejnera on RNPregledIpostavkaKontejnera.PrijemID = OtpremaKontejnera.ID " +
" inner join OtpremaKontejneraVozStavke on OtpremaKontejneraVozStavke.IDNadredjenog = OtpremaKontejneraVoz.ID " +
" Inner JOIN TipKontenjera ON PrijemKontejneraVozStavke.TipKontejnera = TipKontenjera.ID " +
" where TerminalRadniNalozi.TIPRN = '11-PREGLED I POSTAVKA  KONTEJNERA  ' ";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            gridGroupingControl1.DataSource = null;
            gridGroupingControl1.ResetTableDescriptor();
            gridGroupingControl1.Refresh();
            var select = "";
            select = "   select TerminalRadniNalozi.BrojKontejnera, KontejnerTekuce.Pokret, KontejnerStatus.Naziv as TrenutniStatus, s1.Naziv as TrenutnoSkladiste, " +
" KontejnerTekuce.Ostecenja as Ispravnost,   TipKontenjera.Naziv as TipKontejnera, Skladista.Naziv as SkladisteRN, Pozicija.Opis as PozicijaRN,  " +
" RNPrijemVoza.NaPozicijuSklad ,PrijemKontejneraVoz.VremeDolaska as ETADolaska, PrijemKontejneraVoz.VremeDolaska as ATADolaska,  " +
" UvozKonacna.Prioritet, UvozKonacna.AtaOtpreme, KontejnerskiTerminali.Naziv as RL_SRBTERMINAL, p1.PaNaziv as Brodar, " +
" UvozKonacna.BukingBrodara,  Voz.BrVoza,Voz.Relacija, TerminalRadniNalozi.IDUsluge, VrstaManipulacije.Naziv as NAzivUSluge, " +
" PArtnerji.PaNaziv as Uvoznik,   p2.PaNaziv as NalogodavacZaVoz, p3.PaNaziv as NalogavacUsluge,  p4.PaNaziv as NalogavacDrumski, " +
" UvozKonacna.BrojPlombe1, UvozKonacna.BrojPlombe2, UvozKonacna.Napomena,  UvozKonacna.Napomena1 from TerminalRadniNalozi " +
" inner join RNPrijemVoza on RNPrijemVoza.ID = TerminalRadniNalozi.IDRadnogNaloga " +
" inner join Skladista on Skladista.ID = RNPrijemVoza.NaSkladiste " +
" inner join Pozicija on Pozicija.ID = RNPrijemVoza.NaPozicijuSklad " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = TerminalRadniNalozi.IDUsluge " +
" inner join PrijemKontejneraVoz on RNPrijemVoza.PrijemID = PrijemKontejneraVoz.ID " +
" inner join Voz on PrijemKontejneraVoz.IdVoza = Voz.ID " +
" inner join UvozKonacna on UvozKonacna.BrojKontejnera = RNPrijemVoza.BrojKontejnera " +
" inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
" inner join PArtnerji On PArtnerji.PaSifra = UvozKonacna.Uvoznik " +
" inner join PArtnerji as P1 On P1.PaSifra = UvozKonacna.Brodar " +
" inner join PArtnerji as P2 On P2.PaSifra = UvozKonacna.Nalogodavac1 " +
" inner join PArtnerji as P3 On P3.PaSifra = UvozKonacna.Nalogodavac2 " +
" inner join PArtnerji as P4 On P4.PaSifra = UvozKonacna.Nalogodavac2 " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.id = UvozKonacna.RLTerminali " +
" inner join KontejnerTekuce on KontejnerTekuce.Kontejner = UvozKonacna.BrojKontejnera " +
" inner join KontejnerStatus on KontejnerStatus.ID = KontejnerTekuce.StatusKontejnera " +
" inner join Skladista s1 on s1.Id = KontejnerTekuce.Skladiste  inner join Pozicija po1 on po1.Id = KontejnerTekuce.Pozicija " +
" where TerminalRadniNalozi.TIPRN = '1-Prijem kontejnera VOZ' and PrijemKontejneraVoz.Vozom = 1";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.TESTIRANJEConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }
    }
}
