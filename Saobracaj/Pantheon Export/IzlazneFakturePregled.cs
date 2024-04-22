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
    public partial class IzlazneFakturePregled : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        public IzlazneFakturePregled()
        {
            InitializeComponent();
            FillGV();
        }
        private void FillGV()
        {
            var select = "Select FaStFak as ID," +
                "(Select MAX(RTrim(NosiociTroskova.NazivNosiocaTroska)) from FakturaPostav inner join NosiociTroskova on FakturaPostav.NosilacTroska = NosiociTroskova.ID where FakturaPostav.FaPStFak = Faktura.FaStFak) as [NT Naziv]," +
                "Faktura.Status as Status,Format(FaDatFak, 'dd.MM.yyyy') as Datum,FaStatus as FaStatus,FaModul as FaModul,RTrim(PaNaziv) as Kupac,Format(FaDatVal, 'dd.MM.yyyy') as [Datum Valute],Kurs as Kurs," +
                "FaValutaCene as Valuta,Format(FaObdobje, 'dd.MM.yyyy') as DatumPDV,MestoUtovara as [Mesto Utovara],Format(DatumUtovara, 'dd.MM.yyyy') as [Datum Utovara],FaDostMesto as [Mesto Istovara], " +
                "Format(DatumIstovara, 'dd.MM.yyyy') as [Datum Istovara],(Rtrim(deIme) + ' ' + RTrim(DePriimek)) as Referent ,RTrim(Izjave.Naziv) as Izjava,RTrim(FaOpomba2) as Napomena,FaPartPlac as [Platilac]," +
                "FaRefer as [FaRefer],(Select Sum(FaPZnesSk) from FakturaPostav where FakturaPostav.FaPStFak = Faktura.FaStFak ) as Iznos,FaOpomba1," +
                "(Select SUM(IznosRSD) From FakturaPostav Where FakturaPostav.FaPStFak = Faktura.FaStFak) as IznosRSD,RTrim(FaVPisalIme) as Korisnik,CRMID,PantheonID " +
                "From Faktura " +
                "inner join Partnerji on Faktura.FaPartPlac = Partnerji.PaSifra " +
                "Inner join Delavci on Faktura.FaRefer = Delavci.DeSifra " +
                "inner join Izjave on Faktura.FaOpomba1 = Izjave.ID order by FaStFak desc";

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


            dataGridView1.Columns["FaStatus"].Visible=false;
            dataGridView1.Columns["FaModul"].Visible = false;
            dataGridView1.Columns["Mesto Utovara"].Visible = false;
            dataGridView1.Columns["Mesto Istovara"].Visible = false;
            dataGridView1.Columns["Datum Utovara"].Visible = false;
            dataGridView1.Columns["Datum Istovara"].Visible = false;
            dataGridView1.Columns["Referent"].Visible = false;
            dataGridView1.Columns["Izjava"].Visible = false;

            dataGridView1.Columns["ID"].Width = 80;
            dataGridView1.Columns["NT Naziv"].Width = 150;
            dataGridView1.Columns["Status"].Width = 50;
            dataGridView1.Columns["Kupac"].Width = 200;
            dataGridView1.Columns["Kurs"].Width = 60;
            dataGridView1.Columns["Valuta"].Width = 50;
            dataGridView1.Columns["Napomena"].Width = 300;
            dataGridView1.Columns["Platilac"].Visible = false;
            dataGridView1.Columns["FaRefer"].Visible = false;
            dataGridView1.Columns["Iznos"].Width = 100;
            dataGridView1.Columns["FaOpomba1"].Visible = false;
            dataGridView1.Columns["IznosRSD"].Visible = false;
        }
        string Valuta, MestoUtovara, MestoIstovara, Napomena,Vrsta;
        int ID, Primalac, Referent, Izjava,StatusFak;
        DateTime DatumDokumenta, DatumPDV, DatumValute, DatumUtovara, DatumIstovara;
        decimal Kurs;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        ID = Convert.ToInt32(row.Cells["ID"].Value);
                        DatumDokumenta = Convert.ToDateTime(row.Cells["Datum"].Value.ToString());
                        Vrsta = row.Cells["FaModul"].Value.ToString();
                        Primalac = Convert.ToInt32(row.Cells["Platilac"].Value);
                        Valuta = row.Cells["Valuta"].Value.ToString().TrimEnd();
                        Kurs = Convert.ToDecimal(row.Cells["Kurs"].Value.ToString());
                        //DateTime pdvPom = Convert.ToDateTime(row.Cells["FaObdobje"].Value); 
                        DatumPDV = Convert.ToDateTime(row.Cells["DatumPDV"].Value);
                        DatumValute = Convert.ToDateTime(row.Cells["Datum Valute"].Value.ToString());
                        MestoUtovara = row.Cells["Mesto Utovara"].Value.ToString();
                        DatumUtovara = Convert.ToDateTime(row.Cells["Datum Utovara"].Value.ToString());
                        MestoIstovara = row.Cells["Mesto Istovara"].Value.ToString();
                        DatumIstovara = Convert.ToDateTime(row.Cells["Datum Istovara"].Value.ToString());
                        Referent = Convert.ToInt32(row.Cells["FaRefer"].Value);
                        Izjava = Convert.ToInt32(row.Cells["FaOpomba1"].Value.ToString());
                        Napomena = row.Cells["Napomena"].Value.ToString();
                        StatusFak = Convert.ToInt32(row.Cells["Status"].Value.ToString());
                    }
                }
            }
            catch { }
        }
        public class Uplata
        {
            public int SetType { get; set; }
            public string FakturaBrP { get; set; }
            public string KlijentId { get; set; }
            public string FakturaBr { get; set; }
            public DateTime DatumPlacanja { get; set; }
            public string Valuta { get; set; }
            public decimal Iznos { get; set; }
        }
        public class JsonResponse
        {
            public string Status { get; set; }
            public string Id { get; set; }
            public string Poruka { get; set; }
            public List<Uplata> Uplate { get; set; }
        }

        public int CRMID;
        public decimal Iznos;
        public string ValutaResponse;
        private void btnGetUplate_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:3333/api/UplateKupaca/GetUplateKupaca");
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
                        Iznos = Convert.ToDecimal(row.Cells["Iznos"].Value.ToString().Replace(",", "."));
                        ValutaResponse = row.Cells["Valuta"].Value.ToString().TrimEnd();

                        using (SqlConnection conn = new SqlConnection(connect))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandText = "UPDATE Faktura SET FaStatus='ZA',FaValutaCene='" + ValutaResponse + "', FaZnesFak='" + Iznos + "' WHERE CRMID =" + CRMID;
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Nema novih uplata");
                }
            }
            FillGV();
        }

        private void IzlazneFakturePregled_Load(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
        }
        int status;
        int crm;
        private void btnExportProd_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    status = Convert.ToInt32(row.Cells["Status"].Value);
                    crm = Convert.ToInt32(row.Cells["CRMID"].Value);

                    if (status == 0)
                    {
                        string query1 = "";
                        string query2 = "";
                        if (Convert.ToDecimal(row.Cells["IznosRSD"].Value) > Convert.ToDecimal(row.Cells["Iznos"].Value))
                        {
                            query1 = "SELECT CRMID AS CRMDocumentID, RTrim(FaModul) AS DocType, CONVERT(VARCHAR, FaVpisalDat, 23) AS Date, RTrim(PaNaziv) AS Receiver," +
                                "'RSD' AS Currency, '1' AS FXRate, '' AS Doc1, '' AS DateDoc1, '' AS Doc2, '' AS DateDoc2," +
                                "CONVERT(VARCHAR, FaObdobje, 23) AS DateVAT, CONVERT(VARCHAR, FaDatVal, 23) AS DateDue, RTrim(IDPantheon) AS Statement," +
                                "RTrim(FaRefer) AS UserId, RTrim(FaOpomba2) AS Napomena,FaStFak " +
                                "FROM Faktura " +
                                "INNER JOIN Partnerji ON Faktura.FaPartPlac = Partnerji.PaSifra " +
                                "INNER JOIN Izjave ON Faktura.FaOpomba1 = Izjave.ID " +
                                "WHERE Faktura.Status = 0 and CRMID=" + crm;

                            query2 = "SELECT FakturaPostav.FaPStFak, FakturaPostav.FapStPos AS No, RTrim(MaticniPodatki.MpStaraSif) AS Ident," +
                               "CAST(FakturaPostav.FaPkolOdpr AS DECIMAL(10, 2)) AS Qty," +
                               "CAST(FakturaPostav.IznosRSD AS DECIMAL(10, 2)) AS Price, RTrim(NosiociTroskova.NosilacTroska) as CostDrv, RTrim(MeNaziv) AS JNT, '' AS Product " +
                               "FROM FakturaPostav " +
                               "INNER JOIN MaticniPodatki ON FakturaPostav.FaPSifra = MaticniPodatki.MpSifra " +
                               "INNER JOIN NosiociTroskova ON FakturaPostav.NosilacTroska = NosiociTroskova.ID " +
                               "Inner join MerskeEnote on FakturaPostav.FaPEM = MerskeEnote.MeSifra " +
                               "ORDER BY FakturaPostav.FapStPos ASC";
                        }
                        else
                        {
                            query1 = "SELECT CRMID AS CRMDocumentID, RTrim(FaModul) AS DocType, CONVERT(VARCHAR, FaVpisalDat, 23) AS Date, RTrim(PaNaziv) AS Receiver," +
                               "RTrim(FaValutaCene) AS Currency, Cast(Kurs as DECIMAL(10,2)) AS FXRate, '' AS Doc1, '' AS DateDoc1, '' AS Doc2, '' AS DateDoc2," +
                               "CONVERT(VARCHAR, FaObdobje, 23) AS DateVAT, CONVERT(VARCHAR, FaDatVal, 23) AS DateDue, RTrim(IDPantheon) AS Statement," +
                               "RTrim(FaRefer) AS UserId, RTrim(FaOpomba2) AS Napomena,FaStFak " +
                               "FROM Faktura " +
                               "INNER JOIN Partnerji ON Faktura.FaPartPlac = Partnerji.PaSifra " +
                               "INNER JOIN Izjave ON Faktura.FaOpomba1 = Izjave.ID " +
                               "WHERE Faktura.Status = 0 and CRMID=" + crm;

                            query2 = "SELECT FakturaPostav.FaPStFak, FakturaPostav.FapStPos AS No, RTrim(MaticniPodatki.MpStaraSif) AS Ident," +
                               "CAST(FakturaPostav.FaPkolOdpr AS DECIMAL(10, 2)) AS Qty," +
                               "CAST(FakturaPostav.FaPCenaEM AS DECIMAL(10, 2)) AS Price, RTrim(NosiociTroskova.NosilacTroska) as CostDrv, RTrim(MeNaziv) AS JNT, '' AS Product " +
                               "FROM FakturaPostav " +
                               "INNER JOIN MaticniPodatki ON FakturaPostav.FaPSifra = MaticniPodatki.MpSifra " +
                               "INNER JOIN NosiociTroskova ON FakturaPostav.NosilacTroska = NosiociTroskova.ID " +
                               "Inner join MerskeEnote on FakturaPostav.FaPEM = MerskeEnote.MeSifra " +
                               "ORDER BY FakturaPostav.FapStPos ASC";
                        }
                        string FaStFak = "";

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
                                    ins.InsApiLog("IzlFak-" + ID.ToString(), jsonOutput, response);
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
                                    }
                                }
                            }
                            ins.InsApiLog("IzlFak-" + ID.ToString(), jsonOutput, response);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Faktura CRMID:" + crm + " je već poslata sinhornizacijom!");
                        return;
                    }
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
                    ins.DeleteFaktura(Convert.ToInt32(row.Cells["ID"]));
                }
            }
            FillGV();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Pantheon_Export.IzlazneFakture frm = new IzlazneFakture();
            frm.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Pantheon_Export.IzlazneFakture frm = new IzlazneFakture(ID, DatumDokumenta, Vrsta, Primalac, Valuta, Kurs, DatumPDV, DatumValute, MestoUtovara, DatumUtovara, MestoIstovara, DatumIstovara, Referent, Izjava, Napomena, StatusFak);
            frm.Show();
        }
    }
}
