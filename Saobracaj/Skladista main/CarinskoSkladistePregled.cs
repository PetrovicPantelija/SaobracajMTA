using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Org.BouncyCastle.Math;
using Saobracaj.Drumski;
using Saobracaj.Skladista;
using Saobracaj.Skladista_main.Dokumenta;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main
{
    public partial class CarinskoSkladistePregled : Form
    {
        string Tip;
        public CarinskoSkladistePregled(string tip)
        {
            InitializeComponent();
            Tip = tip;
            VratiRN();
            InitOtprema();   // RAZ-11: novi nalog otpreme (CarinskoSkladistePregled.Otprema.cs)
            PrimeniSava();   // redizajn Sava (CarinskoSkladistePregled.Sava.cs)
        }

        // RAZ-11: nalog može nastati bez komercijalnog naloga (Radni nalozi > Kreiraj novi > Carinsko skladište > Prijem,
        // ili Novi nalog otpreme sa ove liste). Takav nalog nema interni nalog RN20 (usluga 74) ni potvrdu LO, a ostaje
        // Formiran = 0, jer se forma bez RN20 ne može formirati. Zato se interni nalog i nalogodavac spajaju sa LEFT JOIN,
        // prikazuju se i neformirani nalozi, a kolone Formiran i Izvor (Komercijalni / Direktni) pokazuju stanje.
        // Komercijalni nalozi (Aktiviraj u Radnim nalozima) i dalje dolaze sa RN20 i Formiran = 1.
        private string UpitListe()
        {
            string tip = tipListe ?? Tip;   // Tip menja button24_Click (TipRN izabranog reda)
            return " Select RadniNalogSkladista.ID,RadniNalogSkladista.Datum as Datum,Korisnik,VrstaRN,TipRN,CarinskoSkladiste, " +
               "  RTRIM(p1.PaNaziv) as Nalogodavac,RTrim(vlr.Naziv) as VlasnikRobe, " +
            " OpisPosla,Napomena,Aktivan, " +
            " CASE WHEN RadniNalogSkladista.Formiran = 1 THEN 'Da' ELSE 'Ne' END as Formiran, " +
            " CASE WHEN t1.BrojRN IS NULL THEN 'Direktni' ELSE 'Komercijalni' END as Izvor, " +
            " t1.LO, t1.Prijem, t1.Viljuskarista " +
            " from RadniNalogSkladista " +
            " left join Partnerji as p1 on RadniNalogSkladista.Nalogodavac = p1.PaSifra " +
            " left join (SELECT id, Naziv FROM VlasniciRobe) as vlr on RadniNalogSkladista.VlasnikRobe = vlr.id " +   // RAZ-06: samo id i Naziv, da ne bude dvosmislenih kolona
            " left join(SELECT radninaloginterni.brojrn, RadniNalogInterniPotvrda.LOSkladiste as lo,RadniNalogInterniPotvrda.PrijemSklediste as prijem, RadniNalogInterniPotvrda.ViljuskaristaSkladiste as viljuskarista " +
                            "    FROM radninaloginterni " +
                            "     INNER JOIN RadniNalogInterniPotvrda    ON RadniNalogInterniPotvrda.idnaloga = radninaloginterni.id   WHERE radninaloginterni.tiprn = 'RN20' AND IDManipulacijaJED = 74) as T1 " +
                            " on RadniNalogSkladista.ID = t1.BrojRN " +
                            " WHere TipRN = '" + tip + "' and VrstaRN = 'Carinsko' order by RadniNalogSkladista.ID desc   ";
        }

        private void VratiRN()
        {
            var select = UpitListe();

            //var select = " Select ID,RadniNalogSkladista.Datum as Datum,Korisnik,VrstaRN,TipRN,CarinskoSkladiste, " +
            //   "  RTRIM(p1.PaNaziv) as Nalogodavac,RTrim(p2.PaNaziv) as VlasnikRobe, " +
            //" OpisPosla,Napomena,Aktivan, t1.LO, t1.Prijem, t1.Viljuskarista " +
            //" from RadniNalogSkladista " +
            //" inner join Partnerji as p1 on RadniNalogSkladista.Nalogodavac = p1.PaSifra " +
            //" left join Partnerji as p2 on RadniNalogSkladista.VlasnikRobe = p2.PaSifra " +
            //" inner join(SELECT radninaloginterni.brojrn, RadniNalogInterniPotvrda.LOSkladiste as lo,RadniNalogInterniPotvrda.PrijemSklediste as prijem, RadniNalogInterniPotvrda.ViljuskaristaSkladiste as viljuskarista " +
            //                "    FROM radninaloginterni " +
            //                "     INNER JOIN RadniNalogInterniPotvrda    ON RadniNalogInterniPotvrda.idnaloga = radninaloginterni.id   WHERE radninaloginterni.tiprn = 'RN20' AND IDManipulacijaJED = 74) as T1 " +
            //                " on RadniNalogSkladista.ID = t1.BrojRN " +
            //                " WHere TipRN = '" + Tip + "' and Formiran = 1 and VrstaRN = 'Carinsko' order by ID desc   ";


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
        int ID;
        string Vrsta;
        string Ulaz;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private void button24_Click(object sender, EventArgs e)
        {
            if (gridGroupingControl1.Table.CurrentRecord == null)   // ISP-10
            {
                MessageBox.Show("Izaberite nalog u tabeli.");
                return;
            }
            if (gridGroupingControl1.Table.CurrentRecord != null)
            {
                ID = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString());
                Vrsta = gridGroupingControl1.Table.CurrentRecord.GetValue("VrstaRN").ToString();
                Tip = gridGroupingControl1.Table.CurrentRecord.GetValue("TipRN").ToString();
                var main = this.TopLevelControl as NewMain;
                if (main == null) return;

                // RAZ-11: nalog bez internog naloga RN20 je direktni; bez ovoga Ulaz ostaje od prethodnog reda (ili prazan),
                // pa forma naloga ne podešava polja (podesiPolja radi samo za Direktni i Izvoz)
                Ulaz = "Direktni";
                using(SqlConnection conn=new SqlConnection(connection))
                {
                    using(SqlCommand cmd=new SqlCommand("Select OJIzdavanja From RadniNalogInterni Where TipRN='RN20' and IDManipulacijaJED = 74 and BrojRN=" + ID, conn))
                    {
                        conn.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (Convert.ToInt32(reader[0].ToString()) == 1)
                                {
                                    Ulaz = "Uvoz";
                                }
                                if (Convert.ToInt32(reader[0].ToString()) == 2)
                                {
                                    Ulaz = "Izvoz";
                                }
                                if (Convert.ToInt32(reader[0].ToString()) == 7)
                                {
                                    Ulaz = "Direktni";
                                }
                            }
                        }
                    }
                }

                if (Tip == "Prijem")
                {
                    // main.OtvoriFormuBezPrava(() => new Prijem(ID,Ulaz, Vrsta));
                    main.OtvoriFormuBezPrava(() => new frmNalogPrijem("LO", Tip, Vrsta, Ulaz,ID, null));
                }
                if (Tip == "Otprema")
                {
                    main.OtvoriFormuBezPrava(() => new Otprema(ID, Ulaz, Vrsta));
                }
            }
        }

        private void btnViljuskarista_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;
            if (gridGroupingControl1.Table.CurrentRecord == null)   // ISP-10
            {
                MessageBox.Show("Izaberite nalog u tabeli.");
                return;
            }
            ID = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString());
            main.OtvoriFormuBezPrava(() => new ViljuskaristiPregled(ID));
            //Bio nalog viljuskaristipregled
        }

        private void button25_Click(object sender, EventArgs e)
        {
            /*
            if (Aktivan == 0)
            {
                MessageBox.Show("RN nije aktivan!");
                return;
            }

            if (Formiran == 0)
            {
                MessageBox.Show("RN nije formiran!");
                return;
            }
            */
            if (gridGroupingControl1.Table.CurrentRecord == null)   // ISP-10
            {
                MessageBox.Show("Izaberite nalog u tabeli.");
                return;
            }
            ID = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString());
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new Prijemnica(Tip, Vrsta, Convert.ToInt32(ID)));
        }

        private void CarinskoSkladistePregled_Load(object sender, EventArgs e)
        {
            cboProtokol.SelectedIndex = 0;
            cboStampaj.SelectedIndex = 0;
            cboSkeniraj.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
        
            if (cboStampaj.SelectedIndex == -1)
            {
                MessageBox.Show("Izaberite izveštaj.");
                return;
            }

            var record = gridGroupingControl1.Table.CurrentRecord;
            if (record == null) return;

            object idObj = record.GetValue("ID");
            if (idObj == null || idObj == DBNull.Value) return;

            int id = Convert.ToInt32(idObj);
            int izabraniIndeks = cboStampaj.SelectedIndex;

            // Pozivamo istu formu, samo menjamo Enum tip u zavisnosti od indeksa
            //  TipIzvestaja tip = (TipIzvestaja)izabraniIndeks;

            frmIzvestajCarinskoObelezje f = new frmIzvestajCarinskoObelezje(id, izabraniIndeks);
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var record = gridGroupingControl1.Table.CurrentRecord;
            if (record == null) return;

            object idObj = record.GetValue("ID");
            if (idObj == null || idObj == DBNull.Value)
            {
                MessageBox.Show("ID je nevažeći.");
                return;
            }

            int id = Convert.ToInt32(idObj);

            using (var frm = new frmSeniranjeDokumenata(id, "skladiste", cboSkeniraj.Text.Trim()))
                frm.ShowDialog();
        }
    }
}
