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
        private void UpdateVrednostiPolja(int IdZaPromenu)
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
    }
}
