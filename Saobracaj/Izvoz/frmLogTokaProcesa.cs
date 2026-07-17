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

namespace Saobracaj.Izvoz
{
    public partial class frmLogTokaProcesa: Form
    {
        private int ID = 0;
        DataTable tabelaPodaci;

        public frmLogTokaProcesa()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        public frmLogTokaProcesa(int id, DataTable dt)
        {
            ID = id;
            tabelaPodaci = dt;
            InitializeComponent();
            ChangeTextBox();
        }

      
        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            //this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            //this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            //meniHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                //meniHeader.Visible = true;
                //meniHeader.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                //foreach (Control control in groupBox1.Controls)
                //{
                //    if (control is System.Windows.Forms.Button buttons)
                //    {

                //        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                //        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                //        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                //        buttons.FlatStyle = FlatStyle.Flat;
                //    }
                //}


                foreach (System.Windows.Forms.Control control in this.Controls)
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
                //meniHeader.Visible = false;
                //meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
        private void GenerisiTabeluLogova(DataTable dt)
        {
            // Očistimo panel od prethodne tabele ako je već bila iscrtana
            panelKontejner.Controls.Clear();

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Nema podataka za izabrani kontejner.");
                return;
            }

            // Kreiramo dinamički TableLayoutPanel
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Dock = DockStyle.Top;
            tlp.AutoSize = true;
            tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single; // Daje tanke crne linije kao na slici
            tlp.BackColor = Color.White;

            // Broj kolona je uvek 3 (Poruka, Datum/vreme, Lokacija)
            tlp.ColumnCount = 3;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Predefinisani deo poruke zauzima najviše
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F)); // Datum/vreme
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F)); // Lokacija

            // Broj redova = 1 (Glavno zaglavlje) + 1 (Podzaglavlje) + Broj redova iz baze
            int ukupanBrojRedova = 2 + dt.Rows.Count;
            tlp.RowCount = ukupanBrojRedova;

            // --- REZ 1: GLAVNO ZAGLAVLJE ("GENERISANA PORUKA") ---
            Label lblGlavniHeader = new Label();
            lblGlavniHeader.Text = "GENERISANA PORUKA";
            lblGlavniHeader.Font = new Font("Arial", 10, FontStyle.Bold);
            lblGlavniHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblGlavniHeader.Dock = DockStyle.Fill;
            lblGlavniHeader.Margin = new Padding(0);

   
            tlp.Controls.Add(lblGlavniHeader, 0, 0);
            tlp.SetColumnSpan(lblGlavniHeader, 3);

            // --- RED 2: PODZAGLAVLJE 
            string[] zaglavlja = { "Predefinisani deo poruke", "Datum/vreme", "Lokacija" };
            for (int i = 0; i < 3; i++)
            {
                Label lblSub = new Label();
                lblSub.Text = zaglavlja[i];
                lblSub.Font = new Font("Arial", 9, FontStyle.Bold);
                lblSub.TextAlign = ContentAlignment.MiddleLeft;
                lblSub.Dock = DockStyle.Fill;
                lblSub.Padding = new Padding(5, 2, 5, 2);
                tlp.Controls.Add(lblSub, i, 1);
            }

            // --- REDOVI SA PODACIMA IZ BAZE ---
            int trenutniRed = 2;
            foreach (DataRow row in dt.Rows)
            {
                // Uzimamo vrednosti iz baze
                string poruka = row["Poruka"].ToString();
                string vreme = ""; /*= row["DatumVreme"].ToString();*/
                if (row["DatumVreme"] != DBNull.Value)
                {
                    // Ako je objekat već DateTime tipa u DataTable-u
                    if (row["DatumVreme"] is DateTime dtObj)
                    {
                        vreme = dtObj.ToString("dd.MM.yyyy HH:mm");
                    }
                    else
                    {
                        // Ako stiže kao string (npr. "17.6.2026. 13:44:22")
                        string siroviDatum = row["DatumVreme"].ToString().Trim();
                        try
                        {
                            // Ovi formati pokrivaju i jednocifrene dane/mesece (d.M) i tačku nakon godine
                            string[] formati = { "d.M.yyyy. HH:mm:ss", "dd.MM.yyyy. HH:mm:ss", "d.M.yyyy. H:mm:ss" };

                            DateTime isparsovanDatum = DateTime.ParseExact(siroviDatum, formati, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                            vreme = isparsovanDatum.ToString("dd.MM.yyyy HH:mm");
                        }
                        catch
                        {
                            // Ako parsovanje ipak ne uspe, vrati sirovi string čisto da vidiš šta piše
                            vreme = siroviDatum;
                        }
                    }
                }
                string lokacija = row["Lokacija"].ToString();


                FontStyle stilFonta = FontStyle.Bold;
                if (poruka.ToLower().Contains("očekivano") || poruka.ToLower().Contains("ocekivano"))
                {
                    stilFonta = FontStyle.Italic;
                }

                // Kolona 1: Poruka
                Label lblPoruka = new Label();
                lblPoruka.Text = poruka;
                lblPoruka.Font = new Font("Arial", 9, stilFonta);
                lblPoruka.AutoSize = true;
                lblPoruka.TextAlign = ContentAlignment.MiddleLeft;
                lblPoruka.Dock = DockStyle.Fill;
                lblPoruka.Padding = new Padding(5, 5, 5, 5);
                tlp.Controls.Add(lblPoruka, 0, trenutniRed);

                // Kolona 2: Vreme
                Label lblVreme = new Label();
                lblVreme.Text = vreme;
                lblVreme.Font = new Font("Arial", 9, FontStyle.Regular); // Vreme i lokacija izgledaju obično na slici
                lblVreme.AutoSize = true;
                lblVreme.TextAlign = ContentAlignment.MiddleLeft;
                lblVreme.Dock = DockStyle.Fill;
                lblVreme.Padding = new Padding(5, 5, 5, 5);
                tlp.Controls.Add(lblVreme, 1, trenutniRed);

                // Kolona 3: Lokacija
                Label lblLokacija = new Label();
                lblLokacija.Text = lokacija;
                lblLokacija.Font = new Font("Arial", 9, FontStyle.Regular);
                lblLokacija.AutoSize = true;
                lblLokacija.TextAlign = ContentAlignment.MiddleLeft;
                lblLokacija.Dock = DockStyle.Fill;
                lblLokacija.Padding = new Padding(5, 5, 5, 5);
                tlp.Controls.Add(lblLokacija, 2, trenutniRed);

                trenutniRed++;
            }


            panelKontejner.Controls.Add(tlp);
        }

      
        private System.Data.DataTable VratiPodatkeZaLog(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            System.Data.DataTable dt = new System.Data.DataTable();


            using (SqlConnection con = new SqlConnection(s_connection))
            {

                string query = $@"
                        SELECT Poruka, Datum AS DatumVreme , Lokacija
                        FROM KontejnerLog
                        WHERE KontejnetID = {ID} order by ID asc ";

                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    dt.Load(dr);

                 
                }
                catch (Exception ex)
                {

                }
            }

            return dt;


        }

        private void txtBrojKontejnera_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmLogTokaProcesa_Load(object sender, EventArgs e)
        {
           
          //  DataTable dtLogovi = VratiPodatkeZaLog(ID);
            GenerisiTabeluLogova(tabelaPodaci);
            this.Text = "Log toka procesa";
        }
    
    }
}