using Microsoft.ReportingServices.Diagnostics.Internal;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testiranje.Sifarnici;

namespace Saobracaj.Izvoz
{
    public partial class Vaganje : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        string brojKontejnera;
        string  Vozilo, Vozac;
        int kontID, vrsta;
        bool status = false;

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;
            this.ControlBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
              //  meniHeader.Visible = false;
                panelHeader.Visible = true;
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
              //  meniHeader.Visible = true;
                panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
        public Vaganje()
        {
            InitializeComponent();
            ChangeTextBox();
        }
        public Vaganje(string kontejner,int KontID, string VoziloK, string Vozac)
        {
            InitializeComponent();
            ChangeTextBox();
            brojKontejnera = kontejner;
           
            kontID = KontID;
            Vozilo = VoziloK;
        }
        

        private void Vaganje_Load(object sender, EventArgs e)
        {
            txtKontejner.Text = brojKontejnera;
            cbovrstakontejnera.SelectedValue = vrsta;
            txtKontID.Text = kontID.ToString();

            SqlConnection conn = new SqlConnection(connection);

            var TipKontenjera = "Select ID,SkNaziv from TipKontenjera order by ID";
            var daTK = new SqlDataAdapter(TipKontenjera, conn);
            var daDS = new System.Data.DataSet();
            daTK.Fill(daDS);
            cbovrstakontejnera.DataSource = daDS.Tables[0];
            cbovrstakontejnera.DisplayMember = "SkNaziv";
            cbovrstakontejnera.ValueMember = "ID";


            VratiVrstuKontejnera(txtKontID.Text);

            FillGV();
        }

        private void VratiVrstuKontejnera(string KontID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" select VrstaKontejnera, BrojKontejnera, Vozilo from IzvozKonacna " +
             " where IzvozKonacna.ID = " + KontID, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
             
                cbovrstakontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                txtKontejner.Text = dr["BrojKontejnera"].ToString();
                txtVozilo.Text = dr["Vozilo"].ToString();
            }

            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select ID From Vaganje Where IzvozKonacnaID='" + txtKontID.Text+"'", con);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                MessageBox.Show("Kontejner je već vagan!");
                return;
            }
            else
            {
                InsertIzvoz ins = new InsertIzvoz();
                ins.InsVaganje(Convert.ToInt32(txtKontID.Text), txtVozilo.Text, txtKontejner.Text.ToString().TrimEnd(),  txtVagarskaPotvrda.Text.ToString().TrimEnd(), Convert.ToDecimal(txtBruto.Text),
                    Convert.ToDecimal(txtTara.Text), Convert.ToDecimal(txtNeto.Text), Convert.ToDateTime(dtpDatumMerenja.Value), Sifarnici.frmLogovanje.user);
            }
            con.Close();
            FillGV();
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


        }
        private void FillGV()
        {
            var select = "select Vaganje.ID, Kamion, VagarskaPotvrdaBroj, Bruto, Vaganje.Tara, Neto, DatumMerenja, IzvozKonacnaID, Korisnik,\r\nIZvozKonacna.BrojKontejnera, IzvozKonacna.VrstaKontejnera, IzvozKonacna.Cirada from Vaganje\r\ninner join IzvozKonacna on IzvozKonacna.ID = IzvozKonacnaID";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);
            /*
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

            */
        }


        private void txtNeto_MouseEnter(object sender, EventArgs e)
        {
            if(txtBruto.Text!="" && txtTara.Text != "")
            {
                decimal neto;
                neto = Convert.ToDecimal(txtBruto.Text) - Convert.ToDecimal(txtTara.Text);
                txtNeto.Text = neto.ToString();
            }
        }
        int ID;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    ID = Convert.ToInt32(row.Cells["ID"].Value.ToString());
                    txtID.Text = row.Cells["ID"].Value.ToString();
                    VratiPodatke(txtID.Text);

                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {


            if (status == true)
            {
                InsertIzvoz ins = new InsertIzvoz();
                ins.InsVaganje(Convert.ToInt32(txtKontID.Text), txtVozilo.Text, txtKontejner.Text.ToString().TrimEnd(), txtVagarskaPotvrda.Text.ToString().TrimEnd(), Convert.ToDecimal(txtBruto.Text),
                    Convert.ToDecimal(txtTara.Text), Convert.ToDecimal(txtNeto.Text), Convert.ToDateTime(dtpDatumMerenja.Value), Sifarnici.frmLogovanje.user);
                status = false;
            }
            else
            {
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                InsertIzvoz upd = new InsertIzvoz();
                upd.UpdVaganje(Convert.ToInt32(txtID.Text), Convert.ToInt32(txtKontID.Text), txtVozilo.Text, txtKontejner.Text.ToString().TrimEnd(), txtVagarskaPotvrda.Text.ToString().TrimEnd(), Convert.ToDecimal(txtBruto.Text),
                    Convert.ToDecimal(txtTara.Text), Convert.ToDecimal(txtNeto.Text), Convert.ToDateTime(dtpDatumMerenja.Value), Sifarnici.frmLogovanje.user);
            }
            if (chkCirada.Checked == true)
            {
                InsertIzvoz upd = new InsertIzvoz();
                upd.UpdateIzvozKonacnaCirada(Convert.ToInt32(txtKontID.Text), Convert.ToDecimal(txtNeto.Text));
            }
            else
            {
                InsertIzvoz upd = new InsertIzvoz();
                upd.UpdateIzvozKonacnaPlatforma(Convert.ToInt32(txtKontID.Text), Convert.ToDecimal(txtNeto.Text));
            }



            FillGV();


            /*
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select ID From Vaganje Where IzvozKonacnaID='" + txtKontID.Text + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                MessageBox.Show("Kontejner je već vagan!");
                return;
            }
            else
            {
               
            }
            con.Close();
           */
        }

        private void button23_Click(object sender, EventArgs e)
        {
            int tip = 0;
            if (chkCirada.Checked == true)
            { tip = 1; }
            IzvestajVaganja frm = new IzvestajVaganja(ID,tip);
            frm.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            status = true;
            txtID.Text = "";
            txtID.Enabled = false;
           // txtNaziv.Text = "";
        }

        private void txtBruto_Leave(object sender, EventArgs e)
        {
            txtNeto.Value = txtBruto.Value - txtTara.Value;
        }

        private void VratiPodatke(string ID)
        {
         
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand(" select Vaganje.ID, Kamion, VagarskaPotvrdaBroj, Bruto, Vaganje.Tara, Neto, DatumMerenja, IzvozKonacnaID, Korisnik,\r\nIZvozKonacna.BrojKontejnera, IzvozKonacna.VrstaKontejnera, IzvozKonacna.Cirada, IzvozKonacna.Taraz from Vaganje inner join IzvozKonacna on IzvozKonacna.ID = IzvozKonacnaID " +
                 " where Vaganje.ID = " + ID, con);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    cbovrstakontejnera.SelectedValue = Convert.ToInt32(dr["VrstaKontejnera"].ToString());
                    txtKontID.Text = dr["IzvozKonacnaID"].ToString();
                    txtKontejner.Text = dr["BrojKontejnera"].ToString();
                    txtVagarskaPotvrda.Text = dr["VagarskaPotvrdaBroj"].ToString();
                    txtVozilo.Text = dr["Kamion"].ToString();
                    txtBruto.Value = Convert.ToDecimal(dr["Bruto"].ToString());
                    txtNeto.Value = Convert.ToDecimal(dr["Neto"].ToString());
                    txtTara.Value = Convert.ToDecimal(dr["Tara"].ToString());
                    dtpDatumMerenja.Value = Convert.ToDateTime(dr["DatumMerenja"].ToString());
                    txtTaraZ.Value = Convert.ToDecimal(dr["TaraZ"].ToString());
                if (dr["Cirada"].ToString() == "1")
                {
                    chkCirada.Checked = true;
                    chkPlatforma.Checked = false;
                }
                else
                {
                    chkCirada.Checked = true;
                    chkPlatforma.Checked = false;
                }
            }

                con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tip = 0;
            if (chkCirada.Checked == true)
            { tip = 1; }
            IzvestajVaganja frm = new IzvestajVaganja(ID, tip);
            frm.Show();
        }
    }
    
}
