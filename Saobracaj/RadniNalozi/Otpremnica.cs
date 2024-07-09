using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    public partial class Otpremnica : Syncfusion.Windows.Forms.Office2010Form
    {
        string connect = frmLogovanje.connectionString;
        string korisnik;
        bool dgvComboEnabled = true;
        public Otpremnica()
        {
            InitializeComponent();
        }
        int nalog;
        string kontejner,korisink;
        public Otpremnica(int NalogID, string Kontejner,string Registracija,string Vozac)
        {
            InitializeComponent();
            nalog = NalogID;
            kontejner = Kontejner;
            txtBrojKontejnera.Text = kontejner;
            txtVozilo.Text = Registracija;
            txtVozac.Text = Vozac;
            btn_Povuci.Visible = false;
            cbo_MestoTroska.Visible = false;
        }

        private void Otpremnica_Load(object sender, EventArgs e)
        {
            korisnik = frmLogovanje.user;
            FillCombo();
            panel1.Visible = false;
            if (frmLogovanje.Firma == "Leget")
            {
                var query = "select top 1 SkladisteU,LokacijaU From Promet Where VrstaDokumenta='PRI' and PrSifVrstePrometa='PRV' and BrojKontejnera='" + kontejner + "'";
                SqlConnection conn = new SqlConnection(connect);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo_Skladiste.SelectedValue = Convert.ToInt32(dr[0].ToString());
                    cbo_Lokacija.SelectedValue = Convert.ToInt32(dr[1].ToString());
                }
                conn.Close();
                korisink = frmLogovanje.user;

                dgvComboEnabled = false;

                PovuciIzPrijemnice();
                var selectSvi = "Select BrojKontejnera,DatumTransakcije,VrstaDokumenta,PrStDokumenta,PrOznSled,PrPrimKol,PrIzdKol,SkladisteU,LokacijaU,SkladisteIz,LokacijaIz,MpSifra," +
                   "RTrim(Naziv) as MpNaziv,JedinicaMere,Lot,Skladisteno " +
                   "From Promet " +
                   "Inner Join NHM on Promet.MpSifra=NHM.ID " +
                   "Where PrIzdKol<PrPrimKol and BrojKontejnera<>'" + kontejner + "'";
                var da2 = new SqlDataAdapter(selectSvi, conn);
                var ds2 = new System.Data.DataSet();
                da2.Fill(ds2);
                dataGridView4.ReadOnly = true;
                dataGridView4.DataSource = ds2.Tables[0];

                dataGridView4.Columns["PrIzdKol"].DefaultCellStyle.BackColor = Color.LightGreen;
            }
            DGVCombo();

        }
        DataTable dt=new DataTable();
        private void PovuciIzPrijemnice()
        {
            var select = "Select BrojKontejnera,DatumTransakcije,VrstaDokumenta,PrStDokumenta,PrOznSled,PrPrimKol,PrIzdKol,SkladisteU,LokacijaU,SkladisteIz,LokacijaIz,MpSifra," +
                "RTrim(Naziv) as MpNaziv,JedinicaMere,Lot,Skladisteno " +
                "From Promet " +
                "Inner Join NHM on Promet.MpSifra=NHM.ID " +
                "Where PrIzdKol<PrPrimKol and BrojKontejnera='" + kontejner + "'";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd=new SqlCommand(select, conn);
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["PrIzdKol"].DefaultCellStyle.BackColor = Color.LightGreen;
        }
        private void RefreshGV()
        {
            dataGridView1.DataSource = dt;
        }
        private void FillCombo()
        {
            string query, display, value,query7,display7,value7;
            if (frmLogovanje.Firma == "Leget")
            {
                query = "select ID,Naziv from Skladista";
                display = "Naziv";
                value = "ID";
                query7 = "Select ID,Oznaka from Pozicija";
                display7 = "Oznaka";
                value7 = "ID";
            }
            else
            {
                query = "select skSifra,SkNaziv from Sklad";
                display = "SkNaziv";
                value = "SkSifra";
                query7 = "select LokSifra,LokOpis from Lokac";
                display7 = "LokOpis";
                value7 = "LokSifra";
            }
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbo_Skladiste.DataSource = ds.Tables[0];
            cbo_Skladiste.DisplayMember = display;
            cbo_Skladiste.ValueMember = value;

            var query3 = "Select PaSifra,PaNaziv from Partnerji order by PaNaziv";
            SqlDataAdapter da3 = new SqlDataAdapter(query3, conn);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3);
            cbo_Partner.DataSource = ds3.Tables[0];
            cbo_Partner.DisplayMember = "PaNaziv";
            cbo_Partner.ValueMember = "PaSifra";

            var query4 = "Select DeSifra as ID,(RTrim(DePriimek)+' '+RTrim(DeIme)) as Opis From Delavci Where DeSifStat<>'P' order by Opis";
            SqlDataAdapter da4 = new SqlDataAdapter(query4, conn);
            DataSet ds4 = new DataSet();
            da4.Fill(ds4);
            comboBox1.DataSource = ds4.Tables[0];
            comboBox1.DisplayMember = "Opis";
            comboBox1.ValueMember = "ID";

            var query5 = "Select DeSifra as ID,(RTrim(DePriimek)+' '+RTrim(DeIme)) as Opis From Delavci Where DeSifStat <>'P' order by Opis";
            SqlDataAdapter da5 = new SqlDataAdapter(query5, conn);
            DataSet ds5 = new DataSet();
            da5.Fill(ds5);
            cboVozac.DataSource = ds5.Tables[0];
            cboVozac.DisplayMember = "Opis";
            cboVozac.ValueMember = "Opis";

            var query6 = "Select Naziv From Vozila";
            SqlDataAdapter da6 = new SqlDataAdapter(query6, conn);
            DataSet ds6 = new DataSet();
            da6.Fill(ds6);
            cboVozilo.DataSource = ds6.Tables[0];
            cboVozilo.DisplayMember = "Naziv";
            cboVozilo.ValueMember = "Naziv";


            SqlDataAdapter da7 = new SqlDataAdapter(query7, conn);
            DataSet ds7 = new DataSet();
            da7.Fill(ds7);
            cbo_Lokacija.DataSource = ds7.Tables[0];
            cbo_Lokacija.DisplayMember = display7;
            cbo_Lokacija.ValueMember = value7;

            var query2 = "Select SmNaziv,SmSifra From Mesta";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, conn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            cbo_MestoTroska.DataSource = ds2.Tables[0];
            cbo_MestoTroska.DisplayMember = "SmNaziv";
            cbo_MestoTroska.ValueMember = "SmSIfra";
        }
        private void cbo_Skladiste_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DGVCombo()
        {
            if (dgvComboEnabled == true)
            {
                DataGridViewComboBoxColumn cbo = new DataGridViewComboBoxColumn();
                DataGridViewTextBoxColumn cbo2 = new DataGridViewTextBoxColumn();

                cbo.HeaderText = "MPNaziv";
                cbo.Name = "MPNaziv";
                var query = "Select MpSifra,MpNaziv from MaticniPodatki order by MpNaziv";
                SqlConnection conn = new SqlConnection(connect);
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cbo.DataSource = ds.Tables[0];
                cbo.DisplayMember = "MpNaziv";
                cbo.ValueMember = "MpSifra";


                cbo2.HeaderText = "Kolicina";
                cbo2.Name = "Kolicina";

                cbo.Width = 450;
                cbo2.Width = 120;


                dataGridView1.Columns.Add(cbo);
                dataGridView1.Columns.Add(cbo2);
            }
        }
        int prStDokumenta;
        int skladisteno;
        private void button1_Click(object sender, EventArgs e)
        {
            if (frmLogovanje.Firma == "Leget")
            {
                var query = "Select (Max(PrStDokumenta)+1) From Promet";
                SqlConnection conn = new SqlConnection(connect);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    prStDokumenta = Convert.ToInt32(dr[0].ToString());
                }
                conn.Close();
                InsertIsporuka ins = new InsertIsporuka();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row != null && row.Cells[0].Value != null && Convert.ToDecimal(row.Cells["PrIzdKol"].Value)!=0)
                    {
                        if (row.Cells["Skladisteno"].Value == null || Convert.ToInt32(row.Cells["Skladisteno"].Value)==0)
                        {
                            skladisteno = 0;
                        }
                        else { skladisteno = 1; }
                        ins.InsertPromet(Convert.ToDateTime(dtpVreme.Value), "OTP", prStDokumenta, txtBrojKontejnera.Text.ToString().TrimEnd(), "OTP", 0, Convert.ToDecimal(row.Cells["PrIzdKol"].Value), 0, 0, Convert.ToInt32(cbo_Skladiste.SelectedValue),
                            Convert.ToInt32(cbo_Lokacija.SelectedValue),  Convert.ToDateTime(DateTime.Now), korisnik, 0, Convert.ToInt32(comboBox1.SelectedValue), Convert.ToDateTime(dtpVreme.Value.ToString()), row.Cells["JedinicaMere"].Value.ToString(),
                            row.Cells["Lot"].Value.ToString(), nalog, Convert.ToInt32(row.Cells["MpSifra"].Value), skladisteno);
                        //isporuka.InsertPrijemnicaPostav(Convert.ToInt32(row.Cells[0].Value), Convert.ToDecimal(row.Cells[1].Value), Convert.ToInt32(cbo_Skladiste.SelectedValue), cbo_Lokacija.SelectedValue.ToString(), cbo_MestoTroska.SelectedValue.ToString());
                        // progressBar1.Value = progressBar1.Value + 1;
                    }
                }
            }
            else
            {
                InsertIsporuka isporuka = new InsertIsporuka();
                if (checkBox1.Checked == true)
                {
                    isporuka.InsertDobavnica(Convert.ToInt32(cbo_Partner.SelectedValue), cbo_MestoTroska.SelectedValue.ToString(), Convert.ToInt32(comboBox1.SelectedValue), txtVozac.Text.ToString().TrimEnd(), txtVozilo.Text.ToString().TrimEnd(), Convert.ToDateTime(dtpVreme.Value), txtBrojKontejnera.Text.ToString().TrimEnd());
                }
                else
                {
                    isporuka.InsertDobavnica(Convert.ToInt32(cbo_Partner.SelectedValue), cbo_MestoTroska.SelectedValue.ToString(), Convert.ToInt32(comboBox1.SelectedValue), cboVozac.SelectedValue.ToString(), cboVozilo.SelectedValue.ToString(), Convert.ToDateTime(dtpVreme.Value), txtBrojKontejnera.ToString().TrimEnd());
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //Status proverava da li je iz porudzbenice ili se pravi nova od nule. Ako je status 1 vuce podatke iz narudzbenice
                    if (status == 0)
                    {
                        if (row != null && row.Cells[0].Value != null)
                        {
                            isporuka.InsertDobavnicaPostav(Convert.ToInt32(row.Cells[0].Value), Convert.ToDecimal(row.Cells[1].Value), Convert.ToInt32(cbo_Skladiste.SelectedValue), cbo_Lokacija.SelectedValue.ToString(), cbo_MestoTroska.SelectedValue.ToString());
                            // progressBar1.Value = progressBar1.Value + 1;
                        }
                    }
                    if (status == 1)
                    {
                        if (row != null && row.Cells[2].Value != null)
                        {
                            isporuka.InsertDobavnicaPostav(Convert.ToInt32(row.Cells[2].Value), Convert.ToDecimal(row.Cells[4].Value), Convert.ToInt32(cbo_Skladiste.SelectedValue), cbo_Lokacija.SelectedValue.ToString(), cbo_MestoTroska.SelectedValue.ToString());
                        }
                    }
                }
                MessageBox.Show("Formirana je otpremnica u centralnoj bazi podataka");
                OtpremnicaPregled frm = new OtpremnicaPregled();
                frm.Show();


                //foreach(DataGridViewRow row in datagridview1){ int sifra=cbo.SelectedValue; int kolicina=cbo2.value }
                //Posto je dozvoljeno dodavanje kroz dgv rucno, kad radi upis u bazu trebalo bi da petlja ide do n-1 jer gleda i poslednji red koji je po default-u prazan
            }
        }

        private void cbo_Skladiste_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btn_Povuci_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Location = new Point(this.ClientSize.Width / 2 - panel1.Size.Width / 2, this.ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;

            var query = "select NaStNar as [Broj Porudzbenice],NaStatus as [Status],NaSmSifra as [Mesto],NaNaziv as [Partner],NaZNes as [Iznos] " +
                "from Narocilo " +
                "inner join sklad on Narocilo.NaSmSifra=sklad.SkSifSM " +
                "inner join Skladiste_Korisnik on sklad.SkSifra = Skladiste_Korisnik.Sif_Skladiste " +
                "Where NaStatus='PO' and Skladiste_Korisnik.Korisnik = '" + korisnik + "' order by NaStNar desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

        }
        int status = 0;
        private void btn_Izaberi_Click(object sender, EventArgs e)
        {
            if (txt_ID.Text != "")
            {
                status = 1;
                var query = "select NaStNar as [BrojPorudzbenice],NaStatus as [Status],NaSmSifra as [Mesto],NaPartPlac,NaNaziv as [Partner],NaZnes as [Iznos],NaPSifra,NaPKolNar,SkSifra " +
                "From Narocilo " +
                "Inner join NarociloPostav on Narocilo.NaStNar = NarociloPostav.NaPStNar " +
                "inner join sklad on Narocilo.NaSmSifra=sklad.SkSifSm " +
                "Where NaPStNar = " + Convert.ToInt32(txt_ID.Text);
                SqlConnection conn = new SqlConnection(connect);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                int count = dr.FieldCount;
                while (dr.Read())
                {
                    cbo_MestoTroska.Enabled = false;
                    cbo_Partner.Enabled = false;
                    cbo_Skladiste.SelectedValue = dr[8].ToString();
                    cbo_MestoTroska.SelectedValue = dr[2].ToString();
                    cbo_Partner.SelectedValue = Convert.ToInt32(dr[3].ToString());
                    cbo_Skladiste_SelectionChangeCommitted(cbo_Skladiste, e);
                }
                conn.Close();

                var query2 = "Select NaPSifra,MpNaziv,NaPKolNar From NarociloPostav inner join MaticniPodatki on NarociloPostav.NaPSifra=MaticniPodatki.MpSifra Where NaPStNar=" + Convert.ToInt32(txt_ID.Text);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query2, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].HeaderText = "MpNaziv";
                dataGridView1.Columns[3].Width = 450;
                dataGridView1.Columns[4].HeaderText = "Količina";
                dataGridView1.Columns[4].Width = 120;
                conn.Close();

                panel1.Visible = false;
            }
            else
            {
                status = 0;
                panel1.Visible = false;
            }

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Selected)
                {
                    txt_ID.Text = row.Cells[0].Value.ToString();

                    SqlConnection conn = new SqlConnection(connect);
                    var query2 = "Select NaPSifra,MpNaziv,NaPKolNar From NarociloPostav inner join MaticniPodatki on NarociloPostav.NaPSifra=MaticniPodatki.MpSifra Where NaPStNar=" + Convert.ToInt32(txt_ID.Text);
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query2, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dataGridView3.DataSource = ds.Tables[0];
                    dataGridView3.Columns[0].Visible = false;
                    dataGridView3.Columns[1].HeaderText = "MpNaziv";
                    dataGridView3.Columns[1].Width = 350;
                    dataGridView3.Columns[2].HeaderText = "Kolicina";
                    dataGridView3.Columns[2].Width = 120;

                    conn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnDodeli_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (row.Selected)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = row.Cells[0].Value;
                    dr[1] = row.Cells[1].Value;
                    dr[2] = row.Cells[2].Value;
                    dr[3] = row.Cells[3].Value;
                    dr[4] = row.Cells[4].Value;
                    dr[5] = row.Cells[5].Value;
                    dr[6] = row.Cells[6].Value;
                    dr[7] = row.Cells[7].Value;
                    dr[8] = row.Cells[8].Value;
                    dr[9] = row.Cells[9].Value;
                    dr[10] = row.Cells[10].Value;
                    dr[11] = row.Cells[11].Value;
                    dr[12] = row.Cells[12].Value;
                    dr[13] = row.Cells[13].Value;
                    dr[14] = row.Cells[14].Value;
                    dr[15] = row.Cells[15].Value;

                    dt.Rows.Add(dr);
                }
            }
            RefreshGV();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OtpremnicaPregled op = new OtpremnicaPregled();
            op.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }

}
