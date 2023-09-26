using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Linq.Dynamic;

namespace Saobracaj.eDokumenta
{
    public partial class PoslataDokumenta : Form
    {
        string statusID = "";
        DateTime vremeOd;
        DateTime vremeDo;
        public PoslataDokumenta()
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
            ds.Add(new Statusi { Opis = "30 - Poslato", Status = "30" });
            ds.Add(new Statusi { Opis = "40 - Dostavljeno", Status = "40" });
            ds.Add(new Statusi { Opis = "45 - Opozvano", Status = "45" });
            ds.Add(new Statusi { Opis = "50 - Neuspešno", Status = "50" });

            cbo_Status.DataSource = ds;
            cbo_Status.DisplayMember = "Opis";
            cbo_Status.ValueMember = "Status";
        }

        private void QueryOutbox()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/queryDocumentProcessStatusOutbox");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\n" +
                    "Username:Test," +
                    "\nPassword:\"TestPass\"," +
                    "\nCompanyId:\""+Saobracaj.Sifarnici.frmLogovanje.PIB+"\"," +
                    "\nSoftwareId:\"MtaSoft-001\"\n}";

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                dynamic dynJson = JsonConvert.DeserializeObject(result);

                dataGridView1.DataSource = dynJson;

                dataGridView1.Columns[2].Width = 70;
                dataGridView1.Columns[3].HeaderText = "Faktura";
                dataGridView1.Columns[3].Width = 70;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Tip dokumenta";
                dataGridView1.Columns[5].Width = 70;
                dataGridView1.Columns[6].Width = 70;
                dataGridView1.Columns[6].HeaderText = "Status";
                dataGridView1.Columns[6].Width = 60;
                dataGridView1.Columns[7].HeaderText = "Pib primaoca";
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].HeaderText = "Primalac";
                dataGridView1.Columns[9].Width = 230;
                dataGridView1.Columns[10].Width = 130;
                dataGridView1.Columns[11].Width = 130;
                dataGridView1.Columns[12].Width = 130;
                dataGridView1.Columns[13].Width = 130;
                dataGridView1.Columns[14].HeaderText = "SEF Status";
                dataGridView1.Columns[14].Width = 50;
                dataGridView1.Columns[15].HeaderText = "SEF Status-Opis";
                dataGridView1.Columns[15].Width = 70;
                dataGridView1.Columns[16].HeaderText = "SEF Kod Greske";
                dataGridView1.Columns[16].Width = 60;
                dataGridView1.Columns[17].HeaderText = "SEF Opis Greske";
                dataGridView1.Columns[17].Width = 130;
                dataGridView1.Columns[18].HeaderText = "Razlog odbijanja";
                dataGridView1.Columns[18].Width = 150;

                dataGridView1.Columns["Column1"].DisplayIndex = 18;
                dataGridView1.Columns["Column2"].DisplayIndex = 18;
                dataGridView1.Columns["Column2"].HeaderText = "Ponovi slanje";

                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 100;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    dataGridView1.Columns["Column1"].ReadOnly = true;
                    int status = Convert.ToInt32(row.Cells[6].Value);

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

        private void PoslataDokumenta_Load(object sender, EventArgs e)
        {
            QueryOutbox();
            FillCombo();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column1")
            {
                if (statusID != "30")
                {
                    string Poruka = "";
                    if (statusID == "45") { Poruka += "Ovaj dokument je već opozvan"; }
                    if (statusID == "40") { Poruka += "Primalac je preuzeo dokument"; }
                    MessageBox.Show("Mogu se opozvati samo dokumenta u statusu 30!\n" + Poruka);
                    return;
                }
                else
                {
                    if (MessageBox.Show("Da li ste sigurni da zelite da opozovete slanje ovog dokumenta?", "Opozovi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (txt_Opoziv.Text != "")
                        {
                            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/documentAction");
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Method = "POST";

                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                string json = "{\n" +
                                    "Username:Test," +
                                    "\nPassword:\"TestPass\"," +
                                    "\nCompanyId:\""+Saobracaj.Sifarnici.frmLogovanje.PIB+"\"," +
                                    "\nSoftwareId:\"MtaSoft-001\"," +
                                    "\nElectronicId:\"" + txt_EID.Text + "\"," +
                                    "\nApply:\"cancel\"," +
                                    "\nCancelReason:\"" + txt_Opoziv.Text + "\"}";

                                streamWriter.Write(json);
                            }
                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                                txt_Response.Text = result.ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mora se upisati razlog opozivanja dokumenta");
                            return;
                        }

                    }
                    else
                    {
                        return;
                    }
                }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column2")
            {
                if (MessageBox.Show("Da li ste sigurni da zelite da ponovo pošaljete mail obaveštenje dokumenta?", "Re-Send", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/documentAction");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "{\n" +
                            "Username:Test," +
                            "\nPassword:\"TestPass\"," +
                            "\nCompanyId:\""+Saobracaj.Sifarnici.frmLogovanje.PIB+"\"," +
                            "\nSoftwareId:\"MtaSoft-001\"," +
                            "\nElectronicId:\"" + txt_EID.Text + "\"," +
                            "\nApply:\"resend\"}";

                        streamWriter.Write(json);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        txt_Response.Text = result.ToString();
                    }
                }
                else
                {
                    return;
                }
            }
            QueryOutbox();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txt_EID.Text = row.Cells[2].Value.ToString();
                    statusID = row.Cells[6].Value.ToString();
                }
            }
        }

        private void btn_Status_Click(object sender, EventArgs e)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/queryOutbox");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\n" +
                    "Username:Test," +
                    "\nPassword:\"TestPass\"," +
                    "\nCompanyId:\""+Saobracaj.Sifarnici.frmLogovanje.PIB+"\"," +
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
                    QueryOutbox();
                }
                else
                {
                    dataGridView1.Columns[2].Width = 70;
                    dataGridView1.Columns[3].HeaderText = "Faktura";
                    dataGridView1.Columns[3].Width = 70;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[5].HeaderText = "Tip dokumenta";
                    dataGridView1.Columns[5].Width = 70;
                    dataGridView1.Columns[6].Width = 70;
                    dataGridView1.Columns[6].HeaderText = "Status";
                    dataGridView1.Columns[6].Width = 60;
                    dataGridView1.Columns[7].HeaderText = "Pib primaoca";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].HeaderText = "Primalac";
                    dataGridView1.Columns[9].Width = 230;
                    dataGridView1.Columns[10].Width = 130;
                    dataGridView1.Columns[11].Width = 130;
                    dataGridView1.Columns[12].Width = 130;
                    dataGridView1.Columns[13].Width = 130;

                    dataGridView1.Columns["Column1"].DisplayIndex = 14;
                    dataGridView1.Columns["Column2"].DisplayIndex = 14;
                    dataGridView1.Columns["Column2"].HeaderText = "Ponovi slanje";

                    dataGridView1.Columns[0].Width = 100;
                    dataGridView1.Columns[1].Width = 100;


                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        dataGridView1.Columns["Column1"].ReadOnly = true;
                        int status = Convert.ToInt32(row.Cells[6].Value);

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

        private void btn_Datum_Click(object sender, EventArgs e)
        {

            vremeOd = Convert.ToDateTime(dateTimePicker1.Value.ToString());
            vremeDo = Convert.ToDateTime(dateTimePicker2.Value.ToString());


            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://demo.moj-eracun.rs/apis/v2/queryOutbox");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\n" +
                    "Username:Test," +
                    "\nPassword:\"TestPass\"," +
                    "\nCompanyId:\""+Saobracaj.Sifarnici.frmLogovanje.PIB+"\"," +
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
                    MessageBox.Show("Nema dokumenata u izabranom statusu");
                    QueryOutbox();
                }
                else
                {

                    dataGridView1.Columns[2].Width = 70;
                    dataGridView1.Columns[3].HeaderText = "Faktura";
                    dataGridView1.Columns[3].Width = 70;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[5].HeaderText = "Tip dokumenta";
                    dataGridView1.Columns[5].Width = 70;
                    dataGridView1.Columns[6].Width = 70;
                    dataGridView1.Columns[6].HeaderText = "Status";
                    dataGridView1.Columns[6].Width = 60;
                    dataGridView1.Columns[7].HeaderText = "Pib primaoca";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].HeaderText = "Primalac";
                    dataGridView1.Columns[9].Width = 230;
                    dataGridView1.Columns[10].Width = 130;
                    dataGridView1.Columns[11].Width = 130;
                    dataGridView1.Columns[12].Width = 130;
                    dataGridView1.Columns[13].Width = 130;

                    dataGridView1.Columns["Column1"].DisplayIndex = 14;
                    dataGridView1.Columns["Column2"].DisplayIndex = 14;
                    dataGridView1.Columns["Column2"].HeaderText = "Ponovi slanje";

                    dataGridView1.Columns[0].Width = 100;
                    dataGridView1.Columns[1].Width = 100;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        dataGridView1.Columns["Column1"].ReadOnly = true;
                        int status = Convert.ToInt32(row.Cells[6].Value);

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
