using Saobracaj.Dokumenta;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmAutomobilEdit: Form
    {
        DataRow rpod;
        bool status = false;
        public event Action<int> SnimanjeZavrseno;


        public frmAutomobilEdit()
        {
            InitializeComponent();
            ChangeTextBox();
            ucitajComboBoxove();
            status = true;
            txtSifra.Enabled = false;
            button1.Visible = false;
            lblNaslov.Text = "UNOS NOVOG JE U TOKU";

        }
        public frmAutomobilEdit(DataRow r)
        {
            rpod = r;
            InitializeComponent();
            ChangeTextBox();
            ucitajComboBoxove();
            VratiPodatke(rpod);
            lblNaslov.Text = "IZMENA ZAPISA JE U TOKU";

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

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
           


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
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
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
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

        private void VratiPodatke(DataRow redPodataka)
        {
            DataRow red = redPodataka;

            if (red != null)
            {
                txtSifra.Text = red["ID"].ToString();
                txtVozac.Text = red["Vozac"].ToString().Trim();
                txtVozacTelefon.Text = red["BrojTelefona"].ToString().Trim();
                txtLKVozaca.Text = red["LicnaKarta"].ToString().Trim();

                if (red["VlasnistvoLegeta"] != DBNull.Value)
                    cboTipVozila.SelectedValue = red["VlasnistvoLegeta"].ToString();

                txtRegBr.Text = red["RegBr"].ToString().Trim();

                if (red["PartnerID"] != DBNull.Value && int.TryParse(red["PartnerID"].ToString(), out int partnerId))
                    cboPrevoznik.SelectedValue = partnerId;
                else
                    cboPrevoznik.SelectedValue = -1;
            }
            else
            {
                MessageBox.Show("Nema podataka za zadati ID.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int noviID = -1;
            int VlasnistvoLegeta = 0;
            if (cboTipVozila.SelectedValue != null)
            {
                VlasnistvoLegeta = Convert.ToInt32(cboTipVozila.SelectedValue);
            }
            //int? selectedID = null;
            //if (dataGridView1.CurrentRow != null)
            //{
            //    selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            //}

            int? ZaposleniID = string.IsNullOrWhiteSpace(txtZaposleniID.Text.ToString()) ? (int?)null : Convert.ToInt32(txtZaposleniID.Text);
            int? parnerID = null;
            if (cboPrevoznik.SelectedValue != null && int.TryParse(cboPrevoznik.SelectedValue.ToString(), out int parsedPrevoznikID) && parsedPrevoznikID > -1)
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

           
            if (noviID > 0)
                SnimanjeZavrseno?.Invoke(noviID);

            this.Close();

            //if (noviID > 0)
            //{
            //    VratiPodatke(txtSifra.Text);
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        if (Convert.ToInt32(row.Cells["ID"].Value) == noviID)
            //        {
            //            row.Selected = true;
            //            dataGridView1.CurrentCell = row.Cells[0];
            //            break;
            //        }
            //    }
            //}
        }

        private void frmAutomobilEdit_Load(object sender, EventArgs e)
        {
            PostaviVrednostZaposleni();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sifraID;
            if (!string.IsNullOrWhiteSpace(txtSifra.Text) && int.TryParse(txtSifra.Text, out sifraID))
            {
                DialogResult result = MessageBox.Show(
                        "Da li ste sigurni da želite da obrišete ovaj zapis?",
                        "Potvrda brisanja",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    InsertAutomobili ins = new InsertAutomobili();
                    ins.DeleteAutomobili(Convert.ToInt32(txtSifra.Text));
                    status = false;
                    SnimanjeZavrseno?.Invoke(0);
                    this.Close();
                }            
            }
            else
            {
                MessageBox.Show("Selektuj red za brisanje.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
