﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Saobracaj.Administracija;
using Saobracaj.Sifarnici;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;

namespace Saobracaj.Pantheon_Export
{

    public partial class UlFakPregled : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        int ID;
        public UlFakPregled()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }
        private void FillGV()
        {
            var select = "Select UlFak.ID as ID," +
                "(Select MAX(RTrim(NosiociTroskova.NosilacTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT]," +
                "(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT Naziv]," +
                "UlFak.Status as Status,RTrim(Predvidjanje.PredvidjanjeId) as PredvidjanjeID,VrstaDokumenta as [Vrsta Dokumenta],Tip as [Tip]," +
                "Format(DatumPrijema,'dd.MM.yyyy') as [DatumPrijema],RTrim(UlFak.Valuta) as Valuta,UlFak.Kurs as Kurs,FakturaBr as [Faktura BR],RTrim(PaNaziv) as Dobavljac," +
                "RTrim(RacunDobavljaca) as RacunDobavljaca,Format(DatumIzdavanja,'dd.MM.yyy') as [DatumIzdavanja],Format(DatumPDVa,'dd.MM.yyyy') as [DatumPDVa]," +
                "Format(DatumValute,'dd.MM.yyyy') as [DatumValute],(RTrim(DeIMe) + ' ' + RTrim(DePriimek)) as Referent,(Select Sum(Cena) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as Iznos," +
                "(Select Sum(IznosRSD) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as IznosRSD, UlFak.Napomena as Napomena,UlFak.CRMID as CRMID," +
                "Predvidjanje as Predvidjanje,IDDobavljaca as IDDobavljaca," +
                "UlFak.Referent as Referent," +
                "RTrim(UlFak.Korisnik) as Korisnik,PantheonID " +
                "From UlFak " +
                "Inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID " +
                "inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                "inner join Delavci on UlFak.Referent = Delavci.DeSifra " +
                "Order by UlFak.ID desc";

            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["NT Naziv"].Width = 120;
            dataGridView1.Columns["Status"].Width = 45;
            dataGridView1.Columns["PredvidjanjeID"].Width = 90;
            dataGridView1.Columns["Vrsta Dokumenta"].Visible = false;
            dataGridView1.Columns["Tip"].Width = 55;
            dataGridView1.Columns["Valuta"].Width = 55;
            dataGridView1.Columns["Kurs"].Width = 60;
            dataGridView1.Columns["Dobavljac"].Width = 150;
            dataGridView1.Columns["RacunDobavljaca"].Visible = false;
            dataGridView1.Columns["Referent"].Visible = false;
            dataGridView1.Columns["Iznos"].Width = 80;
            dataGridView1.Columns["IznosRSD"].Visible = false;
            dataGridView1.Columns["Napomena"].Width = 280;
            dataGridView1.Columns["CRMID"].Width = 80;
            dataGridView1.Columns["Predvidjanje"].Visible = false;
            dataGridView1.Columns["IDDobavljaca"].Visible = false;
            dataGridView1.Columns["Referent1"].Visible = false;
        }

        public int Predvidjanje, Dobavljac, Referent;
        public string Valuta, VrstaDokumenta, TipDokumenta, FakturaBr, RacunDobavljaca, Napomena;
        public DateTime DatumPrijema, DatumIzdavanja, DatumPDVa, DatumValute;
        public decimal Kurs;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ID = Convert.ToInt32(row.Cells["ID"].Value);
                        VrstaDokumenta = row.Cells["Vrsta Dokumenta"].Value.ToString();
                        TipDokumenta = row.Cells["Tip"].Value.ToString();
                        DatumPrijema = Convert.ToDateTime(row.Cells["DatumPrijema"].Value);
                        Valuta = row.Cells["Valuta"].Value.ToString();
                        Kurs = Convert.ToDecimal(row.Cells["Kurs"].Value);
                        FakturaBr = row.Cells["Faktura BR"].Value.ToString();
                        RacunDobavljaca = row.Cells["RacunDobavljaca"].Value.ToString();
                        DatumIzdavanja = Convert.ToDateTime(row.Cells["DatumIzdavanja"].Value);
                        DatumPDVa = Convert.ToDateTime(row.Cells["DatumPDVa"].Value);
                        DatumValute = Convert.ToDateTime(row.Cells["DatumValute"].Value);
                        Napomena = row.Cells["Napomena"].Value.ToString();
                        Predvidjanje = Convert.ToInt32(row.Cells["Predvidjanje"].Value);
                        Dobavljac = Convert.ToInt32(row.Cells["IDDobavljaca"].Value);
                        Referent = Convert.ToInt32(row.Cells["Referent1"].Value);
                    }
                }
            }
            catch { }
        }
        private void UlFakPregled_Load(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
        }
        public class Placanja
        {
            public int SetType { get; set; }
            public string FakturaBr { get; set; }
            public string KlijentId { get; set; }
            public DateTime DatumIzdavanja { get; set; }
            public DateTime DatumPlacanja { get; set; }
            public int TipFakture { get; set; }
            public string Valuta { get; set; }
            public decimal Iznos { get; set; }
        }
        public class JsonResponse
        {
            public string Status { get; set; }
            public string Id { get; set; }
            public string Poruka { get; set; }
            public List<Placanja> Uplate { get; set; }
        }

        int CRMID;
        decimal Iznos;
        string ValutaResponse;
        DateTime DatumPlacanja;


        private void btnGetPlacanja_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:3333/api/PlacanjaDobavljacima/GetPlacanjaDobavljacima");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                try
                {
                    JsonResponse response = JsonConvert.DeserializeObject<JsonResponse>(result);

                    if (response.Uplate != null && response.Uplate.Count > 0)
                    {
                        dataGridView2.DataSource = response.Uplate;
                    }
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        CRMID = Convert.ToInt32(row.Cells["FakturaBr"].Value.ToString());
                        Iznos = Convert.ToDecimal(row.Cells["Iznos"].Value.ToString());
                        ValutaResponse = row.Cells["Valuta"].Value.ToString().TrimEnd();
                        DateTime datumPom = Convert.ToDateTime(row.Cells["DatumPlacanja"].Value.ToString());
                        DatumPlacanja = Convert.ToDateTime(datumPom.ToString("yyyy-MM-dd"));

                        using (SqlConnection conn = new SqlConnection(connect))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "UPDATE UlFak SET Valuta='" + ValutaResponse + "', Iznos='" + Iznos.ToString().Replace(",", ".") + "', DatumPlacanja='" + DatumPlacanja.ToString("yyyy-MM-dd") + "' WHERE CRMID = " + CRMID;
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nema novih uplata\n" + ex.ToString());
                }
            }
            FillGV();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (frmLogovanje.user.ToString().TrimEnd() == "mikic.d" || frmLogovanje.user.ToString().TrimEnd() == "cvetkovic.a" || frmLogovanje.user.ToString().TrimEnd() == "jovanovic.v")
            {
                InsertPatheonExport ins = new InsertPatheonExport();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ins.DeleteUlFak(Convert.ToInt32(row.Cells["ID"].Value));
                    }
                }
                FillGV();
            }
            else
            {
                MessageBox.Show("Nemate pravo brisanja zapisa!");
            }
        }
        int crm;
        int status;
        string valuta;
        string valutaPom = "";

        int drzava;
        private void btnExportProd_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    crm = Convert.ToInt32(row.Cells["CRMID"].Value);
                    status = Convert.ToInt32(row.Cells["Status"].Value);

                    int partner = Convert.ToInt32(row.Cells["IDDObavljaca"].Value.ToString());
                    string select = "Select PaSifDrzave From Partnerji Where PaSifra=" + partner;
                    SqlConnection con = new SqlConnection(connect);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(select, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        drzava = Convert.ToInt32(dr[0].ToString());
                    }
                    con.Close();
                    valuta = row.Cells["Valuta"].Value.ToString();

                    if (status == 0)
                    {
                        if (drzava == 82 && valuta != "RSD")
                        {
                            Valuta frm = new Valuta(2, valuta, crm);
                            frm.Show();
                        }
                        else
                        {
                            valutaPom = valuta;
                            Export();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Faktura CRMID:" + crm + " je već poslata singronizacijom!");
                        return;
                    }
                }
            }
            FillGV();
        }
        private void Export()
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            string query1 = "";
            string query2 = "";
            try
            {
                if (valutaPom == "RSD")
                {
                    query1 = "Select RTrim(CRMID) as CRMDocumentId,RTrim(Tip) as DocType,CONVERT(VARCHAR, DatumIzdavanja, 23) as Date,Rtrim(PaNaziv) as Issuer,'RSD' as Currency,'1' as FXRate,RTrim(FakturaBr) as Doc1, " +
                            "CONVERT(VARCHAR, DatumIzdavanja, 23) as DateDoc1,CONVERT(VARCHAR, DatumPDVa, 23) as DateVAT,CONVERT(VARCHAR, DatumValute, 23) as DateDue,Rtrim(PredvidjanjeID) as PredvidjanjeId, " +
                            "UlFak.Referent as UserId,UlFak.Napomena as Napomena,UlFak.ID as ID " +
                            "from UlFak " +
                            "Inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                            "inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID Where UlFak.Status=0 and CRMID=" + crm;

                    query2 = "select RB as No,Rtrim(MpStaraSif) as Ident,CAST(Kolicina AS DECIMAL(10, 2)) as Qty,CAST(IznosRSD/Kolicina AS DECIMAL(10, 2)) as Price,Rtrim(NosiociTroskova.NosilacTroska) as CostDrv,RTrim(JM) as JNT,'' as Proizvod,IDFak " +
                        "From UlFakPostav " +
                        "inner join MaticniPodatki on UlFakPostav.Mp = MaticniPodatki.MpSifra " +
                        "inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID " +
                        "order by IDFak asc";
                }
                else
                {
                    query1 = "Select RTrim(CRMID) as CRMDocumentId,RTrim(Tip) as DocType,CONVERT(VARCHAR, DatumIzdavanja, 23) as Date,Rtrim(PaNaziv) as Issuer,RTrim(UlFak.Valuta) as Currency,UlFak.Kurs as FXRate,RTrim(FakturaBr) as Doc1, " +
                    "CONVERT(VARCHAR, DatumIzdavanja, 23) as DateDoc1,CONVERT(VARCHAR, DatumPDVa, 23) as DateVAT,CONVERT(VARCHAR, DatumValute, 23) as DateDue,Rtrim(PredvidjanjeID) as PredvidjanjeId, " +
                    "UlFak.Referent as UserId,UlFak.Napomena as Napomena,UlFak.ID as ID " +
                    "from UlFak " +
                    "Inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                    "inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID Where UlFak.Status=0 and CRMID=" + crm;

                    query2 = "select RB as No,Rtrim(MpStaraSif) as Ident,CAST(Kolicina AS DECIMAL(10, 2)) as Qty,CAST(Cena/Kolicina AS DECIMAL(10, 2)) as Price,Rtrim(NosiociTroskova.NosilacTroska) as CostDrv,RTrim(JM) as JNT,'' as Proizvod,IDFak " +
                        "From UlFakPostav " +
                        "inner join MaticniPodatki on UlFakPostav.Mp = MaticniPodatki.MpSifra " +
                        "inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID " +
                        "order by IDFak asc";
                }
                string ID = "";

                List<object> combinedData = new List<object>();

                using (SqlConnection conn = new SqlConnection(connect))
                {
                    SqlCommand cmd1 = new SqlCommand(query1, conn);
                    SqlCommand cmd2 = new SqlCommand(query2, conn);

                    conn.Open();

                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    DataTable table1 = new DataTable();
                    table1.Load(dr1);

                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    DataTable table2 = new DataTable();
                    table2.Load(dr2);

                    foreach (DataRow row1 in table1.Rows)
                    {
                        Dictionary<string, object> obj = new Dictionary<string, object>();
                        foreach (DataColumn column in table1.Columns)
                        {

                            ID = row1["ID"].ToString();
                            if (column.ColumnName != "ID") // Exclude the field FapStFak from the JSON object
                            {
                                obj.Add(column.ColumnName, row1[column]);
                            }
                        }

                        List<object> fakturaPoz = new List<object>();

                        DataRow[] relatedRows = table2.Select($"IDFak = '{row1["ID"]}'");
                        foreach (DataRow row2 in relatedRows)
                        {
                            Dictionary<string, object> fakturaPozObj = new Dictionary<string, object>();
                            foreach (DataColumn column in table2.Columns)
                            {
                                if (column.ColumnName != "IDFak") // Exclude the field FapStFak from the JSON object
                                {
                                    fakturaPozObj.Add(column.ColumnName, row2[column]);
                                }
                            }
                            fakturaPoz.Add(fakturaPozObj);
                        }

                        obj.Add("FakturaPoz", fakturaPoz);
                        combinedData.Add(obj);
                    }

                    conn.Close();
                }

                foreach (var item in combinedData)
                {
                    string jsonOutput = JsonConvert.SerializeObject(item, Formatting.Indented);

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:3333/api/RacunDobavljaca/RacunDobavljacaPost");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(jsonOutput);
                    }
                    string response = "";
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        response = result.ToString();
                        if (response.Contains("Error") == true || response.Contains("Greška") == true || response.Contains("ERROR") == true || response.Contains("Duplikat") == true)
                        {
                            MessageBox.Show("Slanje nije uspelo\n" + response.ToString());
                            ins.InsApiLog("UlFak-" + ID.ToString(), jsonOutput, response);
                            return;
                        }
                        else
                        {
                            using (SqlConnection conn = new SqlConnection(connect))
                            {
                                using (SqlCommand cmd1 = conn.CreateCommand())
                                {

                                    //string pantheon = JObject.Parse(result)["Id"].ToString().Trim();
                                    //cmd1.CommandText = "UPDATE UlFak SET Status = 1,PantheonID='" + pantheon + "' WHERE ID = " + ID;
                                    cmd1.CommandText = "UPDATE UlFak SET Status = 1 WHERE ID = " + ID;
                                    conn.Open();
                                    cmd1.ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                        }

                    }
                    ins.InsApiLog("UlFak-" + ID.ToString(), jsonOutput, response);
                }
            }
            catch(Exception ex)
            {
                if (ex.ToString().Contains("TimeOut") == true || ex.ToString().Contains("Timeout") == true)
                {
                    MessageBox.Show("Pokušan je export velike količine podataka. Server je vratio grešku TIMEOUT!\nProverite ispravnost exportovanih podataka u Pantheon-u.\nKontaktirati Dragana Mikića za izmenu statusa exportovanih podataka!");
                    return;
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
        }



        private void button1_Click(object sender, EventArgs e)
        {
            UlazneFakture frm = new UlazneFakture(ID, Predvidjanje, VrstaDokumenta, TipDokumenta, DatumPrijema, Valuta, Kurs, FakturaBr, Dobavljac, RacunDobavljaca, DatumIzdavanja, DatumPDVa, DatumValute, Referent, Napomena);
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UlazneFakture frm = new UlazneFakture();
            frm.Show();
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);
            var nt = "Select ID,RTrim(NosilacTroska) as NT from NosiociTroskova order by ID desc";
            var ntDa = new SqlDataAdapter(nt, conn);
            var ntDS = new DataSet();
            ntDa.Fill(ntDS);
            comboBox1.DataSource = ntDS.Tables[0];
            comboBox1.DisplayMember = "NT";
            comboBox1.ValueMember = "ID";

            var ntNaziv = "Select ID,RTrim(NazivNosiocaTroska) as NT from NosiociTroskova order by ID desc";
            var ntNazivDa = new SqlDataAdapter(ntNaziv, conn);
            var ntNazivDS = new DataSet();
            ntNazivDa.Fill(ntNazivDS);
            comboBox2.DataSource = ntNazivDS.Tables[0];
            comboBox2.DisplayMember = "NT";
            comboBox2.ValueMember = "ID";

            var partner = "Select ID,RTrim(PredvidjanjeID) as Naziv from Predvidjanje order by ID desc";
            var parterDa = new SqlDataAdapter(partner, conn);
            var partnerDS = new DataSet();
            parterDa.Fill(partnerDS);
            comboBox3.DataSource = partnerDS.Tables[0];
            comboBox3.DisplayMember = "Naziv";
            comboBox3.ValueMember = "ID";
        }
        private void Filter(string query)
        {
            string select = query;
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["NT Naziv"].Width = 120;
            dataGridView1.Columns["Status"].Width = 45;
            dataGridView1.Columns["PredvidjanjeID"].Width = 90;
            dataGridView1.Columns["Vrsta Dokumenta"].Visible = false;
            dataGridView1.Columns["Tip"].Width = 55;
            dataGridView1.Columns["Valuta"].Width = 55;
            dataGridView1.Columns["Kurs"].Width = 60;
            dataGridView1.Columns["Dobavljac"].Width = 150;
            dataGridView1.Columns["RacunDobavljaca"].Visible = false;
            dataGridView1.Columns["Referent"].Visible = false;
            dataGridView1.Columns["Iznos"].Width = 80;
            dataGridView1.Columns["IznosRSD"].Visible = false;
            dataGridView1.Columns["Napomena"].Width = 280;
            dataGridView1.Columns["CRMID"].Width = 80;
            dataGridView1.Columns["Predvidjanje"].Visible = false;
            dataGridView1.Columns["IDDobavljaca"].Visible = false;
            dataGridView1.Columns["Referent1"].Visible = false;
        }


        private void btnFilterNT_Click(object sender, EventArgs e)
        {
            var select = "Select UlFak.ID as ID," +
                "(Select MAX(RTrim(NosiociTroskova.NosilacTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT]," +
                "(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT Naziv]," +
                "UlFak.Status as Status,RTrim(Predvidjanje.PredvidjanjeId) as PredvidjanjeID,VrstaDokumenta as [Vrsta Dokumenta],Tip as [Tip]," +
                "Format(DatumPrijema,'dd.MM.yyyy') as [DatumPrijema],RTrim(UlFak.Valuta) as Valuta,UlFak.Kurs as Kurs,FakturaBr as [Faktura BR],RTrim(PaNaziv) as Dobavljac," +
                "RTrim(RacunDobavljaca) as RacunDobavljaca,Format(DatumIzdavanja,'dd.MM.yyy') as [DatumIzdavanja],Format(DatumPDVa,'dd.MM.yyyy') as [DatumPDVa]," +
                "Format(DatumValute,'dd.MM.yyyy') as [DatumValute],(RTrim(DeIMe) + ' ' + RTrim(DePriimek)) as Referent,(Select Sum(Cena) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as Iznos," +
                "(Select Sum(IznosRSD) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as IznosRSD, UlFak.Napomena as Napomena,UlFak.CRMID as CRMID," +
                "Predvidjanje as Predvidjanje,IDDobavljaca as IDDobavljaca," +
                "UlFak.Referent as Referent," +
                "RTrim(UlFak.Korisnik) as Korisnik,PantheonID " +
                "From UlFak " +
                "Inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID " +
                "inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                "inner join Delavci on UlFak.Referent = Delavci.DeSifra " +
                "inner join UlFakPostav on UlFak.ID=UlFakPostav.IDFak " +
                "Where UlFakPostav.NosilacTroska="+Convert.ToInt32(comboBox1.SelectedValue)+" Order by UlFak.ID desc";

            Filter(select);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var select = "Select UlFak.ID as ID," +
                "(Select MAX(RTrim(NosiociTroskova.NosilacTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT]," +
                "(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT Naziv]," +
                "UlFak.Status as Status,RTrim(Predvidjanje.PredvidjanjeId) as PredvidjanjeID,VrstaDokumenta as [Vrsta Dokumenta],Tip as [Tip]," +
                "Format(DatumPrijema,'dd.MM.yyyy') as [DatumPrijema],RTrim(UlFak.Valuta) as Valuta,UlFak.Kurs as Kurs,FakturaBr as [Faktura BR],RTrim(PaNaziv) as Dobavljac," +
                "RTrim(RacunDobavljaca) as RacunDobavljaca,Format(DatumIzdavanja,'dd.MM.yyy') as [DatumIzdavanja],Format(DatumPDVa,'dd.MM.yyyy') as [DatumPDVa]," +
                "Format(DatumValute,'dd.MM.yyyy') as [DatumValute],(RTrim(DeIMe) + ' ' + RTrim(DePriimek)) as Referent,(Select Sum(Cena) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as Iznos," +
                "(Select Sum(IznosRSD) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as IznosRSD, UlFak.Napomena as Napomena,UlFak.CRMID as CRMID," +
                "Predvidjanje as Predvidjanje,IDDobavljaca as IDDobavljaca," +
                "UlFak.Referent as Referent," +
                "RTrim(UlFak.Korisnik) as Korisnik,PantheonID " +
                "From UlFak " +
                "Inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID " +
                "inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                "inner join Delavci on UlFak.Referent = Delavci.DeSifra " +
                "inner join UlFakPostav on UlFak.ID=UlFakPostav.IDFak " +
                "Where UlFakPostav.NosilacTroska=" + Convert.ToInt32(comboBox2.SelectedValue) + " Order by UlFak.ID desc";

            Filter(select);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            var select = "Select UlFak.ID as ID," +
            "(Select MAX(RTrim(NosiociTroskova.NosilacTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT]," +
            "(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT Naziv]," +
            "UlFak.Status as Status,RTrim(Predvidjanje.PredvidjanjeId) as PredvidjanjeID,VrstaDokumenta as [Vrsta Dokumenta],Tip as [Tip]," +
            "Format(DatumPrijema,'dd.MM.yyyy') as [DatumPrijema],RTrim(UlFak.Valuta) as Valuta,UlFak.Kurs as Kurs,FakturaBr as [Faktura BR],RTrim(PaNaziv) as Dobavljac," +
            "RTrim(RacunDobavljaca) as RacunDobavljaca,Format(DatumIzdavanja,'dd.MM.yyy') as [DatumIzdavanja],Format(DatumPDVa,'dd.MM.yyyy') as [DatumPDVa]," +
            "Format(DatumValute,'dd.MM.yyyy') as [DatumValute],(RTrim(DeIMe) + ' ' + RTrim(DePriimek)) as Referent,(Select Sum(Cena) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as Iznos," +
            "(Select Sum(IznosRSD) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as IznosRSD, UlFak.Napomena as Napomena,UlFak.CRMID as CRMID," +
            "Predvidjanje as Predvidjanje,IDDobavljaca as IDDobavljaca," +
            "UlFak.Referent as Referent," +
            "RTrim(UlFak.Korisnik) as Korisnik,PantheonID " +
            "From UlFak " +
            "Inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID " +
            "inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
            "inner join Delavci on UlFak.Referent = Delavci.DeSifra " +
            "Where UlFak.Predvidjanje=" + Convert.ToInt32(comboBox3.SelectedValue) + " Order by UlFak.ID desc";

            Filter(select);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            var select = "Select UlFak.ID as ID," +
            "(Select MAX(RTrim(NosiociTroskova.NosilacTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT]," +
            "(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT Naziv]," +
            "UlFak.Status as Status,RTrim(Predvidjanje.PredvidjanjeId) as PredvidjanjeID,VrstaDokumenta as [Vrsta Dokumenta],Tip as [Tip]," +
            "Format(DatumPrijema,'dd.MM.yyyy') as [DatumPrijema],RTrim(UlFak.Valuta) as Valuta,UlFak.Kurs as Kurs,FakturaBr as [Faktura BR],RTrim(PaNaziv) as Dobavljac," +
            "RTrim(RacunDobavljaca) as RacunDobavljaca,Format(DatumIzdavanja,'dd.MM.yyy') as [DatumIzdavanja],Format(DatumPDVa,'dd.MM.yyyy') as [DatumPDVa]," +
            "Format(DatumValute,'dd.MM.yyyy') as [DatumValute],(RTrim(DeIMe) + ' ' + RTrim(DePriimek)) as Referent,(Select Sum(Cena) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as Iznos," +
            "(Select Sum(IznosRSD) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as IznosRSD, UlFak.Napomena as Napomena,UlFak.CRMID as CRMID," +
            "Predvidjanje as Predvidjanje,IDDobavljaca as IDDobavljaca," +
            "UlFak.Referent as Referent," +
            "RTrim(UlFak.Korisnik) as Korisnik,PantheonID " +
            "From UlFak " +
            "Inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID " +
            "inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
            "inner join Delavci on UlFak.Referent = Delavci.DeSifra " +
            "Order by UlFak.ID desc";

            Filter(select);
        }
        int gv1 = 0;
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var select = "Select UlFak.ID as ID," +
                "(Select MAX(RTrim(NosiociTroskova.NosilacTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT]," +
                "(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT Naziv]," +
                "UlFak.Status as Status,RTrim(Predvidjanje.PredvidjanjeId) as PredvidjanjeID,VrstaDokumenta as [Vrsta Dokumenta],Tip as [Tip]," +
                "Format(DatumPrijema, 'dd.MM.yyyy') as [DatumPrijema],RTrim(UlFak.Valuta) as Valuta,UlFak.Kurs as Kurs,FakturaBr as [Faktura BR],RTrim(PaNaziv) as Dobavljac," +
                "RTrim(RacunDobavljaca) as RacunDobavljaca,Format(DatumIzdavanja, 'dd.MM.yyy') as [DatumIzdavanja],Format(DatumPDVa, 'dd.MM.yyyy') as [DatumPDVa]," +
                "Format(DatumValute, 'dd.MM.yyyy') as [DatumValute],(RTrim(DeIMe) + ' ' + RTrim(DePriimek)) as Referent,(Select Sum(Cena) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as Iznos," +
                "(Select Sum(IznosRSD) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as IznosRSD, UlFak.Napomena as Napomena,UlFak.CRMID as CRMID," +
                "Predvidjanje as Predvidjanje,IDDobavljaca as IDDobavljaca," +
                "UlFak.Referent as Referent,RTrim(UlFak.Korisnik) as Korisnik,PantheonID " +
                "From UlFak " +
                "Inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID " +
                "inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                "inner join Delavci on UlFak.Referent = Delavci.DeSifra " +
                "group by UlFak.ID,UlFak.Status,Predvidjanje.PredvidjanjeId,VrstaDokumenta,Tip,DatumPrijema,UlFak.Valuta,UlFak.Kurs,UlFak.FakturaBr,PaNaziv,RacunDobavljaca,DatumIzdavanja,DatumPDVa,DatumValute,DeIme,DePriimek, " +
                "UlFak.Napomena,UlFak.CRMID,UlFak.Predvidjanje,UlFak.IDDobavljaca,UlFak.Referent,UlFak.Korisnik,UlFak.PantheonID ";

            if (dataGridView1.Columns[e.ColumnIndex].Name == "ID")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by UlFak.ID asc";
                }
                else
                {
                    select = select + " order by UlFak.ID desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "NT")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by NT asc";
                }
                else
                {
                    select = select + " order by NT desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "PredvidjanjeID")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by UlFak.Predvidjanje asc";
                }
                else
                {
                    select = select + " order by UlFak.Predvidjanje desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DatumPrijema")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by UlFak.DatumPrijema asc";
                }
                else
                {
                    select = select + " order by UlFak.DatumPrijema desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Dobavljac")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by UlFak.IDDobavljaca asc";
                }
                else
                {
                    select = select + " order by UlFak.IDDobavljaca desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DatumIzdavanja")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by UlFak.DatumIzdavanja asc";
                }
                else
                {
                    select = select + " order by UlFak.DatumIzdavanja desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DatumPDVa")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by UlFak.DatumPDVa asc";
                }
                else
                {
                    select = select + " order by UlFak.DatumPDVa desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DatumValute")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by UlFak.DatumValute asc";
                }
                else
                {
                    select = select + " order by UlFak.DatumValute desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "CRMID")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + "order by UlFak.CRMID asc";
                }
                else
                {
                    select = select + " order by UlFak.CRMID desc";
                }
                gv1++;
            }



            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["NT Naziv"].Width = 120;
            dataGridView1.Columns["Status"].Width = 45;
            dataGridView1.Columns["PredvidjanjeID"].Width = 90;
            dataGridView1.Columns["Vrsta Dokumenta"].Visible = false;
            dataGridView1.Columns["Tip"].Width = 55;
            dataGridView1.Columns["Valuta"].Width = 55;
            dataGridView1.Columns["Kurs"].Width = 60;
            dataGridView1.Columns["Dobavljac"].Width = 150;
            dataGridView1.Columns["RacunDobavljaca"].Visible = false;
            dataGridView1.Columns["Referent"].Visible = false;
            dataGridView1.Columns["Iznos"].Width = 80;
            dataGridView1.Columns["IznosRSD"].Visible = false;
            dataGridView1.Columns["Napomena"].Width = 280;
            dataGridView1.Columns["CRMID"].Width = 80;
            dataGridView1.Columns["Predvidjanje"].Visible = false;
            dataGridView1.Columns["IDDobavljaca"].Visible = false;
            dataGridView1.Columns["Referent1"].Visible = false;
        }
    }
}
