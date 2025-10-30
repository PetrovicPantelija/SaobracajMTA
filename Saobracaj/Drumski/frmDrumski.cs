using iTextSharp.text.pdf;
using iTextSharp.text;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using Saobracaj.Sifarnici;
using Saobracaj.Izvoz;
using Saobracaj.Uvoz;

namespace Saobracaj.Drumski
{
    public partial class frmDrumski : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int? Uvoz = null;
        int id = 0;
        bool status = false;
        private string noviZapisSaNovimNalogID = "";
        private int? mainNalogID;

        public frmDrumski()
        {
            InitializeComponent();
            ChangeTextBox();
            FillCombo();
            this.BindingContext = new BindingContext();
            VratiPodatke();
            txtNapomenaPoz.Visible = false;
            if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int tipNalogaId) && tipNalogaId == 2)
            {
                cboMestoPreuzimanja.SelectedValue = 8;
                cboMestoUtovara.SelectedValue = 8;
                txtAdresaUtovara.Text = "Jarački put";
            }
            lbtHederTekst.Text = "";
            lbtHederTekst.Visible = true;
            button1.Visible = false;
            button4.Visible = false;
        }

        public frmDrumski(string noviNalogID, int? NalogID)
        {
            noviZapisSaNovimNalogID = noviNalogID;
            mainNalogID = NalogID;
            InitializeComponent();
            ChangeTextBox();
            FillCombo();
            this.BindingContext = new BindingContext();
            VratiPodatke();
            txtNapomenaPoz.Visible = false;
            if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int tipNalogaId) && tipNalogaId == 2)
            {
                cboMestoPreuzimanja.SelectedValue = 8;
                cboMestoUtovara.SelectedValue = 8;
                txtAdresaUtovara.Text = "Jarački put";
            }
            if (noviNalogID == "NOVINALOG")
            {
                status = true;
                lbtHederTekst.Text = "UNOS NOVOG ZAPISA JE U TOKU!";
                lbtHederTekst.Visible = true;
            }
            else
            {
                lbtHederTekst.Text = "IZMENA ZAPISA JE U TOKU";
                lbtHederTekst.Visible = true;
            }
        }

        public frmDrumski(int ID)
        {
            this.id = ID;
            InitializeComponent();
            FillCombo();
            this.BindingContext = new BindingContext();
            ChangeTextBox();
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


     
        private void VratiPodatke()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT	rn.ID ," +
             "ISNULL(rn.NalogID, -1) AS NalogID, rn.Uvoz, rn.KontejnerID, rn.Status, rn.IDVrstaManipulacije,  rn.AutoDan, rn.Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "ik.Klijent3 AS Klijent, ik.MesoUtovara AS MestoUtovara, ik.KontaktOsoba as KontaktOsobaUtovarInt, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara, rn.MestoIstovara AS MestoIstovara, (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, rn.DatumUtovara, rn.DatumIstovara, rn.AdresaIstovara,  " +
             "rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz, CAST(ik.Spedicija AS nvarchar) AS KontaktSpeditera, " +
             "rn.Trosak, rn.Valuta, ik.BookingBrodara,  ik.BrojKontejnera,rn.BrojKontejnera2, ik.VrstaKontejnera AS TipKontejnera, ik.BrodskaPlomba AS BrojPlombe,  '' AS BrodskaTeretnica,  " +
             " ik.VGMBrod AS BTTKontejnetra, ik.BrutoRobe AS BTTRobe, " +
             "ik.NapomenaZaRobu as NapomenaZaPozicioniranje, a.RegBr,rn.KamionID , a.LicnaKarta, a.Vozac, a.BrojTelefona, pa.PaNaziv AS Prevoznik, rn.Cena, cc.Naziv AS CarinjenjeIzvozno,CAST(ik.Cirada AS VARCHAR) as TipTransporta," +
             "(ccp.Oznaka + ' ' + ccp.Naziv) AS NapomenaCarinskiPostupak , 0 AS OdredisnaCarina, ik.MestoCarinjenja as polaznaCarinarnica, ik.Spedicija as polaznaSpedicija, 0 as OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt,'' AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
             "FROM    RadniNalogDrumski rn " +
                      "INNER JOIN IzvozKonacna ik ON rn.KontejnerID = ik.ID " +
                      "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = ik.MesoUtovara AND pko.PaKOZapSt = ik.KontaktOsoba " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                      "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                      "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = ik.NapomenaReexport " +
                      "LEFT JOIN Carinarnice cc on cc.ID = ik.MestoCarinjenja " +
                      "LEFT JOIN IzvozKonacnaZaglavlje ukz ON ukz.ID = ik.IDNadredjena " +
                      "LEFT JOIN Voz v ON v.ID = ukz.IDVoza " +
             "where rn.ID=" + id + " AND rn.Uvoz = 0 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 " +
             "UNION " +
             "SELECT	rn.ID ," +
             "ISNULL(rn.NalogID, -1) AS NalogID, rn.Uvoz, rn.KontejnerID, rn.Status, rn.IDVrstaManipulacije, rn.AutoDan, rn.Ref, rn.MestoPreuzimanjaKontejnera, " +
             "i.Klijent3 AS Klijent,  i.MesoUtovara AS MestoUtovara,i.KontaktOsoba  as KontaktOsobaUtovarInt, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,rn.MestoIstovara AS MestoIstovara, (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, rn.DatumUtovara, rn.DatumIstovara, rn.AdresaIstovara, " +
             "rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz,CAST(i.Spedicija AS nvarchar) AS KontaktSpeditera, " +
             "rn.Trosak, rn.Valuta, i.BookingBrodara,  i.BrojKontejnera,rn.BrojKontejnera2,i.VrstaKontejnera AS TipKontejnera, i.BrodskaPlomba AS BrojPlombe, '' AS BrodskaTeretnica,  " +
             " i.VGMBrod AS BTTKontejnetra,  i.BrutoRobe AS BTTRobe, " +
             "i.NapomenaZaRobu AS NapomenaZaPozicioniranje, a.RegBr, rn.KamionID,  a.LicnaKarta, a.Vozac, a.BrojTelefona,pa.PaNaziv AS Prevoznik, rn.Cena, cc.Naziv AS CarinjenjeIzvozno, CAST(i.Cirada AS VARCHAR) as TipTransporta," +
             "(ccp.Oznaka + ' ' + ccp.Naziv) AS NapomenaCarinskiPostupak , 0 AS  OdredisnaCarina,i.MestoCarinjenja as polaznaCarinarnica,  i.Spedicija as polaznaSpedicija, 0 as OdredisnaSpedicija,'' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt, '' AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, '' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
             "FROM    RadniNalogDrumski rn " +
                      "INNER JOIN  Izvoz i ON rn.KontejnerID = i.ID  " +
                      "LEFT JOIN partnerjiKontOsebaMU pko ON  pko.PaKOSifra = i.MesoUtovara AND pko.PaKOZapSt = i.KontaktOsoba " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                      "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                      "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = i.NapomenaReexport " +
                      "LEFT JOIN Carinarnice cc on cc.ID = i.MestoCarinjenja " +
             "where rn.ID=" + id + " AND rn.Uvoz = 0 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 " +
             "UNION " +
             "SELECT rn.ID ," +
             "ISNULL(rn.NalogID, -1) AS NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.AutoDan,uk.Ref3 AS Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "uk.Nalogodavac3 AS Klijent,  rn.MestoUtovara,-1 as KontaktOsobaUtovarInt, rn.AdresaUtovara,uk.MestoIstovara AS MestoIstovara,uk.KontaktOsobe as KontaktOsobaUtovarIstovar, rn.DatumUtovara,rn.DatumIstovara, (Rtrim(pko.PaKOOpomba)) AS AdresaIstovara, " +
             "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,0 AS BookingBrodara,  uk.BrojKontejnera,rn.BrojKontejnera2, uk.TipKontejnera, '' AS BrojPlombe,  uk.BrodskaTeretnica,  " +
             " uk.BrutoKontejnera AS BTTKontejnetra, uk.BrutoRobe AS BTTRobe," +
             " np.Naziv as NapomenaZaPozicioniranje, a.RegBr, rn.KamionID,  a.LicnaKarta, a.Vozac, a.BrojTelefona , pa.PaNaziv AS Prevoznik, rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, pr.Naziv as TipTransporta, " +
             "'' AS NapomenaCarinskiPostupak,uk.OdredisnaCarina as OdredisnaCarina , 0 as polaznaCarinarnica, 0 as polaznaSpedicija, uk.OdredisnaSpedicija as OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt, rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta AS TipTransportaDrumski " +
             "FROM  RadniNalogDrumski rn " +
                    "INNER JOIN UvozKonacna uk ON rn.KontejnerID = uk.ID " +
                    "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = uk.MestoIstovara AND PaKOZapSt = uk.AdresaMestaUtovara " + /*AND PaKOSifra = mu.Naziv*/
                    "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = uk.NapomenaZaPozicioniranje " +
                    "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                    "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                    "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = uk.PostupakSaRobom " +
                    "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = uk.CarinskiPostupak " +
                    //"LEFT JOIN Carinarnice c on c.ID = uk.OdredisnaCarina " +
                    //"LEFT JOIN Partnerji p2 on p2.PaSifra = uk.OdredisnaSpedicija " +
                    "LEFT JOIN UvozKonacnaZaglavlje ukz ON ukz.ID = uk.IDNadredjeni " +
                    "LEFT JOIN Voz v ON v.ID = ukz.IDVoza " +
             "where rn.ID= " + id + " AND rn.Uvoz = 1  AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 " +
             "UNION " +
             "SELECT rn.ID ," +
             "ISNULL(rn.NalogID, -1) AS NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.AutoDan,u.Ref3 AS Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "u.Nalogodavac3 AS Klijent, rn.MestoUtovara, -1 as KontaktOsobaUtovarInt, rn.AdresaUtovara,u.MestoIstovara AS MestoIstovara,u.KontaktOsobe as KontaktOsobaUtovarIstovar, rn.DatumUtovara,rn.DatumIstovara,(Rtrim(pko.PaKOOpomba)) AS AdresaIstovara,  " +
             "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,0 AS BookingBrodara,  u.BrojKontejnera,rn.BrojKontejnera2, u.TipKontejnera AS TipKontejnera, '' AS BrojPlombe,  u.BrodskaTeretnica,   " +
             "u.BrutoKontejnera AS BTTKontejnetra, u.BrutoRobe AS BTTRobe, " +
             " np.Naziv as NapomenaZaPozicioniranje, a.RegBr, rn.KamionID, a.LicnaKarta, a.Vozac, a.BrojTelefona,pa.PaNaziv AS Prevoznik, rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, pr.Naziv as TipTransporta," +
             " '' AS NapomenaCarinskiPostupak, u.OdredisnaCarina as OdredisnaCarina,0 as polaznaCarinarnica, 0 as polaznaSpedicija, u.OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt, rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV,'' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
             "FROM  RadniNalogDrumski rn " +
                    "INNER JOIN  Uvoz u ON rn.KontejnerID = u.ID " +
                    "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = u.MestoIstovara AND pko.PaKOZapSt = u.AdresaMestaUtovara " + /*AND PaKOSifra = mu.Naziv*/
                    "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = u.NapomenaZaPozicioniranje " +
                    "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                    "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                    "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = u.PostupakSaRobom " +
                    "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = u.CarinskiPostupak " +
                    "LEFT JOIN Carinarnice c on c.ID = u.OdredisnaCarina " +
                    "LEFT JOIN Partnerji p2 on p2.PaSifra = u.OdredisnaSpedicija " +
             "where rn.ID= " + id + " AND rn.Uvoz = 1 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 " +
             "UNION " +
             "SELECT rn.ID ," +
             "ISNULL(rn.NalogID, -1) AS NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.AutoDan,rn.Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "rn.Klijent, rn.MestoUtovara, -1 as KontaktOsobaUtovarInt, rn.AdresaUtovara,rn.MestoIstovara AS MestoIstovara,rn.KontaktOsobaNaIstovaru AS KontaktOsobaUtovarIstovar, rn.DatumUtovara,rn.DatumIstovara,rn.AdresaIstovara AS AdresaIstovara,  " +
             "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,rn.BookingBrodara,  rn.BrojKontejnera,rn.BrojKontejnera2, rn.TipKontejnera AS TipKontejnera, rn.BrodskaPlomba AS BrojPlombe,   rn.BrodskaTeretnica,  " +
             " rn.BrutoKontejnera AS BTTKontejnetra, rn.BrutoRobe AS BTTRobe,  " +
             "CAST(rn.NapomenaZaPozicioniranje AS varchar(50)) AS NapomenaZaPozicioniranje, a.RegBr, rn.KamionID, a.LicnaKarta, a.Vozac, a.BrojTelefona, pa.PaNaziv AS Prevoznik, rn.Cena,'' as CarinjenjeIzvozno, '' as TipTransporta," +
             " '' AS NapomenaCarinskiPostupak, rn.OdredisnaCarinarnica as OdredisnaCarina,rn.PolaznaCarinarnica , rn.PolaznaSpedicija ,rn.OdredisnaSpedicija, rn.PolaznaSpedicijaKontakt, rn.OdredisnaSpedicijaKontakt, rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, " +
             "rn.PDV, rn.BrojVoza as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
             "FROM  RadniNalogDrumski rn " +
              "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
              "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
             "where rn.ID= " + id + " AND rn.Uvoz in (-1,2,3, 4, 5) AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 ", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtID.Text = dr["ID"].ToString();
                Uvoz = Convert.ToInt32(dr["Uvoz"].ToString());
                txtTipNaloga1.Text = Uvoz == 1 ? "Uvoz" : "Izvoz";
                int TipNaloga = Convert.ToInt32(dr["Uvoz"].ToString());

                if (dr["AutoDan"] != DBNull.Value && Convert.ToInt32(dr["AutoDan"].ToString()) == 1)
                    chkAutoDan.Checked = true;
                txtBokingBrodara.Text = dr["BookingBrodara"].ToString();
                txtReferenca.Text = dr["Ref"].ToString();
                txtBL.Text = dr["BrodskaTeretnica"].ToString();
                txtBrodskaPlomba.Text = dr["BrojPlombe"].ToString();
                
                if (dr["MestoPreuzimanjaKontejnera"] != DBNull.Value && int.TryParse(dr["MestoPreuzimanjaKontejnera"].ToString(), out int parsedMestoPreuzimanjaID))
                    cboMestoPreuzimanja.SelectedValue = parsedMestoPreuzimanjaID;
                else
                    cboMestoPreuzimanja.SelectedIndex = -1;

                if (dr["Klijent"] != DBNull.Value && int.TryParse(dr["Klijent"].ToString(), out int parsedKlijentID))
                    cboKlijent.SelectedValue = parsedKlijentID;
                else
                    cboKlijent.SelectedIndex = -1;

                if (dr["MestoUtovara"] != DBNull.Value && int.TryParse(dr["MestoUtovara"].ToString(), out int parsedMestoUtovaraID))
                    cboMestoUtovara.SelectedValue = parsedMestoUtovaraID;
                else
                    cboMestoUtovara.SelectedIndex = -1;
                if (dr["MestoIstovara"] != DBNull.Value && int.TryParse(dr["MestoIstovara"].ToString(), out int parsedMestoIstovaraID))
                    cboMestoIstovara.SelectedValue = parsedMestoIstovaraID;
                else
                    cboMestoIstovara.SelectedIndex = -1;

                txtKontaktOsobaUtovara.Text = dr["KontaktOsobaUtovarInt"].ToString();
                txtAdresaUtovara.Text = dr["AdresaUtovara"].ToString();
                txtAdresaIstovara.Text = dr["AdresaIstovara"].ToString();
                if (dr["DatumUtovara"] != DBNull.Value)
                    dtpUtovara.Value = Convert.ToDateTime(dr["DatumUtovara"].ToString());
                if (dr["DatumIstovara"] != DBNull.Value)
                    dtIstovara.Value = Convert.ToDateTime(dr["DatumIstovara"].ToString());
                if (dr["DtPreuzimanjaPraznogKontejnera"] != DBNull.Value)
                    dtPreuzimanjaPraznogKontejnera.Value = Convert.ToDateTime(dr["DtPreuzimanjaPraznogKontejnera"].ToString());

                // txtKontaktOsobeIstovar.Text = dr["KontaktOsobaNaIstovaru"].ToString();
                txtkontaktNaIstovaru.Text = dr["KontaktOsobaUtovarIstovar"].ToString();
                if (dr["BTTKontejnetra"] != DBNull.Value)
                    txtBrutoK.Value = Convert.ToDecimal(dr["BTTKontejnetra"].ToString());
                if (dr["BTTRobe"] != DBNull.Value)
                    txtBrutoR.Value = Convert.ToDecimal(dr["BTTRobe"].ToString());

                txtBrojVoza.Text = dr["NAzivVoza"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                txtBrojKontejnera2.Text = dr["BrojKontejnera2"].ToString();

                if (dr["PDV"] != DBNull.Value && Convert.ToInt32(dr["PDV"].ToString()) == 1)
                    chkPDV.Checked = true;

                if (dr["TipTransportaDrumski"] != DBNull.Value && int.TryParse(dr["TipTransportaDrumski"].ToString(), out int parsedTipTransportaDrumskiID))
                    cboTipTransporta.SelectedValue = parsedTipTransportaDrumskiID;
                else
                    cboTipTransporta.SelectedIndex = -1;


                if (Uvoz != 1 && Uvoz != 0)
                {
                    if (dr["NapomenaZaPozicioniranje"] != DBNull.Value && int.TryParse(dr["NapomenaZaPozicioniranje"].ToString(), out int parsedNapomenaZaPozicioniranjeID))
                    {
                        cboNapomenaPoz.SelectedValue = parsedNapomenaZaPozicioniranjeID;
                    }
                    else 
                    {
                        cboNapomenaPoz.SelectedValue = -1;
                    }
                }
                else if (Uvoz == 1 || Uvoz == 0)
                    txtNapomenaPoz.Text = dr["NapomenaZaPozicioniranje"].ToString();

                txtVozac.Text = dr["Vozac"].ToString();
                txtBrojTelefona.Text = dr["BrojTelefona"].ToString();
                txtBrojLK.Text = dr["LicnaKarta"].ToString();
                txtPrevoznik.Text = dr["Prevoznik"].ToString();
                txtGranicniPrelaz.Text = dr["GranicniPrelaz"].ToString();
                                
                if (dr["Trosak"] != DBNull.Value)
                    txtTrosak.Value = Convert.ToDecimal(dr["Trosak"].ToString());
                if (dr["Valuta"] != DBNull.Value)
                {
                    string sifraValute = dr["Valuta"].ToString().Trim();
                    txtValuta.SelectedValue = sifraValute;
                }
                else
                {
                    txtValuta.SelectedIndex = -1;
                }
                if (dr["Status"] != DBNull.Value && int.TryParse(dr["Status"].ToString(), out int parsedStatusID))
                    cboStatus.SelectedValue = parsedStatusID;
                else
                    cboStatus.SelectedIndex = -1;

                if (dr["Uvoz"] != DBNull.Value && int.TryParse(dr["Uvoz"].ToString(), out int parsedUvozID))
                    cboTipNaloga.SelectedValue = parsedUvozID;
                else
                    cboTipNaloga.SelectedIndex = -1;
                string k = dr["KamionID"].ToString();

                if (dr["KamionID"] != DBNull.Value)
                    cboKamion.SelectedValue = (dr["KamionID"].ToString());

                if (dr["TipKontejnera"] != DBNull.Value)
                    cboVrstaKontejnera.SelectedValue = (dr["TipKontejnera"].ToString());
                else
                    cboVrstaKontejnera.SelectedIndex = -1;

                txtDodatniOpis.Text = dr["DodatniOpis"].ToString();

                if (dr["OdredisnaCarina"] != DBNull.Value && int.TryParse(dr["OdredisnaCarina"].ToString(), out int parsedOdredisnaCarina))
                {
                    cboOCarinarnica.SelectedValue = parsedOdredisnaCarina;   
                }
                else
                {
                    cboOCarinarnica.SelectedIndex = -1;
                }

                if (dr["PolaznaCarinarnica"] != DBNull.Value && int.TryParse(dr["PolaznaCarinarnica"].ToString(), out int parsedPolaznaCarinarnica))
                {
                    cboPolaznaCarinarnica.SelectedValue = parsedPolaznaCarinarnica;
                }
                else
                {
                    cboPolaznaCarinarnica.SelectedIndex = -1;
                }
 
                txtKontaktOSpedicije.Text = dr["OdredisnaSpedicijaKontakt"].ToString();
                txtKontaktPolazneSpedicije.Text = dr["PolaznaSpedicijaKontakt"].ToString();

                if (dr["OdredisnaSpedicija"] != DBNull.Value && int.TryParse(dr["OdredisnaSpedicija"].ToString(), out int parsedOdredisnaSpedicija))
                    cbOspedicija.SelectedValue = parsedOdredisnaSpedicija;
                else
                    cbOspedicija.SelectedIndex = -1;
                cbOspedicija.SelectedValue = 187;     

                if (dr["PolaznaSpedicija"] != DBNull.Value && int.TryParse(dr["PolaznaSpedicija"].ToString(), out int parsedPolaznaSpedicija))
                    cboPolaznaSpedicija.SelectedValue = parsedPolaznaSpedicija;
                else
                    cboPolaznaSpedicija.SelectedIndex = -1;


                if (dr["Cena"] != DBNull.Value)
                    txtCena.Value = Convert.ToDecimal(dr["Cena"].ToString());
                txtVozac.Enabled = false;
                txtBrojLK.Enabled = false;
                txtBrojTelefona.Enabled = false;
                txtPrevoznik.Enabled = false;
                int NalogID = -1;
                if (dr["NalogID"] != DBNull.Value && int.TryParse(dr["NalogID"].ToString(), out int conertedNalogID))
                     NalogID = conertedNalogID;
                
                    
                if (Uvoz == 0)
                {
                    txtBokingBrodara.Enabled = false;
                    // label18.Visible = false;  // labela BL
                    txtBL.Enabled = false;
                    cboMestoUtovara.Enabled = false;
                    txtAdresaUtovara.Enabled = false;
                    cboKlijent.Enabled = false;
                    cboVrstaKontejnera.Enabled = false;
                    txtBrutoK.Enabled = false;
                    txtBrutoR.Enabled = false;
                    txtBrojVoza.Enabled = false;
                    cboTipNaloga.Visible = false;
                    txtTipNaloga1.Visible = true;
                    txtBrodskaPlomba.Enabled = false;
                    txtBrojKontejnera.Enabled = false;
                    label12.Text = "Kontakt osoba na utovaru";
                    cboPolaznaCarinarnica.Enabled= false;
                    cboOCarinarnica.Enabled = false;
                    txtKontaktPolazneSpedicije.Enabled = false;
                    cboPolaznaSpedicija.Enabled = false;
                    cbOspedicija.Enabled = false;
                    txtKontaktOSpedicije.Enabled = false;
                    cboNapomenaPoz.Visible = false;
                    txtNapomenaPoz.Visible = true;
                    btnFormiranjeNaloga.Visible = false;
                    // button3.Visible = NalogID > 0 ? false : true;
                }
                else if (Uvoz == 1)
                {
                    //label29.Visible = false;
                    txtBrodskaPlomba.Enabled = false;
                    txtBokingBrodara.Enabled = false;
                    txtBL.Visible = true;
                    txtReferenca.Enabled = false;
                    txtAdresaUtovara.Enabled = true;
                    cboMestoIstovara.Enabled = false;
                    txtAdresaIstovara.Enabled = false;
                    txtkontaktNaIstovaru.Enabled = false;
                    txtBL.Enabled = false;
                    cboKlijent.Enabled = false;
                    cboVrstaKontejnera.Enabled = false;
                    txtBrutoK.Enabled = false;
                    txtBrutoR.Enabled = false;
                    txtBrojVoza.Enabled = false;
                    cboTipNaloga.Visible = false;
                    txtTipNaloga1.Visible = true;
                    txtBrojKontejnera.Enabled = false;
                    label12.Text = "Kontakt osoba na istovaru";
                    cboPolaznaCarinarnica.Enabled = false;
                    cboPolaznaSpedicija.Enabled = false;
                    txtKontaktPolazneSpedicije.Enabled = false;
                    cboOCarinarnica.Enabled = false;
                    cbOspedicija.Enabled = false;

                    txtKontaktOSpedicije.Enabled = true;
                    cboNapomenaPoz.Visible = false;
                    txtNapomenaPoz.Visible = true;
                    btnFormiranjeNaloga.Visible = false;
                    //   button3.Visible = NalogID > 0 ? false : true;
                }
                else if (Uvoz == 2)
                {
                    btnFormiranjeNaloga.Visible = !(NalogID > 0 || (mainNalogID.HasValue && mainNalogID > 0));
                    //btnFormiranjeNaloga.Visible = NalogID > 0 ? false : true;
                    label12.Text = "Kontakt osoba na istovaru";
                    cboTipNaloga.Visible = true;
                    txtTipNaloga1.Visible = false;
                    cboKlijent.Enabled = true;
                    cboVrstaKontejnera.Enabled = true;
                    button21.Visible =  true;
                    cboNapomenaPoz.Visible = true;
                    txtNapomenaPoz.Visible = false;
                    txtBokingBrodara.Enabled = false;
                   
                    //delete je bilo vidljivo jedino ako nije dodeljeno nekom nalogu
                    // button21.Visible = NalogID > 0 ? false : true;
                    //  button3.Visible = NalogID > 0 ? false : true;
                }
                else if (Uvoz == 3)
                {
                    txtBL.Visible = true;
                    //btnFormiranjeNaloga.Visible = NalogID > 0 ? false : true;
                    btnFormiranjeNaloga.Visible = !(NalogID > 0 || (mainNalogID.HasValue && mainNalogID > 0));
                    button21.Visible = true;
                    //button21.Visible = NalogID > 0 ? false : true;
                    //     button3.Visible = NalogID > 0 ? false : true;
                    label12.Text = "Kontakt osoba na utovaru";
                    cboTipNaloga.Visible = true;
                    txtTipNaloga1.Visible = false;
                    cboKlijent.Enabled = true;
                    cboVrstaKontejnera.Enabled = true;
                    cboNapomenaPoz.Visible = true;
                    txtNapomenaPoz.Visible = false;
                }
                else if (Uvoz == 4)
                {
                    txtBokingBrodara.Enabled = false;
                    btnFormiranjeNaloga.Visible = !(NalogID > 0 || (mainNalogID.HasValue && mainNalogID > 0));
                    //btnFormiranjeNaloga.Visible = NalogID > 0 ? false : true;
                    button21.Visible = true;
                    //button21.Visible = NalogID > 0 ? false : true;
                    //     button3.Visible = NalogID > 0 ? false : true;
                    label12.Text = "Kontakt osoba na utovaru";
                    cboTipNaloga.Visible = true;
                    txtTipNaloga1.Visible = false;
                    cboKlijent.Enabled = true;
                    cboVrstaKontejnera.Enabled = true;
                    cboNapomenaPoz.Visible = true;
                    txtNapomenaPoz.Visible = false;
                }
                else if (Uvoz == 5)
                {
                    txtBL.Visible = true;
                    btnFormiranjeNaloga.Visible = !(NalogID > 0 || (mainNalogID.HasValue && mainNalogID > 0));
                    //btnFormiranjeNaloga.Visible = NalogID > 0 ? false : true;
                    button21.Visible = true;
                    //button21.Visible = NalogID > 0 ? false : true;
                    //     button3.Visible = NalogID > 0 ? false : true;
                    label12.Text = "Kontakt osoba na utovaru";
                    cboTipNaloga.Visible = true;
                    txtTipNaloga1.Visible = false;
                    cboKlijent.Enabled = true;
                    cboVrstaKontejnera.Enabled = true;
                    cboNapomenaPoz.Visible = true;
                    txtNapomenaPoz.Visible = false;
                }
                else
                {
                  //  button3.Visible = NalogID > 0 ? false : true;
                    cboTipNaloga.Visible = true;
                    txtTipNaloga1.Visible = false;
                    cboKlijent.Enabled = true;
                    cboVrstaKontejnera.Enabled = true;
                    cboNapomenaPoz.Visible = true;
                    txtNapomenaPoz.Visible = false;
                }
            }
            con.Close();
        }

        public void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);
            var val = "Select VaSifra, VaNaziv from Valute order by VaSifra";
            var valSAD = new SqlDataAdapter(val, conn);
            var valSDS = new DataSet();
            valSAD.Fill(valSDS);
            txtValuta.DataSource = valSDS.Tables[0];
            txtValuta.DisplayMember = "VaNaziv";
            txtValuta.ValueMember = "VaSifra";

            // postavi na default vrednost
            txtValuta.SelectedValue = "EUR";

            UcitajKamione(null);
            var stv = "select ID, Ltrim(Rtrim(Naziv)) AS Naziv from StatusVozila order by Naziv";
            var stvAD = new SqlDataAdapter(stv, conn);
            var stvDS = new DataSet();
            stvAD.Fill(stvDS);

            System.Data.DataTable dt = stvDS.Tables[0];
            DataRow prazanRed = dt.NewRow();
            prazanRed["ID"] = DBNull.Value;
            prazanRed["Naziv"] = "";
            dt.Rows.InsertAt(prazanRed, 0);

            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "Naziv";
            cboStatus.ValueMember = "ID";

            var tpv = " select ID, LTRIM(RTRIM(Naziv)) as Naziv from VrstaVozila";
            var tpvAD = new SqlDataAdapter(tpv, conn);
            var tpvDS = new DataSet();
            tpvAD.Fill(tpvDS);

            System.Data.DataTable dt2 = tpvDS.Tables[0];
            DataRow prazanRed2 = dt2.NewRow();
            prazanRed2["ID"] = DBNull.Value;
            prazanRed2["Naziv"] = "";
            dt2.Rows.InsertAt(prazanRed2, 0);

            cboTipTransporta.DataSource = dt2;
            cboTipTransporta.DisplayMember = "Naziv";
            cboTipTransporta.ValueMember = "ID";

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("ID", typeof(int));
            dt1.Columns.Add("Naziv", typeof(string));

            // Prazan red (opciono)
            DataRow prazan = dt.NewRow();
            prazan["ID"] = DBNull.Value;
            prazan["Naziv"] = "";
            dt.Rows.Add(prazan);

            // Fiksne vrednosti
            dt1.Rows.Add(4, "Uvoz");
            dt1.Rows.Add(5, "Izvoz");
            dt1.Rows.Add(2, "3PU");
            dt1.Rows.Add(3, "3PI");

            cboTipNaloga.DataSource = dt1;
            cboTipNaloga.DisplayMember = "Naziv";  
            cboTipNaloga.ValueMember = "ID";     

            var klijent = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var klAD = new SqlDataAdapter(klijent, conn);
            var klDS = new DataSet();
            klAD.Fill(klDS);
            cboKlijent.DataSource = klDS.Tables[0];
            cboKlijent.DisplayMember = "PaNaziv";
            cboKlijent.ValueMember = "PaSifra";

            var dip = "Select ID,Naziv from MestaUtovara order by Naziv";
            var dipAD = new SqlDataAdapter(dip, conn);
            var dipDS = new DataSet();
            dipAD.Fill(dipDS);
            cboMestoPreuzimanja.DataSource = dipDS.Tables[0];
            cboMestoPreuzimanja.DisplayMember = "Naziv";
            cboMestoPreuzimanja.ValueMember = "ID";

            var dir = "Select ID,Naziv from MestaUtovara order by Naziv";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new DataSet();
            dirAD.Fill(dirDS);
            cboMestoUtovara.DataSource = dirDS.Tables[0];
            cboMestoUtovara.DisplayMember = "Naziv";
            cboMestoUtovara.ValueMember = "ID";

            var mu = "select ID, Naziv from MestaUtovara order by Naziv";
            var muAD = new SqlDataAdapter(mu, conn);
            var muDS = new DataSet();
            muAD.Fill(muDS);
            cboMestoIstovara.DataSource = muDS.Tables[0];
            cboMestoIstovara.DisplayMember = "Naziv";
            cboMestoIstovara.ValueMember = "ID";

            var poz = "Select ID,LTRIM(RTRIM(Napomena)) AS Napomena from DrumskiPozicioniranje order by Napomena";
            var pozAD = new SqlDataAdapter(poz, conn);
            var pozDS = new DataSet();
            pozAD.Fill(pozDS);

            //System.Data.DataTable dt3 = pozDS.Tables[0];
            //DataRow prazanRed3 = dt3.NewRow();
            //prazanRed3["ID"] = DBNull.Value;
            //prazanRed3["Napomena"] = "/";
            //dt3.Rows.InsertAt(prazanRed3, 0);

            cboNapomenaPoz.DataSource = pozDS.Tables[0];
            cboNapomenaPoz.DisplayMember = "Napomena";
            cboNapomenaPoz.ValueMember = "ID";

            var car = "Select ID, Naziv From Carinarnice order by Naziv";
            var carAD = new SqlDataAdapter(car, conn);
            var carDS = new DataSet();
            carAD.Fill(carDS);
            cboOCarinarnica.DataSource = carDS.Tables[0];
            cboOCarinarnica.DisplayMember = "Naziv";
            cboOCarinarnica.ValueMember = "ID";

            // --- AutoComplete podesavanja ---
            cboOCarinarnica.DropDownStyle = ComboBoxStyle.DropDown;
            cboOCarinarnica.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboOCarinarnica.AutoCompleteSource = AutoCompleteSource.CustomSource;

            AutoCompleteStringCollection autoSrc = new AutoCompleteStringCollection();
            foreach (DataRow row in carDS.Tables[0].Rows)
            {
                autoSrc.Add(row["Naziv"].ToString());
            }
            cboOCarinarnica.AutoCompleteCustomSource = autoSrc;


            var caru= "Select ID, Naziv From Carinarnice order by Naziv";
            var caruAD = new SqlDataAdapter(caru, conn);
            var caruDS = new DataSet();
            carAD.Fill(caruDS);
            cboPolaznaCarinarnica.DataSource = caruDS.Tables[0];
            cboPolaznaCarinarnica.DisplayMember = "Naziv";
            cboPolaznaCarinarnica.ValueMember = "ID";


            var partner5 = "Select PaSifra,PaNaziv From Partnerji where Spediter = 1";
            var partAD5 = new SqlDataAdapter(partner5, conn);

            var partDS5 = new DataSet();
            partAD5.Fill(partDS5);
            cbOspedicija.DataSource = partDS5.Tables[0];
            cbOspedicija.DisplayMember = "PaNaziv";
            cbOspedicija.ValueMember = "PaSifra";


            var partner4 = "SELECT PaSifra,PaNaziv From Partnerji where Spediter = 1";
            var partAD4 = new SqlDataAdapter(partner4, conn);
    
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboPolaznaSpedicija.DataSource = partDS4.Tables[0];
            cboPolaznaSpedicija.DisplayMember = "PaNaziv";
            cboPolaznaSpedicija.ValueMember = "PaSifra";

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new DataSet();
            tkAD.Fill(tkDS);
            cboVrstaKontejnera.DataSource = tkDS.Tables[0];
            cboVrstaKontejnera.DisplayMember = "SkNaziv";
            cboVrstaKontejnera.ValueMember = "ID";
        }

        private void ComboBox_SearchAndPosition_OnKeyUp(object sender, KeyEventArgs e)
        {
            // Ne želimo da reagujemo na kontrolne tastere ili na već rešene unose
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape || e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || isSearching)
            {
                return;
            }

            ComboBox cbo = (ComboBox)sender;
            string currentText = cbo.Text;

            // Provera da li je ComboBox fokusiran i da li ima teksta za pretragu
            if (!string.IsNullOrEmpty(currentText))
            {
                string lowerSearchText = currentText.ToLower();

                isSearching = true; // Uključujemo flag

                // **KLJUČNO ZA STABILNOST:** Privremeno postavljamo SelectionStart pre bilo kakve manipulacije
                // da bismo 'zaključali' kursor i sprecili automatsko dopisivanje.
                cbo.SelectionStart = currentText.Length;
                cbo.SelectionLength = 0;

                bool found = false;
                int foundIndex = -1;

                // KORAK 1: PRONALAZENJE
                for (int i = 0; i < cbo.Items.Count; i++)
                {
                    DataRowView item = (DataRowView)cbo.Items[i];
                    string itemText = item[cbo.DisplayMember].ToString().ToLower();

                    if (itemText.StartsWith(lowerSearchText))
                    {
                        foundIndex = i;
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    // KORAK 2: POZICIONIRANJE I OTVARANJE

                    // Postavljamo SelectedIndex (ovo NEĆE automatski dopisati reč jer smo 
                    // PRETHODNO zaključali kursor SelectionStart/Length).
                    cbo.SelectedIndex = foundIndex;

                    // AKO SE I DALJE DEDESI AUTOMATSKO DOPISIVANJE, OVO PONIŠTAVA:
                    cbo.Text = currentText;

                    // Postavljamo kursor na kraj (ova provera je u KeyUp robustnija nego u TextChanged)
                    cbo.SelectionStart = currentText.Length;
                    cbo.SelectionLength = 0;

                    // Otvaramo padajuću listu (sada kada je pozicija kursora stabilna)
                    if (!cbo.DroppedDown)
                    {
                        cbo.DroppedDown = true;
                    }
                }
                else
                {
                    // Ako nema podudaranja, zatvorimo listu da se ne bi prikazivala
                    // cbo.DroppedDown = false; // Možda je bolje zadržati je otvorenom?
                }

                isSearching = false; // Isključujemo flag
                e.Handled = true; // Potvrđujemo da smo mi obradili ovaj unos (opcionalno)
            }
            // Ako se tekst obriše
            else if (string.IsNullOrEmpty(currentText))
            {
                isSearching = true;
                cbo.DroppedDown = false;
                cbo.SelectedIndex = -1;
                isSearching = false;
            }
        }

        private void ComboBox_SearchAndPosition(object sender)
        {
            // === GUARD FLAG: Sprecava rekurziju ===
            if (isSearching)
            {
                return;
            }

            ComboBox cbo = (ComboBox)sender;

            // Provera da li je ComboBox fokusiran i da li ima teksta za pretragu
            if (cbo.Focused && !string.IsNullOrEmpty(cbo.Text))
            {
                string currentText = cbo.Text;
                string lowerSearchText = currentText.ToLower();

                // Postavljamo flag na true pre programskih promena
                isSearching = true;

                bool found = false;

                // Prolazimo kroz sve stavke
                // (Pretpostavljamo da su svi combobox-ovi vezani za DataTable sa DisplayMember "Naziv")
                foreach (DataRowView item in cbo.Items)
                {
                    string itemText = item[cbo.DisplayMember].ToString().ToLower(); // Koristi DisplayMember!

                    if (itemText.StartsWith(lowerSearchText))
                    {
                        // KORAK 1: POZICIONIRANJE
                        cbo.SelectedIndex = cbo.Items.IndexOf(item);

                        // KORAK 2: VRAĆANJE TEKSTA I KURORA
                        cbo.Text = currentText;
                        cbo.SelectionStart = currentText.Length;
                        cbo.SelectionLength = 0;

                        found = true;
                        break;
                    }
                }

                // KORAK 3: OTVARANJE COMBOBOX-A
                if (found && !cbo.DroppedDown)
                {
                    cbo.DroppedDown = true;
                }

                // Resetujemo flag na false
                isSearching = false;
            }
            // Ako se tekst obriše (polje je prazno)
            else if (string.IsNullOrEmpty(cbo.Text))
            {
                isSearching = true;
                cbo.DroppedDown = false;
                cbo.SelectedIndex = -1; // Resetujemo selekciju
                isSearching = false;
            }
        } 
        private void cboMestoPreuzimanja_TextChanged(object sender, EventArgs e)
        {
            //// Trenutno uneti tekst (koristimo ToLower() za pretragu bez obzira na velika/mala slova)
            //string searchText = cboMestoPreuzimanja.Text.ToLower();

            //// Proveravamo da li je ComboBox fokusiran i da li ima teksta za pretragu
            //if (cboMestoPreuzimanja.Focused && !string.IsNullOrEmpty(searchText))
            //{
            //    // Cuvamo trenutnu poziciju kursora i duzinu selekcije
            //    int selectionStart = cboMestoPreuzimanja.SelectionStart;

            //    // Prolazimo kroz sve stavke
            //    foreach (DataRowView item in cboMestoPreuzimanja.Items)
            //    {
            //        // Dohvatamo vrednost DisplayMember-a (Naziv)
            //        string itemText = item["Naziv"].ToString().ToLower();

            //        if (itemText.StartsWith(searchText))

            //            cboMestoPreuzimanja.SelectedIndex = cboMestoPreuzimanja.Items.IndexOf(item);


            //            cboMestoPreuzimanja.Text = searchText; // Samo tekst koji je korisnik ukucao

            //            // 2. Postavite kursor na kraj unetog teksta
            //            cboMestoPreuzimanja.SelectionStart = cboMestoPreuzimanja.Text.Length;

            //            // Opcionalno: Otvorite padajuću listu da biste prikazali rezultat
            //            // if (!cboMestoPreuzimanja.DroppedDown)
            //            // {
            //            //     cboMestoPreuzimanja.DroppedDown = true;
            //            // }

            //            return;
            //     }
            //  }

            ComboBox_SearchAndPosition(sender);
        }
        
        private void UcitajKamione(int? tipTransportaId)
        {
            SqlConnection conn = new SqlConnection(connection);

            List<string> statusi = new List<string>();

            using (conn)
            {
                conn.Open();

                // 1. Učitaj status vrednosti iz SistemskePostavke
                SqlCommand cmd1 = new SqlCommand("SELECT Vrednost FROM SistemskePostavke WHERE Naziv LIKE 'StatusKamiona%'", conn);
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statusi.Add(reader.GetString(0));
                    }
                }

                // 2. Priprema statusa za upit
                string statusiZaUpit = string.Join(",", statusi
                  .Select(s => int.TryParse(s.Trim(), out int broj) ? broj.ToString() : null)
                  .Where(s => s != null));

                int radniNalogID = 0;
                int.TryParse(txtID.Text, out radniNalogID);

                // 3. Kreiraj glavni SQL upit
                
                string kam = $@" SELECT a.ID, a.Marka, a.RegBr, a.Vozac
                                FROM Automobili a
                                WHERE a.VoziloDrumskog = 1  AND (
                                    -- Nema NIJEDAN radni nalog sa statusom različitim od 7
                                    NOT EXISTS (
                                        SELECT 1
                                        FROM   RadniNalogDrumski r
                                        WHERE  r.KamionID = a.ID
                                          AND  (r.Status IS NULL OR r.Status NOT IN ({statusiZaUpit}))
                                          AND  r.ID <> @ID
                                    )
                                    OR
                                    EXISTS (
                                        -- ILI je već dodeljen ovom nalogu
                                        SELECT 1
                                        FROM   RadniNalogDrumski r
                                        WHERE  r.KamionID = a.ID AND r.ID = @ID
                                    )
                                )";

                SqlCommand cmd = new SqlCommand(kam, conn);
                cmd.Parameters.AddWithValue("@ID", radniNalogID);
                if (tipTransportaId.HasValue && tipTransportaId.Value > 0)
                {
                    kam += " AND VlasnistvoLegeta = @TipTransporta";
                    cmd.Parameters.AddWithValue("@TipTransporta", tipTransportaId.Value);
                    cmd.CommandText = kam; // ponovo postavi CommandText ako si dodao AND
                }

                SqlDataAdapter kamAD = new SqlDataAdapter(cmd);
                DataSet kmaDS = new DataSet();
                kamAD.Fill(kmaDS);

                DataTable dt1 = kmaDS.Tables[0];
                DataRow prazanR = dt1.NewRow();
                prazanR["ID"] = DBNull.Value;
                prazanR["RegBr"] = "";
                dt1.Rows.InsertAt(prazanR, 0);

                cboKamion.DataSource = dt1;
                cboKamion.DisplayMember = "RegBr";
                cboKamion.ValueMember = "ID";

                foreach (DataRowView item in cboKamion.Items)
                {
                    Console.WriteLine($"Combo item: ID={item["ID"]}, RegBr={item["RegBr"]}");
                }
            }
        }

        private void frmDrumski_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button3, "Kreiranje novog zapisa");
            toolTip1.SetToolTip(button2, "Snimanje izmena");
            toolTip1.SetToolTip(button21, "Brisanje zapisa");
            dtpUtovara.Value = DateTime.Today;
            dtIstovara.Value = DateTime.Today;
            dtPreuzimanjaPraznogKontejnera.Value = DateTime.Today;
            VratiPodatke();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string referenca = null;
            int? mestoIstovara = null;
            int? mestoUtovara = null;
            int? klijent = null;
            decimal? bttoKontejnera = null;
            decimal? bttoRobe = null;
            string brojVoza = null;
            string brojKontejnera = null;
            string brodskaPlomba = null;
            string brodskaTeretnica = null;
            DateTime? datumIstovara = null;
            DateTime? datumUtovara = null;
            int? bookingBrodara = null;
            string adresaIstovara = null;
            string adresaUtovara = null;
            int? napomenaPoz = null;
            int? polaznaCarinarnica = null;
            int? odredisnaCarinarnica = null;
            int? polaznaSpedicija = null;
            int? odredisnaSpedicija = null;
            string odredisnaSpedicijaKontakt = null;
            string polaznaSpedicijaKontakt = null;
            int kreirajNalogID = 0;
            int? nalogID = null;

            int iD = 0;
            if (txtID.Text != null && int.TryParse(txtID.Text, out int parsedID))
            {
                iD = parsedID;
            }
            int autoDan = chkAutoDan.Checked ? 1 : 0;
            int PDV = chkPDV.Checked ? 1 : 0;

            int? mestoPreuzimanja = null;
            if (cboMestoPreuzimanja.SelectedValue != null
                && Uvoz != 0
                && int.TryParse(cboMestoPreuzimanja.SelectedValue.ToString(), out int parsedMestoPreuzimanjaID))
            {
                mestoPreuzimanja = parsedMestoPreuzimanjaID;
            }
            else if (!string.IsNullOrWhiteSpace(cboMestoUtovara.Text))
            {
                int mestoP = InsertMestoUtovaraUSifarnik();
                mestoPreuzimanja = mestoP > -1 ? mestoP : (int?)null;
            }

            string kontaktOsobaistovara = string.IsNullOrWhiteSpace(txtkontaktNaIstovaru.Text) ? null : txtkontaktNaIstovaru.Text.Trim();

            if (dtpUtovara.Checked)
                datumUtovara = dtpUtovara.Value;

            if (dtIstovara.Checked)
                datumIstovara = dtIstovara.Value;

            DateTime? dtPreuzimanjaPraznogKont = null;
            if (dtPreuzimanjaPraznogKontejnera.Checked)
                dtPreuzimanjaPraznogKont = dtPreuzimanjaPraznogKontejnera.Value;

            string granicniPrelaz = string.IsNullOrWhiteSpace(txtGranicniPrelaz.Text) ? null : txtGranicniPrelaz.Text.Trim();

            decimal? trosak = null;
            decimal? cena = null;
            if (!string.IsNullOrWhiteSpace(txtTrosak.Text) && decimal.TryParse(txtTrosak.Text, out decimal parsedTrosak))
                trosak = parsedTrosak;

            if (!string.IsNullOrWhiteSpace(txtCena.Text) && decimal.TryParse(txtCena.Text, out decimal parsedCena))
                cena = parsedCena;

            string valutaID = null;
            if (txtValuta.SelectedValue != null)
                valutaID = txtValuta.SelectedValue.ToString();

            int? kamionID = null;
            if (cboKamion.SelectedValue != null && int.TryParse(cboKamion.SelectedValue.ToString(), out int parsedKamionID))
                kamionID = parsedKamionID;

            int? statusID = null;
            if (cboStatus.SelectedValue != null && int.TryParse(cboStatus.SelectedValue.ToString(), out int parsedStatusID))
                statusID = parsedStatusID;

            int? tipNaloga = null;
            if (Uvoz != 1 && Uvoz != 0)
            {
                if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int parsedTipNaloga))
                    tipNaloga = parsedTipNaloga;
            }
            else
            {
                tipNaloga = Uvoz;
            }

            int? vrstaKontejnera = null;
            if (Uvoz != 1 && Uvoz != 0)
            {
                if (cboVrstaKontejnera.SelectedValue != null && int.TryParse(cboVrstaKontejnera.SelectedValue.ToString(), out int parsedVrstaKontejnera))
                    vrstaKontejnera = parsedVrstaKontejnera;
            }
            else
            {
                vrstaKontejnera = null;
            }

            int? tipTransportaID = null;
            if (cboTipTransporta.SelectedValue != null && int.TryParse(cboTipTransporta.SelectedValue.ToString(), out int parsedTipTransportaID))
                tipTransportaID = parsedTipTransportaID;

            string dodatniOpis = string.IsNullOrWhiteSpace(txtDodatniOpis.Text) ? null : txtDodatniOpis.Text.Trim();
            string brojKontejnera2 = string.IsNullOrWhiteSpace(txtBrojKontejnera2.Text) ? null : txtBrojKontejnera2.Text.Trim();

            adresaUtovara = string.IsNullOrWhiteSpace(txtAdresaUtovara.Text) ? null : txtAdresaUtovara.Text.Trim();
            adresaIstovara = string.IsNullOrWhiteSpace(txtAdresaIstovara.Text) ? null : txtAdresaIstovara.Text.Trim();

            if (cboMestoUtovara.SelectedValue != null && Uvoz != 0 && int.TryParse(cboMestoUtovara.SelectedValue.ToString(), out int parsedMestoUtovaraID))
                mestoUtovara = parsedMestoUtovaraID;
            else if (!string.IsNullOrWhiteSpace(cboMestoUtovara.Text))
            {
                int mestoU = InsertMestoUtovaraUSifarnik();
                mestoUtovara = mestoU > -1 ? mestoU : (int?)null;
            }

            if (cboMestoIstovara.SelectedValue != null && Uvoz != 1 && int.TryParse(cboMestoIstovara.SelectedValue.ToString(), out int parsedMestoIstovaraID))
                mestoIstovara = parsedMestoIstovaraID;
            else if (!string.IsNullOrWhiteSpace(cboMestoIstovara.Text))
            {
                int mesto = InsertMestoIstovaraUSifarnik();
                mestoIstovara = mesto > -1 ? mesto : (int?)null;
            }

            if (Uvoz != 1)
                referenca = string.IsNullOrWhiteSpace(txtReferenca.Text) ? null : txtReferenca.Text.Trim();

            if (Uvoz != 1 && Uvoz != 0 && cboKlijent.SelectedValue != null && int.TryParse(cboKlijent.SelectedValue.ToString(), out int parsedKlijentID))
                klijent = parsedKlijentID;
            else if (!string.IsNullOrWhiteSpace(cboKlijent.Text))
                klijent = InsertKlijentaUSifarnik();

            if (Uvoz != 1 && Uvoz != 0 && txtBokingBrodara.Text != null && int.TryParse(txtBokingBrodara.Text.ToString(), out int parsedBookingBrodara))
                bookingBrodara = parsedBookingBrodara;

            if (Uvoz != 1 && Uvoz != 0 && decimal.TryParse(txtBrutoK.Text, out decimal parsedBrutoK))
                bttoKontejnera = parsedBrutoK;

            if (Uvoz != 1 && Uvoz != 0 && decimal.TryParse(txtBrutoR.Text, out decimal parsedBrutoR))
                bttoRobe = parsedBrutoR;

            if (Uvoz != 1 && Uvoz != 0)
                brojKontejnera = string.IsNullOrWhiteSpace(txtBrojKontejnera.Text) ? null : txtBrojKontejnera.Text.Trim();

            if (Uvoz != 1 && Uvoz != 0)
                brojVoza = string.IsNullOrWhiteSpace(txtBrojVoza.Text) ? null : txtBrojVoza.Text.Trim();

            if (Uvoz != 0)
                brodskaTeretnica = string.IsNullOrWhiteSpace(txtBL.Text) ? null : txtBL.Text.Trim();

            if (Uvoz != 1)
                brodskaPlomba = string.IsNullOrWhiteSpace(txtBrodskaPlomba.Text) ? null : txtBrodskaPlomba.Text.Trim();

            if (Uvoz != 1 && Uvoz != 0 && !string.IsNullOrWhiteSpace(cboNapomenaPoz.Text))
            {
                int parsedNapomenaPoz = -1;

                if (cboNapomenaPoz.SelectedValue != null &&
                    int.TryParse(cboNapomenaPoz.SelectedValue.ToString(), out int val))
                {
                    parsedNapomenaPoz = val;
                    napomenaPoz = parsedNapomenaPoz;
                }
                else
                {
                    int pozicija = InsertPozicijaUsifarnik();
                    napomenaPoz = pozicija > 0 ? pozicija : (int?)null;
                }
            }

            if (Uvoz != 1 && Uvoz != 0)
                polaznaCarinarnica = cboPolaznaCarinarnica.SelectedValue == null ? (int?)null : Convert.ToInt32(cboPolaznaCarinarnica.SelectedValue);

            if (Uvoz != 1 && Uvoz != 0)
                odredisnaCarinarnica = cboOCarinarnica.SelectedValue == null ? (int?)null : Convert.ToInt32(cboOCarinarnica.SelectedValue);

            if (Uvoz != 1 && Uvoz != 0)
                polaznaSpedicija = cboPolaznaSpedicija.SelectedValue == null ? (int?)null : Convert.ToInt32(cboPolaznaSpedicija.SelectedValue);

            if (Uvoz != 1 && Uvoz != 0)
                odredisnaSpedicija = cbOspedicija.SelectedValue == null ? (int?)null : Convert.ToInt32(cbOspedicija.SelectedValue);

            if (Uvoz != 1 && Uvoz != 0)
                polaznaSpedicijaKontakt = string.IsNullOrWhiteSpace(txtKontaktPolazneSpedicije.Text) ? null : txtKontaktPolazneSpedicije.Text.Trim();

            if (Uvoz != 1 && Uvoz != 0)
                odredisnaSpedicijaKontakt = string.IsNullOrWhiteSpace(txtKontaktOSpedicije.Text) ? null : txtKontaktOSpedicije.Text.Trim();

            int zaposleniID = PostaviVrednostZaposleni();

            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
            InsertRadniNalogInterni updi = new InsertRadniNalogInterni();
            if (status == true)
            {
                if (cboTipNaloga.SelectedValue == null || (int)cboTipNaloga.SelectedValue == -1)
                {
                    MessageBox.Show("Polje 'Tip naloga' je obavezno!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (noviZapisSaNovimNalogID == "NOVINALOG")
                {
                    kreirajNalogID = 1;
                }
                else if (mainNalogID != null && mainNalogID > 0)
                {
                    nalogID = mainNalogID;
                }

                    int noviID = ins.InsRadniNalogDrumski(tipNaloga, kreirajNalogID, nalogID, autoDan, referenca, mestoPreuzimanja, klijent, mestoUtovara, adresaUtovara, mestoIstovara, datumUtovara, datumIstovara, adresaIstovara,
                        dtPreuzimanjaPraznogKont, granicniPrelaz, trosak, valutaID, kamionID, statusID, dodatniOpis, cena, kontaktOsobaistovara, PDV, tipTransportaID, brojVoza, bttoKontejnera, bttoRobe, brojKontejnera, brojKontejnera2,
                        bookingBrodara, brodskaTeretnica, brodskaPlomba, napomenaPoz, polaznaCarinarnica, odredisnaCarinarnica, polaznaSpedicija, odredisnaSpedicija, polaznaSpedicijaKontakt, odredisnaSpedicijaKontakt, zaposleniID, vrstaKontejnera);

                txtID.Text = noviID.ToString();
                lbtHederTekst.Text = "Unos novog zapisa završen!";
                status = false;
                button1.Visible = true;
                button4.Visible = true;
            }
            else
            {
                // 1. Proveri stari status i RadniNalogInterniID
                int? stariStatusID = null;
                int? radniNalogInterniID = null;
                using (var connection = new SqlConnection(Sifarnici.frmLogovanje.connectionString))
                {
                    connection.Open();
                    var cmd = new SqlCommand(@"
                                    SELECT ISNULL(rn.Status, 0) AS Status, ri.ID AS RadniNalogInterniID
                                    FROM RadniNalogDrumski rn
                                    LEFT JOIN RadniNalogInterni ri ON ri.KonkretaIDUsluge = rn.UKID
                                    WHERE rn.ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", iD);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["Status"] != DBNull.Value)
                                stariStatusID = Convert.ToInt32(reader["Status"]);
                            if (reader["RadniNalogInterniID"] != DBNull.Value)
                                radniNalogInterniID = Convert.ToInt32(reader["RadniNalogInterniID"]);
                        }
                    }
                }

                // 2. Učitaj listu završnih statusa
                var statusiZavrsni = new List<int>();
                using (var connection = new SqlConnection(Sifarnici.frmLogovanje.connectionString))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SELECT Vrednost FROM SistemskePostavke WHERE Naziv LIKE 'StatusKamiona%'", connection);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (int.TryParse(reader.GetString(0).Trim(), out int parsed))
                                statusiZavrsni.Add(parsed);
                        }
                    }
                }

                // 3. Update glavnog naloga
                ins.UpdateRadniNalogDrumski(iD, tipNaloga, autoDan, referenca, mestoPreuzimanja, mestoUtovara, adresaUtovara, mestoIstovara, datumUtovara, datumIstovara, adresaIstovara,
                    dtPreuzimanjaPraznogKont, granicniPrelaz, trosak, valutaID, kamionID, statusID, dodatniOpis, cena, kontaktOsobaistovara, PDV, tipTransportaID, bookingBrodara, klijent,
                    bttoKontejnera, bttoRobe, brojVoza, brojKontejnera, brojKontejnera2, brodskaTeretnica, brodskaPlomba, napomenaPoz, polaznaCarinarnica, odredisnaCarinarnica, polaznaSpedicija, odredisnaSpedicija,
                    polaznaSpedicijaKontakt, odredisnaSpedicijaKontakt, zaposleniID, vrstaKontejnera);

                // 4. Ako se status promenio i novi spada u završne onda ide update internog
                if (statusID.HasValue &&
                   (!stariStatusID.HasValue || statusID.Value != stariStatusID.Value))
                {
                    if (statusiZavrsni.Contains(statusID.Value) && radniNalogInterniID.HasValue)
                    {
                        
                        updi.UpdRadniNalogInterniZavrsen(
                            radniNalogInterniID.Value,
                            Saobracaj.Sifarnici.frmLogovanje.user.Trim()
                        );
                    }
                }
            }
        }

        //private void button21_Click(object sender, EventArgs e)
        //{
        //    string referenca = null;
        //    int? mestoIstovara = null;
        //    int? mestoUtovara = null;
        //    int? klijent = null;
        //    decimal? bttoKontejnera = null;
        //    decimal? bttoRobe = null;
        //    string brojVoza = null;
        //    string brojKontejnera = null;
        //    string brodskaPlomba = null;
        //    string brodskaTeretnica = null;
        //    DateTime? datumIstovara = null;
        //    DateTime? datumUtovara = null;
        //    int? bookingBrodara = null;
        //    string adresaIstovara = null;
        //    string adresaUtovara = null;
        //    int? napomenaPoz = null;
        //    string polaznaCarinarnica = null;
        //    string odredisnaCarinarnica = null;
        //    string odredisnaSpedicijaKontakt = null;
        //    string polaznaSpedicijaKontakt = null;

        //    int iD = 0;
        //    if (txtID.Text != null && int.TryParse(txtID.Text, out int parsedID))
        //    {
        //        iD = parsedID;
        //    }
        //    int autoDan = 0;
        //    if (chkAutoDan.Checked == true)
        //    {
        //        autoDan = 1;
        //    }
        //    int PDV = 0;
        //    if (chkPDV.Checked == true)
        //    {
        //        PDV = 1;
        //    }
        //    int? mestoPreuzimanja = null;

        //    if (cboMestoPreuzimanja.SelectedValue != null
        //        && Uvoz != 0
        //        && int.TryParse(cboMestoPreuzimanja.SelectedValue.ToString(), out int parsedMestoPreuzimanjaID))
        //    {
        //        mestoPreuzimanja = parsedMestoPreuzimanjaID;
        //    }
        //    else if (!string.IsNullOrWhiteSpace(cboMestoUtovara.Text))
        //    {
        //        int mestoP = InsertMestoUtovaraUSifarnik();
        //        mestoPreuzimanja = mestoP > -1 ? mestoP : (int?)null;
        //    }
        //    string kontaktOsobaistovara = string.IsNullOrWhiteSpace(txtkontaktNaIstovaru.Text) ? null : txtkontaktNaIstovaru.Text.Trim();

        //    if (dtpUtovara.Checked)
        //    {
        //        datumUtovara = dtpUtovara.Value;
        //    }
        //    if (dtIstovara.Checked)
        //    {
        //        datumIstovara = dtIstovara.Value;
        //    }

        //    DateTime? dtPreuzimanjaPraznogKont = null;
        //    if (dtPreuzimanjaPraznogKontejnera.Checked)
        //    {
        //        dtPreuzimanjaPraznogKont = dtPreuzimanjaPraznogKontejnera.Value;
        //    }
        //    string granicniPrelaz = string.IsNullOrWhiteSpace(txtGranicniPrelaz.Text) ? null : txtGranicniPrelaz.Text.Trim();
        //    decimal? trosak = null;
        //    decimal? cena = null;
        //    if (!string.IsNullOrWhiteSpace(txtTrosak.Text) && decimal.TryParse(txtTrosak.Text, out decimal parsedTrosak))
        //    {
        //        trosak = parsedTrosak;
        //    }
        //    if (!string.IsNullOrWhiteSpace(txtCena.Text) && decimal.TryParse(txtCena.Text, out decimal parsedCena))
        //    {
        //        cena = parsedCena;
        //    }
        //    string valutaID = null;
        //    if (txtValuta.SelectedValue != null)
        //    {
        //        valutaID = txtValuta.SelectedValue.ToString();
        //    }
        //    int? kamionID = null;
        //    if (cboKamion.SelectedValue != null && int.TryParse(cboKamion.SelectedValue.ToString(), out int parsedKamionID))
        //    {
        //        kamionID = parsedKamionID;
        //    }
        //    int? statusID = null;
        //    if (cboStatus.SelectedValue != null && int.TryParse(cboStatus.SelectedValue.ToString(), out int parsedStatusID))
        //    {
        //        statusID = parsedStatusID;
        //    }
        //    int? tipNaloga = null;
        //    if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int parsedTipNaloga))
        //    {
        //        tipNaloga = parsedTipNaloga;
        //    }
        //    int? tipTransportaID = null;
        //    if (cboTipTransporta.SelectedValue != null && int.TryParse(cboTipTransporta.SelectedValue.ToString(), out int parsedTipTransportaID))
        //    {
        //        tipTransportaID = parsedTipTransportaID;
        //    }

        //    string dodatniOpis = string.IsNullOrWhiteSpace(txtDodatniOpis.Text) ? null : txtDodatniOpis.Text.Trim();
        //    string brojKontejnera2 = string.IsNullOrWhiteSpace(txtBrojKontejnera2.Text) ? null : txtBrojKontejnera2.Text.Trim();

        //    adresaUtovara = string.IsNullOrWhiteSpace(txtAdresaUtovara.Text) ? null : txtAdresaUtovara.Text.Trim();
        //    adresaIstovara = string.IsNullOrWhiteSpace(txtAdresaIstovara.Text) ? null : txtAdresaIstovara.Text.Trim();
        //    if (cboMestoUtovara.SelectedValue != null && Uvoz != 0 && int.TryParse(cboMestoUtovara.SelectedValue.ToString(), out int parsedMestoUtovaraID))
        //    {
        //        mestoUtovara = parsedMestoUtovaraID;
        //    }
        //    else if (!string.IsNullOrWhiteSpace(cboMestoUtovara.Text))
        //    {
        //        int mestoU = InsertMestoUtovaraUSifarnik();
        //        mestoUtovara = mestoU > -1 ? mestoU : (int?)null;
        //    }
        //    if (cboMestoIstovara.SelectedValue != null && Uvoz != 1 && int.TryParse(cboMestoIstovara.SelectedValue.ToString(), out int parsedMestoIstovaraID))
        //    {
        //        mestoIstovara = parsedMestoIstovaraID;
        //    }
        //    else if (!string.IsNullOrWhiteSpace(cboMestoIstovara.Text))
        //    {
        //       int mesto = InsertMestoIstovaraUSifarnik();
        //       mestoIstovara = mesto > -1 ? mesto : (int?)null; 
        //    }
        //    if (Uvoz != 1)
        //        referenca = string.IsNullOrWhiteSpace(txtReferenca.Text) ? null : txtReferenca.Text.Trim();
        //    if (Uvoz != 1 && Uvoz != 0 && cboKlijent.SelectedValue != null && int.TryParse(cboKlijent.SelectedValue.ToString(), out int parsedKlijentID))
        //    {
        //        klijent = parsedKlijentID;
        //    }
        //    else if (!string.IsNullOrWhiteSpace(cboKlijent.Text))
        //    { 
        //        klijent = InsertKlijentaUSifarnik();
        //    }
        //    if (Uvoz != 1 && Uvoz != 0 && txtBokingBrodara.Text != null && int.TryParse(txtBokingBrodara.Text.ToString(), out int parsedBookingBrodara))
        //        bookingBrodara = parsedBookingBrodara;
        //    if (Uvoz != 1 && Uvoz != 0 && decimal.TryParse(txtBrutoK.Text, out decimal parsedBrutoK))
        //        bttoKontejnera = parsedBrutoK;
        //    if (Uvoz != 1 && Uvoz != 0 && decimal.TryParse(txtBrutoR.Text, out decimal parsedBrutoR))
        //        bttoRobe = parsedBrutoR;
        //    if (Uvoz != 1 && Uvoz != 0)
        //        brojKontejnera = string.IsNullOrWhiteSpace(txtBrojKontejnera.Text) ? null : txtBrojKontejnera.Text.Trim();
        //    if (Uvoz != 1 && Uvoz != 0)
        //        brojVoza = string.IsNullOrWhiteSpace(txtBrojVoza.Text) ? null : txtBrojVoza.Text.Trim();
        //    if (Uvoz != 0)
        //        brodskaTeretnica = string.IsNullOrWhiteSpace(txtBL.Text) ? null : txtBL.Text.Trim();
        //    if (Uvoz != 1)
        //        brodskaPlomba = string.IsNullOrWhiteSpace(txtBrodskaPlomba.Text) ? null : txtBrodskaPlomba.Text.Trim();
        //    if (Uvoz != 1 && Uvoz != 0 && !string.IsNullOrWhiteSpace(cboNapomenaPoz.Text))
        //    {
        //        int parsedNapomenaPoz = -1;

        //        if (cboNapomenaPoz.SelectedValue != null &&
        //            int.TryParse(cboNapomenaPoz.SelectedValue.ToString(), out int val))
        //        {
        //            // Korisnik je izabrao postojeću stavku
        //            parsedNapomenaPoz = val;
        //            napomenaPoz = parsedNapomenaPoz;
        //        }
        //        else
        //        {
        //            // Korisnik je uneo novu vrednost u combo
        //            int pozicija = InsertPozicijaUsifarnik();
        //            napomenaPoz = pozicija > 0 ? pozicija : (int?)null;
        //        }
        //    }
        //    if (Uvoz != 1 && Uvoz != 0) 
        //        polaznaCarinarnica = string.IsNullOrWhiteSpace(txtCarinjenjeUvozno.Text) ? null : txtCarinjenjeUvozno.Text.Trim();
        //    if (Uvoz != 1 && Uvoz != 0)
        //        odredisnaCarinarnica = string.IsNullOrWhiteSpace(txtOdredisnaCarinarnica.Text) ? null : txtOdredisnaCarinarnica.Text.Trim();
        //    if (Uvoz != 1 && Uvoz != 0)
        //        polaznaSpedicijaKontakt = string.IsNullOrWhiteSpace(txtPolaznaSpedicijaKontakt.Text) ? null : txtPolaznaSpedicijaKontakt.Text.Trim();
        //    if (Uvoz != 1 && Uvoz != 0)
        //        odredisnaSpedicijaKontakt = string.IsNullOrWhiteSpace(txtOdredisnaSpedicijaKontakt.Text) ? null : txtOdredisnaSpedicijaKontakt.Text.Trim();

        //    int zaposleniID = PostaviVrednostZaposleni();

        //    InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
        //    if (status == true)
        //    {
        //        if (cboTipNaloga.SelectedValue == null || (int)cboTipNaloga.SelectedValue == -1)
        //        {
        //            MessageBox.Show("Polje 'Tip naloga' je obavezno!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return; // prekida dalje izvršavanje
        //        }

        //        int noviID = ins.InsRadniNalogDrumski(tipNaloga, autoDan, referenca, mestoPreuzimanja, klijent, mestoUtovara, adresaUtovara, mestoIstovara, datumUtovara, datumIstovara, adresaIstovara,
        //           dtPreuzimanjaPraznogKont, granicniPrelaz, trosak, valutaID, kamionID, statusID, dodatniOpis, cena, kontaktOsobaistovara, PDV, tipTransportaID, brojVoza, bttoKontejnera, bttoRobe, brojKontejnera, brojKontejnera2,
        //           bookingBrodara, brodskaTeretnica, brodskaPlomba, napomenaPoz, polaznaCarinarnica, odredisnaCarinarnica, polaznaSpedicijaKontakt, odredisnaSpedicijaKontakt, zaposleniID);

        //        txtID.Text = noviID.ToString();
        //        status = false;
        //    }
        //    else
        //    {
        //        ins.UpdateRadniNalogDrumski(iD, autoDan, referenca, mestoPreuzimanja, mestoUtovara, adresaUtovara, mestoIstovara, datumUtovara, datumIstovara, adresaIstovara,
        //           dtPreuzimanjaPraznogKont, granicniPrelaz, trosak, valutaID, kamionID, statusID, dodatniOpis, cena, kontaktOsobaistovara, PDV, tipTransportaID, bookingBrodara, klijent, bttoKontejnera, bttoRobe, brojVoza,
        //           brojKontejnera, brojKontejnera2, brodskaTeretnica, brodskaPlomba, napomenaPoz, polaznaCarinarnica, odredisnaCarinarnica, polaznaSpedicijaKontakt, odredisnaSpedicijaKontakt, zaposleniID);
        //    }
        //    if()
        //}

        public int InsertKlijentaUSifarnik()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int newKlijentID = -1;

            con.Open();
            string klijent = cboKlijent.Text.Trim().ToUpper();
            SqlCommand cmd = new SqlCommand("Select PaSifra,PaNaziv " +
                "FROM Partnerji " +
                "WHERE DrumskiPrevoz = 1 AND UPPER(LTRIM(RTRIM(PaNaziv))) LIKE UPPER(@Klijent)", con);
 
            cmd.Parameters.AddWithValue("@Klijent", klijent);
            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                InsertPartnerji ins = new InsertPartnerji();
                newKlijentID =  ins.InsPartneri(cboKlijent.Text.Trim(), null, null, null, null, null, null, null, null, null, null, null, false, false, false, 0, 0, 0, 0, 0, 0, 0, null, null, null, null, null, 0, 0, 0, 0, null, null, null, null, 0, 0, 1, null);
            }
            return newKlijentID;
        }

        public int InsertMestoIstovaraUSifarnik()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int mestoIstovara = -1;
            con.Open();
            string mesto = cboMestoIstovara.Text.Trim().ToUpper();
            SqlCommand cmd = new SqlCommand("SELECT ID, Naziv  " +
                "FROM MestaUtovara " +
                "WHERE  UPPER(LTRIM(RTRIM(Naziv))) LIKE UPPER(@mesto)", con);

            cmd.Parameters.AddWithValue("@mesto", mesto);
            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                InsertMestaUtovara ins = new InsertMestaUtovara();
                mestoIstovara = ins.InsMestaUtovara(cboMestoIstovara.Text.Trim(), null);
            }
            return mestoIstovara;
        }


        public int InsertMestoUtovaraUSifarnik()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int mestoIstovara = -1;

            con.Open();
            string mesto = cboMestoUtovara.Text.Trim().ToUpper();
            SqlCommand cmd = new SqlCommand("SELECT ID, Naziv  " +
                "FROM   MestaUtovara " +
                "WHERE  UPPER(LTRIM(RTRIM(Naziv))) LIKE UPPER(@mesto)", con);

            cmd.Parameters.AddWithValue("@mesto", mesto);
            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                InsertMestaUtovara ins = new InsertMestaUtovara();
                mestoIstovara = ins.InsMestaUtovara(cboMestoUtovara.Text.Trim(), null);
            }
            return mestoIstovara;
        }

        public int InsertPozicijaUsifarnik()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int pozicijaID = -1;

            con.Open();
            string napomena = cboNapomenaPoz.Text.Trim().ToUpper();
            SqlCommand cmd = new SqlCommand("Select ID,Napomena " +
                "FROM DrumskiPozicioniranje " +
                "WHERE UPPER(LTRIM(RTRIM(Napomena))) LIKE UPPER(@Napomena)", con);

            cmd.Parameters.AddWithValue("@Napomena", napomena);
            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                InsertNapomena ins = new InsertNapomena();
                pozicijaID = ins.InsNapomena(cboNapomenaPoz.Text.Trim());
            }
            return pozicijaID;
        }

        private void ResetujVrednostiPolja()
        {
            Uvoz = -1;
            cboTipNaloga.Visible = true;
            txtTipNaloga1.Visible = false;
            txtID.Text = "";
            txtID.Enabled = false;
            cboStatus.SelectedValue = -1;
            cboTipNaloga.SelectedValue = -1;
            cboTipNaloga.Enabled = true;
            txtBokingBrodara.Text = "";
            txtBokingBrodara.Enabled = true;
            cboMestoPreuzimanja.SelectedValue = -1;
           // txtMestoPreuzimanja.Text = "Leget";
            cboKlijent.SelectedValue = -1;
            cboKlijent.Enabled = true;
            cboVrstaKontejnera.SelectedValue = -1;
            cboVrstaKontejnera.Enabled = true;
            cboMestoUtovara.SelectedValue = 8;
            cboMestoUtovara.Enabled = true;
           // txtAdresaUtovara.Text = "Jarački put";
            txtAdresaUtovara.Text = "";
            txtAdresaUtovara.Enabled = true;
            txtAdresaIstovara.Enabled = true;

            cboMestoIstovara.SelectedValue = -1;
            cboMestoIstovara.Enabled = true;
            //FillAdresaIstovara();
            //FillAdresaUtovara();
            txtBrutoK.Value = 0.00m;
            txtBrutoK.Enabled = true;
            txtBrutoR.Value = 0.00m;
            txtBrutoR.Enabled = true;
            cboPolaznaCarinarnica.Text = "";
            cboOCarinarnica.Text = "";
            txtReferenca.Text = "";
            txtReferenca.Enabled = true;
            txtBL.Text = "";
            cboVrstaKontejnera.SelectedValue = -1;
            txtBrojKontejnera.Text = "";
            txtBrojKontejnera.Enabled = true;
            txtBrojKontejnera2.Text = "";
            txtBrojKontejnera2.Enabled = true;
            txtBrodskaPlomba.Text = "";
            txtBrodskaPlomba.Enabled = true;
            dtpUtovara.Value = DateTime.Now;
            dtIstovara.Value = DateTime.Now;
            txtKontaktPolazneSpedicije.Text = "";
            txtKontaktOSpedicije.Text = "";
            chkAutoDan.Checked = false;
            txtBrojVoza.Text = "";
            txtBrojVoza.Enabled = true;
            dtPreuzimanjaPraznogKontejnera.Value = DateTime.Now;
            cboKamion.SelectedValue = -1;
            txtVozac.Text = "";
            txtVozac.Enabled = false;
            txtBrojLK.Text = "";
            txtBrojLK.Enabled = false;
            txtBrojTelefona.Text = "";
            txtBrojTelefona.Enabled = false;
            txtPrevoznik.Text = "";
            txtPrevoznik.Enabled = false;
            txtValuta.SelectedValue = "EUR";
            txtTrosak.Value = 0.00m;
            txtCena.Value = 0.00m;
            chkPDV.Checked = false;
            txtkontaktNaIstovaru.Text = "";
            txtGranicniPrelaz.Text = "";
            txtNapomenaPoz.Visible = false;
            cboNapomenaPoz.Visible = true;
            cboNapomenaPoz.SelectedValue = -1;
            txtDodatniOpis.Text = "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            status = true;
            Uvoz = -1;
            FillCombo();
            ResetujVrednostiPolja();
            button21.Visible = true;
            lbtHederTekst.Text = "UNOS NOVOG ZAPISA JE U TOKU!";
            lbtHederTekst.Visible = true;
            button1.Visible = false;
            button4.Visible = false;
        }

        private void cboTipTransporta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipTransporta.SelectedValue != null && int.TryParse(cboTipTransporta.SelectedValue.ToString(), out int tipTransportaId) && tipTransportaId > 0)
            {
                UcitajKamione(tipTransportaId);
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT	rn.KamionID " +
                 "FROM    RadniNalogDrumski rn " +
                 "INNER JOIN Automobili a on a.ID = rn.KamionID " +
                 "where rn.ID=" + id + " AND a.VoziloDrumskog =  " + cboTipTransporta.SelectedValue, con);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cboKamion.SelectedValue = Convert.ToInt32(dr["KamionID"]);
                }
            }
            else
            {
                UcitajKamione(null); // Ako nije validan ID, učitaj sve kamione bez filtera
            }
        }

        private void btnKontaktOsobe_Click(object sender, EventArgs e)
        {
            using (var detailForm = new Izvoz.frmKontaktOsobeMU(txtAdresaIstovara.Text))
            {
                detailForm.ShowDialog();
                txtkontaktNaIstovaru.Text = detailForm.GetSviKontaktiPoAdresi(txtAdresaIstovara.Text);
            }
        }

        private void cboKamion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboKamion.SelectedValue != null && int.TryParse(cboKamion.SelectedValue.ToString(), out int parsedKamionId) && parsedKamionId > 0)
            {
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT	 LicnaKarta, Vozac,BrojTelefona, p.PaNaziv AS Prevoznik " +
                 "FROM   Automobili a " + 
                 "LEFT join Partnerji p on  a.PartnerID = p.PaSifra  " +
                 "where ID =  " + parsedKamionId, con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txtBrojLK.Text = dr["LicnaKarta"].ToString();
                    txtVozac.Text = dr["Vozac"].ToString();
                    txtBrojTelefona.Text = dr["BrojTelefona"].ToString();
                    txtPrevoznik.Text = dr["Prevoznik"].ToString();
                }
            }
            else
            {
                txtBrojLK.Text = "";
                txtVozac.Text = "";
                txtBrojTelefona.Text = "";
                txtPrevoznik.Text = "";
            }
        }

        private void KreirajPdf(string putanjaFajla)
        {
            using (FileStream fs = new FileStream(putanjaFajla, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                doc.Add(new Paragraph("Ovo je automatski generisan izveštaj."));
                doc.Add(new Paragraph("Datum: " + DateTime.Now.ToString("dd.MM.yyyy")));
                doc.Close();
            }
        }

        private void PosaljiEmailSaPrilogom(string primalac,string korisnickiEmail, string korisnickaLozinka, string putanjaFajla)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(korisnickiEmail);
            mail.To.Add(primalac);
            mail.Subject = "Izveštaj u PDF formatu";
            mail.Body = "U prilogu se nalazi automatski generisan PDF.";
            string Trosak = txtTrosak.Text.Trim();
            string BrojKontejnera = txtBrojKontejnera.Text.Trim();
            string Cena = txtCena.Text.Trim();

            string htmlBody = $@"
            <html>
            <body>
            <h2 style='color:#2a7ae2;'>Podaci sa forme</h2>
            <table border='1' cellpadding='6' cellspacing='0' style='border-collapse:collapse; font-family:Arial; font-size:14px;'>
                <tr><th align='left'>Trosak</th><td>{Trosak}</td></tr>
                <tr><th align='left'>Broj Kontejnera</th><td>{BrojKontejnera}</td></tr>
                <tr><th align='left'>Cena</th><td>{Cena}</td></tr>
            </table>
            </body>
            </html>";

            mail.IsBodyHtml = true;
            mail.Body = htmlBody;

            Attachment attachment = new Attachment(putanjaFajla);
            mail.Attachments.Add(attachment);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(korisnickiEmail, korisnickaLozinka); // 
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Timeout = 20000;
            smtp.Send(mail);
        }

        //private void GenerisiIPosaljiMail_Click(object sender, EventArgs e)
        //{
        //    frmEmailLogin em = new frmEmailLogin();
    
        //    if (em.ShowDialog() == DialogResult.OK)
        //    {
        //        string korisnickiEmail = em.EmailAdresa;
        //        string korisnickaLozinka = em.Lozinka;

        //        try
        //        {
        //            // 1. Generiši privremeni PDF fajl
        //            string tempPdfPath = Path.Combine(Path.GetTempPath(), "Izvestaj_" + Guid.NewGuid() + ".pdf");
        //            KreirajPdf(tempPdfPath);

        //            // 2. Pošalji email
        //            string emailAdresaPrimaoca = "";
        //            if (!string.IsNullOrEmpty(emailAdresaPrimaoca))
        //            {
        //                PosaljiEmailSaPrilogom(emailAdresaPrimaoca, korisnickiEmail, korisnickaLozinka, tempPdfPath);
        //                MessageBox.Show("PDF je poslat na: " + emailAdresaPrimaoca);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Unesite email adresu.");
        //            }

        //            // 3. (opciono) Obriši temp fajl posle slanja
        //            if (File.Exists(tempPdfPath))
        //                File.Delete(tempPdfPath);
        //        }
        //        catch (Exception ex)
        //        {
        //            //MessageBox.Show("Greška: " + ex.Message);
        //            MessageBox.Show("Greška: " + ex.Message +
        //    (ex.InnerException != null ? "\nDetalji: " + ex.InnerException.Message : ""));
        //        }
        //    }
        //}

        private void cboTipNaloga_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int tipNalogaId) && tipNalogaId > 0)
            {
                if ((tipNalogaId == 3 || tipNalogaId == 5) )
                {
                    label12.Text = "Kontakt osoba na utovaru";
                    if (status == true)
                    {
                        cboMestoPreuzimanja.SelectedValue = -1;
                        cboMestoUtovara.SelectedValue = -1;
                        txtAdresaUtovara.Text = "";
                    }

                }
                else
                {
                    label12.Text = "Kontakt osoba na istovaru";
                    if (tipNalogaId == 2 && status == true)
                    {
                        cboMestoPreuzimanja.SelectedValue = 8;
                        cboMestoUtovara.SelectedValue = 8;
                        txtAdresaUtovara.Text = "Jarački put";
                    }
                    else if( status == true)
                    {
                        cboMestoPreuzimanja.SelectedValue = -1;
                        cboMestoUtovara.SelectedValue = -1;
                        txtAdresaUtovara.Text = "";
                    }

                }
            }
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int parsedTipNaloga) &&
                 (parsedTipNaloga == 2 || parsedTipNaloga == 3 || parsedTipNaloga == 4 || parsedTipNaloga == 5))
            {
                DialogResult result = MessageBox.Show(
                         "Da li ste sigurni da želite da obrišete ovaj zapis?",
                         "Potvrda brisanja",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
    );

                if (result == DialogResult.Yes)
                {
                    // Pozovi metodu za brisanje
                    InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                    ins.DelRadniNalogDrumski(Convert.ToInt32(txtID.Text));
                    txtID.Text = "0";
                    ResetujVrednostiPolja();
                }
            }
            else
            {
                MessageBox.Show("Ovaj nalog nije moguće obrisati.");
                return;
            }    
        }

        private void btnFormiranjeNaloga_Click(object sender, EventArgs e)
        {
            InsertRadniNalogDrumski isu = new InsertRadniNalogDrumski();
            // ostaje logika sa listom da ne bih pravila novu metodu za kreiranje naloga id, u ovom slucaju je jedna stavka jedan nalogId
            List<int> stavke = new List<int>();
            int ID = Convert.ToInt32(txtID.Text.Trim());
            stavke.Add((ID));
            isu.PostaviNalogIDNaRedove(stavke);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          
            if (!int.TryParse(txtID.Text, out int radniNalogDrumskiID))
            {
                MessageBox.Show("ID ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int zaposleniID = PostaviVrednostZaposleni();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Odaberite fajl za upload";
            ofd.Filter = "Svi fajlovi|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string izabraniFajl = ofd.FileName;
                string ekstenzija = Path.GetExtension(izabraniFajl);
                string cleanName = Path.GetFileNameWithoutExtension(izabraniFajl);

                // Očisti naziv fajla od nedozvoljenih karaktera
                string nazivFajla = string.Join("_", cleanName.Split(Path.GetInvalidFileNameChars())) + ekstenzija;

                // Putanja na server
                string targetPath = $@"\\192.168.99.10\Leget\Drumski\Dokumenta\ID_{radniNalogDrumskiID}";
                string destinacija = Path.Combine(targetPath, nazivFajla);

                try
                {
                    // Ako ne postoji folder, napravi ga
                    if (!Directory.Exists(targetPath))
                        Directory.CreateDirectory(targetPath);

                    // Provera da li fajl već postoji
                    if (File.Exists(destinacija))
                    {
                        DialogResult result = MessageBox.Show("Fajl sa istim imenom već postoji. Da li želite da ga zamenite?",
                                                              "Upozorenje",
                                                              MessageBoxButtons.YesNo,
                                                              MessageBoxIcon.Warning);

                        if (result != DialogResult.Yes)
                            return; // korisnik ne želi da zameni fajl
                    }

                    // Kopiraj fajl
                    File.Copy(izabraniFajl, destinacija, true);

                    // Snimi u bazu
                    InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                    ins.SnimiUFajlBazu(radniNalogDrumskiID, nazivFajla, destinacija, zaposleniID);

                    MessageBox.Show("Fajl uspešno sačuvan i evidentiran u bazi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška prilikom kopiranja fajla: " + ex.Message);
                }
            }
        }
        private int PostaviVrednostZaposleni()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int ulogovaniZaposleniID = 0;

            con.Open();

            SqlCommand cmd = new SqlCommand("Select  k.DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Zaposleni " +
                                            " FROM Korisnici k " +
                                            "INNER JOIN Delavci d ON k.DeSifra = d.DeSifra " +
                                            "where Trim(Korisnik) like '" + Saobracaj.Sifarnici.frmLogovanje.user.Trim() + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["ID"] != DBNull.Value)
                    ulogovaniZaposleniID = Convert.ToInt32(dr["ID"].ToString());
            }
            return ulogovaniZaposleniID;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            if (!int.TryParse(txtID.Text, out int radniNalogID))
            {
                MessageBox.Show("ID ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }        

            frmPregledFajlova pregled = new frmPregledFajlova(radniNalogID);
            pregled.ShowDialog();
            
        }

        private void txtBrutoR_Enter(object sender, EventArgs e)
        {
            if (txtBrutoR.Value == 0)
            {
                txtBrutoR.ResetText(); // obriše prikazani tekst, ali ne menja Value
            }
        }

        private void txtBrutoK_Enter(object sender, EventArgs e)
        {
            if (txtBrutoK.Value == 0)
            {
                txtBrutoK.ResetText(); // obriše prikazani tekst, ali ne menja Value
            }
        }

        private void cboOCarinarnica_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void txtCena_Enter(object sender, EventArgs e)
        {
            if (txtCena.Value == 0)
            {
                txtCena.ResetText(); // obriše prikazani tekst, ali ne menja Value
            }
        }

        private void txtTrosak_Enter(object sender, EventArgs e)
        {
            if (txtTrosak.Value == 0)
            {
                txtTrosak.ResetText(); // obriše prikazani tekst, ali ne menja Value
            }
        }

        private bool isSearching = false;
        //private void cboPolaznaCarinarnica_TextChanged(object sender, EventArgs e)
        //{
        //    // 1. Guard Flag: Sprečava rekurziju
        //    if (isSearching)
        //    {
        //        return;
        //    }

        //    string searchText = cboPolaznaCarinarnica.Text;

        //    if (cboPolaznaCarinarnica.Focused && !string.IsNullOrEmpty(searchText))
        //    {
        //        string lowerSearchText = searchText.ToLower();

        //        // Postavljamo flag na true
        //        isSearching = true;

        //        bool found = false;

        //        // Prolazimo kroz sve stavke
        //        foreach (DataRowView item in cboPolaznaCarinarnica.Items)
        //        {
        //            string itemText = item["Naziv"].ToString().ToLower();

        //            if (itemText.StartsWith(lowerSearchText))
        //            {
        //                // **2. POZICIONIRANJE:**
        //                cboPolaznaCarinarnica.SelectedIndex = cboPolaznaCarinarnica.Items.IndexOf(item);

        //                // Vraćamo originalno ukucan tekst i kursor
        //                cboPolaznaCarinarnica.Text = searchText;
        //                cboPolaznaCarinarnica.SelectionStart = searchText.Length;

        //                found = true;
        //                break; // Prekidamo pretragu nakon pronalaska prvog podudaranja
        //            }
        //        }

        //        // **3. OTVARANJE COMBOBOX-A:**
        //        // Otvori padajuću listu samo ako je pronađeno podudaranje ILI ako želite da se uvek otvori
        //        // dok se kuca (ja predlažem da se uvek otvori ako je tekst prisutan).
        //        if (!cboPolaznaCarinarnica.DroppedDown)
        //        {
        //            // Ova linija otvara padajuću listu
        //            cboPolaznaCarinarnica.DroppedDown = true;
        //        }

        //        // Resetujemo flag na false nakon sto smo zavrsili programsku promenu
        //        isSearching = false;
        //    }
        //    else if (string.IsNullOrEmpty(searchText))
        //    {
        //        // Ako se tekst obriše, možemo zatvoriti padajuću listu
        //        cboPolaznaCarinarnica.DroppedDown = false;

        //        // Takođe resetujte selekciju kada je polje prazno
        //        cboPolaznaCarinarnica.SelectedIndex = -1;
        //    }
        //}

        private void cboPolaznaCarinarnica_TextChanged(object sender, EventArgs e)
        {
            ComboBox_SearchAndPosition(sender);
        }


        private void cboMestoUtovara_TextChanged(object sender, EventArgs e)
        {
            ComboBox_SearchAndPosition(sender);
        }

        private void cboMestoIstovara_TextChanged(object sender, EventArgs e)
        {
            ComboBox_SearchAndPosition(sender);
        }

        private void cboOCarinarnica_TextChanged(object sender, EventArgs e)
        {
            ComboBox_SearchAndPosition(sender);
        }

        private void cboMestoPreuzimanja_KeyUp(object sender, KeyEventArgs e)
        {
            ComboBox_SearchAndPosition_OnKeyUp(sender, e);
        }

        private void cboKlijent_TextChanged(object sender, EventArgs e)
        {
            ComboBox_SearchAndPosition(sender);
        }

        private void cboMestoUtovara_KeyUp(object sender, KeyEventArgs e)
        {
            ComboBox_SearchAndPosition(sender);
        }

        private void cboPolaznaSpedicija_TextChanged(object sender, EventArgs e)
        {
            ComboBox_SearchAndPosition(sender);
        }

        private void cbOspedicija_TextChanged(object sender, EventArgs e)
        {
            ComboBox_SearchAndPosition(sender);
        }
    }
}