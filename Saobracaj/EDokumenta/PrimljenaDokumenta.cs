using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Saobracaj.eDokumenta
{
    public partial class PrimljenaDokumenta : Form
    {
        XmlDocument doc = new XmlDocument();
        string Faktura = "";
        DateTime vremeOd;
        DateTime vremeDo;
        public PrimljenaDokumenta()
        {
            InitializeComponent();
        }
        public class Statusi
        {
            public string Opis { get; set; }
            public string Status { get; set; }
        }
        private void FillCombo()
        {
            var ds = new List<Statusi>();
            ds.Add(new Statusi { Opis = "10 - U pripremi", Status = "10" });
            ds.Add(new Statusi { Opis = "20 - Validacija", Status = "20" });
            ds.Add(new Statusi { Opis = "30 - Novo", Status = "30" });
            ds.Add(new Statusi { Opis = "40 - Preuzeto", Status = "40" });
            ds.Add(new Statusi { Opis = "45 - Opozvano", Status = "45" });
            ds.Add(new Statusi { Opis = "50 - Neuspešno", Status = "50" });

            cbo_Status.DataSource = ds;
            cbo_Status.DisplayMember = "Opis";
            cbo_Status.ValueMember = "Status";
        }
        private void QueryInbox()
        {
            //queryDocumentProcessStatusInbox
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/queryDocumentProcessStatusInbox");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\n" +
                    "Username:Test," +
                    "\nPassword:\"TestPass\"," +
                    "\nCompanyId:\"" + Saobracaj.Sifarnici.frmLogovanje.PIB + "\"," +
                    "\nSoftwareId:\"MtaSoft-001\"\n}";

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic dynJson = JsonConvert.DeserializeObject(result);

                dataGridView1.DataSource = dynJson;


                dataGridView1.Columns[0].Width = 70;
                dataGridView1.Columns[1].HeaderText = "Faktura";
                dataGridView1.Columns[1].Width = 70;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].HeaderText = "Tip dokumenta";
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[4].Width = 70;
                dataGridView1.Columns[4].HeaderText = "Status";
                dataGridView1.Columns[5].Width = 60;
                dataGridView1.Columns[5].HeaderText = "Pib primaoca";
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].HeaderText = "Posiljalac";
                dataGridView1.Columns[7].Width = 230;
                dataGridView1.Columns[8].Width = 130;
                dataGridView1.Columns[9].Width = 130;
                dataGridView1.Columns[10].Width = 130;
                dataGridView1.Columns[11].HeaderText = "SEF Status";
                dataGridView1.Columns[11].Width = 50;
                dataGridView1.Columns[12].HeaderText = "SEF Status-Opis";
                dataGridView1.Columns[12].Width = 70;
                dataGridView1.Columns[13].HeaderText = "SEF Kod Greske";
                dataGridView1.Columns[13].Width = 60;
                dataGridView1.Columns[14].HeaderText = "Razlog odbijanja";
                dataGridView1.Columns[14].Width = 150;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int status = Convert.ToInt32(row.Cells[4].Value);
                    string pom = row.Cells[11].Value.ToString();
                    int sefStatus = 0;
                    if (pom != "")
                    {
                        sefStatus = Convert.ToInt32(row.Cells[11].Value);
                    }
                    if (status == 40)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    if (status == 30)
                    {
                        row.DefaultCellStyle.BackColor = Color.Wheat;
                    }
                    if (status == 45)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                    if (sefStatus == 1)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                    if (status == 50)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
        }

        private void PrimljenaDokumenta_Load(object sender, EventArgs e)
        {
            QueryInbox();
            FillCombo();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txt_EID.Text = row.Cells[0].Value.ToString();
                    Faktura = row.Cells[1].Value.ToString();
                }
            }
        }
        private XmlDocument GetXmlDocument(string xmlString)
        {
            string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (xmlString.StartsWith(byteOrderMarkUtf8, StringComparison.Ordinal))
            {
                xmlString = xmlString.Remove(0, byteOrderMarkUtf8.Length);
            }

            doc.LoadXml(xmlString);

            return doc;
        }
        private void btn_Preuzmi_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/receive");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\n" +
                    "Username:Test," +
                    "\nPassword:\"TestPass\"," +
                    "\nCompanyId:\"" + Saobracaj.Sifarnici.frmLogovanje.PIB + "\"," +
                    "\nSoftwareId:\"MtaSoft-001\"," +
                    "\nElectronicId:\"" + txt_EID.Text.ToString().Trim() + "\"\n}";

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                string json = JsonConvert.SerializeObject(result);
                doc = GetXmlDocument(result);

                var embeddedFile = doc.GetElementsByTagName("EmbeddedDocumentBinaryObject", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                string pdfBase64 = string.Empty;
                if (embeddedFile.Count != 0)
                {
                    var file = embeddedFile[0].InnerText;
                    pdfBase64 = file;
                    string[] path = { Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "UF" };
                    string save = Path.Combine(path);
                    byte[] pdf = Convert.FromBase64String(file);
                    File.WriteAllBytes(save + @"\" + Faktura + ".pdf", pdf);
                    //File.Open(@"\\192.168.1.6\uf\" + Faktura + ".pdf", FileMode.Open);
                    string path2 = save + @"\" + Faktura + ".pdf";
                    //path2 = path2.Replace("192.168.1.6", "WSS");
                    System.Diagnostics.Process.Start(path2);
                }
            }
        }

        private void btn_Ospori_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/UpdateDokumentProcessStatus");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\n" +
                    "Username:Test," +
                    "\nPassword:\"TestPass\"," +
                    "\nCompanyId:\"" + Saobracaj.Sifarnici.frmLogovanje.PIB + "\"," +
                    "\nSoftwareId:\"MtaSoft-001\"," +
                    "\nElectronicId:\"" + txt_EID.Text.ToString().Trim() + "\"," +
                    "\nStatusId:1," +
                    "\nRejectReason:\"" + txt_Ospori.Text.ToString().Trim() + "\"}";

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                string json = JsonConvert.SerializeObject(result);
                txt_Response.Text = json.ToString();
            }
        }

        private void btn_Datum_Click(object sender, EventArgs e)
        {
            vremeOd = Convert.ToDateTime(dateTimePicker1.Value.ToString());
            vremeDo = Convert.ToDateTime(dateTimePicker2.Value.ToString());


            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/queryInbox");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\n" +
                    "Username:Test," +
                    "\nPassword:\"TestPass\"," +
                    "\nCompanyId:\"" + Saobracaj.Sifarnici.frmLogovanje.PIB + "\"," +
                    "\nSoftwareId:\"MtaSoft-001\"," +
                    "\nFrom:\"" + vremeOd.ToString("yyyy-MM-dd") + "\"," +
                    "\nTo:\"" + vremeDo.ToString("yyyy-MM-dd") + "\"}";


                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic dynJson = JsonConvert.DeserializeObject(result);

                dataGridView1.DataSource = dynJson;

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Nema dokumenata za izabrani period");
                    QueryInbox();
                }
                else
                {
                    dataGridView1.Columns[0].Width = 70;
                    dataGridView1.Columns[1].HeaderText = "Faktura";
                    dataGridView1.Columns[1].Width = 70;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].HeaderText = "Tip dokumenta";
                    dataGridView1.Columns[3].Width = 70;
                    dataGridView1.Columns[4].Width = 70;
                    dataGridView1.Columns[4].HeaderText = "Status";
                    dataGridView1.Columns[5].Width = 60;
                    dataGridView1.Columns[5].HeaderText = "Pib primaoca";
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].HeaderText = "Posiljalac";
                    dataGridView1.Columns[7].Width = 230;
                    dataGridView1.Columns[8].Width = 130;
                    dataGridView1.Columns[9].Width = 130;
                    dataGridView1.Columns[10].Width = 130;
                    /*dataGridView1.Columns[11].HeaderText = "SEF Status";
                    dataGridView1.Columns[11].Width = 50;
                    dataGridView1.Columns[12].HeaderText = "SEF Status-Opis";
                    dataGridView1.Columns[12].Width = 70;
                    dataGridView1.Columns[13].HeaderText = "SEF Kod Greske";
                    dataGridView1.Columns[13].Width = 60;
                    dataGridView1.Columns[14].HeaderText = "Razlog odbijanja";
                    dataGridView1.Columns[14].Width = 150;*/

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        int status = Convert.ToInt32(row.Cells[4].Value);
                        string sefStatus = row.Cells[12].Value.ToString();

                        if (status == 40 && sefStatus == "APPROVED")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        if (status == 30)
                        {
                            row.DefaultCellStyle.BackColor = Color.Wheat;
                        }
                        if (status == 45)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightSalmon;
                        }
                        if (status == 50)
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }

            }
        }

        private void btn_Status_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/queryInbox");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\n" +
                    "Username:Test," +
                    "\nPassword:\"TestPass\"," +
                    "\nCompanyId:\"" + Saobracaj.Sifarnici.frmLogovanje.PIB + "\"," +
                    "\nSoftwareId:\"MtaSoft-001\"," +
                    "\nStatusId:" + cbo_Status.SelectedValue + "}";

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic dynJson = JsonConvert.DeserializeObject(result);

                dataGridView1.DataSource = dynJson;

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Nema dokumenata u izabranom statusu");
                    QueryInbox();
                }
                else
                {
                    dataGridView1.Columns[0].Width = 70;
                    dataGridView1.Columns[1].HeaderText = "Faktura";
                    dataGridView1.Columns[1].Width = 70;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].HeaderText = "Tip dokumenta";
                    dataGridView1.Columns[3].Width = 70;
                    dataGridView1.Columns[4].Width = 70;
                    dataGridView1.Columns[4].HeaderText = "Status";
                    dataGridView1.Columns[5].Width = 60;
                    dataGridView1.Columns[5].HeaderText = "Pib primaoca";
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].HeaderText = "Posiljalac";
                    dataGridView1.Columns[7].Width = 230;
                    dataGridView1.Columns[8].Width = 130;
                    dataGridView1.Columns[9].Width = 130;
                    dataGridView1.Columns[10].Width = 130;
                    /*dataGridView1.Columns[11].HeaderText = "SEF Status";
                    dataGridView1.Columns[11].Width = 50;
                    dataGridView1.Columns[12].HeaderText = "SEF Status-Opis";
                    dataGridView1.Columns[12].Width = 70;
                    dataGridView1.Columns[13].HeaderText = "SEF Kod Greske";
                    dataGridView1.Columns[13].Width = 60;
                    dataGridView1.Columns[14].HeaderText = "Razlog odbijanja";
                    dataGridView1.Columns[14].Width = 150;*/

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        int status = Convert.ToInt32(row.Cells[4].Value);

                        if (status == 40)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        if (status == 30)
                        {
                            row.DefaultCellStyle.BackColor = Color.Wheat;
                        }
                        if (status == 45)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightSalmon;
                        }
                        if (status == 50)
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }
            }
        }
    }
}