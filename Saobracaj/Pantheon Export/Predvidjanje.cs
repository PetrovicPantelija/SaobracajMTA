using GMap.NET.Internals;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms.Tools;
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
        string korisnik = frmLogovanje.user;
        public Predvidjanje()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            panel1.Visible = false;
        }
        private void FillGV()
        {
            var select = "select p.ID,p.IDp,RTrim(PredvidjanjeID) as PredvidjanjeID,p.PredvodjanjePoz,p.Datum,RTrim(PaNaziv) as Subjekt,RTrim(NosiociTroskova.NazivNosiocaTroska) as NT,RTrim(SifraSubjekta) as Odeljenje,Iznos," +
                "Valuta,NosiociTroskova.NosilacTroska,p.Subjekt,RTrim(p.NosilacTroska) as NosilacTroska,p.Odeljenje,p.NajavaID,p.Napomena as Napomena,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident,JM,Kolicina,Ident,IznosRSD,p.Kurs,p.Korisnik " +
                "From Predvidjanje p " +
                "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
                "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
                "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
                "inner join Odeljenja on p.Odeljenje = Odeljenja.ID Where p.Status = 0 order by p.ID desc";
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
            dataGridView1.Columns[5].Width = 280;
            dataGridView1.Columns[6].Width = 200;
            dataGridView1.Columns[7].Width = 200;
            dataGridView1.Columns[9].Width = 60;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[19].Visible = false;
            dataGridView1.Columns[20].Visible = false;

            var select2 ="select p.ID,p.IDp,PredvidjanjeID,p.PredvodjanjePoz,p.Datum,RTrim(PaNaziv) as PaNaziv,RTrim(NosiociTroskova.NazivNosiocaTroska) as NT,RTrim(SifraSubjekta) as Odeljenje,Iznos,Valuta,NosiociTroskova.NosilacTroska,p.Subjekt,p.NosilacTroska,p.Odeljenje,p.NajavaID,p.Status as Status,p.Napomena as Napomena,JM,Kolicina,Ident,p.Korisnik " +
                "From Predvidjanje p  " +
                "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
                "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
                "inner join Odeljenja on p.Odeljenje=Odeljenja.ID order by p.ID desc";
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

            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 50;
            dataGridView2.Columns[2].Width = 80;
            dataGridView2.Columns[3].HeaderText = "Poz";
            dataGridView2.Columns[3].Width = 50;
            dataGridView2.Columns[5].Width = 350;
            dataGridView2.Columns[6].Width = 200;
            dataGridView2.Columns[7].Width = 200;
            dataGridView2.Columns[11].Visible = false;
            dataGridView2.Columns[12].Visible = false;
            dataGridView2.Columns[13].Visible = false;
            dataGridView2.Columns[14].Visible = false;
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var valuta = "Select VaSifra,RTrim(VaNaziv) as VaNaziv From Valute";
            var valutaDa = new SqlDataAdapter(valuta, conn);
            var valutaDS = new DataSet();
            valutaDa.Fill(valutaDS);
            cboValuta.DataSource = valutaDS.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

            var query = "Select PaSifra,RTrim(PaNaziv) as PaNaziv from Partnerji order by PaSifra";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboSubjekt.DataSource = ds.Tables[0];
            cboSubjekt.DisplayMember = "PaNaziv";
            cboSubjekt.ValueMember = "PaSifra";

            var nosilac = "Select ID,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska from NosiociTroskova order by ID desc";
            var nosilacDa = new SqlDataAdapter(nosilac, conn);
            var nosilacDS = new DataSet();
            nosilacDa.Fill(nosilacDS);
            cboNosilacTroska.DataSource = nosilacDS.Tables[0];
            cboNosilacTroska.DisplayMember = "NazivNosiocaTroska";
            cboNosilacTroska.ValueMember = "ID";

            var query2 = "Select ID,RTrim(SifraSubjekta) as SifraSubjekta from Odeljenja order by Naziv2 desc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            cboOdeljenje.DataSource = ds2.Tables[0];
            cboOdeljenje.DisplayMember = "SifraSubjekta";
            cboOdeljenje.ValueMember = "ID";


            var Mp = "Select MpSifra,(RTrim(MpStaraSif)+'-'+RTrim(MpNaziv)) as MpNaziv from MaticniPodatki";
            var MpDa = new SqlDataAdapter(Mp, conn);
            var MpDS = new DataSet();
            MpDa.Fill(MpDS);
            cboIdent.DataSource = MpDS.Tables[0];
            cboIdent.DisplayMember = "MpNaziv";
            cboIdent.ValueMember = "MpSifra";

            var jm = "Select RTrim(MeNaziv) as MeNaziv from MerskeEnote order by MeSifra";
            var jmDa = new SqlDataAdapter(jm, conn);
            var jmDS = new DataSet();
            jmDa.Fill(jmDS);
            cboJM.DataSource = jmDS.Tables[0];
            cboJM.DisplayMember = "MeNaziv";
            cboJM.ValueMember = "MeNaziv";
        }

        int ID, IDp,RB,Najava,StatusSelektovanog;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        txtIDPredvidjanja.Text = row.Cells[1].Value.ToString();
                        txtPredvidjanje.Text = row.Cells[2].Value.ToString();
                        RB = Convert.ToInt32(row.Cells[3].Value.ToString());
                        DateTime pomDT= Convert.ToDateTime(row.Cells[4].Value.ToString());
                        dateTimePicker1.Value = Convert.ToDateTime(pomDT.ToShortDateString());
                        cboSubjekt.SelectedValue = Convert.ToInt32(row.Cells[11].Value.ToString());
                        cboNosilacTroska.SelectedValue = Convert.ToInt32(row.Cells[12].Value.ToString());
                        cboOdeljenje.SelectedValue = Convert.ToInt32(row.Cells[13].Value.ToString());
                        txtIznos.Value = Convert.ToDecimal(row.Cells[8].Value.ToString());
                        cboValuta.SelectedValue = row.Cells[9].Value.ToString();
                        Najava = Convert.ToInt32(row.Cells[14].Value.ToString());
                        txtNapomena.Text = row.Cells[15].Value.ToString().TrimEnd();
                        cboJM.SelectedValue = row.Cells[17].Value.ToString();
                        txtKolicina.Value = Convert.ToDecimal(row.Cells[18].Value);
                        cboIdent.SelectedValue = Convert.ToInt32(row.Cells[19].Value);
                        txtKurs.Value = Convert.ToDecimal(row.Cells[21].Value);
                    }
                }
            }
            catch { }
        }
        
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    txtIDPredvidjanja.Text = row.Cells[1].Value.ToString();
                    txtPredvidjanje.Text = row.Cells[2].Value.ToString();
                    RB = Convert.ToInt32(row.Cells[3].Value.ToString());
                    DateTime pomDT = Convert.ToDateTime(row.Cells[4].Value.ToString());
                    dateTimePicker1.Value = Convert.ToDateTime(pomDT.ToShortDateString());
                    cboSubjekt.SelectedValue = Convert.ToInt32(row.Cells[11].Value.ToString());
                    cboNosilacTroska.SelectedValue = Convert.ToInt32(row.Cells[12].Value.ToString());
                    cboOdeljenje.SelectedValue = Convert.ToInt32(row.Cells[13].Value.ToString());
                    txtIznos.Value = Convert.ToDecimal(row.Cells[8].Value.ToString());
                    cboValuta.SelectedValue = row.Cells[9].Value.ToString();
                    Najava = Convert.ToInt32(row.Cells[14].Value.ToString());
                    StatusSelektovanog = Convert.ToInt32(row.Cells[15].Value.ToString());
                    txtNapomena.Text = row.Cells[16].Value.ToString().TrimEnd();
                    cboJM.SelectedValue = row.Cells[17].Value.ToString();
                    txtKolicina.Value = Convert.ToDecimal(row.Cells[18].Value);
                    cboIdent.SelectedValue = Convert.ToInt32(row.Cells[19].Value);
                }
            }
        }
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
                { IDp = 1; }
                else
                {
                    IDp = Convert.ToInt32(dr[0].ToString());
                }
                txtIDPredvidjanja.Text = IDp.ToString();
                string g = DateTime.Now.ToString("yy");
                txtPredvidjanje.Text = "SUPP-" + IDp.ToString() + "-" + g;
            }
            conn.Close();

            RB = 1;
        }
        int sifDr;
        decimal iznosRSD;
        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            SqlConnection conn = new SqlConnection(connect);
            string query2 = "Select PaSifDrzave from Partnerji Where PaSifra=" + Convert.ToInt32(cboSubjekt.SelectedValue);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                sifDr = Convert.ToInt32(dr2[0].ToString());
            }
            conn.Close();
            string valuta = cboValuta.SelectedValue.ToString();
            if (sifDr == 82 && valuta != "RSD")
            {
                DialogResult result = MessageBox.Show("Za domaćeg dobavljača dokument treba biti u dinarima\nDa li želite da potvrdite dokument u valuti " + valuta +" po kursu: "+txtKurs.Value.ToString().TrimEnd(), "Potvrda valute", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (status == true)
                    {
                        var query = "Select Posao From NosiociTroskova Where ID=" + Convert.ToInt32(cboNosilacTroska.SelectedValue);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Najava = Convert.ToInt32(dr[0].ToString());
                        }
                        iznosRSD = (Convert.ToDecimal(txtIznos.Value) * Convert.ToDecimal(txtKurs.Value));
                        ins.InsPredvidjanje(IDp, txtPredvidjanje.Text.ToString().TrimEnd(), RB, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 0, Najava, Convert.ToInt32(cboIdent.SelectedValue), Convert.ToDecimal(txtKolicina.Value), cboJM.SelectedValue.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(),Convert.ToDecimal(txtKurs.Value),iznosRSD,korisnik);
                    }
                    else
                    {
                        if (StatusSelektovanog == 0)
                        {
                            var query = "Select Posao From NosiociTroskova Where ID=" + Convert.ToInt32(cboNosilacTroska.SelectedValue);
                            conn.Open();
                            SqlCommand cmd = new SqlCommand(query, conn);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                Najava = Convert.ToInt32(dr[0].ToString());
                            }

                            iznosRSD = (Convert.ToDecimal(txtIznos.Value) * Convert.ToDecimal(txtKurs.Value));
                            ins.UpdPredvidjanje(Convert.ToInt32(txtID.Text), txtPredvidjanje.Text.ToString().TrimEnd(), RB, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue),
                                Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Value), cboValuta.SelectedValue.ToString(), Najava, Convert.ToInt32(txtIDPredvidjanja.Text), Convert.ToInt32(cboIdent.SelectedValue), Convert.ToDecimal(txtKolicina.Value), cboJM.SelectedValue.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(),Convert.ToDecimal(txtKurs.Value),iznosRSD,korisnik);
                        }
                        else
                        {
                            MessageBox.Show("Nije moguće izmeniti zapis koji je poslat sinhronizacijom!");
                        }
                    }
                    FillGV();
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                if (status == true)
                {
                    var query = "Select Posao From NosiociTroskova Where ID=" + Convert.ToInt32(cboNosilacTroska.SelectedValue);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Najava = Convert.ToInt32(dr[0].ToString());
                    }
                    ins.InsPredvidjanje(IDp, txtPredvidjanje.Text.ToString().TrimEnd(), RB, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 0, Najava, Convert.ToInt32(cboIdent.SelectedValue), Convert.ToDecimal(txtKolicina.Value), cboJM.SelectedValue.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(),Convert.ToDecimal(txtKurs.Value),Convert.ToDecimal(txtIznos.Value),korisnik);
                }
                else
                {
                    if (StatusSelektovanog == 0)
                    {
                        var query = "Select Posao From NosiociTroskova Where ID=" + Convert.ToInt32(cboNosilacTroska.SelectedValue);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Najava = Convert.ToInt32(dr[0].ToString());
                        }

                        ins.UpdPredvidjanje(Convert.ToInt32(txtID.Text), txtPredvidjanje.Text.ToString().TrimEnd(), RB, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue),
                            Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Value), cboValuta.SelectedValue.ToString(), Najava, Convert.ToInt32(txtIDPredvidjanja.Text), Convert.ToInt32(cboIdent.SelectedValue), Convert.ToDecimal(txtKolicina.Value), cboJM.SelectedValue.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(),Convert.ToInt32(txtKurs.Value),Convert.ToDecimal(txtIznos.Value),korisnik);
                    }
                    else
                    {
                        MessageBox.Show("Nije moguće izmeniti zapis koji je poslat sinhronizacijom!");
                    }
                }
                FillGV();
            }
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
            InsertPatheonExport ins = new InsertPatheonExport();

            int ID;
            string PredvidjanjeID, Poz, Kupac, NTNaziv, Odeljenje, Iznos, Valuta, Datum;
            DateTime datumPom;
            string json;
            try
            {
                if(dataGridView1.Rows.Count > 0 ) 
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected)
                        {
                            ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                            PredvidjanjeID = row.Cells[2].Value.ToString().TrimEnd();
                            Poz = row.Cells[3].Value.ToString().TrimEnd();
                            datumPom = Convert.ToDateTime(row.Cells[4].Value.ToString());
                            Datum = datumPom.ToString("yyyy-MM-dd");
                            Kupac = row.Cells[5].Value.ToString().TrimEnd();
                            NTNaziv = row.Cells[10].Value.ToString().TrimEnd();
                            Odeljenje = row.Cells[7].Value.ToString().TrimEnd();
                            if (Convert.ToInt32(row.Cells[20].Value) > Convert.ToInt32(row.Cells[8].Value))
                            {
                                Valuta = "RSD";
                            }
                            else
                            {
                                Valuta = row.Cells[9].Value.ToString();
                            }
                            Iznos = row.Cells[20].Value.ToString();
                            

                            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:6333/api/Predvidjanje/PredvidjanjePost");
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Method = "POST";
                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                 json = "{" +
                                               "\n\"PredvidjanjeID\":\"" + PredvidjanjeID + "\"," +
                                               "\n\"PredvidjanjePoz\":\"" + Poz + "\"," +
                                               "\n\"Datum\":\"" + Datum + "\"," +
                                               "\n\"Subject\":\"" + Kupac + "\"," +
                                              "\n\"Strn\":\"" + NTNaziv + "\"," +
                                              "\n\"Odeljenje\":\"" + Odeljenje + "\"," +
                                              "\n\"Iznos\":\"" + Iznos + "\"," +
                                               "\n\"Valuta\":\"" + Valuta + "\"\n}";
                                streamWriter.Write(json);

                            }
                            string response = "";
                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                                response = result.ToString();
                                if (response.Contains("Error") == true || response.Contains("Greška") == true || response.Contains("ERROR") == true || response.Contains("Duplikat") == true)
                                {
                                    MessageBox.Show("Slanje nije uspelo \n" + response.ToString());
                                    ins.InsApiLog("Predvidjanje-" + ID.ToString(), json, response);
                                    return;
                                }
                            }
                            ins.InsApiLog("Predvidjanje-" + ID.ToString()+"/"+Poz.ToString()+"/DEMO", json, response);

                        }
                    }

                }
            }
            catch { }
            FillGV();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            int ID;
            string PredvidjanjeID, Poz, Kupac, NTNaziv, Odeljenje, Iznos, Valuta, Datum;
            DateTime datumPom;
            string json;
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Selected)
                        {
                            ID = Convert.ToInt32(row.Cells[0].Value.ToString());
                            PredvidjanjeID = row.Cells[2].Value.ToString().TrimEnd();
                            Poz = row.Cells[3].Value.ToString().TrimEnd();
                            datumPom = Convert.ToDateTime(row.Cells[4].Value.ToString());
                            Datum = datumPom.ToString("yyyy-MM-dd");
                            Kupac = row.Cells[5].Value.ToString().TrimEnd();
                            NTNaziv = row.Cells[10].Value.ToString().TrimEnd();
                            Odeljenje = row.Cells[7].Value.ToString().TrimEnd();
                            if (Convert.ToInt32(row.Cells[20].Value) > Convert.ToInt32(row.Cells[8].Value))
                            {
                                Valuta = "RSD";
                            }
                            else
                            {
                                Valuta = row.Cells[9].Value.ToString();
                            }
                            Iznos = row.Cells[20].Value.ToString();

                            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.129.2:3333/api/Predvidjanje/PredvidjanjePost");
                            httpWebRequest.ContentType = "application/json";
                            httpWebRequest.Method = "POST";
                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                json = "{" +
                                              "\n\"PredvidjanjeID\":\"" + PredvidjanjeID + "\"," +
                                              "\n\"PredvidjanjePoz\":\"" + Poz + "\"," +
                                              "\n\"Datum\":\"" + Datum + "\"," +
                                              "\n\"Subject\":\"" + Kupac + "\"," +
                                             "\n\"Strn\":\"" + NTNaziv + "\"," +
                                             "\n\"Odeljenje\":\"" + Odeljenje + "\"," +
                                             "\n\"Iznos\":\"" + Iznos + "\"," +
                                              "\n\"Valuta\":\"" + Valuta + "\"\n}";
                                streamWriter.Write(json);

                            }
                            string response = "";
                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                                response = result.ToString();
                                if (response.Contains("Error") == true || response.Contains("Greška") == true || response.Contains("ERROR") == true || response.Contains("Duplikat") == true)
                                {
                                    MessageBox.Show("Slanje nije uspelo \n" + response.ToString());
                                    ins.InsApiLog("Predvidjanje-" + ID.ToString()+"/"+Poz.ToString(), json, response);
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
                                }
                            }
                            ins.InsApiLog("Predvidjanje-"+ID.ToString()+"/"+Poz.ToString(), json, response);
                        }
                    }

                }
            }
            catch { }
            FillGV();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    ins.VratiPredvidjenjeStatus(Convert.ToInt32(row.Cells[0].Value.ToString()));
                }
            }
            StatusSelektovanog = 0;
            FillGV();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void txtIznos_ValueChanged(object sender, EventArgs e)
        {
        }
        private void Izracunaj()
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Izracunaj();
        }

        private void txtKolicina_ValueChanged(object sender, EventArgs e)
        {
        }

        private void txtKolicina_Leave(object sender, EventArgs e)
        {
            numericUpDown1.Value=Convert.ToDecimal(txtIznos.Value)/(txtKolicina.Value);
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            txtIznos.Value = Convert.ToDecimal(txtKolicina.Value) * Convert.ToDecimal(numericUpDown1.Value);
        }

        private void txtIznos_Leave(object sender, EventArgs e)
        {
            numericUpDown1.Value = Convert.ToDecimal(txtIznos.Value) / Convert.ToDecimal(txtKolicina.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
        int poz;
        private void GetNosiociInfo()
        {
            var select = "Select Posao from NosiociTroskova Where ID=" + Convert.ToInt32(cboNosilacTroska.SelectedValue);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(select, conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                Najava = Convert.ToInt32(dr2[0].ToString());
            }
            conn.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var query = "Select (Max(PredvodjanjePoz)) as Poz From Predvidjanje Where IDP=" + Convert.ToInt32(txtIDPredvidjanja.Text);
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                poz= Convert.ToInt32(dr[0])+1;
            }
            conn.Close();

            GetNosiociInfo();
            iznosRSD = (Convert.ToDecimal(txtIznos.Value) * Convert.ToDecimal(txtKurs.Value));
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.InsPredvidjanje(Convert.ToInt32(txtIDPredvidjanja.Text),txtPredvidjanje.Text.ToString().TrimEnd(), poz, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 0,Najava,Convert.ToInt32(cboIdent.SelectedValue),Convert.ToDecimal(txtKolicina.Value),cboJM.SelectedValue.ToString().TrimEnd(),txtNapomena.Text.ToString().TrimEnd(),Convert.ToInt32(txtKurs.Value),iznosRSD,korisnik);
            FillGV();
        }
    }
}
