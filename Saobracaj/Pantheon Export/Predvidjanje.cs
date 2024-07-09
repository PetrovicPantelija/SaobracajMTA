using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
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
            var select = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT," +
                "RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv],RTrim(PredvidjanjeID) as Predvidjanje,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
                "p.PredvodjanjePoz as [Poz.],p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
                "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
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

            dataGridView1.Columns["ID"].Width = 55;
            dataGridView1.Columns["IDp"].Width = 60;
            dataGridView1.Columns["NTID"].Visible = false;
            dataGridView1.Columns["NT"].Width = 70;
            dataGridView1.Columns["NT Naziv"].Width = 90;
            dataGridView1.Columns["Ident"].Width = 120;
            dataGridView1.Columns["Poz."].Width = 55;
            dataGridView1.Columns["Napomena"].Width = 120;
            dataGridView1.Columns["Subjekt"].Width = 150;
            dataGridView1.Columns["Odeljenje"].Visible = false;
            dataGridView1.Columns["Iznos"].Width = 80;
            dataGridView1.Columns["Valuta"].Width = 50;
            dataGridView1.Columns["Kolicina"].Width = 60;
            dataGridView1.Columns["JM"].Width = 55;
            dataGridView1.Columns["Jedinicna Cena"].Width = 70;
            dataGridView1.Columns["Kurs"].Width = 65;
            dataGridView1.Columns["Subjekt1"].Visible = false;
            dataGridView1.Columns["Odeljenje1"].Visible = false;
            dataGridView1.Columns["Ident1"].Visible = false;
            dataGridView1.Columns["Korisnik"].Width = 70;
            dataGridView1.Columns["IznosRSD"].Visible = false;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells["ID"].Value.ToString();
                        txtIDPredvidjanja.Text = row.Cells["IDp"].Value.ToString();
                        txtPredvidjanje.Text = row.Cells["Predvidjanje"].Value.ToString();
                        RB = Convert.ToInt32(row.Cells["Poz."].Value.ToString());
                        DateTime pomDT = Convert.ToDateTime(row.Cells["Datum"].Value.ToString());
                        dateTimePicker1.Value = Convert.ToDateTime(pomDT.ToShortDateString());
                        cboSubjekt.SelectedValue = Convert.ToInt32(row.Cells["Subjekt1"].Value.ToString());
                        cboNosilacTroska.SelectedValue = Convert.ToInt32(row.Cells["NTID"].Value.ToString());
                        cboOdeljenje.SelectedValue = Convert.ToInt32(row.Cells["Odeljenje1"].Value.ToString());
                        txtIznos.Value = Convert.ToDecimal(row.Cells["Iznos"].Value.ToString());
                        cboValuta.SelectedValue = row.Cells["Valuta"].Value.ToString();
                        Najava = Convert.ToInt32(row.Cells["NajavaID"].Value.ToString());
                        txtNapomena.Text = row.Cells["Napomena"].Value.ToString().TrimEnd();
                        cboJM.SelectedValue = row.Cells["JM"].Value.ToString();
                        txtKolicina.Value = Convert.ToDecimal(row.Cells["Kolicina"].Value);
                        cboIdent.SelectedValue = Convert.ToInt32(row.Cells["Ident1"].Value);
                        txtKurs.Value = Convert.ToDecimal(row.Cells["Kurs"].Value);
                        numericUpDown1.Value = Convert.ToDecimal(row.Cells["Iznos"].Value) / Convert.ToDecimal(row.Cells["Kolicina"].Value);
                    }
                }
            }
            catch { }
        }
        private void Filteri(string query)
        {
            var select = query;

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

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

            dataGridView2.Columns["ID"].Width = 55;
            dataGridView2.Columns["IDp"].Width = 55;
            dataGridView2.Columns["NTID"].Visible = false;
            dataGridView2.Columns["NT"].Width = 70;
            dataGridView2.Columns["NT Naziv"].Width = 90;
            dataGridView2.Columns["Status"].Width = 55;
            dataGridView2.Columns["Predvidjanje"].Width = 80;
            dataGridView2.Columns["Ident"].Width = 100;
            dataGridView2.Columns["Poz."].Width = 50;
            dataGridView2.Columns["Napomena"].Width = 100;
            dataGridView2.Columns["Subjekt"].Width = 150;
            dataGridView2.Columns["Odeljenje"].Visible = false;
            dataGridView2.Columns["Iznos"].Width = 70;
            dataGridView2.Columns["Valuta"].Width = 50;
            dataGridView2.Columns["Kolicina"].Width = 70;
            dataGridView2.Columns["JM"].Width = 55;
            dataGridView2.Columns["Jedinicna cena"].Width = 70;
            dataGridView2.Columns["Kurs"].Width = 60;
            dataGridView2.Columns["Subjekt1"].Visible = false;
            dataGridView2.Columns["Ident1"].Visible = false;
            dataGridView2.Columns["IznosRSD"].Visible = false;
            dataGridView2.Columns["Odeljenje1"].Visible = false;
        }
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells["ID"].Value.ToString();
                    txtIDPredvidjanja.Text = row.Cells["IDp"].Value.ToString();
                    txtPredvidjanje.Text = row.Cells["Predvidjanje"].Value.ToString();
                    RB = Convert.ToInt32(row.Cells["Poz."].Value.ToString());
                    DateTime pomDT = Convert.ToDateTime(row.Cells["Datum"].Value.ToString());
                    dateTimePicker1.Value = Convert.ToDateTime(pomDT.ToShortDateString());
                    cboSubjekt.SelectedValue = Convert.ToInt32(row.Cells["Subjekt1"].Value.ToString());
                    cboNosilacTroska.SelectedValue = Convert.ToInt32(row.Cells["NTID"].Value.ToString());
                    cboOdeljenje.SelectedValue = Convert.ToInt32(row.Cells["Odeljenje1"].Value.ToString());
                    txtIznos.Value = Convert.ToDecimal(row.Cells["Iznos"].Value.ToString());
                    cboValuta.SelectedValue = row.Cells["Valuta"].Value.ToString();
                    Najava = Convert.ToInt32(row.Cells["NajavaID"].Value.ToString());
                    StatusSelektovanog = Convert.ToInt32(row.Cells["Status"].Value.ToString());
                    txtNapomena.Text = row.Cells["Napomena"].Value.ToString().TrimEnd();
                    cboJM.SelectedValue = row.Cells["JM"].Value.ToString();
                    txtKolicina.Value = Convert.ToDecimal(row.Cells["Kolicina"].Value);
                    cboIdent.SelectedValue = Convert.ToInt32(row.Cells["Ident1"].Value);
                    numericUpDown1.Value = Convert.ToDecimal(row.Cells["Iznos"].Value) / Convert.ToDecimal(row.Cells["Kolicina"].Value);

                    //ovde upisati da kada se unese kolicina racuna odmah iznos umesto jedinicne
                }
            }
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

            var pID = "select Distinct RTrim(PRedvidjanjeID) as PredvidjanjeID,IDP From Predvidjanje order by IDp desc";
            var pDa = new SqlDataAdapter(pID, conn);
            var pDS = new DataSet();
            pDa.Fill(pDS);
            cboPredvidjanjeIDFilter.DataSource = pDS.Tables[0];
            cboPredvidjanjeIDFilter.DisplayMember = "PredvidjanjeID";
            cboPredvidjanjeIDFilter.ValueMember = "PredvidjanjeID";

            var filterNTtxt = "Select ID,RTrim(NosilacTroska) as NosilacTroska from NosiociTroskova order by ID desc";
            var DafNT = new SqlDataAdapter(filterNTtxt, conn);
            var DsfNt = new DataSet();
            DafNT.Fill(DsfNt);
            cboFilterNT.DataSource = DsfNt.Tables[0];
            cboFilterNT.DisplayMember = "NosilacTroska";
            cboFilterNT.ValueMember = "ID";

            var filterNTTxt = "Select ID,RTrim(NazivNosiocaTroska) as NazivNosiocaTroska from NosiociTroskova order by ID desc";
            var DafNTtxt = new SqlDataAdapter(filterNTTxt, conn);
            var DsfNttxt = new DataSet();
            DafNTtxt.Fill(DsfNttxt);
            cboFilterNazivNT.DataSource = DsfNttxt.Tables[0];
            cboFilterNazivNT.DisplayMember = "NazivNosiocaTroska";
            cboFilterNazivNT.ValueMember = "ID";
        }

        int ID, IDp, RB, Najava, StatusSelektovanog;
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
                DialogResult result = MessageBox.Show("Za domaćeg dobavljača dokument treba biti u dinarima\nDa li želite da potvrdite dokument u valuti " + valuta + " po kursu: " + txtKurs.Value.ToString().TrimEnd(), "Potvrda valute", MessageBoxButtons.YesNo);
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
                        ins.InsPredvidjanje(IDp, txtPredvidjanje.Text.ToString().TrimEnd(), RB, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 0, Najava, Convert.ToInt32(cboIdent.SelectedValue), Convert.ToDecimal(txtKolicina.Value), cboJM.SelectedValue.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(), Convert.ToDecimal(txtKurs.Value), iznosRSD, korisnik);
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
                                Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Value), cboValuta.SelectedValue.ToString(), Najava, Convert.ToInt32(txtIDPredvidjanja.Text), Convert.ToInt32(cboIdent.SelectedValue), Convert.ToDecimal(txtKolicina.Value), cboJM.SelectedValue.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(), Convert.ToDecimal(txtKurs.Value), iznosRSD, korisnik);
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
                    ins.InsPredvidjanje(IDp, txtPredvidjanje.Text.ToString().TrimEnd(), RB, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 0, Najava, Convert.ToInt32(cboIdent.SelectedValue), Convert.ToDecimal(txtKolicina.Value), cboJM.SelectedValue.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(), Convert.ToDecimal(txtKurs.Value), Convert.ToDecimal(txtIznos.Value), korisnik);
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
                            Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Value), cboValuta.SelectedValue.ToString(), Najava, Convert.ToInt32(txtIDPredvidjanja.Text), Convert.ToInt32(cboIdent.SelectedValue), Convert.ToDecimal(txtKolicina.Value), cboJM.SelectedValue.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(txtKurs.Value), Convert.ToDecimal(txtIznos.Value), korisnik);
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
            if (korisnik.TrimEnd() == "mikic.d")
            {
                InsertPatheonExport ins = new InsertPatheonExport();
                ins.DelPredvidjanje(Convert.ToInt32(txtID.Text));
                FillGV();
            }
            else
            {
                MessageBox.Show("Nemate pravo brisanja zapisa!");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            string query = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT," +
                 "RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv],p.Status as Status,RTrim(PredvidjanjeID) as Predvidjanje,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
                 "p.PredvodjanjePoz as [Poz.],p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
                 "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
                 "From Predvidjanje p " +
                 "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
                 "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
                 "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
                 "inner join Odeljenja on p.Odeljenje = Odeljenja.ID Where Kolicina>0 order by p.ID desc";

            Filteri(query);
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
                            ID = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                            PredvidjanjeID = row.Cells["Predvidjanje"].Value.ToString().TrimEnd();
                            Poz = row.Cells["Poz."].Value.ToString().TrimEnd();
                            datumPom = Convert.ToDateTime(row.Cells["Datum"].Value.ToString());
                            Datum = datumPom.ToString("yyyy-MM-dd");
                            Kupac = row.Cells["Subjekt"].Value.ToString().TrimEnd();
                            NTNaziv = row.Cells["NT"].Value.ToString().TrimEnd();
                            Odeljenje = row.Cells["Odeljenje"].Value.ToString().TrimEnd();
                            if (Convert.ToInt32(row.Cells["IznosRSD"].Value) > Convert.ToInt32(row.Cells["Iznos"].Value))
                            {
                                Valuta = "RSD";
                            }
                            else
                            {
                                Valuta = row.Cells["Valuta"].Value.ToString();
                            }
                            Iznos = row.Cells["IznosRSD"].Value.ToString();

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
                                    ins.InsApiLog("Predvidjanje-" + ID.ToString() + "/" + Poz.ToString(), json, response);
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
                                    ins.InsApiLog("Predvidjanje-" + ID.ToString() + "/" + Poz.ToString(), json, response);
                                }
                            }
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
                    ins.VratiPredvidjenjeStatus(Convert.ToInt32(row.Cells["ID"].Value.ToString()));
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
            numericUpDown1.Value = Convert.ToDecimal(txtIznos.Value) / (txtKolicina.Value);
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            txtIznos.Value = Convert.ToDecimal(txtKolicina.Value) * Convert.ToDecimal(numericUpDown1.Value);
        }

        private void txtIznos_Leave(object sender, EventArgs e)
        {
            numericUpDown1.Value = Convert.ToDecimal(txtIznos.Value) / Convert.ToDecimal(txtKolicina.Value);
        }

        private void Predvidjanje_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }
        private void btnNTFilter_Click(object sender, EventArgs e)
        {
            int nt = Convert.ToInt32(cboFilterNT.SelectedValue);

            string query = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT," +
            "RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv],p.Status as Status,RTrim(PredvidjanjeID) as Predvidjanje,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
            "p.PredvodjanjePoz as [Poz.],p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
            "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
            "From Predvidjanje p " +
            "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
            "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
            "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
            "inner join Odeljenja on p.Odeljenje = Odeljenja.ID Where Kolicina>0 and NosiociTroskova.ID=" + nt + " order by p.ID desc";

            Filteri(query);
        }

        private void btnNTNazivFilter_Click(object sender, EventArgs e)
        {
            int nt = Convert.ToInt32(cboFilterNazivNT.SelectedValue);

            string query = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT," +
            "RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv],p.Status as Status,RTrim(PredvidjanjeID) as Predvidjanje,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
            "p.PredvodjanjePoz as [Poz.],p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
            "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
            "From Predvidjanje p " +
            "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
            "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
            "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
            "inner join Odeljenja on p.Odeljenje = Odeljenja.ID Where Kolicina>0 and NosiociTroskova.ID=" + nt + " order by p.ID desc";

            Filteri(query);
        }

        private void btnPredvidjanjeFilter_Click(object sender, EventArgs e)
        {
            string nt = cboPredvidjanjeIDFilter.Text.ToString().TrimEnd();

            string query = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT," +
            "RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv],p.Status as Status,RTrim(PredvidjanjeID) as Predvidjanje,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
            "p.PredvodjanjePoz as [Poz.],p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
            "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
            "From Predvidjanje p " +
            "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
            "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
            "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
            "inner join Odeljenja on p.Odeljenje = Odeljenja.ID Where Kolicina>0 and PredvidjanjeID='" + nt + "' order by p.ID desc";

            Filteri(query);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string query = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT," +
            "RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv],p.Status as Status,RTrim(PredvidjanjeID) as Predvidjanje,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
            "p.PredvodjanjePoz as [Poz.],p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
            "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
            "From Predvidjanje p " +
            "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
            "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
            "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
            "inner join Odeljenja on p.Odeljenje = Odeljenja.ID Where Kolicina>0 order by p.ID desc";

            Filteri(query);
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
            string pomVal = "";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand select = new SqlCommand("Select Top 1 Valuta from Predvidjanje Where IDP=" + Convert.ToInt32(txtIDPredvidjanja.Text) + " order by ID desc", conn);
            SqlDataReader reader = select.ExecuteReader();
            while (reader.Read())
            {
                pomVal = reader[0].ToString().TrimEnd();
            }
            conn.Close();

            if (pomVal == cboValuta.SelectedValue.ToString().TrimEnd())
            {
                var query = "Select (Max(PredvodjanjePoz)) as Poz From Predvidjanje Where IDP=" + Convert.ToInt32(txtIDPredvidjanja.Text);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    poz = Convert.ToInt32(dr[0]) + 1;
                }
                conn.Close();

                GetNosiociInfo();
                iznosRSD = (Convert.ToDecimal(txtIznos.Value) * Convert.ToDecimal(txtKurs.Value));


                InsertPatheonExport ins = new InsertPatheonExport();
                ins.InsPredvidjanje(Convert.ToInt32(txtIDPredvidjanja.Text), txtPredvidjanje.Text.ToString().TrimEnd(), poz, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(cboSubjekt.SelectedValue), Convert.ToInt32(cboNosilacTroska.SelectedValue), Convert.ToInt32(cboOdeljenje.SelectedValue), Convert.ToDecimal(txtIznos.Text.ToString()), cboValuta.SelectedValue.ToString(), 0, Najava, Convert.ToInt32(cboIdent.SelectedValue), Convert.ToDecimal(txtKolicina.Value), cboJM.SelectedValue.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd(), Convert.ToInt32(txtKurs.Value), iznosRSD, korisnik);
                FillGV();
            }
            else
            {
                MessageBox.Show("Za ovo predviđanje pozicija mora biti u valuti " + pomVal, "Sve pozicije predvidjanja moraju biti u istoj valuti!");
                return;
            }
        }
        int gv1 = 0;
        int gv2 = 0;
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var select = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT," +
                "RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv],RTrim(PredvidjanjeID) as Predvidjanje,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
                "p.PredvodjanjePoz as [Poz.],p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
                "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
                "From Predvidjanje p " +
                "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
                "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
                "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
                "inner join Odeljenja on p.Odeljenje = Odeljenja.ID Where p.Status = 0 ";

            if (dataGridView1.Columns[e.ColumnIndex].Name == "ID")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + " order by p.ID asc";
                }
                else
                {
                    select = select + " order by p.ID desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "IDp")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + " order by p.IDp asc,PredvodjanjePoz asc";
                }
                else
                {
                    select = select + " order by p.IDp desc,PredvodjanjePoz desc";
                }
                gv1++;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "NT")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + " order by p.NosilacTroska asc,PredvodjanjePoz asc";
                }
                else
                {
                    select = select + " order by p.NosilacTroska desc,PredvodjanjePoz desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Predvidjanje")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + " order by p.IDP asc,PredvodjanjePoz asc";
                }
                else
                {
                    select = select + " order by p.IDP desc,PredvodjanjePoz desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Ident")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + " order by p.Ident asc";
                }
                else
                {
                    select = select + " order by p.Ident desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Datum")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + " order by p.Datum asc";
                }
                else
                {
                    select = select + " order by p.Datum desc";
                }
                gv1++;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Subjekt")
            {

                if (gv1 % 2 == 0)
                {
                    select = select + " order by p.Subjekt asc";
                }
                else
                {
                    select = select + " order by p.Subjekt desc";
                }
                gv1++;
            }
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

            dataGridView1.Columns["ID"].Width = 55;
            dataGridView1.Columns["IDp"].Width = 60;
            dataGridView1.Columns["NTID"].Visible = false;
            dataGridView1.Columns["NT"].Width = 70;
            dataGridView1.Columns["NT Naziv"].Width = 90;
            dataGridView1.Columns["Ident"].Width = 120;
            dataGridView1.Columns["Poz."].Width = 55;
            dataGridView1.Columns["Napomena"].Width = 120;
            dataGridView1.Columns["Subjekt"].Width = 150;
            dataGridView1.Columns["Odeljenje"].Visible = false;
            dataGridView1.Columns["Iznos"].Width = 80;
            dataGridView1.Columns["Valuta"].Width = 50;
            dataGridView1.Columns["Kolicina"].Width = 60;
            dataGridView1.Columns["JM"].Width = 55;
            dataGridView1.Columns["Jedinicna Cena"].Width = 70;
            dataGridView1.Columns["Kurs"].Width = 65;
            dataGridView1.Columns["Subjekt1"].Visible = false;
            dataGridView1.Columns["Odeljenje1"].Visible = false;
            dataGridView1.Columns["Ident1"].Visible = false;
            dataGridView1.Columns["Korisnik"].Width = 70;
            dataGridView1.Columns["IznosRSD"].Visible = false;

        }

        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string select = "select p.ID as ID,p.IDp as IDp,RTrim(p.NosilacTroska) as NTID,RTrim(NosiociTroskova.NosilacTroska) as NT," +
     "RTrim(NosiociTroskova.NazivNosiocaTroska) as [NT Naziv],p.Status as Status,RTrim(PredvidjanjeID) as Predvidjanje,(RTrim(MpStaraSif) + '-' + RTrim(MpNaziv)) as Ident," +
     "p.PredvodjanjePoz as [Poz.],p.Napomena as Napomena,Format(p.Datum,'dd.MM.yyyy') as Datum,RTrim(PaNaziv) as Subjekt,RTrim(SifraSubjekta) as Odeljenje," +
     "Iznos,Valuta,Kolicina,JM,Cast((Iznos/Kolicina) as decimal(18,4))as [Jedinicna Cena],p.Kurs as Kurs,p.Subjekt,p.Odeljenje,p.NajavaID,Ident,RTrim(p.Korisnik) as Korisnik,IznosRSD " +
     "From Predvidjanje p " +
     "inner join Partnerji on p.Subjekt = Partnerji.PaSifra " +
     "inner join NosiociTroskova on p.NosilacTroska = NosiociTroskova.ID " +
     "inner join MaticniPodatki on p.Ident = MaticniPodatki.MpSifra " +
     "inner join Odeljenja on p.Odeljenje = Odeljenja.ID Where Kolicina>0 ";

            if (dataGridView2.Columns[e.ColumnIndex].Name == "ID")
            {

                if (gv2 % 2 == 0)
                {
                    select = select + " order by p.ID asc";
                }
                else
                {
                    select = select + " order by p.ID desc";
                }
                gv2++;
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "IDp")
            {

                if (gv2 % 2 == 0)
                {
                    select = select + " order by p.IDp asc,PredvodjanjePoz asc";
                }
                else
                {
                    select = select + " order by p.IDp desc,PredvodjanjePoz desc";
                }
                gv2++;
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "NT")
            {

                if (gv2 % 2 == 0)
                {
                    select = select + " order by p.NosilacTroska asc,PredvodjanjePoz asc";
                }
                else
                {
                    select = select + " order by p.NosilacTroska desc,PredvodjanjePoz desc";
                }
                gv2++;
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Predvidjanje")
            {

                if (gv2 % 2 == 0)
                {
                    select = select + " order by p.IDP asc,PredvodjanjePoz asc";
                }
                else
                {
                    select = select + " order by p.IDP desc,PredvodjanjePoz desc";
                }
                gv2++;
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Ident")
            {

                if (gv2 % 2 == 0)
                {
                    select = select + " order by p.Ident asc";
                }
                else
                {
                    select = select + " order by p.Ident desc";
                }
                gv2++;
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Datum")
            {

                if (gv2 % 2 == 0)
                {
                    select = select + " order by p.Datum asc";
                }
                else
                {
                    select = select + " order by p.Datum desc";
                }
                gv2++;
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Subjekt")
            {

                if (gv2 % 2 == 0)
                {
                    select = select + " order by p.Subjekt asc";
                }
                else
                {
                    select = select + " order by p.Subjekt desc";
                }
                gv2++;
            }
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

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

            dataGridView2.Columns["ID"].Width = 55;
            dataGridView2.Columns["IDp"].Width = 55;
            dataGridView2.Columns["NTID"].Visible = false;
            dataGridView2.Columns["NT"].Width = 70;
            dataGridView2.Columns["NT Naziv"].Width = 90;
            dataGridView2.Columns["Status"].Width = 55;
            dataGridView2.Columns["Predvidjanje"].Width = 80;
            dataGridView2.Columns["Ident"].Width = 100;
            dataGridView2.Columns["Poz."].Width = 50;
            dataGridView2.Columns["Napomena"].Width = 100;
            dataGridView2.Columns["Subjekt"].Width = 150;
            dataGridView2.Columns["Odeljenje"].Visible = false;
            dataGridView2.Columns["Iznos"].Width = 70;
            dataGridView2.Columns["Valuta"].Width = 50;
            dataGridView2.Columns["Kolicina"].Width = 70;
            dataGridView2.Columns["JM"].Width = 55;
            dataGridView2.Columns["Jedinicna cena"].Width = 70;
            dataGridView2.Columns["Kurs"].Width = 60;
            dataGridView2.Columns["Subjekt1"].Visible = false;
            dataGridView2.Columns["Ident1"].Visible = false;
            dataGridView2.Columns["IznosRSD"].Visible = false;
            dataGridView2.Columns["Odeljenje1"].Visible = false;
        }
    }
}
