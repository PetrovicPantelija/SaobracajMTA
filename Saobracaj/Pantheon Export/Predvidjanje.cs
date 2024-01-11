using Org.BouncyCastle.Asn1.X509.Qualified;
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
    public partial class Predvidjanje : Form
    {
        bool status = false;
        private string connect = Sifarnici.frmLogovanje.connectionString;
        public Predvidjanje()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            panel1.Visible = false;
        }
        private void FillGV()
        {
            var select = "select p.ID,p.IDp,PredvidjanjeID,p.PredvodjanjePoz,p.Datum,PaNaziv,NosiociTroskova.NazivNosiocaTroska,RTrim(SifraSubjekta) as Odeljenje,Iznos,Valuta,NosiociTroskova.NosilacTroska " +
                "From Predvidjanje p  " +
                "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
                "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
                "inner join Odeljenja on p.Odeljenje=Odeljenja.ID Where p.Status = 0 order by p.ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
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
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].HeaderText = "Poz";
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.Columns[5].Width = 350;
            dataGridView1.Columns[6].Width = 200;
            dataGridView1.Columns[7].Width = 200;
            // dataGridView1.Columns[4].Visible = false;
            // dataGridView1.Columns[6].Visible = false;
            /*
                        dataGridView1.Columns["ID"].Width = 50;
                        dataGridView1.Columns["RB"].Width = 50;
                        dataGridView1.Columns["PredvidjanjeID"].Width = 100;
                        //dataGridView1.Columns["PredividjanjeID"].HeaderText = "PredvidjanjeID";
                        dataGridView1.Columns["Datum"].Width = 150;
                        dataGridView1.Columns["PaNaziv"].Width = 330;
                        dataGridView1.Columns["Nosilac troska"].Width = 80;
                        dataGridView1.Columns["Odeljenje"].Width = 150;
                        dataGridView1.Columns["Iznos"].Width = 150;
                        dataGridView1.Columns["Valuta"].Width = 50;*/

            var select2 = "Select * from Predvidjanje";
            var da = new SqlDataAdapter(select2, conn);
            var ds2 = new DataSet();
            da.Fill(ds2);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds2.Tables[0];


            dataGridView2.BorderStyle = BorderStyle.FixedSingle;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var valuta = "Select VaSifra,VaNaziv From Valute";
            var valutaDa = new SqlDataAdapter(valuta, conn);
            var valutaDS = new DataSet();
            valutaDa.Fill(valutaDS);
            cboValuta.DataSource = valutaDS.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

            var query = "Select PaSifra,PaNaziv from Partnerji order by PaSifra";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboSubjekt.DataSource = ds.Tables[0];
            cboSubjekt.DisplayMember = "PaNaziv";
            cboSubjekt.ValueMember = "PaSifra";

            var nosilac = "Select ID,NazivNosiocaTroska from NosiociTroskova order by ID desc";
            var nosilacDa = new SqlDataAdapter(nosilac, conn);
            var nosilacDS = new DataSet();
            nosilacDa.Fill(nosilacDS);
            cboNosilacTroska.DataSource = nosilacDS.Tables[0];
            cboNosilacTroska.DisplayMember = "NazivNosiocaTroska";
            cboNosilacTroska.ValueMember = "ID";

            var query2 = "Select ID,SifraSubjekta from Odeljenja order by Naziv2 desc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            cboOdeljenje.DataSource = ds2.Tables[0];
            cboOdeljenje.DisplayMember = "SifraSubjekta";
            cboOdeljenje.ValueMember = "ID";
        }
        int RB;
        string predvidjanjeID;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {/*
                        txtID.Text = row.Cells[0].Value.ToString();
                        RB = Convert.ToInt32(row.Cells[1].Value);
                        predvidjanjeID = row.Cells[2].Value.ToString();
                        txtID.Text = row.Cells["ID"].Value.ToString();
                        dateTimePicker1.Value = Convert.ToDateTime(row.Cells["Datum"].Value);
                        cboSubjekt.SelectedValue = Convert.ToInt32(row.Cells[4].Value.ToString());
                        cboNosilacTroska.SelectedValue = Convert.ToInt32(row.Cells[6].Value);
                        //txtOdeljenje.Text = row.Cells["Odeljenje"].Value.ToString().TrimEnd();
                        txtIznos.Text = row.Cells["Iznos"].Value.ToString();*/
                    }
                }
            }
            catch { }
        }
        int idP;
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;

            
            var query = "Select (Max(IDP)+1) as IDP From Predvidjanje";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0].GetType() == typeof(DBNull))
                { idP = 1; }
                else
                {
                    idP = Convert.ToInt32(dr[0].ToString());
                }
                //idP = dr[0].ToString();
                txtID.Text = idP.ToString();
                //int pom = DateTime.Now.Year;
                string g = DateTime.Now.ToString("yy");
                txtPredvidjanje.Text = "SUPP-" + idP.ToString() + "-" +g;
            }
            conn.Close();
        }
        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            if (status == true)
            {
                int posao=0;
                var query = "Select Posao From NosiociTroskova Where ID="+Convert.ToInt32(cboNosilacTroska.SelectedValue);
                SqlConnection conn = new SqlConnection(connect);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    posao = Convert.ToInt32(dr[0].ToString());
                }
                    ins.InsPredvidjanje(Convert.ToInt32(idP),txtPredvidjanje.Text.ToString().TrimEnd(), poz, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 0,posao);
            }
            else
            {
                //ins.UpdPredvidjanje(Convert.ToInt32(txtID.Text), predvidjanjeID, RB, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), txtOdeljenje.Text.ToString(), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 1);
            }
            FillGV();
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.DelPredvidjanje(Convert.ToInt32(txtID.Text));
            FillGV();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int ID;
            string PredvidjanjeID, Poz, Kupac, NTNaziv, Odeljenje, Iznos, Valuta, Datum;
            DateTime datumPom;
            try
            {
                if(dataGridView1.Rows.Count > 0 ) {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                        PredvidjanjeID = row.Cells[2].Value.ToString().TrimEnd();
                        Poz = row.Cells[3].Value.ToString().TrimEnd();
                        datumPom = Convert.ToDateTime(row.Cells[4].Value.ToString());
                        Datum = datumPom.ToString("yyyy-MM-dd");
                        Kupac = row.Cells[5].Value.ToString().TrimEnd();
                        NTNaziv = row.Cells[10].Value.ToString().TrimEnd();
                        Odeljenje = row.Cells[7].Value.ToString().TrimEnd();
                        Iznos = row.Cells[8].Value.ToString();
                        Valuta = row.Cells[9].Value.ToString();

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:6333/api/Predvidjanje/PredvidjanjePost");
                        httpWebRequest.ContentType = "application/json";
                        httpWebRequest.Method = "POST";
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            string json = "{" +
                                           "\n\"PredvidjanjeID\":\"" + PredvidjanjeID + "\"," +
                                           "\n\"PredvidjanjePoz\":\"" + Poz + "\"," +
                                           "\n\"Datum\":\"" + Datum + "\"," +
                                           "\n\"Subject\":\"" + Kupac + "\"," +
                                          "\n\"Strn\":\"" + NTNaziv + "\"," +
                                          "\n\"Odeljenje\":\"" + Odeljenje + "\"," +
                                          "\n\"Iznos\":\"" + Iznos + "\"," +
                                           "\n\"Valuta\":\"" + Valuta + "\"\n}";
                            MessageBox.Show(json.ToString());
                            streamWriter.Write(json);
                         
                        }
                        string response = "";
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            response = result.ToString();
                            MessageBox.Show(response.ToString());
                            if (response.Contains("Error") == true || response.Contains("Greška") == true || response.Contains("ERROR") == true)
                            {
                                MessageBox.Show("Slanje nije uspelo \n" + response.ToString());
                                return;
                            }
                            else
                            {
                                using (SqlConnection conn = new SqlConnection(connect))
                                {
                                    using (SqlCommand cmd = conn.CreateCommand())
                                    {
                                        cmd.CommandText = "UPDATE Predvidjanje SET Status = 1  WHERE ID = " + ID;
                                        conn.Open();
                                        cmd.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                }
                                MessageBox.Show("Uspešan prenos");
                            }
                        }
                    }

                }
            }
            catch { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
           // panel1.Size = new Size(1140, 507);
            panel1.Location = new System.Drawing.Point(12, 32);
            panel1.Size = new Size(1140, 507);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
        int poz = 1;
        private void button2_Click(object sender, EventArgs e)
        {
            //predvidjanje = txtPredvidjanje.Text.ToString().TrimEnd();
            var query = "Select (Max(PredvodjanjePoz)) as Poz From Predvidjanje Where IDP=" + Convert.ToInt32(txtID.Text);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                poz= Convert.ToInt32(dr[0])+1;
            }
            conn.Close();
            int posao = 0;
            var select = "Select Posao from NosiociTroskova Where ID=" + Convert.ToInt32(cboNosilacTroska.SelectedValue);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(select, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                posao = Convert.ToInt32(dr2[0].ToString());
            }
            conn.Close();
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.InsPredvidjanje(Convert.ToInt32(idP),txtPredvidjanje.Text.ToString().TrimEnd(), poz, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 0,posao);
            FillGV();
        }
    }
}
