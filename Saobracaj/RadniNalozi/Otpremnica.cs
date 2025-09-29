using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    public partial class Otpremnica : Form
    {
        string connect = frmLogovanje.connectionString;
        string korisnik;
        bool dgvComboEnabled = true;
        int OtpremnicaID = 0;

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);

            this.ControlBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                }
            }
            else
            {

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        public Otpremnica()
        {
            InitializeComponent();
            ChangeTextBox();
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
            ChangeTextBox();
        }

        public Otpremnica(int NalogID, string Kontejner, string Registracija, string Vozac, int SelektovanaOtpremnica)
        {
            InitializeComponent();
            nalog = NalogID;
            kontejner = Kontejner;
            txtBrojKontejnera.Text = kontejner;
            txtVozilo.Text = Registracija;
            txtVozac.Text = Vozac;
            btn_Povuci.Visible = false;
            cbo_MestoTroska.Visible = false;
            OtpremnicaID = SelektovanaOtpremnica;
            ChangeTextBox();
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
                if (OtpremnicaID != 0)
                {
                    PovuciVecUnete();
                
                }
                else
                {
                    PovuciIzPrijemnice();
                }
               
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

        private void PovuciVecUnete()
        {
            var select = "";
            select = "Select BrojKontejnera,DatumTransakcije,VrstaDokumenta,PrStDokumenta,PrOznSled,PrPrimKol,PrIzdKol,SkladisteU,LokacijaU,SkladisteIz,LokacijaIz,MpSifra," +
                "RTrim(Naziv) as MpNaziv,JedinicaMere,Lot,Skladisteno " +
                "From Promet " +
                "Inner Join NHM on Promet.MpSifra=NHM.ID " +
                " Where PrStDokumenta =" + OtpremnicaID;

            //  "  where  Aktivnosti.Masinovodja = 1 and Zaposleni = " + Convert.ToInt32(cboZaposleni.SelectedValue) + " order by Aktivnosti.ID desc";


            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];

            int row = ds.Tables[0].Rows.Count - 1;

            for (int r = 0; r <= row; r++)
            {
                //dataGridView1.Rows.Add();

                //1 - NHM'), ('2 - LOT'), ('3 - ZBIRNI'


                dataGridView1.Rows[r].Cells[0].Value = ds.Tables[0].Rows[r].ItemArray[0];
                dataGridView1.Rows[r].Cells[1].Value = ds.Tables[0].Rows[r].ItemArray[1];
                dataGridView1.Rows[r].Cells[2].Value = ds.Tables[0].Rows[r].ItemArray[2];
                dataGridView1.Rows[r].Cells[3].Value = ds.Tables[0].Rows[r].ItemArray[3];
                dataGridView1.Rows[r].Cells[4].Value = ds.Tables[0].Rows[r].ItemArray[4];
                dataGridView1.Rows[r].Cells[5].Value = ds.Tables[0].Rows[r].ItemArray[5];
                dataGridView1.Rows[r].Cells[6].Value = ds.Tables[0].Rows[r].ItemArray[6];

            }
            /*
            var select = "Select BrojKontejnera,DatumTransakcije,VrstaDokumenta,PrStDokumenta,PrOznSled,PrPrimKol,PrIzdKol,SkladisteU,LokacijaU,SkladisteIz,LokacijaIz,MpSifra," +
                "RTrim(Naziv) as MpNaziv,JedinicaMere,Lot,Skladisteno " +
                "From Promet " +
                "Inner Join NHM on Promet.MpSifra=NHM.ID " +
                "Where PrIzdKol<PrPrimKol and BrojKontejnera='" + kontejner + "'";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(select, conn);
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["PrIzdKol"].DefaultCellStyle.BackColor = Color.LightGreen;
       
            */
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
                        int Tip = 1;
                        if (row.Cells[0].Value.ToString() == "1 - NHM")
                            Tip = 1;
                        else if (row.Cells[0].Value.ToString() == "2 - LOT")
                            Tip = 2;
                        else
                        {
                            Tip = 3;
                        }
                        ins.InsertPromet(Convert.ToDateTime(dtpVreme.Value), "OTP", prStDokumenta, txtBrojKontejnera.Text.ToString().TrimEnd(), "OTP", 0, Convert.ToDecimal(row.Cells["PrIzdKol"].Value), 0, 0, Convert.ToInt32(cbo_Skladiste.SelectedValue),
                            Convert.ToInt32(cbo_Lokacija.SelectedValue),  Convert.ToDateTime(DateTime.Now), korisnik, 0, Convert.ToInt32(comboBox1.SelectedValue), Convert.ToDateTime(dtpVreme.Value.ToString()), row.Cells["JedinicaMere"].Value.ToString(),
                            row.Cells["Lot"].Value.ToString(), nalog, Convert.ToInt32(row.Cells["MpSifra"].Value), skladisteno, Tip);
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

        private void button4_Click(object sender, EventArgs e)
        {
            InsertIsporuka isporuka = new InsertIsporuka();
            isporuka.PromenaBrojaKontejnera(txtBrojKontejnera.Text, txtNoviKontejner.Text);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }

}
