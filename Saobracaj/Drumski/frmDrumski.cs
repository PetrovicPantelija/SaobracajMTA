using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Ajax.Utilities;
using Saobracaj.Dokumenta.TrainListItem;
using Saobracaj.Pantheon_Export;
using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmDrumski : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int? Uvoz = null;
        int id = 0;
        bool status = false;

        public frmDrumski()
        {
            InitializeComponent();
            ChangeTextBox();
            FillCombo();
            this.BindingContext = new BindingContext();
            VratiPodatke();
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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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


                foreach (Control control in this.Controls)
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        public frmDrumski(int ID)
        {
            this.id = ID;
            InitializeComponent();
            FillCombo();
            this.BindingContext = new BindingContext();
            VratiPodatke();
            ChangeTextBox();
        }
        private void VratiPodatke()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT	rn.ID ," +
             "rn.NalogID, rn.Uvoz, rn.KontejnerID, rn.Status, rn.IDVrstaManipulacije,  rn.AutoDan, rn.Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "ik.Klijent3 AS Klijent, ik.MesoUtovara AS MestoUtovara, ik.KontaktOsoba as KontaktOsobaUtovarInt, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara, rn.MestoIstovara AS MestoIstovara, (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, rn.DatumUtovara, rn.DatumIstovara, rn.AdresaIstovara,  " +
             "rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz, CAST(ik.Spedicija AS nvarchar) AS KontaktSpeditera, " +
             "rn.Trosak, rn.Valuta, ik.BookingBrodara,  ik.BrojKontejnera,rn.BrojKontejnera2, ik.BrodskaPlomba AS BrojPlombe,  '' AS BrodskaTeretnica,  " +
             " ik.VGMBrod AS BTTKontejnetra, ik.BrutoRobe AS BTTRobe, " +
             "ik.NapomenaZaRobu as NapomenaZaPozicioniranje, a.RegBr,rn.KamionID , a.LicnaKarta, a.Vozac, a.BrojTelefona, rn.Cena, cc.Naziv AS CarinjenjeIzvozno,CAST(ik.Cirada AS VARCHAR) as TipTransporta," +
             "(ccp.Oznaka + ' ' + ccp.Naziv) AS NapomenaCarinskiPostupak , '' AS OdredisnaCarina, '' as OdredisnaSpedicija, '' AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
             "FROM    RadniNalogDrumski rn " +
                      "INNER JOIN IzvozKonacna ik ON rn.KontejnerID = ik.ID " +
                      "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = ik.MesoUtovara AND pko.PaKOZapSt = ik.KontaktOsoba " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                      "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = ik.NapomenaReexport " +
                      "LEFT JOIN Carinarnice cc on cc.ID = ik.MestoCarinjenja " +
                      "LEFT JOIN IzvozKonacnaZaglavlje ukz ON ukz.ID = ik.IDNadredjena " +
                      "LEFT JOIN Voz v ON v.ID = ukz.IDVoza " +
             "where rn.ID=" + id + " AND rn.Uvoz = 0 " +
             "UNION " +
             "SELECT	rn.ID ," +
             "rn.NalogID, rn.Uvoz, rn.KontejnerID, rn.Status, rn.IDVrstaManipulacije, rn.AutoDan, rn.Ref, rn.MestoPreuzimanjaKontejnera, " +
             "i.Klijent3 AS Klijent,  i.MesoUtovara AS MestoUtovara,i.KontaktOsoba  as KontaktOsobaUtovarInt, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,rn.MestoIstovara AS MestoIstovara, (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, rn.DatumUtovara, rn.DatumIstovara, rn.AdresaIstovara, " +
             "rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz,CAST(i.Spedicija AS nvarchar) AS KontaktSpeditera, " +
             "rn.Trosak, rn.Valuta, i.BookingBrodara,  i.BrojKontejnera,rn.BrojKontejnera2, i.BrodskaPlomba AS BrojPlombe, '' AS BrodskaTeretnica,  " +
             " i.VGMBrod AS BTTKontejnetra,  i.BrutoRobe AS BTTRobe, " +
             "i.NapomenaZaRobu AS NapomenaZaPozicioniranje, a.RegBr, rn.KamionID,  a.LicnaKarta, a.Vozac, a.BrojTelefona, rn.Cena, cc.Naziv AS CarinjenjeIzvozno, CAST(i.Cirada AS VARCHAR) as TipTransporta," +
             "(ccp.Oznaka + ' ' + ccp.Naziv) AS NapomenaCarinskiPostupak , '' AS  OdredisnaCarina, '' as OdredisnaSpedicija, '' AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, '' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
             "FROM    RadniNalogDrumski rn " +
                      "INNER JOIN  Izvoz i ON rn.KontejnerID = i.ID  " +
                       "LEFT JOIN partnerjiKontOsebaMU pko ON  pko.PaKOSifra = i.MesoUtovara AND pko.PaKOZapSt = i.KontaktOsoba " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                      "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = i.NapomenaReexport " +
                      "LEFT JOIN Carinarnice cc on cc.ID = i.MestoCarinjenja " +
             "where rn.ID=" + id + " AND rn.Uvoz = 0 " +
             "UNION " +
             "SELECT rn.ID ," +
             "rn.NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.AutoDan,uk.Ref3 AS Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "uk.Nalogodavac3 AS Klijent,  rn.MestoUtovara,-1 as KontaktOsobaUtovarInt, rn.AdresaUtovara,uk.MestoIstovara AS MestoIstovara,uk.KontaktOsobe as KontaktOsobaUtovarIstovar, rn.DatumUtovara,rn.DatumIstovara, (Rtrim(pko.PaKOOpomba)) AS AdresaIstovara, " +
             "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,0 AS BookingBrodara,  uk.BrojKontejnera,rn.BrojKontejnera2, '' AS BrojPlombe,  uk.BrodskaTeretnica,  " +
             " uk.BrutoKontejnera AS BTTKontejnetra, uk.BrutoRobe AS BTTRobe," +
             " np.Naziv as NapomenaZaPozicioniranje, a.RegBr, rn.KamionID,  a.LicnaKarta, a.Vozac, a.BrojTelefona , rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, pr.Naziv as TipTransporta, " +
             "'' AS NapomenaCarinskiPostupak,c.Naziv as OdredisnaCarina ,p2.PaNaziv as OdredisnaSpedicija,  rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta AS TipTransportaDrumski " +
             "FROM  RadniNalogDrumski rn " +
                    "INNER JOIN UvozKonacna uk ON rn.KontejnerID = uk.ID " +
                    "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = uk.MestoIstovara AND PaKOZapSt = uk.AdresaMestaUtovara " + /*AND PaKOSifra = mu.Naziv*/
                    "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = uk.NapomenaZaPozicioniranje " +
                    "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                    "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = uk.PostupakSaRobom " +
                    "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = uk.CarinskiPostupak " +
                    "LEFT JOIN Carinarnice c on c.ID = uk.OdredisnaCarina " +
                    "LEFT JOIN Partnerji p2 on p2.PaSifra = uk.OdredisnaSpedicija " +
                    "LEFT JOIN UvozKonacnaZaglavlje ukz ON ukz.ID = uk.IDNadredjeni " +
                    "LEFT JOIN Voz v ON v.ID = ukz.IDVoza " +
             "where rn.ID= " + id + " AND rn.Uvoz = 1 " +
             "UNION " +
             "SELECT rn.ID ," +
             "rn.NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.AutoDan,u.Ref3 AS Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "u.Nalogodavac3 AS Klijent, rn.MestoUtovara, -1 as KontaktOsobaUtovarInt, rn.AdresaUtovara,u.MestoIstovara AS MestoIstovara,u.KontaktOsobe as KontaktOsobaUtovarIstovar, rn.DatumUtovara,rn.DatumIstovara,(Rtrim(pko.PaKOOpomba)) AS AdresaIstovara,  " +
             "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,0 AS BookingBrodara,  u.BrojKontejnera,rn.BrojKontejnera2, '' AS BrojPlombe,  u.BrodskaTeretnica,   " +
             "u.BrutoKontejnera AS BTTKontejnetra, u.BrutoRobe AS BTTRobe, " +
             " np.Naziv as NapomenaZaPozicioniranje, a.RegBr, rn.KamionID, a.LicnaKarta, a.Vozac, a.BrojTelefona, rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, pr.Naziv as TipTransporta," +
             " '' AS NapomenaCarinskiPostupak, c.Naziv as OdredisnaCarina, p2.PaNaziv as OdredisnaSpedicija, rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV,'' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
             "FROM  RadniNalogDrumski rn " +
                    "INNER JOIN  Uvoz u ON rn.KontejnerID = u.ID " +
                    "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = u.MestoIstovara AND pko.PaKOZapSt = u.AdresaMestaUtovara " + /*AND PaKOSifra = mu.Naziv*/
                    "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = u.NapomenaZaPozicioniranje " +
                    "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                    "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = u.PostupakSaRobom " +
                    "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = u.CarinskiPostupak " +
                    "LEFT JOIN Carinarnice c on c.ID = u.OdredisnaCarina " +
                    "LEFT JOIN Partnerji p2 on p2.PaSifra = u.OdredisnaSpedicija " +
             "where rn.ID= " + id + " AND rn.Uvoz = 1" +
             "UNION " +
             "SELECT rn.ID ," +
             "rn.NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.AutoDan,rn.Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "rn.Klijent, rn.MestoUtovara, -1 as KontaktOsobaUtovarInt, rn.AdresaUtovara,rn.MestoIstovara AS MestoIstovara,rn.KontaktOsobaNaIstovaru AS KontaktOsobaUtovarIstovar, rn.DatumUtovara,rn.DatumIstovara,rn.AdresaIstovara AS AdresaIstovara,  " +
             "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,rn.BookingBrodara,  rn.BrojKontejnera,rn.BrojKontejnera2, rn.BrodskaPlomba AS BrojPlombe,   rn.BrodskaTeretnica,  " +
             " rn.BrutoKontejnera AS BTTKontejnetra, rn.BrutoRobe AS BTTRobe,  " +
             "rn.NapomenaZaPozicioniranje as NapomenaZaPozicioniranje, a.RegBr, rn.KamionID, a.LicnaKarta, a.Vozac, a.BrojTelefona, rn.Cena,'' as CarinjenjeIzvozno, '' as TipTransporta," +
             " '' AS NapomenaCarinskiPostupak, '' as OdredisnaCarina,'' as OdredisnaSpedicija, rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV,rn.BrojVoza as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski " +
             "FROM  RadniNalogDrumski rn " +
              "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
             "where rn.ID= " + id + " AND rn.Uvoz in (-1,2,3)", con);

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
                txtMestoPreuzimanja.Text = dr["MestoPreuzimanjaKontejnera"].ToString();

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
                //FillAdresaUtovara();
                //FillAdresaIstovara();
                //if (dr["AdresaIstovara"] != DBNull.Value && int.TryParse(dr["AdresaIstovara"].ToString(), out int parsedAdresaIstovaraID))
                //    cboAdresaIstovara.SelectedValue = parsedAdresaIstovaraID;
                //else
                //    cboAdresaIstovara.SelectedIndex = -1;
                //if (dr["AdresaUtovara"] != DBNull.Value && int.TryParse(dr["AdresaUtovara"].ToString(), out int parsedAdresaUtovaraID))
                //    cboAdresaUtovara.SelectedValue = parsedAdresaUtovaraID;
                //else
                //    cboAdresaUtovara.SelectedIndex = -1;
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

                txtNapomenaPoz.Text = dr["NapomenaZaPozicioniranje"].ToString();
                txtVozac.Text = dr["Vozac"].ToString();
                txtBrojTelefona.Text = dr["BrojTelefona"].ToString();
                txtBrojLK.Text = dr["LicnaKarta"].ToString();
                txtGranicniPrelaz.Text = dr["GranicniPrelaz"].ToString();
                //    txtKontaktOsobeSpeditera.Text = dr["KontaktSpeditera"].ToString();
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


                if (dr["KamionID"] != DBNull.Value)
                    cboKamion.SelectedValue = (dr["KamionID"].ToString());

                txtDodatniOpis.Text = dr["DodatniOpis"].ToString();
                txtOdredisnaCarinarnica.Text = dr["OdredisnaCarina"].ToString();
                //txtSpediterCarinarnice.Text = dr["OdredisnaSpedicija"].ToString();
                //txtCarinjenjeUvozno.Text = dr["NapomenaCarinskiPostupak"].ToString();
                //  txtkontaktNaIstovaru.Text = dr["KontaktNaIstovaru"].ToString();   PROVERI!!!!!
                if (dr["Cena"] != DBNull.Value)
                    txtCena.Value = Convert.ToDecimal(dr["Cena"].ToString());
                txtVozac.Enabled = false;
                txtBrojLK.Enabled = false;
                txtBrojTelefona.Enabled = false;
                if (Uvoz == 0)
                {
                    txtBokingBrodara.Enabled = false;
                    // label18.Visible = false;  // labela BL
                    txtBL.Enabled = false;
                    cboMestoUtovara.Enabled = false;
                    txtAdresaUtovara.Enabled = false;
                    cboKlijent.Enabled = false;
                    txtBrutoK.Enabled = false;
                    txtBrutoR.Enabled = false;
                    txtNapomenaPoz.Enabled = false;
                    txtBrojVoza.Enabled = false;
                    cboTipNaloga.Visible = false;
                    txtTipNaloga1.Visible = true;
                    txtBrodskaPlomba.Enabled = false;
                    txtBrojKontejnera.Enabled = false;
                    label12.Text = "Kontakt osoba na utovaru";
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
                    txtBrutoK.Enabled = false;
                    txtBrutoR.Enabled = false;
                    txtNapomenaPoz.Enabled = false;
                    txtBrojVoza.Enabled = false;
                    cboTipNaloga.Visible = false;
                    txtTipNaloga1.Visible = true;
                    txtBrojKontejnera.Enabled = false;
                    label12.Text = "Kontakt osoba na istovaru";
                }
                else if (Uvoz == 2)
                {
                    label12.Text = "Kontakt osoba na istovaru";
                    cboTipNaloga.Visible = true;
                    txtTipNaloga1.Visible = false;
                    cboKlijent.Enabled = true;
                    button21.Visible = true;
                }
                else if (Uvoz == 3)
                {
                    label12.Text = "Kontakt osoba na utovaru";
                    cboTipNaloga.Visible = true;
                    txtTipNaloga1.Visible = false;
                    cboKlijent.Enabled = true;
                    button21.Visible = true;
                }
                else
                {
                    cboTipNaloga.Visible = true;
                    txtTipNaloga1.Visible = false;
                    cboKlijent.Enabled = true;
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
            dt1.Rows.Add(2, "3PU");
            dt1.Rows.Add(3, "3PI");

            cboTipNaloga.DataSource = dt1;
            cboTipNaloga.DisplayMember = "Naziv";   // šta se prikazuje
            cboTipNaloga.ValueMember = "ID";        // ID vrednost

            var klijent = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var klAD = new SqlDataAdapter(klijent, conn);
            var klDS = new DataSet();
            klAD.Fill(klDS);
            cboKlijent.DataSource = klDS.Tables[0];
            cboKlijent.DisplayMember = "PaNaziv";
            cboKlijent.ValueMember = "PaSifra";

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
        }

        //public void FillAdresaIstovara()
        //{
        //    cboAdresaIstovara.DataSource = null;
        //    cboAdresaIstovara.Items.Clear();
        //    cboAdresaIstovara.SelectedIndex = -1;

        //    if (cboMestoIstovara.SelectedIndex == -1 || cboMestoIstovara.SelectedValue == null || cboMestoIstovara.SelectedValue == DBNull.Value)
        //        return;

        //    using (SqlConnection conn = new SqlConnection(connection))
        //    {
        //        string query = @"SELECT PaKoZapSt, 
        //                        (RTRIM(PaKOOpomba)) AS Naziv 
        //                     FROM partnerjiKontOsebaMU 
        //                     WHERE PaKOSifra = @MestoIstovara 
        //                     ORDER BY PaKOIme";
        //        //+', ' + (LTRIM(RTRIM(PaKOIme))) + ' ' + (LTRIM(RTRIM(PaKOPriimek)))
        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@MestoIstovara", Convert.ToInt32(cboMestoIstovara.SelectedValue));
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();
        //            da.Fill(ds);

        //            cboAdresaIstovara.DataSource = ds.Tables[0];
        //            cboAdresaIstovara.DisplayMember = "Naziv";
        //            cboAdresaIstovara.ValueMember = "PaKoZapSt";

        //            if (ds.Tables[0].Rows.Count == 0)
        //            {
        //                cboAdresaIstovara.SelectedIndex = -1;
        //            }
        //        }
        //    }
        //}


        //public void FillAdresaUtovara()
        //{
        //    cboAdresaUtovara.DataSource = null;
        //    cboAdresaUtovara.Items.Clear();
        //    cboAdresaUtovara.SelectedIndex = -1;

        //    if (cboMestoUtovara.SelectedIndex == -1 || cboMestoUtovara.SelectedValue == null || cboMestoUtovara.SelectedValue == DBNull.Value)
        //        return;

        //    using (SqlConnection conn = new SqlConnection(connection))
        //    {
        //        string where = ""; 
        //        int kontaktOsoba = 0;
        //        if (Uvoz == 0 && int.TryParse(txtKontaktOsobaUtovara.Text, out kontaktOsoba))
        //            where = "WHERE PaKOZapSt = " + kontaktOsoba;
        //        else if (cboMestoUtovara.SelectedValue != null && Convert.ToInt32(cboMestoUtovara.SelectedValue) != -1)
        //            where = " WHERE PaKOSifra =  " + cboMestoUtovara.SelectedValue;
        //        string query = @"SELECT PaKOOpomba, PaKoZapSt FROM partnerjiKontOsebaMU  " + where;

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            if (Uvoz == 1)
        //                cmd.Parameters.AddWithValue("@KontaktOsoba", kontaktOsoba);
        //            else
        //                cmd.Parameters.AddWithValue("@MestoUtovara", Convert.ToInt32(cboMestoUtovara.SelectedValue));
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();
        //            da.Fill(ds);

        //            cboAdresaUtovara.DataSource = ds.Tables[0];
        //            cboAdresaUtovara.DisplayMember = "PaKOOpomba";
        //            cboAdresaUtovara.ValueMember = "PaKoZapSt";
        //        }
        //    }
        //}

        public void FillAdresaMestaUtovara()
        {
            SqlConnection conn = new SqlConnection(connection);
            // PaKOOpomba
            var ko = "select PaKoZapSt, (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv from partnerjiKontOsebaMU where PaKoZapSt = '" + Convert.ToInt32(cboMestoUtovara.SelectedValue) + "'  order by PaKOIme";

            var koAD = new SqlDataAdapter(ko, conn);
            var koDS = new DataSet();
            koAD.Fill(koDS);
            //txtKontaktOsoba.DataSource = koDS.Tables[0];
            //txtKontaktOsoba.DisplayMember = "Naziv";
            //txtKontaktOsoba.ValueMember = "PaKoZapSt";
        }

        private void UcitajKamione(int? tipTransportaId)
        {
            SqlConnection conn = new SqlConnection(connection);
            string kam = "SELECT a.ID, a.Marka, a.RegBr, a.Vozac " +
                         "FROM Automobili a " +
                        " LEFT JOIN( " +
                                    " SELECT r1.KamionID, r1.Status " +
                                   "  FROM RadniNalogDrumski r1 " +
                                   "  INNER JOIN( " +
                                   "      SELECT KamionID, MAX(ID) AS MaxID " +
                                   "      FROM RadniNalogDrumski " +
                                    "     GROUP BY KamionID " +
                                   "  ) r2 ON r1.KamionID = r2.KamionID AND r1.ID = r2.MaxID " +
                               "  ) rn ON a.ID = rn.KamionID " +

                        " WHERE a.VoziloDrumskog = 1 AND (rn.KamionID IS NULL OR rn.Status = 7) ";

            SqlCommand cmd = new SqlCommand();

            if (tipTransportaId.HasValue && tipTransportaId.Value > 0)
            {
                kam += " AND VlasnistvoLegeta = @TipTransporta";
                cmd.Parameters.AddWithValue("@TipTransporta", tipTransportaId.Value);
            }

            cmd.CommandText = kam;
            cmd.Connection = conn;

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
        }

        private void frmDrumski_Load(object sender, EventArgs e)
        {
            //FillCombo();
            //this.BindingContext = new BindingContext();
            //VratiPodatke();
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
            string napomenaPoz = null;

            int iD = 0;
            if (txtID.Text != null && int.TryParse(txtID.Text, out int parsedID))
            {
                iD = parsedID;
            }
            int autoDan = 0;
            if (chkAutoDan.Checked == true)
            {
                autoDan = 1;
            }
            int PDV = 0;
            if (chkPDV.Checked == true)
            {
                PDV = 1;
            }
            string mestoPreuzimanja = string.IsNullOrWhiteSpace(txtMestoPreuzimanja.Text) ? null : txtMestoPreuzimanja.Text.Trim();
            string kontaktOsobaistovara = string.IsNullOrWhiteSpace(txtkontaktNaIstovaru.Text) ? null : txtkontaktNaIstovaru.Text.Trim();

            if (dtpUtovara.Checked)
            {
                datumUtovara = dtpUtovara.Value;
            }
            if (dtIstovara.Checked)
            {
                datumIstovara = dtIstovara.Value;
            }


            DateTime? dtPreuzimanjaPraznogKont = null;
            if (dtPreuzimanjaPraznogKontejnera.Checked)
            {
                dtPreuzimanjaPraznogKont = dtPreuzimanjaPraznogKontejnera.Value;
            }
            string granicniPrelaz = string.IsNullOrWhiteSpace(txtGranicniPrelaz.Text) ? null : txtGranicniPrelaz.Text.Trim();
            decimal? trosak = null;
            decimal? cena = null;
            if (!string.IsNullOrWhiteSpace(txtTrosak.Text) && decimal.TryParse(txtTrosak.Text, out decimal parsedTrosak))
            {
                trosak = parsedTrosak;
            }

            if (!string.IsNullOrWhiteSpace(txtCena.Text) && decimal.TryParse(txtCena.Text, out decimal parsedCena))
            {
                cena = parsedCena;
            }

            string valutaID = null;
            if (txtValuta.SelectedValue != null)
            {
                valutaID = txtValuta.SelectedValue.ToString();
            }
            int? kamionID = null;
            if (cboKamion.SelectedValue != null && int.TryParse(cboKamion.SelectedValue.ToString(), out int parsedKamionID))
            {
                kamionID = parsedKamionID;
            }
            int? statusID = null;
            if (cboStatus.SelectedValue != null && int.TryParse(cboStatus.SelectedValue.ToString(), out int parsedStatusID))
            {
                statusID = parsedStatusID;
            }
            int? tipNaloga = null;
            if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int parsedTipNaloga))
            {
                tipNaloga = parsedTipNaloga;
            }
            int? tipTransportaID = null;
            if (cboTipTransporta.SelectedValue != null && int.TryParse(cboTipTransporta.SelectedValue.ToString(), out int parsedTipTransportaID))
            {
                tipTransportaID = parsedTipTransportaID;
            }

            string dodatniOpis = string.IsNullOrWhiteSpace(txtDodatniOpis.Text) ? null : txtDodatniOpis.Text.Trim();
            string brojKontejnera2 = string.IsNullOrWhiteSpace(txtBrojKontejnera2.Text) ? null : txtBrojKontejnera2.Text.Trim();
            //if (Uvoz != 0 && cboAdresaUtovara.SelectedValue != null && int.TryParse(cboAdresaUtovara.SelectedValue.ToString(), out int parsedAdresaUtovaraID))
            //    adresaUtovara = parsedAdresaUtovaraID;
            //if (Uvoz != 1 && cboAdresaIstovara.SelectedValue != null && int.TryParse(cboAdresaIstovara.SelectedValue.ToString(), out int parsedAdresaIstovaraID))
            //    adresaIstovara = parsedAdresaIstovaraID;
            adresaUtovara = string.IsNullOrWhiteSpace(txtAdresaUtovara.Text) ? null : txtAdresaUtovara.Text.Trim();
            adresaIstovara = string.IsNullOrWhiteSpace(txtAdresaIstovara.Text) ? null : txtAdresaIstovara.Text.Trim();
            if (cboMestoUtovara.SelectedValue != null && Uvoz != 0 && int.TryParse(cboMestoUtovara.SelectedValue.ToString(), out int parsedMestoUtovaraID))
                mestoUtovara = parsedMestoUtovaraID;
            if (cboMestoIstovara.SelectedValue != null && Uvoz != 1 && int.TryParse(cboMestoIstovara.SelectedValue.ToString(), out int parsedMestoIstovaraID))
                mestoIstovara = parsedMestoIstovaraID;
            if (Uvoz != 1)
                referenca = string.IsNullOrWhiteSpace(txtReferenca.Text) ? null : txtReferenca.Text.Trim();
            if (Uvoz != 1 && Uvoz != 0 && cboKlijent.SelectedValue != null && int.TryParse(cboKlijent.SelectedValue.ToString(), out int parsedKlijentID))
                klijent = parsedKlijentID;
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
            if (Uvoz != 1 && Uvoz != 0)
                napomenaPoz = string.IsNullOrWhiteSpace(txtNapomenaPoz.Text) ? null : txtNapomenaPoz.Text.Trim();
            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
            if (status == true)
            {
                if (cboTipNaloga.SelectedValue == null || (int)cboTipNaloga.SelectedValue == -1)
                {
                    MessageBox.Show("Polje 'Tip naloga' je obavezno!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // prekida dalje izvršavanje
                }

                int noviID = ins.InsRadniNalogDrumski(tipNaloga, autoDan, referenca, mestoPreuzimanja, klijent, mestoUtovara, adresaUtovara, mestoIstovara, datumUtovara, datumIstovara, adresaIstovara,
                   dtPreuzimanjaPraznogKont, granicniPrelaz, trosak, valutaID, kamionID, statusID, dodatniOpis, cena, kontaktOsobaistovara, PDV, tipTransportaID, brojVoza, bttoKontejnera, bttoRobe, brojKontejnera, brojKontejnera2,
                   bookingBrodara, brodskaTeretnica, brodskaPlomba, napomenaPoz);

                txtID.Text = noviID.ToString();
                status = false;
            }
            else
            {

                ins.UpdateRadniNalogDrumski(iD, autoDan, referenca, mestoPreuzimanja, mestoUtovara, adresaUtovara, mestoIstovara, datumUtovara, datumIstovara, adresaIstovara,
                   dtPreuzimanjaPraznogKont, granicniPrelaz, trosak, valutaID, kamionID, statusID, dodatniOpis, cena, kontaktOsobaistovara, PDV, tipTransportaID, bookingBrodara, klijent, bttoKontejnera, bttoRobe, brojVoza,
                   brojKontejnera, brojKontejnera2, brodskaTeretnica, brodskaPlomba, napomenaPoz);
            }

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
            txtMestoPreuzimanja.Text = "";
            cboKlijent.SelectedValue = -1;
            cboKlijent.Enabled = true;
            cboMestoUtovara.SelectedValue = -1;
            cboMestoUtovara.Enabled = true;
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
            txtCarinjenjeUvozno.Text = "";
            txtOdredisnaCarinarnica.Text = "";
            txtReferenca.Text = "";
            txtReferenca.Enabled = true;
            txtBL.Text = "";
            cboTipTransporta.SelectedValue = -1;
            txtBrojKontejnera.Text = "";
            txtBrojKontejnera.Enabled = true;
            txtBrojKontejnera2.Text = "";
            txtBrojKontejnera2.Enabled = true;
            txtBrodskaPlomba.Text = "";
            txtBrodskaPlomba.Enabled = true;
            dtpUtovara.Value = DateTime.Now;
            dtIstovara.Value = DateTime.Now;
            txtKontaktSpeditera.Text = "";
            txtSpediterCarinarnice.Text = "";
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
            txtValuta.SelectedValue = -1;
            txtTrosak.Value = 0.00m;
            txtCena.Value = 0.00m;
            chkPDV.Checked = false;
            txtkontaktNaIstovaru.Text = "";
            txtGranicniPrelaz.Text = "";
            txtNapomenaPoz.Text = "";
            txtDodatniOpis.Text = "";

        }
        private void button3_Click(object sender, EventArgs e)
        {
            status = true;
            Uvoz = -1;
            ResetujVrednostiPolja();
            button21.Visible = true;

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
                    cboKamion.SelectedValue = (dr["KamionID"].ToString());

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

                SqlCommand cmd = new SqlCommand("SELECT	 LicnaKarta, Vozac,BrojTelefona " +
                 "FROM   Automobili  " +
                 "where ID =  " + parsedKamionId, con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txtBrojLK.Text = (dr["LicnaKarta"].ToString());
                    txtVozac.Text = (dr["Vozac"].ToString());
                    txtBrojTelefona.Text = (dr["BrojTelefona"].ToString());

                }
            }
            else
            {
                txtBrojLK.Text = "";
                txtVozac.Text = "";
                txtBrojTelefona.Text = "";
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
        //            string emailAdresaPrimaoca = "jelena.djokicbb@gmail.com";
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
                if (tipNalogaId == 3)
                    label12.Text = "Kontakt osoba na utovaru";
                else
                    label12.Text = "Kontakt osoba na istovaru";
            }
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int parsedTipNaloga) &&
                 (parsedTipNaloga == 2 || parsedTipNaloga == 3))
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
    }
}
