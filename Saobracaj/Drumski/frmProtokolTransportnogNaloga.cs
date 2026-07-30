

using Org.BouncyCastle.Asn1.Pkcs;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace Saobracaj.Drumski
{
    public partial class frmProtokolTransportnogNaloga: Form
    {
        bool autodan = false;
        bool dodatnaRuta = false;
        bool ostalo = false;
        int tipTransporta = 0; // 1 za platformu, 2 za ceradu
        int id = 0;
        int protokolID = 0;
        int radniNalogDrumskiID = 0;
        int tipProtokola = 0;
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;

        public frmProtokolTransportnogNaloga(int ProtokolID, int TipProtokola, int ID, int TipTransporta)
        {
            protokolID = ProtokolID;
            id = ID; 
            tipProtokola = TipProtokola;
            tipTransporta = TipTransporta;

            switch (TipProtokola)
            {
                case 1:
                    autodan = true;
                    break;
                case 2:
                    dodatnaRuta = true;
                    break;
                case 3:
                    ostalo = true;
                    break;
            }
            

            InitializeComponent();
            ChangeTextBox();
            ucitajComboBoxove();
            inicijalizujDatume();
            PopuniPolja();

         

            if (tipTransporta == 1)
            {
                panelPlatforma.Visible = true;
                panelCerada.Visible = false;

                if (protokolID == 0)
                    txtKorisnik.Text = tKorisnik;
            }
            else 
            {
                panelPlatforma.Visible = false;
                panelCerada.Visible = true;

                if (protokolID == 0)
                    txtKorisnikCerada.Text = tKorisnik;
            }
            

        }

        private void inicijalizujDatume()
        {
            if (tipTransporta == 1)
            {
                dtPreuzimanjaPraznogKontejnera.Value = DateTime.Now;
                dtPreuzimanjaPraznogKontejneraNovi.Value = DateTime.Now;
                dtpUtovara.Value = DateTime.Now;
                dtpUtovaraNovi.Value = DateTime.Now;
                dtpSpustanjePunog.Value = DateTime.Now;
                dtpSpustanjePunogNovi.Value = DateTime.Now;
            }
            else 
            {

                dtpUtovaraCerade.Value = DateTime.Now;
                dtpUtovaraCeradeNovi.Value = DateTime.Now;
                dtpRealiUtovaraCerade.Value = DateTime.Now;
                dtpIstovaraCerade.Value = DateTime.Now;
                dtpIstovaraCeradeNovi.Value = DateTime.Now;
                dtpRealiIstovaraCerade.Value = DateTime.Now;
            }
        }

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            meniHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                meniHeader.Visible = true;
                meniHeader.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                //foreach (Control control in groupBox1.Controls)
                //{
                //    if (control is System.Windows.Forms.Button buttons)
                //    {

                //        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                //        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                //        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                //        buttons.FlatStyle = FlatStyle.Flat;
                //    }
                //}


                foreach (System.Windows.Forms.Control control in this.Controls)
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
                meniHeader.Visible = false;
                meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ucitajComboBoxove()
        {
            
            var s_connection5 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
       

            // DrumskiPrevoz 
            //var partner = "Select PaSifra,PaNaziv From Partnerji  WHERE DrumskiPrevoz = 1 AND ISNULL(Kamioner, 0) = 1 order by PaNaziv";
            var partner = "Select PaSifra,PaNaziv From Partnerji  WHERE DrumskiPrevoz = 1 order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, s_connection5);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            DataTable dt = partDS.Tables[0];

            // Kreiraj novi red sa praznim tekstom i ID -1
            DataRow prazanRed = dt.NewRow();
            prazanRed["PaNaziv"] = "";
            prazanRed["PaSifra"] = -1;

            // Ubaci kao prvi red
            dt.Rows.InsertAt(prazanRed, 0);
            cboPrevoznik.DataSource = partDS.Tables[0];
            cboPrevoznik.DisplayMember = "PaNaziv";
            cboPrevoznik.ValueMember = "PaSifra";

            var partnerC = "Select PaSifra,PaNaziv From Partnerji  WHERE DrumskiPrevoz = 1 order by PaNaziv";
            var partCAD = new SqlDataAdapter(partner, s_connection5);
            var partCDS = new DataSet();
            partCAD.Fill(partCDS);
            DataTable dtC = partCDS.Tables[0];

            // Kreiraj novi red sa praznim tekstom i ID -1
            DataRow prazanRedC = dtC.NewRow();
            prazanRedC["PaNaziv"] = "";
            prazanRedC["PaSifra"] = -1;

            // Ubaci kao prvi red
            dtC.Rows.InsertAt(prazanRedC, 0);
            cboPrevoznikCerada.DataSource = partCDS.Tables[0];
            cboPrevoznikCerada.DisplayMember = "PaNaziv";
            cboPrevoznikCerada.ValueMember = "PaSifra";


            var klijent = "Select PaSifra,PaNaziv, Brodar, Spediter From Partnerji order by PaNaziv";
            SqlDataAdapter sviPartneriAD = new SqlDataAdapter(klijent, s_connection5);
            DataTable dtSviPartneri = new DataTable();
            sviPartneriAD.Fill(dtSviPartneri);
            cboKlijent.DataSource = dtSviPartneri.Copy();
            cboKlijent.DisplayMember = "PaNaziv";
            cboKlijent.ValueMember = "PaSifra";

            cboKlijentCerada.DataSource = dtSviPartneri.Copy();
            cboKlijentCerada.DisplayMember = "PaNaziv";
            cboKlijentCerada.ValueMember = "PaSifra";

            var dip = "Select ID,Naziv from MestaUtovara order by Naziv";
            SqlDataAdapter mestoUAD = new SqlDataAdapter(dip, s_connection5);
            DataTable dtMestoU = new DataTable();
            mestoUAD.Fill(dtMestoU);

            cboMestoPreuzimanja.DataSource = dtMestoU.Copy();
            cboMestoPreuzimanja.DisplayMember = "Naziv";
            cboMestoPreuzimanja.ValueMember = "ID";

            cboMestoSpustanjaPunog.DataSource = dtMestoU.Copy();
            cboMestoSpustanjaPunog.DisplayMember = "Naziv";
            cboMestoSpustanjaPunog.ValueMember = "ID";

            cboMestoUtovara.DataSource = dtMestoU.Copy();
            cboMestoUtovara.DisplayMember = "Naziv";
            cboMestoUtovara.ValueMember = "ID";

            var carp = "Select ID, Naziv From Carinarnice order by Naziv";
            SqlDataAdapter carpAD = new SqlDataAdapter(carp, s_connection5);
            DataTable dtCarP = new DataTable();
            carpAD.Fill(dtCarP);
            cboPolaznaCarinarnica.DataSource = dtCarP.Copy();
            cboPolaznaCarinarnica.DisplayMember = "Naziv";
            cboPolaznaCarinarnica.ValueMember = "ID";

            cboPolaznaCarinarnicaCerada.DataSource = dtCarP.Copy();
            cboPolaznaCarinarnicaCerada.DisplayMember = "Naziv";
            cboPolaznaCarinarnicaCerada.ValueMember = "ID";


            var car = "Select ID, Naziv From Carinarnice order by Naziv";
            SqlDataAdapter carAD = new SqlDataAdapter(car, s_connection5);
            DataTable dtCar = new DataTable();
            carAD.Fill(dtCar);
            cboOCarinarnica.DataSource = dtCar.Copy();
            cboOCarinarnica.DisplayMember = "Naziv";
            cboOCarinarnica.ValueMember = "ID";

            cboOCarinarnicaCerada.DataSource = dtCar.Copy();
            cboOCarinarnicaCerada.DisplayMember = "Naziv";
            cboOCarinarnicaCerada.ValueMember = "ID";


            cboMestoUtovaraCerade.DataSource = dtMestoU.Copy();
            cboMestoUtovaraCerade.DisplayMember = "Naziv";
            cboMestoUtovaraCerade.ValueMember = "ID";

            cboMestoIstovaraCerade.DataSource = dtMestoU.Copy();
            cboMestoIstovaraCerade.DisplayMember = "Naziv";
            cboMestoIstovaraCerade.ValueMember = "ID";

            //var sti = "Select ID, NazivPodtipa From PodtipProtokolaRazno   WHERE TipTransporta = "+ tipTransporta + " order by NazivPodtipa";
            //SqlDataAdapter sitAD = new SqlDataAdapter(sti, s_connection5);
            //DataTable dtSit = new DataTable();
            //sitAD.Fill(dtSit);
            //cboSituacija.DataSource = dtSit.Copy();
            //cboSituacija.DisplayMember = "NazivPodtipa";
            //cboSituacija.ValueMember = "ID";


            var sit = "Select ID, NazivPodtipa From PodtipProtokolaRazno   WHERE TipTransporta = " + tipTransporta + " order by NazivPodtipa";
            var sitAD = new SqlDataAdapter(sit, s_connection5);
            var sitDS = new DataSet();
            sitAD.Fill(sitDS);
            DataTable dt1 = sitDS.Tables[0];

            // Kreiraj novi red sa praznim tekstom i ID -1
            DataRow prazanRed1 = dt1.NewRow();
            prazanRed1["NazivPodtipa"] = "";
            prazanRed1["ID"] = -1;

            // Ubaci kao prvi red
            dt1.Rows.InsertAt(prazanRed1, 0);
            cboSituacija.DisplayMember = "NazivPodtipa";
            cboSituacija.ValueMember = "ID";

            cboSituacija.DataSource = dt1;


            var sitC = "Select ID, NazivPodtipa From PodtipProtokolaRazno   WHERE TipTransporta = " + tipTransporta + " order by NazivPodtipa";
            var sitCAD = new SqlDataAdapter(sit, s_connection5);
            var sitCDS = new DataSet();
            sitAD.Fill(sitCDS);
            DataTable dtC1 = sitDS.Tables[0];

            // Kreiraj novi red sa praznim tekstom i ID -1
            DataRow prazanRedC1 = dtC1.NewRow();
            prazanRedC1["NazivPodtipa"] = "";
            prazanRedC1["ID"] = -1;

            // Ubaci kao prvi red
            dtC1.Rows.InsertAt(prazanRedC1, 0);
            cboSituacijaCerada.DisplayMember = "NazivPodtipa";
            cboSituacijaCerada.ValueMember = "ID";

            cboSituacijaCerada.DataSource = dtC1;

            FillComboSpedicija();
        }

        public void FillComboSpedicija()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            SqlConnection conn = new SqlConnection(s_connection);
            var partner5 = "Select PaSifra,PaNaziv From Partnerji where Spediter = 1";
            SqlDataAdapter partAD5 = new SqlDataAdapter(partner5, conn);
            DataTable dtPart5 = new DataTable();
            partAD5.Fill(dtPart5);

            cbOspedicija.DataSource = dtPart5.Copy();
            cbOspedicija.DisplayMember = "PaNaziv";
            cbOspedicija.ValueMember = "PaSifra";


            cbOspedicijaCerada.DataSource = dtPart5.Copy();
            cbOspedicijaCerada.DisplayMember = "PaNaziv";
            cbOspedicijaCerada.ValueMember = "PaSifra";


            var partner4 = "SELECT PaSifra,PaNaziv From Partnerji where Spediter = 1";
            SqlDataAdapter partAD4 = new SqlDataAdapter(partner4, conn);
            DataTable dtPart4 = new DataTable();
            partAD4.Fill(dtPart4);

            cboPolaznaSpedicija.DataSource = dtPart4.Copy();
            cboPolaznaSpedicija.DisplayMember = "PaNaziv";
            cboPolaznaSpedicija.ValueMember = "PaSifra";

            cboPolaznaSpedicijaCerada.DataSource = dtPart4.Copy();
            cboPolaznaSpedicijaCerada.DisplayMember = "PaNaziv";
            cboPolaznaSpedicijaCerada.ValueMember = "PaSifra";



        }

        private DataTable VratiPodatke()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            DataTable dt = new DataTable();

            string query = @"SELECT	rn.ID , ik.ID AS IDNadredjena, rn.Uvoz, " +
             " ISNULL(rn.NalogID, -1) AS NalogID, rn.Uvoz, rn.KontejnerID, ik.BrojKontejnera," +
             " ik.Klijent3 AS Nalogodavac, ik.BookingBrodara," +
             " ik.MestoPreuzimanja AS MestoPreuzimanjaKontejnera,ik.MesoUtovara AS MestoUtovara," +
             " (Rtrim(pko.PaKOOpomba)) as AdresaUtovara, (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) as KontaktOsobaNaUtovaru ,ik.MestoPreuzimanja2 as MestoSpustanjaPunog," +
             " a.RegBr,a.Vozac,  a.PartnerID AS Prevoznik, ik.Korisnik, prn.OdredisnaCarinarnica," +
             " prn.PolaznaCarinarnica ," +
             " prn.PolaznaSpedicija , prn.OdredisnaSpedicija, prn.PolaznaSpedicijaKontakt, prn.OdredisnaSpedicijaKontakt," +
             " prn.PolaznaSpedicijaKontaktNovi, prn.OdredisnaSpedicijaKontaktNovi, prn.MestoUtovara AS MestoUtovaraProtokol," +
             " prn.AdresaUtovara AS AdresaUtovaraProtokol,prn.KontaktOsobaNaUtovaru AS KontaktUtovaraProtokol, prn.DatumUtovara AS DatumUtovaraProtokol," +
             " prn.DtNoviUtovaraKontejnera AS NoviDatumUtovaraProtokol,prn.MestoSpustanjaPunog AS MestoSpustanjaPunogProtokol, prn.DtSpustanja AS DtSpustanjaProtokol," +
             " prn.DtNoviSpustanja AS DtNoviSpustanjaProtokol, prn.MestoPreuzimanjaKontejnera AS MestoPreuzimanjaKontejneraProtokol," +
             " prn.DtPreuzimanjaPraznogKontejnera as DtPreuzimanjaPraznogKontejneraProtokol,prn.DtNoviPreuzimanjaKontejnera AS DtNoviPreuzimanjaKontejneraProtokol, prn.Cena AS CenaProtokol," +
             " prn.Trosak AS TrosakProtokol, prn.Opis, prn.Situacija,ik.MestoUtovaraCerade,ik.MestoIstovaraCerade, " +
             " rn.AdresaIstovaraCerade, rn.AdresaUtovaraCerade ,ik.KontaktOsobaIstovaraCerade AS KontaktOIstovaraCerade,'' AS KontaktOIstovaraCeradeString," +
             " ik.KontaktOsobaUtovaraCerade AS KontaktOUtovaraCerade, '' AS KontaktOUtovaraCeradeString," +
             " prn.DtUtovaraCerade, prn.DtNoviUtovaraCerade, prn.DtRealizacijeUtovaraCerade , prn.DtIstovaraCerade , prn.DtNoviIstovaraCerade , prn.DtRealizacijeIstovaraCerade ," +
             " prn.MestoUtovaraCerade AS MestoUtovaraCeradeProtokol, prn.MestoIstovaraCerade AS MestoIstovaraCeradeProtokol, prn.AdresaIstovaraCerade AS AdresaIstovaraCeradeProtokol," +
             " prn.AdresaUtovaraCerade AS AdresaUtovaraCeradeProtokol,prn.KontaktIstovaraCerade AS KontaktIstovaraCeradeProtokol, prn.KontaktUtovaraCerade  AS KontaktUtovaraCeradeProtokol," +
             " prn.ProtokolKreirao " +
             " FROM    RadniNalogDrumski rn " +
                      "INNER JOIN IzvozKonacna ik ON rn.KontejnerID = ik.ID " +
                      "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = ik.MesoUtovara AND pko.PaKOZapSt = ik.KontaktOsoba " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                      "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                      "LEFT JOIN ProtokolRadniNalogDrumski prn on rn.ID = prn.RadniNalogDrumskiID  AND prn.ID = @ProtokolID " +
             "where rn.ID= @id AND rn.Uvoz = 0 " +
             "UNION " +
             " SELECT	rn.ID , i.ID AS IDNadredjena, rn.Uvoz, " +
             " ISNULL(rn.NalogID, -1) AS NalogID, rn.Uvoz, rn.KontejnerID, i.BrojKontejnera," +
             " i.Klijent3 AS Nalogodavac, i.BookingBrodara," +
             " i.MestoPreuzimanja AS MestoPreuzimanjaKontejnera, " +
             " i.MesoUtovara AS MestoUtovara, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,  (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) as KontaktOsobaNaUtovaru , i.MestoPreuzimanja2 as MestoSpustanjaPunog," +
             " a.RegBr,a.Vozac,  a.PartnerID AS Prevoznik, i.Korisnik, prn.OdredisnaCarinarnica," +
             " prn.PolaznaCarinarnica ," +
             " prn.PolaznaSpedicija , prn.OdredisnaSpedicija, prn.PolaznaSpedicijaKontakt, prn.OdredisnaSpedicijaKontakt," +
             " prn.PolaznaSpedicijaKontaktNovi, prn.OdredisnaSpedicijaKontaktNovi, prn.MestoUtovara AS MestoUtovaraProtokol," +
             " prn.AdresaUtovara AS AdresaUtovaraProtokol,prn.KontaktOsobaNaUtovaru AS KontaktUtovaraProtokol, prn.DatumUtovara AS DatumUtovaraProtokol," +
             " prn.DtNoviUtovaraKontejnera AS NoviDatumUtovaraProtokol,prn.MestoSpustanjaPunog AS MestoSpustanjaPunogProtokol, prn.DtSpustanja AS DtSpustanjaProtokol," +
             " prn.DtNoviSpustanja AS DtNoviSpustanjaProtokol, prn.MestoPreuzimanjaKontejnera AS MestoPreuzimanjaKontejneraProtokol," +
             " prn.DtPreuzimanjaPraznogKontejnera as DtPreuzimanjaPraznogKontejneraProtokol,prn.DtNoviPreuzimanjaKontejnera AS DtNoviPreuzimanjaKontejneraProtokol, prn.Cena AS CenaProtokol," +
             " prn.Trosak AS TrosakProtokol, prn.Opis, prn.Situacija, i.MestoUtovaraCerade, i.MestoIstovaraCerade,rn.AdresaIstovaraCerade, rn.AdresaUtovaraCerade," +
             " i.KontaktOsobaIstovaraCerade AS KontaktOIstovaraCerade,'' AS KontaktOIstovaraCeradeString,i.KontaktOsobaUtovaraCerade AS KontaktOUtovaraCerade, '' AS KontaktOUtovaraCeradeString ," +
             " prn.DtUtovaraCerade, prn.DtNoviUtovaraCerade, prn.DtRealizacijeUtovaraCerade , prn.DtIstovaraCerade , prn.DtNoviIstovaraCerade , prn.DtRealizacijeIstovaraCerade," +
             " prn.MestoUtovaraCerade AS MestoUtovaraCeradeProtokol, prn.MestoIstovaraCerade AS MestoIstovaraCeradeProtokol, prn.AdresaIstovaraCerade AS AdresaIstovaraCeradeProtokol," +
             " prn.AdresaUtovaraCerade  AS AdresaUtovaraCeradeProtokol,prn.KontaktIstovaraCerade AS KontaktIstovaraCeradeProtokol, prn.KontaktUtovaraCerade  AS KontaktUtovaraCeradeProtokol ," +
             " prn.ProtokolKreirao " +
             " FROM    RadniNalogDrumski rn " +
                      "INNER JOIN  Izvoz i ON rn.KontejnerID = i.ID  " +
                      "LEFT JOIN partnerjiKontOsebaMU pko ON  pko.PaKOSifra = i.MesoUtovara AND pko.PaKOZapSt = i.KontaktOsoba " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                      "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                      "LEFT JOIN ProtokolRadniNalogDrumski prn on rn.ID = prn.RadniNalogDrumskiID  AND prn.ID = @ProtokolID " +
             "where rn.ID= @id AND rn.Uvoz = 0  " +
           
             "UNION " +
             " SELECT rn.ID ,  rn.ID AS IDNadredjena, rn.Uvoz, " +
             " ISNULL(rn.NalogID, -1) AS NalogID,rn.Uvoz,rn.KontejnerID,  rn.BrojKontejnera," +
             " rn.Klijent AS Nalogodavac, rn.BookingBrodara," +
             " rn.MestoPreuzimanjaKontejnera," +
             " rn.MestoUtovara, rn.AdresaUtovara,rn.KontaktOsobaNaUtovaru, rn.MestoSpustanjaPunog," +
             " a.RegBr,a.Vozac,  a.PartnerID AS Prevoznik, ko.Korisnik , prn.OdredisnaCarinarnica," +
             " prn.PolaznaCarinarnica ," +
             " prn.PolaznaSpedicija , prn.OdredisnaSpedicija, prn.PolaznaSpedicijaKontakt, prn.OdredisnaSpedicijaKontakt," +
             " prn.PolaznaSpedicijaKontaktNovi, prn.OdredisnaSpedicijaKontaktNovi, prn.MestoUtovara AS MestoUtovaraProtokol," +
             " prn.AdresaUtovara AS AdresaUtovaraProtokol,prn.KontaktOsobaNaUtovaru AS KontaktUtovaraProtokol, prn.DatumUtovara AS DatumUtovaraProtokol," +
             " prn.DtNoviUtovaraKontejnera AS NoviDatumUtovaraProtokol,prn.MestoSpustanjaPunog AS MestoSpustanjaPunogProtokol, prn.DtSpustanja AS DtSpustanjaProtokol," +
             " prn.DtNoviSpustanja AS DtNoviSpustanjaProtokol, prn.MestoPreuzimanjaKontejnera AS MestoPreuzimanjaKontejneraProtokol," +
             " prn.DtPreuzimanjaPraznogKontejnera as DtPreuzimanjaPraznogKontejneraProtokol,prn.DtNoviPreuzimanjaKontejnera AS DtNoviPreuzimanjaKontejneraProtokol, prn.Cena AS CenaProtokol," +
             " prn.Trosak AS TrosakProtokol, prn.Opis, prn.Situacija , rn.MestoUtovaraCerade, rn.MestoIstovaraCerade, rn.AdresaIstovaraCerade, rn.AdresaUtovaraCerade," +
             " 0 AS KontaktOIstovaraCerade,rn.KontaktOsobaIstovaraCerade AS KontaktOIstovaraCeradeString, 0 AS KontaktOUtovaraCerade,rn.KontaktOsobaUtovaraCerade  AS KontaktOUtovaraCeradeString," +
             " prn.DtUtovaraCerade, prn.DtNoviUtovaraCerade, prn.DtRealizacijeUtovaraCerade , prn.DtIstovaraCerade , prn.DtNoviIstovaraCerade , prn.DtRealizacijeIstovaraCerade," +
             " prn.MestoUtovaraCerade AS MestoUtovaraCeradeProtokol, prn.MestoIstovaraCerade AS MestoIstovaraCeradeProtokol, prn.AdresaIstovaraCerade AS AdresaIstovaraCeradeProtokol," +
             " prn.AdresaUtovaraCerade  AS AdresaUtovaraCeradeProtokol,prn.KontaktIstovaraCerade AS KontaktIstovaraCeradeProtokol, prn.KontaktUtovaraCerade  AS KontaktUtovaraCeradeProtokol," +
             "prn.ProtokolKreirao " +
             " FROM  RadniNalogDrumski rn " +
                  "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                  "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                  "LEFT JOIN ProtokolRadniNalogDrumski prn on rn.ID = prn.RadniNalogDrumskiID  AND prn.ID = @ProtokolID " +
                  "OUTER APPLY (SELECT TOP(1) DeSifra, Korisnik FROM Korisnici   WHERE desifra = rn.nalogkreiraokorisnik)  ko " +
             "  where rn.ID= @id AND rn.Uvoz in (-1,2,3, 4, 5) ";

            using (SqlConnection con = new SqlConnection(s_connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Bezbedno prosleđivanje parametra
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@ProtokolID", protokolID);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;

        }
        private void PopuniPolja()
        {
            // Povlačimo tabelu iz baze za prosleđeni ID
            DataTable dt = VratiPodatke();

            // Ako nema podataka, ne radimo ništa da izbegnemo greške
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Nisu pronađeni podaci za ovaj nalog.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow row = dt.Rows[0]; // Uzimamo prvi i jedini red

            // 1. Popunjavamo ono što je zajedničko za sve scenarije
             PopuniZajednickaPolja(row);

            // 2. Specifična logika bez mešanja koda
            if (autodan)
            {
                PopuniZaAutoDan(row);
                postaviVidljivostAutoDan();
            }
            else if (dodatnaRuta)
            {
                PopuniZaDodatnuRutu(row);
            }
            else if (ostalo)
            {
                PopuniZaOstalo(row);
            }
        }


        private void PopuniZajednickaPolja(DataRow row)
        {
            if (tipTransporta == 1)
            {
                if (row["ID"] != DBNull.Value && int.TryParse(row["ID"].ToString(), out int id))
                    radniNalogDrumskiID = id;

                txtID.Text = row["ID"].ToString();
                txtID.ReadOnly = true;
                txtNalogID.Text = row["NalogID"].ToString();
                txtNalogID.ReadOnly = true;
                txtKorisnik.ReadOnly = true;

                txtVozac.Text = row["Vozac"].ToString();
                txtVozac.ReadOnly = true;
                txtVozilo.Text = row["RegBr"].ToString();
                txtVozilo.ReadOnly = true;
                if (row["Nalogodavac"] != DBNull.Value && int.TryParse(row["Nalogodavac"].ToString(), out int nalogodavac))
                    cboKlijent.SelectedValue = nalogodavac;
                else
                    cboKlijent.SelectedValue = -1;
                cboKlijent.Enabled = false;

                if (row["Prevoznik"] != DBNull.Value && int.TryParse(row["Prevoznik"].ToString(), out int prevoznik))
                    cboPrevoznik.SelectedValue = prevoznik;
                else
                    cboPrevoznik.SelectedValue = -1;
                cboPrevoznik.Enabled = false;

                txtOpis.Text = row["Opis"].ToString();

                txtBokingBrodara.Text = row["BookingBrodara"].ToString();
                txtBokingBrodara.ReadOnly = true;
                txtBrojKontejnera.Text = row["BrojKontejnera"].ToString();
                txtBrojKontejnera.ReadOnly = true;
            }

            else if (tipTransporta == 2)
            {
           
                if (row["ID"] != DBNull.Value && int.TryParse(row["ID"].ToString(), out int id))
                    radniNalogDrumskiID = id;

                txtIDCerada.Text = row["ID"].ToString();
                txtNalogIDCerada.ReadOnly = true;
                txtNalogIDCerada.Text = row["NalogID"].ToString();
                txtNalogIDCerada.ReadOnly = true;
                txtKorisnikCerada.ReadOnly = true;

                txtVozacCerada.Text = row["Vozac"].ToString();
                txtVozacCerada.ReadOnly = true;
                txtVoziloCerada.Text = row["RegBr"].ToString();
                txtVoziloCerada.ReadOnly = true;
                if (row["Nalogodavac"] != DBNull.Value && int.TryParse(row["Nalogodavac"].ToString(), out int nalogodavac))
                    cboKlijentCerada.SelectedValue = nalogodavac;
                else
                    cboKlijentCerada.SelectedValue = -1;
                cboKlijentCerada.Enabled = false;

                if (row["Prevoznik"] != DBNull.Value && int.TryParse(row["Prevoznik"].ToString(), out int prevoznik))
                    cboPrevoznikCerada.SelectedValue = prevoznik;
                else
                    cboPrevoznikCerada.SelectedValue = -1;
                cboPrevoznikCerada.Enabled = false;

                txtOpisCerada.Text = row["Opis"].ToString();
           
            }

        }


        private void PopuniZaAutoDan(DataRow row)
        {

            //txtBokingBrodara.Text = row["BookingBrodara"].ToString();
            //txtBokingBrodara.ReadOnly = true;
            //txtBrojKontejnera.Text = row["BrojKontejnera"].ToString();
            //txtBrojKontejnera.ReadOnly = true;

            if(tipTransporta == 1)
            {
                if (row["MestoPreuzimanjaKontejnera"] != DBNull.Value && int.TryParse(row["MestoPreuzimanjaKontejnera"].ToString(), out int mestoPreuzimanja))
                    cboMestoPreuzimanja.SelectedValue = mestoPreuzimanja;
                else
                    cboMestoPreuzimanja.SelectedValue = -1;

     
                if (row["MestoSpustanjaPunog"] != DBNull.Value && int.TryParse(row["MestoSpustanjaPunog"].ToString(), out int mestoSpustanjaPunog))
                    cboMestoSpustanjaPunog.SelectedValue = mestoSpustanjaPunog;
                else
                    cboMestoSpustanjaPunog.SelectedValue = -1;
           

                if (row["MestoUtovara"] != DBNull.Value && int.TryParse(row["MestoUtovara"].ToString(), out int mestoUtovara))
                    cboMestoUtovara.SelectedValue = mestoUtovara;
                else
                    cboMestoUtovara.SelectedValue = -1;
    
                txtAdresaUtovara.Text = row["AdresaUtovara"].ToString();

                txtTrosak.Value = (row["TrosakProtokol"] != DBNull.Value && row["TrosakProtokol"] != null) ? Convert.ToDecimal(row["TrosakProtokol"]) : 0; 
                txtCena.Value = (row["CenaProtokol"] != DBNull.Value && row["CenaProtokol"] != null) ? Convert.ToDecimal(row["CenaProtokol"]) : 0;

                if (row["DtNoviPreuzimanjaKontejneraProtokol"] != DBNull.Value && row["DtNoviPreuzimanjaKontejneraProtokol"] != null)
                {
                    dtPreuzimanjaPraznogKontejneraNovi.Value = Convert.ToDateTime(row["DtNoviPreuzimanjaKontejneraProtokol"]);
                }

                if (row["DtNoviSpustanjaProtokol"] != DBNull.Value && row["DtNoviSpustanjaProtokol"] != null)
                {
                    dtpSpustanjePunogNovi.Value = Convert.ToDateTime(row["DtNoviSpustanjaProtokol"]);
                }

                if (row["NoviDatumUtovaraProtokol"] != DBNull.Value && row["NoviDatumUtovaraProtokol"] != null)
                {
                    dtpUtovaraNovi.Value = Convert.ToDateTime(row["NoviDatumUtovaraProtokol"]);
                }
                if (protokolID > 0)
                {

                    if (row["ProtokolKreirao"] != DBNull.Value && row["ProtokolKreirao"] != null)
                    {
                        txtKorisnik.Text = row["ProtokolKreirao"].ToString();
                    }
                }
            }
            else
            {
                int Uvoz = -1;
        
                if (row["Uvoz"] != DBNull.Value && int.TryParse(row["Uvoz"].ToString(), out int uvozConverted))
                    Uvoz = uvozConverted;

                if (row["MestoUtovaraCerade"] != DBNull.Value && int.TryParse(row["MestoUtovaraCerade"].ToString(), out int mestoUtovara))
                    cboMestoUtovaraCerade.SelectedValue = mestoUtovara;
                else
                    cboMestoUtovaraCerade.SelectedValue = -1;

                if (row["MestoIstovaraCerade"] != DBNull.Value && int.TryParse(row["MestoIstovaraCerade"].ToString(), out int mestoIstovara))
                    cboMestoIstovaraCerade.SelectedValue = mestoIstovara;
                else
                    cboMestoIstovaraCerade.SelectedValue = -1;



                if (Uvoz == 3)
                {
                    // Forsiramo Windows da završi sa obradom DataSource-a pre nego što upišemo tekst
                    // Application.DoEvents();
                    cboAdresaUtovaraCerade.Text = row["AdresaUtovaraCerade"].ToString();
                    cboKontaktUtovaraCerade.Text = row["KontaktOUtovaraCeradeString"].ToString(); // Pretpostavljam da i ovo treba

                    cboAdresaIstovaraCerade.Text = row["AdresaIstovaraCerade"].ToString();
                    cboKontaktIstovaraCerade.Text = row["KontaktOIstovaraCeradeString"].ToString(); // Pretpostavljam da i ovo treba
                }
                else
                {

                    PopuniAdresu(cboMestoUtovaraCerade, cboAdresaUtovaraCerade);
                    PopuniKontaktOsobu(cboMestoUtovaraCerade, cboKontaktUtovaraCerade);

                    PopuniAdresu(cboMestoIstovaraCerade, cboAdresaIstovaraCerade);
                    PopuniKontaktOsobu(cboMestoIstovaraCerade, cboKontaktIstovaraCerade);
                }

                if (row["DtNoviUtovaraCerade"] != DBNull.Value && row["DtNoviUtovaraCerade"] != null)
                {
                    dtpUtovaraCeradeNovi.Value = Convert.ToDateTime(row["DtNoviUtovaraCerade"]);
                }


                if (row["DtNoviIstovaraCerade"] != DBNull.Value && row["DtNoviIstovaraCerade"] != null)
                {
                    dtpIstovaraCeradeNovi.Value = Convert.ToDateTime(row["DtNoviIstovaraCerade"]);
                }


                txtTrosakCerada.Value = (row["TrosakProtokol"] != DBNull.Value && row["TrosakProtokol"] != null) ? Convert.ToDecimal(row["TrosakProtokol"]) : 0;
                txtCenaCerada.Value = (row["CenaProtokol"] != DBNull.Value && row["CenaProtokol"] != null) ? Convert.ToDecimal(row["CenaProtokol"]) : 0;
               
                if (protokolID > 0)
                {

                    if (row["ProtokolKreirao"] != DBNull.Value && row["ProtokolKreirao"] != null)
                    {
                        txtKorisnikCerada.Text = row["ProtokolKreirao"].ToString();
                    }
                }

                lblOpisCerada.Location = new Point(txtOpisCerada.Location.X, lblAdresaIstovaraCerade.Location.Y +54);
                txtOpisCerada.Location = new Point(txtOpisCerada.Location.X, cboAdresaIstovaraCerade.Location.Y + 54);
            }
        }

        private void postaviVidljivostAutoDan()
        {
            if (tipTransporta == 1)
            {
                lblPolaznaCarinarnica.Visible = cboPolaznaCarinarnica.Visible = false;
                lblPolaznaSpedicija.Visible = cboPolaznaSpedicija.Visible = false;
                lblKontaktPolazneSpedicije.Visible = txtKontaktPolazneSpedicije.Visible = false;
                lblNoviSpediterP.Visible = txtNoviSpediterP.Visible = false;
                lblOCarinarnica.Visible = cboOCarinarnica.Visible = false;
                lblOspedicija.Visible = cbOspedicija.Visible = false;
                lblKontaktOSpedicije.Visible = txtKontaktOSpedicije.Visible = false;
                lblNoviSpediterO.Visible = txtNoviSpediterO.Visible = false;
                lblSpustanjePunog.Visible = dtpSpustanjePunog.Visible = false;
                lblUtovara.Visible = dtpUtovara.Visible = false;
                lblDatumPreuzimanjaPraznog.Visible = dtPreuzimanjaPraznogKontejnera.Visible = false;
                lblkontaktNaUtovaru.Visible = txtkontaktNaUtovaru.Visible = false;
                lblSituacija.Visible = cboSituacija.Visible = false;
                cboMestoPreuzimanja.Enabled = false;
                cboMestoUtovara.Enabled = false;
                txtAdresaUtovara.Enabled = false;
                cboMestoSpustanjaPunog.Enabled = false;

                lblPreuzimanjaPraznogKontejneraNovi.Location = lblDatumPreuzimanjaPraznog.Location;
                dtPreuzimanjaPraznogKontejneraNovi.Location = dtPreuzimanjaPraznogKontejnera.Location;
                lblUtovaraNovi.Location = lblUtovara.Location;
                dtpUtovaraNovi.Location = dtpUtovara.Location;
                lblSpustanjePunogNovi.Location = lblSpustanjePunog.Location;
                dtpSpustanjePunogNovi.Location = dtpSpustanjePunog.Location;
            }
            else if (tipTransporta == 2)
            {
                lblPolaznaCarinarnicaCerada.Visible = cboPolaznaCarinarnicaCerada.Visible = false;
                lblPolaznaSpedicijaCerada.Visible = cboPolaznaSpedicijaCerada.Visible = false;
                lblKontaktPolazneSpedicijeCerada.Visible = txtKontaktPolazneSpedicijeCerada.Visible = false;
                lblNoviSpediterPCerada.Visible = txtNoviSpediterPCerada.Visible = false;
                lblOCarinarnicaCerada.Visible = cboOCarinarnicaCerada.Visible = false;
                lblOspedicijaCerada.Visible = cbOspedicijaCerada.Visible = false;
                lblKontaktOSpedicijeCerada.Visible = txtKontaktOSpedicijeCerada.Visible = false;
                lblNoviSpediterOCerada.Visible = txtNoviSpediterOCerada.Visible = false;
                lblUtovaraCerade.Visible = dtpUtovaraCerade.Visible = false;
                lblRealiUtovaraCerade.Visible = dtpRealiUtovaraCerade.Visible = false;
                lblIstovaraCerade.Visible = dtpIstovaraCerade.Visible = false;
                lblRealiIstovaraCerade.Visible = dtpRealiIstovaraCerade.Visible = false;
                lblKontaktUtovaraCerade.Visible = cboKontaktUtovaraCerade.Visible = false;
                lblKontaktIstovaraCerade.Visible = cboKontaktIstovaraCerade.Visible = false;
                lblSituacijaCerada.Visible = cboSituacijaCerada.Visible = false;

                lblUtovaraCeradeNovi.Location = lblUtovaraCerade.Location;
                dtpUtovaraCeradeNovi.Location = dtpUtovaraCerade.Location;
                lblIstovaraCeradeNovi.Location = lblIstovaraCerade.Location;
                dtpIstovaraCeradeNovi.Location = dtpIstovaraCerade.Location;


                cboMestoUtovaraCerade.Enabled = false;
                cboMestoIstovaraCerade.Enabled = false;
                cboAdresaUtovaraCerade.Enabled = false;
                cboAdresaIstovaraCerade.Enabled = false;
            }

                bool isVisible = (tipTransporta == 2) ? false : true;

            txtBrojKontejnera.Visible = lblBrojKontejnera.Visible = isVisible;
            txtBokingBrodara.Visible = lblBokingBrodara.Visible = isVisible;
            

        }

        private void PopuniZaDodatnuRutu(DataRow row)
        {
            if (tipTransporta == 1)
            {


                if (row["PolaznaCarinarnica"] != DBNull.Value && int.TryParse(row["PolaznaCarinarnica"].ToString(), out int polaznaCarinarnica))
                    cboPolaznaCarinarnica.SelectedValue = polaznaCarinarnica;
                else
                    cboPolaznaCarinarnica.SelectedValue = -1;

                if (row["OdredisnaCarinarnica"] != DBNull.Value && int.TryParse(row["OdredisnaCarinarnica"].ToString(), out int oCarinarnica))
                    cboOCarinarnica.SelectedValue = oCarinarnica;
                else
                    cboOCarinarnica.SelectedValue = -1;

                if (row["PolaznaSpedicija"] != DBNull.Value && int.TryParse(row["PolaznaSpedicija"].ToString(), out int polaznaSpedicija))
                    cboPolaznaSpedicija.SelectedValue = polaznaSpedicija;
                else
                    cboPolaznaSpedicija.SelectedValue = -1;

                if (row["OdredisnaSpedicija"] != DBNull.Value && int.TryParse(row["OdredisnaSpedicija"].ToString(), out int oSpedicija))
                    cbOspedicija.SelectedValue = oSpedicija;
                else
                    cbOspedicija.SelectedValue = -1;

                txtKontaktPolazneSpedicije.Text = row["PolaznaSpedicijaKontakt"].ToString();
                txtKontaktOSpedicije.Text = row["OdredisnaSpedicijaKontakt"].ToString();

                txtNoviSpediterP.Text = row["PolaznaSpedicijaKontaktNovi"].ToString();
                txtNoviSpediterO.Text = row["OdredisnaSpedicijaKontaktNovi"].ToString();

                if (row["MestoPreuzimanjaKontejnera"] != DBNull.Value && int.TryParse(row["MestoPreuzimanjaKontejnera"].ToString(), out int mestoPreuzimanja))
                    cboMestoPreuzimanja.SelectedValue = mestoPreuzimanja;
                else
                    cboMestoPreuzimanja.SelectedValue = -1;


                if (row["DtPreuzimanjaPraznogKontejneraProtokol"] != DBNull.Value && row["DtPreuzimanjaPraznogKontejneraProtokol"] != null)
                {
                    dtPreuzimanjaPraznogKontejnera.Value = Convert.ToDateTime(row["DtPreuzimanjaPraznogKontejneraProtokol"]);
                }

                if (row["DtNoviPreuzimanjaKontejneraProtokol"] != DBNull.Value && row["DtNoviPreuzimanjaKontejneraProtokol"] != null)
                {
                    dtPreuzimanjaPraznogKontejneraNovi.Value = Convert.ToDateTime(row["DtNoviPreuzimanjaKontejneraProtokol"]);
                }

                if (row["MestoSpustanjaPunog"] != DBNull.Value && int.TryParse(row["MestoSpustanjaPunog"].ToString(), out int mestoSpustanjaPunog))
                    cboMestoSpustanjaPunog.SelectedValue = mestoSpustanjaPunog;
                else
                    cboMestoSpustanjaPunog.SelectedValue = -1;

                if (row["DtSpustanjaProtokol"] != DBNull.Value && row["DtSpustanjaProtokol"] != null)
                {
                    dtpSpustanjePunog.Value = Convert.ToDateTime(row["DtSpustanjaProtokol"]);
                }

                if (row["DtNoviSpustanjaProtokol"] != DBNull.Value && row["DtNoviSpustanjaProtokol"] != null)
                {
                    dtpSpustanjePunogNovi.Value = Convert.ToDateTime(row["DtNoviSpustanjaProtokol"]);
                }

                if (row["MestoUtovaraProtokol"] != DBNull.Value && int.TryParse(row["MestoUtovaraProtokol"].ToString(), out int mestoUtovara))
                    cboMestoUtovara.SelectedValue = mestoUtovara;
                else
                    cboMestoUtovara.SelectedValue = -1;

                if (row["DatumUtovaraProtokol"] != DBNull.Value && row["DatumUtovaraProtokol"] != null)
                {
                    dtpUtovara.Value = Convert.ToDateTime(row["DatumUtovaraProtokol"]);
                }

                if (row["NoviDatumUtovaraProtokol"] != DBNull.Value && row["NoviDatumUtovaraProtokol"] != null)
                {
                    dtpUtovaraNovi.Value = Convert.ToDateTime(row["NoviDatumUtovaraProtokol"]);
                }

                txtAdresaUtovara.Text = row["AdresaUtovaraProtokol"].ToString();
                txtkontaktNaUtovaru.Text = row["KontaktUtovaraProtokol"].ToString();

                txtCena.Value = (row["CenaProtokol"] != DBNull.Value && row["CenaProtokol"] != null) ? Convert.ToDecimal(row["CenaProtokol"]) : 0;
                txtTrosak.Value = (row["TrosakProtokol"] != DBNull.Value && row["TrosakProtokol"] != null) ? Convert.ToDecimal(row["TrosakProtokol"]) : 0;

                lblSituacija.Visible = cboSituacija.Visible = false;

                if (protokolID > 0)
                {

                    if (row["ProtokolKreirao"] != DBNull.Value && row["ProtokolKreirao"] != null)
                    {
                        txtKorisnik.Text = row["ProtokolKreirao"].ToString();
                    }
                }

            }
            else 
            {
                int Uvoz = -1;

                if (row["Uvoz"] != DBNull.Value && int.TryParse(row["Uvoz"].ToString(), out int uvozConverted))
                    Uvoz = uvozConverted;

                if (row["PolaznaCarinarnica"] != DBNull.Value && int.TryParse(row["PolaznaCarinarnica"].ToString(), out int polaznaCarinarnica))
                    cboPolaznaCarinarnicaCerada.SelectedValue = polaznaCarinarnica;
                else
                    cboPolaznaCarinarnicaCerada.SelectedValue = -1;

                if (row["OdredisnaCarinarnica"] != DBNull.Value && int.TryParse(row["OdredisnaCarinarnica"].ToString(), out int oCarinarnica))
                    cboOCarinarnicaCerada.SelectedValue = oCarinarnica;
                else
                    cboOCarinarnicaCerada.SelectedValue = -1;

                if (row["PolaznaSpedicija"] != DBNull.Value && int.TryParse(row["PolaznaSpedicija"].ToString(), out int polaznaSpedicija))
                    cboPolaznaSpedicijaCerada.SelectedValue = polaznaSpedicija;
                else
                    cboPolaznaSpedicijaCerada.SelectedValue = -1;

                if (row["OdredisnaSpedicija"] != DBNull.Value && int.TryParse(row["OdredisnaSpedicija"].ToString(), out int oSpedicija))
                    cbOspedicijaCerada.SelectedValue = oSpedicija;
                else
                    cbOspedicijaCerada.SelectedValue = -1;

                txtKontaktPolazneSpedicijeCerada.Text = row["PolaznaSpedicijaKontakt"].ToString();
                txtKontaktOSpedicijeCerada.Text = row["OdredisnaSpedicijaKontakt"].ToString();

                txtNoviSpediterPCerada.Text = row["PolaznaSpedicijaKontaktNovi"].ToString();
                txtNoviSpediterOCerada.Text = row["OdredisnaSpedicijaKontaktNovi"].ToString();

                if (row["MestoUtovaraCeradeProtokol"] != DBNull.Value && int.TryParse(row["MestoUtovaraCeradeProtokol"].ToString(), out int mestoUtovaraCerade))
                    cboMestoUtovaraCerade.SelectedValue = mestoUtovaraCerade;
                else
                    cboMestoUtovaraCerade.SelectedValue = -1;

                if (row["MestoIstovaraCeradeProtokol"] != DBNull.Value && int.TryParse(row["MestoIstovaraCeradeProtokol"].ToString(), out int mestoIstovaraCerade))
                    cboMestoIstovaraCerade.SelectedValue = mestoIstovaraCerade;
                else
                    cboMestoIstovaraCerade.SelectedValue = -1;

         
                cboAdresaUtovaraCerade.Text = row["AdresaUtovaraCeradeProtokol"].ToString();
                cboKontaktUtovaraCerade.Text = row["KontaktUtovaraCeradeProtokol"].ToString(); 

                cboAdresaIstovaraCerade.Text = row["AdresaIstovaraCeradeProtokol"].ToString();
                cboKontaktIstovaraCerade.Text = row["KontaktIstovaraCeradeProtokol"].ToString(); 
               
                if (row["DtUtovaraCerade"] != DBNull.Value && row["DtUtovaraCerade"] != null)
                {
                    dtpUtovaraCerade.Value = Convert.ToDateTime(row["DtUtovaraCerade"]);
                }

                if (row["DtNoviUtovaraCerade"] != DBNull.Value && row["DtNoviUtovaraCerade"] != null)
                {
                    dtpUtovaraCeradeNovi.Value = Convert.ToDateTime(row["DtNoviUtovaraCerade"]);
                }

                if (row["DtRealizacijeUtovaraCerade"] != DBNull.Value && row["DtRealizacijeUtovaraCerade"] != null)
                {
                    dtpRealiUtovaraCerade.Value = Convert.ToDateTime(row["DtRealizacijeUtovaraCerade"]);
                }
                if (row["DtIstovaraCerade"] != DBNull.Value && row["DtIstovaraCerade"] != null)
                {
                    dtpIstovaraCerade.Value = Convert.ToDateTime(row["DtIstovaraCerade"]);
                }

                if (row["DtNoviIstovaraCerade"] != DBNull.Value && row["DtNoviIstovaraCerade"] != null)
                {
                    dtpIstovaraCeradeNovi.Value = Convert.ToDateTime(row["DtNoviIstovaraCerade"]);
                }

                if (row["DtRealizacijeIstovaraCerade"] != DBNull.Value && row["DtRealizacijeIstovaraCerade"] != null)
                {
                    dtpRealiIstovaraCerade.Value = Convert.ToDateTime(row["DtRealizacijeIstovaraCerade"]);
                }

                txtCenaCerada.Value = (row["CenaProtokol"] != DBNull.Value && row["CenaProtokol"] != null) ? Convert.ToDecimal(row["CenaProtokol"]) : 0;
                txtTrosakCerada.Value = (row["TrosakProtokol"] != DBNull.Value && row["TrosakProtokol"] != null) ? Convert.ToDecimal(row["TrosakProtokol"]) : 0;

                lblSituacijaCerada.Visible = cboSituacijaCerada.Visible = false;

                if (protokolID > 0)
                {

                    if (row["ProtokolKreirao"] != DBNull.Value && row["ProtokolKreirao"] != null)
                    {
                        txtKorisnikCerada.Text = row["ProtokolKreirao"].ToString();
                    }
                }
            }
           
        }

        private void PopuniZaOstalo(DataRow row)
        {
            if (tipTransporta == 1)
            {
                lblMestoPreuzimanja.Visible = cboMestoPreuzimanja.Visible = false;
                lblMestoUtovara.Visible = cboMestoUtovara.Visible = false;
                lblMestoSpustanjaPunog.Visible = cboMestoSpustanjaPunog.Visible = false;
                lblPolaznaCarinarnica.Visible = cboPolaznaCarinarnica.Visible = false;
                lblPolaznaSpedicija.Visible = cboPolaznaSpedicija.Visible = false;
                lblKontaktPolazneSpedicije.Visible = txtKontaktPolazneSpedicije.Visible = false;
                lblNoviSpediterP.Visible = txtNoviSpediterP.Visible = false;
                lblOCarinarnica.Visible = cboOCarinarnica.Visible = false;
                lblOspedicija.Visible = cbOspedicija.Visible = false;
                lblKontaktOSpedicije.Visible = txtKontaktOSpedicije.Visible = false;
                lblNoviSpediterO.Visible = txtNoviSpediterO.Visible = false;
                lblSpustanjePunog.Visible = dtpSpustanjePunog.Visible = false;
                lblUtovara.Visible = dtpUtovara.Visible = false;
                lblUtovaraNovi.Visible = dtpUtovaraNovi.Visible = false;
                lblSpustanjePunogNovi.Visible = dtpSpustanjePunogNovi.Visible = false;
                lblDatumPreuzimanjaPraznog.Visible = dtPreuzimanjaPraznogKontejnera.Visible = false;
                lblPreuzimanjaPraznogKontejneraNovi.Visible = dtPreuzimanjaPraznogKontejneraNovi.Visible = false;
                lblkontaktNaUtovaru.Visible = txtkontaktNaUtovaru.Visible = false;
                lblMestoPreuzimanjaPraznog.Visible = false;
                lblUtovarKontejnera.Visible = false;
                lblSpustanjePunogKontejnera.Visible = false;
                lblAdresaUtovara.Visible = txtAdresaUtovara.Visible = false;

                bool isVisible = (tipTransporta == 2) ? false : true;

                txtBrojKontejnera.Visible = lblBrojKontejnera.Visible = isVisible;
                txtBokingBrodara.Visible = lblBokingBrodara.Visible = isVisible;


                if (row["Situacija"] != DBNull.Value && int.TryParse(row["Situacija"].ToString(), out int situacija))
                    cboSituacija.SelectedValue = situacija;
                else
                    cboSituacija.SelectedValue = -1;

                txtCena.Value = (row["CenaProtokol"] != DBNull.Value && row["CenaProtokol"] != null) ? Convert.ToDecimal(row["CenaProtokol"]) : 0;
                txtTrosak.Value = (row["TrosakProtokol"] != DBNull.Value && row["TrosakProtokol"] != null) ? Convert.ToDecimal(row["TrosakProtokol"]) : 0;

                if (protokolID > 0)
                {

                    if (row["ProtokolKreirao"] != DBNull.Value && row["ProtokolKreirao"] != null)
                    {
                        txtKorisnik.Text = row["ProtokolKreirao"].ToString();
                    }
                }
                label7.Location = new Point(lblBokingBrodara.Location.X, lblSituacija.Location.Y);
                txtOpis.Location = new Point(lblBokingBrodara.Location.X, cboSituacija.Location.Y);

            }
            else 
            {

                lblPolaznaCarinarnicaCerada.Visible = cboPolaznaCarinarnicaCerada.Visible = false;
                lblPolaznaSpedicijaCerada.Visible = cboPolaznaSpedicijaCerada.Visible = false;
                lblKontaktPolazneSpedicijeCerada.Visible = txtKontaktPolazneSpedicijeCerada.Visible = false;
                lblNoviSpediterPCerada.Visible = txtNoviSpediterPCerada.Visible = false;
                lblOCarinarnicaCerada.Visible = cboOCarinarnicaCerada.Visible = false;
                lblOspedicijaCerada.Visible = cbOspedicijaCerada.Visible = false;
                lblKontaktOSpedicijeCerada.Visible = txtKontaktOSpedicijeCerada.Visible = false;
                lblNoviSpediterOCerada.Visible = txtNoviSpediterOCerada.Visible = false;
                lblUCerada.Visible = lblICerada.Visible = false;
                lblMestoUtovaraCerade.Visible = cboMestoUtovaraCerade.Visible = false;
                lblAdresaUtovaraCerade.Visible = cboAdresaUtovaraCerade.Visible = false;
                lblKontaktUtovaraCerade.Visible = cboKontaktUtovaraCerade.Visible = false;
                lblMestoIstovaraCerade.Visible = cboMestoIstovaraCerade.Visible = false;
                lblAdresaIstovaraCerade.Visible = cboAdresaIstovaraCerade.Visible = false;
                lblKontaktIstovaraCerade.Visible = cboKontaktIstovaraCerade.Visible = false;
                lblUtovaraCerade.Visible = dtpUtovaraCerade.Visible = false;
                lblUtovaraCeradeNovi.Visible = dtpUtovaraCeradeNovi.Visible = false;
                lblRealiUtovaraCerade.Visible = dtpRealiUtovaraCerade.Visible = false;
                lblIstovaraCerade.Visible = dtpIstovaraCerade.Visible = false;
                lblIstovaraCeradeNovi.Visible = dtpIstovaraCeradeNovi.Visible = false;
                lblRealiIstovaraCerade.Visible = dtpRealiIstovaraCerade.Visible = false;

                if (row["Situacija"] != DBNull.Value && int.TryParse(row["Situacija"].ToString(), out int situacija))
                    cboSituacijaCerada.SelectedValue = situacija;
                else
                    cboSituacijaCerada.SelectedValue = -1;

                txtCenaCerada.Value = (row["CenaProtokol"] != DBNull.Value && row["CenaProtokol"] != null) ? Convert.ToDecimal(row["CenaProtokol"]) : 0;
                txtTrosakCerada.Value = (row["TrosakProtokol"] != DBNull.Value && row["TrosakProtokol"] != null) ? Convert.ToDecimal(row["TrosakProtokol"]) : 0;

                if (protokolID > 0)
                {

                    if (row["ProtokolKreirao"] != DBNull.Value && row["ProtokolKreirao"] != null)
                    {
                        txtKorisnikCerada.Text = row["ProtokolKreirao"].ToString();
                    }
                }
                lblSituacijaCerada.Location = new Point(cboSituacijaCerada.Location.X, lblKlijentCerada.Location.Y + 54);
                cboSituacijaCerada.Location = new Point(cboSituacijaCerada.Location.X, cboKlijentCerada.Location.Y + 54);

                lblOpisCerada.Location = new Point(txtOpisCerada.Location.X, lblKlijentCerada.Location.Y + 54);
                txtOpisCerada.Location = new Point(txtOpisCerada.Location.X, cboKlijentCerada.Location.Y + 54);

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int? polaznaCarinarnica = null;
            int? polaznaSpedicija = null;
            string polaznaSpedicijaKontakt = null;
            string polaznaSpedicijaKontaktNovi = null;
            int? odredisnaCarinarnica = null;
            int? odredisnaSpedicija = null;
            string odredisnaSpedicijaKontakt = null;
            string odredisnaSpedicijaKontaktNovi = null;
            int? mestoUtovara = null;
            string adresaUtovara = null;
            string kontaktOsobaNaUtovaru = null;
            DateTime? datumUtovara = null; 
            DateTime? dtNoviUtovaraKontejnera = null;
            int? mestoSpustanjaPunog = null;
            DateTime? datSpustanja = null;
            DateTime? dtNoviSpustanja = null;
            int? mestoPreuzimanjaKontejnera = null;
            DateTime? dtPreuzimanjaPraznog = null;
            DateTime? dtNoviPreuzimanjaPraznog = null;
            string protokolKreirao = null;
            decimal? trosak = 0;
            decimal? cena =  0;
            string opis = null;
            int? situacija = null;
            int? mestoIstovaraCerade = null;
            int? mestoUtovaraCerade = null;
            string adresaIstovaraCerade = null;
            string adresaUtovaraCerade = null;
            string kontaktUtovaraCerade = null;
            string kontaktIstovaraCerade = null;
            DateTime? dtUtovaraCerade = null;
            DateTime? dtIstovaraCeradeNovi = null;
            DateTime? dtUtovaraCeradeNovi = null;
            DateTime? dtRealizacijeUtovaraCerade = null;
            DateTime? dtIstovaraCerade = null;
            DateTime? dtRealizacijeIstovaraCerade = null;


            if (tipTransporta == 1)
            {
                txtKorisnik.Text = tKorisnik;

                if (cboPolaznaCarinarnica.Visible && cboPolaznaCarinarnica.SelectedValue != null && cboPolaznaCarinarnica.Enabled == true)
                {
                    polaznaCarinarnica = (int)cboPolaznaCarinarnica.SelectedValue;
                }
                if (cboPolaznaSpedicija.Visible && cboPolaznaSpedicija.SelectedValue != null && cboPolaznaSpedicija.Enabled == true)
                {
                    polaznaSpedicija = (int)cboPolaznaSpedicija.SelectedValue;
                }
                if (txtKontaktPolazneSpedicije.Visible && !string.IsNullOrWhiteSpace(txtKontaktPolazneSpedicije.Text) && txtKontaktPolazneSpedicije.ReadOnly != true)
                {
                    polaznaSpedicijaKontakt = txtKontaktPolazneSpedicije.Text.Trim();
                }
                if (txtNoviSpediterP.Visible && !string.IsNullOrWhiteSpace(txtNoviSpediterP.Text) && txtNoviSpediterP.ReadOnly != true)
                {
                    polaznaSpedicijaKontaktNovi = txtNoviSpediterP.Text.Trim();
                }
                if (cboOCarinarnica.Visible && cboOCarinarnica.SelectedValue != null && cboOCarinarnica.Enabled == true)
                {
                    odredisnaCarinarnica = (int)cboOCarinarnica.SelectedValue;
                }
                if (cbOspedicija.Visible && cbOspedicija.SelectedValue != null && cbOspedicija.Enabled == true)
                {
                    odredisnaSpedicija = (int)cbOspedicija.SelectedValue;
                }
                if (txtKontaktOSpedicije.Visible && !string.IsNullOrWhiteSpace(txtKontaktOSpedicije.Text) && txtKontaktOSpedicije.ReadOnly != true)
                {
                    odredisnaSpedicijaKontakt = txtKontaktOSpedicije.Text.Trim();
                }
                if (txtNoviSpediterO.Visible && !string.IsNullOrWhiteSpace(txtNoviSpediterO.Text) && txtNoviSpediterO.ReadOnly != true)
                {
                    odredisnaSpedicijaKontaktNovi = txtNoviSpediterO.Text.Trim();
                }
                if (cboMestoUtovara.Visible && cboMestoUtovara.SelectedValue != null && cboMestoUtovara.Enabled == true)
                {
                    mestoUtovara = (int)cboMestoUtovara.SelectedValue;
                }
                if (txtAdresaUtovara.Visible && !string.IsNullOrWhiteSpace(txtAdresaUtovara.Text) && txtAdresaUtovara.ReadOnly != true)
                {
                    adresaUtovara = txtAdresaUtovara.Text.Trim();
                }
                if (txtkontaktNaUtovaru.Visible && !string.IsNullOrWhiteSpace(txtkontaktNaUtovaru.Text) && txtkontaktNaUtovaru.ReadOnly != true)
                    kontaktOsobaNaUtovaru = txtkontaktNaUtovaru.Text.Trim();
                
                if (dtpUtovara.Visible)
                {
                    datumUtovara = dtpUtovara.Value; 
                }
                if (dtpUtovaraNovi.Visible)
                {
                    dtNoviUtovaraKontejnera = dtpUtovaraNovi.Value;
                }
                    
                if (cboMestoSpustanjaPunog.Visible && cboMestoSpustanjaPunog.SelectedValue != null && cboMestoSpustanjaPunog.Enabled == true)
                {
                    mestoSpustanjaPunog = (int)cboMestoSpustanjaPunog.SelectedValue;
                }
                if (dtpSpustanjePunog.Visible)
                {
                    datSpustanja = dtpSpustanjePunog.Value;
                }
                if (dtpSpustanjePunogNovi.Visible)
                {
                    dtNoviSpustanja = dtpSpustanjePunogNovi.Value;
                }

                if (cboMestoPreuzimanja.Visible && cboMestoPreuzimanja.SelectedValue != null && cboMestoPreuzimanja.Enabled == true)
                {
                    mestoPreuzimanjaKontejnera = (int)cboMestoPreuzimanja.SelectedValue;
                }
                if (dtPreuzimanjaPraznogKontejnera.Visible)
                {
                    dtPreuzimanjaPraznog = dtPreuzimanjaPraznogKontejnera.Value;
                }
                if (dtPreuzimanjaPraznogKontejneraNovi.Visible)
                {
                    dtNoviPreuzimanjaPraznog = dtPreuzimanjaPraznogKontejneraNovi.Value;
                }
                if (cboSituacija.Visible != false && cboSituacija.SelectedValue != null &&  cboSituacija.SelectedValue != DBNull.Value)
                {
                    if (int.TryParse(cboSituacija.SelectedValue.ToString(), out int parsedSituacija))
                    {
                        situacija = parsedSituacija;
                    }
                }


                trosak = decimal.TryParse(txtTrosak.Text, out decimal valTrosak) ? valTrosak : 0;
                cena = decimal.TryParse(txtCena.Text, out decimal valCena) ? valCena : 0;
                opis = string.IsNullOrEmpty(txtOpis.Text.Trim()) ? null : txtOpis.Text.Trim();

            }
            else if(tipTransporta == 2)
            {
                txtKorisnikCerada.Text = tKorisnik;

                if (cboPolaznaCarinarnicaCerada.Visible && cboPolaznaCarinarnicaCerada.SelectedValue != null && cboPolaznaCarinarnicaCerada.Enabled == true)
                {
                    polaznaCarinarnica = (int)cboPolaznaCarinarnicaCerada.SelectedValue;
                }
                if (cboPolaznaSpedicijaCerada.Visible && cboPolaznaSpedicijaCerada.SelectedValue != null && cboPolaznaSpedicijaCerada.Enabled == true)
                {
                    polaznaSpedicija = (int)cboPolaznaSpedicijaCerada.SelectedValue;
                }
                if (txtKontaktPolazneSpedicijeCerada.Visible && !string.IsNullOrWhiteSpace(txtKontaktPolazneSpedicijeCerada.Text) && txtKontaktPolazneSpedicijeCerada.ReadOnly != true)
                {
                    polaznaSpedicijaKontakt = txtKontaktPolazneSpedicijeCerada.Text.Trim();
                }
                if (txtNoviSpediterPCerada.Visible && !string.IsNullOrWhiteSpace(txtNoviSpediterPCerada.Text) && txtNoviSpediterPCerada.ReadOnly != true)
                {
                    polaznaSpedicijaKontaktNovi = txtNoviSpediterPCerada.Text.Trim();
                }
                if (cboOCarinarnicaCerada.Visible && cboOCarinarnicaCerada.SelectedValue != null && cboOCarinarnicaCerada.Enabled == true)
                {
                    odredisnaCarinarnica = (int)cboOCarinarnicaCerada.SelectedValue;
                }
                if (cbOspedicijaCerada.Visible && cbOspedicijaCerada.SelectedValue != null && cbOspedicijaCerada.Enabled == true)
                {
                    odredisnaSpedicija = (int)cbOspedicijaCerada.SelectedValue;
                }
                if (txtKontaktOSpedicijeCerada.Visible && !string.IsNullOrWhiteSpace(txtKontaktOSpedicijeCerada.Text) && txtKontaktOSpedicijeCerada.ReadOnly != true)
                {
                    odredisnaSpedicijaKontakt = txtKontaktOSpedicijeCerada.Text.Trim();
                }
                if (txtNoviSpediterOCerada.Visible && !string.IsNullOrWhiteSpace(txtNoviSpediterOCerada.Text) && txtNoviSpediterOCerada.ReadOnly != true)
                {
                    odredisnaSpedicijaKontaktNovi = txtNoviSpediterOCerada.Text.Trim();
                }
                if (cboAdresaUtovaraCerade != null && !string.IsNullOrWhiteSpace(cboAdresaUtovaraCerade.Text))
                    adresaUtovaraCerade = cboAdresaUtovaraCerade.Text.Trim();

                if (cboAdresaIstovaraCerade != null && !string.IsNullOrWhiteSpace(cboAdresaIstovaraCerade.Text))
                    adresaIstovaraCerade = cboAdresaIstovaraCerade.Text.Trim();

                if (cboKontaktUtovaraCerade != null && !string.IsNullOrWhiteSpace(cboKontaktUtovaraCerade.Text))
                    kontaktUtovaraCerade = cboKontaktUtovaraCerade.Text.Trim();

                if (cboKontaktIstovaraCerade != null && !string.IsNullOrWhiteSpace(cboKontaktIstovaraCerade.Text))
                    kontaktIstovaraCerade = cboKontaktIstovaraCerade.Text.Trim();

                if (cboMestoIstovaraCerade.Visible && cboMestoIstovaraCerade.SelectedValue != null && cboMestoIstovaraCerade.Enabled == true)
                {
                    mestoIstovaraCerade = (int)cboMestoIstovaraCerade.SelectedValue;
                }
                if (cboMestoUtovaraCerade.Visible && cboMestoUtovaraCerade.SelectedValue != null && cboMestoUtovaraCerade.Enabled == true)
                {
                    mestoUtovaraCerade = (int)cboMestoUtovaraCerade.SelectedValue;
                }
                if (dtPreuzimanjaPraznogKontejnera.Visible)
                {
                    dtPreuzimanjaPraznog = dtPreuzimanjaPraznogKontejnera.Value;
                }
                if (dtPreuzimanjaPraznogKontejneraNovi.Visible)
                {
                    dtNoviPreuzimanjaPraznog = dtPreuzimanjaPraznogKontejneraNovi.Value;
                }
                if (cboSituacijaCerada.Visible != false && cboSituacijaCerada.SelectedValue != null && cboSituacijaCerada.SelectedValue != DBNull.Value)
                {
                    if (int.TryParse(cboSituacijaCerada.SelectedValue.ToString(), out int parsedSituacija))
                    {
                        situacija = parsedSituacija;
                    }
                }

                if (dtpUtovaraCerade.Visible )
                    dtUtovaraCerade = (dtpUtovaraCerade != null) ? dtpUtovaraCerade.Value : (DateTime?)null;

                if (dtpUtovaraCeradeNovi.Visible)
                    dtUtovaraCeradeNovi = (dtpUtovaraCeradeNovi != null) ? dtpUtovaraCeradeNovi.Value : (DateTime?)null;

                if (dtpIstovaraCeradeNovi.Visible)
                    dtIstovaraCeradeNovi = (dtpIstovaraCeradeNovi != null) ? dtpIstovaraCeradeNovi.Value : (DateTime?)null;

                if (dtpRealiUtovaraCerade.Visible)
                    dtRealizacijeUtovaraCerade = (dtpRealiUtovaraCerade != null) ? dtpRealiUtovaraCerade.Value : (DateTime?)null;

                if (dtpIstovaraCerade.Visible)
                    dtIstovaraCerade = (dtpIstovaraCerade != null) ? dtpIstovaraCerade.Value : (DateTime?)null;

                if (dtpRealiIstovaraCerade.Visible)
                    dtRealizacijeIstovaraCerade = (dtpRealiIstovaraCerade != null) ? dtpRealiIstovaraCerade.Value : (DateTime?)null;

                trosak = decimal.TryParse(txtTrosakCerada.Text, out decimal valTrosak) ? valTrosak : 0;
                cena = decimal.TryParse(txtCenaCerada.Text, out decimal valCena) ? valCena : 0;
                opis = string.IsNullOrEmpty(txtOpisCerada.Text.Trim()) ? null : txtOpisCerada.Text.Trim();

            }
            if (protokolID == 0)
            {
                InsertProtokolTransportnogNaloga ins = new InsertProtokolTransportnogNaloga();
                protokolID = ins.InsertProtokol(radniNalogDrumskiID, tipProtokola, tipTransporta, polaznaCarinarnica, polaznaSpedicija, polaznaSpedicijaKontakt,
                polaznaSpedicijaKontaktNovi, odredisnaCarinarnica, odredisnaSpedicija, odredisnaSpedicijaKontakt, odredisnaSpedicijaKontaktNovi, mestoUtovara,
                adresaUtovara, kontaktOsobaNaUtovaru, datumUtovara, dtNoviUtovaraKontejnera, mestoSpustanjaPunog, datSpustanja, dtNoviSpustanja,
                mestoPreuzimanjaKontejnera, dtPreuzimanjaPraznog, dtNoviPreuzimanjaPraznog, tKorisnik, trosak, cena, opis, situacija, mestoUtovaraCerade, adresaUtovaraCerade,
                kontaktUtovaraCerade, mestoIstovaraCerade, adresaIstovaraCerade, kontaktIstovaraCerade, dtUtovaraCerade, dtUtovaraCeradeNovi, dtRealizacijeUtovaraCerade,
                dtIstovaraCerade, dtIstovaraCeradeNovi, dtRealizacijeIstovaraCerade);

            }
            else
            {
                InsertProtokolTransportnogNaloga ins = new InsertProtokolTransportnogNaloga();
                ins.UpdateProtokol(protokolID, polaznaCarinarnica, polaznaSpedicija, polaznaSpedicijaKontakt,
                polaznaSpedicijaKontaktNovi, odredisnaCarinarnica, odredisnaSpedicija, odredisnaSpedicijaKontakt, odredisnaSpedicijaKontaktNovi, mestoUtovara,
                adresaUtovara, kontaktOsobaNaUtovaru, datumUtovara, dtNoviUtovaraKontejnera, mestoSpustanjaPunog, datSpustanja, dtNoviSpustanja,
                mestoPreuzimanjaKontejnera, dtPreuzimanjaPraznog, dtNoviPreuzimanjaPraznog, trosak, cena, opis, situacija, mestoUtovaraCerade, adresaUtovaraCerade,
                kontaktUtovaraCerade, mestoIstovaraCerade, adresaIstovaraCerade, kontaktIstovaraCerade, dtUtovaraCerade, dtUtovaraCeradeNovi, dtRealizacijeUtovaraCerade,
                dtIstovaraCerade, dtIstovaraCeradeNovi, dtRealizacijeIstovaraCerade);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                                   "Da li ste sigurni da želite da stornirate protokol?",
                                   "Potvrda storniranja",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question
                               );

            // Ako korisnik klikne na 'No' (ili zatvori prozor), samo prekida se izvršavanje
            if (result != DialogResult.Yes)
            {
                return;
            }

            InsertProtokolTransportnogNaloga ins = new InsertProtokolTransportnogNaloga();
            ins.DelProtokolRadniNalogDrumski(protokolID);

            // Opciono: poruka o uspehu ili osvežavanje prikaza
            MessageBox.Show("Protokol je uspešno storniran.", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;

            // 2. Zatvaramo trenutnu formu
            this.Close();
        }

        private void PopuniAdresu(ComboBox cboIzvor, ComboBox cboCilj)
        {
             string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            if (cboIzvor == null || cboIzvor.SelectedValue == null)
            {
                cboCilj.DataSource = null; // Brišemo listu jer mesto nije iz šifarnika
                return; // Ne idemo u bazu, ali polje ostaje slobodno za kucanje
            }
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
             string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
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

        private void cboMestoUtovaraCerade_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoUtovaraCerade, cboAdresaUtovaraCerade);
            PopuniKontaktOsobu(cboMestoUtovaraCerade, cboKontaktUtovaraCerade);
        }

        private void cboMestoIstovaraCerade_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoIstovaraCerade, cboAdresaIstovaraCerade);
            PopuniKontaktOsobu(cboMestoIstovaraCerade, cboKontaktIstovaraCerade);
        }
    }
}
