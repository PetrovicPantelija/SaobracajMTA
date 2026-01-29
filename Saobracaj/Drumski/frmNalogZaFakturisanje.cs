using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Grid;
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
    public partial class frmNalogZaFakturisanje: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private DataTable dtRadniNalozi;
        int brojRedova = 0;
        private int RadniNalogID;
        int brojDokumenata;
        public frmNalogZaFakturisanje(List<int> tipoviIn, List<int> tipoviNotIn)
        {
            InitializeComponent();
            ChangeTextBox();
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

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshGrid();
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
                           fds.UlaznaFaktura AS BrojUlaznogRačuna, 
                           de.BrojKontejnera AS CNTBroj,
                           de.NalogID AS Nalog,
                           ISNULL(de.Prevoznik, '')   + ' / '  + ISNULL(de.Kamioner, '') AS [Prevoznik / Kamioner],
                           de.Klijent as Nalogodavac,
                           pa.ArtikalNaziv,
                           de.Relacija,
                           de.Cena,
                           de.BL,
                           de.Booking,
                           ISNULL(fds.DatumIzmeneUlazne, GETDATE()) AS DatumIzmene,
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
                          ISNULL( doc.BrojDokumenata,0) AS Dokumenta
                           
                           
                    FROM
                      ( SELECT  
                       rn.ID,
                       LTRIM(RTRIM(p.PaNaziv)) AS Prevoznik,
                       pa.PaNaziv AS Klijent,
                       LTRIM(RTRIM(a.Vozac)) AS Kamioner,
                       rn.Trosak AS Cena,
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
                       rn.DtPreuzimanjaPraznogKontejnera,
                       rn.NalogID
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
                          LTRIM(RTRIM(p.PaNaziv)) AS Prevoznik,
                          pa.PaNaziv AS Klijent,
                          LTRIM(RTRIM(a.Vozac)) AS Kamioner,
                          rn.Trosak AS Cena,
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
                          rn.DtPreuzimanjaPraznogKontejnera,
                          rn.NalogID
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
                          LTRIM(RTRIM(p.PaNaziv)) AS Prevoznik,
                          pa.PaNaziv AS Klijent,
                          LTRIM(RTRIM(a.Vozac)) AS Kamioner,
                          rn.Trosak AS Cena,
                          i.BrodskaTeretnica AS BL,
                          0  AS Booking,
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
                          rn.DtPreuzimanjaPraznogKontejnera,
                          rn.NalogID
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
                          LTRIM(RTRIM(p.PaNaziv)) AS Prevoznik,
                          pa.PaNaziv AS Klijent,
                          LTRIM(RTRIM(a.Vozac)) AS Kamioner,
                          rn.Trosak AS Cena,
                          i.BrodskaTeretnica AS BL,
                          0  AS Booking,
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
                          rn.DtPreuzimanjaPraznogKontejnera,
                          rn.NalogID
                         
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
                          LTRIM(RTRIM(p.PaNaziv)) AS Prevoznik,
                          pa.PaNaziv AS Klijent,
                          LTRIM(RTRIM(a.Vozac)) AS Kamioner,
                          rn.Trosak AS Cena,
                          rn.BrodskaTeretnica AS BL,
                          rn.BookingBrodara AS Booking,
                          mu.Naziv AS MestoUtovara,
                          mi.Naziv AS MestoIstovara,
                          rn.BrojKontejnera as BrojKontejnera,
                          LTRIM(RTRIM(mu.Naziv)) + ' - ' + LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                          rn.TipTransporta,
                          rn.AutoDan,
                          rn.PDV,
                          rn.DodatniTrosakTransporta,
                          rn.Uvoz, 
                          rn.DatumUtovara,
                          rn.DtPreuzimanjaPraznogKontejnera,
                          rn.NalogID
                         
                   FROM RadniNalogDrumski rn
                   INNER JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
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
           WHERE ISNULL(doc.BrojDokumenata,0)>0 AND fds.UlaznaFaktura IS NOT NULL  AND fds.UlaznaFaktura <> ''
           ORDER BY de.ID desc
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

            if (brojRedova == 0)
            {
        
                MessageBox.Show(
                    "Nijedan zapis nije pronađen za zadate kriterijume.",
                    "Obaveštenje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl1);

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);

            // resize po sadržaju (ne samo header)
            gridGroupingControl1.TableModel.ColWidths.ResizeToFit(
                GridRangeInfo.Table(),
                GridResizeToFitOptions.IncludeHeaders
            );

            foreach (GridColumnDescriptor col in gridGroupingControl1.TableDescriptor.Columns)
            {
                if (col.Width < 90) col.Width = 60;
                if (col.Width > 350) col.Width = 350;
            }

            gridGroupingControl1.TableDescriptor.Columns["DatumIzmene"].Appearance.AnyRecordFieldCell.CellType = "Static";
            gridGroupingControl1.TableDescriptor.Columns["DatumIzmene"].Appearance.AnyRecordFieldCell.CellType = "TextBox";
            gridGroupingControl1.TableDescriptor.Columns["DatumIzmene"].Appearance.AnyRecordFieldCell.Format = "dd.MM.yyyy";

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

        }
    }
}
