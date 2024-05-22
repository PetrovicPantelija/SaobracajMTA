using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    public partial class Prijemnica : Syncfusion.Windows.Forms.Office2010Form
    {
        string connect = frmLogovanje.connectionString;
        string korisnik,kontejner;
        int nalog,sklad,poz;
        public Prijemnica()
        {
            InitializeComponent();
        }
        public Prijemnica(int NalogID,string Kontejner,int Sklad,int Poz)
        {
            InitializeComponent();
            nalog = NalogID;
            
            kontejner=Kontejner;
            sklad = Sklad;
            poz = Poz;
        }

        private void Prijemnica_Load(object sender, EventArgs e)
        {
            korisnik = frmLogovanje.user;
            FillCombo();
            DGVCombo();
            panel1.Visible = false;
            if (frmLogovanje.Firma == "Leget")
            {
                btn_Povuci.Visible = false;
                cbo_Skladiste.SelectedValue = sklad;
                cbo_Lokacija.SelectedValue = poz;
                txtBrojKontejnera.Text = kontejner;
                korisnik = frmLogovanje.user;
            }
        }
        private void FillCombo()
        {
            string query,display,value;
            if (frmLogovanje.Firma == "Leget")
            {
                query = "select ID,Naziv from Skladista";
                display = "Naziv";
                value = "ID";
            }
            else
            {
                query = "select skSifra,SkNaziv from Sklad";
                display = "SkNaziv";
                value = "SkSifra";
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
            cbo_Referent.DataSource = ds4.Tables[0];
            cbo_Referent.DisplayMember = "Opis";
            cbo_Referent.ValueMember = "ID";

            var query5 = "Select DeSifra as ID,(Rtrim(DePriimek)+' '+RTrim(DeIme)) as Opis From Delavci Where DeSifStat<>'P' order by Opis";
            SqlDataAdapter da5 = new SqlDataAdapter(query5, conn);
            DataSet ds5 = new DataSet();
            da5.Fill(ds5);
            cboPrimio.DataSource = ds5.Tables[0];
            cboPrimio.DisplayMember = "Opis";
            cboPrimio.ValueMember = "ID";

            var query7 = "Select LokSifra, Lokopis from Lokac";
            SqlDataAdapter da7 = new SqlDataAdapter(query7, conn);
            DataSet ds7 = new DataSet();
            da7.Fill(ds7);
            cbo_Lokacija.DataSource = ds7.Tables[0];
            cbo_Lokacija.DisplayMember = "LokOpis";
            cbo_Lokacija.ValueMember = "LokSifra";

            if (frmLogovanje.Firma == "Leget")
            {
                var query8 = "Select ID,Oznaka from Pozicija";
                SqlDataAdapter da8 = new SqlDataAdapter(query8, conn);
                DataSet ds8 = new DataSet();
                da8.Fill(ds8);
                cbo_Lokacija.DataSource = ds8.Tables[0];
                cbo_Lokacija.DisplayMember = "Oznaka";
                cbo_Lokacija.ValueMember = "ID";
            }


            var query2 = "Select SmNaziv,SmSifra From Mesta";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, conn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            cbo_MestoTroska.DataSource = ds2.Tables[0];
            cbo_MestoTroska.DisplayMember = "SmNaziv";
            cbo_MestoTroska.ValueMember = "SmSifra";
        }
        private void DGVCombo()
        {
            DataGridViewComboBoxColumn cbo = new DataGridViewComboBoxColumn();
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

            DataGridViewTextBoxColumn cbo2 = new DataGridViewTextBoxColumn();
            cbo2.HeaderText = "Kolicina";
            cbo2.Name = "Kolicina";

            cbo.Width = 450;
            cbo2.Width = 150;
            dataGridView1.Columns.Add(cbo);
            dataGridView1.Columns.Add(cbo2);
            if (frmLogovanje.Firma == "Leget")
            {

                var queryMP = "Select ID as MpSifra,(RTrim(Broj)+'-'+RTrim(Naziv)) as MpNaziv from NHM order by ID";
                SqlDataAdapter daMP = new SqlDataAdapter(queryMP, conn);
                DataSet dsMP = new DataSet();
                daMP.Fill(dsMP);
                cbo.DataSource = dsMP.Tables[0];
                cbo.DisplayMember = "MpNaziv";
                cbo.ValueMember = "MpSifra";

                DataGridViewComboBoxColumn cbo3 = new DataGridViewComboBoxColumn();
                cbo3.HeaderText = "JedinicaMere";
                cbo3.Name = "JM";
                var query2 = "Select MeSifra,MeNaziv from MerskeEnote";
                SqlDataAdapter da2 = new SqlDataAdapter(query2, conn);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                cbo3.DataSource = ds2.Tables[0];
                cbo3.DisplayMember = "MeNaziv";
                cbo3.ValueMember = "MeSifra";
                cbo3.Width = 90;

                DataGridViewTextBoxColumn cbo4 = new DataGridViewTextBoxColumn();
                cbo4.HeaderText = "LOT";
                cbo4.Name = "Lot";
                DataGridViewCheckBoxColumn cbo5=new DataGridViewCheckBoxColumn();
                cbo5.HeaderText = "Skladisteno";
                cbo5.Name = "Skladisteno";

                dataGridView1.Columns.Add(cbo3);
                dataGridView1.Columns.Add(cbo4);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //foreach(DataGridViewRow row in datagridview1){ int sifra=cbo.SelectedValue; }
        }

        private void cbo_Skladiste_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        int prStDokumenta;
        
        private void button1_Click_1(object sender, EventArgs e)
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
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row != null && row.Cells[0].Value != null)
                    {
                        int skladisteno = 0;
                        if ((bool)row.Cells["Skladisteno"].Value == true)
                        {
                            skladisteno = 1;
                        }
                        ins.InsertPromet(Convert.ToDateTime(dtpVreme.Value), "PRI", prStDokumenta, txtBrojKontejnera.Text.ToString().TrimEnd(), "PRV", Convert.ToDecimal(row.Cells["Kolicina"].Value), 0, Convert.ToInt32(cbo_Skladiste.SelectedValue),
                            Convert.ToInt32(cbo_Lokacija.SelectedValue), 0, 0, Convert.ToDateTime(DateTime.Now), korisnik, 0, Convert.ToInt32(cbo_Referent.SelectedValue), Convert.ToDateTime(dtpVreme.Value.ToString()), row.Cells["JM"].Value.ToString(),
                            row.Cells["Lot"].Value.ToString(), nalog, Convert.ToInt32(row.Cells["MpNaziv"].Value),skladisteno);
                        //isporuka.InsertPrijemnicaPostav(Convert.ToInt32(row.Cells[0].Value), Convert.ToDecimal(row.Cells[1].Value), Convert.ToInt32(cbo_Skladiste.SelectedValue), cbo_Lokacija.SelectedValue.ToString(), cbo_MestoTroska.SelectedValue.ToString());
                        // progressBar1.Value = progressBar1.Value + 1;
                    }
                }
            }
            else
            {
                InsertIsporuka isporuka = new InsertIsporuka();
                isporuka.InsertPrijemnica(Convert.ToInt32(cbo_Partner.SelectedValue), cbo_MestoTroska.SelectedValue.ToString(), Convert.ToInt32(cbo_Referent.SelectedValue), Convert.ToInt32(cboPrimio.SelectedValue), Convert.ToDateTime(dtpVreme.Value), txtBrojKontejnera.Text.ToString().TrimEnd());

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (status == 0)
                    {
                        if (row != null && row.Cells[0].Value != null)
                        {
                            isporuka.InsertPrijemnicaPostav(Convert.ToInt32(row.Cells[0].Value), Convert.ToDecimal(row.Cells[1].Value), Convert.ToInt32(cbo_Skladiste.SelectedValue), cbo_Lokacija.SelectedValue.ToString(), cbo_MestoTroska.SelectedValue.ToString());
                            // progressBar1.Value = progressBar1.Value + 1;
                        }
                    }
                    if (status == 1)
                    {
                        if (row != null && row.Cells[2].Value != null)
                        {
                            isporuka.InsertPrijemnicaPostav(Convert.ToInt32(row.Cells[2].Value), Convert.ToDecimal(row.Cells[4].Value), Convert.ToInt32(cbo_Skladiste.SelectedValue), cbo_Lokacija.SelectedValue.ToString(), cbo_MestoTroska.SelectedValue.ToString());
                            isporuka.UpdPorudzbenica(brPor, Convert.ToInt32(row.Cells[2].Value), Convert.ToDecimal(row.Cells[4].Value));
                        }
                    }
                }
                MessageBox.Show("Formirana je prijemnica u centralnoj bazi podataka");
                PrijemnicaPregled frm = new PrijemnicaPregled();
                frm.Show();
            }
        }
        private void btn_Povuci_Click(object sender, EventArgs e)
        {
            panel1.Location = new Point(this.ClientSize.Width / 2 - panel1.Size.Width / 2, this.ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;

            panel1.Visible = true;
            var query = "Select NNaStNal as [Broj narudžbenice],NNaStatus as [Status],NNaSmSIfra as [Mesto],NNaNaziv as [Partner],RTrim(RTrim(DeIme)+' '+RTrim(DePriimek)) as Primio " +
                "From NNal " +
                "inner join sklad on NNal.NNaSmSifra = sklad.SkSifSM " +
                "inner join NPreP on NNal.NNaStNal = NPrep.NPrPStPre " +
                "inner join NPre on NPreP.NPrPstPre = NPre.NPrStPre " +
                "inner join Delavci on NPre.NPrStDelPre = Delavci.DeSifra " +
                "WHere NNaStatus='PO' ' order by NNaStNal desc";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[3].Width = 170;
            dataGridView2.Columns[1].Width = 60;
            dataGridView2.Columns[0].Width = 75;
        }
        int status = 0;
        int brPor;
        private void btn_Izaberi_Click(object sender, EventArgs e)
        {
            if (txt_ID.Text != "")
            {
                status = 1;
                var query = "Select NNaStNal as [BrojPorudzbenice],NNaStatus as [Status],NNaSmSifra as [Mesto],NNaPartPlac, NNaNaziv as [Partner],NnaZnes as Iznos,SkSifra " +
                    "From NNal " +
                    "inner join sklad on NNal.NNaSmSifra=sklad.SkSifSm " +
                    "Where NNaStNal=" + Convert.ToInt32(txt_ID.Text);
                SqlConnection conn = new SqlConnection(connect);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    brPor = Convert.ToInt32(dr[0].ToString());
                    cbo_MestoTroska.Enabled = false;
                    cbo_Partner.Enabled = false;
                    cbo_MestoTroska.SelectedValue = dr[2].ToString();
                    cbo_Partner.SelectedValue = Convert.ToInt32(dr[3].ToString());
                    cbo_Skladiste.SelectedValue = dr[6].ToString();
                    cbo_Skladiste_SelectionChangeCommitted(cbo_Skladiste, e);
                }
                conn.Close();
                var query2 = "Select NNaPSifra,MpNaziv,NNaPKolNar From NNalP inner join MaticniPodatki on NNalP.NNaPSifra=MaticniPodatki.MpSifra WHere NNaPStNar=" + Convert.ToInt32(txt_ID.Text);
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
                    var query2 = "Select NNaPSifra,MpNaziv,NNaPKolNar From NNalP inner join MaticniPodatki on NNalP.NNaPSifra=MaticniPodatki.MpSifra WHere NNaPStNar=" + Convert.ToInt32(txt_ID.Text);
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
