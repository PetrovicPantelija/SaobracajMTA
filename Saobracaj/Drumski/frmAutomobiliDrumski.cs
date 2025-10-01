using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmAutomobiliDrumski : Form
    {
        string Poruka = "";
        bool status = false;
        string id = "0";


        public frmAutomobiliDrumski()
        {
            InitializeComponent();
            ChangeTextBox();
            ucitajComboBoxove();

        }
        public frmAutomobiliDrumski(int ID)
        {
            id = ID.ToString();
            InitializeComponent();
            ChangeTextBox();
            ucitajComboBoxove();
            VratiPodatke(id);

        }
        private void PostaviVrednostZaposleni()
        {
         
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select  k.DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Zaposleni " +
                                            " FROM Korisnici k " +
                                            "INNER JOIN Delavci d ON k.DeSifra = d.DeSifra " +
                                            "where Trim(Korisnik) like '" + Saobracaj.Sifarnici.frmLogovanje.user.Trim() + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["ID"] != DBNull.Value)
                {
                    txtZaposleniID.Text = dr["ID"].ToString().Trim();
                }
                if (dr["Zaposleni"] != DBNull.Value)
                {
                    txtZaposleni.Text = dr["Zaposleni"].ToString().Trim();
                }
            }
            
        }
        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;


                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                panelHeader.Visible = false;
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Enabled = false;
            ResetujVrednostiPolja();
        }

        private void frmAutomobiliDrumski_Load(object sender, EventArgs e)
        {
            RefreshDataGRid();
            PostaviVrednostZaposleni();

            //this.BeginInvoke(new Action(() =>
            //{

            //    IstekPPomoc();
            //    IstekPP();

            //    IstekReg();
            //}));    

        }
        private void ucitajComboBoxove() 
        {
            var select5 = " select ID, LTRIM(RTRIM(Naziv)) as Naziv from VrstaVozila";
            var s_connection5 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboTipVozila.DataSource = ds5.Tables[0];
            cboTipVozila.DisplayMember = "Naziv";
            cboTipVozila.ValueMember = "ID";

            var partner = "Select PaSifra,PaNaziv From Partnerji  WHERE DrumskiPrevoz = 1 AND ISNULL(Kamioner, 0) = 1 order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, s_connection5);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            DataTable dt = partDS.Tables[0];

            // Kreiraj novi red sa praznim tekstom i ID -1
            DataRow prazanRed = dt.NewRow();
            prazanRed["PaNaziv"] = "";
            prazanRed["PaSifra"] = -1;

            // Ubaci kao prvi red
            dt.Rows.InsertAt(prazanRed, 0);
            cboPrevoznik.DataSource = partDS.Tables[0];
            cboPrevoznik.DisplayMember = "PaNaziv";
            cboPrevoznik.ValueMember = "PaSifra";
        }
        private void RefreshDataGRid()
        {
           

            var select = " select a.ID as ID, " +
           "a.RegBr,vv.Naziv AS Vozilo,  Vozac, p.PaNaziv AS Prevoznik, BrojTelefona, LicnaKarta," +
           "Rtrim(d.DeIme) + ' ' +  Rtrim(d.DePriimek) as ZaposleniIzmenio, " +
           "Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as ZaposleniKreirao " +
           "from Automobili a " +
           "inner join Delavci d on d.DeSifra = a.Zaposleni " +
           "inner join Delavci dk on dk.DeSifra = a.KreiraoZaposleni " +
           "left join VrstaVozila vv on a.VlasnistvoLegeta = vv.ID " +
           "left join Partnerji p on  a.PartnerID = p.PaSifra  " +
           "WHERE a.VoziloDrumskog = 1 ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            int totalWidth = dataGridView1.Width;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 60;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Reg br";
            dataGridView1.Columns[1].Width = (int)(totalWidth * 0.09);

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Tip vozila";
            dataGridView1.Columns[2].Width = (int)(totalWidth * 0.11);

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Vozač";
            dataGridView1.Columns[3].Width = (int)(totalWidth * 0.13);

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Prevoznik";
            dataGridView1.Columns[4].Width = (int)(totalWidth * 0.13);

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Broj telefona";
            dataGridView1.Columns[5].Width = (int)(totalWidth * 0.13);

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Lična karta";
            dataGridView1.Columns[6].Width = (int)(totalWidth * 0.09);

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Zadnja izmena";
            dataGridView1.Columns[7].Width = (int)(totalWidth * 0.13);

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "Kreirao";
            dataGridView1.Columns[8].Width = (int)(totalWidth * 0.13);

        }
        private void tsSave_Click(object sender, EventArgs e)
        {

                int noviID = -1;
                int VlasnistvoLegeta = 0;
                if (cboTipVozila.SelectedValue != null)
                {
                    VlasnistvoLegeta = Convert.ToInt32(cboTipVozila.SelectedValue);
                }
                int? selectedID = null;
                if (dataGridView1.CurrentRow != null)
                {
                    selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                }

                int? ZaposleniID = string.IsNullOrWhiteSpace(txtZaposleniID.Text.ToString()) ? (int?)null : Convert.ToInt32(txtZaposleniID.Text);
                int? parnerID = null;
                if (cboPrevoznik.SelectedValue != null && int.TryParse(cboPrevoznik.SelectedValue.ToString(), out int parsedPrevoznikID) && parsedPrevoznikID >-1)
                {
                parnerID = parsedPrevoznikID;
                }
            if (status == true)
            {
                InsertAutomobili ins = new InsertAutomobili();
                ins.InsAutomobili(ZaposleniID, txtRegBr.Text, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                VlasnistvoLegeta, txtVozac.Text.Trim(), txtLKVozaca.Text.Trim(), txtVozacTelefon.Text.Trim(), 1, ZaposleniID, parnerID, out noviID);
                status = false;
                txtSifra.Text = noviID.ToString();
            }
            else if (!string.IsNullOrWhiteSpace(txtSifra.Text))
            {
                InsertAutomobili upd = new InsertAutomobili();
                upd.UpdAutobili(Convert.ToInt32(txtSifra.Text), ZaposleniID, txtRegBr.Text, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                    VlasnistvoLegeta, txtVozac.Text.Trim(), txtLKVozaca.Text.Trim(), txtVozacTelefon.Text.Trim(), parnerID);
                noviID = Convert.ToInt32(txtSifra.Text);
            }
            RefreshDataGRid();

            if (noviID > 0)
            {
                VratiPodatke(txtSifra.Text);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (Convert.ToInt32(row.Cells["ID"].Value) == noviID)
                    {
                        row.Selected = true;
                        dataGridView1.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }         
        }
        private void ResetujVrednostiPolja()
        {
            txtSifra.Text = "";
            txtRegBr.Text = "";
            txtVozac.Text = "";
            txtLKVozaca.Text = "";
            txtVozacTelefon.Text = "";
            cboTipVozila.SelectedValue = -1; 
        }
        private void tsDelete_Click(object sender, EventArgs e)
        {
            int sifraID;
            if (!string.IsNullOrWhiteSpace(txtSifra.Text) && int.TryParse(txtSifra.Text, out sifraID))
            {
                InsertAutomobili ins = new InsertAutomobili();
            ins.DeleteAutomobili(Convert.ToInt32(txtSifra.Text));
            status = false;
            ResetujVrednostiPolja();
            RefreshDataGRid();
            }
            else
            {
                MessageBox.Show("Selektuj red za brisanje.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void VratiPodatke(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            using (SqlConnection con = new SqlConnection(s_connection))
            {
                con.Open();

                txtSifra.Text = !string.IsNullOrWhiteSpace(ID) ? ID : txtSifra.Text;

                if (string.IsNullOrWhiteSpace(txtSifra.Text) && string.IsNullOrWhiteSpace(ID))
                {
                    MessageBox.Show("Nije unet ID vozila.");
                    return;
                }

                SqlCommand cmd = new SqlCommand("SELECT [ID] " +
             " ,[Zaposleni],[RegBr] ,[Marka],[Sluzbeni] " +
             " ,[Model],[DatumRegistracije],[GodinaProizvodnje],[Gorivo] " +
             " ,[ZapreminaMotora],[Kategorija] ,[VServisUradjen],[VServisKM] " +
             " ,[VServisSledeci],[MServisUradjen] ,[MServisKM],[MServisSledeci] " +
             " ,[BrojPlombe1],[BrojPlombe2] ,[PPAparatDatumOvere],[PPAparatDatumIsteka] " +
             " ,[PPAparatSeriski],[PRvaPomocDatumIsteka] ,[TrougaoIma],[SajlaZaVucu] " +
             " ,[Marker],[Lanci] ,[LokacijaLanci],[ZGDOT] " +
             " ,[ZGLokacija],[ZGDubinaSare],[LGDot],[LGLokacija] " +
             " ,[LGDubinaSare],[Napomena],[CistocaSpolja],[CistocaUnutra] " +
             " ,[NivoUlja],[Nepravilnosti] ,[MestoTroska], [VlasnistvoLegeta], [Vozac], [BrojTelefona], [LicnaKarta], [PartnerID] " +
             "  FROM [Automobili] " +
             " WHERE ID = " + txtSifra.Text, con);


               using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        txtVozac.Text = dr["Vozac"].ToString().Trim();
                        txtVozacTelefon.Text = dr["BrojTelefona"].ToString().Trim();
                        txtLKVozaca.Text = dr["LicnaKarta"].ToString().Trim();
                        if (dr["VlasnistvoLegeta"].ToString() != "")
                            cboTipVozila.SelectedValue = dr["VlasnistvoLegeta"].ToString();
                        txtRegBr.Text = dr["RegBr"].ToString().Trim();
                        string s = dr["PartnerID"].ToString();
                        if (dr["PartnerID"] != DBNull.Value && int.TryParse(dr["PartnerID"].ToString(), out int parsedPartnerID))
                            cboPrevoznik.SelectedValue = parsedPartnerID;
                        else
                            cboPrevoznik.SelectedValue = -1;

                    }

                }
            }
        }

      
        public void IstekPP()
        {
            var connect = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand PPA = new SqlCommand("Select Marka, Model,RegBR FROM Automobili Where DateDiff(Day,GetDate(),PPAparatDatumIsteka)<=30", conn);
            conn.Open();
            SqlDataReader dr = PPA.ExecuteReader();
            while (dr.Read())
            {
                for (int i = 0; i < 1; i++)
                {
                    string RegBR = dr["RegBr"].ToString();
                    string Marka = dr["Marka"].ToString();
                    string Model = dr["Model"].ToString();
                    Poruka += "Za vozilo " + Marka + " " + Model + " " + RegBR + " " + "PP aparat ističe za manje od 30 dana!" + Environment.NewLine + "\n";
                }
            }
            conn.Close();
        }
        public void IstekPPomoc()
        {
            var connect = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand PPomoc = new SqlCommand("Select Marka,Model,RegBr From Automobili Where DateDiff(Day,GetDate(),PrvaPomocDatumIsteka)<=30", conn);
            conn.Open();
            SqlDataReader dr = PPomoc.ExecuteReader();
            while (dr.Read())
            {
                for (int i = 0; i < 1; i++)
                {
                    string RegBR = dr["RegBr"].ToString();
                    string Marka = dr["Marka"].ToString();
                    string Model = dr["Model"].ToString();
                    Poruka += "Za vozilo " + Marka + " " + Model + " " + RegBR + " " + "prva pomoc ističe za manje od 30 dana!" + Environment.NewLine + "\n";
                }
            }
            conn.Close();
        }
        public void IstekReg()
        {
            var connect = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand Reg = new SqlCommand("Select Marka,Model,RegBr From Automobili Where DateDiff(Day,GetDate(),DateAdd(year,1,DatumRegistracije))<=30", conn);
            conn.Open();
            SqlDataReader dr = Reg.ExecuteReader();
            while (dr.Read())
            {
                for (int i = 0; i < 1; i++)
                {
                    string RegBR = dr["RegBr"].ToString();
                    string Marka = dr["Marka"].ToString();
                    string Model = dr["Model"].ToString();
                    Poruka += "Za vozilo " + Marka + " " + Model + " " + RegBR + " " + "registracija ističe za manje od 30 dana!" + Environment.NewLine + "\n";
                }
            }
            MessageBox.Show(Poruka, "Informacije o vozilima", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            conn.Close();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmAutomobiliDokumenta ad = new frmAutomobiliDokumenta(txtSifra.Text);
            ad.Show();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmAutomobiliServis ass = new frmAutomobiliServis(txtSifra.Text);
            ass.Show();
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmAutomobiliRegistracija reg = new frmAutomobiliRegistracija(txtSifra.Text);
            reg.Show();
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmEvidencijaTroskova trosak = new frmEvidencijaTroskova();
            trosak.Show();
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmEvidencijaKvarova kvar = new frmEvidencijaKvarova();
            kvar.Show();
        }

 
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txtSifra.Text);

                        //DateTime danas = DateTime.Now;
                        //DateTime PP = Convert.ToDateTime(row.Cells[8].Value);
                        //DateTime pPomoc = Convert.ToDateTime(row.Cells[9].Value);
                        //DateTime reg = Convert.ToDateTime(row.Cells[10].Value);
                        //DateTime regIstek = reg.AddYears(1);

                        //TimeSpan tsPP = PP - danas;
                        //TimeSpan tsPomoc = pPomoc - danas;
                        //TimeSpan tsReg = regIstek - danas;

                        //int diffPP = tsPP.Days;
                        //if (diffPP <= 30)
                        //{
                        //    MessageBox.Show("PP Aparat ističe za manje od 30 dana");
                        //}
                        //int diffPomoc = tsPomoc.Days;
                        //if (diffPomoc <= 30)
                        //{
                        //    MessageBox.Show("Prva Pomoć ističe za manje od 30 dana");
                        //}
                        //int diffReg = tsReg.Days;
                        //if (diffReg <= 30)
                        //{
                        //    MessageBox.Show("Registracija ističe za manje od 30 dana");
                        //}
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
    }
}
