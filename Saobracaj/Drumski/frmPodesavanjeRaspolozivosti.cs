using Saobracaj.Dokumenta;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmPodesavanjeRaspolozivosti: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        public frmPodesavanjeRaspolozivosti()
        {
            InitializeComponent();
            ChangeTextBox();
            this.Text = "Tehnička dostupnost vozila";
            ucitajComboBox();
            RefreshDataGRid();
        }

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            //panelHeader.Visible = false;


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                //panelHeader.Visible = true;
                //meniHeader.Visible = false;
                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                //panelHeader.Visible = false;
                //meniHeader.Visible = true;
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


        private void ucitajComboBox()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            //var partner = "Select PaSifra,PaNaziv From Partnerji  WHERE DrumskiPrevoz = 1 AND ISNULL(Kamioner, 0) = 1 order by PaNaziv";
            // zakomentarisan Kamioner 
            var partner = "Select PaSifra,PaNaziv From Partnerji  WHERE DrumskiPrevoz = 1 order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, s_connection);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            DataTable dt = partDS.Tables[0];

            // Kreiraj novi red sa praznim tekstom i ID -1
            DataRow prazanRed = dt.NewRow();
            prazanRed["PaNaziv"] = "";
            prazanRed["PaSifra"] = -1;

            // Ubaci kao prvi red
            dt.Rows.InsertAt(prazanRed, 0);
            cboPrevoznikFilter.DataSource = partDS.Tables[0];
            cboPrevoznikFilter.DisplayMember = "PaNaziv";
            cboPrevoznikFilter.ValueMember = "PaSifra";
        }

        private void RefreshDataGRid()
        {
            List<string> statusi = new List<string>();
            SqlConnection conn = new SqlConnection(connection);
            using (conn)
            {
                conn.Open();
              
                string condition = "";

                if (cboPrevoznikFilter.SelectedValue != null && int.TryParse(cboPrevoznikFilter.SelectedValue.ToString(), out int parsedPrevoznik) && parsedPrevoznik > -1)
                    condition = condition + " AND  a.PartnerID = " + parsedPrevoznik;

                var select = $@" SELECT a.ID AS ID,
                     LTRIM(RTRIM(vv.Naziv)) AS TipVozila,
                     LTRIM(RTRIM(p.PaNaziv)) AS Prevoznik,
                     LTRIM(RTRIM(a.Vozac)) AS Vozac,
                     a.RegBr,
                     LTRIM(RTRIM(a.BrojTelefona)) AS BrojTelefona,
                     LTRIM(RTRIM(a.LicnaKarta)) AS LicnaKarta,
                     a.VlasnistvoLegeta AS TipTransporta,
                     CASE
                         WHEN atp.VoziloID IS NOT NULL THEN 'Tehnički Problem'
                         ELSE 'Raspoloživ' 
                     END AS Raspoloziv
                     FROM Automobili a
                     INNER JOIN Delavci d ON d.DeSifra = a.Zaposleni
                     INNER JOIN Delavci dk ON dk.DeSifra = a.KreiraoZaposleni
                     LEFT JOIN VrstaVozila vv ON a.VlasnistvoLegeta = vv.ID
                     LEFT JOIN Partnerji p ON a.PartnerID = p.PaSifra
                     LEFT JOIN AutomobiliTehnickiProblem atp ON
                     a.ID = atp.VoziloID AND CONVERT(date, atp.Datum) = CONVERT(date, GETDATE())

                     WHERE a.VoziloDrumskog = 1 {condition}";
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(select, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
            PodesiDatagridView(dataGridView1);

            int totalWidth = dataGridView1.Width;

            double[] proporcije = { 0.09, 0.15, 0.15, 0.15, 0.11, 0.12, 0.15 };
            string[] headers = { "Tip vozila", "Prevoznik", "Vozač", "Reg br", "Broj telefona", "Lična karta", "Raspoloživost" };

            // Postavi kolone (počevši od indeksa 1 jer je [0] ID)
            for (int i = 0; i < proporcije.Length; i++)
            {
                int index = i + 1;
                if (index < dataGridView1.Columns.Count)
                {
                    dataGridView1.Columns[index].HeaderText = headers[i];
                    dataGridView1.Columns[index].Width = (int)(totalWidth * proporcije[i]);
                    dataGridView1.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }
            }

            // sakrivene kolone 
            if (dataGridView1.Columns.Contains("TipTransporta"))
                dataGridView1.Columns["TipTransporta"].Visible = false;
            if (dataGridView1.Columns.Contains("ID"))
                dataGridView1.Columns["ID"].Visible = false;

            //  Poslednja kolona popunjava preostali prostor
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertAutomobili ins = new InsertAutomobili();
            List<int> selektovaniIdjevi = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells["ID"].Value != null)
                {
                    int voziloId = Convert.ToInt32(row.Cells["ID"].Value);
                    selektovaniIdjevi.Add(voziloId);
                }
            }

            if (selektovaniIdjevi.Count == 0)
            {
                MessageBox.Show("Nije izabrano nijedno vozilo.");
                return;
            }

            string ids = string.Join(",", selektovaniIdjevi);
            var parametri = selektovaniIdjevi.Select((id, i) => $"@id{i}").ToList();
            string inClause = string.Join(",", parametri);

            string query = $@"
                SELECT VoziloID
                FROM AutomobiliTehnickiProblem
                WHERE CAST(Datum AS date) = CAST(GETDATE() AS date)
                AND VoziloID IN ({inClause})
            ";

            List<int> vecPostoje = new List<int>();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    for (int i = 0; i < selektovaniIdjevi.Count; i++)
                    {
                        cmd.Parameters.AddWithValue($"@id{i}", selektovaniIdjevi[i]);
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vecPostoje.Add(Convert.ToInt32(reader["VoziloID"]));
                        }
                    }
                }
            }

            // 3. Nađi ID-jeve koji ne postoje u bazi
            var noviIdjevi = selektovaniIdjevi.Except(vecPostoje).ToList();

            if (noviIdjevi.Count == 0)
            {
                MessageBox.Show("Svi izabrani zapisi su već nedostupni za današnji datum.");
                return;
            }

            // 4. Pozovi Insert za svaki novi zapis
            foreach (int voziloId in noviIdjevi)
            {
                ins.InsAutomobiliTehnickiProblem(voziloId);
            }
            if(noviIdjevi.Count > 1 )
                MessageBox.Show("Izabrana vozila su označena kao vozila sa tehničkim problemom.");
            else
                MessageBox.Show("Izabrano vozilo je označena kao vozilo sa tehničkim problemom.");
            RefreshDataGRid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertAutomobili ins = new InsertAutomobili();
            List<int> selektovaniIdjevi = new List<int>();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells["ID"].Value != null)
                {
                    int voziloId = Convert.ToInt32(row.Cells["ID"].Value);
                    selektovaniIdjevi.Add(voziloId);
                }
            }

            if (selektovaniIdjevi.Count == 0)
            {
                MessageBox.Show("Nije izabrano nijedno vozilo.");
                return;
            }

            foreach (int voziloId in selektovaniIdjevi)
            {
                ins.DelAutomobiliTehnickiProblem(voziloId);
               
            }
            if (selektovaniIdjevi.Count > 1)
                MessageBox.Show("Izabrana vozila su označena kao raspoloživa.");
            else
                MessageBox.Show("Izabrano vozilo je označena kao raspoloživo.");
            RefreshDataGRid();

        }
    }
}
