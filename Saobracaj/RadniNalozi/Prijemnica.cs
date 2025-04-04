﻿using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Saobracaj.Uvoz;

namespace Saobracaj.RadniNalozi
{
    public partial class Prijemnica : Form
    {
        string connect = frmLogovanje.connectionString;
        string korisnik,kontejner;
        int nalog,sklad,poz;
        int usao = 0;
        int PrijemnicaID = 0;

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

                foreach (Control control in this.Controls)
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

        public Prijemnica()
        {
            InitializeComponent();
            ChangeTextBox();
        }
        public Prijemnica(int NalogID,string Kontejner,int Sklad,int Poz)
        {
            InitializeComponent();
            nalog = NalogID;
            
            kontejner=Kontejner;
            sklad = Sklad;
            poz = Poz;
            txtNalogID.Text = nalog.ToString();
            if (frmLogovanje.Firma == "Leget")
            {
                cbo_Skladiste.Visible = false;
                cbo_Lokacija.Visible = false;
                cbo_MestoTroska.Visible = false;
                cbo_Partner.Visible = false;
            }
            ChangeTextBox();
            }

        public Prijemnica(int NalogID, string Kontejner, int Sklad, int Poz, int Prijemnica)
        {
            InitializeComponent();
            nalog = NalogID;

            kontejner = Kontejner;
            sklad = Sklad;
            poz = Poz;
            txtNalogID.Text = nalog.ToString();
            if (frmLogovanje.Firma == "Leget")
            {
                cbo_Skladiste.Visible = false;
                cbo_Lokacija.Visible = false;
                cbo_MestoTroska.Visible = false;
                cbo_Partner.Visible = false;
            }
            PrijemnicaID = Prijemnica;
            ChangeTextBox();
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
                if (PrijemnicaID != 0)
                {
                    SetGrupaPolja();
                    RefreshDataGrid();
                    
                    
                  

                }
            }
        }

        private void SetGrupaPolja()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 SkladistaGrupa.ID from SkladistaGrupa inner join Skladista on Skladista.GrupaPoljaID = SkladistaGrupa.ID inner join Promet on Promet.SkladisteU = Skladista.ID where Promet.PrStDokumenta = " + PrijemnicaID, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cboGrupaPolja.SelectedValue = Convert.ToInt32(dr["ID"].ToString());
            }

            con.Close();

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
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            cbo_Skladiste.DataSource = ds.Tables[0];
            cbo_Skladiste.DisplayMember = display;
            cbo_Skladiste.ValueMember = value;

            var query3 = "Select PaSifra,PaNaziv from Partnerji order by PaNaziv";
            SqlDataAdapter da3 = new SqlDataAdapter(query3, conn);
            System.Data.DataSet ds3 = new System.Data.DataSet();
            da3.Fill(ds3);
            cbo_Partner.DataSource = ds3.Tables[0];
            cbo_Partner.DisplayMember = "PaNaziv";
            cbo_Partner.ValueMember = "PaSifra";

            var query4 = "Select DeSifra as ID,(RTrim(DePriimek)+' '+RTrim(DeIme)) as Opis From Delavci Where DeSifStat<>'P' order by Opis";
            SqlDataAdapter da4 = new SqlDataAdapter(query4, conn);
            System.Data.DataSet ds4 = new System.Data.DataSet();
            da4.Fill(ds4);
            cbo_Referent.DataSource = ds4.Tables[0];
            cbo_Referent.DisplayMember = "Opis";
            cbo_Referent.ValueMember = "ID";

            var query5 = "Select DeSifra as ID,(Rtrim(DePriimek)+' '+RTrim(DeIme)) as Opis From Delavci Where DeSifStat<>'P' order by Opis";
            SqlDataAdapter da5 = new SqlDataAdapter(query5, conn);
            System.Data.DataSet ds5 = new System.Data.DataSet();
            da5.Fill(ds5);
            cboPrimio.DataSource = ds5.Tables[0];
            cboPrimio.DisplayMember = "Opis";
            cboPrimio.ValueMember = "ID";

            var query7 = "Select LokSifra, Lokopis from Lokac";
            SqlDataAdapter da7 = new SqlDataAdapter(query7, conn);
            System.Data.DataSet ds7 = new System.Data.DataSet();
            da7.Fill(ds7);
            cbo_Lokacija.DataSource = ds7.Tables[0];
            cbo_Lokacija.DisplayMember = "LokOpis";
            cbo_Lokacija.ValueMember = "LokSifra";

            var select41 = "  select ID, Naziv from SkladistaGrupa order by ID";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c41 = new SqlConnection(s_connection4);
            var dataAdapter41 = new SqlDataAdapter(select41, c41);

            var commandBuilder41 = new SqlCommandBuilder(dataAdapter41);
            var ds41 = new System.Data.DataSet();
            dataAdapter41.Fill(ds41);
            cboGrupaPolja.DataSource = ds41.Tables[0];
            cboGrupaPolja.DisplayMember = "Naziv";
            cboGrupaPolja.ValueMember = "ID";

            if (frmLogovanje.Firma == "Leget")
            {
                var query8 = "Select ID,Oznaka from Pozicija";
                SqlDataAdapter da8 = new SqlDataAdapter(query8, conn);
                System.Data.DataSet ds8 = new System.Data.DataSet();
                da8.Fill(ds8);
                cbo_Lokacija.DataSource = ds8.Tables[0];
                cbo_Lokacija.DisplayMember = "Oznaka";
                cbo_Lokacija.ValueMember = "ID";
            }


            var query2 = "Select SmNaziv,SmSifra From Mesta";
            SqlDataAdapter da2 = new SqlDataAdapter(query2, conn);
            System.Data.DataSet ds2 = new System.Data.DataSet();
            da2.Fill(ds2);
            cbo_MestoTroska.DataSource = ds2.Tables[0];
            cbo_MestoTroska.DisplayMember = "SmNaziv";
            cbo_MestoTroska.ValueMember = "SmSifra";
        }

        DataGridViewComboBoxColumn cbo = new DataGridViewComboBoxColumn();
       
        private void DGVCombo()
        {
            DataGridViewComboBoxColumn cb = new DataGridViewComboBoxColumn();
            cb.HeaderText = "Tip";
            cb.Name = "Tip";
            var query21 = "SELECT ID, Naziv FROM TipRobe";
            SqlConnection conn21 = new SqlConnection(connect);
            SqlDataAdapter da21 = new SqlDataAdapter(query21, conn21);
            System.Data.DataSet ds21 = new System.Data.DataSet();
            da21.Fill(ds21);
            cb.DataSource = ds21.Tables[0];
            cb.DisplayMember = "Naziv";
            cb.ValueMember = "ID";

            cbo.HeaderText = "MPNaziv";
            cbo.Name = "MPNaziv";
            var query = "Select MpSifra,MpNaziv from MaticniPodatki  order by MpNaziv";
            SqlConnection conn = new SqlConnection(connect);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            cbo.DataSource = ds.Tables[0];
            cbo.DisplayMember = "MpNaziv";
            cbo.ValueMember = "MpSifra";
        

            DataGridViewTextBoxColumn cbo2 = new DataGridViewTextBoxColumn();
            cbo2.HeaderText = "Kolicina";
            cbo2.Name = "Kolicina";
            cbo.Width = 450;
            cbo2.Width = 150;
            dataGridView1.Columns.Add(cb);
            dataGridView1.Columns.Add(cbo);
            dataGridView1.Columns.Add(cbo2);
            if (frmLogovanje.Firma == "Leget")
            {
                
                var queryMP = "Select ID as MpSifra,(RTrim(Broj)+'-'+RTrim(Naziv)) as MpNaziv from NHM where Interni = 1 order by ID";
                SqlDataAdapter daMP = new SqlDataAdapter(queryMP, conn);
                System.Data.DataSet dsMP = new System.Data.DataSet();
                daMP.Fill(dsMP);
                cbo.DataSource = dsMP.Tables[0];
                cbo.DisplayMember = "MpNaziv";
                cbo.ValueMember = "MpSifra";
            

                DataGridViewComboBoxColumn cbo3 = new DataGridViewComboBoxColumn();
                cbo3.HeaderText = "JedinicaMere";
                cbo3.Name = "JM";
                var query2 = "Select MeSifra,MeNaziv from MerskeEnote";
                SqlDataAdapter da2 = new SqlDataAdapter(query2, conn);
                System.Data.DataSet ds2 = new System.Data.DataSet();
                da2.Fill(ds2);
                cbo3.DataSource = ds2.Tables[0];
                cbo3.DisplayMember = "MeNaziv";
                cbo3.ValueMember = "MeSifra";
                cbo3.Width = 90;


                DataGridViewComboBoxColumn cbo31 = new DataGridViewComboBoxColumn();
                cbo31.HeaderText = "Skladiste";
                cbo31.Name = "Skladiste";
                var query31 = "select ID ,Naziv from Skladista order by ID";
                SqlDataAdapter da31 = new SqlDataAdapter(query31, conn);
                System.Data.DataSet ds31 = new System.Data.DataSet();
                da31.Fill(ds31);
                cbo31.DataSource = ds31.Tables[0];
                cbo31.DisplayMember = "Naziv";
                cbo31.ValueMember = "ID";
                cbo31.Width = 90;

                usao = 1;

                DataGridViewTextBoxColumn cbo4 = new DataGridViewTextBoxColumn();
                cbo4.HeaderText = "LOT";
                cbo4.Name = "Lot";
                DataGridViewCheckBoxColumn cbo5=new DataGridViewCheckBoxColumn();
                cbo5.HeaderText = "Skladisteno";
                cbo5.Name = "Skladisteno";

                dataGridView1.Columns.Add(cbo3);
                dataGridView1.Columns.Add(cbo31);
                dataGridView1.Columns.Add(cbo4);
                dataGridView1.Columns.Add(cbo5);
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

        int VratiIDPrometa()
        {

            return 0;
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            string LOt = "";
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
                        if (Convert.ToBoolean(row.Cells["Skladisteno"].Value )== true)
                        {
                            skladisteno = 1;
                        }
                        if (row.Cells["Lot"].Value is null)
                        {
                            LOt = " ";
                        }
                        else
                        {
                            LOt = row.Cells["Lot"].Value.ToString();
                        }
                        //       string LOt = row.Cells["Lot"].Value.ToString();

                        int Tip = 1;
                        if (row.Cells[0].Value.ToString() == "NHM")
                            Tip = 1;
                        else if (row.Cells[0].Value.ToString() == "LOT")
                            Tip = 2;
                        else
                        {
                            Tip = 3;
                        }
                          
                        ins.InsertPromet(Convert.ToDateTime(dtpVreme.Value), "PRI", prStDokumenta, txtBrojKontejnera.Text.ToString().TrimEnd(), "PRV", Convert.ToDecimal(row.Cells["Kolicina"].Value), 0, Convert.ToInt32(row.Cells["Skladiste"].Value),
                            Convert.ToInt32(cbo_Lokacija.SelectedValue), 0, 0, Convert.ToDateTime(DateTime.Now), korisnik.Trim(), 0, Convert.ToInt32(cbo_Referent.SelectedValue), Convert.ToDateTime(dtpVreme.Value.ToString()), row.Cells["JM"].Value.ToString(),
                            LOt, nalog, Convert.ToInt32(row.Cells["MpNaziv"].Value),skladisteno, Tip);

                       // txtPrometID.Text = VratiIDPrometa().ToString();
                    
                        //isporuka.InsertPrijemnicaPostav(Convert.ToInt32(row.Cells[0].Value), Convert.ToDecimal(row.Cells[1].Value), Convert.ToInt32(cbo_Skladiste.SelectedValue), cbo_Lokacija.SelectedValue.ToString(), cbo_MestoTroska.SelectedValue.ToString());
                        // progressBar1.Value = progressBar1.Value + 1;
                    }
                }
            InsertRN up = new InsertRN();

           up.PotvrdiUradjenPretovarCirade(nalog, korisnik);
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
            System.Data.DataSet ds = new System.Data.DataSet();
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
                System.Data.DataSet ds = new System.Data.DataSet();
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
                    System.Data.DataSet ds = new System.Data.DataSet();
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

        private void button3_Click_1(object sender, EventArgs e)
        {
            PrijemnicaPregled pp = new PrijemnicaPregled();
            pp.Show();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
          
                var cell = dataGridView1[e.ColumnIndex, e.RowIndex];
                if (cell.OwningColumn.Name == "Tip")
                {
                if (cell.OwningRow.Cells["Tip"].Value.ToString() == "ZBIRNI")

                {
                   
                    int row = cell.RowIndex;
                    int col = cell.ColumnIndex;
                   // dataGridView1.Rows[row].Selected = true;
                 //   dataGridView1.Rows[row].Cells[col].Selected = true;
                    dataGridView1.Rows[row].Cells[1].Value= 0;

                }
            }
        }

        private void cboGrupaPolja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (usao == 1)
            {
                SqlConnection conn = new SqlConnection(connect);
                DataGridViewComboBoxColumn col = dataGridView1.Columns[4] as DataGridViewComboBoxColumn;
               // col.HeaderText = "Skladiste";
              //  col.Name = "Skladiste";
                var query31 = "select ID ,Naziv from Skladista where GrupaPoljaID = " + Convert.ToInt32(cboGrupaPolja.SelectedValue) + " order by ID";
                SqlDataAdapter da31 = new SqlDataAdapter(query31, conn);
               System.Data.DataSet ds31 = new System.Data.DataSet();
                da31.Fill(ds31);
                col.DataSource = ds31.Tables[0];
                col.DisplayMember = "Naziv";
                col.ValueMember = "ID";
              


           
            }
        }

        private void RefreshDataGrid()
        {
            var select = "";
            select = @" Select Tip, MpSifra, PrPrimKol, JedinicaMere, SkladisteU, Lot, Skladisteno from Promet Where PrStDokumenta =" + PrijemnicaID;

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

            int row = ds.Tables[0].Rows.Count - 1 ;

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

        }

        private void button23_Click(object sender, EventArgs e)
        {
            ZapisnikOOstecenjuRobe zor = new ZapisnikOOstecenjuRobe(txtNalogID.Text);
            zor.Show();
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
