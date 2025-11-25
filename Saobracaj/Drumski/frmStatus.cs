using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Saobracaj.Drumski
{
    public partial class frmStatus: Form
    {
        private string upozorenjeTehnickiNeispravni = "";
        private DataTable mainTable;
        private bool cellClickHandlerAttached = false;
        private Form aktivnaFormaPregleda;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private readonly List<int> _tipoviIn;
        private readonly List<int> _tipoviNotIn;

        public frmStatus(List<int> tipoviIn, List<int> tipoviNotIn)
        {
            InitializeComponent();
            ChangeTextBox();
            this.Text = "Status";
            _tipoviIn = tipoviIn;
            _tipoviNotIn = tipoviNotIn;
            RefreshDataGrid3();
            if (!string.IsNullOrWhiteSpace(upozorenjeTehnickiNeispravni))
            {
                MessageBox.Show(
                    "Upozorenje!\n\n" + upozorenjeTehnickiNeispravni,
                    "Upareni tehnički neispravni kamioni",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
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

        private void RefreshDataGrid3()
        {
            SqlConnection conn = new SqlConnection(connection);
            List<string> statusi = new List<string>();
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
               

                ////3.  Određivanje datuma za proveru (DatumIstovara)
                //bool prikaziZaDanas = chkDatumD.Checked;
                //bool prikaziZaSutra = chkDatumS.Checked;


                string datumZaProveru = "";
                datumZaProveru = "GETDATE()";
                //if (prikaziZaDanas)
                //{
                //    // CONVERT(date, GETDATE())
                //    datumZaProveru = "GETDATE()";
                //}
                //else if (prikaziZaSutra)
                //{
                //    // CONVERT(date, DATEADD(day, 1, GETDATE()))
                //    datumZaProveru = "DATEADD(day, 1, GETDATE())";
                //}

                string uslovTipVozila = "1 = 1 ";

                if (_tipoviIn?.Any() == true)
                {
                    string lista = string.Join(",", _tipoviIn);
                    uslovTipVozila += $" AND x.VoziloDrumskog IN ({lista}) ";
                }

                if (_tipoviNotIn?.Any() == true)
                {
                    string lista = string.Join(",", _tipoviNotIn);
                    uslovTipVozila += $" AND x.VoziloDrumskog NOT IN ({lista}) ";
                }

                var select = $@"
                            SELECT   
                                -- Agregirana kolona (Spojeni ID-jevi)
                                x.ID,
                                LTRIM(RTRIM( x.Nalogodavac)) AS Nalogodavac, 
                                x.Relacija,
                                x.DatumIstovara, 
                                LTRIM(RTRIM(x.Prevoznik)) AS Prevoznik, 
                                LTRIM(RTRIM(x.Vozac)) AS Vozac,
                                LTRIM(RTRIM(x.Kamion)) AS Kamion, 
                                x.VoziloDrumskog,
                                x.NalogID, 
                                x.PoslataNajava,
                                x.NajavuPoslao, 
                                x.SlanjeNajave, 
                                x.Status AS Status,
                                x.StatusID,
                                x.TehnickiNeispravan

                            FROM 
                            (
                                -- Deo 1 (Izvoz)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac, 
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       au.VoziloDrumskog,
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                INNER JOIN Izvoz i ON i.ID = rn.KontejnerID 
                                LEFT JOIN MestaUtovara mu on i.MesoUtovara = mu.ID 
                                LEFT JOIN MestaUtovara mi on rn.MestoIstovara = mi.ID
                                LEFT JOIN Partnerji pa ON pa.PaSifra = i.Klijent3 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                LEFT JOIN AutomobiliTehnickiProblem ap ON au.ID = ap.VoziloID AND CAST(ap.Datum AS date) = CAST({datumZaProveru} AS date)
                                WHERE rn.Uvoz = 0 AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID IS NOT NULL AND rn.KamionID != 0 
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DtPreuzimanjaPraznogKontejnera) = CONVERT(date, {datumZaProveru} )
    
                                UNION ALL 
                                -- Deo 2 (IzvozKonacna)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       au.VoziloDrumskog,
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                INNER JOIN IzvozKonacna ik ON ik.ID = rn.KontejnerID 
                                LEFT join MestaUtovara mu on ik.MesoUtovara = mu.ID
                                LEFT join MestaUtovara mi on rn.MestoIstovara = mi.ID
                                LEFT JOIN Partnerji pa ON pa.PaSifra = ik.Klijent3 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                LEFT JOIN AutomobiliTehnickiProblem ap ON au.ID = ap.VoziloID AND CAST(ap.Datum AS date) = CAST({datumZaProveru} AS date)
                                WHERE rn.Uvoz = 0 AND rn.KamionID IS NOT NULL AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID != 0 
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DtPreuzimanjaPraznogKontejnera) = CONVERT(date, {datumZaProveru} )
    
                                UNION ALL 
                                -- Deo 3 (UvozKonacna)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       au.VoziloDrumskog,
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                INNER JOIN UvozKonacna uk ON uk.ID = rn.KontejnerID 
                                LEFT JOIN MestaUtovara mu on  rn.MestoUtovara = mu.ID
                                LEFT JOIN MestaUtovara mi on  uk.MestoIstovara = mi.ID 
                                LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                LEFT JOIN AutomobiliTehnickiProblem ap ON au.ID = ap.VoziloID AND CAST(ap.Datum AS date) = CAST({datumZaProveru} AS date)
                                WHERE rn.Uvoz = 1 AND rn.KamionID IS NOT NULL AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID != 0 
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DtPreuzimanjaPraznogKontejnera) = CONVERT(date, {datumZaProveru} )
    
                                UNION ALL 
                                -- Deo 4 (Uvoz)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac, 
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija, 
                                       au.Vozac,
                                       au.RegBr AS Kamion,
                                       au.VoziloDrumskog, 
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                INNER JOIN Uvoz u ON u.ID = rn.KontejnerID 
                                LEFT JOIN Partnerji pa ON pa.PaSifra = u.Nalogodavac3 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                LEFT JOIN MestaUtovara mu on  rn.MestoUtovara = mu.ID
                                LEFT JOIN MestaUtovara mi on  u.MestoIstovara = mi.ID
                                LEFT JOIN AutomobiliTehnickiProblem ap ON au.ID = ap.VoziloID AND CAST(ap.Datum AS date) = CAST({datumZaProveru} AS date)
                                WHERE rn.Uvoz = 1 AND rn.KamionID IS NOT NULL AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID != 0 
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DtPreuzimanjaPraznogKontejnera) = CONVERT(date, {datumZaProveru} )
    
                                UNION ALL 
                                -- Deo 5 (Ostali drumski)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       au.VoziloDrumskog,
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                LEFT JOIN Partnerji pa ON pa.PaSifra = rn.Klijent 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN MestaUtovara mu on  rn.MestoUtovara = mu.ID
                                LEFT JOIN MestaUtovara mi on  rn.MestoIstovara = mi.ID
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                LEFT JOIN AutomobiliTehnickiProblem ap ON au.ID = ap.VoziloID AND CAST(ap.Datum AS date) = CAST({datumZaProveru} AS date)
                                WHERE rn.Uvoz IN (2, 3, 4, 5) AND rn.NalogID > 0 AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID IS NOT NULL AND rn.KamionID != 0
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DtPreuzimanjaPraznogKontejnera) = CONVERT(date, {datumZaProveru} )

                            ) AS x
                            WHERE  {uslovTipVozila} 
                            GROUP BY 
                                x.ID,
                                x.Nalogodavac, 
                                x.Relacija,
                                x.Vozac,
                                x.Kamion, 
                                x.VoziloDrumskog,
                                x.DatumIstovara, 
                                x.NalogID, 
                                x.Prevoznik, 
                                x.PoslataNajava,
                                x.NajavuPoslao, 
                                x.SlanjeNajave, 
                                x.Status,
                                x.StatusID,
                                x.TehnickiNeispravan

                            ORDER BY 
                                x.NalogID DESC
";
                var da = new SqlDataAdapter(select, conn);
                var ds = new DataSet();
                da.Fill(ds);
                mainTable = ds.Tables[0];
                dataGridView3.ReadOnly = true;
                // Napuni status listu
                var stv = "SELECT ID, LTRIM(RTRIM(Naziv)) AS Naziv FROM StatusVozila ORDER BY Naziv";
                var stvAD = new SqlDataAdapter(stv, conn);
                var stvDS = new DataSet();
                stvAD.Fill(stvDS);
                var dtStatus = stvDS.Tables[0];

                // Očisti null vrednosti
                foreach (DataRow row in mainTable.Rows)
                {
                    if (row.IsNull("Status"))
                        row["Status"] = DBNull.Value;
                }
                // Veži glavnu tabelu
                dataGridView3.DataSource = mainTable;


                DodajDugmadKolonu();
                if (!cellClickHandlerAttached)
                {
                    dataGridView3.CellClick += dataGridView3_CellContentClick;
                    cellClickHandlerAttached = true;
                }



                // Napravi ComboBox kolonu za "Status"
                DataGridViewComboBoxColumn cmbStatus = new DataGridViewComboBoxColumn();
                cmbStatus.HeaderText = "Status";
                cmbStatus.Name = "Status";                // isto ime kao u DataTable
                cmbStatus.DataPropertyName = "Status";    // kolona iz mainTable
                cmbStatus.DataSource = dtStatus;          //  tabela StatusVozila
                cmbStatus.DisplayMember = "Naziv";
                cmbStatus.ValueMember = "ID";
                cmbStatus.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cmbStatus.FlatStyle = FlatStyle.Flat;

                if (dataGridView3.Columns.Contains("Status"))
                    dataGridView3.Columns.Remove("Status");

                // Dodaj ComboBox kolonu na kraj 
                dataGridView3.Columns.Add(cmbStatus);

                // Omogući editovanje samo za tu kolonu
                dataGridView3.ReadOnly = false;
                foreach (DataGridViewColumn col in dataGridView3.Columns)
                {
                    if (col.Name != "Status")
                        col.ReadOnly = true;
                }
            }

            int colIndex = dataGridView3.Columns["PoslataNajava"].Index;

            // Ukloni originalnu kolonu
            dataGridView3.Columns.RemoveAt(colIndex);

            // Dodaj novu CheckBox kolonu
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
            chk.Name = "PoslataNajava";
            chk.HeaderText = "Poslata najava";
            chk.DataPropertyName = "PoslataNajava"; // mora da se poklapa sa imenom kolone iz DataTable
            chk.TrueValue = 1;
            chk.FalseValue = 0;
            chk.ThreeState = false;

            // Ubaci novu kolonu na isto mesto
            dataGridView3.Columns.Insert(colIndex, chk);

            upozorenjeTehnickiNeispravni = ""; // reset pre svakog punjenja

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Cells["TehnickiNeispravan"] != null &&
                    row.Cells["TehnickiNeispravan"].Value != DBNull.Value &&
                    row.Cells["TehnickiNeispravan"].Value.ToString() == "1")
                {
                    string regBr = row.Cells["Kamion"].Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(regBr))
                    {
                        upozorenjeTehnickiNeispravni += $"- Kamion {regBr} koji je već dodeljen je tehnički neispravan.\n";
                    }
                }
            }

            PodesiDatagridView(dataGridView3);

            dataGridView3.RowHeadersWidth = 30; // ili bilo koja vrednost u pikselima

            string[] koloneZaSakrivanje = new string[] {
                    "ID", "KamionID", "Uvoz","StatusID", "IdsRadniNalogDrumski","TehnickiNeispravan"
                    };
            //string[] koloneZaSakrivanje = new string[] {
            //        "ID", "KamionID", "Cena", "DtPreuzimanjaPraznogKontejnera", "AdresaUtovara", "AdresaIstovara", "MestoUtovara", "MestoIstovara", "BrojKontejnera2",
            //        "KontaktOsobaUtovarIstovar", "NapomenaZaPozicioniranje" , "OdredisnaCarina", "PolaznaCarinarnica", "PolaznaSpedicija", "OdredisnaSpedicija", "BrojKontejnera",
            //        "PDV", "Uvoz","StatusID", "DatumUtovara", "TipVozila"
            //        };

            foreach (string kolona in koloneZaSakrivanje)
            {
                if (dataGridView3.Columns.Contains(kolona))
                {
                    dataGridView3.Columns[kolona].Visible = false;
                }
            }
        }
        private void DodajDugmadKolonu()
        {
            // Kolona za instrukcije
            DataGridViewButtonColumn instrukcijeBtn = new DataGridViewButtonColumn();
            instrukcijeBtn.Name = "Instrukcije";
            instrukcijeBtn.HeaderText = "Instrukcije";
            instrukcijeBtn.Text = "Pošalji";
            instrukcijeBtn.UseColumnTextForButtonValue = true;
            instrukcijeBtn.Width = 100;

            DataGridViewButtonColumn uploadBtn = new DataGridViewButtonColumn();
            uploadBtn.Name = "Upload";
            uploadBtn.HeaderText = "Dokumenta";
            uploadBtn.Text = "Dodaj";
            uploadBtn.UseColumnTextForButtonValue = true;
            uploadBtn.Width = 100;

            DataGridViewButtonColumn openUploadedBtn = new DataGridViewButtonColumn();
            openUploadedBtn.Name = "Dokumenta";
            openUploadedBtn.HeaderText = ""; // prazno
            openUploadedBtn.Text = "Otvori";
            openUploadedBtn.UseColumnTextForButtonValue = true;
            openUploadedBtn.Width = 100;

            uploadBtn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            // Dodaj ako već ne postoje
            if (!dataGridView3.Columns.Contains("Instrukcije"))
                dataGridView3.Columns.Add(instrukcijeBtn);

            if (!dataGridView3.Columns.Contains("Upload"))
                dataGridView3.Columns.Add(uploadBtn);

            if (!dataGridView3.Columns.Contains("Dokumenta"))
                dataGridView3.Columns.Add(openUploadedBtn);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                var grid = dataGridView3;
                var kolona = grid.Columns[e.ColumnIndex].Name;
                int? radniNalogDrumskiID = 0;
                //if (kolona == "Instrukcije")
                //{

                //    DataGridViewRow row = grid.Rows[e.RowIndex];

                //    string idsString = row.Cells["ID"].Value?.ToString();

                //    if (string.IsNullOrEmpty(idsString))
                //    {
                //        MessageBox.Show("Nema ID za odabrani red.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }

                //    // Izvlacimo prvi ID iz stringa (samo za proveru statusa PoslataNajava)

                //    string prviIdString = idsString.Split(',').First().Trim();

                //    if (int.TryParse(prviIdString, out int parsedID))
                //    {
                //        radniNalogDrumskiID = parsedID;
                //    }
                //    else
                //    {
                //        MessageBox.Show("Greška pri parsiranju ID-ja za proveru statusa.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }

                //    int? poslateInstrukcije = ProveriDaLiJePorukaPoslata(radniNalogDrumskiID);
                //    if (poslateInstrukcije > 0)
                //    {
                //        DialogResult result = MessageBox.Show(
                //            "Za ovaj nalog instrukcije su već poslate.\nDa li želite da ih ponovo pošaljete?",
                //            "Upozorenje",
                //            MessageBoxButtons.YesNo,
                //            MessageBoxIcon.Question);

                //        if (result == DialogResult.No)
                //            return; // prekid
                //    }

                //    List<int> idjeviZaNajavu;
                //    try
                //    {
                //        idjeviZaNajavu = idsString
                //            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                //            .Select(s => int.Parse(s.Trim()))
                //            .ToList();
                //    }
                //    catch (FormatException)
                //    {
                //        MessageBox.Show("Greška u formatu ID-jeva naloga. Proverite podatke.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }

                //    DataTable detaljnaTabela = DobaviDetaljeZaNajavu(idjeviZaNajavu);
                //    string nalogodavac = row.Cells["Nalogodavac"].Value?.ToString() ?? "";

                //    if (detaljnaTabela.Rows.Count == 0)
                //    {
                //        MessageBox.Show("Nema detaljnih podataka za odabrane naloge.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }

                //    frmPregledPorukeVozacu pe = new frmPregledPorukeVozacu(detaljnaTabela, radniNalogDrumskiID);
                //    pe.StartPosition = FormStartPosition.CenterParent;
                //    pe.ShowDialog(this);
                //}
                //else if (kolona == "Upload")
                //{
                //    radniNalogDrumskiID = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value);
                //    int zaposleniID = PostaviVrednostZaposleni();

                //    OpenFileDialog ofd = new OpenFileDialog();
                //    ofd.Title = "Odaberite fajl za upload";
                //    ofd.Filter = "Svi fajlovi|*.*";

                //    if (ofd.ShowDialog() == DialogResult.OK)
                //    {
                //        string izabraniFajl = ofd.FileName;
                //        string ekstenzija = Path.GetExtension(izabraniFajl);
                //        string cleanName = Path.GetFileNameWithoutExtension(izabraniFajl);

                //        // Očisti naziv fajla od nedozvoljenih karaktera
                //        string nazivFajla = string.Join("_", cleanName.Split(Path.GetInvalidFileNameChars())) + ekstenzija;

                //        // Putanja na server
                //        string targetPath = $@"\\192.168.99.10\Leget\Drumski\Dokumenta\ID_{radniNalogDrumskiID}";
                //        string destinacija = Path.Combine(targetPath, nazivFajla);

                //        try
                //        {
                //            // Ako ne postoji folder, napravi ga
                //            if (!Directory.Exists(targetPath))
                //                Directory.CreateDirectory(targetPath);

                //            // Provera da li fajl već postoji
                //            if (File.Exists(destinacija))
                //            {
                //                DialogResult result = MessageBox.Show("Fajl sa istim imenom već postoji. Da li želite da ga zamenite?",
                //                                                      "Upozorenje",
                //                                                      MessageBoxButtons.YesNo,
                //                                                      MessageBoxIcon.Warning);

                //                if (result != DialogResult.Yes)
                //                    return; // korisnik ne želi da zameni fajl
                //            }

                //            // Kopiraj fajl
                //            File.Copy(izabraniFajl, destinacija, true);

                //            // Snimi u bazu
                //            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                //            ins.SnimiUFajlBazu(radniNalogDrumskiID, nazivFajla, destinacija, zaposleniID);

                //            MessageBox.Show("Fajl uspešno sačuvan i evidentiran u bazi.");
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show("Greška prilikom kopiranja fajla: " + ex.Message);
                //        }
                //    }
                //}
                //else if (kolona == "Dokumenta")
                //{
                //    if (aktivnaFormaPregleda == null || aktivnaFormaPregleda.IsDisposed)
                //    {
                //        var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                //        int radniNalogID = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value);

                //        frmPregledFajlova pregled = new frmPregledFajlova(radniNalogID);
                //        pregled.ShowDialog();
                //    }
                //}
            }
        }


    }
}
