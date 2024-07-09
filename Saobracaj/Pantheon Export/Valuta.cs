using Newtonsoft.Json;
using Saobracaj.Sifarnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Pantheon_Export
{
    public partial class Valuta : Form
    {
        string connect = frmLogovanje.connectionString;
        public Valuta()
        {
            InitializeComponent();
        }
        int CRMID, Najava;
        int fakturaTip;
        public Valuta(int tip, string valuta, int crm, int najava)
        {
            InitializeComponent();

            fakturaTip = tip;
            btnValuta.Text = valuta;
            CRMID = crm;
            Najava = najava;
        }
        public Valuta(int tip,string valuta,int crm)
        {
            InitializeComponent();

            fakturaTip = tip;
            btnValuta.Text = valuta;
            CRMID = crm;
        }

        string query1;
        string query2;

        private void btnRSD_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            try
            {
                if (fakturaTip == 1)
                {
                    query1 = "SELECT CRMID AS CRMDocumentID, RTrim(FaModul) AS DocType, CONVERT(VARCHAR, FaVpisalDat, 23) AS Date, RTrim(PaNaziv) AS Receiver," +
                            "'RSD' AS Currency, '1' AS FXRate, '' AS Doc1, '' AS DateDoc1, '' AS Doc2, '' AS DateDoc2," +
                            "CONVERT(VARCHAR, FaObdobje, 23) AS DateVAT, CONVERT(VARCHAR, FaDatVal, 23) AS DateDue, RTrim(IDPantheon) AS Statement," +
                            "RTrim(FaRefer) AS UserId, RTrim(FaOpomba2) AS Napomena,FaStFak " +
                            "FROM Faktura " +
                            "INNER JOIN Partnerji ON Faktura.FaPartPlac = Partnerji.PaSifra " +
                            "INNER JOIN Izjave ON Faktura.FaOpomba1 = Izjave.ID " +
                            "WHERE Faktura.Status = 0 and CRMID=" + CRMID;

                    query2 = "SELECT FakturaPostav.FaPStFak, FakturaPostav.FapStPos AS No, RTrim(MaticniPodatki.MpStaraSif) AS Ident," +
                       "CAST(FakturaPostav.FaPkolOdpr AS DECIMAL(10, 2)) AS Qty," +
                       "CAST(FakturaPostav.IznosRSD AS DECIMAL(10, 2)) AS Price, RTrim(NosiociTroskova.NosilacTroska) as CostDrv, RTrim(MeNaziv) AS JNT, '' AS Product " +
                       "FROM FakturaPostav " +
                       "INNER JOIN MaticniPodatki ON FakturaPostav.FaPSifra = MaticniPodatki.MpSifra " +
                       "INNER JOIN NosiociTroskova ON FakturaPostav.NosilacTroska = NosiociTroskova.ID " +
                       "Inner join MerskeEnote on FakturaPostav.FaPEM = MerskeEnote.MeSifra " +
                       "ORDER BY FakturaPostav.FapStPos ASC";

                    string FaStFak = "";

                    List<object> combinedData = new List<object>();
                    using (SqlConnection conn = new SqlConnection(connect))
                    {
                        SqlCommand cmd1 = new SqlCommand(query1, conn);
                        SqlCommand cmd2 = new SqlCommand(query2, conn);

                        conn.Open();

                        SqlDataReader dr1 = cmd1.ExecuteReader();
                        System.Data.DataTable table1 = new System.Data.DataTable();
                        table1.Load(dr1);

                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        System.Data.DataTable table2 = new System.Data.DataTable();
                        table2.Load(dr2);

                        foreach (DataRow row1 in table1.Rows)
                        {
                            Dictionary<string, object> obj = new Dictionary<string, object>();
                            foreach (DataColumn column in table1.Columns)
                            {
                                FaStFak = row1["FaStFak"].ToString();

                                if (column.ColumnName != "FaStFak") // Exclude the field FaStFak from the JSON object
                                {
                                    obj.Add(column.ColumnName, row1[column]);
                                }
                            }

                            List<object> fakturaPoz = new List<object>();

                            DataRow[] relatedRows = table2.Select($"FaPStFak = '{row1["FaStFak"]}'");
                            foreach (DataRow row2 in relatedRows)
                            {
                                Dictionary<string, object> fakturaPozObj = new Dictionary<string, object>();
                                foreach (DataColumn column in table2.Columns)
                                {
                                    if (column.ColumnName != "FaPStFak") // Exclude the field FapStFak from the JSON object
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
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:3333/api/Faktura/FakturaPost");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Timeout = 120000;
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

                            if (response.Contains("ERROR") == true || response.Contains("Greška") == true || response.Contains("Error") == true || response.Contains("Duplikat") == true)
                            {
                                MessageBox.Show("Slanje nije uspelo\n" + response.ToString());
                                ins.InsApiLog("IzlFak-" + FaStFak.ToString(), jsonOutput, response);
                                return;
                            }
                            else
                            {
                                using (SqlConnection conn = new SqlConnection(connect))
                                {
                                    using (SqlCommand cmd2 = conn.CreateCommand())
                                    {
                                        //string pantheon = JObject.Parse(result)["Id"].ToString().Trim();
                                        //cmd2.CommandText = "UPDATE Faktura SET Status = 1,PantheonID='" + pantheon + "' WHERE FaStFak = " + FaStFak;
                                        cmd2.CommandText = "UPDATE Faktura SET Status = 1 WHERE FaStFak = " + FaStFak;
                                        conn.Open();
                                        cmd2.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                    using (SqlCommand cmd3 = conn.CreateCommand())
                                    {
                                        cmd3.CommandText = "Update Najava SET Faktura='" + FaStFak + "' Where ID=" + Najava;
                                        conn.Open();
                                        cmd3.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                }
                            }
                        }
                        ins.InsApiLog("IzlFak-" + FaStFak.ToString(), jsonOutput, response);
                    }
                }
                if (fakturaTip == 2)
                {
                    query1 = "Select RTrim(CRMID) as CRMDocumentId,RTrim(Tip) as DocType,CONVERT(VARCHAR, DatumIzdavanja, 23) as Date,Rtrim(PaNaziv) as Issuer,'RSD' as Currency,'1' as FXRate,RTrim(FakturaBr) as Doc1, " +
                       "CONVERT(VARCHAR, DatumIzdavanja, 23) as DateDoc1,CONVERT(VARCHAR, DatumPDVa, 23) as DateVAT,CONVERT(VARCHAR, DatumValute, 23) as DateDue,Rtrim(PredvidjanjeID) as PredvidjanjeId, " +
                       "UlFak.Referent as UserId,UlFak.Napomena as Napomena,UlFak.ID as ID " +
                       "from UlFak " +
                       "Inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                       "inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID Where UlFak.Status=0 and CRMID=" + CRMID;

                    query2 = "select RB as No,Rtrim(MpStaraSif) as Ident,CAST(Kolicina AS DECIMAL(10, 2)) as Qty,CAST(IznosRSD AS DECIMAL(10, 2)) as Price,Rtrim(NosiociTroskova.NosilacTroska) as CostDrv,RTrim(JM) as JNT,'' as Proizvod,IDFak " +
                        "From UlFakPostav " +
                        "inner join MaticniPodatki on UlFakPostav.Mp = MaticniPodatki.MpSifra " +
                        "inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID " +
                        "order by IDFak asc";

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
            }
            catch (Exception ex)
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
            this.Close();
        }

        private void btnValuta_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            try
            {
                if (fakturaTip == 1)
                {
                    query1 = "SELECT CRMID AS CRMDocumentID, RTrim(FaModul) AS DocType, CONVERT(VARCHAR, FaVpisalDat, 23) AS Date, RTrim(PaNaziv) AS Receiver," +
                      "RTrim(FaValutaCene) AS Currency, Cast(Kurs as DECIMAL(10,2)) AS FXRate, '' AS Doc1, '' AS DateDoc1, '' AS Doc2, '' AS DateDoc2," +
                      "CONVERT(VARCHAR, FaObdobje, 23) AS DateVAT, CONVERT(VARCHAR, FaDatVal, 23) AS DateDue, RTrim(IDPantheon) AS Statement," +
                      "RTrim(FaRefer) AS UserId, RTrim(FaOpomba2) AS Napomena,FaStFak " +
                      "FROM Faktura " +
                      "INNER JOIN Partnerji ON Faktura.FaPartPlac = Partnerji.PaSifra " +
                      "INNER JOIN Izjave ON Faktura.FaOpomba1 = Izjave.ID " +
                      "WHERE Faktura.Status = 0 and CRMID=" + CRMID;

                    query2 = "SELECT FakturaPostav.FaPStFak, FakturaPostav.FapStPos AS No, RTrim(MaticniPodatki.MpStaraSif) AS Ident," +
                       "CAST(FakturaPostav.FaPkolOdpr AS DECIMAL(10, 2)) AS Qty," +
                       "CAST(FakturaPostav.FaPCenaEM AS DECIMAL(10, 2)) AS Price, RTrim(NosiociTroskova.NosilacTroska) as CostDrv, RTrim(MeNaziv) AS JNT, '' AS Product " +
                       "FROM FakturaPostav " +
                       "INNER JOIN MaticniPodatki ON FakturaPostav.FaPSifra = MaticniPodatki.MpSifra " +
                       "INNER JOIN NosiociTroskova ON FakturaPostav.NosilacTroska = NosiociTroskova.ID " +
                       "Inner join MerskeEnote on FakturaPostav.FaPEM = MerskeEnote.MeSifra " +
                       "ORDER BY FakturaPostav.FapStPos ASC";

                    string FaStFak = "";

                    List<object> combinedData = new List<object>();
                    using (SqlConnection conn = new SqlConnection(connect))
                    {
                        SqlCommand cmd1 = new SqlCommand(query1, conn);
                        SqlCommand cmd2 = new SqlCommand(query2, conn);

                        conn.Open();

                        SqlDataReader dr1 = cmd1.ExecuteReader();
                        System.Data.DataTable table1 = new System.Data.DataTable();
                        table1.Load(dr1);

                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        System.Data.DataTable table2 = new System.Data.DataTable();
                        table2.Load(dr2);

                        foreach (DataRow row1 in table1.Rows)
                        {
                            Dictionary<string, object> obj = new Dictionary<string, object>();
                            foreach (DataColumn column in table1.Columns)
                            {
                                FaStFak = row1["FaStFak"].ToString();

                                if (column.ColumnName != "FaStFak") // Exclude the field FaStFak from the JSON object
                                {
                                    obj.Add(column.ColumnName, row1[column]);
                                }
                            }

                            List<object> fakturaPoz = new List<object>();

                            DataRow[] relatedRows = table2.Select($"FaPStFak = '{row1["FaStFak"]}'");
                            foreach (DataRow row2 in relatedRows)
                            {
                                Dictionary<string, object> fakturaPozObj = new Dictionary<string, object>();
                                foreach (DataColumn column in table2.Columns)
                                {
                                    if (column.ColumnName != "FaPStFak") // Exclude the field FapStFak from the JSON object
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
                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:3333/api/Faktura/FakturaPost");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        httpWebRequest.Timeout = 120000;
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

                            if (response.Contains("ERROR") == true || response.Contains("Greška") == true || response.Contains("Error") == true || response.Contains("Duplikat") == true)
                            {
                                MessageBox.Show("Slanje nije uspelo\n" + response.ToString());
                                ins.InsApiLog("IzlFak-" + FaStFak.ToString(), jsonOutput, response);
                                return;
                            }
                            else
                            {
                                using (SqlConnection conn = new SqlConnection(connect))
                                {
                                    using (SqlCommand cmd2 = conn.CreateCommand())
                                    {
                                        //string pantheon = JObject.Parse(result)["Id"].ToString().Trim();
                                        //cmd2.CommandText = "UPDATE Faktura SET Status = 1,PantheonID='" + pantheon + "' WHERE FaStFak = " + FaStFak;
                                        cmd2.CommandText = "UPDATE Faktura SET Status = 1 WHERE FaStFak = " + FaStFak;
                                        conn.Open();
                                        cmd2.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                    using (SqlCommand cmd3 = conn.CreateCommand())
                                    {
                                        cmd3.CommandText = "Update Najava SET Faktura='" + FaStFak + "' Where ID=" + Najava;
                                        conn.Open();
                                        cmd3.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                }
                            }
                        }
                        ins.InsApiLog("IzlFak-" + FaStFak.ToString(), jsonOutput, response);
                    }
                }
                if (fakturaTip == 2)
                {
                    query1 = "Select RTrim(CRMID) as CRMDocumentId,RTrim(Tip) as DocType,CONVERT(VARCHAR, DatumIzdavanja, 23) as Date,Rtrim(PaNaziv) as Issuer,RTrim(UlFak.Valuta) as Currency,UlFak.Kurs as FXRate,RTrim(FakturaBr) as Doc1, " +
                "CONVERT(VARCHAR, DatumIzdavanja, 23) as DateDoc1,CONVERT(VARCHAR, DatumPDVa, 23) as DateVAT,CONVERT(VARCHAR, DatumValute, 23) as DateDue,Rtrim(PredvidjanjeID) as PredvidjanjeId, " +
                "UlFak.Referent as UserId,UlFak.Napomena as Napomena,UlFak.ID as ID " +
                "from UlFak " +
                "Inner join Partnerji on UlFak.IDDobavljaca = Partnerji.PaSifra " +
                "inner join Predvidjanje on UlFak.Predvidjanje = Predvidjanje.ID Where UlFak.Status=0 and CRMID=" + CRMID;

                    query2 = "select RB as No,Rtrim(MpStaraSif) as Ident,CAST(Kolicina AS DECIMAL(10, 2)) as Qty,CAST(Cena/Kolicina AS DECIMAL(10, 2)) as Price,Rtrim(NosiociTroskova.NosilacTroska) as CostDrv,RTrim(JM) as JNT,'' as Proizvod,IDFak " +
                        "From UlFakPostav " +
                        "inner join MaticniPodatki on UlFakPostav.Mp = MaticniPodatki.MpSifra " +
                        "inner join NosiociTroskova on UlFakPostav.NosilacTroska = NosiociTroskova.ID " +
                        "order by IDFak asc";

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
            }
            catch (Exception ex)
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
            this.Close();
        }
    } 
}
