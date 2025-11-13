using Syncfusion.Windows.Forms.Tools;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Saobracaj.Carinsko;

namespace Saobracaj.Izvoz
{
    public partial class frmProdajniNalogIzvoz : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        public frmProdajniNalogIzvoz()
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
         
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;



         


                foreach (Control control in Controls)
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
            // dgv.ColumnHeadersHeight = 30;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "NAdredjeni";
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[1].Visible = false;


            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kolicina";
            dataGridView1.Columns[2].Width = 150;

            DataGridViewColumn column4 = dataGridView1.Columns[3];
            dataGridView1.Columns[3].HeaderText = "JM";
            dataGridView1.Columns[3].Width = 140;



            DataGridViewColumn column5 = dataGridView1.Columns[4];
            dataGridView1.Columns[4].HeaderText = "Tip kontejnera";
            dataGridView1.Columns[4].Width = 240;

            DataGridViewColumn column6 = dataGridView1.Columns[5];
            dataGridView1.Columns[5].HeaderText = "Code";
            dataGridView1.Columns[5].Width = 240;


            DataGridViewColumn column7 = dataGridView1.Columns[6];
            dataGridView1.Columns[6].HeaderText = "Kvalitet kontejnera";
            dataGridView1.Columns[6].Width = 240;

            DataGridViewColumn column8 = dataGridView1.Columns[7];
            dataGridView1.Columns[7].HeaderText = "Referenca za fakturisanje";
            dataGridView1.Columns[7].Width = 240;



        }


        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);


            var bro = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
            var broAD = new SqlDataAdapter(bro, conn);
            var broDS = new DataSet();
            broAD.Fill(broDS);
            cboBrodar.DataSource = broDS.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";



            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            cboIzvoznik.DataSource = partDS.Tables[0];
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";

            var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal1AD = new SqlDataAdapter(nalogodavac1, conn);
            var nal1DS = new DataSet();
            nal1AD.Fill(nal1DS);
            cboNalogodavac.DataSource = nal1DS.Tables[0];
            cboNalogodavac.DisplayMember = "PaNaziv";
            cboNalogodavac.ValueMember = "PaSifra";

           

         



       
        



        }

        private void button21_Click(object sender, EventArgs e)
        {
            InsertProdajniNalogIzvoz ins = new InsertProdajniNalogIzvoz();
            ins.InsProdajniNalogIzvoz(tKorisnik , Convert.ToInt32(cboNalogodavac.SelectedValue), txtOpisPosla.Text, Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue), txtLink.Text, Convert.ToDateTime(dtpCutOffPort.Value), txtBoking.Text);
            UnesiStavke();
        }

        private void frmProdajniNalogIzvoz_Load(object sender, EventArgs e)
        {
            FillCombo();
            txtKorisnik.Text = tKorisnik;
            txtBrojDokumenta.Text = GetMaxID().ToString();
            DGVCombo();
            PodesiDatagridView(dataGridView1);
        }

        int GetMaxID()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("Select (Max(ID) + 1) as MAXID from ProdajniNalogIzvoz  " , con);
            SqlDataReader dr = cmd.ExecuteReader();




            while (dr.Read())
            {

                return Convert.ToInt32(dr["MAXID"].ToString());
               
            }
            con.Close();

            return 0;

        }


        private void DGVCombo()
        {
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            ID.HeaderText = "Stavka";
            ID.Name = "ID";
            ID.Width = 50;

            DataGridViewTextBoxColumn IDNadredjena = new DataGridViewTextBoxColumn();
            IDNadredjena.HeaderText = "ID";
            IDNadredjena.Name = "IDNadredjena";
            IDNadredjena.Width = 50;

            DataGridViewTextBoxColumn kolicina = new DataGridViewTextBoxColumn();
            kolicina.HeaderText = "kolicina";
            kolicina.Name = "Kolicina";
            kolicina.Width = 100;



             DataGridViewComboBoxColumn JM = new DataGridViewComboBoxColumn();
            JM.HeaderText = "JM";
            JM.Name = "JM";
            var query21 = "SELECT MeSifra FROM MerskeEnote";
            SqlConnection conn21 = new SqlConnection(connection);
            SqlDataAdapter da21 = new SqlDataAdapter(query21, conn21);
            System.Data.DataSet ds21 = new System.Data.DataSet();
            da21.Fill(ds21);
            JM.DataSource = ds21.Tables[0];
            JM.DisplayMember = "MeSifra";
            JM.ValueMember = "MeSifra";
            JM.Width = 100;





            DataGridViewComboBoxColumn tipkontejnera = new DataGridViewComboBoxColumn();
            tipkontejnera.HeaderText = "Tip kontejnera";
            tipkontejnera.Name = "Tip kontejnera";
            var query211 = "select ID, SkNaziv from TipKontenjera where Aktivan = 1 order by SkNaziv ";
            SqlConnection conn211 = new SqlConnection(connection);
            SqlDataAdapter da211 = new SqlDataAdapter(query211, conn211);
            System.Data.DataSet ds211 = new System.Data.DataSet();
            da211.Fill(ds211);
            tipkontejnera.DataSource = ds211.Tables[0];
            tipkontejnera.DisplayMember = "SkNaziv";
            tipkontejnera.ValueMember = "ID";
            tipkontejnera.Width = 150;


            DataGridViewTextBoxColumn code = new DataGridViewTextBoxColumn();
            code.HeaderText = "Code";
            code.Name = "Code";
            code.Width = 150;



            DataGridViewComboBoxColumn kvalitetkontejnera = new DataGridViewComboBoxColumn();
            kvalitetkontejnera.HeaderText = "Kvalitet kontejnera";
            kvalitetkontejnera.Name = "Kvalitet kontejnera";
            var query212 = "select ID, Naziv from uvKvalitetKontejnera order by ID";
            SqlConnection conn212 = new SqlConnection(connection);
            SqlDataAdapter da212 = new SqlDataAdapter(query212, conn212);
            System.Data.DataSet ds212 = new System.Data.DataSet();
            da212.Fill(ds212);
            kvalitetkontejnera.DataSource = ds212.Tables[0];
            kvalitetkontejnera.DisplayMember = "Naziv";
            kvalitetkontejnera.ValueMember = "ID";
            kvalitetkontejnera.Width = 150;


            DataGridViewTextBoxColumn refzafakt = new DataGridViewTextBoxColumn();
            refzafakt.HeaderText = "Referenca za fakturisanje";
            refzafakt.Name = "Referenca za fakturisanje";
            refzafakt.Width = 150;

          

            dataGridView1.Columns.Add(ID);
            dataGridView1.Columns.Add(IDNadredjena);
            dataGridView1.Columns.Add(kolicina);
            dataGridView1.Columns.Add(JM);
            dataGridView1.Columns.Add(tipkontejnera);
            dataGridView1.Columns.Add(code);
            dataGridView1.Columns.Add(kvalitetkontejnera);
            dataGridView1.Columns.Add(refzafakt);

        }

        private void RefreshDataGrid()
        {
            
                dataGridView1.AutoGenerateColumns = false;

                var select = "";
                select = @" SELECT [ProdajniNalogIzvozStavke].[ID]      ,[IDNAdredjenog]    ,[Kolicina],  [JM] ,ProdajniNalogIzvozStavke.[TipKontejnera], TipKontenjera.ISO   ,[KvalitetKontejnera]      ,[ReferencaZaFakturisanje] " +
  " FROM [dbo].[ProdajniNalogIzvozStavke] " +
 " inner join TipKontenjera on TipKontenjera.ID = ProdajniNalogIzvozStavke.TipKontejnera where IDNAdredjenog =" + txtBrojDokumenta.Text;

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
                   

                    dataGridView1.Rows[r].Cells[0].Value = ds.Tables[0].Rows[r].ItemArray[0];
                    dataGridView1.Rows[r].Cells[1].Value = ds.Tables[0].Rows[r].ItemArray[1];
                    dataGridView1.Rows[r].Cells[2].Value = ds.Tables[0].Rows[r].ItemArray[2];
                    dataGridView1.Rows[r].Cells[3].Value = ds.Tables[0].Rows[r].ItemArray[3];
                    dataGridView1.Rows[r].Cells[4].Value = ds.Tables[0].Rows[r].ItemArray[4];
                    dataGridView1.Rows[r].Cells[5].Value = ds.Tables[0].Rows[r].ItemArray[5];
                    dataGridView1.Rows[r].Cells[6].Value = ds.Tables[0].Rows[r].ItemArray[6];
                    dataGridView1.Rows[r].Cells[7].Value = ds.Tables[0].Rows[r].ItemArray[7];


            }

                PodesiDatagridView(dataGridView1);

            

        }

        private void UnesiStavke()
        {
            InsertProdajniNalogIzvoz ins = new InsertProdajniNalogIzvoz();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string postojeciID = "0";
                if (row != null)
                {
                    if (row.Cells[0].Value == null)
                    {
                        postojeciID = "0";

                    }
                    else
                    {
                        postojeciID = row.Cells[0].Value.ToString();
                    }



                }
                string brk = "";
                string paleta = "";
                string vrstp = "";
                string dim = "";
                if (row != null)
                {







                }
                // Convert.ToInt32(postojeciID),

                if (row.Cells[2].Value != null)
                {
                    ins.InsProdajniNalogIzvozStavke(Convert.ToInt32(txtBrojDokumenta.Text), Convert.ToDouble(row.Cells[2].Value),
                        row.Cells[3].Value.ToString(), Convert.ToInt32(row.Cells[4].Value), Convert.ToInt32(row.Cells[6].Value),
                       row.Cells[7].Value.ToString())
                       ;
                }


            }

            RefreshDataGrid();

        }



        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
