using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Saobracaj.Dokumenta.TrainListItem;
using System.Text;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Net;
using Saobracaj.Uvoz;

namespace Saobracaj.Drumski
{
    public partial class frmStatus: Form
    {
        private string upozorenjeTehnickiNeispravni = "";
        private DataTable mainTable;
        private bool cellClickHandlerAttached = false;
        private Form aktivnaFormaPregleda;
        private bool editingControlHandlerAttached = false;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private readonly List<int> _tipoviIn;
        private readonly List<int> _tipoviNotIn;
        private int _stariStatusID = -1;

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
          

                string datumZaProveru = "";
                datumZaProveru = "GETDATE()";

                string uslovTipVozila = "1 = 1 ";

                if (_tipoviIn?.Any() == true)
                {
                    string lista = string.Join(",", _tipoviIn);
                    uslovTipVozila += $" AND x.VlasnistvoLegeta IN ({lista}) ";
                }

                if (_tipoviNotIn?.Any() == true)
                {
                    string lista = string.Join(",", _tipoviNotIn);
                    uslovTipVozila += $" AND x.VlasnistvoLegeta NOT IN ({lista}) ";
                }

                int stringEmpty = 0;
                int nalogID = 0;
                if (string.IsNullOrWhiteSpace(txtNalogID.Text))
                {

                    stringEmpty = 1;
                }

                else if (!int.TryParse(txtNalogID.Text.ToString(), out nalogID))
                {
                    MessageBox.Show("Uneti ID nije validan broj.");
                    return; // Prekidamo izvršavanje ako nije broj
                }

              if(stringEmpty != 1)
                uslovTipVozila += $" AND x.NalogID = {nalogID} ";


                var select = $@"
                        WITH Dokumenti AS (
                            SELECT
                                RadniNalogDrumskiID,
                                 COUNT(*) AS BrojDokumenata
                            FROM DokumentaRadnogNalogaDrumski
                            GROUP BY RadniNalogDrumskiID
                        )
                            SELECT   
                                -- Agregirana kolona (Spojeni ID-jevi)
                                x.ID,
                                LTRIM(RTRIM( x.Nalogodavac)) AS Nalogodavac, 
                                x.Relacija,
                                x.DatumIstovara, 
                                LTRIM(RTRIM(x.Prevoznik)) AS Prevoznik, 
                                LTRIM(RTRIM(x.Vozac)) AS Vozac,
                                LTRIM(RTRIM(x.Kamion)) AS Kamion, 
                                x.NalogID, 
                                x.Status AS Status,
                                x.DatumPromeneStatusa,
                                x.StatusID,
                                x.TehnickiNeispravan,
                                isNull(d.BrojDokumenata,0) AS BrojDokumenata,
                                x.BrojKontejnera,
                                x.BrojKontejnera2,
		                        x.Uvoz,
		                        x.PolaznaCarinarnica,
		                        x.OdredisnaCarinarnica,
                                x.VlasnistvoLegeta


                            FROM 
                            (
                                -- Deo 1 (Izvoz)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac, 
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       au.VlasnistvoLegeta,
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan, i.BrojKontejnera,rn.BrojKontejnera2,
                                       rn.DatumPromeneStatusa, rn.Uvoz,i.MestoCarinjenja as polaznaCarinarnica, 0 AS OdredisnaCarinarnica
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
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 
                                
                                UNION ALL 
                                -- Deo 2 (IzvozKonacna)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       au.VlasnistvoLegeta,
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan,  ik.BrojKontejnera,rn.BrojKontejnera2,
                                       rn.DatumPromeneStatusa, rn.Uvoz,ik.MestoCarinjenja as polaznaCarinarnica, 0 AS OdredisnaCarinarnica 
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
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 
                        
                                UNION ALL 
                                -- Deo 3 (UvozKonacna)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       au.VlasnistvoLegeta,
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan, uk.BrojKontejnera,rn.BrojKontejnera2,
                                       rn.DatumPromeneStatusa, rn.Uvoz,  0 as polaznaCarinarnica,  uk.OdredisnaCarina as OdredisnaCarinarnica 
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
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 
                
                                UNION ALL 
                                -- Deo 4 (Uvoz)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac, 
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija, 
                                       au.Vozac,
                                       au.RegBr AS Kamion,
                                       au.VlasnistvoLegeta, 
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan, u.BrojKontejnera,rn.BrojKontejnera2, rn.DatumPromeneStatusa,
                                       rn.Uvoz,  0 as polaznaCarinarnica, u.OdredisnaCarina as OdredisnaCarinarnica 
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
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 
             
                                UNION ALL 
                                -- Deo 5 (Ostali drumski)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       au.VlasnistvoLegeta,
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID , CASE WHEN ap.VoziloID IS NOT NULL THEN 1 ELSE 0 END AS TehnickiNeispravan,  rn.BrojKontejnera,rn.BrojKontejnera2,
                                       rn.DatumPromeneStatusa,rn.Uvoz ,rn.PolaznaCarinarnica, rn.OdredisnaCarinarnica as OdredisnaCarinarnica
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
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 
                
                            ) AS x

                            LEFT JOIN (SELECT tt.ID AS ScenarioID, m.TipNalogaID, tt.TipTransporta, tt.PolaznaCI, tt.OdredisnaCI
                            FROM ScenarijaTokaTransporta tt
                            JOIN MapiranjeTipaNaloga m ON tt.VrstaNaloga = m.VrstaNaloga
                                ) AS IdentifikatorScenarija ON 
                                IdentifikatorScenarija.TipTransporta = x.VlasnistvoLegeta -- Uzimamo tip iz naloga
                                AND IdentifikatorScenarija.TipNalogaID = x.Uvoz -- To je 0,3,5...
                                -- Ovde dodaš logiku za CI (npr. provera da li su polja za CI popunjena)
                                AND IdentifikatorScenarija.PolaznaCI = (CASE WHEN x.PolaznaCarinarnica IS NOT NULL AND x.PolaznaCarinarnica > 0 THEN 1 ELSE 0 END) 
                                AND IdentifikatorScenarija.OdredisnaCI = (CASE WHEN x.OdredisnaCarinarnica IS NOT NULL AND x.OdredisnaCarinarnica > 0 THEN 1 ELSE 0 END)

                            -- Sada spajamo sa spojnom tabelom preko izračunatog Scenarija
                            LEFT JOIN ScenarioTokTransporta_Statusi sts ON 
                                sts.ScenarioID = IdentifikatorScenarija.ScenarioID 
                                AND sts.Status = x.Status
                            LEFT JOIN Dokumenti d ON d.RadniNalogDrumskiID = x.id
                           WHERE  {uslovTipVozila}  AND ( ISNULL(sts.JesteZavrsni, 0) = 0 OR  (ISNULL(sts.JesteZavrsni, 0) = 1 AND ISNULL(d.BrojDokumenata, 0) = 0))

                            ORDER BY 
                                x.NalogID DESC";
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


                if (!editingControlHandlerAttached)
                {
                    dataGridView3.EditingControlShowing += dataGridView3_EditingControlShowing;
                    editingControlHandlerAttached = true;
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


                DodajDugmadKolonuPosaljiStatus();

                // Omogući editovanje samo za tu kolonu
                dataGridView3.ReadOnly = false;
                foreach (DataGridViewColumn col in dataGridView3.Columns)
                {
                    if (col.Name != "Status")
                        col.ReadOnly = true;
                }
            }    

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
                    "ID", "KamionID", "Uvoz","StatusID", "IdsRadniNalogDrumski","TehnickiNeispravan","BrojDokumenata", "BrojKontejnera", "BrojKontejnera2","Uvoz", "PolaznaCarinarnica","OdredisnaCarinarnica",
                    "VlasnistvoLegeta","TipTransporta"
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

            string[] koloneZaFiksiranje = {  "nalogID" };

            foreach (string nazivKolone in koloneZaFiksiranje)
            {
                // Proveravamo da li kolona sa tim imenom postoji u DataGridView-u
                if (dataGridView3.Columns.Contains(nazivKolone))
                {
                    // Pristupamo koloni preko njenog imena i menjamo svojstva
                    dataGridView3.Columns[nazivKolone].Width = 60;

                    // Onemogućavamo korisniku da je ručno menja (ako želite)
                    dataGridView3.Columns[nazivKolone].Resizable = DataGridViewTriState.False;
                }
            }
            if (dataGridView3.Columns.Contains("DatumPromeneStatusa"))
            {
                dataGridView3.Columns["DatumPromeneStatusa"].ValueType = typeof(DateTime);
                dataGridView3.Columns["DatumPromeneStatusa"].DefaultCellStyle.Format = "dd.MM.yyyy";
            }
        }

        private void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView3.CurrentCell.ColumnIndex == dataGridView3.Columns["Status"].Index && e.Control is ComboBox combo)
            {
                // 1. Ovo sprečava "zaključavanje" fokusa na comoboxu
                combo.CausesValidation = false;

                combo.SelectionChangeCommitted -= Combo_SelectionChangeCommitted;

                object trenutnaVrednost = dataGridView3.CurrentCell.Value;
                if (trenutnaVrednost == DBNull.Value) trenutnaVrednost = 0;

                DataRowView drv = dataGridView3.CurrentRow.DataBoundItem as DataRowView;
                if (drv != null)
                {
                    DataRow row = drv.Row;
                    int tip = Convert.ToInt32(row["VlasnistvoLegeta"]);
                    int tipNaloga = Convert.ToInt32(row["Uvoz"]);
                    int imaCarinuP = (row["PolaznaCarinarnica"] != DBNull.Value && Convert.ToInt32(row["PolaznaCarinarnica"]) > 0) ? 1 : 0;
                    int imaCarinuO = (row["OdredisnaCarinarnica"] != DBNull.Value && Convert.ToInt32(row["OdredisnaCarinarnica"]) > 0) ? 1 : 0;

                    DataTable dt = VratiStatuseZaScenario(tip, tipNaloga, imaCarinuP, imaCarinuO);
                    combo.DataSource = dt;
                    combo.DisplayMember = "Naziv";
                    combo.ValueMember = "ID";

                    // 2. SIGURNIJA SELEKCIJA: Ako nema vrednosti, postavi na praznu stavku (index 0)
                    if (trenutnaVrednost != null && Convert.ToInt32(trenutnaVrednost) != 0)
                        combo.SelectedValue = trenutnaVrednost;
                    else
                        combo.SelectedIndex = 0; // Podrazumevamo da je ID 0 prazna stavka

                    combo.SelectionChangeCommitted += Combo_SelectionChangeCommitted;

                    // 3. DROPDOWN TRIK: Koristimo provere da ne bismo "zaledili" UI
                    this.BeginInvoke(new MethodInvoker(() => {
                        if (dataGridView3.EditingControl != null && combo.IsHandleCreated)
                        {
                            combo.DroppedDown = true;
                        }
                    }));
                }
            }
        }

        private void Combo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo != null && combo.SelectedValue != null)
            {
                // 1. Upisujemo vrednost direktno u ćeliju
                dataGridView3.CurrentCell.Value = combo.SelectedValue;

                // 2. Forsiramo kraj editovanja - ovo će okinuti CellValueChanged
                dataGridView3.EndEdit();
            }
        }

        private DataTable VratiStatuseZaScenario(int tip, int nalog, int cip, int cio)
        {
            string query = @"SELECT 0 AS ID, '' AS Naziv, 0 AS Redosled
                            UNION ALL
                            SELECT s.ID, s.Naziv ,ss.Redosled
                            FROM StatusVozila s
                            JOIN ScenarioTokTransporta_Statusi ss ON s.ID = ss.Status
                            JOIN ScenarijaTokaTransporta tt ON ss.ScenarioID = tt.ID
                            JOIN MapiranjeTipaNaloga m ON tt.VrstaNaloga = m.VrstaNaloga
                            WHERE tt.TipTransporta = @tip 
                              AND m.TipNalogaID = @nalog
                              AND tt.PolaznaCI = @cip
                              AND tt.OdredisnaCI = @cio
                            ORDER BY Redosled";

            using (SqlDataAdapter da = new SqlDataAdapter(query, connection))
            {
                da.SelectCommand.Parameters.AddWithValue("@tip", tip);
                da.SelectCommand.Parameters.AddWithValue("@nalog", nalog);
                da.SelectCommand.Parameters.AddWithValue("@cip", cip);
                da.SelectCommand.Parameters.AddWithValue("@cio", cio);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        private void DodajDugmadKolonuPosaljiStatus()
        {
            // Kolona za instrukcije
            DataGridViewButtonColumn najavaBtn = new DataGridViewButtonColumn();
            najavaBtn.Name = "PosaljiStatus";
            najavaBtn.HeaderText = "Posalji";
            najavaBtn.Text = "Trenutni status";
            najavaBtn.UseColumnTextForButtonValue = true;
            najavaBtn.Width = 100;



            //uploadBtn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //Dodaj ako već ne postoje
            if (!dataGridView3.Columns.Contains("PosaljiStatus"))
                dataGridView3.Columns.Add(najavaBtn);

        }
        private void DodajDugmadKolonu()
        {
            //// Kolona za instrukcije
            //DataGridViewButtonColumn instrukcijeBtn = new DataGridViewButtonColumn();
            //instrukcijeBtn.Name = "Instrukcije";
            //instrukcijeBtn.HeaderText = "Instrukcije";
            //instrukcijeBtn.Text = "Pošalji";
            //instrukcijeBtn.UseColumnTextForButtonValue = true;
            //instrukcijeBtn.Width = 100;

            DataGridViewButtonColumn uploadBtn = new DataGridViewButtonColumn();
            uploadBtn.Name = "Upload";
            uploadBtn.HeaderText = "";
            uploadBtn.Text = "Dodaj";
            uploadBtn.UseColumnTextForButtonValue = true;
            uploadBtn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            uploadBtn.Width = 100;

            DataGridViewButtonColumn openUploadedBtn = new DataGridViewButtonColumn();
            openUploadedBtn.Name = "Dokumenta";
            openUploadedBtn.HeaderText = "Dokumenta"; 
            openUploadedBtn.Text = "Otvori";
            openUploadedBtn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            openUploadedBtn.UseColumnTextForButtonValue = true;
            openUploadedBtn.Width = 100;

            //uploadBtn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            // Dodaj ako već ne postoje
            //if (!dataGridView3.Columns.Contains("Instrukcije"))
            //    dataGridView3.Columns.Add(instrukcijeBtn);

            //if (!dataGridView3.Columns.Contains("Upload"))
            //    dataGridView3.Columns.Add(uploadBtn);

            if (!dataGridView3.Columns.Contains("Dokumenta"))
                dataGridView3.Columns.Add(openUploadedBtn);

            if (!dataGridView3.Columns.Contains("Upload"))
                dataGridView3.Columns.Add(uploadBtn);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                var grid = dataGridView3;
                var kolona = grid.Columns[e.ColumnIndex].Name;
                int? radniNalogDrumskiID = 0;

                if (kolona == "Upload")
                {
                    radniNalogDrumskiID = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value);
                    int zaposleniID = PostaviVrednostZaposleni();

                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Odaberite fajl za upload";
                    ofd.Filter = "Svi fajlovi|*.*";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string izabraniFajl = ofd.FileName;
                        string ekstenzija = Path.GetExtension(izabraniFajl);
                        string cleanName = Path.GetFileNameWithoutExtension(izabraniFajl);

                        // Očisti naziv fajla od nedozvoljenih karaktera
                        string nazivFajla = string.Join("_", cleanName.Split(Path.GetInvalidFileNameChars())) + ekstenzija;

                        // Putanja na server
                        string targetPath = $@"\\192.168.150.110\Leget\Drumski\Dokumenta\ID_{radniNalogDrumskiID}";
                        string destinacija = Path.Combine(targetPath, nazivFajla);

                        try
                        {
                            // Ako ne postoji folder, napravi ga
                            if (!Directory.Exists(targetPath))
                                Directory.CreateDirectory(targetPath);

                            // Provera da li fajl već postoji
                            if (File.Exists(destinacija))
                            {
                                DialogResult result = MessageBox.Show("Fajl sa istim imenom već postoji. Da li želite da ga zamenite?",
                                                                      "Upozorenje",
                                                                      MessageBoxButtons.YesNo,
                                                                      MessageBoxIcon.Warning);

                                if (result != DialogResult.Yes)
                                    return; // korisnik ne želi da zameni fajl
                            }

                            // Kopiraj fajl
                            File.Copy(izabraniFajl, destinacija, true);

                            // Snimi u bazu
                            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                            ins.SnimiUFajlBazu(radniNalogDrumskiID, nazivFajla, destinacija, zaposleniID);

                            MessageBox.Show("Fajl uspešno sačuvan i evidentiran u bazi.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Greška prilikom kopiranja fajla: " + ex.Message);
                        }
                    }
                }
                else if (kolona == "Dokumenta")
                {
                    if (aktivnaFormaPregleda == null || aktivnaFormaPregleda.IsDisposed)
                    {
                        var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                        int radniNalogID = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value);

                        frmPregledFajlova pregled = new frmPregledFajlova(radniNalogID);
                        pregled.ShowDialog();
                    }
                }
                else if (kolona == "PosaljiStatus")
                {
                    if (aktivnaFormaPregleda == null || aktivnaFormaPregleda.IsDisposed)
                    {
                        int radniNalogID = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value);
                        posaljiStatusKontejnera(radniNalogID);
                        int.TryParse( grid.Rows[e.RowIndex].Cells["ID"].Value.ToString(), out int idRN );

                         List<int> ids = new List<int> { idRN };

                        InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                        ins.UpdateStatusPoslat(ids);
                    }
                }
                
            }
        }

        private int PostaviVrednostZaposleni()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int ulogovaniZaposleniID = 0;

            con.Open();

            SqlCommand cmd = new SqlCommand("Select  k.DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Zaposleni " +
                                            " FROM Korisnici k " +
                                            "INNER JOIN Delavci d ON k.DeSifra = d.DeSifra " +
                                            "where Trim(Korisnik) like '" + Saobracaj.Sifarnici.frmLogovanje.user.Trim() + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["ID"] != DBNull.Value)
                    ulogovaniZaposleniID = Convert.ToInt32(dr["ID"].ToString());
            }
            return ulogovaniZaposleniID;

        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int postojiStavkaZaArhiviranje = 0;
            bool trebaOkidatiInterni = false;
            // 1. Provera da li je promenjena ćelija "Status" u ispravnom redu
            if (e.RowIndex >= 0 && dataGridView3.Columns[e.ColumnIndex].Name == "Status")
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                // Provera da li su potrebne kolone dostupne
                if (row.Cells["Status"].Value == DBNull.Value || row.Cells["ID"].Value == null)
                {
                    return;
                }

                int noviStatusID = Convert.ToInt32(row.Cells["Status"].Value);
                int brojDokumenata = Convert.ToInt32(row.Cells["BrojDokumenata"].Value);
                string idsString = row.Cells["ID"].Value.ToString();
                int tip = Convert.ToInt32(row.Cells["VlasnistvoLegeta"].Value);
                int tipNalogaID = Convert.ToInt32(row.Cells["Uvoz"].Value); // Polje 0,1,2,3...
                int imaCarinuP = (row.Cells["PolaznaCarinarnica"].Value != DBNull.Value && Convert.ToInt32(row.Cells["PolaznaCarinarnica"].Value) > 0) ? 1 : 0;
                int imaCarinuO = (row.Cells["OdredisnaCarinarnica"].Value != DBNull.Value && Convert.ToInt32(row.Cells["OdredisnaCarinarnica"].Value) > 0) ? 1 : 0;

                // 1. Provera da li je status završni za ovaj konkretan nalog/scenario
                bool jeZavrsni = ProveriDaLiJeZavrsni(noviStatusID, tip, tipNalogaID, imaCarinuP, imaCarinuO);

              
                    // ako su stari i novi status jednaki nista ne radimo
                if (noviStatusID == _stariStatusID)
                    return;
                if (jeZavrsni)
                {
                 
                    trebaOkidatiInterni = true;

                    if (brojDokumenata > 0)
                    {
                        // Učitaj naziv statusa (iz StatusVozila)
                        string nazivStatusa = VratiNazivStatusa(noviStatusID);

                        var result = MessageBox.Show(
                            $"Da li ste sigurni da želite da promenite status u '{nazivStatusa}'?\n" +
                            $"Ova akcija će arhivirati stavku naloga!",
                            "Potvrda",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.No)
                        {
                            RefreshDataGrid3();
                            return;
                        }
                        else if (result == DialogResult.Yes)
                        {
                            trebaOkidatiInterni = true;
                        }
                        postojiStavkaZaArhiviranje++;
                       
                    }
                }

                int id;

                if (int.TryParse(idsString, out id))
                {
                    try
                    {
                        InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                        ins.UpdateStatusRadniNalogDrumski(id, noviStatusID);

                        if (jeZavrsni)
                        {
                            ins.ArhiviranRadniNalogDrumski(id);
                        }
                            if (trebaOkidatiInterni)
                        {
                            InsertRadniNalogInterni updi = new InsertRadniNalogInterni();
                            //updi.UpdRadniNalogInterniZavrsen(id, Saobracaj.Sifarnici.frmLogovanje.user.Trim());

                            // takođe odradi update statusa na arhiviran
                            
                        }
                        
                        List<int> ids = new List<int> { id };

                        ins.UpdateStatusPoslat(ids);
                        if (jeZavrsni)
                        {
                            ins.ArhiviranRadniNalogDrumski(id);

                            this.BeginInvoke(new MethodInvoker(() => {
                                // 1. Prvo "ubijam" fokus da ne puca na poslednjem redu
                                dataGridView3.CurrentCell = null;

                                // 2. Isključjem event
                                dataGridView3.CellValueChanged -= dataGridView3_CellValueChanged;
                                RefreshDataGrid3();
                                dataGridView3.CellValueChanged += dataGridView3_CellValueChanged;
                            }));

                            this.BeginInvoke(new MethodInvoker(() => {
                                posaljiStatusKontejnera(id);
                            }));
                            return; // 3. Veoma važno: prekini dalje izvršavanje ako je yavrsni
                        }

                       

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Greška pri snimanju statusa: " + ex.Message);
                    }
                }

            }
            
        }

        private bool ProveriDaLiJeZavrsni(int statusID, int tip, int nalog, int cip, int cio)
        {
            bool result = false;
            string query = @"
                            SELECT sts.JesteZavrsni
                            FROM ScenarioTokTransporta_Statusi sts
                            JOIN ScenarijaTokaTransporta tt ON sts.ScenarioID = tt.ID
                            JOIN MapiranjeTipaNaloga m ON tt.VrstaNaloga = m.VrstaNaloga
                            WHERE sts.Status = @statusID 
                              AND tt.TipTransporta = @tip 
                              AND m.TipNalogaID = @nalog
                              AND tt.PolaznaCI = @cip
                              AND tt.OdredisnaCI = @cio";


            using (SqlConnection conn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@statusID", statusID);
                cmd.Parameters.AddWithValue("@tip", tip);
                cmd.Parameters.AddWithValue("@nalog", nalog);
                cmd.Parameters.AddWithValue("@cip", cip);
                cmd.Parameters.AddWithValue("@cio", cio);

                conn.Open();
                var val = cmd.ExecuteScalar();
                if (val != null && val != DBNull.Value)
                    result = Convert.ToBoolean(val);
            }
            return result;
        }

        private void posaljiStatusKontejnera(int id)
        {
            if (id == null)
            {
                MessageBox.Show("Došlo je do greške prilikom pravljenja poruke o promeni statusa.", "Upozorenje",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ID = id;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT rn.ID, " +
                                       "LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  " +
                                       "i.BrojKontejnera,rn.BrojKontejnera2, " +
                                       "LTRIM(RTRIM(sv.Naziv)) AS Status, " +
                                       "au.Vozac, " +
                                       "rn.DatumPromeneStatusa, " +
                                       "au.RegBr AS Kamion " +
                                "FROM RadniNalogDrumski rn " +
                                "INNER JOIN Automobili au ON au.ID = rn.KamionID " +
                                "LEFT JOIN StatusVozila sv ON sv.ID = rn.Status " +
                                "INNER JOIN Izvoz i ON i.ID = rn.KontejnerID " +
                                "LEFT JOIN Partnerji pa ON pa.PaSifra = i.Klijent3 " +
                                "WHERE rn.Uvoz = 0 AND rn.ID =  " + ID +


                                "UNION ALL " +
                               " SELECT rn.ID, " +
                               "        LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac, " +
                               "        ik.BrojKontejnera,rn.BrojKontejnera2, " +
                               "        LTRIM(RTRIM(sv.Naziv)) AS Status, " +
                               "        au.Vozac, " +
                               "        rn.DatumPromeneStatusa, " +
                               "        au.RegBr AS Kamion" +
                               " FROM RadniNalogDrumski rn " +
                               " INNER JOIN Automobili au ON au.ID = rn.KamionID " +
                               " INNER JOIN IzvozKonacna ik ON ik.ID = rn.KontejnerID " +
                               " LEFT JOIN Partnerji pa ON pa.PaSifra = ik.Klijent3 " +
                               " LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra " +
                               " LEFT JOIN StatusVozila sv ON sv.ID = rn.Status " +
                               " WHERE rn.Uvoz = 0  AND rn.ID = " + ID +


                               " UNION ALL " +
                               " SELECT rn.ID,  " +
                                "       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  " +
                               "        uk.BrojKontejnera,rn.BrojKontejnera2, " +
                               "        LTRIM(RTRIM(sv.Naziv)) AS Status, " +
                               "        au.Vozac, " +
                               "        rn.DatumPromeneStatusa, " +
                               "        au.RegBr AS Kamion " +
                               " FROM RadniNalogDrumski rn  " +
                               " INNER JOIN Automobili au ON au.ID = rn.KamionID  " +
                               " INNER JOIN UvozKonacna uk ON uk.ID = rn.KontejnerID  " +
                               " LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3  " +
                               " LEFT JOIN StatusVozila sv ON sv.ID = rn.Status  " +
                               " WHERE rn.Uvoz = 1 AND rn.ID = " + ID +


                               " UNION ALL  " +
                               " SELECT rn.ID,  " +
                               "        LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  " +
                               "        u.BrojKontejnera,rn.BrojKontejnera2,  " +
                               "        LTRIM(RTRIM(sv.Naziv)) AS Status, " +
                               "        au.Vozac, " +
                               "        rn.DatumPromeneStatusa, " +
                               "        au.RegBr AS Kamion " +
                               " FROM RadniNalogDrumski rn  " +
                               " INNER JOIN Automobili au ON au.ID = rn.KamionID  " +
                               " INNER JOIN Uvoz u ON u.ID = rn.KontejnerID  " +
                               " LEFT JOIN Partnerji pa ON pa.PaSifra = u.Nalogodavac3 " +
                               " LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra  " +
                               " LEFT JOIN StatusVozila sv ON sv.ID = rn.Status  " +
                               " WHERE rn.Uvoz = 1 AND rn.ID =  " + ID +

                               " UNION ALL  " +
                               " SELECT rn.ID,  " +
                               "        LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac, " +
                               "        rn.BrojKontejnera,rn.BrojKontejnera2, " +
                               "        LTRIM(RTRIM(sv.Naziv)) AS Status, " +
                               "        au.Vozac, " +
                               "        rn.DatumPromeneStatusa, " +
                               "        au.RegBr AS Kamion " +
                               " FROM RadniNalogDrumski rn  " +
                               " INNER JOIN Automobili au ON au.ID = rn.KamionID  " +
                               " LEFT JOIN Partnerji pa ON pa.PaSifra = rn.Klijent  " +
                               " LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra  " +
                               " LEFT JOIN StatusVozila sv ON sv.ID = rn.Status  " +
                              " WHERE rn.Uvoz IN (2, 3, 4, 5) AND rn.ID = " + ID, con);

            SqlDataReader dr = cmd.ExecuteReader();
            string nalogodavac = "";
            string k1 = "";
            string k2 = "";
            string noviStatusTekst = "";
            DateTime? datumPromene = null;
            string kamion = "";
            string vozac = "";
            while (dr.Read())
            {
                nalogodavac = dr["Nalogodavac"].ToString()?.Trim();
                k1 = dr["BrojKontejnera"] == DBNull.Value ? "" : dr["BrojKontejnera"].ToString().Trim();
                k2 = dr["BrojKontejnera2"] == DBNull.Value ? "" : dr["BrojKontejnera"].ToString().Trim();
                noviStatusTekst = dr["Status"] == DBNull.Value ? "" : dr["Status"].ToString().Trim();
                if (dr["DatumPromeneStatusa"] != DBNull.Value)
                    datumPromene = Convert.ToDateTime(dr["DatumPromeneStatusa"]);
                kamion = dr["Kamion"] == DBNull.Value ? "" : dr["Kamion"].ToString().Trim();
                vozac = dr["Vozac"] == DBNull.Value ? "" : dr["Vozac"].ToString().Trim();
                //    noviStatusTekst = row.Cells["Status"]?.FormattedValue?.ToString();
            }
            string kontejner = !string.IsNullOrWhiteSpace(k2) ? $"{k1}, {k2}" : k1;

            string datumZaPrikaz = datumPromene?.ToString("dd.MM.yyyy HH:mm") ?? "";


            // HTML
            var sb = new StringBuilder();

            sb.AppendLine(@"
                        <div style='display:grid;
                             grid-template-columns: 40px 140px 200px 1fr 1fr 1fr;
                             grid-template-rows: auto auto;
                             column-gap: 8px;
                             row-gap: 4px;
                             font-family: Arial;
                             font-size: 14px;
                             margin-bottom: 12px;'>

                            <div style='grid-column:2;'><b>Nalogodavac:</b></div>
                            <div style='grid-column:3;'>" + nalogodavac + @"</div>

                            <div style='grid-column:2;'><b>Datum:</b></div>
                            <div style='grid-column:3;'>" + DateTime.Now.ToString("dd.MM.yyyy") + @"</div>
                        </div>
                        ");

            sb.AppendLine("<table cellpadding='4' cellspacing='0' style='border-collapse:collapse; border:1px solid #9fbbe7;'>");

            sb.AppendLine("<tr style='background-color:#d9e8fb; color:#0b3c6d; font-weight:bold; text-align:center;'>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>RB</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>KONTEJNER</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>NOVI STATUS</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>UPDATE</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>REGISTARSKI BROJ</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>VOZAČ</th>");
            sb.AppendLine("</tr>");

            sb.AppendLine("<tr>");
            sb.AppendLine("<td style='border:1px solid #9fbbe7;'>1</td>");
            sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{kontejner}</td>");
            sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{noviStatusTekst}</td>");
            sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{datumZaPrikaz}</td>");
            sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{kamion}</td>");
            sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{vozac}</td>");
            sb.AppendLine("</tr>");

            sb.AppendLine("</table>");

            SetClipboardHtml(sb.ToString());
            MessageBox.Show("Podaci su kopirani u clipboard.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        private void dataGridView3_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView3.Columns[e.ColumnIndex].Name == "Status")
            {
                var value = dataGridView3.Rows[e.RowIndex].Cells["Status"].Value;
                _stariStatusID = value != null && value != DBNull.Value
                    ? Convert.ToInt32(value)
                    : -1;
            }
        }

        private void frmStatus_Load(object sender, EventArgs e)
        {
            dataGridView3.DataError += (s, err) => {
                err.ThrowException = false;
            };
            dataGridView3.CellMouseDown += dataGridView3_CellMouseDown;
            DataTable dtGlavni = (DataTable)dataGridView3.DataSource;
            if (dtGlavni != null && dtGlavni.Columns.Contains("Status"))
            {
                dtGlavni.Columns["Status"].AllowDBNull = true;
            }
        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // 1. Utišaj grešku
            e.ThrowException = false;

            // 2.  Reci gridu da ne pokušava da zadrži fokus u toj ćeliji
            e.Cancel = false;

            if (e.Context.HasFlag(DataGridViewDataErrorContexts.Commit))
            {
                dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
            }
        }

        private void dataGridView3_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView3.Columns[e.ColumnIndex].Name == "Status")
            {

                if (dataGridView3.IsCurrentCellInEditMode)
                {
                    dataGridView3.EndEdit(DataGridViewDataErrorContexts.Commit);
                    dataGridView3.CancelEdit(); // Ako commit ne prođe, otkaži ga skroz
                }

                this.BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        dataGridView3.CurrentCell = dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        dataGridView3.BeginEdit(true);
                    }
                    catch { }
                }));
            }
        }


        private string VratiNazivStatusa(int statusID)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT LTRIM(RTRIM(Naziv)) AS Naziv FROM StatusVozila WHERE ID = @ID", conn);

                cmd.Parameters.AddWithValue("@ID", statusID);

                var result = cmd.ExecuteScalar();

                return result?.ToString() ?? "(nepoznat status)";
            }
        }

        private void dataGridView3_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView3.IsCurrentCellDirty)
            {
                dataGridView3.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnPosaljiStatuse_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count == 0)
            {
                MessageBox.Show("Nema podataka u gridu.", "Upozorenje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<int> idjeviZaNajavu = new List<int>();
            int? nalogId = null;
            string nalogodavac = null;

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.IsNewRow)
                    continue;

                // ID
                if (!int.TryParse(row.Cells["ID"].Value?.ToString(), out int id))
                {
                    MessageBox.Show("Postoji red bez validnog ID-a.", "Greška",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                idjeviZaNajavu.Add(id);

                // NalogID
                if (!int.TryParse(row.Cells["NalogID"].Value?.ToString(), out int trenutniNalogId))
                {
                    MessageBox.Show("Postoji red bez validnog NalogID-a.", "Greška",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (nalogId == null)
                    nalogId = trenutniNalogId;
                else if (nalogId != trenutniNalogId)
                {
                    MessageBox.Show("Svi redovi moraju imati isti nalog.", "Upozorenje",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Nalogodavac (uzimamo iz prvog reda)
                if (nalogodavac == null)
                    nalogodavac = row.Cells["Nalogodavac"].Value?.ToString();

                // Nalogodavac
                string trenutniNalogodavac = row.Cells["Nalogodavac"].Value?.ToString()?.Trim();

                if (string.IsNullOrEmpty(trenutniNalogodavac))
                {
                    MessageBox.Show("Postoji red bez popunjenog nalogodavca.", "Greška",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (nalogodavac == null)
                    nalogodavac = trenutniNalogodavac;
                else if (!string.Equals(nalogodavac, trenutniNalogodavac, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Svi redovi moraju imati istog nalogodavca.", "Upozorenje",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (idjeviZaNajavu.Count == 0)
            {
                MessageBox.Show("Nema validnih redova za obradu.", "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sb = new StringBuilder();

            // Zaglavlje
            sb.AppendLine(@"
            <div style='display:grid;grid-template-columns: 40px 140px 200px 1fr 1fr 1fr; grid-template-rows: auto auto; column-gap: 8px;row-gap: 4px; font-family: Arial; font-size: 14px;
                margin-bottom: 12px;'>

                <!-- Red 1 -->
                <div style='grid-column:2; grid-row:1;'><b>Nalogodavac:</b></div>
                <div style='grid-column:3; grid-row:1;'>" + nalogodavac + @"</div>

                <!-- Red 2 -->
                <div style='grid-column:2; grid-row:2;'><b>Datum:</b></div>
                <div style='grid-column:3; grid-row:2;'>" + DateTime.Now.ToString("dd.MM.yyyy") + @"</div>

            </div>
            ");

            // Tabela
            //sb.AppendLine("<table border='1' cellpadding='4' cellspacing='0' style='border-collapse:collapse;'>");
            //sb.AppendLine("<tr style='background-color:#f0f0f0;'>");
            //sb.AppendLine("<th>RB</th>");
            //sb.AppendLine("<th>KONTEJNER</th>");
            //sb.AppendLine("<th>STATUS</th>");
            //sb.AppendLine("<th>UPDATE</th>");
            //sb.AppendLine("<th>REGISTARSKI BROJ</th>");
            //sb.AppendLine("<th>VOZAČ</th>");
            //sb.AppendLine("</tr>");
            sb.AppendLine("<table cellpadding='4' cellspacing='0' style='border-collapse:collapse; border:1px solid #9fbbe7;'>");

            sb.AppendLine("<tr style='background-color:#d9e8fb; color:#0b3c6d; font-weight:bold; text-align:center;'>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>RB</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>KONTEJNER</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>NOVI STATUS</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>UPDATE</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>REGISTARSKI BROJ</th>");
            sb.AppendLine("<th style='border:1px solid #9fbbe7;'>VOZAČ</th>");
            sb.AppendLine("</tr>");

            int rb = 1;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.IsNewRow)
                    continue;
                string kontejner1 = row.Cells["BrojKontejnera"]?.Value?.ToString()?.Trim();
                string kontejner2 = row.Cells["BrojKontejnera2"]?.Value?.ToString()?.Trim();

                string noviStatusTekst = row.Cells["Status"]?.FormattedValue?.ToString();

                DateTime? datumPromene = null;

                var cellValue = row.Cells["DatumPromeneStatusa"]?.Value;

                if (cellValue != null && cellValue != DBNull.Value)
                {
                    datumPromene = Convert.ToDateTime(cellValue);
                }

                string datumZaPrikaz = datumPromene.HasValue
                    ? datumPromene.Value.ToString("dd.MM.yyyy HH:mm")
                    : string.Empty;


                string tekstKontejnera = kontejner1;

                if (!string.IsNullOrWhiteSpace(kontejner2))
                {
                    tekstKontejnera = $"{kontejner1}, {kontejner2}";
                }
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{rb++}</td>");
                sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{tekstKontejnera} </td>");
                sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{noviStatusTekst}</td>");
                sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{datumZaPrikaz}</td>");
                sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{row.Cells["Kamion"].Value}</td>");
                sb.AppendLine($"<td style='border:1px solid #9fbbe7;'>{row.Cells["Vozac"].Value}</td>");
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            sb.AppendLine("</div>");

            SetClipboardHtml(sb.ToString());

            MessageBox.Show("Podaci su kopirani u clipboard.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
            ins.UpdateStatusPoslat(idjeviZaNajavu);

        }
        private void SetClipboardHtml(string html)
        {
            string header =
                "Version:0.9\r\nStartHTML:00000097\r\nEndHTML:{0:00000000}\r\nStartFragment:00000131\r\nEndFragment:{1:00000000}\r\n";

            string pre = "<html><body><!--StartFragment-->";
            string post = "<!--EndFragment--></body></html>";

            string fullHtml = pre + html + post;

            string final = string.Format(header, pre.Length + html.Length + post.Length, pre.Length + html.Length) + fullHtml;

            Clipboard.Clear();
            Clipboard.SetText(final, TextDataFormat.Html);

            txtNalogID.Text = "";
            RefreshDataGrid3();
        }


        private void btnFitriraj_Click(object sender, EventArgs e)
        {
            RefreshDataGrid3();
        }
    }
}
