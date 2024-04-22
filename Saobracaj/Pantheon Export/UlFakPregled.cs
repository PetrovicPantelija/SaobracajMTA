using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
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
        }
        private void FillGV()
        {
            var select = "Select UlFak.ID as ID,(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from UlFakPostav inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID where UlFakPostav.IDFak = UlFak.ID) as [NT Naziv]," +
                "UlFak.Status as Status,RTrim(Predvidjanje.PredvidjanjeId) as PredvidjanjeID,VrstaDokumenta as [Vrsta Dokumenta],Tip as [Tip]," +
                "Format(DatumPrijema,'dd.MM.yyyy') as [Datum prijema],RTrim(UlFak.Valuta) as Valuta,UlFak.Kurs as Kurs,FakturaBr as [Faktura BR],RTrim(PaNaziv) as Dobavljac," +
                "RTrim(RacunDobavljaca) as RacunDobavljaca,Format(DatumIzdavanja,'dd.MM.yyy') as [Datum Izdavanja],Format(DatumPDVa,'dd.MM.yyyy') as [Datum PDVa]," +
                "Format(DatumValute,'dd.MM.yyyy') as [Datum Valute],(RTrim(DeIMe) + ' ' + RTrim(DePriimek)) as Referent,(Select Sum(Cena) from UlFakPostav Where UlFakPostav.IDFak = UlFak.ID) as Iznos," +
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

            dataGridView1.Columns["ID"].Width=50;
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
                        DatumPrijema = Convert.ToDateTime(row.Cells["Datum prijema"].Value);
                        Valuta = row.Cells["Valuta"].Value.ToString();
                        Kurs = Convert.ToDecimal(row.Cells["Kurs"].Value);
                        FakturaBr = row.Cells["Faktura BR"].Value.ToString();
                        RacunDobavljaca = row.Cells["RacunDobavljaca"].Value.ToString();
                        DatumIzdavanja = Convert.ToDateTime(row.Cells["Datum Izdavanja"].Value);
                        DatumPDVa = Convert.ToDateTime(row.Cells["Datum PDVa"].Value);
                        DatumValute = Convert.ToDateTime(row.Cells["Datum Valute"].Value);
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
            InsertPatheonExport ins = new InsertPatheonExport();
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    ins.DeleteUlFak(Convert.ToInt32(row.Cells["ID"].Value));
                }
            }
            FillGV();
        }
        int crm; 
        int status;
        private void btnExportProd_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    crm = Convert.ToInt32(row.Cells["CRMID"].Value);
                    status = Convert.ToInt32(row.Cells["Status"].Value);

                    if (status == 0)
                    {
                        string query1 = "";
                        string query2 = "";
                        if (Convert.ToDecimal(row.Cells["IznosRSD"].Value) > Convert.ToInt32(row.Cells["Iznos"].Value))
                        {
                            query1 = "Select RTrim(CRMID) as CRMDocumentId,RTrim(Tip) as DocType,CONVERT(VARCHAR, DatumIzdavanja, 23) as Date,Rtrim(PaNaziv) as Issuer,'RSD' as Currency,'1' as FXRate,RTrim(FakturaBr) as Doc1, " +
                                    "CONVERT(VARCHAR, DatumIzdavanja, 23) as DateDoc1,CONVERT(VARCHAR, DatumPDVa, 23) as DateVAT,CONVERT(VARCHAR, DatumValute, 23) as DateDue,Rtrim(PredvidjanjeID) as PredvidjanjeId, " +
                                    "UlFak.Referent as UserId,UlFak.Napomena as Napomena,UlFak.ID as ID " +
                                    "from UlFak " +
                                    "Inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                                    "inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID Where UlFak.Status=0 and CRMID=" + crm;

                            query2 = "select RB as No,Rtrim(MpStaraSif) as Ident,CAST(Kolicina AS DECIMAL(10, 2)) as Qty,CAST(IznosRSD AS DECIMAL(10, 2)) as Price,Rtrim(NosiociTroskova.NosilacTroska) as CostDrv,RTrim(JM) as JNT,'' as Proizvod,IDFak " +
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

                            query2 = "select RB as No,Rtrim(MpStaraSif) as Ident,CAST(Kolicina AS DECIMAL(10, 2)) as Qty,CAST(Cena AS DECIMAL(10, 2)) as Price,Rtrim(NosiociTroskova.NosilacTroska) as CostDrv,RTrim(JM) as JNT,'' as Proizvod,IDFak " +
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
                    else
                    {
                        MessageBox.Show("Faktura CRMID:" + crm + " je već poslata singronizacijom!");
                    }
                }
            }
            FillGV();
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
    }
}
