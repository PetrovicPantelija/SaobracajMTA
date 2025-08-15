using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Grouping;
using System.Xml;
using System.Net.Http;
using Saobracaj.Dokumenta;

namespace Saobracaj.Drumski
{
    public partial class frmPregledFaktura: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private SqlDataAdapter dataAdapter;
        private System.Data.DataTable mainTable;

        public frmPregledFaktura()
        {
            InitializeComponent();
            ChangeTextBox();
            RefreshGrid();
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {
                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


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
                panelHeader.Visible = false;

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
     
        private void RefreshGrid()
        {
            var s_connection = Sifarnici.frmLogovanje.connectionString;
            var connection = new SqlConnection(s_connection);
            using (connection)
            {
                connection.Open();

                var select = $@"
                           ;WITH F AS
                        (
                            SELECT 
                                fd.RadniNalogDrumskiID AS RNID,
                                MAX(CASE WHEN fs.TipFakture = 0 THEN fs.IzlaznaFaktura END) AS IzlaznaFaktura,
                                MAX(CASE WHEN fs.TipFakture = 1 THEN fs.UlaznaFaktura END)  AS UlaznaFaktura,
                                MAX(CASE WHEN fs.TipFakture = 1 THEN fs.BeleskaUlazneFakture END) AS BeleskaUlazneFakture
                            FROM FakturaDrumski fd
                            LEFT JOIN FakturaDrumskiStavka fs ON fs.FaktureDrumskogID = fd.ID
                            GROUP BY fd.RadniNalogDrumskiID
                        )
                        -- 1)
                        SELECT  
                            rn.ID,
                            rn.NalogID,
                            CASE  WHEN EXISTS ( SELECT 1  FROM DokumentaRadnogNalogaDrumski d  WHERE d.RadniNalogDrumskiID = rn.ID ) THEN 'TS'ELSE '' END AS TS,
                            F.UlaznaFaktura,
                            F.BeleskaUlazneFakture,
                            p.PaNaziv AS Prevoznik,
                            pa.PaNaziv AS Klijent,
                            rn.Ref AS Referenca,
                            ik.BookingBrodara AS Booking,
                            mu.Naziv AS MestoUtovara,
                            mi.Naziv AS MestoIstovara,
                            CONVERT(varchar, rn.DatumUtovara,104) AS DatumUtovara,
                            CONVERT(varchar, rn.DatumIstovara,104) AS DatumIstovara,
                            vv.Naziv AS TipTransporta,
                            CASE 
                                WHEN rn.Uvoz = 0 THEN 'Izvoz'
                                WHEN rn.Uvoz = 1 THEN 'Uvoz'
                                WHEN rn.Uvoz = 2 THEN '3PI'
                                WHEN rn.Uvoz = 3 THEN '3PU'
                                ELSE ''
                            END AS TipKupca,
                            a.RegBr,
                            ik.BrojKontejnera,
                            '' AS BrojKontejnera2,
                            v.NAzivVoza AS BrojVoza,
                            CASE WHEN ISNULL(rn.PDV, 0) = 1 THEN 'Da' ELSE 'Ne' END AS PDV,
                            rn.Trosak,
                            rn.Cena,
                            rn.Valuta,
                            rn.DatumKreiranjaNaloga,
                            F.IzlaznaFaktura,
                            CASE WHEN F.IzlaznaFaktura IS NOT NULL THEN CONVERT(varchar, rn.DatumIstovara,104)  ELSE  NULL END AS DatumPrometa,
                            a.ID as KamionID
                        FROM RadniNalogDrumski rn
                        INNER JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN IzvozKonacna ik ON ik.ID = rn.KontejnerID
                        LEFT JOIN MestaUtovara mu ON mu.ID = ik.MesoUtovara
                        LEFT JOIN MestaUtovara mi ON mi.ID = rn.MestoIstovara
                        LEFT JOIN Partnerji pa ON pa.PaSifra = ik.Klijent3
                        LEFT JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        LEFT JOIN TipKontenjera tk ON ik.VrstaKontejnera = tk.ID
                        LEFT JOIN VrstaVozila vv ON vv.ID = ik.Cirada
                        LEFT JOIN IzvozKonacnaZaglavlje ukz ON ukz.ID = ik.IDNadredjena
                        LEFT JOIN Voz v ON v.ID = ukz.IDVoza
                        LEFT JOIN F ON F.RNID = rn.ID
                        WHERE rn.Uvoz = 0  AND ISNULL(rn.PoslataNajava, 0) = 1   

                        UNION ALL
                        -- 2)
                        SELECT rn.ID,
                               rn.NalogID,
                               CASE  WHEN EXISTS ( SELECT 1  FROM DokumentaRadnogNalogaDrumski d  WHERE d.RadniNalogDrumskiID = rn.ID ) THEN 'TS'ELSE '' END AS TS,
                               F.UlaznaFaktura,
                               F.BeleskaUlazneFakture,
                               p.PaNaziv AS Prevoznik,
                               pa.PaNaziv AS Klijent,
                               rn.Ref AS Referenca,
                               i.BookingBrodara AS Booking,
                               mu.Naziv AS MestoUtovara,
                               mi.Naziv AS MestoIstovara,
                               CONVERT(varchar, rn.DatumUtovara,104) AS DatumUtovara,
                               CONVERT(varchar, rn.DatumIstovara,104) AS DatumIstovara,
                               vv.Naziv AS TipTransporta,
                               CASE 
                                    WHEN rn.Uvoz = 0 THEN 'Izvoz'
                                    WHEN rn.Uvoz = 1 THEN 'Uvoz'
                                    WHEN rn.Uvoz = 2 THEN '3PI'
                                    WHEN rn.Uvoz = 3 THEN '3PU'
                                    ELSE ''
                               END AS TipKupca,
                               a.RegBr,
                               i.BrojKontejnera,
                               '' AS BrojKontejnera2,
                               '' AS BrojVoza,
                               CASE WHEN ISNULL(rn.PDV, 0) = 1 THEN 'Da' ELSE 'Ne' END AS PDV,
                               rn.Trosak,
                               rn.Cena,
                               rn.Valuta,
                               rn.DatumKreiranjaNaloga,
                               F.IzlaznaFaktura,
                               CASE WHEN F.IzlaznaFaktura IS NOT NULL THEN CONVERT(varchar, rn.DatumIstovara,104)  ELSE  NULL END AS DatumPrometa,
                               a.ID as KamionID
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
                        LEFT JOIN TipKontenjera tk ON i.VrstaKontejnera = tk.ID
                        LEFT JOIN F ON F.RNID = rn.ID
                        WHERE rn.Uvoz = 0 AND ISNULL(rn.PoslataNajava, 0) = 1  

                        UNION ALL
                        -- 3)
                        SELECT rn.ID, 
                               rn.NalogID,
                               CASE  WHEN EXISTS ( SELECT 1  FROM DokumentaRadnogNalogaDrumski d  WHERE d.RadniNalogDrumskiID = rn.ID ) THEN 'TS'ELSE '' END AS TS,
                               F.UlaznaFaktura,
                               F.BeleskaUlazneFakture,
                               p.PaNaziv AS Prevoznik,
                               pa.PaNaziv AS Klijent,
                               uk.Ref3 AS Referenca,
                               0  AS Booking,
                               mu.Naziv AS MestoUtovara,
                               mi.Naziv AS MestoIstovara,
                               CONVERT(varchar, rn.DatumUtovara,104) AS DatumUtovara,
                               CONVERT(varchar, rn.DatumIstovara,104) AS DatumIstovara,
                               vv.Naziv AS TipTransporta,
                               CASE 
                                    WHEN rn.Uvoz = 0 THEN 'Izvoz'
                                    WHEN rn.Uvoz = 1 THEN 'Uvoz'
                                    WHEN rn.Uvoz = 2 THEN '3PI'
                                    WHEN rn.Uvoz = 3 THEN '3PU'
                                    ELSE ''
                               END AS TipKupca,
                               a.RegBr,
                               uk.BrojKontejnera,
                               '' AS BrojKontejnera2,
                               v.NAzivVoza AS BrojVoza,
                               CASE WHEN ISNULL(rn.PDV, 0) = 1 THEN 'Da' ELSE 'Ne' END AS PDV,
                               rn.Trosak,
                               rn.Cena,
                               rn.Valuta,
                               rn.DatumKreiranjaNaloga,
                               F.IzlaznaFaktura,
                               CASE WHEN F.IzlaznaFaktura IS NOT NULL THEN CONVERT(varchar, rn.DatumIstovara,104)  ELSE  NULL END AS DatumPrometa,
                               a.ID as KamionID
                        FROM RadniNalogDrumski rn
                        INNER JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN UvozKonacna uk ON uk.ID = rn.KontejnerID
                        LEFT JOIN VrstaVozila vv ON vv.ID = rn.TipTransporta
                        LEFT JOIN MestaUtovara mu ON mu.ID = rn.MestoUtovara 
                        LEFT JOIN MestaUtovara mi ON mi.ID = uk.MestoIstovara
                        LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3
                        LEFT JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        LEFT JOIN TipKontenjera tk ON uk.TipKontejnera = tk.ID
                        LEFT JOIN UvozKonacnaZaglavlje ukz ON ukz.ID = uk.IDNadredjeni 
                        LEFT JOIN Voz v ON v.ID = ukz.IDVoza
                        LEFT JOIN F ON F.RNID = rn.ID
                        WHERE rn.Uvoz = 1 AND ISNULL(rn.PoslataNajava, 0) = 1  

                        UNION ALL
                        -- 4)
                        SELECT rn.ID, 
                               rn.NalogID,
                               CASE  WHEN EXISTS ( SELECT 1  FROM DokumentaRadnogNalogaDrumski d  WHERE d.RadniNalogDrumskiID = rn.ID ) THEN 'TS'ELSE '' END AS TS,
                               F.UlaznaFaktura,
                               F.BeleskaUlazneFakture,
                               p.PaNaziv AS Prevoznik,
                               pa.PaNaziv AS Klijent,
                               uk.Ref3 AS Referenca,
                               0  AS Booking,
                               mu.Naziv AS MestoUtovara,
                               mi.Naziv AS MestoIstovara,
                               CONVERT(varchar, rn.DatumUtovara,104) AS DatumUtovara,
                               CONVERT(varchar, rn.DatumIstovara,104) AS DatumIstovara,
                               vv.Naziv AS TipTransporta,
                               CASE 
                                    WHEN rn.Uvoz = 0 THEN 'Izvoz'
                                    WHEN rn.Uvoz = 1 THEN 'Uvoz'
                                    WHEN rn.Uvoz = 2 THEN '3PI'
                                    WHEN rn.Uvoz = 3 THEN '3PU'
                                    ELSE ''
                               END AS TipKupca,
                               a.RegBr,
                               uk.BrojKontejnera,
                               '' AS BrojKontejnera2,
                               '' as BrojVoza,
                               CASE WHEN ISNULL(rn.PDV, 0) = 1 THEN 'Da' ELSE 'Ne' END AS PDV,
                               rn.Trosak,
                               rn.Cena,
                               rn.Valuta,
                               rn.DatumKreiranjaNaloga,
                               F.IzlaznaFaktura,
                               CASE WHEN F.IzlaznaFaktura IS NOT NULL THEN CONVERT(varchar, rn.DatumIstovara,104)  ELSE  NULL END AS DatumPrometa,
                               a.ID as KamionID
                        FROM RadniNalogDrumski rn
                        INNER JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        INNER JOIN Uvoz uk ON uk.ID = rn.KontejnerID
                        LEFT JOIN VrstaVozila vv ON vv.ID = rn.TipTransporta
                        LEFT JOIN MestaUtovara mu ON mu.ID = rn.MestoUtovara 
                        LEFT JOIN MestaUtovara mi ON mi.ID = uk.MestoIstovara
                        LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3
                        LEFT JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        LEFT JOIN TipKontenjera tk ON uk.TipKontejnera = tk.ID
                        LEFT JOIN F ON F.RNID = rn.ID
                        WHERE rn.Uvoz = 1 AND ISNULL(rn.PoslataNajava, 0) = 1  

                        UNION ALL
                        -- 5)
                        SELECT rn.ID, 
                               rn.NalogID,
                               CASE  WHEN EXISTS ( SELECT 1  FROM DokumentaRadnogNalogaDrumski d  WHERE d.RadniNalogDrumskiID = rn.ID ) THEN 'TS'ELSE '' END AS TS,
                               F.UlaznaFaktura,
                               F.BeleskaUlazneFakture,
                               p.PaNaziv AS Prevoznik,
                               pa.PaNaziv AS Klijent,
                               rn.Ref AS Referenca,
                               rn.BookingBrodara AS Booking,
                               mu.Naziv AS MestoUtovara,
                               mi.Naziv AS MestoIstovara,
                               CONVERT(varchar, rn.DatumUtovara,104) AS DatumUtovara,
                               CONVERT(varchar, rn.DatumIstovara,104) AS DatumIstovara,
                               vv.Naziv AS TipTransporta,
                               CASE 
                                    WHEN rn.Uvoz = 0 THEN 'Izvoz'
                                    WHEN rn.Uvoz = 1 THEN 'Uvoz'
                                    WHEN rn.Uvoz = 2 THEN '3PI'
                                    WHEN rn.Uvoz = 3 THEN '3PU'
                                    ELSE ''
                               END AS TipKupca,
                               a.RegBr,
                               rn.BrojKontejnera as BrojKontejnera,
                               rn.BrojKontejnera2 AS BrojKontejnera2,
                               rn.BrojVoza as BrojVoza,
                               CASE WHEN ISNULL(rn.PDV, 0) = 1 THEN 'Da' ELSE 'Ne' END AS PDV,
                               rn.Trosak,
                               rn.Cena,
                               rn.Valuta,
                               rn.DatumKreiranjaNaloga,
                               F.IzlaznaFaktura,
                               CASE WHEN F.IzlaznaFaktura IS NOT NULL THEN CONVERT(varchar, rn.DatumIstovara,104)  ELSE  NULL END AS DatumPrometa,
                               a.ID as KamionID
                        FROM RadniNalogDrumski rn
                        INNER JOIN FakturaDrumski fd ON rn.ID = fd.RadniNalogDrumskiID
                        LEFT JOIN MestaUtovara mu ON mu.ID = rn.MestoUtovara 
                        LEFT JOIN MestaUtovara mi ON mi.ID = rn.MestoIstovara
                        LEFT JOIN Automobili a ON rn.KamionID = a.ID
                        LEFT JOIN StatusVozila sv ON sv.ID = rn.Status
                        LEFT JOIN Partnerji pa ON pa.PaSifra = rn.Klijent
                        LEFT JOIN Partnerji p on  a.PartnerID = p.PaSifra
                        LEFT JOIN VrstaVozila vv ON vv.ID = rn.TipTransporta
                        LEFT JOIN F ON F.RNID = rn.ID
                        WHERE rn.Uvoz in (-1,2, 3) AND ISNULL(rn.PoslataNajava, 0) = 1 
                        ORDER BY ID DESC";

                dataAdapter = new SqlDataAdapter(select, connection);
                var commandBuilder = new SqlCommandBuilder(dataAdapter);

                var ds = new DataSet();
                dataAdapter.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    mainTable = ds.Tables[0];

                    // Dodaj kolone ako ne postoje
                    void DodajKolonu(string naziv)
                    {
                        if (!mainTable.Columns.Contains(naziv))
                            mainTable.Columns.Add(naziv, typeof(decimal));
                    }

                    DodajKolonu("CenaEUR");
                    DodajKolonu("CenaRSD");
                    DodajKolonu("TrosakEUR");
                    DodajKolonu("TrosakRSD");

                    var kursCache = new Dictionary<(string valuta, DateTime datum), decimal>();

                    using (var kursCommand = connection.CreateCommand())
                    {
                        kursCommand.CommandText = @" SELECT SrednjiKurs 
                                                     FROM KursnaLista
                                                     WHERE Datum = @Datum AND Valuta = @Valuta";

                        kursCommand.Parameters.Add(new SqlParameter("@Datum", SqlDbType.Date));
                        kursCommand.Parameters.Add(new SqlParameter("@Valuta", SqlDbType.VarChar, 3));

                        foreach (DataRow row in mainTable.Rows)
                        {
                            string valuta = row["Valuta"]?.ToString()?.Trim() ?? "RSD";
                            decimal cena = row["Cena"] != DBNull.Value ? Convert.ToDecimal(row["Cena"]) : 0;
                            decimal trosak = row["Trosak"] != DBNull.Value ? Convert.ToDecimal(row["Trosak"]) : 0;

                            if (!DateTime.TryParse(row["DatumKreiranjaNaloga"]?.ToString(), out DateTime datumKursa))
                                continue;

                            if (valuta == "RSD")
                            {
                                row["CenaRSD"] = cena;
                                row["TrosakRSD"] = trosak;
                                row["CenaEUR"] = 0;
                                row["TrosakEUR"] = 0;
                                continue;
                            }

                            // Ključ za keš
                            var key = (valuta, datumKursa.Date);
                            if (!kursCache.TryGetValue(key, out decimal kurs))
                            {
                                // Nije u kešu – uzmi iz baze
                                kursCommand.Parameters["@Datum"].Value = datumKursa.Date;
                                kursCommand.Parameters["@Valuta"].Value = valuta;

                                using (var reader = kursCommand.ExecuteReader())
                                {
                                    if (reader.Read())
                                        kurs = reader.GetDecimal(0);
                                }

                                kursCache[key] = kurs; // Ubaci i ako je 0, da ne pokušava opet
                            }

                            if (kurs == 0)
                                continue;

                            row["CenaRSD"] = Math.Round(cena * kurs, 2);
                            row["TrosakRSD"] = Math.Round(trosak * kurs,2);
                            row["CenaEUR"] = valuta == "EUR" ? cena : 0;
                            row["TrosakEUR"] = valuta == "EUR" ? trosak : 0;
                        }
                    }

                    gridGroupingControl1.DataSource = mainTable;

                    if (mainTable.Columns.Contains("BrojVoza"))
                    {
                        var index = mainTable.Columns["BrojVoza"].Ordinal;
                        mainTable.Columns["CenaEUR"].SetOrdinal(index + 1);
                        mainTable.Columns["CenaRSD"].SetOrdinal(index + 2);
                        mainTable.Columns["TrosakEUR"].SetOrdinal(index + 3);
                        mainTable.Columns["TrosakRSD"].SetOrdinal(index + 4);
                    }
                    // Filter bar
                    gridGroupingControl1.ShowGroupDropArea = true;
                    gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

                    foreach (GridColumnDescriptor column in gridGroupingControl1.TableDescriptor.Columns)
                    {
                        column.AllowFilter = true;
                    }
                     
                    var td = gridGroupingControl1.TableDescriptor;

                    td.VisibleColumns.Add("CenaEUR");
                    td.VisibleColumns.Add("CenaRSD");
                    td.VisibleColumns.Add("TrosakEUR");
                    td.VisibleColumns.Add("TrosakRSD");

                    if (td.Columns.Contains("ID"))
                    {
                        td.VisibleColumns.Remove("ID"); // Skida je sa prikaza
                    }

                    if (td.Columns.Contains("DatumKreiranjaNaloga"))
                    {
                        td.VisibleColumns.Remove("DatumKreiranjaNaloga");
                    }
                    if (td.Columns.Contains("KamionID"))
                    {
                        td.VisibleColumns.Remove("KamionID");
                    }
                    if (td.Columns.Contains("Cena"))
                    {
                        td.VisibleColumns.Remove("Cena");
                    }
                    if (td.Columns.Contains("Trosak"))
                    {
                        td.VisibleColumns.Remove("Trosak");
                    }
                    if (td.Columns.Contains("Valuta"))
                    {
                        td.VisibleColumns.Remove("Valuta");
                    }

                }
            }
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


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var record = gridGroupingControl1.Table.CurrentRecord;
            if (record == null) return;


            object idObj = record.GetValue("ID");
            if (idObj == null || idObj == DBNull.Value)
            {
                MessageBox.Show("ID je nevažeći.");
                return;
            }

            int id = Convert.ToInt32(idObj); // ili: var id = (int)idObj;

            int nalogId = Convert.ToInt32(record.GetValue("NalogID"));

            string klijent = Convert.ToString(record.GetValue("Klijent"));

            using (var frm = new frmFaktureDetalji(id, nalogId, klijent))
                frm.ShowDialog();

        }


        public async Task UcitajKurseveZaGrid(System.Data.DataTable tabela)
        {

            var kombinacije = tabela.AsEnumerable()
                            .Select(r => (
                                valuta: r.Field<string>("Valuta"),
                                datum: r.Field<DateTime?>("DatumKreiranjaNaloga")?.Date // Nullable!
                            ))
                            .Where(x => !string.IsNullOrEmpty(x.valuta) && x.datum.HasValue)
                            .Select(x => (x.valuta, x.datum.Value)) // 
                            .Distinct();

            foreach (var (valuta, datum) in kombinacije)
            {
                _ = await KursHelper.VratiSrednjiKurs(valuta, datum);
            }
        }

        public async Task ObracunajVrednostiUGridu(System.Data.DataTable tabela)
        {
            if (!tabela.Columns.Contains("CenaRSD"))
                tabela.Columns.Add("CenaRSD", typeof(decimal));
            if (!tabela.Columns.Contains("TrosakRSD"))
                tabela.Columns.Add("TrosakRSD", typeof(decimal));

            foreach (DataRow row in tabela.Rows)
            {
                var valuta = row["Valuta"].ToString();
                var datum = Convert.ToDateTime(row["DatumUtovara"]);
                var cena = Convert.ToDecimal(row["Cena"]);
                var trosak = Convert.ToDecimal(row["Trosak"]);

                var kurs = await KursHelper.VratiSrednjiKurs(valuta, datum);

                row["CenaRSD"] = Math.Round(cena * kurs, 2);
                row["TrosakRSD"] = Math.Round(trosak * kurs, 2);
            }
        }

        private void toolStripPregledNaloga_Click(object sender, EventArgs e)
        {
            var record = gridGroupingControl1.Table.CurrentRecord;
            if (record == null) return;

            object idObj = record.GetValue("ID");
            if (idObj == null || idObj == DBNull.Value)
            {
                MessageBox.Show("ID je nevažeći.");
                return;
            }

            int id = Convert.ToInt32(idObj); 

            using (var frm = new frmDrumski(id))
                frm.ShowDialog();
        }

        private void toolStripPregledKamiona_Click(object sender, EventArgs e)
        {
            var record = gridGroupingControl1.Table.CurrentRecord;
            if (record == null) return;

            object idObj = record.GetValue("KamionID");
            if (idObj == null || idObj == DBNull.Value)
            {
                MessageBox.Show("ID je nevažeći.");
                return;
            }

            int kamionID = Convert.ToInt32(idObj);

            using (var frm = new frmAutomobiliDrumski(kamionID))
                frm.ShowDialog();
        }

    }
}

public static class KursHelper
{
    private static Dictionary<(string valuta, DateTime datum), decimal> kursCache = new Dictionary<(string valuta, DateTime datum), decimal>();

    public static async Task<decimal> VratiSrednjiKurs(string valuta, DateTime datum)
    {
        if (valuta == "RSD") return 1;

        var key = (valuta, datum.Date);

        if (kursCache.TryGetValue(key, out var kurs))
        {
            return kurs;
        }

        // TODO: ovde ide poziv ka NBS
        kurs = await PreuzmiKursSaNBS(valuta, datum);
        kursCache[key] = kurs;
        return kurs;
    }

    private static async Task<decimal> PreuzmiKursSaNBS(string valuta, DateTime datum)
    {
        string url = $"https://www.nbs.rs/kursnaListaModul/srednjiKurs.do?date={datum:dd.MM.yyyy}";

        using (HttpClient client = new HttpClient())
        {
            string xml = await client.GetStringAsync(url);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList nodes = doc.SelectNodes("//item");

            foreach (XmlNode node in nodes)
            {
                string xmlValuta = node["valuta"]?.InnerText;
                if (xmlValuta == valuta)
                {
                    string srednji = node["sred"]?.InnerText;

                    if (decimal.TryParse(srednji, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out decimal kurs))
                    {
                        return kurs;
                    }
                }
            }

            throw new Exception($"Kurs za valutu {valuta} na datum {datum:dd.MM.yyyy} nije pronađen.");
        }
    }

}
