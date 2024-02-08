using Microsoft.Ajax.Utilities;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto;
using Syncfusion.Windows.Forms.HTMLUI;
using Syncfusion.XlsIO.Parser.Biff_Records;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
            var select = "Select UlFak.ID,RTrim(Predvidjanje.PredvidjanjeId) as PredvidjanjeID,VrstaDokumenta,Tip,DatumPrijema,RTrim(UlFak.Valuta) as Valuta,Kurs,FakturaBr,RTrim(PaNaziv) as Dobavljac,RTrim(RacunDobavljaca) as RacunDobavljaca,DatumIzdavanja,DatumPDVa,DatumValute, " +
                "(RTrim(DeIMe) + ' ' + RTrim(DePriimek)) as Referent, UlFak.Napomena, UlFak.Status,UlFak.CRMID,Predvidjanje,IDDobavljaca,UlFak.Referent " +
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


            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[5].Width = 60;
            dataGridView1.Columns[6].Width = 60;
            dataGridView1.Columns[8].Width = 230;
            dataGridView1.Columns[12].Width = 180;
            
            dataGridView1.Columns[17].Visible=false;//Predvidnjanje
            dataGridView1.Columns[18].Visible=false;//Dobavljac
            dataGridView1.Columns[19].Visible = false; // referent
            


        }
        public int Predvidjanje, Dobavljac, Referent;
        public string Valuta, VrstaDokumenta, TipDokumenta, FakturaBr, RacunDobavljaca, Napomena;

        public class Placanja
        {
            public int SetType { get; set; }
            public string FakturaBr { get; set; }
            public string KlijentId { get; set; }
            public DateTime DatumIzdavanja { get; set; }
            public DateTime DatumPlacanja { get; set; }
            public int TipFakture {  get; set; }
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

        private void UlFakPregled_Load(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
        }

        private void btnGetPlacanja_Click(object sender, EventArgs e)
        {
            //dataGridView2.Visible = true;
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
                        //.ToString("yyyy-MM-dd")

                        using (SqlConnection conn = new SqlConnection(connect))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "UPDATE UlFak SET Valuta='" + ValutaResponse + "', Iznos='" + Iznos.ToString().Replace(",",".") + "', DatumPlacanja='"+ DatumPlacanja.ToString("yyyy-MM-dd") + "' WHERE CRMID = " + CRMID;
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
        int crm;

        private void btnExportProd_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    crm = Convert.ToInt32(row.Cells[16].Value);
                    status = Convert.ToInt32(row.Cells[15].Value);

                    if (status == 0)
                    {
                        string ID = "";
                        string query1 = "Select RTrim(CRMID) as CRMDocumentId,RTrim(Tip) as DocType,CONVERT(VARCHAR, DatumIzdavanja, 23) as Date,Rtrim(PaNaziv) as Issuer,RTrim(UlFak.Valuta) as Currency,Kurs as FXRate,RTrim(FakturaBr) as Doc1, " +
                            "CONVERT(VARCHAR, DatumIzdavanja, 23) as DateDoc1,CONVERT(VARCHAR, DatumPDVa, 23) as DateVAT,CONVERT(VARCHAR, DatumValute, 23) as DateDue,Rtrim(PredvidjanjeID) as PredvidjanjeId, " +
                            "UlFak.Referent as UserId,UlFak.Napomena as Napomena,UlFak.ID as ID " +
                            "from UlFak " +
                            "Inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                            "inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID Where UlFak.Status=0 and CRMID=" + crm;

                        string query2 = "select RB as No,Rtrim(MpStaraSif) as Ident,CAST(Kolicina AS DECIMAL(10, 2)) as Qty,CAST(Cena AS DECIMAL(10, 2)) as Price,Rtrim(NosiociTroskova.NosilacTroska) as CostDrv,RTrim(JM) as JNT,'' as Proizvod,IDFak " +
                            "From UlFakPostav " +
                            "inner join MaticniPodatki on UlFakPostav.Mp = MaticniPodatki.MpSifra " +
                            "inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID " +
                            "order by IDFak asc";

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
                                    ApiLogovi.Log("UlFak", ID, jsonOutput, response);
                                    ApiLogovi.Save();
                                    return;
                                }
                                else
                                {

                                    using (SqlConnection conn = new SqlConnection(connect))
                                    {
                                        using (SqlCommand cmd1 = conn.CreateCommand())
                                        {
                                            cmd1.CommandText = "UPDATE UlFak SET Status = 1  WHERE ID = " + ID;
                                            conn.Open();
                                            cmd1.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                    }
                                }

                            }
                            ApiLogovi.Log("Predvidjanje", ID, jsonOutput, response);
                            ApiLogovi.Save();
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

        int status;
        private void btnExport_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    crm = Convert.ToInt32(row.Cells[16].Value);
                    status=Convert.ToInt32(row.Cells[15].Value);

                    if (status == 0)
                    {
                        string ID = "";
                        string query1 = "Select RTrim(CRMID) as CRMDocumentId,RTrim(Tip) as DocType,CONVERT(VARCHAR, DatumIzdavanja, 23) as Date,Rtrim(PaNaziv) as Issuer,RTrim(UlFak.Valuta) as Currency,Kurs as FXRate,RTrim(FakturaBr) as Doc1, " +
                            "CONVERT(VARCHAR, DatumIzdavanja, 23) as DateDoc1,CONVERT(VARCHAR, DatumPDVa, 23) as DateVAT,CONVERT(VARCHAR, DatumValute, 23) as DateDue,Rtrim(PredvidjanjeID) as PredvidjanjeId, " +
                            "UlFak.Referent as UserId,UlFak.Napomena as Napomena,UlFak.ID as ID " +
                            "from UlFak " +
                            "Inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                            "inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID Where UlFak.Status=0 and CRMID=" + crm;

                        string query2 = "select RB as No,Rtrim(MpStaraSif) as Ident,CAST(Kolicina AS DECIMAL(10, 2)) as Qty,CAST(Cena AS DECIMAL(10, 2)) as Price,Rtrim(NosiociTroskova.NosilacTroska) as CostDrv,RTrim(JM) as JNT,'' as Proizvod,IDFak " +
                            "From UlFakPostav " +
                            "inner join MaticniPodatki on UlFakPostav.Mp = MaticniPodatki.MpSifra " +
                            "inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID " +
                            "order by IDFak asc";

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

                            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:6333/api/RacunDobavljaca/RacunDobavljacaPost");
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
                                    MessageBox.Show("Slanje nije uspelo\n"+response.ToString());
                                    ApiLogovi.Log("UlFak", ID, jsonOutput, response);
                                    ApiLogovi.Save();
                                    return;
                                }
                                /*else
                                {

                                    using (SqlConnection conn = new SqlConnection(connect))
                                    {
                                        using (SqlCommand cmd1 = conn.CreateCommand())
                                        {
                                            cmd1.CommandText = "UPDATE UlFak SET Status = 1  WHERE ID = " + ID;
                                            conn.Open();
                                            cmd1.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                    }
                                }*/

                            }
                            ApiLogovi.Log("Predvidjanje",ID, jsonOutput, response);
                            ApiLogovi.Save();
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

        public DateTime DatumPrijema, DatumIzdavanja, DatumPDVa, DatumValute;
        public decimal Kurs;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value);
                        
                        VrstaDokumenta = row.Cells[2].Value.ToString();
                        TipDokumenta = row.Cells[3].Value.ToString();
                        DatumPrijema = Convert.ToDateTime(row.Cells[4].Value);
                        Valuta = row.Cells[5].Value.ToString();
                        Kurs = Convert.ToDecimal(row.Cells[6].Value);
                        FakturaBr = row.Cells[7].Value.ToString();
                        
                        RacunDobavljaca = row.Cells[9].Value.ToString();
                        DatumIzdavanja = Convert.ToDateTime(row.Cells[10].Value);
                        DatumPDVa = Convert.ToDateTime(row.Cells[11].Value);
                        DatumValute = Convert.ToDateTime(row.Cells[12].Value);
                        
                        Napomena = row.Cells[14].Value.ToString();


                        Predvidjanje = Convert.ToInt32(row.Cells[17].Value);
                        Dobavljac = Convert.ToInt32(row.Cells[18].Value);
                        Referent = Convert.ToInt32(row.Cells[19].Value);
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int id,int predvidjanje,string vrstaDokumenta, string tipDokumenta, DateTime datumPrijema,string valuta,decimal kurs,string fakturaBr,int dobavljac,string racunDobavljaca, DateTime datumIzdavanja,
            //DateTime datumPDVa, DateTime datumValute,int referent,string napomena
            UlazneFakture frm = new UlazneFakture(ID,Predvidjanje,VrstaDokumenta,TipDokumenta,DatumPrijema,Valuta,Kurs,FakturaBr,Dobavljac,RacunDobavljaca,DatumIzdavanja,DatumPDVa,DatumValute,Referent,Napomena);
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UlazneFakture frm = new UlazneFakture();
            frm.Show();
        }
    }
}
