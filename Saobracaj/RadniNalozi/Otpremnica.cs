using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    public partial class Otpremnica : Syncfusion.Windows.Forms.Office2010Form
    {
        string connect = frmLogovanje.connectionString;
        string korisnik;
        public Otpremnica()
        {
            InitializeComponent();
        }

        private void Otpremnica_Load(object sender, EventArgs e)
        {
            korisnik = frmLogovanje.user;
            FillCombo();
            DGVCombo();
            panel1.Visible = false;
            //panel2.Visible = false;
        }
        private void FillCombo()
        {
            var query = "select skSifra,SkNaziv from Sklad";/* Where SkSifra in (Select Sif_Skladiste From Skladiste_Korisnik Where Korisnik='"+korisnik.ToString()+"')";*/
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbo_Skladiste.DataSource = ds.Tables[0];
            cbo_Skladiste.DisplayMember = "SkNaziv";
            cbo_Skladiste.ValueMember = "SkSifra";



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


            var query7 = "select LokSifra,LokOpis from Lokac";
            SqlDataAdapter da7 = new SqlDataAdapter(query7, conn);
            DataSet ds7 = new DataSet();
            da7.Fill(ds7);
            cbo_Lokacija.DataSource = ds7.Tables[0];
            cbo_Lokacija.DisplayMember = "LokOpis";
            cbo_Lokacija.ValueMember = "LokSifra";

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

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }

}
