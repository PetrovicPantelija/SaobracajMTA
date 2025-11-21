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

namespace Saobracaj.Drumski
{
    public partial class frmRaspolozivostVozila: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private readonly List<int> _tipoviIn;
        private readonly List<int> _tipoviNotIn;
        public frmRaspolozivostVozila(List<int> tipoviIn, List<int> tipoviNotIn)
        {
            _tipoviIn = tipoviIn;
            _tipoviNotIn = tipoviNotIn;
            InitializeComponent();
            chkDatumD.Checked = true;
            ChangeTextBox();
            UcitajFiltere();
            RefreshDataGrid1();
            this.Text = "Provera raspoloživosti vozila";
        }

        private void ChangeTextBox()
        {
            //panelHeader.Visible = false;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                //panelHeader.Visible = true;
                //meniHeader.Visible = false;
                this.BackColor = Color.White;
                //this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                //this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
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

        private void UcitajFiltere()
        {
;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            var partner = "Select PaSifra,PaNaziv From Partnerji WHERE DrumskiPrevoz = 1 order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, s_connection);
            var partDS = new DataSet();
            partAD.Fill(partDS);

            DataTable dt1 = partDS.Tables[0];

            // Kreiraj novi red sa praznim tekstom i ID -1
            DataRow prazanRed1 = dt1.NewRow();
            prazanRed1["PaNaziv"] = "All";
            prazanRed1["PaSifra"] = -1;

            // Ubaci kao prvi red
            dt1.Rows.InsertAt(prazanRed1, 0);

            cboPrevoznik.DataSource = dt1;
            cboPrevoznik.DisplayMember = "PaNaziv";
            cboPrevoznik.ValueMember = "PaSifra";

            string reg = "SELECT ID, Marka, RegBr, Vozac " +
                       "FROM Automobili " +
                       "WHERE VoziloDrumskog = 1";


            var regAD = new SqlDataAdapter(reg, s_connection);
            var regDS = new DataSet();
            regAD.Fill(regDS);

            DataTable dt2 = regDS.Tables[0];

            // Kreiraj novi red sa praznim tekstom i ID -1
            DataRow prazanRed2 = dt2.NewRow();
            prazanRed2["RegBr"] = "All";
            prazanRed2["ID"] = -1;

            // Ubaci kao prvi red
            dt2.Rows.InsertAt(prazanRed2, 0);

            cboRegistracija.DataSource = dt2;
            cboRegistracija.DisplayMember = "RegBr";
            cboRegistracija.ValueMember = "ID";
        }


        private void RefreshDataGrid1()
        {
            List<string> statusi = new List<string>();
            SqlConnection conn = new SqlConnection(connection);
            using (conn)
            {
                conn.Open();

                // 1. Učitaj status vrednosti iz SistemskePostavke
                SqlCommand cmd1 = new SqlCommand("SELECT Vrednost FROM SistemskePostavke WHERE Naziv LIKE 'StatusKamiona%'", conn);
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statusi.Add(reader.GetString(0));
                    }
                }

                // 2. Priprema statusa za upit
                string statusiZaUpit = string.Join(",", statusi
                    .Select(s => s.Trim())
                    .Where(s => int.TryParse(s, out _)));
                string condition = "";

                if (cboPrevoznik.SelectedValue != null && int.TryParse(cboPrevoznik.SelectedValue.ToString(), out int parsedPrevoznik) && parsedPrevoznik > -1)
                    condition = condition + " AND  a.PartnerID = " + parsedPrevoznik;
                if (cboRegistracija.SelectedValue != null && int.TryParse(cboRegistracija.SelectedValue.ToString(), out int parsedRegistracija) && parsedRegistracija > -1)
                {
                    string regBr = cboRegistracija.Text.Trim();
                    condition += " AND a.RegBr LIKE '" + regBr + "'";
                }

                //  LOGIKA ZA R/N
                // "raspoloživ" = kamion nije vezan za nalog sa DatumPakovanja = sutra
                // "neraspoloživ" = kamion jeste vezan za nalog sa DatumPakovanja = sutra

                bool prikaziZaDanas = chkDatumD.Checked;
                bool prikaziZaSutra = chkDatumS.Checked;

                // 1. Određivanje datuma za proveru (DatumPakovanja)
                string datumZaProveru = "";
                if (prikaziZaDanas)
                {
                    // CONVERT(date, GETDATE())
                    datumZaProveru = "GETDATE()";
                }
                else if (prikaziZaSutra)
                {
                    // CONVERT(date, DATEADD(day, 1, GETDATE()))
                    datumZaProveru = "DATEADD(day, 1, GETDATE())";
                }
                // Ako nije čekiran ni 'Danas' ni 'Sutra', onda se logika raspoloživosti ne primjenjuje.

                string joinLogika = "";

                // Logika se primjenjuje samo ako je odabran datum (Danas ili Sutra)
                if ( !string.IsNullOrEmpty(datumZaProveru))
                {
                    // Baza upita za provjeru raspoloživosti/neraspoloživosti
                    string subQueryNaloga = $@"
                        SELECT DISTINCT ISNULL(KamionID, 0) AS KamionID
                        FROM RadniNalogDrumski
                        WHERE CONVERT(date, DtPreuzimanjaPraznogKontejnera) = CONVERT(date, {datumZaProveru}) ";

                    // Subquery za vozila koja su u tehničkom problemu (neraspoloživa zbog kvara)
                    string subQueryKvarova = $@"
                        SELECT DISTINCT ISNULL(VoziloID, 0) AS VoziloID
                        FROM AutomobiliTehnickiProblem
                        WHERE CAST(Datum AS date) = CAST({datumZaProveru} AS date)";
           
                        // Samo raspoloživa vozila – koja NISU ni u nalogu ni u kvaru
                        joinLogika = $@"
                            AND a.ID NOT IN ({subQueryNaloga})
                            AND a.ID NOT IN ({subQueryKvarova}) ";
                   
                    // 3. Konačan SELECT
                }
                string uslovTipVozila = "";

                if (_tipoviIn?.Any() == true)
                {
                    string lista = string.Join(",", _tipoviIn);
                    uslovTipVozila += $" AND VlasnistvoLegeta IN ({lista}) ";
                }

                if (_tipoviNotIn?.Any() == true)
                {
                    string lista = string.Join(",", _tipoviNotIn);
                    uslovTipVozila += $" AND VlasnistvoLegeta NOT IN ({lista}) ";
                }
                var select = $@" SELECT a.ID,  LTRIM(RTRIM(vv.Naziv)) AS TipVozila, a.VlasnistvoLegeta as TipTransporta, LTRIM(RTRIM(p.PaNaziv)) AS Prevoznik, LTRIM(RTRIM(a.Vozac)) AS Vozac, LTRIM(RTRIM(a.RegBr)) AS RegBr   , LTRIM(RTRIM(BrojTelefona)) as TelefonVozaca
                            FROM Automobili a 
                            LEFT JOIN VrstaVozila vv on a.VlasnistvoLegeta = vv.ID 
                            LEFT JOIN Partnerji p on a.PartnerID = p.PaSifra  
                            LEFT JOIN AutomobiliTehnickiProblem ap ON  a.ID = ap.VoziloID AND CAST(ap.Datum AS date) = CAST({datumZaProveru} AS date)
                            WHERE VoziloDrumskog = 1  {uslovTipVozila}  {condition}  {joinLogika}";

                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

            }
            PodesiDatagridView(dataGridView1);

            if (dataGridView1.Columns.Contains("ID"))
            {
                dataGridView1.Columns["ID"].Visible = false;
            }
            if (dataGridView1.Columns.Contains("TipTransporta"))
            {
                dataGridView1.Columns["TipTransporta"].Visible = false;
            }
        }

        private void ChkDatum_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox s = (CheckBox)sender;

            // 1.Ako se jedan čeka, drugi se automatski odčeka.
            if (s == chkDatumD && chkDatumD.Checked)
            {
                chkDatumS.Checked = false;
            }
            else if (s == chkDatumS && chkDatumS.Checked)
            {
                chkDatumD.Checked = false;
            }

            // 2.Sprečava da oba budu odčekirana.
            if (s.Checked == false)
            {

                if (s == chkDatumD && chkDatumS.Checked == false)
                {
                    chkDatumD.Checked = true;
                    return;
                }

                else if (s == chkDatumS && chkDatumD.Checked == false)
                {
                    chkDatumS.Checked = true;
                    return;
                }
            }

            RefreshDataGrid1();
        }

        private void btnFiltriraj_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }
    }


}
