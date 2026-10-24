using Microsoft.ReportingServices.Diagnostics.Internal;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testiranje.Sifarnici;

namespace Saobracaj.TrackModal.Sifarnici
{
    public partial class frmZone : Form
    {
        public static string code = "frmZone";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string niz = "";

        string KorisnikCene;
        bool status = false;
        public frmZone()
        {
            InitializeComponent();
            ChangeTextBox();
           
        }

        private void ChangeTextBox()
        {
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
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
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
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
            txtOznaka.Text = "";
            txtNaziv.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
           
            if (txtSifra.Text == "")
            {

                status = true;
            }


 int tmpAktivna = 0;
            if (chkAktivna.Checked == true)
            { tmpAktivna = 1; };

            int id = status ? 0 : HijerarhijaSkladista.CeoBroj(txtSifra.Text);
            string greska = HijerarhijaSkladista.ProveriOznaku(NivoSkladista.Zona, id, txtOznaka.Text)
                ?? HijerarhijaSkladista.ProveriAktivnost(NivoSkladista.Zona, id, 0, tmpAktivna == 1);
            if (greska != null)
            {
                MessageBox.Show(greska, "Zone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool bioAktivan = id > 0 && HijerarhijaSkladista.JeAktivan(NivoSkladista.Zona, id);
            if (status == true)
            {
                InsertZone ins = new InsertZone();
                ins.InsZone(txtOznaka.Text, txtNaziv.Text, Convert.ToInt32(cboTipZoneID.SelectedValue),tmpAktivna,  Convert.ToDateTime(DateTime.Now), Kor);
                status = false;
            }
            else
            {
                //int TipCenovnika ,int Komitent, double Cena , int VrstaManipulacije ,DateTime  Datum , string Korisnik
                InsertZone upd = new InsertZone();
                upd.UpdZone(Convert.ToInt32(txtSifra.Text), txtOznaka.Text, txtNaziv.Text, Convert.ToInt32(cboTipZoneID.SelectedValue), tmpAktivna, Convert.ToDateTime(DateTime.Now), Kor);
            }
            // promena aktivnosti zone prenosi se na lokacije, objekte, polja i pozicije
            HijerarhijaSkladista.PrenesiAktivnostPosleSnimanja(NivoSkladista.Zona, id, bioAktivan, tmpAktivna == 1, "Zone");
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            string greska = HijerarhijaSkladista.ProveriBrisanje(NivoSkladista.Zona, HijerarhijaSkladista.CeoBroj(txtSifra.Text));
            if (greska != null)
            {
                MessageBox.Show(greska, "Zone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da brišete zonu?", "Izbor", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                InsertZone upd = new InsertZone();
                upd.DeleteZone(Convert.ToInt32(txtSifra.Text));
                RefreshDataGrid();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void RefreshDataGrid()
        {
            var select = " Select Zone.ID, Zone.Oznaka, Zone.NAziv as Naziv, Zone.TipZoneID, TipZone.Naziv as TipZone, " +
                " CASE WHEN Zone.Aktivna > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Aktivna from Zone inner join TipZone on TipZone.ID = TipZoneID";
            SifarnikGrid.Napuni(gridGroupingControl1, HijerarhijaSkladista.Tabela(select));
            SifarnikGrid.Kolona(gridGroupingControl1, "ID", "ID", 50);
            SifarnikGrid.Kolona(gridGroupingControl1, "Oznaka", "Šifra", 80);
            SifarnikGrid.Kolona(gridGroupingControl1, "Naziv", "Naziv", 250);
            SifarnikGrid.Sakrij(gridGroupingControl1, "TipZoneID");
            SifarnikGrid.Kolona(gridGroupingControl1, "TipZone", "Tip zone", 240);
            SifarnikGrid.Kolona(gridGroupingControl1, "Aktivna", "Aktivna", 70);
        }

        private void VratiPodatke(string ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT Zone.ID, Zone.Oznaka, Zone.NAziv as Naziv, Zone.TipZoneID,  Zone.Aktivna from Zone where ID=" + txtSifra.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                // Convert.ToInt32(cboTipCenovnika.SelectedValue), Convert.ToInt32(cboKomitent.SelectedValue), Convert.ToDouble(txtCena.Text), Convert.ToInt32(cboVrstaManipulacije.SelectedValue), Convert.ToDateTime(DateTime.Now), KorisnikCene

                txtNaziv.Text = dr["Naziv"].ToString();
                txtOznaka.Text = dr["Oznaka"].ToString();
                cboTipZoneID.SelectedValue = Convert.ToInt32(dr["TipZoneID"].ToString());
                if (dr["Aktivna"].ToString() == "1")
                {
                    chkAktivna.Checked = true;
                }
                else
                {
                    chkAktivna.Checked = false;
                }
            }

            con.Close();
        }

        // izabrani red u gridu puni polja forme
        private void gridGroupingControl1_SelectedRecordsChanged(object sender, Syncfusion.Grouping.SelectedRecordsChangedEventArgs e)
        {
            int id = SifarnikGrid.IzabraniId(gridGroupingControl1);
            if (id == 0)
                return;
            txtSifra.Text = id.ToString();
            status = false;
            VratiPodatke(txtSifra.Text);
        }
        private void FillCombo()
        {

            SqlConnection conn = new SqlConnection(connection);

            var dir = "Select ID,Naziv from TipZone order by ID";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new System.Data.DataSet();
            dirAD.Fill(dirDS);
            cboTipZoneID.DataSource = dirDS.Tables[0];
            cboTipZoneID.DisplayMember = "Naziv";
            cboTipZoneID.ValueMember = "ID";
        }

        private void frmZone_Load(object sender, EventArgs e)
        {

            FillCombo();
            RefreshDataGrid();
        }

        // Aktivne / Neaktivne: izabrane zone i sve njihove lokacije, objekti, polja i pozicije
        private void button23_Click(object sender, EventArgs e)
        {
            HijerarhijaSkladista.PromeniAktivnostIzabranih(gridGroupingControl1, NivoSkladista.Zona, true, "Zone");
            RefreshDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HijerarhijaSkladista.PromeniAktivnostIzabranih(gridGroupingControl1, NivoSkladista.Zona, false, "Zone");
            RefreshDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }
    }
}
