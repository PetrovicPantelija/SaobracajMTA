using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.XlsIO.Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class PakovanjeKamiona1: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        private int? _rightClickedRowIndexDG2 = null;
        private string izabranaRegistracija = null;
        private string izabraniTipTransporta = "";
        private bool cellClickHandlerAttached = false;
        private Form aktivnaFormaPregleda;
        private DataTable mainTable;

        public PakovanjeKamiona1()
        {
            InitializeComponent();
            ChangeTextBox();
            ChangeTextBoxTable();
            UcitajFiltere();
            chkDatumD.Checked = true;
            chkR.Checked = true;
            RefreshDataGrid1();
            RefreshDataGrid2();
            RefreshDataGrid3();

            this.Text = "Pakovanje kamiona";
            this.dataGridView2.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseDown);

        }
        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
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

                //    if (control is System.Windows.Forms.TextBox textBox)
                //    {

                //        textBox.BackColor = Color.White;// Example: Change background color
                //        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                //        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //        // Example: Change font
                //    }


                //    if (control is System.Windows.Forms.Label label)
                //    {
                //        // Change properties here
                //        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                //        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                //    }

                //    if (control is DateTimePicker dtp)
                //    {
                //        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.CheckBdatagridviewox chk)
                //    {
                //        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ListBox lb)
                //    {
                //        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ComboBox cb)
                //    {
                //        cb.ForeColor = Color.FromArgb(51, 51, 54);
                //        cb.BackColor = Color.White;// Example: Change background color
                //        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.NumericUpDown nu)
                //    {
                //        nu.ForeColor = Color.FromArgb(51, 51, 54);
                //        nu.BackColor = Color.White;// Example: Change background color
                //        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }
                //}
            }
            else
            {

                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }



            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                foreach (System.Windows.Forms.Control control in Controls)
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
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
        }

        private void ChangeTextBoxTable()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
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

                //    if (control is System.Windows.Forms.TextBox textBox)
                //    {

                //        textBox.BackColor = Color.White;// Example: Change background color
                //        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                //        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //        // Example: Change font
                //    }


                //    if (control is System.Windows.Forms.Label label)
                //    {
                //        // Change properties here
                //        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                //        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                //    }

                //    if (control is DateTimePicker dtp)
                //    {
                //        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.CheckBox chk)
                //    {
                //        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ListBox lb)
                //    {
                //        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ComboBox cb)
                //    {
                //        cb.ForeColor = Color.FromArgb(51, 51, 54);
                //        cb.BackColor = Color.White;// Example: Change background color
                //        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.NumericUpDown nu)
                //    {
                //        nu.ForeColor = Color.FromArgb(51, 51, 54);
                //        nu.BackColor = Color.White;// Example: Change background color
                //        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }
                //}
            }
            else
            {

                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }



            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                foreach (System.Windows.Forms.Control control in tableLayoutPanel1.Controls)
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
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
        }

        private void UcitajFiltere()
        {
            var select = " select ID, LTRIM(RTRIM(Naziv)) as Naziv from VrstaVozila";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection5 = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder5 = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);

            DataTable dt = ds.Tables[0];

            // Kreiraj novi red sa praznim tekstom i ID -1
            DataRow prazanRed = dt.NewRow();
            prazanRed["Naziv"] = "All";
            prazanRed["ID"] = -1;

            // Ubaci kao prvi red
            dt.Rows.InsertAt(prazanRed, 0);

            cboTipVozila.DataSource = dt;
            cboTipVozila.DisplayMember = "Naziv";
            cboTipVozila.ValueMember = "ID";

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

                if (cboTipVozila.SelectedValue != null && int.TryParse(cboTipVozila.SelectedValue.ToString(), out int parsedTipNaloga) && parsedTipNaloga > -1)
                    condition = condition + " AND  a.VlasnistvoLegeta = " + parsedTipNaloga;
                if (cboPrevoznik.SelectedValue != null && int.TryParse(cboPrevoznik.SelectedValue.ToString(), out int parsedPrevoznik) && parsedPrevoznik > -1)
                    condition = condition + " AND  a.PartnerID = " + parsedPrevoznik;
                if (cboRegistracija.SelectedValue != null && int.TryParse(cboRegistracija.SelectedValue.ToString(), out int parsedRegistracija) && parsedRegistracija > -1)
                {
                    string regBr = cboRegistracija.Text.Trim();
                    condition += " AND a.RegBr LIKE '" + regBr + "'";
                }

                //  LOGIKA ZA R/N
                // "raspoloživ" = kamion nije vezan za nalog sa DatumIstovara = sutra
                // "neraspoloživ" = kamion jeste vezan za nalog sa DatumIstovara = sutra
                bool prikaziRaspolozive = chkR.Checked;
                bool prikaziNeraspolozive = chkN.Checked; 
                bool prikaziZaDanas = chkDatumD.Checked; 
                bool prikaziZaSutra = chkDatumS.Checked; 

                // 1. Određivanje datuma za proveru (DatumIstovara)
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
                if ((prikaziRaspolozive || prikaziNeraspolozive) && !string.IsNullOrEmpty(datumZaProveru))
                {
                    // Baza upita za provjeru raspoloživosti/neraspoloživosti
                    string subQuery = $@"
                        SELECT DISTINCT ISNULL(KamionID,0) AS KamionID
                        FROM RadniNalogDrumski
                        WHERE CONVERT(date, DatumIstovara) = CONVERT(date, {datumZaProveru})";

                    if (prikaziRaspolozive && !prikaziNeraspolozive)
                    {
                        // Samo raspoloživi – koji NISU vezani za nalog na odabrani datum
                        joinLogika = $@" AND a.ID NOT IN ({subQuery})";
                    }
                    else if (!prikaziRaspolozive && prikaziNeraspolozive)
                    {
                        // Samo neraspoloživi – koji JESU vezani za nalog na odabrani datum
                        joinLogika = $@" AND a.ID IN ({subQuery})";
                    }

                    // 3. Konačan SELECT
                }
                var select = $@" SELECT a.ID,  LTRIM(RTRIM(vv.Naziv)) AS TipVozila, a.VlasnistvoLegeta as TipTransporta, LTRIM(RTRIM(p.PaNaziv)) AS Prevoznik, LTRIM(RTRIM(a.Vozac)) AS Vozac, LTRIM(RTRIM(a.RegBr)) AS RegBr   , LTRIM(RTRIM(BrojTelefona)) as TelefonVozaca
                            FROM Automobili a 
                            LEFT JOIN VrstaVozila vv on a.VlasnistvoLegeta = vv.ID 
                            LEFT JOIN Partnerji p on a.PartnerID = p.PaSifra  
                            WHERE VoziloDrumskog = 1  {condition}  {joinLogika}" ;


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

        private void RefreshDataGrid2()
        {
            bool prikaziZaDanas = chkDatumD.Checked;
            bool prikaziZaSutra = chkDatumS.Checked;

            // 1. Određivanje datuma za proveru (DatumIstovara)
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

            // 2. Uslov za TipTransporta
            string dodatniUslovTipTransporta = "";
            bool koristiFilterTipa = false;
            int selektovaniTip = 0;

            if (cboTipVozila.SelectedValue != null && int.TryParse(cboTipVozila.SelectedValue.ToString(), out selektovaniTip))
            {
                if (selektovaniTip > 0)
                {
                    koristiFilterTipa = true;
                    dodatniUslovTipTransporta = " AND rn.TipTransporta = @TipTransporta ";
                }
            }
            var select = $@"
                        SELECT
                            -- STRING_AGG funkcija za spajanje svih ID-jeva u jedan string, odvojen zarezom
                           x.ID,
                           LTRIM(RTRIM(x.Nalogodavac)) AS Nalogodavac,
                           x.NalogodavacID,
	                       x.BrojKontejnera,
                           x.NalogID,
                           x.Kamion,
	                       CONVERT(VARCHAR,x.DatumUtovara,104) AS DatumUtovara,
                           CONVERT(VARCHAR,x.DatumIstovara,104) AS DatumIstovara,
                           x.Relacija,
                           x.TipTransporta
                           
                        FROM
                        (
                             SELECT  pa.PaNaziv AS Nalogodavac,
                                     i.Klijent3 AS NalogodavacID,
			                         i.BrojKontejnera,
                                     rn.NalogID,
                                     '' AS Kamion,
			                         rn.DatumUtovara,
                                     rn.DatumIstovara,
                                     rn.ID,
                                     LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                    rn.TipTransporta
                            FROM RadniNalogDrumski rn
                            INNER JOIN Izvoz i ON i.ID = rn.KontejnerID
                            LEFT JOIN MestaUtovara mu ON mu.id = i.MesoUtovara
                            LEFT JOIN MestaUtovara mi ON mi.id = rn.MestoIstovara
                            LEFT JOIN Partnerji pa ON pa.PaSifra = i.Klijent3 AND ISNULL(rn.KamionID,0) = 0
                            WHERE rn.Uvoz = 0 {dodatniUslovTipTransporta}

                            UNION ALL
                            SELECT  pa.PaNaziv AS Nalogodavac,
                                     ik.Klijent3 AS NalogodavacID,
			                         ik.BrojKontejnera,
                                     rn.NalogID,
                                     '' AS Kamion,
			                         rn.DatumUtovara,
                                     rn.DatumIstovara,
                                     rn.ID ,
                                     LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                    rn.TipTransporta
                            FROM RadniNalogDrumski rn
                            INNER JOIN VrstaManipulacije vm ON vm.ID = rn.IDVrstaManipulacije
                            INNER JOIN IzvozKonacna ik ON ik.ID = rn.KontejnerID
                            LEFT JOIN MestaUtovara mu ON mu.id = ik.MesoUtovara
                            LEFT JOIN MestaUtovara mi ON mi.id = rn.MestoIstovara
                            LEFT JOIN Partnerji pa ON pa.PaSifra = ik.Klijent3
                            WHERE rn.Uvoz = 0 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 AND ISNULL(rn.KamionID,0) = 0 {dodatniUslovTipTransporta}

                            UNION ALL
                            SELECT   pa.PaNaziv AS Nalogodavac,
                                     uk.Nalogodavac3 AS NalogodavacID,
			                         uk.BrojKontejnera,
                                     rn.NalogID,
                                     '' AS Kamion,
			                         rn.DatumUtovara,
                                     rn.DatumIstovara,
                                     rn.ID,
                                     LTRIM(RTRIM(mi.Naziv)) + ' - ' +  LTRIM(RTRIM(mu.Naziv)) AS Relacija ,
                                    rn.TipTransporta
                            FROM RadniNalogDrumski rn
                            INNER JOIN VrstaManipulacije vm ON vm.ID = rn.IDVrstaManipulacije
                            INNER JOIN UvozKonacna uk ON uk.ID = rn.KontejnerID
                            LEFT JOIN MestaUtovara mi ON mi.id = uk.MestoIstovara 
                            LEFT JOIN MestaUtovara mu ON mu.id = rn.MestoUtovara
                            LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3
                            WHERE rn.Uvoz = 1 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 AND ISNULL(rn.KamionID,0) = 0 {dodatniUslovTipTransporta}

                            UNION ALL
                            SELECT  pa.PaNaziv AS Nalogodavac,
                                     u.Nalogodavac3 AS NalogodavacID,
			                         u.BrojKontejnera,
                                     rn.NalogID,
                                     '' AS Kamion,
			                         rn.DatumUtovara,
                                     rn.DatumIstovara,
                                     rn.ID ,
                                     LTRIM(RTRIM(mi.Naziv)) + ' - ' +  LTRIM(RTRIM(mu.Naziv)) AS Relacija ,
                                    rn.TipTransporta
                            FROM RadniNalogDrumski rn
                            INNER JOIN VrstaManipulacije vm ON vm.ID = rn.IDVrstaManipulacije
                            INNER JOIN Uvoz u ON u.ID = rn.KontejnerID
                            LEFT JOIN MestaUtovara mi ON mi.id = u.MestoIstovara 
                            LEFT JOIN MestaUtovara mu ON mu.id = rn.MestoUtovara
                            LEFT JOIN Partnerji pa ON pa.PaSifra = u.Nalogodavac3
                            WHERE rn.Uvoz = 1 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 AND ISNULL(rn.KamionID,0) = 0 {dodatniUslovTipTransporta}
 
                            UNION ALL
                            SELECT   pa.PaNaziv AS Nalogodavac,
                                     rn.Klijent AS NalogodavacID,
			                         rn.BrojKontejnera,
                                     rn.NalogID,
                                     '' AS Kamion,
			                         rn.DatumUtovara,
                                     rn.DatumIstovara,
                                     rn.ID ,
                                     CASE 
                                            WHEN rn.Uvoz IN (1, 2, 4) 
                                                THEN LTRIM(RTRIM(mi.Naziv)) +' - ' + LTRIM(RTRIM(mu.Naziv))
                                            WHEN rn.Uvoz IN (0, 3, 5) 
                                                THEN LTRIM(RTRIM(mu.Naziv)) + ' - ' + LTRIM(RTRIM(mi.Naziv))
                                            ELSE ''
                                        END AS Relacija,
                                    rn.TipTransporta
                            FROM RadniNalogDrumski rn
                            LEFT JOIN Partnerji pa ON pa.PaSifra = rn.Klijent
                            LEFT JOIN MestaUtovara mi ON mi.id = rn.MestoIstovara 
                            LEFT JOIN MestaUtovara mu ON mu.id = rn.MestoUtovara
                            WHERE rn.Uvoz IN (2, 3, 4, 5 ) AND rn.NalogID > 0 AND ISNULL(rn.KamionID,0) = 0 {dodatniUslovTipTransporta}
                        ) AS x
                        WHERE CONVERT(date, DatumIstovara) = CONVERT(date, {datumZaProveru})
                     
                        ORDER BY NalogID DESC
                        ";
            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(select, conn))
            {
                if (koristiFilterTipa)
                    cmd.Parameters.AddWithValue("@TipTransporta", selektovaniTip);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = ds.Tables[0];
            }


            PodesiDatagridView(dataGridView2);
            dataGridView2.RowHeadersWidth = 30;

            if (dataGridView2.Columns.Contains("ID"))
            {
                dataGridView2.Columns["ID"].Visible = false;
            }
            if (dataGridView2.Columns.Contains("KamionID"))
            {
                dataGridView2.Columns["KamionID"].Visible = false;
            }
            if (dataGridView2.Columns.Contains("RadniNalogOtkazan"))
            {
                dataGridView2.Columns["RadniNalogOtkazan"].Visible = false;
            }
     
            if (dataGridView2.Columns.Contains("NalogodavacID"))
            {
                dataGridView2.Columns["NalogodavacID"].Visible = false;
            }

            if (dataGridView2.Columns.Contains("TipTransporta"))
            {
                dataGridView2.Columns["TipTransporta"].Visible = false;
            }
            // Postavi proporcije samo za vidljive kolone
            float totalWeight = 100f;
            float relacijaWeight = 20f;
            float nalogodavacWeight = 20f;

            // Postavi dve glavne
            if (dataGridView2.Columns.Contains("Relacija"))
                dataGridView2.Columns["Relacija"].FillWeight = relacijaWeight;
            if (dataGridView2.Columns.Contains("Nalogodavac"))
                dataGridView2.Columns["Nalogodavac"].FillWeight = nalogodavacWeight;

            // Izračunaj preostalo za ostale kolone
            var visibleCols = dataGridView2.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible && c.Name != "Relacija" && c.Name != "Nalogodavac")
                .ToList();

            float preostalo = totalWeight - relacijaWeight - nalogodavacWeight;
            float weightPoKoloni = visibleCols.Count > 0 ? preostalo / visibleCols.Count : 0;

            foreach (var col in visibleCols)
                col.FillWeight = weightPoKoloni;
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

                //3.  Određivanje datuma za proveru (DatumIstovara)
                bool prikaziZaDanas = chkDatumD.Checked;
                bool prikaziZaSutra = chkDatumS.Checked;

                
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

                var select = $@"
                            SELECT 
                                
                                -- Agregirana kolona (Spojeni ID-jevi)
                                STRING_AGG(CONVERT(VARCHAR(MAX), x.ID), ',') AS IdsRadniNalogDrumski,
                                x.ID,
                                LTRIM(RTRIM( x.Nalogodavac)) AS Nalogodavac, 
                                x.Relacija,
                                x.DatumIstovara, 
                                LTRIM(RTRIM(x.Prevoznik)) AS Prevoznik, 
                                LTRIM(RTRIM(x.Vozac)) AS Vozac,
                                LTRIM(RTRIM(x.Kamion)) AS Kamion, 
                                x.NalogID, 
                                x.PoslataNajava,
                                x.NajavuPoslao, 
                                x.SlanjeNajave, 
                                x.Status AS Status,
                                x.StatusID

                            FROM 
                            (
                                -- Deo 1 (Izvoz)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac, 
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID 
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                INNER JOIN Izvoz i ON i.ID = rn.KontejnerID 
                                LEFT JOIN MestaUtovara mu on i.MesoUtovara = mu.ID 
                                LEFT JOIN MestaUtovara mi on rn.MestoIstovara = mi.ID
                                LEFT JOIN Partnerji pa ON pa.PaSifra = i.Klijent3 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                WHERE rn.Uvoz = 0 AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID IS NOT NULL AND rn.KamionID != 0 
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DatumIstovara) = CONVERT(date, {datumZaProveru} )
    
                                UNION ALL 
                                -- Deo 2 (IzvozKonacna)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID 
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                INNER JOIN IzvozKonacna ik ON ik.ID = rn.KontejnerID 
                                LEFT join MestaUtovara mu on ik.MesoUtovara = mu.ID
                                LEFT join MestaUtovara mi on rn.MestoIstovara = mi.ID
                                LEFT JOIN Partnerji pa ON pa.PaSifra = ik.Klijent3 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                WHERE rn.Uvoz = 0 AND rn.KamionID IS NOT NULL AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID != 0 
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DatumIstovara) = CONVERT(date, {datumZaProveru} )
    
                                UNION ALL 
                                -- Deo 3 (UvozKonacna)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID 
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                INNER JOIN UvozKonacna uk ON uk.ID = rn.KontejnerID 
                                LEFT JOIN MestaUtovara mu on  rn.MestoUtovara = mu.ID
                                LEFT JOIN MestaUtovara mi on  uk.MestoIstovara = mi.ID 
                                LEFT JOIN Partnerji pa ON pa.PaSifra = uk.Nalogodavac3 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                WHERE rn.Uvoz = 1 AND rn.KamionID IS NOT NULL AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID != 0 
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DatumIstovara) = CONVERT(date, {datumZaProveru} )
    
                                UNION ALL 
                                -- Deo 4 (Uvoz)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac, 
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija, 
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID 
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                INNER JOIN Uvoz u ON u.ID = rn.KontejnerID 
                                LEFT JOIN Partnerji pa ON pa.PaSifra = u.Nalogodavac3 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                LEFT JOIN MestaUtovara mu on  rn.MestoUtovara = mu.ID
                                LEFT JOIN MestaUtovara mi on  u.MestoIstovara = mi.ID
                                WHERE rn.Uvoz = 1 AND rn.KamionID IS NOT NULL AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID != 0 
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DatumIstovara) = CONVERT(date, {datumZaProveru} )
    
                                UNION ALL 
                                -- Deo 5 (Ostali drumski)
                                SELECT rn.ID, 
                                       LTRIM(RTRIM(pa.PaNaziv)) AS Nalogodavac,  
                                       LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija,
                                       au.Vozac,
                                       au.RegBr AS Kamion, 
                                       CONVERT(VARCHAR,rn.DatumIstovara,104) AS DatumIstovara, 
                                       rn.NalogID, p.PaNaziv AS Prevoznik, 
                                       rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' + Rtrim(dk.DePriimek) AS NajavuPoslao, 
                                       CONVERT(VARCHAR,rn.NajavaPoslataDatum,104) AS SlanjeNajave,
                                       rn.Status, rn.Status AS StatusID 
                                FROM RadniNalogDrumski rn 
                                LEFT JOIN Delavci dk ON dk.DeSifra = rn.NajavuPoslaoKorisnik 
                                INNER JOIN Automobili au ON au.ID = rn.KamionID 
                                LEFT JOIN Partnerji pa ON pa.PaSifra = rn.Klijent 
                                LEFT JOIN Partnerji p ON au.PartnerID = p.PaSifra 
                                LEFT JOIN MestaUtovara mu on  rn.MestoUtovara = mu.ID
                                LEFT JOIN MestaUtovara mi on  rn.MestoIstovara = mi.ID
                                LEFT JOIN StatusVozila sv ON sv.ID = rn.Status 
                                WHERE rn.Uvoz IN (2, 3, 4, 5) AND rn.NalogID > 0 AND ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID IS NOT NULL AND rn.KamionID != 0
                                      AND ISNULL(rn.Arhiviran, 0) <> 1 AND (rn.Status IS NULL OR rn.Status NOT IN ( {statusiZaUpit} )) 
                                      AND CONVERT(date, rn.DatumIstovara) = CONVERT(date, {datumZaProveru} )

                            ) AS x
                            GROUP BY 
                                x.ID,
                                x.Nalogodavac, 
                                x.Relacija,
                                x.Vozac,
                                x.Kamion, 
                                x.DatumIstovara, 
                                x.NalogID, 
                                x.Prevoznik, 
                                x.PoslataNajava,
                                x.NajavuPoslao, 
                                x.SlanjeNajave, 
                                x.Status,
                                x.StatusID

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

            

            PodesiDatagridView(dataGridView3);

            dataGridView3.RowHeadersWidth = 30; // ili bilo koja vrednost u pikselima

            string[] koloneZaSakrivanje = new string[] {
                    "ID", "KamionID", "Uvoz","StatusID", "IdsRadniNalogDrumski"
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

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                int poslateInstrukcije = 0;
                var grid = dataGridView3;
                var kolona = grid.Columns[e.ColumnIndex].Name;

                //if (kolona == "Instrukcije")
                //{

                //    if (dataGridView3.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                //    {
                //        var row = dataGridView3.Rows[e.RowIndex];
                //        int? radniNalogDrumskiID = 0;
                //        if (row.Cells["ID"].Value != DBNull.Value && int.TryParse(row.Cells["ID"].Value.ToString(), out int parsedRadniNalogDrumskiID))
                //            radniNalogDrumskiID = parsedRadniNalogDrumskiID;
                //        string kontejnerString = row.Cells["BrojKontejnera"].Value?.ToString() ?? "";

                //        poslateInstrukcije = ProveriDaLiJePorukaPoslata(radniNalogDrumskiID);
                //        if (poslateInstrukcije > 0)
                //        {
                //            DialogResult result = MessageBox.Show(
                //                   "Za ovaj nalog instrukcije su već poslate.\nDa li želite da je ponovo pošaljete?",
                //                   "Upozorenje",
                //                   MessageBoxButtons.YesNo,
                //                   MessageBoxIcon.Question);

                //            if (result == DialogResult.No)
                //            {
                //                return; // prekida dalje izvršavanje metode
                //            }
                //        }

                //        // Čitanje vrednosti iz reda

                //        if (!string.IsNullOrEmpty(row.Cells["BrojKontejnera2"].Value?.ToString()))
                //            kontejnerString += ", " + row.Cells["BrojKontejnera2"].Value?.ToString();
                //        string datumUtovara = row.Cells["DatumUtovara"].Value?.ToString();
                //        string datumIstovara = row.Cells["DatumIstovara"].Value?.ToString();
                //        string propratnicuRadi = row.Cells["Nalogodavac"].Value?.ToString();
                //        string mestoUtovara = row.Cells["MestoUtovara"].Value?.ToString();
                //        string adresaUtovara = row.Cells["AdresaUtovara"].Value?.ToString();
                //        string mestoIstovara = row.Cells["MestoIstovara"].Value?.ToString();
                //        string adresaIstovara = row.Cells["AdresaIstovara"].Value?.ToString();
                //        string brojNaloga = row.Cells["NalogID"].Value?.ToString();
                //        string kontaktUtovaraIstovara = row.Cells["KontaktOsobaUtovarIstovar"].Value?.ToString();
                //        string napomenaZaPozicioniranje = row.Cells["NapomenaZaPozicioniranje"].Value?.ToString();
                //        string odredisnaCarinarnica = row.Cells["OdredisnaCarina"].Value?.ToString();
                //        string polaznaCarinarnica = row.Cells["PolaznaCarinarnica"].Value?.ToString();
                //        string polaznaSpedicija = row.Cells["PolaznaSpedicija"].Value?.ToString();
                //        string odredisnaSpedicija = row.Cells["OdredisnaSpedicija"].Value?.ToString();


                //        // Formiranje poruke
                //        string poruka = $"Kontejner {kontejnerString} preuzimate {datumUtovara} na {mestoUtovara}\n" +
                //                        $"Propratnicu Vam radi {polaznaCarinarnica} {polaznaSpedicija} \n" +
                //                        $"Javljate se {datumIstovara} na {odredisnaCarinarnica}: {odredisnaSpedicija}\n" +
                //                        $"Kontejner istovarate {mestoIstovara} {adresaIstovara}\n" +
                //                        $",{kontaktUtovaraIstovara}\n" +
                //                        $"Posebni uslovi transporta: {napomenaZaPozicioniranje}\n" +
                //                        $"Broj naloga: {brojNaloga}";

                //        InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                //        ins.SnimiToken(radniNalogDrumskiID);
                //        int temp = PostaviVrednostZaposleni();
                //        int? NajavuPoslaoKorisnik = temp == 0 ? (int?)null : temp;
                //        ins.PoslateInstrukcije(radniNalogDrumskiID, NajavuPoslaoKorisnik);
                //        // Prikaz
                //        MessageBox.Show(poruka, "Tekst za Viber", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //        // Alternativa ako koristiš npr. RichTextBox umesto MessageBox-a
                //        // richTextBox1.Text = poruka;
                //    }
                //}
                //else
                if (kolona == "Upload")
                {
                    int radniNalogDrumskiID = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value);
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
                        string targetPath = $@"\\192.168.99.10\Leget\Drumski\Dokumenta\ID_{radniNalogDrumskiID}";
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
                else if (kolona == "Otvori")
                {
                    if (aktivnaFormaPregleda == null || aktivnaFormaPregleda.IsDisposed)
                    {
                        var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                        int radniNalogID = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["ID"].Value);

                        frmPregledFajlova pregled = new frmPregledFajlova(radniNalogID);
                        pregled.ShowDialog();
                    }
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

            // Kolona za upload
            DataGridViewButtonColumn uploadBtn = new DataGridViewButtonColumn();
            uploadBtn.Name = "Upload";
            uploadBtn.HeaderText = "";
            uploadBtn.Text = "Dodaj";
            uploadBtn.UseColumnTextForButtonValue = true;
            uploadBtn.Width = 100;

            // Kolona za otvori 
            DataGridViewButtonColumn openUploadedBtn = new DataGridViewButtonColumn();
            openUploadedBtn.Name = "Otvori";
            openUploadedBtn.HeaderText = "Otvori";
            openUploadedBtn.Text = "Otvori";
            openUploadedBtn.UseColumnTextForButtonValue = true;
            openUploadedBtn.Width = 100;

            // Dodaj ako već ne postoje
            if (!dataGridView3.Columns.Contains("Instrukcije"))
                dataGridView3.Columns.Add(instrukcijeBtn);

            if (!dataGridView3.Columns.Contains("Upload"))
                dataGridView3.Columns.Add(uploadBtn);

            if (!dataGridView3.Columns.Contains("Otvori"))
                dataGridView3.Columns.Add(openUploadedBtn);
        }
        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Helvetica", 12F, GraphicsUnit.Pixel);
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

        private void toolStripItemPregledNaloga_Click(object sender, EventArgs e)
        {
            if (_rightClickedRowIndexDG2 == null || _rightClickedRowIndexDG2 < 0)
            {
                MessageBox.Show("Molimo kliknite desnim tasterom miša na red koji želite da otvorite.");
                return;
            }

            DataGridViewRow selectedRow = dataGridView2.Rows[_rightClickedRowIndexDG2.Value];

            if (selectedRow.Cells["NalogID"].Value == null || selectedRow.Cells["NalogID"].Value == DBNull.Value)
            {
                MessageBox.Show("NalogID nije pronađen ili je nevažeći za izabrani red.");
                return;
            }
            string dan = "";
            int nalogID = Convert.ToInt32(selectedRow.Cells["NalogID"].Value);
            int? NalogodavacID = null;
            if (selectedRow.Cells["NalogodavacID"]!=null)
                if (selectedRow.Cells["NalogodavacID"]?.Value != null && int.TryParse(selectedRow.Cells["NalogodavacID"].Value.ToString(), out int temp))
                {
                    NalogodavacID = temp;
                }
            if (chkDatumD.Checked == true)
                 dan = "D";
            else
                 dan = "S";


            frmPregledNalogaDrumski dr = new frmPregledNalogaDrumski(nalogID, NalogodavacID, dan);
            dr.FormClosed += (s, args) =>
            {
                RefreshDataGrid2();
            };

            dr.Show();
        }
        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView2.ClearSelection();
                dataGridView2.Rows[e.RowIndex].Selected = true;
                _rightClickedRowIndexDG2 = e.RowIndex;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var colName = "RegBr";
            var colName1 = "TipTransporta";

            if (dataGridView1.Columns.Contains(colName))
            {
                var cellValue = dataGridView1.Rows[e.RowIndex].Cells[colName].Value;
                if (cellValue != null)
                {
                    izabranaRegistracija = cellValue.ToString();
                }
            }
            if (dataGridView1.Columns.Contains(colName1))
            {
                var tipValue = dataGridView1.Rows[e.RowIndex].Cells[colName1].Value;
                izabraniTipTransporta = tipValue?.ToString() ?? "";
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (string.IsNullOrWhiteSpace(izabranaRegistracija))
            {
                MessageBox.Show("Niste izabrali registraciju iz liste vozila.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //  Uzimanje tipa transporta kontejnera iz grida 2
            string tipKontejnera = dataGridView2.Rows[e.RowIndex].Cells["TipTransporta"].Value?.ToString() ?? "";

            //  Provera da li se tipovi poklapaju
            if (!string.Equals(izabraniTipTransporta, tipKontejnera, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    $"Tip prevoza za selektovani kamion " +
                    $"nije odgovarajući tipu prevoza kontejnera.",
                    "Nepodudaranje tipa prevoza",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return; // prekini — ne upisuj kamion
            }

            string dan = "";
            if ( chkDatumD.Checked)
            {
                dan = " za danas ";
            }
            else if ( chkDatumS.Checked)
            {
                dan = " za sutra ";
            }
            // Upisuje registraciju u kolonu "Kamion" bez obzira gde se klikne
            if (chkN.Checked == true)
            {
                var result = MessageBox.Show(
                  $"Kamion sa registarskim tablicama {izabranaRegistracija} je {dan} već dodeljen, da li ste sigurni da želite da ga dodelite još jednom?",
                  "Potvrda dodavanja kamiona",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                    dataGridView2.Rows[e.RowIndex].Cells["Kamion"].Value = izabranaRegistracija;
            }
            else
                dataGridView2.Rows[e.RowIndex].Cells["Kamion"].Value = izabranaRegistracija;
        }

        private void btnFiltriraj_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
            RefreshDataGrid2();
        }

        private void btnKreiraj_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmDrumski")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Drumski.frmDrumski part = new Drumski.frmDrumski("NOVINALOG", null);
                part.FormClosed += (s, args) =>
                {
                    RefreshDataGrid2();
                };

                part.Show();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkR_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox s = (CheckBox)sender;

            // A. Logika za automatsko odčekiravanje drugog boksa 
            if (s == chkR && chkR.Checked)
            {
                chkN.Checked = false;
            }
            else if (s == chkN && chkN.Checked)
            {
                chkR.Checked = false;
            }

            // Ako pokušamo odčekirati boks, a drugi boks nije čekiran:
            if (s.Checked == false)
            {
                if (s == chkR && chkN.Checked == false)
                {
                    // Ponovo čekiraj Raspolozivi jer je Neraspolozivi odčekiran
                    chkR.Checked = true;
                    return; // Izlazimo da sprečimo redundantno osvežavanje
                }
                else if (s == chkN && chkR.Checked == false)
                {
                    // Ponovo čekiraj Neraspolozivi jer je Raspolozivi odčekiran
                    chkN.Checked = true;
                    return; // Izlazimo da sprečimo redundantno osvežavanje
                }
            }

            // Osveži DataGrid samo ako je došlo do STVARNE promene
            RefreshDataGrid1();
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
            RefreshDataGrid2();
            RefreshDataGrid3();
        }

        private void dataGridView3_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView3.IsCurrentCellDirty)
            {
                dataGridView3.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Provera da li je promenjena ćelija "Status" u ispravnom redu
            if (e.RowIndex >= 0 && dataGridView3.Columns[e.ColumnIndex].Name == "Status")
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                // Provera da li su potrebne kolone dostupne
                if (row.Cells["Status"].Value == DBNull.Value || row.Cells["IdsRadniNalogDrumski"].Value == null)
                {
                    return;
                }

                // 2. Dohvatanje nove vrednosti Status ID
                int? noviStatusID = Convert.ToInt32(row.Cells["Status"].Value);

                // 3. Dohvatanje spojenog stringa ID-jeva
                string idsString = row.Cells["IdsRadniNalogDrumski"].Value.ToString();

                // 4. Razdvajanje stringa u listu celih brojeva
                List<int> idjeviZaUpdate = idsString
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.Parse(s.Trim()))
                    .ToList();

                // 5. Poziv metode za grupno ažuriranje
                try
                {
                    InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                    ins.AzurirajStatusViseKontejera(idjeviZaUpdate, noviStatusID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška pri snimanju statusa: " + ex.Message);
                }
            }
            RefreshDataGrid3();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string IdsString = ""; 
            string BrojKamiona = "";

            // Lista ID-eva redova iz grida 2 koji su preneti
            List<int> prenetiIdjevi = new List<int>();
            List<string> zauzetiKontejneri = new List<string>();

            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();

            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    // PROVERA DA LI JE KAMION DODELJEN
                    if (row.Cells["Kamion"].Value != null && !string.IsNullOrEmpty(row.Cells["Kamion"].Value.ToString()))
                    {

                        BrojKamiona = row.Cells["Kamion"].Value.ToString();

                        if (row.Cells["ID"] == null || !int.TryParse(row.Cells["ID"].Value.ToString(), out int id))
                        {
                            MessageBox.Show("ID ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (ProveriDaLiJeRadniNalogVecDodeljen(id) > 0)
                        {
                            // Uzmi podatke o kontejneru (ili broju naloga)
                            string brojKontejnera = row.Cells["BrojKontejnera"].Value?.ToString() ?? "(nepoznat)";
                            zauzetiKontejneri.Add($"Kontejneru {brojKontejnera} je već dodeljen kamion registracionog broja {BrojKamiona}.");
                            continue; // preskoči dodelu
                        }
                      
                            // 2. POZIV za update
                            ins.DodeliKamionP(id, BrojKamiona);

                            // 3. PAMĆENJE PRENETIH ID-JEVA ZA REFRESHE/SELEKCIJU
                            prenetiIdjevi.Add(id);

                    }
                }

                if (zauzetiKontejneri.Any())
                {
                    string poruka = string.Join(Environment.NewLine, zauzetiKontejneri);
                    MessageBox.Show("Neki kamioni nisu dodeljeni jer su već zauzeti:\n\n" + poruka,
                                    "Upozorenje",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }

                RefreshDataGrid3();
                RefreshDataGrid2();
                RefreshDataGrid1();

                dataGridView3.ClearSelection();

                //foreach (DataGridViewRow row in dataGridView3.Rows)
                //{
                //    if (!row.IsNewRow && row.Cells["ID"].Value != null && prenetiIdjevi.Contains(Convert.ToInt32(row.Cells["ID"].Value)))
                //    {
                //        row.Selected = true;
                //    }
                //}

                //if (dataGridView3.SelectedRows.Count > 0)
                //{
                //    dataGridView3.FirstDisplayedScrollingRowIndex = dataGridView3.SelectedRows[0].Index;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom snimanja dodele kamiona: " + ex.Message);
            }
        }

        public int ProveriDaLiJeRadniNalogVecDodeljen(int nalog)
        {
            List<string> statusi = new List<string>();
            SqlConnection conn = new SqlConnection(connection);
            using (conn)
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand($@"
                            SELECT COUNT(*) 
                            FROM RadniNalogDrumski rn
                            WHERE id = @Nalog AND IsNull(KamionID, 0) > 0
                        ", conn);

                cmd.Parameters.AddWithValue("@Nalog", nalog);

                int count = (int)cmd.ExecuteScalar();
                return count;
            }
       
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();

            // Lista ID-jeva prenetih za kasnije obeležavanje
            List<int> prenetiIdjevi = new List<int>();

            try
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    if (row.IsNewRow) continue;

                    // Dohvati spojeni string ID-jeva
                    string idsString = row.Cells["IdsRadniNalogDrumski"].Value.ToString();


                    // 1. RAZDVAJANJE STRINGA NA LISTU ID-JEVA
                    List<int> idjeviZaUpdate = idsString
                        .Split(',')
                        .Select(s => int.Parse(s.Trim()))
                        .ToList();

                    // 2. POZIV NOVE METODE ZA UPDATE
                    // Prosleđujemo celu listu ID-jeva metodi
                    ins.VratiKamionUNerasporedjeneLista(idjeviZaUpdate);

                    // 3. PAMĆENJE PRENETIH ID-JEVA 
                    // Dodajemo SVE pojedinačne ID-jeve za kasniji refresh
                    prenetiIdjevi.AddRange(idjeviZaUpdate);
                }

                // --- Refreshi gridova ---
                RefreshDataGrid3();
                RefreshDataGrid2();
                RefreshDataGrid1();

                // --- LOGIKA ZA SELEKCIJU U DRUGOM GRIDU (dataGridView2) ---
                // Budući da se vraća u neraspoređene (grid 2), nalozi moraju biti selektovani u gridu 2
                dataGridView2.ClearSelection();

                //foreach (DataGridViewRow row in dataGridView2.Rows) 
                //{
                //    // Proveravamo da li je ID u listi prenetih ID-jeva 
                //    if (!row.IsNewRow && row.Cells["ID"].Value != null && prenetiIdjevi.Contains(Convert.ToInt32(row.Cells["ID"].Value)))
                //    {
                //        row.Selected = true;
                //    }
                //}

                //// Opcionalno: skrol do prvog selektovanog
                //if (dataGridView2.SelectedRows.Count > 0)
                //{
                //    dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.SelectedRows[0].Index;
                //}
                //foreach (DataGridViewRow row in dataGridView2.Rows) // ITERIRAMO KROZ SVE POJEDINAČNE REDOVE u GRIDU 2
                //{
                //    // 1. Provera da li je red validan i ima vrednost za ID
                //    if (!row.IsNewRow && row.Cells["ID"].Value != null)
                //    {
                //        int currentRowID = Convert.ToInt32(row.Cells["ID"].Value);
        
                //        // 2. Provera da li se pojedinačni ID iz reda nalazi u listi prenetih ID-jeva
                //        if (prenetiIdjevi.Contains(currentRowID))
                //        {
                //            row.Selected = true;
                //        }
                //    }
                //}

            // Opcionalno: skrol do prvog selektovanog
            if (dataGridView2.SelectedRows.Count > 0)
            {
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.SelectedRows[0].Index;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška prilikom vraćanja kamiona: " + ex.Message);
            }
        }

        private void btnInstrukcije_Click(object sender, EventArgs e)
        {

        }
        private void btnNajava_Click(object sender, EventArgs e)
        {

        }

        private int GetInt(DataRow row, string colName)
        {
            // 1. Proveravamo da li kolona postoji 
            if (!row.Table.Columns.Contains(colName))
            {
                return 0; // Vraća 0 ako kolona ne postoji
            }

            // 2. Proveravamo DBNull.Value (ako je NULL u bazi)
            if (row[colName] == DBNull.Value || row[colName] == null)
            {
                return 0; 
            }

            // 3. Sigurna konverzija
            if (int.TryParse(row[colName].ToString(), out int result))
            {
                return result;
            }

            // 4. Ako konverzija ne uspe (npr. unet tekst), vraća 0
            return 0;
        }

        private int ProveriDaLiJePorukaPoslata(int? radniNalogDrumskiID)
        {
            SqlConnection conn1 = new SqlConnection(connection);
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM RadniNalogDrumski WHERE ID = @ID and InstrukcijePoslate = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", radniNalogDrumskiID);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0 ? 1 : 0;
                }
            }
        }

        private DataTable DobaviDetaljeZaNajavu(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return new DataTable();
            }

            // Spajanje ID-jeva u string format za SQL IN klauzulu
            string idsInClause = string.Join(",", ids);

            // 2. upit

            var unionQueryBody = @"select  rn.ID, " +
                                 "LTRIM(RTRIM(pa.PaNaziv)) as Nalogodavac, " +
                                 "i.BrojKontejnera," +
                                 "'' as BrojKontejnera2,  i.BookingBrodara," +
                                 "au.RegBr AS Kamion, " +
                                 "vv.Naziv as TipVozila, " +
                                 "au.ID as KamionID, " +
                                 "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, mu.Naziv  AS MestoUtovara, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,  (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, " +
                                 "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara,  mi.Naziv AS MestoIstovara , rn.AdresaIstovara,  rn.NalogID,  p.PaNaziv AS Prevoznik, " +
                                 "rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave," +
                                 " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena, CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera, rn.MestoPreuzimanjaKontejnera," +
                                 "i.NapomenaZaRobu AS NapomenaZaPozicioniranje ,  '' AS OdredisnaCarina," +
                                 "'' as polaznaCarinarnica, '' as polaznaSpedicija, '' as OdredisnaSpedicija,'' AS PolaznaSpedicijaKontakt,  '' AS OdredisnaSpedicijaKontakt, " +
                                 "ISNULL(rn.PDV,0) AS PDV, rn.Uvoz, rn.Status, rn.Status AS StatusID, tk.SkNaziv AS TipKontejnera, i.NapomenaZaRobu AS NapomenaZaPozicioniranje,  rn.Opis AS DodatniOpis," +
                                 "LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija   " +
                         " from  RadniNalogDrumski rn " +
                                 "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                 "inner join Automobili au on au.ID = rn.KamionID " +
                                 "inner join Izvoz i ON i.ID = rn.KontejnerID " +
                                 "left join TipKontenjera tk ON i.VrstaKontejnera = tk.ID " +
                                 "left join partnerjiKontOsebaMU pko ON pko.PaKOSifra = i.MesoUtovara AND pko.PaKOZapSt = i.KontaktOsoba " +
                                 "left join Partnerji pa ON pa.PaSifra = i.Klijent3 " +
                                 "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                 "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                 "left join MestaUtovara mu on i.MesoUtovara = mu.ID  " +
                                 "left join MestaUtovara mi on rn.MestoIstovara = mi.ID  " +
                                 "left join StatusVozila sv ON sv.ID = rn.Status  " +
                                 "where rn.Uvoz = 0 and ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID is not NULL AND rn.KamionID != 0  " +
                                 "      AND ISNULL(rn.Arhiviran, 0) <> 1  " +
                         " union all " +
                         " select  rn.ID, " +
                                   "LTRIM(RTRIM(pa.PaNaziv)) as Nalogodavac, " +
                                   "ik.BrojKontejnera," +
                                    "'' AS BrojKontejnera2,  ik.BookingBrodara," +
                                   "au.RegBr AS Kamion, " +
                                   "vv.Naziv as TipVozila, " +
                                   "au.ID as KamionID, " +
                                   "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, mu.Naziv  AS MestoUtovara, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,  (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, " +
                                   "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara,   mi.Naziv AS MestoIstovara, rn.AdresaIstovara,  rn.NalogID,  p.PaNaziv AS Prevoznik, " +
                                   "rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave," +
                                   " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena, CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera , rn.MestoPreuzimanjaKontejnera, " +
                                   "ik.NapomenaZaRobu as NapomenaZaPozicioniranje,  '' AS OdredisnaCarina, " +
                                   "'' as polaznaCarinarnica, '' as polaznaSpedicija, '' as OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt, '' AS OdredisnaSpedicijaKontakt, " +
                                   "ISNULL(rn.PDV,0) AS PDV, rn.Uvoz, rn.Status, rn.Status AS StatusID, tk.SkNaziv AS TipKontejnera, ik.NapomenaZaRobu AS NapomenaZaPozicioniranje,  rn.Opis AS DodatniOpis," +
                                   " LTRIM(RTRIM(mu.Naziv)) + ' - ' +  LTRIM(RTRIM(mi.Naziv)) AS Relacija  " +
                         " from     RadniNalogDrumski rn " +
                                   "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                   "inner join Automobili au on au.ID = rn.KamionID " +
                                   "inner join IzvozKonacna ik ON ik.ID = rn.KontejnerID " +
                                   "left join TipKontenjera tk ON ik.VrstaKontejnera = tk.ID " +
                                   "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = ik.MesoUtovara AND pko.PaKOZapSt = ik.KontaktOsoba " +
                                   "left join Partnerji pa ON pa.PaSifra = ik.Klijent3 " +
                                   "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                   "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                   "left join MestaUtovara mu on ik.MesoUtovara = mu.ID  " +
                                   "left join MestaUtovara mi on rn.MestoIstovara = mi.ID  " +
                                   "left join StatusVozila sv ON sv.ID = rn.Status  " +
                                   "where rn.Uvoz = 0 and rn.KamionID is NOT NULL and ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID != 0 " +
                                   "       AND ISNULL(rn.Arhiviran, 0) <> 1 " +
                         " union all " +
                         " select  rn.ID,  " +
                                   "LTRIM(RTRIM(pa.PaNaziv)) as Nalogodavac, " +
                                   "uk.BrojKontejnera," +
                                   " '' as BrojKontejnera2, 0 AS BookingBrodara," +
                                   "au.RegBr AS Kamion, " +
                                   "vv.Naziv as TipVozila, " +
                                   "au.ID as KamionID, " +
                                   "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara,mu.Naziv  AS MestoUtovara, rn.AdresaUtovara, uk.KontaktOsobe as KontaktOsobaUtovarIstovar, " +
                                   "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara, mi.Naziv AS MestoIstovara,  (Rtrim(pko.PaKOOpomba)) AS AdresaIstovara,  rn.NalogID,  p.PaNaziv AS Prevoznik, " +
                                   "rn.PoslataNajava,Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave," +
                                   " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena, CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera, rn.MestoPreuzimanjaKontejnera, " +
                                   " np.Naziv as NapomenaZaPozicioniranje, c.Naziv as OdredisnaCarina," +
                                   "'' as polaznaCarinarnica, '' as polaznaSpedicija, p2.PaNaziv as OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt, '' AS OdredisnaSpedicijaKontakt, " +
                                   "ISNULL(rn.PDV,0) AS PDV , rn.Uvoz, rn.Status, rn.Status AS StatusID, tk.SkNaziv AS TipKontejnera,  np.Naziv as NapomenaZaPozicioniranje, rn.Opis AS DodatniOpis," +
                                   "LTRIM(RTRIM(mi.Naziv)) + ' - ' +  LTRIM(RTRIM(mu.Naziv)) AS Relacija  " +
                         " from     RadniNalogDrumski rn " +
                                   "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                   "inner join Automobili au on au.ID = rn.KamionID " +
                                   "inner join VrstaManipulacije vm on vm.ID = rn.IDVrstaManipulacije " +
                                   "inner join UvozKonacna uk ON uk.ID = rn.KontejnerID " +
                                   "left join NapomenaZaPozicioniranje np ON np.ID = uk.NapomenaZaPozicioniranje " +
                                   "left join TipKontenjera tk ON uk.TipKontejnera = tk.ID " +
                                   "left join Carinarnice c on c.ID = uk.OdredisnaCarina " +
                                   "left join partnerjiKontOsebaMU pko ON pko.PaKOSifra = uk.MestoIstovara AND PaKOZapSt = uk.AdresaMestaUtovara " +
                                   "left join Partnerji pa ON pa.PaSifra = uk.Nalogodavac3 " +
                                   "LEFT JOIN Partnerji p2 on p2.PaSifra = uk.OdredisnaSpedicija " +
                                   "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                   "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                   "left join MestaUtovara mu on  rn.MestoUtovara = mu.ID  " +
                                   "left join MestaUtovara mi on  uk.MestoIstovara = mi.ID  " +
                                   "left join StatusVozila sv ON sv.ID = rn.Status  " +
                                   "where rn.Uvoz = 1 and rn.KamionID is NOT NULL  and ISNULL(RadniNalogOtkazan, 0) <> 1 AND rn.KamionID != 0 " +
                                   "       AND ISNULL(rn.Arhiviran, 0) <> 1  " +
                         " union all " +
                         " select   rn.ID,  " +
                                   "LTRIM(RTRIM(pa.PaNaziv)) as Nalogodavac, " +
                                   "u.BrojKontejnera," +
                                   "'' AS BrojKontejnera2, 0 AS BookingBrodara," +
                                   "au.RegBr AS Kamion, " +
                                   "vv.Naziv as TipVozila, " +
                                   "au.ID as KamionID, " +
                                   "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, mu.Naziv  AS MestoUtovara,rn.AdresaUtovara, u.KontaktOsobe as KontaktOsobaUtovarIstovar, " +
                                   "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara,  mi.Naziv AS MestoIstovara,  (Rtrim(pko.PaKOOpomba)) AS AdresaIstovara,  rn.NalogID,  p.PaNaziv AS Prevoznik, " +
                                   "rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave , " +
                                   " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena , CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera, rn.MestoPreuzimanjaKontejnera, " +
                                   "np.Naziv as NapomenaZaPozicioniranje, c.Naziv as OdredisnaCarina, '' as polaznaCarinarnica, '' as polaznaSpedicija, p2.PaNaziv as OdredisnaSpedicija,'' AS PolaznaSpedicijaKontakt, '' AS OdredisnaSpedicijaKontakt, " +
                                   "ISNULL(rn.PDV, 0) AS PDV , rn.Uvoz, rn.Status, rn.Status AS StatusID, tk.SkNaziv AS TipKontejnera,  np.Naziv as NapomenaZaPozicioniranje,  rn.Opis AS DodatniOpis ," +
                                   " LTRIM(RTRIM(mi.Naziv)) + ' - ' +  LTRIM(RTRIM(mu.Naziv)) AS Relacija" +
                         " from     RadniNalogDrumski rn " +
                                   "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                   "inner join Automobili au on au.ID = rn.KamionID " +
                                   "inner join Uvoz u ON u.ID = rn.KontejnerID " +
                                   "left join NapomenaZaPozicioniranje np ON np.ID = u.NapomenaZaPozicioniranje " +
                                   "left join TipKontenjera tk ON u.TipKontejnera = tk.ID " +
                                   "LEFT JOIN Carinarnice c on c.ID = u.OdredisnaCarina " +
                                   "left join partnerjiKontOsebaMU pko ON pko.PaKOSifra = u.MestoIstovara AND pko.PaKOZapSt = u.AdresaMestaUtovara " +
                                   "left join Partnerji pa ON pa.PaSifra = u.Nalogodavac3 " +
                                   "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                   "left join Partnerji p on au.PartnerID = p.PaSifra " +
                                   "LEFT JOIN Partnerji p2 on p2.PaSifra = u.OdredisnaSpedicija " +
                                   "left join MestaUtovara mu on  rn.MestoUtovara = mu.ID  " +
                                   "left join MestaUtovara mi on  u.MestoIstovara = mi.ID  " +
                                   "left join StatusVozila sv ON sv.ID = rn.Status  " +
                                   "where rn.Uvoz = 1 and rn.KamionID is NOT NULL  and ISNULL(RadniNalogOtkazan, 0) <> 1 and rn.KamionID != 0 " +
                                   "       AND ISNULL(rn.Arhiviran, 0) <> 1  " +
                         " union all " +
                         " select   rn.ID,  " +
                                   "LTRIM(RTRIM(pa.PaNaziv)) as Nalogodavac, " +
                                   "rn.BrojKontejnera," +
                                   "rn.BrojKontejnera2,rn.BookingBrodara AS BookingBrodara," +
                                   "au.regbr AS Kamion, " +
                                   "vv.Naziv as TipVozila, " +
                                   "au.ID as KamionID, " +
                                   "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, mu.Naziv  AS MestoUtovara, rn.AdresaUtovara,  rn.KontaktOsobaNaIstovaru AS KontaktOsobaUtovarIstovar, " +
                                   "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara,  mi.Naziv AS MestoIstovara , rn.AdresaIstovara AS AdresaIstovara, rn.NalogID, p.PaNaziv AS Prevoznik,  + " +
                                   "rn.PoslataNajava,Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave," +
                                   " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena , CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera, rn.MestoPreuzimanjaKontejnera, " +
                                   "dp.Napomena as NapomenaZaPozicioniranje, co.Naziv as OdredisnaCarina," +
                                   "cp.Naziv AS polaznaCarinarnica, rn.PolaznaSpedicija,  rn.OdredisnaSpedicija, rn.PolaznaSpedicijaKontakt, rn.OdredisnaSpedicijaKontakt, " +
                                   "ISNULL(rn.PDV, 0) AS PDV, rn.Uvoz, rn.Status, rn.Status AS StatusID, tk.SkNaziv AS TipKontejnera, LTRIM(RTRIM(dp.Napomena)) AS NapomenaZaPozicioniranje,  rn.Opis AS DodatniOpis," +
                                   " CASE WHEN rn.Uvoz IN (1, 2, 4)  THEN LTRIM(RTRIM(mi.Naziv)) +' - ' + LTRIM(RTRIM(mu.Naziv)) WHEN rn.Uvoz IN (0, 3, 5)  THEN LTRIM(RTRIM(mu.Naziv)) + ' - ' + LTRIM(RTRIM(mi.Naziv)) ELSE '' END AS Relacija  " +
                         " from     RadniNalogDrumski rn " +
                                   "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                   "inner join Automobili au on au.ID = rn.KamionID " +
                                   "left join Partnerji pa ON pa.PaSifra = rn.Klijent " +
                                   "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                   "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                   "left join MestaUtovara mu on  rn.MestoUtovara = mu.ID  " +
                                   "left join MestaUtovara mi on  rn.MestoIstovara = mi.ID  " +
                                   "left join DrumskiPozicioniranje dp ON dp.id = rn.NapomenaZaPozicioniranje " +
                                   "left join TipKontenjera tk ON rn.TipKontejnera = tk.ID " +
                                   "left join StatusVozila sv ON sv.ID = rn.Status  " +
                                   "LEFT JOIN Carinarnice cp ON cp.ID = rn.PolaznaCarinarnica " +
                                   "LEFT JOIN Carinarnice co ON co.ID = rn.PolaznaCarinarnica " +
                                   "where rn.Uvoz in (2, 3, 4, 5) and rn.NalogID > 0  and ISNULL(RadniNalogOtkazan, 0) <> 1 and rn.KamionID is not NULL AND rn.KamionID != 0" +
                                   "       AND ISNULL(rn.Arhiviran, 0) <> 1   ";

           // 3.
            var finalSelect = $@"
                                SELECT Detalji.*
                                FROM (
                                    {unionQueryBody}
                                ) AS Detalji
                                WHERE Detalji.ID IN ({idsInClause});
                            ";

            // 4. Izvršavanje upita
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            DataTable dt = new DataTable();

            using (SqlConnection c = new SqlConnection(s_connection))
            {
                try
                {
                    // Izvršavanje upita
                    var dataAdapter = new SqlDataAdapter(finalSelect, c);
                    dataAdapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception("Greška pri dohvatanju detalja za najavu.", ex);
                }
            }

            return dt;
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
        }

        private (string ImePrezime, string BrLK, string Telefon) DobaviVozaca(int kamionID)
        {
            string query = @"SELECT Vozac, LicnaKarta, BrojTelefona 
                     FROM Automobili 
                     WHERE ID = @KamionID";

            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@KamionID", kamionID);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string ime = reader["Vozac"].ToString();
                        string lk = reader["LicnaKarta"].ToString();
                        string tel = reader["BrojTelefona"].ToString();
                        return (ime, lk, tel);
                    }
                    else
                    {
                        return ("-", "-", "-");
                    }
                }
            }
        }

        private int ProveriPostojanjeRadnogNaloga(int? radniNalogDrumskiID)
        {
            SqlConnection conn1 = new SqlConnection(connection);
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM FakturaDrumski WHERE RadniNalogDrumskiID = @ID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", radniNalogDrumskiID);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0 ? 1 : 0;
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

        private int ProveriDaLiJeNajavaPoslata(int? radniNalogDrumskiID)
        {
            SqlConnection conn1 = new SqlConnection(connection);
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM RadniNalogDrumski WHERE ID = @ID and PoslataNajava = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", radniNalogDrumskiID);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0 ? 1 : 0;
                }
            }
        }

        private void btnArhiva_Click(object sender, EventArgs e)
        {
            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();

            try
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    // Proveri da li red nije novi prazan red
                    if (row.IsNewRow) continue;

                    //  (int) i KamionID (int)
             
                    string idsString = row.Cells["IdsRadniNalogDrumski"].Value?.ToString();

                    List<int> idjeviZaNajavu;
                    try
                    {
                        idjeviZaNajavu = idsString
                            // 1. Razdvaja string po zarezima (i uklanja prazne elemente)
                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            // 2. Za svaki dobijeni string element, pokušava da ga parsira u int
                            .Select(s => int.Parse(s.Trim()))
                            // 3. Rezultat pretvara u List<int>
                            .ToList();
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Greška u formatu ID-jeva naloga. Proverite podatke.");
                        return;
                    }

                    foreach (int id in idjeviZaNajavu)
                    {
                        try
                        {
                            ins.ArhiviranRadniNalogDrumski(id);
                        }
                        catch (Exception ex)
                        {
                            // po želji možeš prijaviti grešku za pojedinačni ID
                            MessageBox.Show($"Greška pri arhiviranju naloga ID={id}: {ex.Message}");
                        }
                    }
                }
                RefreshDataGrid3();
                RefreshDataGrid1();
                dataGridView2.ClearSelection();

            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void btnResetuj_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
            RefreshDataGrid2();
            RefreshDataGrid3();
        }

        private void ukloniDodeljenKamionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Niste izabrali nijedan zapis.", "Upozorenje",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView2.SelectedRows[0];
            var kamionVrednost = row.Cells["Kamion"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(kamionVrednost))
            {
                MessageBox.Show("Za označeni zapis kamion nije bio ni dodeljen.", "Informacija",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // potvrda brisanja
            var potvrda = MessageBox.Show(
                $"Da li ste sigurni da želite da uklonite kamion {kamionVrednost} iz ovog zapisa?",
                "Potvrda uklanjanja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (potvrda == DialogResult.Yes)
            {
                row.Cells["Kamion"].Value = null;
            }
        }
    }
}
