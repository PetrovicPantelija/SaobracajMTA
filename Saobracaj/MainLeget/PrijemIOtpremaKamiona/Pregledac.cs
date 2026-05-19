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

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    public partial class Pregledac : Form
    {
        public Pregledac()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var select = "";

            select = "    select RadniNalogInterni.ID as KomNalogID, 'Izvoz' as Izvor, 'DOLAZAK' as Smer,  KorisnikIzdao, IZvozKonacna.OpisPosla, IZvozKonacna.BrojKontejnera, " +
" TipKontenjera.SkNaziv as VrstaKontejnera, " +
" (Select Top 1 Scenario.Naziv from Scenario where Scenario.ID = IzvozKonacna.Scenario) as SC,  " +
" CASE Drumski  WHEN 0 THEN ' '  WHEN 1 THEN 'L'  END AS Drumski,  " +
" CASE Cirada " +
" WHEN 0 THEN 'PLATFORMA' " +
" WHEN 1 THEN 'CIRADA' " +
" END AS TipNaloga, IzvozKonacna.ID AS IzvozID, " +
" Partnerji.PANaziv as Brodar, IzvozKonacna.Tara, p1.PaNaziv as VlasnikBrodskaPlomba, BrodskaPlomba as BrojBrodskePlombe, OstalePlombe , PlaniranDtSpustanjaPunog as PlaniraniDatum, PlaniraniDtSpustanjaKontejnera as NoviDatum, Kapija.DatumDolaska as KapijaDolazak, BrojStavkePorudzbenice, KapijaUlaz from RadniNalogInterni " +
" inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
" inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
" inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
" inner join Partnerji on PArtnerji.PaSifra = IzvozKonacna.Brodar " +
" inner join Partnerji p1 on p1.PaSifra = IzvozKonacna.Brodar " +
" inner join Kapija on Kapija.NalogID = RadniNalogInterni.ID " +
" where Pregledac = 0 ";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            this.gridGroupingControl2.Table.Records.DeleteAll();

            gridGroupingControl2.DataSource = ds.Tables[0];
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("IzvozID");
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[Kapija] = '1'";
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfd3);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
        }

        int VratiRN(int NajavaID)
        {
            int pom = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" select ID from RNPrijemPlatforme where NalogID = " + Convert.ToInt32(NajavaID), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               pom = Convert.ToInt32(dr["ID"].ToString());

            }
            con.Close();

            return pom;


        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Kor = Sifarnici.frmLogovanje.user;


            InsertRN up = new InsertRN();

            if (this.gridGroupingControl2.Table.SelectedRecords.Count > 0)
            {
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
                {
                    int BrojRN= VratiRN(Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()));
                   up.UpdateRN4UradjeneVizuelni(BrojRN, Kor);
                  
                }
            }



          
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl2.Table.SelectedRecords.Count > 0)
            {
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
                {
                    ZapisnikONepravilnosti zon = new ZapisnikONepravilnosti(selectedRecord.Record.GetValue("KomNalogID").ToString());
                    zon.Show();

                }
            }

         
        }
        int VratiPrijemID(int NajavaID)
        {
            int pom = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select ID from PrijemKontejneraVozStavke where NAjavaID = " + Convert.ToInt32(NajavaID), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pom = Convert.ToInt32(dr["ID"].ToString());

            }
            con.Close();

            return pom;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Kor = Sifarnici.frmLogovanje.user;
            string NalogID = "";
            if (this.gridGroupingControl2.Table.SelectedRecords.Count > 0)
            {  
         
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl2.Table.SelectedRecords)
                {
                   InsertRN rn = new InsertRN();
                        NalogID = selectedRecord.Record.GetValue("KomNalogID").ToString();
                    rn.UpdateRN4PotrebanCIR(Convert.ToInt32(selectedRecord.Record.GetValue("KomNalogID").ToString()), Kor);

                }
            }
            DialogResult dialogResult = MessageBox.Show("Da li želite da napravite CIR u app?", "Izraditi CIR", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int BrojRN = VratiRN(Convert.ToInt32(NalogID));
                int PrijemID = VratiPrijemID(Convert.ToInt32(NalogID));
                frmCIR cir = new frmCIR(PrijemID,1,"RN4", BrojRN, Convert.ToInt32(NalogID));
                cir.Show();
            }
           
           
        }

        private void toolStripMenuItemOtvori_Click(object sender, EventArgs e)
        {
            if (gridGroupingControl2.Table.SelectedRecords.Count > 0)
            {
                // Uzimamo prvi selektovani red
                Record rec = gridGroupingControl2.Table.SelectedRecords[0].Record;

                if (rec != null)
                {
                    int ID = Convert.ToInt32(rec.GetValue("IzvozID"));
                    string Izvor = (rec.GetValue("Izvor").ToString());
                    if (Izvor == "Izvoz")
                    {
                        PrijemIOtpremaKamiona.frmPrijemKamionaDetalji sc1 = new PrijemIOtpremaKamiona.frmPrijemKamionaDetalji(ID);
                        sc1.Show();
                    }
                }
            }
        }

        private void GridGroupingControl1_TableControlMouseDown(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlMouseEventArgs e)
        {

            if (System.Windows.Forms.Control.MouseButtons == MouseButtons.Right)
            {
                // Dobavljanje pozicije kliknutog reda i kolone
                // Pronađi red i kolonu pod mišem
                int rowIndex, colIndex;
                e.TableControl.PointToRowCol(new System.Drawing.Point(e.Inner.X, e.Inner.Y), out rowIndex, out colIndex);

                // Uzmi stil kliknutog polja
                GridTableCellStyleInfo style = e.TableControl.GetTableViewStyleInfo(rowIndex, colIndex);

                // Proveri da li je kliknuto u redu sa podacima
                if (style.TableCellIdentity.DisplayElement.Kind == DisplayElementKind.Record)
                {
                    // Postavi aktivni red
                    this.gridGroupingControl2.Table.CurrentRecord = style.TableCellIdentity.DisplayElement.ParentRecord;

                    // Prikaži context menu na poziciji miša
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

    }
}
