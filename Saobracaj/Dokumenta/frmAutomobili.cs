using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmAutomobili : Form
    {
        string Poruka = "";
        bool status = false;
        public frmAutomobili()
        {
            InitializeComponent();
            ChangeTextBox();
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
            txtSifra.Text = "";
            txtRegBr.Text = "";
            txtMarka.Text = "";
        }

        private void frmAutomobili_Load(object sender, EventArgs e)
        {
            RefreshDataGRid();
            this.BeginInvoke(new Action(() =>
            {
               
                IstekPPomoc();
                IstekPP();

                IstekReg();
            }));    
        
        }
        private void RefreshDataGRid()
        {
            var select3 = " select DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Opis from Delavci order by opis";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboZaposleni.DataSource = ds3.Tables[0];
            cboZaposleni.DisplayMember = "Opis";
            cboZaposleni.ValueMember = "ID";


            var select4 = " select SmSifra from Mesta";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboMestoTroska.DataSource = ds4.Tables[0];
            cboMestoTroska.DisplayMember = "SmSifra";
            cboMestoTroska.ValueMember = "SmSifra";

            var select5 = " select ID, LTRIM(RTRIM(Naziv)) as Naziv from VrstaVozila";
            var s_connection5 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection5);
            var c5 = new SqlConnection(s_connection5);
            var dataAdapter5 = new SqlDataAdapter(select5, c5);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter5);
            var ds5 = new DataSet();
            dataAdapter5.Fill(ds5);
            cboVozilo.DataSource = ds5.Tables[0];
            cboVozilo.DisplayMember = "Naziv";
            cboVozilo.ValueMember = "ID";

            var select = "  select Automobili.ID as ID, Automobili.Zaposleni, " +
           " Rtrim(Delavci.DeIme) + ' ' +  Rtrim(Delavci.DePriimek) as ZaposleniNaziv, " +
           " Automobili.RegBr, Automobili.Marka, Automobili.Sluzbeni, VServisSledeci as VelServisSled, MServisSledeci as MaliServSled," +
           " PPAparatDatumIsteka,PRvaPomocDatumIsteka,DatumRegistracije,Vozac ,VrstaVozila.Naziv AS Vozilo " +
           " from Automobili " +
            " inner join Delavci on Delavci.DeSifra = Automobili.Zaposleni " +
            " left join VrstaVozila on Automobili.VlasnistvoLegeta = VrstaVozila.ID";

            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Zaposleni ID";
            dataGridView1.Columns[1].Width = 50;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Zaposleni naziv";
            dataGridView1.Columns[2].Width = 150;


            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "Reg br";
            dataGridView1.Columns[3].Width = 150;

            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Marka";
            dataGridView1.Columns[4].Width = 80;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Službeni";
            dataGridView1.Columns[5].Width = 40;

            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Veliki servis istek";
            dataGridView1.Columns[6].Width = 70;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Mali servis istek";
            dataGridView1.Columns[7].Width = 70;

            DataGridViewColumn column9 = dataGridView1.Columns[8];
            dataGridView1.Columns[8].HeaderText = "PPAparatDatumIsteka";
            dataGridView1.Columns[8].Width = 120;

            DataGridViewColumn column10 = dataGridView1.Columns[9];
            dataGridView1.Columns[9].HeaderText = "PrvaPomocDatumIsteka";
            dataGridView1.Columns[9].Width = 120;

            DataGridViewColumn column11 = dataGridView1.Columns[10];
            dataGridView1.Columns[10].HeaderText = "DatumRegistracije";
            dataGridView1.Columns[10].Width = 120;
        }
        private void tsSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSifra.Text))
            {
                int Sluzbeni = 0;
                int TrougaoIma = 0;
                int MarkerIma = 0;
                int SajluZaVucu = 0;
                int ImaLance = 0;
                int VlasnistvoLegeta = 0;

                int? selectedID = null;
                if (dataGridView1.CurrentRow != null)
                {
                    selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                }


                if (chkLanci.Checked)
                {
                    ImaLance = 1;
                }
                else
                {
                    ImaLance = 0;
                }

                if (chkSajlaZaVucu.Checked)
                {
                    SajluZaVucu = 1;
                }
                else
                {
                    SajluZaVucu = 0;
                }

                if (chkMarker.Checked)
                {
                    MarkerIma = 1;
                }
                else
                {
                    MarkerIma = 0;
                }
                if (chkImaTrougao.Checked)
                {
                    TrougaoIma = 1;
                }
                else
                {
                    TrougaoIma = 0;
                }
                if (chkSluzbeni.Checked)
                {
                    Sluzbeni = 1;
                }
                else
                {
                    Sluzbeni = 0;
                }
                //if (chkVlasnistvoLegeta.Checked)
                //{
                //    VlasnistvoLegeta = 1;
                //}
                //else
                //{
                //    VlasnistvoLegeta = 0;
                //}Convert.ToInt32(txtMesto.SelectedValue)

                if (cboVozilo.SelectedValue != null)
                {
                    VlasnistvoLegeta = Convert.ToInt32(cboVozilo.SelectedValue);
                }
                if (status == true)
                {
                    InsertAutomobili ins = new InsertAutomobili();
                    ins.InsAutomobili(Convert.ToInt32(cboZaposleni.SelectedValue), txtRegBr.Text, txtMarka.Text, Sluzbeni, txtModel.Text, dtpDatumRegistracije.Value
                        , Convert.ToInt32(txtGodinaProizvodnje.Text), txtGorivo.Text, Convert.ToInt32(txtZapreminaMotora.Text),
                        txtKategorija.Text, Convert.ToDateTime(dtpVServisUradjen.Value), Convert.ToDouble(txtVServisKM.Text), Convert.ToDateTime(dtpVServisSledeci.Value),
                        Convert.ToDateTime(dtpMaliServisUradjen.Value), Convert.ToDouble(txtMaliServisKM.Text), Convert.ToDateTime(dtpMaliServisSledeci.Value),
                        txtBrojPlombe1.Text, txtBrojPlombe2.Text, Convert.ToDateTime(dtpPPAparatDatumOvere.Value), Convert.ToDateTime(dtpPPAparatDatumIsteka.Value),
                        txtPPAparatSeriskiBroj.Text, Convert.ToDateTime(dtpPrvaPomocDatumIsteka.Value), TrougaoIma,
                    MarkerIma, SajluZaVucu, ImaLance, txtLokacijaLanci.Text, txtZGDot.Text, txtZGLokacija.Text,
                    txtZGSare.Text, txtLGgumeDOT.Text, txtLGLokacija.Text, txtLGSare.Text, txtNapomena.Text,
                    txtCistocaSpolja.Text, txtCistocaUnutra.Text, txtNivoUlja.Text, txtNepravilnosti.Text, cboMestoTroska.Text,
                    VlasnistvoLegeta, txtVozac.Text.Trim(), txtLKVozaca.Text.Trim(), txtVozacTelefon.Text.Trim());
                    status = false;
                }
                else
                {
                    InsertAutomobili upd = new InsertAutomobili();
                    upd.UpdAutobili(Convert.ToInt32(txtSifra.Text), Convert.ToInt32(cboZaposleni.SelectedValue), txtRegBr.Text, txtMarka.Text, Sluzbeni, txtModel.Text, dtpDatumRegistracije.Value
                        , Convert.ToInt32(txtGodinaProizvodnje.Text), txtGorivo.Text, Convert.ToInt32(txtZapreminaMotora.Text),
                        txtKategorija.Text, Convert.ToDateTime(dtpVServisUradjen.Value), Convert.ToDouble(txtVServisKM.Text), Convert.ToDateTime(dtpVServisSledeci.Value),
                        Convert.ToDateTime(dtpMaliServisUradjen.Value), Convert.ToDouble(txtMaliServisKM.Text), Convert.ToDateTime(dtpMaliServisSledeci.Value),
                        txtBrojPlombe1.Text, txtBrojPlombe2.Text, Convert.ToDateTime(dtpPPAparatDatumOvere.Value), Convert.ToDateTime(dtpPPAparatDatumIsteka.Value),
                        txtPPAparatSeriskiBroj.Text, Convert.ToDateTime(dtpPrvaPomocDatumIsteka.Value), TrougaoIma,
                    MarkerIma, SajluZaVucu, ImaLance, txtLokacijaLanci.Text, txtZGDot.Text, txtZGLokacija.Text,
                    txtZGSare.Text, txtLGgumeDOT.Text, txtLGLokacija.Text, txtLGSare.Text, txtNapomena.Text,
                    txtCistocaSpolja.Text, txtCistocaUnutra.Text, txtNivoUlja.Text, txtNepravilnosti.Text, cboMestoTroska.Text,
                    VlasnistvoLegeta, txtVozac.Text.Trim(), txtLKVozaca.Text.Trim(), txtVozacTelefon.Text.Trim());
                }
                RefreshDataGRid();
                if (selectedID.HasValue)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["ID"].Value) == selectedID.Value)
                        {
                            row.Selected = true;
                            dataGridView1.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
                VratiPodatke(txtSifra.Text);
            }
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertAutomobili ins = new InsertAutomobili();
            ins.DeleteAutomobili(Convert.ToInt32(txtSifra.Text));
            status = false;
            RefreshDataGRid();
        }

        private void VratiPodatke(string ID)
        {
            var s_connection =Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

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
             " ,[NivoUlja],[Nepravilnosti] ,[MestoTroska], [VlasnistvoLegeta], [Vozac], [BrojTelefona], [LicnaKarta] " +
             " FROM [Automobili] " +
             " WHERE ID=" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtModel.Text = dr["Model"].ToString();
                dtpDatumRegistracije.Value = Convert.ToDateTime(dr["DatumRegistracije"].ToString());
                txtGodinaProizvodnje.Text = dr["GodinaProizvodnje"].ToString();
                txtGorivo.Text = dr["Gorivo"].ToString();
                txtZapreminaMotora.Text = dr["ZapreminaMotora"].ToString();
                txtKategorija.Text = dr["Kategorija"].ToString();
                dtpVServisUradjen.Value = Convert.ToDateTime(dr["VServisUradjen"].ToString());
                txtVServisKM.Text = dr["VServisKM"].ToString();
                dtpVServisSledeci.Value = Convert.ToDateTime(dr["VServisSledeci"].ToString());
                dtpMaliServisUradjen.Value = Convert.ToDateTime(dr["MServisUradjen"].ToString());
                txtMaliServisKM.Text = dr["MServisKM"].ToString();
                dtpMaliServisSledeci.Value = Convert.ToDateTime(dr["MServisSledeci"].ToString());
                txtBrojPlombe1.Text = dr["BrojPlombe1"].ToString();
                txtBrojPlombe2.Text = dr["BrojPlombe2"].ToString();
                dtpPPAparatDatumOvere.Value = Convert.ToDateTime(dr["PPAparatDatumOvere"].ToString());
                dtpPPAparatDatumIsteka.Value = Convert.ToDateTime(dr["PPAparatDatumIsteka"].ToString());
                txtPPAparatSeriskiBroj.Text = dr["PPAparatSeriski"].ToString();
                dtpPrvaPomocDatumIsteka.Value = Convert.ToDateTime(dr["PRvaPomocDatumIsteka"].ToString());
                txtVozac.Text= dr["Vozac"].ToString();
                txtVozacTelefon.Text = dr["BrojTelefona"].ToString();
                txtLKVozaca.Text = dr["LicnaKarta"].ToString();
                if (dr["TrougaoIma"].ToString() == "1")
                {
                    chkImaTrougao.Checked = true;
                }
                else
                {
                    chkImaTrougao.Checked = false;
                }

                if (dr["SajlaZaVucu"].ToString() == "1")
                {
                    chkSajlaZaVucu.Checked = true;
                }
                else
                {
                    chkSajlaZaVucu.Checked = false;
                }

                if (dr["Marker"].ToString() == "1")
                {
                    chkMarker.Checked = true;
                }
                else
                {
                    chkMarker.Checked = false;
                }

                if (dr["Lanci"].ToString() == "1")
                {
                    chkLanci.Checked = true;
                }
                else
                {
                    chkLanci.Checked = false;
                }
                //if (dr["VlasnistvoLegeta"].ToString() == "1")
                //{
                //    chkVlasnistvoLegeta.Checked = true;
                //}
                //else
                //{
                //    chkVlasnistvoLegeta.Checked = false;
                //}
                if (dr["VlasnistvoLegeta"].ToString() != "")
                    cboVozilo.SelectedValue = dr["VlasnistvoLegeta"].ToString();


                txtLokacijaLanci.Text = dr["LokacijaLanci"].ToString();
                txtZGDot.Text = dr["ZGDOT"].ToString();
                txtZGLokacija.Text = dr["ZGLokacija"].ToString();
                txtZGSare.Text = dr["ZGDubinaSare"].ToString();
                txtLGgumeDOT.Text = dr["LGDot"].ToString();
                txtLGLokacija.Text = dr["LGLokacija"].ToString();
                txtLGSare.Text = dr["LGDubinaSare"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();
                txtCistocaSpolja.Text = dr["CistocaSpolja"].ToString();
                txtCistocaUnutra.Text = dr["CistocaUnutra"].ToString();
                txtNivoUlja.Text = dr["NivoUlja"].ToString();
                txtNepravilnosti.Text = dr["Nepravilnosti"].ToString();
                cboMestoTroska.SelectedValue = dr["MestoTroska"].ToString();
                cboZaposleni.SelectedValue = Convert.ToInt32(dr["Zaposleni"].ToString());
                txtRegBr.Text = dr["RegBr"].ToString();
                txtMarka.Text = dr["Marka"].ToString();

                if (dr["Sluzbeni"].ToString() == "1")
                {
                    chkSluzbeni.Checked = true;
                }
                else
                {
                    chkSluzbeni.Checked = false;
                }
            }
            con.Close();
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

                        DateTime danas = DateTime.Now;
                        DateTime PP = Convert.ToDateTime(row.Cells[8].Value);
                        DateTime pPomoc = Convert.ToDateTime(row.Cells[9].Value);
                        DateTime reg = Convert.ToDateTime(row.Cells[10].Value);
                        DateTime regIstek = reg.AddYears(1);

                        TimeSpan tsPP = PP - danas;
                        TimeSpan tsPomoc = pPomoc - danas;
                        TimeSpan tsReg = regIstek - danas;

                        int diffPP = tsPP.Days;
                        if (diffPP <= 30)
                        {
                            MessageBox.Show("PP Aparat ističe za manje od 30 dana");
                        }
                        int diffPomoc = tsPomoc.Days;
                        if (diffPomoc <= 30)
                        {
                            MessageBox.Show("Prva Pomoć ističe za manje od 30 dana");
                        }
                        int diffReg = tsReg.Days;
                        if (diffReg <= 30)
                        {
                            MessageBox.Show("Registracija ističe za manje od 30 dana");
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
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

        private void txtCistocaUnutra_TextChanged(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {

        }
    }
}
