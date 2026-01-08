using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmObradaUlaznihFaktura: Form
    {

        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private DataTable dtRadniNalozi;
        int brojRedova = 0;
        private int RadniNalogID;

        public frmObradaUlaznihFaktura()
        {
            InitializeComponent();
            ChangeTextBox();
            panel2.Visible = false;
            panel3.Visible = false;
            gridGroupingControl1.TableControl.CellDoubleClick += Grid_TableControl_CellDoubleClick;
            this.KeyPreview = true;
            dtpPregleda.Value = DateTime.Today;
            btnIzadjiBezPromena.Visible = false;
        }

        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);



            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
              //  meniHeader.Visible = false;
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
               // meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void RefreshGrid()
        {
            var select = "";
            StringBuilder whereExtra = new StringBuilder();
            StringBuilder whereExtraRN = new StringBuilder();
            StringBuilder whereExtraIzvoz = new StringBuilder();
            StringBuilder whereExtraUvoz = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(txtKontejnerID.Text))
            {
                whereExtra.Append(" AND rn.ID = @ID");
                whereExtraRN.Append(" AND rn.ID = @ID");
            }


            if (!string.IsNullOrWhiteSpace(txtBrojKontejnera.Text))
            {
                whereExtra.Append(" AND i.BrojKontejnera = @BrojKontejnera ");
                whereExtraRN.Append(" AND rn.BrojKontejnera = @BrojKontejnera ");
            }
    

            if (!string.IsNullOrWhiteSpace(txtBL.Text))
            {
                whereExtraUvoz.Append(" AND i.BrodskaTeretnica = @BL ");
                whereExtraIzvoz.Append(" AND rn.Uvoz = 1 ");
                whereExtraRN.Append(" AND rn.BrodskaTeretnica = @BL ");
            }

            if (!string.IsNullOrWhiteSpace(txtBooking.Text))
            {
                whereExtraUvoz.Append(" AND rn.Uvoz = 0 ");
                whereExtraIzvoz.Append("  AND i.BookingBrodara = @Booking ");
                whereExtraRN.Append(" AND rn.BookingBrodara =  @Booking ");                                                                                                                                                                                                                                                                                                     
            }


            select = $@" 

                        WITH Dokumenti AS (
                            SELECT
                                RadniNalogDrumskiID,
                                 COUNT(*) AS BrojDokumenata
                            FROM DokumentaRadnogNalogaDrumski
                            GROUP BY RadniNalogDrumskiID
                        )

                        SELECT 
                                de.ID AS KontejnerID, 
                                de.NalogID AS CNTBroj,
                                ISNULL(de.Prevoznik, '')   + ' / '  + ISNULL(de.Kamioner, '') AS [Prevoznik / Kamioner],
                                de.Klijent as Nalogodavac,
                                pa.ArtikalNaziv,
                                de.Relacija,
                                de.Cena,
                                fds.UlaznaFaktura, 
                                de.BL,
                                de.Booking,
                                CONVERT(varchar,
                                ISNULL(fds.DatumIzmeneUlazne, GETDATE()),104) AS DatumIzmene,
                                de.MestoUtovara,
                                de.MestoIstovara,
                                de.BrojKontejnera,
                                pa.ArtikalSifra,
                                de.TipTransporta,
                                de.AutoDan,
                                de.PDV,
                                de.DodatniTrosakTransporta,
                                de.Uvoz,
                                de.DatumUtovara,
                                de.DtPreuzimanjaPraznogKontejnera,
                                doc.BrojDokumenata AS Dokumenta
                                
                                
                         FROM
                           ( SELECT  
                            rn.ID,
                            rn.NalogID,
                            p.PaNaziv AS Prevoznik,
                            pa.PaNaziv AS Klijent,
                            a.Vozac AS Kamioner,
                            rn.Cena,
                            '' AS BL,
                            i.BookingBrodara AS Booking,
                            mu.Naziv AS MestoUtovara,
                            mi.Naziv AS MestoIstovara,
                            i.BrojKontejnera,
                            LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                            rn.TipTransporta,
                            rn.AutoDan,
                            rn.PDV,
                            rn.DodatniTrosakTransporta,
                            rn.Uvoz, 
                            rn.DatumUtovara,
                            rn.DtPreuzimanjaPraznogKontejnera
                        FROM RadniNalogDrumski rn
                        INNER JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN IzvozKonacna i ON i.ID = rn.KontejnerID
                        LEFT JOIN MestaUtovara mu ON mu.ID = i.MesoUtovara
                        LEFT JOIN MestaUtovara mi ON mi.ID = rn.MestoIstovara
                        LEFT JOIN Partnerji pa ON pa.PaSifra = i.Klijent3
                        LEFT JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        WHERE rn.Uvoz = 0  AND ISNULL(rn.PoslataNajava, 0) = 1  {whereExtra}  {whereExtraIzvoz}

                        UNION ALL
                        -- 2)
                        SELECT rn.ID,
                               rn.NalogID,
                               p.PaNaziv AS Prevoznik,
                               pa.PaNaziv AS Klijent,
                               a.Vozac AS Kamioner,
                               rn.Cena,
                               '' AS BL,
                               i.BookingBrodara AS Booking,
                               mu.Naziv AS MestoUtovara,
                               mi.Naziv AS MestoIstovara,
                               i.BrojKontejnera,
                               LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                               rn.TipTransporta,
                               rn.AutoDan,
                               rn.PDV,
                               rn.DodatniTrosakTransporta,
                               rn.Uvoz, 
                               rn.DatumUtovara,
                               rn.DtPreuzimanjaPraznogKontejnera
                        FROM RadniNalogDrumski rn
                        INNER JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN Izvoz i ON i.ID = rn.KontejnerID
                        LEFT JOIN VrstaVozila vv ON vv.ID = i.Cirada
                        LEFT JOIN MestaUtovara mu ON mu.ID = i.MesoUtovara 
                        LEFT JOIN MestaUtovara mi ON mi.ID = rn.MestoIstovara
                        LEFT JOIN Partnerji pa ON pa.PaSifra = i.Klijent3
                        LEFT JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        WHERE rn.Uvoz = 0 AND ISNULL(rn.PoslataNajava, 0) = 1  {whereExtra}  {whereExtraIzvoz}

                        UNION ALL
                        -- 3)
                        SELECT rn.ID, 
                               rn.NalogID,
                               p.PaNaziv AS Prevoznik,
                               pa.PaNaziv AS Klijent,
                               a.Vozac AS Kamioner,
                               rn.Cena,
                               i.BrodskaTeretnica AS BL,
                               0  AS Booking,
                               mu.Naziv AS MestoUtovara,
                               mi.Naziv AS MestoIstovara,
                               i.BrojKontejnera,
                               LTRIM(RTRIM(mi.Naziv)) + ' - ' +  LTRIM(RTRIM(mu.Naziv)) AS Relacija,
                               rn.TipTransporta,
                               rn.AutoDan,
                               rn.PDV,
                               rn.DodatniTrosakTransporta,
                               rn.Uvoz, 
                               rn.DatumUtovara,
                               rn.DtPreuzimanjaPraznogKontejnera
                        FROM RadniNalogDrumski rn
                        INNER JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN UvozKonacna i ON i.ID = rn.KontejnerID
                        LEFT JOIN VrstaVozila vv ON vv.ID = rn.TipTransporta
                        LEFT JOIN MestaUtovara mu ON mu.ID = rn.MestoUtovara 
                        LEFT JOIN MestaUtovara mi ON mi.ID = i.MestoIstovara
                        LEFT JOIN Partnerji pa ON pa.PaSifra = i.Nalogodavac3
                        LEFT JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        WHERE rn.Uvoz = 1 AND ISNULL(rn.PoslataNajava, 0) = 1 {whereExtra} {whereExtraUvoz} 

                        UNION ALL
                        -- 4)

                        SELECT rn.ID, 
                               rn.NalogID,
                               p.PaNaziv AS Prevoznik,
                               pa.PaNaziv AS Klijent,
                               a.Vozac AS Kamioner,
                               rn.Cena,
                               i.BrodskaTeretnica AS BL,
                               0  AS Booking,
                               mu.Naziv AS MestoUtovara,
                               mi.Naziv AS MestoIstovara,
                               i.BrojKontejnera,
                               LTRIM(RTRIM(mi.Naziv)) + ' - ' +  LTRIM(RTRIM(mu.Naziv)) AS Relacija,
                               rn.TipTransporta,
                               rn.AutoDan,
                               rn.PDV,
                               rn.DodatniTrosakTransporta,
                               rn.Uvoz, 
                               rn.DatumUtovara,
                               rn.DtPreuzimanjaPraznogKontejnera
                              
                        FROM RadniNalogDrumski rn
                        INNER JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN Uvoz i ON i.ID = rn.KontejnerID
                        LEFT JOIN VrstaVozila vv ON vv.ID = rn.TipTransporta
                        LEFT JOIN MestaUtovara mu ON mu.ID = rn.MestoUtovara 
                        LEFT JOIN MestaUtovara mi ON mi.ID = i.MestoIstovara
                        LEFT JOIN Partnerji pa ON pa.PaSifra = i.Nalogodavac3
                        LEFT JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        WHERE rn.Uvoz = 1 AND ISNULL(rn.PoslataNajava, 0) = 1 {whereExtra}  {whereExtraUvoz} 

                        UNION ALL

                           SELECT rn.ID, 
                               rn.NalogID,
                               p.PaNaziv AS Prevoznik,
                               pa.PaNaziv AS Klijent,
                               a.Vozac AS Kamioner,
                               rn.Cena,
                               rn.BrodskaTeretnica AS BL,
                               rn.BookingBrodara AS Booking,
                               mu.Naziv AS MestoUtovara,
                               mi.Naziv AS MestoIstovara,
                               rn.BrojKontejnera as BrojKontejnera,
                               CASE WHEN rn.Uvoz IN (1, 2, 4) 
                                            THEN LTRIM(RTRIM(mi.Naziv)) +' - ' + LTRIM(RTRIM(mu.Naziv))
                                    WHEN rn.Uvoz IN (0, 3, 5) 
                                            THEN LTRIM(RTRIM(mu.Naziv)) + ' - ' + LTRIM(RTRIM(mi.Naziv))
                                    ELSE ''
                               END AS Relacija,
                               rn.TipTransporta,
                               rn.AutoDan,
                               rn.PDV,
                               rn.DodatniTrosakTransporta,
                               rn.Uvoz, 
                               rn.DatumUtovara,
                               rn.DtPreuzimanjaPraznogKontejnera
                              
                        FROM RadniNalogDrumski rn
                        LEFT JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
                        LEFT JOIN MestaUtovara mu ON mu.ID = rn.MestoUtovara 
                        LEFT JOIN MestaUtovara mi ON mi.ID = rn.MestoIstovara
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN Partnerji pa ON pa.PaSifra = rn.Klijent
                        LEFT JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        WHERE rn.Uvoz in (-1,2, 3, 4, 5) AND ISNULL(rn.PoslataNajava, 0) = 1 {whereExtraRN}
                ) de 
                LEFT JOIN PraviloArtikla pa ON pa.TipNalogaID = de.Uvoz
                                         AND pa.TipTransportaID = de.TipTransporta
                                         AND ISNULL(pa.AutoDan, 0) = ISNULL(de.AutoDan, 0)
                                         AND ISNULL(pa.PDV, 0) = ISNULL(de.PDV, 0)
                                         AND ISNULL(pa.DodatniTrosak, 0)  = ISNULL(de.DodatniTrosakTransporta, 0) 
                LEFT JOIN FakturaDrumski fd ON fd.RadniNalogDrumskiID = de.ID
                LEFT JOIN FakturaDrumskiStavka fds ON fds.FaktureDrumskogID = fd.ID and fds.TipFakture = 1
                LEFT JOIN Dokumenti doc  ON doc.RadniNalogDrumskiID = de.ID
                ";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(connection);



            if (!string.IsNullOrWhiteSpace(txtKontejnerID.Text))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int)
                              .Value = Convert.ToInt32(txtKontejnerID.Text);
            }

            if (!string.IsNullOrWhiteSpace(txtBrojKontejnera.Text))
            {
                cmd.Parameters.Add("@BrojKontejnera", SqlDbType.NVarChar, 30)
                                 .Value = txtBrojKontejnera.Text.Trim();
            }

            if (!string.IsNullOrWhiteSpace(txtBL.Text))
            {
                cmd.Parameters.Add("@BL", SqlDbType.NVarChar, 50)
                              .Value = txtBL.Text.Trim();
            }

            if (!string.IsNullOrWhiteSpace(txtBooking.Text))
            {
                cmd.Parameters.Add("@Booking", SqlDbType.NVarChar, 50)
                              .Value = txtBooking.Text.Trim();
            }

            cmd.CommandText = select;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

           

            brojRedova = ds.Tables[0].Rows.Count;
            dtRadniNalozi = ds.Tables[0];

            // odlučivanje koji panel je vidljiv
            if (brojRedova > 1)
            {
                panel2.Visible = true;
                panel3.Visible = false;
                btnIzadjiBezPromena.Visible = true;

            }
            else if (brojRedova == 1)
            {
                panel2.Visible = false;
                panel3.Visible = true;
                btnIzadjiBezPromena.Visible = false;

                DataRow red = dtRadniNalozi.Rows[0];
                int id = Convert.ToInt32(red["KontejnerID"]);

                PrikaziDetalje(id);
            }
            else 
            {
                panel2.Visible = false;
                panel3.Visible = false;
            }

            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            // Ukloni kolone koje ne želiš da se vide
            var colsToRemove = new[] { "MestoUtovara", "MestoIstovara", "BrojKontejnera", "ArtikalSifra", "TipTransporta", "AutoDan", "PDV", "DodatniTrosakTransporta", "Uvoz", "DatumUtovara", "DtPreuzimanjaPraznogKontejnera" }; // "Status" je Naziv
            foreach (var col in colsToRemove)
            {
                if (gridGroupingControl1.TableDescriptor.VisibleColumns.Contains(col))
                {
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Remove(col);
                }
            }


            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(gridGroupingControl1);
        }

        private void Grid_TableControl_CellDoubleClick(object sender, Syncfusion.Windows.Forms.Grid.GridCellClickEventArgs e)
        {
            // e.Inner.RowIndex > 0 znači da je kliknut red, ne header
            var record = gridGroupingControl1.Table.CurrentRecord as GridRecord;
            if (record != null)
            {
                int id = Convert.ToInt32(record.GetValue("KontejnerID")); // rn.ID
                PrikaziDetalje(id);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void gridGroupingControl1_DoubleClick(object sender, EventArgs e)
        {
            var record = gridGroupingControl1.Table.CurrentRecord as GridRecord;
            if (record != null)
            {
                int id = Convert.ToInt32(record.GetValue("KontejnerID")); // rn.ID
                PrikaziDetalje(id);
            }
        }

        private void PrikaziDetalje(int radniNalogID)
        {
            // Iz memorisane tabele uzmi red
            DataRow[] rows = dtRadniNalozi.Select($"KontejnerID = {radniNalogID}");
            RadniNalogID = radniNalogID;
            if (rows.Length == 1)
            {
                var red = rows[0];

                // Popuni kontrole u panel3
                txtKamioner.Text = red["Prevoznik / Kamioner"].ToString();
                txtNalogodavac.Text = red["Nalogodavac"].ToString();
                txtCenaTransporta.Text = red["Cena"].ToString();
                txtRelacija.Text = red["Relacija"].ToString();
                txtArtikal.Text = red["ArtikalNaziv"].ToString();
                txtBrojUlazneFakture.Text = red["UlaznaFaktura"].ToString();
                txtPrilozenaDokumenta.Text = red["Dokumenta"].ToString();

                if (red["DatumIzmene"] != DBNull.Value)
                {
                    dtpPregleda.Value = Convert.ToDateTime(red["DatumIzmene"]);
                }
                
                // cerada (TipTransporta = 2) onda je datumUtovara u suprotnom DtPreuzimanjaPraznogKontejnera
                if (int.TryParse(red["TipTransporta"]?.ToString(), out int tip) && tip == 2)
                {
                    if (red["DatumUtovara"] != DBNull.Value)
                    {
                        dtpPrometa.Value = Convert.ToDateTime(red["DatumUtovara"]);
                    }
                }
                else
                {
                    if (red["DtPreuzimanjaPraznogKontejnera"] != DBNull.Value)
                    {
                        dtpPrometa.Value = Convert.ToDateTime(red["DtPreuzimanjaPraznogKontejnera"]);
                    }
                }
                panel2.Visible = false;
                panel3.Visible = true;
            }
        }

        private void detaljiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var record = gridGroupingControl1.Table.CurrentRecord;
            if (record == null) return;


            object idObj = record.GetValue("KontejnerID");
            if (idObj == null || idObj == DBNull.Value)
            {
                MessageBox.Show("ID je nevažeći.");
                return;
            }

            int id = Convert.ToInt32(idObj);
            PrikaziDetalje(id);
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void GridGroupingControl1_TableControlMouseDown(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlMouseEventArgs e)
        {

            if (System.Windows.Forms.Control.MouseButtons == MouseButtons.Right)
            {
                // Dobavljanje pozicije kliknutog reda i kolone
                // Pronađi red i kolonu pod mišem
                int rowIndex, colIndex;
                e.TableControl.PointToRowCol(new System.Drawing.Point(e.Inner.X, e.Inner.Y), out rowIndex, out colIndex);

                // Uzmi stil kliknutog polja
                GridTableCellStyleInfo style = e.TableControl.GetTableViewStyleInfo(rowIndex, colIndex);

                // Proveri da li je kliknuto u redu sa podacima
                if (style.TableCellIdentity.DisplayElement.Kind == DisplayElementKind.Record)
                {
                    // Postavi aktivni red
                    this.gridGroupingControl1.Table.CurrentRecord = style.TableCellIdentity.DisplayElement.ParentRecord;

                    // Prikaži context menu na poziciji miša
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

            int? ulaznaID = null;
            int? fakturaDrumskogID = null;
            int izmena = 0;
            int FakturaID = -1;

            // 1. Provera da li postoje zapisi
            using (var con = new SqlConnection(s_connection))
            using (var cmd = new SqlCommand(@"
            SELECT MAX(rn.ID) as ID,
                   MAX(CASE WHEN f.TipFakture = 1 THEN f.ID END) AS UlaznaID
            FROM FakturaDrumski rn LEFT JOIN FakturaDrumskiStavka f on f.FaktureDrumskogID = rn.ID
            WHERE rn.RadniNalogDrumskiID = @fid;", con))
            {
                cmd.Parameters.AddWithValue("@fid", @RadniNalogID);

                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        ulaznaID = dr["UlaznaID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["UlaznaID"]);
                        fakturaDrumskogID = dr["ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["ID"]);
                        FakturaID = dr["ID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["ID"]);
                    }
                }
            }

            var ins = new InsertFakture();
            int zaposleni = PostaviVrednostZaposleni();

            // 2. ULAZNA FAKTURA

            if (ulaznaID == null)
            {
                // Insert
                ins.InsStavkeFakture(1, fakturaDrumskogID, null, txtBrojUlazneFakture.Text.Trim(), null, null, DateTime.Now, zaposleni);
                izmena = 1;
            }
            else
            {
                // Update
                ins.UpdateFakturaDrumskiStavka(1, ulaznaID, txtBrojUlazneFakture.Text.Trim(), null, null, DateTime.Now, zaposleni);
                izmena = 1;
            }

            if (izmena == 1)
                MessageBox.Show("Podaci su uspešno sačuvani.");

            if (brojRedova > 1)
            {
                panel3.Visible = false;
                panel2.Visible = true;
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
        private void frmObradaUlaznihFaktura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                SendKeys.Send("{TAB}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (brojRedova > 1)
            {
                panel3.Visible = false;
                panel2.Visible = true;
            }
        }      
    }
}
