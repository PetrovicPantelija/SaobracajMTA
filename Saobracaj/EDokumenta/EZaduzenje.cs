using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Saobracaj.Sifarnici;
//using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Saobracaj.eDokumenta
{
    public partial class EZaduzenje : Form
    {
        public string connect = frmLogovanje.connectionString;
        static XNamespace cec = XNamespace.Get("urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
        static XNamespace xmlns = XNamespace.Get("urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
        static XNamespace cbc = XNamespace.Get("urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
        static XNamespace cac = XNamespace.Get("urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");

        int pom = 0;
        public EZaduzenje()
        {
            InitializeComponent();
        }
        private void EZaduzenje_Load(object sender, EventArgs e)
        {
            FillCombo();
        }
        private void FillCombo()
        {
            var query = "Select FaStFak as [BR] From Faktura Where FaStatus='OD' and FaVrstDok='BR' order by FaStFak desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbo_Faktura.DataSource = ds.Tables[0];
            cbo_Faktura.DisplayMember = "BR";
            cbo_Faktura.ValueMember = "BR";
        }

        int platilacID;
        string faktura, valuta, primalac, adresa, grad, postanski, punaAdresa, drzava, mb, mail, kreirao, napomena1, napomena2, razlog, modul, pib, pibPom, jbkjs, racun, gln, tip;
        DateTime datumIzdavanja, valutaPlacanja;
        private void PodaciPlatilac()
        {
            var query = "Select FaStFak as [Broj Racuna],FaVpisalDat as [Datum izdavanja],FaDatVal as [Valuta placanja],FaValutaCene as [Valuta],FaPartPlac as [PrimalacID]," +
            "RTrim(Partnerji.PaDMatSt) as [PIB],RTrim(Partnerji.PaNaziv) as [Primalac],RTrim(Partnerji.PaUlicaHisnaSt) as [Adresa],RTrim(Partnerji.PaKraj) as [Mesto]," +
            "RTrim(Partnerji.PaPostnaSt) as [Postanski br],(RTrim(Partnerji.PaUlicaHisnaSt) + ',' + RTrim(Partnerji.PaPostnaSt) + ' ' + RTrim(Partnerji.PaKraj)) as [Puna adresa]," +
            "RTrim(Partnerji.PaSifDrzave) as [Drzava],RTrim(PaEMatSt1) as [MB],RTrim(Partnerji.PaEMail) as [Mail],RTrim(Faktura.NameOper) as [OznakaOperatera]," +
            "RTrim(FaOpomba1) as [Napomena],RTrim(FaOpomba3) as [Napomena2],RTrim(FaOpomba2) as [Napomena PDV],FaModul as Modul,RTrim(partnerjiDod.PaDStaraSif) as [JBKJS]," +
            "BankeFak.BFOpomba as Racun,RTrim(PartnerjiDod.PaDEAN) as GLN,partnerjiDod.PaDNasaSif as [JBKJSTip]  " +
            "From Faktura " +
            "Inner join Partnerji on Faktura.FaPartPlac = Partnerji.PaSifra " +
            "Inner join PartnerjiDod on Partnerji.PaSifra=partnerjiDod.PaDSifra " +
            "Inner join BankeFak on Faktura.FaStFak=BankeFak.BFStFak " +
            "Where FaStFak = " + Convert.ToInt32(cbo_Faktura.SelectedValue);

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        faktura = dr[0].ToString().TrimEnd();
                        datumIzdavanja = Convert.ToDateTime(dr[1].ToString());
                        valutaPlacanja = Convert.ToDateTime(dr[2].ToString());
                        valuta = dr[3].ToString().TrimEnd();
                        platilacID = Convert.ToInt32(dr[4].ToString());
                        pib = dr[5].ToString().TrimEnd();
                        primalac = dr[6].ToString().TrimEnd();
                        adresa = dr[7].ToString().TrimEnd();
                        grad = dr[8].ToString().TrimEnd();
                        postanski = dr[9].ToString().TrimEnd();
                        punaAdresa = dr[10].ToString().TrimEnd();
                        drzava = dr[11].ToString().TrimEnd();
                        mb = dr[12].ToString().TrimEnd();
                        mail = dr[13].ToString().TrimEnd();
                        kreirao = dr[14].ToString().TrimEnd();
                        napomena1 = dr[15].ToString().TrimEnd();
                        napomena2 = dr[16].ToString().TrimEnd();
                        razlog = dr[17].ToString().TrimEnd();
                        modul = dr[18].ToString().TrimEnd();
                        jbkjs = dr[19].ToString().TrimEnd();
                        racun = dr[20].ToString().TrimEnd();
                        gln = dr[21].ToString().TrimEnd();
                        tip = dr[22].ToString();
                    }
                }
            }
            catch { }
            conn.Close();
            if (faktura.Length <= 3) { faktura = "00" + faktura; }
            int pom = Convert.ToInt32(faktura);
            //faktura = pom.ToString()+"-1";
            faktura = pom.ToString();
            txt_brFaktura.Text = faktura;
            dateTimePicker1.Value = datumIzdavanja;
            dateTimePicker2.Value = valutaPlacanja;
            txt_Valuta.Text = valuta;
            txt_PIB.Text = pib;
            txt_Primalac.Text = primalac;
            txt_Adresa.Text = adresa;
            txt_Grad.Text = grad;
            txt_Postanski.Text = postanski;
            txt_Drzava.Text = drzava;
            txt_MB.Text = mb;
            txt_Mail.Text = mail;
            txt_Kreirao.Text = kreirao;
            txt_Napomena1.Text = napomena1;
            txt_Napomena2.Text = napomena2;
            txt_Razlog.Text = razlog;
            txt_Modul.Text = modul;
            txt_JBKJS.Text = jbkjs;
            txt_Racun.Text = racun;
            //txt_GLN.Text = gln;

            if (drzava == "MNE") { drzava = "ME"; }
            if (drzava == "RUS") { drzava = "RU"; }
            if (drzava == "SE") { drzava = "SW"; }
            if (drzava == "SRB") { drzava = "RS"; }

            pib = Regex.Replace(pib, "[A-Za-z]", "");
            pib.Trim();

            pibPom = drzava + pib;

            if (txt_JBKJS.Text != "")
            {
                jbkjs = "JBKJS:" + txt_JBKJS.Text;
            }
            else { jbkjs = ""; }

            var queryKurs = "Select Datum,TeValuta,TeVrednost " +
                            "From Tecaj " +
                            "Where TeValuta = '" + txt_Valuta.Text + "' and Datum= '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(queryKurs, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            if (dr2.HasRows)
            {
                while (dr2.Read())
                {
                    txt_Kurs.Text = dr2[2].ToString();
                }
            }
            conn.Close();
        }

        string nizBrNar = "", jm;
        int pdvPom;
        private void PodaciStavke()
        {
            var query = "Select FaPStPos as [RB]," +
                "RTrim(FaPNaziv) as [Naziv]," +
                "FaPEM as [JM]," +
                "Cast(FaPKolOdpr as decimal(18,2)) as [Kolicina]," +
                "Cast(FapCenaEm as decimal(18, 2)) as [JedinicnaCena]," +
                "Cast(FaPZnesPost as decimal(18, 2)) as [Osnovica]," +
                "FaPDavZapSt as [SifraPDV]," +
                "Cast(Davek.DavProcForw as int) as [PDV %]," +
                "Cast(FaPZnesDavka as decimal(18, 2)) as [Porez]," +
                "FaPProcPopusta as [Rabat %]," +
                "FaValutaCene as [Valuta]," +
                "FaPStDokRef as [Porudzbenica br:],FaPKoda as [Sifra] " +
                "From FakturaPostav " +
                "inner join Faktura on FakturaPostav.FaPStFak = Faktura.FaStFak " +
                "inner join Davek on FakturaPostav.FaPDavZapSt = Davek.DavZapSt " +
                "Where FaPOznaka<>'D' and FaPStFak = " + Convert.ToInt32(cbo_Faktura.SelectedValue);

            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.Columns[0].Width = 45; //rb
            dataGridView2.Columns[1].Width = 250; //naziv
            dataGridView2.Columns[2].Width = 50; //unitCode
            dataGridView2.Columns[3].Width = 50; //kolicina
            dataGridView2.Columns[4].Width = 100;//jedinicna
            dataGridView2.Columns[5].Width = 100; //osnovica
            dataGridView2.Columns[6].Width = 60;//pdvPom
            dataGridView2.Columns[7].Width = 50;//pdv
            dataGridView2.Columns[8].Width = 100;//porez
            dataGridView2.Columns[9].Width = 60; //rabat
            dataGridView2.Columns[10].Width = 50;//valuta
            dataGridView2.Columns[11].Width = 100;//referenca
            dataGridView2.Columns[12].Width = 50;//sifra


            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pdvPom = Convert.ToInt32(dr[6].ToString());
                        jm = dr[2].ToString().TrimEnd();
                        //Jedinice mere standard https://docs.peppol.eu/poacc/billing/3.0/codelist/UNECERec20/
                        if (jm == "T") { jm = "TNE"; }
                        if (jm == "LIT") { jm = "LTR"; }
                        if (jm == "LOK") { jm = "H87"; }
                        if (jm == "VAG") { jm = "H87"; }
                        if (jm == "EUR") { jm = "H87"; }
                        if (jm == "SAT") { jm = "HUR"; }
                        if (jm == "PCS") { jm = "H87"; }
                        if (jm == "KM") { jm = "KMT"; }
                        if (jm == "M") { jm = "MTR"; }
                        if (jm == "DAN") { jm = "DAY"; }
                        if (jm == "KG") { jm = "KGM"; }
                        if (jm == "VOZ") { jm = "H87"; }
                        if (jm == "KOM") { jm = "H87"; }
                    }
                }
            }
            catch { }
            conn.Close();
            var queryBrNar = "select distinct FaPVrstDokRef,FaPStDokRef From FakturaPostav Where FaPOznaka<>'D' and FaPStFak = " + Convert.ToInt32(cbo_Faktura.SelectedValue);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(queryBrNar, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            int count = 0;
            try
            {
                while (dr2.Read())
                {
                    if (count == 0)
                    {
                        nizBrNar = dr2["FaPStDokRef"].ToString().TrimEnd();
                        count++;
                    }
                    else
                    {
                        nizBrNar = nizBrNar + "," + dr2["FaStDokRef"].ToString().TrimEnd() + "-" + dr2["FaPStDokRef"].ToString().TrimEnd();
                        count++;
                    }
                }
            }
            catch { }

            conn.Close();
        }
        string dostavaUlica, dostavaMesto, dostavaPosta, dostavaDrzava, dostavaPrimalac;
        XElement xmlDostava = new XElement(cac + "Delivery");
        private void Dostava()
        {
            SqlConnection conn = new SqlConnection(connect);
            var select = "select FaPartPrjm,PaUlicaHisnaSt,PaKraj,PaPostnaSt,PaSifDrzave,PaDEAN,PaNaziv " +
                "From Faktura " +
                "Inner join Partnerji on Faktura.FaPartPrjm = Partnerji.PaSifra " +
                "inner join PartnerjiDod on  Faktura.FaPartPrjm = PartnerjiDod.PaDSifra " +
                "Where FaStFak = " + Convert.ToInt32(cbo_Faktura.SelectedValue);
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    dostavaUlica = dr[1].ToString().TrimEnd();
                    dostavaMesto = dr[2].ToString().TrimEnd();
                    dostavaPosta = dr[3].ToString().TrimEnd();
                    dostavaDrzava = dr[4].ToString().TrimEnd();
                    dostavaPrimalac = dr[6].ToString().TrimEnd();

                    if (dostavaDrzava == "MNE") { dostavaDrzava = "ME"; }
                    if (dostavaDrzava == "RUS") { dostavaDrzava = "RU"; }
                    if (dostavaDrzava == "SE") { dostavaDrzava = "SW"; }
                    if (dostavaDrzava == "SRB") { dostavaDrzava = "RS"; }
                    if (dostavaDrzava == "") { dostavaDrzava = "RS"; }
                    gln = dr[5].ToString().TrimEnd();
                    txt_GLN.Text = gln;
                }
            }
            catch { }

            conn.Close();

            if (txt_GLN.Text != "")
            {
                xmlDostava.Add(
                    new XElement(cbc + "ActualDeliveryDate", datumIzdavanja.ToString("yyyy-MM-dd")),
                    new XElement(cac + "DeliveryLocation",
                    new XElement(cbc + "ID", txt_GLN.Text.ToString().TrimEnd(), new XAttribute("schemeID", "0088")),
                    new XElement(cac + "Address",
                    new XElement(cbc + "StreetName", dostavaUlica),
                    new XElement(cbc + "CityName", dostavaMesto),
                    new XElement(cbc + "PostalZone", dostavaPosta),
                    new XElement(cac + "Country",
                    new XElement(cbc + "IdentificationCode", dostavaDrzava)))),
                    new XElement(cac + "DeliveryParty",
                    new XElement(cac + "PartyName",
                    new XElement(cbc + "Name", dostavaPrimalac)))
                    );
            }
            if (txt_GLN.Text == "")
            {
                xmlDostava.Add(
                    new XElement(cbc + "ActualDeliveryDate", datumIzdavanja.ToString("yyyy-MM-dd")),
                    new XElement(cac + "DeliveryLocation",
                    new XElement(cac + "Address",
                    new XElement(cbc + "StreetName", dostavaUlica),
                    new XElement(cbc + "CityName", dostavaMesto),
                    new XElement(cbc + "PostalZone", dostavaPosta),
                    new XElement(cac + "Country",
                    new XElement(cbc + "IdentificationCode", dostavaDrzava))))
                );
            }

        }
        private void Validacija()
        {
            string Poruka = "";
            if (txt_Modul.Text == "INO")
            {
                if (txt_Kurs.Text == "" || string.IsNullOrWhiteSpace(txt_Kurs.Text))
                {
                    Poruka += "Za slanje INO fakture potrebno je uneti kurs za zadati datum! \n";
                }
            }
            if (txt_PIB.Text == "")
            {
                Poruka += "Ažurirajte PIB partnera! \n";
            }
            if (txt_Adresa.Text == "")
            {
                Poruka += "Ažurirajte adresu partnera! \n";
            }
            if (txt_Grad.Text == "")
            {
                Poruka += "Ažurirajte mesto/grad partnera! \n";
            }
            if (txt_Postanski.Text == "")
            {
                Poruka += "Ažurirajte poštanski broj partnera! \n";
            }
            if (txt_Mail.Text == "")
            {
                Poruka += "Ažurirajte mail adresu partnera! \n";
            }
            if (txt_MB.Text == "")
            {
                Poruka += "Ažurirajte maticni broj partnera! \n";
            }


            if (Poruka != "")
            {
                MessageBox.Show(Poruka, "Morate ažurirati podatke o partneru " + txt_Primalac.Text.ToString() + "! \n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pom = 1;
            }
            else { pom = 0; }

        }
        private void btn_Pronadji_Click(object sender, EventArgs e)
        {
            status = false;

            PodaciPlatilac();
            PodaciStavke();
            OsnovicaPDV();
            Dostava();
            Validacija();

            if (pom == 1)
            {
                btn_PDF.Enabled = false;
                btn_Posalji.Enabled = false;
            }
            if (pom == 0)
            {
                btn_PDF.Enabled = true;
                btn_Posalji.Enabled = true;
            }
            //txt_Modul.Text = "DOMA";
            textBox7.Text = (Convert.ToDecimal(txt_o20.Text) + Convert.ToDecimal(txt_p20.Text) + Convert.ToDecimal(txt_o10.Text) + Convert.ToDecimal(txt_p10.Text) + Convert.ToDecimal(txt_o0.Text) + Convert.ToDecimal(txt_p0.Text)).ToString();
        }

        bool status = false;

        string B64 = "";

        private void btn_PDF_Click(object sender, EventArgs e)
        {
            string path = "";
            //string dir = @"\\91.148.117.14\";
            //string dir = @"\\192.168.1.6\if";
            //dir = dir.Replace("192.168.1.6", "WSS");
            OpenFileDialog fd = new OpenFileDialog();
            //fd.InitialDirectory = dir;
            fd.Filter = "PDF (*.pdf)|*.PDF";
            fd.FilterIndex = 1;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                path = fd.FileName;
            }
            Byte[] bytes = File.ReadAllBytes(path);
            string pdf = Convert.ToBase64String(bytes);
            B64 = pdf;

            status = true;
        }

        string B64pom = "";
        string[] path;
        XElement prilozi = new XElement(cac + Const.PDF);

        private void button1_Click(object sender, EventArgs e)
        {
            faktura = faktura.ToString() + "-1";
            txt_brFaktura.Text = faktura;
        }

        private void btn_Dodatna_Click(object sender, EventArgs e)
        {

            //string dir = @"\\91.148.117.14\";
            int count;
            //string dir = @"\\192.168.1.6\if";
            //dir = dir.Replace("192.168.1.6", "WSS");
            OpenFileDialog fd = new OpenFileDialog();
            //fd.InitialDirectory = dir;
            fd.Multiselect = false;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                path = fd.FileNames;
            }
            for (int i = 0; i < path.Length; i++)
            {
                foreach (string s in path)
                {
                    Byte[] bytes = File.ReadAllBytes(path[i]);
                    string pdf = Convert.ToBase64String(bytes);
                    count = i;
                    B64pom = pdf;
                    string ime = System.IO.Path.GetFileNameWithoutExtension(fd.FileName);
                    string pomTip = System.IO.Path.GetExtension(fd.FileName);
                    string tip = System.IO.Path.GetExtension(fd.FileName);
                    string mime = "";
                    if (pomTip == ".xlsx")
                    {
                        mime = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    if (pomTip == ".pdf" || pomTip == ".PDF")
                    {
                        mime = @"application/pdf";
                    }
                    if (pomTip == ".doc" || pomTip == ".docx")
                    {
                        mime = @"application/msword";
                    }
                    if (pomTip == ".png")
                    {
                        mime = @"image/png";
                    }
                    if (pomTip == ".jpeg")
                    {
                        mime = @"image/jpeg";
                    }
                    if (pomTip == ".csv")
                    {
                        mime = @"text/csv";
                    }
                    prilozi.Add(
                            new XElement(cbc + Const.Dok, faktura + "(" + count + ")"),
                            new XElement(cbc + Const.kod, "130"),
                            new XElement(cac + Const.Attachment,
                            new XElement(cbc + Const.base64, new XAttribute("mimeCode", mime), new XAttribute("encodingCode", "base64"), new XAttribute("filename", ime + tip), B64pom))
                    );
                }

            }
        }
        decimal o13, o11, o56, o57, o58, o59, o61, o62;
        private void OsnovicaPDV()
        {
            decimal sumO20 = 0, sumO10 = 0, sumO011 = 0, sumO013 = 0, sumO56 = 0, sumO57 = 0, sumO58 = 0, sumO59 = 0, sumO61 = 0, sumO62 = 0;
            decimal sumP20 = 0, sumP10 = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                pdvPom = Convert.ToInt32(row.Cells[6].Value);
                rabat = Convert.ToDecimal(row.Cells[9].Value);
                if (rabat != 0)
                {
                    osnovica = Convert.ToDecimal(row.Cells[3].Value) * Convert.ToDecimal(row.Cells[4].Value);
                    iznosRabata = Convert.ToDecimal((osnovica * rabat) / 100);
                    novaCena = osnovica - iznosRabata;
                    string pom = novaCena.ToString("F");
                    novaCena = Convert.ToDecimal(pom);

                    if (pdvPom == 1 || pdvPom == 21 || pdvPom == 50)
                    {
                        sumO20 = sumO20 + novaCena;
                        sumP20 = sumP20 + (novaCena * 20 / 100);
                        string pomO = sumO20.ToString("F");
                        string pomP = sumP20.ToString("F");
                        sumO20 = Convert.ToDecimal(pomO);
                        sumP20 = Convert.ToDecimal(pomP);
                    }
                    if (pdvPom == 13)
                    {
                        sumO013 = sumO013 + novaCena;
                    }
                    if (pdvPom == 11)
                    {
                        sumO011 = sumO011 + novaCena;
                    }
                    if (pdvPom == 56)
                    {
                        sumO56 = sumO56 + novaCena;
                    }
                    if (pdvPom == 57)
                    {
                        sumO57 = sumO57 + novaCena;
                    }
                    if (pdvPom == 58)
                    {
                        sumO58 = sumO58 + novaCena;
                    }
                    if (pdvPom == 59)
                    {
                        sumO59 = sumO59 + novaCena;
                    }
                    if (pdvPom == 61)
                    {
                        sumO61 = sumO61 + novaCena;
                    }
                    if (pdvPom == 62)
                    {
                        sumO62 = sumO62 + novaCena;
                    }
                }
                else
                {
                    if (pdvPom == 1 || pdvPom == 21 || pdvPom == 50)
                    {
                        sumO20 = sumO20 + Convert.ToDecimal(row.Cells[5].Value);
                        sumP20 = sumP20 + Convert.ToDecimal(row.Cells[5].Value) * 20 / 100;
                    }
                    if (pdvPom == 13)
                    {
                        sumO013 = sumO013 + Convert.ToDecimal(row.Cells[5].Value);
                    }
                    if (pdvPom == 11)
                    {
                        sumO011 = sumO011 + Convert.ToDecimal(row.Cells[5].Value);
                    }
                    if (pdvPom == 56)
                    {
                        sumO56 = sumO56 + Convert.ToDecimal(row.Cells[5].Value);
                    }
                    if (pdvPom == 54)
                    {
                        sumO57 = sumO57 + Convert.ToDecimal(row.Cells[5].Value);
                    }
                    if (pdvPom == 58)
                    {
                        sumO58 = sumO58 + Convert.ToDecimal(row.Cells[5].Value);
                    }
                    if (pdvPom == 59)
                    {
                        sumO59 = sumO59 + Convert.ToDecimal(row.Cells[5].Value);
                    }
                    if (pdvPom == 61)
                    {
                        sumO61 = sumO61 + Convert.ToDecimal(row.Cells[5].Value);
                    }
                    if (pdvPom == 62)
                    {
                        sumO62 = sumO62 + Convert.ToDecimal(row.Cells[5].Value);
                    }
                }
            }
            txt_o20.Text = sumO20.ToString();
            txt_o10.Text = sumO10.ToString();
            txt_o0.Text = (sumO011 + sumO013 + sumO56 + sumO57 + sumO58 + sumO59 + sumO61 + sumO62).ToString();
            txt_p20.Text = sumP20.ToString();
            txt_p10.Text = sumP10.ToString();
            txt_p0.Text = "0";

            o13 = sumO013; o11 = sumO011; o56 = sumO56; o57 = sumO57; o58 = sumO58; o59 = sumO59; o61 = sumO61; o62 = sumO62;

            if (o11 != 0 && o13 != 0)
            {
                MessageBox.Show("Nije moguće poslati fakturu sa poreskim kategorijama 11 i 13.\nFaktura se mora podeliti u dve nove.");
            }
            if (o56 != 0 && o62 != 0)
            {
                MessageBox.Show("Nije moguće poslati fakturu sa poreskim kategorijama 56 i 62.\nFaktura se mora podeliti u dve nove.");
            }
            if (o57 != 0 && o61 != 0)
            {
                MessageBox.Show("Nije moguće poslati fakturu sa poreskim kategorijama 57 i 61.\nFaktura se mora podeliti u dve nove.");
            }
        }

        XElement sub = new XElement(cac + Const.TaxSub);
        XElement tax = new XElement(cac + Const.Tax);
        XElement ino = new XElement(cac + Const.Tax);
        XElement ino_tax = new XElement(cbc + Const.Ino_Tax, "RSD");
        private void SubTotal()
        {
            decimal ukupnoP = Convert.ToDecimal(txt_p20.Text) + Convert.ToDecimal(txt_p10.Text) + Convert.ToDecimal(txt_p0.Text);
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                pdvPom = Convert.ToInt32(row.Cells[6].Value);

                if (pdvPom == 1 || pdvPom == 21 || pdvPom == 50)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(txt_o20.Text).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, Convert.ToDecimal(txt_p20.Text).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "S"),
                    new XElement(cbc + Const.Procenat, "20"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);

                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(txt_p20.Text) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }

                }
                if (pdvPom == 38)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(txt_o10.Text).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, Convert.ToDecimal(txt_p10.Text).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "S"),
                    new XElement(cbc + Const.Procenat, "10"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);


                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(txt_p10.Text) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }
                }
                if (pdvPom == 13)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(o13).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "Z"),
                    new XElement(cbc + Const.Procenat, "0"),
                    new XElement(cbc + Const.OslobodjenoKod, "PDV-RS-24-1-8"),
                    new XElement(cbc + Const.Oslobodjeno, "Poresko oslobođenje sa pravom na odbitak prethodnog poreza za prevozne i ostale usluge koje su u vezi sa izvozom, tranzitom ili privremenim uvozom dobara, osim usluga koje su oslobođene od PDV bez prava na poreski odbitak u skladu sa ovim zakonom"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);

                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(0) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }
                }

                if (pdvPom == 11)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(o11).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "Z"),
                    new XElement(cbc + Const.Procenat, "0"),
                    new XElement(cbc + Const.OslobodjenoKod, "PDV-RS-24-1-1"),
                    new XElement(cbc + Const.Oslobodjeno, "Poresko oslobođenje sa pravom na odbitak prethodnok poreza za prevozne i ostale usluge, koje su povezane sa uvozom dobara, ako je vrednost tih usluga sadržana u osnovici iz člana 19. stav 2. ovog zakona"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);

                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(0) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }
                }
                if (pdvPom == 56)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(o56).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "Z"),
                    new XElement(cbc + Const.Procenat, "0"),
                    new XElement(cbc + Const.OslobodjenoKod, "PDV-RS-24-1-8"),
                    new XElement(cbc + Const.Oslobodjeno, "Poresko oslobođenje sa pravom na odbitak prethodnok poreza za prevozne i ostale usluge, koje su povezane sa uvozom dobara, ako je vrednost tih usluga sadržana u osnovici iz člana 19. stav 2. ovog zakona"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);

                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(0) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }
                }
                if (pdvPom == 57)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(o57).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "O"),
                    new XElement(cbc + Const.Procenat, "0"),
                    new XElement(cbc + Const.OslobodjenoKod, "PDV-RS-12-4"),
                    new XElement(cbc + Const.Oslobodjeno, "Ako se promet usluga vrši poreskom obvezniku, mestom prometa usluga smatra se mesto u inostranstvu u kojem primalac usluga ima sedište ili stalnu poslovnu jedinicu ako se promet usluga vrši stalnoj poslovnoj jedinici koja se ne nalazi u mestu u kojem primalac usluga ima sedište, odnosno mesto u inostranstvu u kojem primalac usluga ima prebivalište ili boravište"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);

                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(0) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }
                }
                if (pdvPom == 58)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(o58).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "E"),
                    new XElement(cbc + Const.Procenat, "0"),
                    new XElement(cbc + Const.OslobodjenoKod, "PDV-RS-25-2-3a"),
                    new XElement(cbc + Const.Oslobodjeno, "Poresko oslobođenje bez prava na odbitak prethodnog poreza za promet dobara i usluga za koje pri nabavci obveznik nije imao pravo na odbitak prethodnog poreza"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);

                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(0) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }
                }
                if (pdvPom == 59)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(o59).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "AE"),
                    new XElement(cbc + Const.Procenat, "0"),
                    new XElement(cbc + Const.OslobodjenoKod, "PDV-RS-10-2-1"),
                    new XElement(cbc + Const.Oslobodjeno, "Poreski dužnik je primalac dobara ili usluga, obveznik PDV, za promet sekundarnih sirovina i usluga koje su neposredno povezane sa tim dobrima, izvršen od strane drugog obveznika PDV"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);

                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(0) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }
                }
                if (pdvPom == 61)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(o61).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "O"),
                    new XElement(cbc + Const.Procenat, "0"),
                    new XElement(cbc + Const.OslobodjenoKod, "PDV-RS-12-6-3"),
                    new XElement(cbc + Const.Oslobodjeno, "Mestom prometa usluga prevoza dobara koja se pruža licu koje nije poreski obveznik, smatra se mesto gde se obavlja prevoz, a ako se prevoz obavlja i u Republici i u inostranstvu, odredbe ovog zakona primenjuju se samo na deo prevoza izvršen u Republici - deo usluge prevoza koji se vrši u inostranstvu"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);

                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(0) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }
                }
                if (pdvPom == 62)
                {
                    sub = new XElement(cac + Const.TaxSub);
                    sub.Add(
                    new XElement(cbc + Const.Osnovica, Convert.ToDecimal(o62).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.UkupnoPorez, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cac + Const.Kategorija,
                    new XElement(cbc + Const.ID, "Z"),
                    new XElement(cbc + Const.Procenat, "0"),
                    new XElement(cbc + Const.OslobodjenoKod, "PDV-RS-24-1-8"),
                    new XElement(cbc + Const.Oslobodjeno, "Poresko oslobođenje sa pravom na odbitak prethodnog poreza za prevozne i ostale usluge koje su u vezi sa izvozom, tranzitom ili privremenim uvozom dobara, osim usluga koje su oslobođene od PDV bez prava na poreski odbitak u skladu sa ovim zakonom"),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, "VAT"))));

                    tax.Add(new XElement(cbc + Const.UkupnoPorez, ukupnoP.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)), sub);

                    if (modul == "INO")
                    {
                        decimal porezRSD = Convert.ToDecimal(Convert.ToDecimal(0) * Convert.ToDecimal(txt_Kurs.Text));

                        ino.Add(new XElement(cbc + Const.Porez, porezRSD.ToString("F").Replace(",", "."),
                            new XAttribute("currencyID", "RSD")));
                    }
                }
            }
        }
        XElement xmlPrimalac = new XElement(cac + Const.Primalac);
        private void Primalac()
        {
            xmlPrimalac.Add(
                new XElement(cac + Const.Party,
                new XElement(cbc + Const.PIB, pib, new XAttribute("schemeID", "9948")),
                new XElement(cac + Const.Identification,
                new XElement(cbc + Const.ID, jbkjs)),
                new XElement(cac + Const.PartyName,
                new XElement(cbc + Const.Name, primalac)),
                new XElement(cac + Const.PodaciOAdresi,
                new XElement(cbc + Const.Ulica, adresa),
                new XElement(cbc + Const.Grad, grad),
                new XElement(cbc + Const.PostanskiBroj, postanski),
                new XElement(cac + Const.AddressLine,
                new XElement(cbc + Const.Line, punaAdresa)),
                new XElement(cac + Const.Drzava,
                new XElement(cbc + Const.Oznaka, drzava))),
                new XElement(cac + Const.PartyTax,
                new XElement(cbc + Const.CompanyID, pibPom),
                new XElement(cac + Const.TaxScheme,
                new XElement(cbc + Const.ID, Const.TaxID_Value))),
                new XElement(cac + Const.PartyLegal,
                new XElement(cbc + Const.RegName, primalac),
                new XElement(cbc + Const.MB, mb),
                new XElement(cbc + Const.DodatnoOFirmi)),
                new XElement(cac + Const.Kontakt,
                new XElement(cbc + Const.Mail, txt_Mail.Text.ToString().TrimEnd()))));


        }

        XmlDocument doc = new XmlDocument();
        public string xml = "";
        decimal rabat, iznosRabata, osnovica, novaCena = 0;
        private void CreateXML()
        {
            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.Indent = true;
            string obj = "";
            if (txt_GLN.Text == "")
            {
                obj = "nema";
            }
            if (txt_GLN.Text != "")
            {
                obj = txt_GLN.Text.ToString();
            }

            string filename = faktura.ToString() + ".xml";
            decimal ukupnoO = Convert.ToDecimal(txt_o20.Text) + Convert.ToDecimal(txt_o10.Text) + Convert.ToDecimal(txt_o0.Text);
            decimal ukupnoP = Convert.ToDecimal(txt_p20.Text) + Convert.ToDecimal(txt_p10.Text) + Convert.ToDecimal(txt_p0.Text);
            using (XmlWriter xw = XmlWriter.Create(filename, xws))
            {
                XElement el = new XElement(xmlns + Const.Invoice,
                     new XAttribute("xmlns", xmlns), new XAttribute(XNamespace.Xmlns + "cec", cec), new XAttribute(XNamespace.Xmlns + "cac", cac),
                     new XAttribute(XNamespace.Xmlns + "cbc", cbc));
                el.Add(
                    new XElement(cbc + Const.CustomizationID, Const.EN_MFIN_CUSTOMIZATION_ID),
                    new XElement(cbc + Const.ProfileID, Const.MeR),
                    new XElement(cbc + Const.BrojFakture, faktura),
                    new XElement(cbc + Const.DatumIzdavanja, DateTime.Now.ToString("yyyy-MM-dd")),
                    new XElement(cbc + Const.ValutaPlacanja, valutaPlacanja.ToString("yyyy-MM-dd")),
                    new XElement(cbc + Const.OznakaDok, "383"),
                    new XElement(cbc + Const.Napomena, "Vreme izdavanja:" + DateTime.Now.ToString("HH:mm:ss") + "\n" + "Oznaka operatera: " + txt_Kreirao.Text.ToString().Trim() + "\n" +
                    "Odgovorna osoba: " + txt_Kreirao.Text.ToString().Trim() + "\n" + (napomena1 + "\n" + napomena2).ToString().Trim()),
                    new XElement(cbc + Const.Valuta, valuta),
                    ino_tax,
                    new XElement(cac + Const.PeriodObaveze,
                    new XElement(cbc + Const.DescriptionCode, 3)),

                    new XElement(cac + Const.BilingRef,
                    new XElement(cac + Const.BilingRefDoc,
                    new XElement(cbc + Const.ID, nizBrNar),
                    new XElement(cbc + Const.DatumIzdavanja, datumIzdavanja.ToString("yyyy-MM-dd")))),
                    //PDF
                    new XElement(cac + Const.PDF,
                    new XElement(cbc + Const.Dok, obj),
                    new XElement(cbc + Const.kod, "130"),
                    new XElement(cac + Const.Attachment,
                    new XElement(cbc + Const.base64, new XAttribute("mimeCode", @"application/pdf"), new XAttribute("encodingCode", "base64"), new XAttribute("filename", faktura + ".pdf"), B64))),
                    //Prilog
                    prilozi,
                    //Podaci o posiljacu
                    new XElement(cac + Const.Posiljalac,
                    new XElement(cac + Const.Party,
                    new XElement(cbc + Const.PIB, frmLogovanje.PIB, new XAttribute("schemeID", "9948")),
                    new XElement(cac + Const.Identification,
                    new XElement(cbc + Const.ID, "9948:" + frmLogovanje.PIB)),
                    new XElement(cac + Const.PartyName,
                    new XElement(cbc + Const.Name, frmLogovanje.Naziv)),
                    new XElement(cac + Const.PodaciOAdresi,
                    new XElement(cbc + Const.Ulica, frmLogovanje.Ulica),
                    new XElement(cbc + Const.Grad, frmLogovanje.Grad),
                    new XElement(cbc + Const.PostanskiBroj, frmLogovanje.PostanskiBR),
                    new XElement(cac + Const.AddressLine,
                    new XElement(cbc + Const.Line, frmLogovanje.Line)),
                    new XElement(cac + Const.Drzava,
                    new XElement(cbc + Const.Oznaka, Const.Oznaka_Value))),
                    new XElement(cac + Const.PartyTax,
                    new XElement(cbc + Const.CompanyID, frmLogovanje.CompanyID),
                    new XElement(cac + Const.TaxScheme,
                    new XElement(cbc + Const.ID, Const.TaxID_Value))),
                    new XElement(cac + Const.PartyLegal,
                    new XElement(cbc + Const.RegName, frmLogovanje.Naziv),
                    new XElement(cbc + Const.MB, frmLogovanje.MB),
                    new XElement(cbc + Const.DodatnoOFirmi)),
                    new XElement(cac + Const.Kontakt,
                    new XElement(cbc + Const.Ime, txt_Kreirao.Text.ToString().Trim()),
                    new XElement(cbc + Const.Mail, "tristic@vitamincandy.com")))),
                    //Podaci o primaocu
                    xmlPrimalac,
                    xmlDostava,
                    //Proveriti za dostavu
                    /*
                    new XElement(cac + Const.Dostava,
                    new XElement(cbc + Const.DatumDostave, datumIzdavanja.ToString("yyyy-MM-dd")),
                    new XElement(cac + Const.LokacijaDostave,
                    new XElement(cac + Const.Adresa,
                    new XElement(cbc + Const.Ulica, adresa),
                    new XElement(cbc + Const.Grad, grad),
                    new XElement(cbc + Const.PostanskiBroj, postanski),
                    new XElement(cac + Const.Drzava,
                    new XElement(cbc + Const.Oznaka, drzava))))),
                    */
                    //porez
                    new XElement(cac + Const.Placanje,
                    new XElement(cbc + Const.SifraPlacanja, Const.SifraPlacanja_Value),
                    new XElement(cbc + Const.NapomenaPlacanja, "Placanje po racunu"),
                    new XElement(cbc + Const.Model, "00 " + faktura + "-" + DateTime.Now.ToString("yyyy")),
                    new XElement(cac + Const.Tekuci,
                    new XElement(cbc + Const.ID, txt_Racun.Text.ToString().Trim()))),
                    new XElement(cac + Const.UsloviPlacanja,
                    new XElement(cbc + Const.Napomena, Const.UsloviPlacanja_Value)),
                    tax, ino,
                    //Rekapitulacija racuna
                    new XElement(cac + Const.Rekap,
                    new XElement(cbc + Const.Neto, ukupnoO.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.NetoPorez, ukupnoO.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.Bruto, (ukupnoO + ukupnoP).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.Popust, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.Trosak, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.Avans, "0.00", new XAttribute("currencyID", valuta)),
                    new XElement(cbc + Const.Ukupno, (ukupnoO + ukupnoP).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)))); ;

                progressBar1.Value = 20;
                //Stavke
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    pdvPom = Convert.ToInt32(row.Cells[6].Value);
                    rabat = Convert.ToDecimal(row.Cells[9].Value);
                    if (rabat != 0)
                    {
                        osnovica = Convert.ToDecimal(row.Cells[3].Value) * Convert.ToDecimal(row.Cells[4].Value);
                        iznosRabata = Convert.ToDecimal((osnovica * rabat) / 100);
                        novaCena = osnovica - iznosRabata;
                        if (pdvPom == 1 || pdvPom == 21 || pdvPom == 50)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                                new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat.ToString("F").Replace(",", ".")),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Tax,
                                   new XElement(cbc + Const.UkupnoPorez, Convert.ToDecimal((novaCena * Convert.ToDecimal(row.Cells[7].Value)) / 100).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.TaxSub,
                                   new XElement(cbc + Const.Osnovica, novaCena.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.UkupnoPorez, Convert.ToDecimal((novaCena * Convert.ToDecimal(row.Cells[7].Value)) / 100).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Kategorija,
                                   new XElement(cbc + Const.ID, "S"),
                                   new XElement(cbc + Const.Procenat, row.Cells[7].Value.ToString()),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT"))))),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "S"),
                                   new XElement(cbc + Const.Procenat, "20"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 38)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                                new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat.ToString("F").Replace(",", ".")),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Tax,
                                   new XElement(cbc + Const.UkupnoPorez, Convert.ToDecimal((novaCena * Convert.ToDecimal(row.Cells[7].Value)) / 100).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.TaxSub,
                                   new XElement(cbc + Const.Osnovica, novaCena.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.UkupnoPorez, Convert.ToDecimal((novaCena * Convert.ToDecimal(row.Cells[7].Value)) / 100).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Kategorija,
                                   new XElement(cbc + Const.ID, "S"),
                                   new XElement(cbc + Const.Procenat, row.Cells[7].Value.ToString()),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT"))))),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "S"),
                                   new XElement(cbc + Const.Procenat, "10"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 13)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat.ToString("F").Replace(",", ".")),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "Z"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 11)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "Z"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 56)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "Z"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 57)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "O"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 58)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "E"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 59)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "AE"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 61)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "O"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 62)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, Convert.ToDecimal(novaCena).ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),

                                   new XElement(cac + "AllowanceCharge",
                                   new XElement(cbc + "ChargeIndicator", "false"),
                                   new XElement(cbc + "AllowanceChargeReason", "Rabat"),
                                   new XElement(cbc + "MultiplierFactorNumeric", rabat),
                                   new XElement(cbc + "Amount", iznosRabata.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + "BaseAmount", osnovica.ToString("F").Replace(",", "."), new XAttribute("currencyID", valuta))),

                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "Z"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                   );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                    }
                    else
                    {
                        if (pdvPom == 1 || pdvPom == 21 || pdvPom == 50)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Tax,
                                   new XElement(cbc + Const.UkupnoPorez, Convert.ToDecimal(row.Cells[8].Value).ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.TaxSub,
                                   new XElement(cbc + Const.Osnovica, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.UkupnoPorez, Convert.ToDecimal(row.Cells[8].Value).ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Kategorija,
                                   new XElement(cbc + Const.ID, "S"),
                                   new XElement(cbc + Const.Procenat, row.Cells[7].Value.ToString()),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT"))))),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "S"),
                                   new XElement(cbc + Const.Procenat, "20"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 13)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "Z"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 11)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "Z"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 56)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "Z"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 57)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "O"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 58)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "E"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 59)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "AE"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 61)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "O"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                        if (pdvPom == 62)
                        {
                            var il = new XElement(cac + Const.Stavke);
                            il.Add(
                            new XElement(cbc + Const.ID, row.Cells[0].Value.ToString()),
                                   new XElement(cbc + Const.Kolicina, row.Cells[3].Value.ToString().Replace(",", "."), new XAttribute("unitCode", jm)),
                                   new XElement(cbc + Const.Neto, row.Cells[5].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cac + Const.Stavka,
                                   new XElement(cbc + Const.Ime, row.Cells[1].Value.ToString()),
                                   new XElement(cac + Const.SifraArtikla,
                                   new XElement(cbc + Const.ID, row.Cells[12].Value.ToString().TrimEnd())),
                                   new XElement(cac + Const.Barkod,
                                   new XElement(cbc + Const.ID, new XAttribute("schemeID", "0160"))),
                                   new XElement(cac + Const.PorezStavka,
                                   new XElement(cbc + Const.ID, "Z"),
                                   new XElement(cbc + Const.Procenat, "0"),
                                   new XElement(cac + Const.TaxScheme,
                                   new XElement(cbc + Const.ID, "VAT")))),
                                   new XElement(cac + Const.Cena,
                                   new XElement(cbc + Const.JedinicnaCena, row.Cells[4].Value.ToString().Replace(",", "."), new XAttribute("currencyID", valuta)),
                                   new XElement(cbc + Const.JedinicnaKolicina, "1", new XAttribute("unitCode", jm)))
                                );

                            el.LastNode.AddAfterSelf(il);
                            progressBar1.Value = 40;
                        }
                    }
                }

                el.Descendants(cac + Const.TaxSub).GroupBy(x => x.Value).SelectMany(x => x.Key == string.Empty ? x : x.Skip(1)).Remove();

                el.Elements(cac + Const.Tax).SelectMany(s => s.Elements(cbc + Const.Porez).GroupBy(g => g.Attribute("currencyID").Value).SelectMany(m => m.Skip(1))).Remove();

                if (ino.IsEmpty == true)
                {
                    ino.Remove();
                }
                if (modul == "DOMA")
                {
                    ino_tax.Remove();
                }
                if (prilozi.IsEmpty == true)
                {
                    prilozi.Remove();
                }

                el.WriteTo(xw);

                doc.Save(xw);
                xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + el.ToString();

                xw.Flush();
            }
            progressBar1.Value = 60;

            ms.Position = 0;
        }
        public string xmlEscape = "";
        private string Escape(string Xml)
        {
            xmlEscape = JsonConvert.SerializeObject(Xml, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii });
            MemoryStream memory = new MemoryStream();
            StreamWriter sw = new StreamWriter(memory);
            sw.Write(xmlEscape);
            sw.Close();
            progressBar1.Value = 80;

            return xmlEscape;
        }
        string response = "";
        private void Send()
        {
            if (tip == "" || tip == "3" || tip == "7" || tip == "8")
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/send");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\n" +
                        "Username:" + txt_uName.Text.ToString().TrimEnd() + "," +
                        "\nPassword:\"" + txt_Password.Text.ToString().TrimEnd() + "\"," +
                        "\nCompanyId:\""+Saobracaj.Sifarnici.frmLogovanje.PIB+"\"," +
                        "\nSoftwareId:\"MtaSoft-001\"," +
                        "\nSendToCir:null," +
                        "\nFile:" + xmlEscape + "\n}";

                    streamWriter.Write(json);
                    progressBar1.Value = 100;
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    response = result.ToString();
                    MessageBox.Show(response.ToString());
                    if (response.Contains("ElectronicId") == true)
                    {
                        dynamic data = JObject.Parse(result);
                        int eid = data.ElectronicId;
                        MessageBox.Show("Uspešno poslat dokument", "MojERacun");
                        SefGreske(eid);

                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Slanje nije uspelo");
                        Application.Restart();
                    }
                }
            }
            else
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/send");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\n" +
                        "Username:" + txt_uName.Text.ToString().TrimEnd() + "," +
                        "\nPassword:\"" + txt_Password.Text.ToString().TrimEnd() + "\"," +
                        "\nCompanyId:\""+Saobracaj.Sifarnici.frmLogovanje.PIB+"\"," +
                        "\nSoftwareId:\"MtaSoft-001\"," +
                        "\nSendToCir:true," +
                        "\nFile:" + xmlEscape + "\n}";

                    streamWriter.Write(json);
                    progressBar1.Value = 100;
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    response = result.ToString();
                    MessageBox.Show(response.ToString());
                    if (response.Contains("ElectronicId") == true)
                    {
                        dynamic data = JObject.Parse(result);
                        int eid = data.ElectronicId;
                        MessageBox.Show("Uspešno poslat dokument", "MojERacun");
                        SefGreske(eid);

                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("Slanje nije uspelo");
                        Application.Restart();
                    }
                }
            }
        }
        string greskaSef = "";
        private void SefGreske(int elID)
        {
            var httpWebRequest2 = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/queryDocumentProcessStatusOutbox");
            httpWebRequest2.ContentType = "application/json";
            httpWebRequest2.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest2.GetRequestStream()))
            {
                string json2 = "{\n" +
            "Username:Test," +
            "\nPassword:\"TestPass\"," +
            "\nCompanyId:\""+Saobracaj.Sifarnici.frmLogovanje.PIB+"\"," +
            "\nSoftwareId:\"MtaSoft-001\"," +
            "\nElectronicId:" + elID + "}";

                streamWriter.Write(json2);
            }
            var httpResponse2 = (HttpWebResponse)httpWebRequest2.GetResponse();
            using (var streamReader2 = new StreamReader(httpResponse2.GetResponseStream()))
            {
                var result2 = streamReader2.ReadToEnd();
                dynamic data = JsonObject.Parse(result2);
                if (result2.Contains("AdditionalDokumentStatusInit") == true)
                {
                    greskaSef = "Dokument je u statusu potpisano (sivo), obrađuje se, treba sačekati da se vidi u koji će status otići nakon potpisa" +
                        " da li će se račun poslati ili će otići u neuspleo.";
                }
                if (result2.Contains("XmlInvalid") == true)
                {
                    greskaSef = "Greska u kreiranju XML-a\nObavestiti IT službu!";
                }
                if (result2.Contains("AdditionalDokumentStatusNoRecipient") == true)
                {
                    greskaSef = "Greska u kreiranju XML-a\nObavestiti IT službu!";
                }
                if (result2.Contains("Unauthorized") == true)
                {
                    greskaSef = "Sef nije dostupan ili se nije aktivirao ključ prilikom generisanja.";
                }
                if (result2.Contains("EInvoiceNumberDublicate") == true)
                {
                    greskaSef = "Dupliran broj fakture, Sef ne dozvoljava slanje dva puta fakturu koja je pod istim internim brojem.";
                }
                if (result2.Contains("EInvoiceInvoiceItemDescriptionMissing") == true)
                {
                    greskaSef = "Greška prilikom unosa artikla.\nObavestiti IT službu!";
                }
                if (result2.Contains("InvoiceRowUnitMissing") == true)
                {
                    greskaSef = "Greška prilikom unosa jedinice mere.\nObavestiti IT službu!";
                }
                if (result2.Contains("InvoiceRowVatRateNotAllowedForVatCategory") == true)
                {
                    greskaSef = "Proveriti poreske kategorije! \nObavestiti IT službu!";
                }
                if (result2.Contains("MissingIban") == true)
                {
                    greskaSef = "Pošiljalac mora upisati svoj bankovni račun na SEF-u u okviru podešavanja.";
                }
                if (result2.Contains("InvoiceNumberInvalid") == true)
                {
                    greskaSef = "Neispravan interni broj, na primer razmak u internom broju, isto može da bude ako se faktura šalje ka crf-u maksimalan broj " +
                        "karaktera je 22";
                }
                if (result2.Contains("IssueDateCannotBeDifferentFromTodays") == true)
                {
                    greskaSef = "Datum slanja mora biti isti kao današnji datum, odnosno datum kada se kreira faktura /Invoice/IssueDate.\nObavestiti IT službu!";
                }
                if (result2.Contains("CirError") == true)
                {
                    greskaSef = "Pogrešan tip korisnika javnih sredstava";
                }
                if (result2.Contains("NegativeTotalSumCirInvoice") == true)
                {
                    greskaSef = "Ne sme da bude negativna faktura. U polju PayableAmaunt mora da bude pozitivan broj";
                }
                if (result2.Contains("SenderJBKJSLengthInvalid") == true)
                {
                    greskaSef = "Loš format PIB-a";
                }
                if (result2.Contains("SenderCompanyNotFound") == true)
                {
                    greskaSef = "Pošiljalac nije registrovan na SEF-u a pritom je unet Api key neke druge firme i ovaj račun je poslat ka SEF-u." +
                        " Zato je i došlo do odgovora SEF-a da ova firma nije pronađena u njihovoj bazi";
                }
                if (result2.Contains("ReceiverCompanyNotFound") == true)
                {
                    greskaSef = "Kupac nema nalog na SEF-u.";
                }
                if (result2.Contains("UBLUnsupportedDocumentType") == true)
                {
                    greskaSef = "Prilog koji se šalje mora biti PDF.";
                }
                if (result2.Contains("UBLSourceInvoiceNumberNotExist") == true)
                {
                    greskaSef = "Ne postoji faktura na koju se vodi eZaduženje. Faktura na koju se pozivate mora biti odobrena na Sef-u";
                }
                if (result2.Contains("UBLSourceInvoiceNotApproved") == true)
                {
                    greskaSef = "Nije odobren dokument na koji je data referenca na eOdobrenju, račun mora da bude u statusu odobreno na SEF-u";
                }
                if (result2.Contains("UBLMandatoryInvoiceDocumentReference") == true)
                {
                    greskaSef = "Nije upisana oznaka fakture koja se navodi kao referenca";
                }
                if (result2.Contains("Invalid") == true)
                {
                    greskaSef = "Greška u XML-u.\nObavestiti IT službu!";
                }
                if (result2.Contains("Invalid") == true)
                {
                    greskaSef = "Greška u XML-u.\nObavestiti IT službu!";
                }
                if (result2.Contains("UBLTotalTaxAmountAndSubtotalTaxAmountDiffer") == true)
                {
                    greskaSef = "Obračun poreza nije dobar.";
                }
                if (result2.Contains("UBLCompanyIsNonBudgetUser") == true)
                {
                    greskaSef = "Nije potrebno upisivati JBKJS broj u XML-u - primaoca (ostali korisnici javnih sredstava)";
                }
                if (result2.Contains("UBLCompanyWithVATRegistrationCodeIsBudgetUser") == true)
                {
                    greskaSef = "Fali jbkjs broj u čvor PartyIdentification, treba proveriti da li je u programu unešen ovaj broj kako bi se u Xml-u kreirala sva polja";
                }
                if (result2.Contains("UBLVATRegistrationCodeDoesNotMatchTheRegistrationCodeOfTheCompanyWithJBKJS") == true)
                {
                    greskaSef = "Kod primaoca je unet pogrešan JBKJS";
                }
                if (result2.Contains("UBLPayeeFinancialAccountIdNotDefined") == true)
                {
                    greskaSef = "Nije upisan bankovni racun posiljaoca u XML-u";
                }
                if (result2.Contains("UBLDeliveryDateNotAllowedForThisInvoiceType") == true)
                {
                    greskaSef = "Šalju Avansni račun i izabrali su da se PDV obračunava na datum prometa(DescriptionCode 3). " +
                        "Kada se šalje avansni račun PDV se obračunava na datum plaćanja (DescriptionCode 432).";
                }
                if (result2.Contains("UBLInvoiceLinePriceAmountMoreDecimalsThanPermitted") == true)
                {
                    greskaSef = "Maksimalni broj decimala mora biti 2 kako bi faktura otišla na Sef.";
                }
                if (result2.Contains("UBLPrepaymentInvoiceLineUnitCodeNotValid") == true)
                {
                    greskaSef = "Za avansni račun mora da bude merna jedinica kom. I količina 1";
                }
                if (result2.Contains("UBLPrepaymentInvoiceLineQuanitityNotValid") == true)
                {
                    greskaSef = "Na avansnom računu količina mora da bude 1";
                }
                if (result2.Contains("UBLInvoiceExtensionNotDefined") == true)
                {
                    greskaSef = "Nedostaje ekstenzija CustomizationId ili Namespace u Xml-u.\nObavestiti IT službu!";
                }
                if (result2.Contains("VATRegistrationCodeLengthInvalid") == true)
                {
                    greskaSef = "Dešava se problem sa formatom PIB-a (može da fali RS prefiks ili da stoji razmak)";
                }
                if (result2.Contains("VatExemptionReasonNotExists") == true)
                {
                    greskaSef = "Nije upisan razlog oslobodjenja";
                }
                if (result2.Contains("VatPointDateTypeNotAllowedForChosenDocumentType") == true)
                {
                    greskaSef = "Nije dobar descriptioncode, najčešće se dešava da treba da bude 3 , a ne 35 jer se šalje zaduženje\nObavestiti IT službu!";
                }
                if (result2.Contains("InvoiceTypeCodeInvalid") == true)
                {
                    greskaSef = "U Xml-u mora jasno da stoji šifra dokumenta u čvoru InvoiceTypecCode (380 za račun, 381 eodobrenje, 383 ezaduženje, 386 avansni račun)\nObavestiti IT službu!";
                }
                if (result2.Contains("InvoicePeriodNotDefined") == true)
                {
                    greskaSef = "Nije definisan InvoicePeriod, nedostaje čvor InvoicePerid ili DescriptionCode u XML-u koji treba da bude 3, 35 ili 432.\nObavestiti IT službu!";
                }
                if (result2.Contains("InvoicePeriodDescriptionCodeNotDefined") == true)
                {
                    greskaSef = "DescriptionCode za Obračunski period na koji se odnosi faktura nije dobro kreiran, treba da bude 3 – PDV se obračunava na dan" +
                        " slanja fakture, 35 - PDV se obračunava na dan prometa fakture, 432 unosi se prilikom kreiranja avansnog računa." +
                        "\nObavestiti IT službu!";
                }
                if (result2.Contains("UBLOrderNumberLotNumberOrContractNumberIsRequired") == true)
                {
                    greskaSef = "Ne postoji referenca na narudžbenicu ili referenca na ugovor - nema kreiran čvor <cac:ContractDocumentReference>";
                }
                if (result2.Contains("UBLPrepaymenInvoiceNotApproved") == true)
                {
                    greskaSef = "Šalje se konačna faktura, a avans na koji se šalje faktura nije odobrena na Sef-u";
                }
                if (result2.Contains("UBLRegistrationCodeDoesNotHaveGoodLength") == true)
                {
                    greskaSef = "Najčešća situacija je da nije dobro unešen matični broj primaoca fakture";
                }

                if (greskaSef == "")
                {
                    MessageBox.Show("Uspešno poslato na SEF", "SEF Greška");
                }
                else
                {
                    MessageBox.Show(greskaSef, "SEF Greška");
                }

            }
        }
        private void btn_Posalji_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                Primalac();
                SubTotal();
                CreateXML();
                Escape(xml);
                Send();

                progressBar1.Value = 100;
            }
            else
            {
                MessageBox.Show("Morate izabrati PDF dokument fakutre");
                return;
            }
        }
        private void faktureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EFaktura ef = new EFaktura();
            ef.Show();
        }

        private void poslataDokumentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PoslataDokumenta posDok = new PoslataDokumenta();
            posDok.Show();
        }

        private void primljenaDokumentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrimljenaDokumenta primDok = new PrimljenaDokumenta();
            primDok.Show();
        }
        private void knjižnaOdobrenjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EOdobrenje eo = new EOdobrenje();
            eo.Show();
        }
        private void avansniRačunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EAvansni ea = new EAvansni();
            ea.Show();
        }
    }
}
