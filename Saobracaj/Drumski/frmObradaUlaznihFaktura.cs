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

        public frmObradaUlaznihFaktura()
        {
            InitializeComponent();
            ChangeTextBox();
            panel2.Visible = false;
            panel3.Visible = false;
            gridGroupingControl1.TableControl.CellDoubleClick += Grid_TableControl_CellDoubleClick;
            this.KeyPreview = true;
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


            select = $@" SELECT de.ID AS KontejnerID, 
                                de.NalogID AS CNTBroj, 
                                de.Prevoznik, 
                                de.Klijent,
                                de.Kamioner,
                                de.Cena,
                                de.BL,
                                de.Booking,
                                de.MestoUtovara,
                                de.MestoIstovara,
                                de.BrojKontejnera,
                                de.Relacija,
                                pa.ArtikalSifra, pa.ArtikalNaziv,
                                de.TipTransporta,
                                de.AutoDan,
                                de.PDV,
                                de.DodatniTrosakTransporta,
                                de.Uvoz,
                                de.DatumUtovara,
                                de.DtPreuzimanjaPraznogKontejnera
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
               
            }
            else if(brojRedova == 1)
            {
                panel2.Visible = false;
                panel3.Visible = true;
               
                DataRow red = dtRadniNalozi.Rows[0];
                int id = Convert.ToInt32(red["KontejnerID"]);

                PrikaziDetalje(id);
            }

            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
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
            if (rows.Length == 1)
            {
                var red = rows[0];

                // Popuni kontrole u panel3
                txtKamioner.Text = red["Prevoznik"].ToString();
                txtNalogodavac.Text = red["Klijent"].ToString();
                txtCenaTransporta.Text = red["Cena"].ToString();
                txtRelacija.Text = red["Relacija"].ToString();
                txtArtikal.Text = red["ArtikalNaziv"].ToString();

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
            panel3.Visible = false;
            panel2.Visible = true;
        }

        private void frmObradaUlaznihFaktura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                SendKeys.Send("{TAB}");
            }
        }
    }
}
