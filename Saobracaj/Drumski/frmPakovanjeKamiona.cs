using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Saobracaj.Drumski
{
    public partial class frmPakovanjeKamiona : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int dragRow = -1;
        private bool cellClickHandlerAttached = false;
        private Form aktivnaFormaPregleda;
        System.Windows.Forms.Label dragLabel = null;

        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

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

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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

        public frmPakovanjeKamiona()
        {
            InitializeComponent();
            ChangeTextBox();
            ChangeTextBoxTable();
            UcitajFiltere();
            RefreshDataGrid1();
            RefreshDataGrid2();
            RefreshDataGrid3();
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

                var select = "SELECT a.ID, vv.Naziv AS TipVozila, a.RegBr, a.Vozac, p.PaNaziv AS Prevoznik " +
                            " FROM Automobili a " +
                            " LEFT JOIN VrstaVozila vv on a.VlasnistvoLegeta = vv.ID " +
                            " LEFT JOIN Partnerji p on a.PartnerID = p.PaSifra  " +          
                            " WHERE VoziloDrumskog = 1 AND (" +
                                    "  NOT EXISTS( " +
                                      "    SELECT 1 " +
                                      "    FROM RadniNalogDrumski r " +
                                      "    WHERE  r.KamionID = a.ID " +
                                      "          AND ISNULL(r.Arhiviran, 0) <> 1" +
                                      "          AND(r.Status IS NULL OR r.Status NOT IN ( " + statusiZaUpit + "))" +
                                      ")) " +  condition;


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
        }

        private void RefreshDataGrid2()
        {
            var select = "select   rn.ID, rn.NalogID, rn.KontejnerID ,  " +
                                   "pa.PaNaziv as Nalogodavac, " +
                                   "i.BrojKontejnera," +
                                   "'' AS Kamion " +
                         "from     RadniNalogDrumski rn " +
                                   "inner join Izvoz i ON i.ID = rn.KontejnerID " +
                                   "left join Partnerji pa ON pa.PaSifra = i.Klijent3 " +
                                   "where rn.Uvoz = 0 and (rn.KamionID is NULL or rn.KamionID = 0) " +
                         "union all " +
                         "select   rn.ID, rn.NalogID, rn.KontejnerID ,  " +
                                   "pa.PaNaziv as Nalogodavac, " +
                                   "ik.BrojKontejnera," +
                                   "'' AS Kamion " +
                         "from     RadniNalogDrumski rn " +
                                   "inner join VrstaManipulacije vm on vm.ID = rn.IDVrstaManipulacije " +
                                   "inner join IzvozKonacna ik ON ik.ID = rn.KontejnerID " +
                                   "left join Partnerji pa ON pa.PaSifra = ik.Klijent3 " +
                                   "where rn.Uvoz = 0 and (rn.KamionID is NULL or rn.KamionID = 0) "+
                         "union all " +
                         "select   rn.ID, rn.NalogID, rn.KontejnerID ,  " +
                                   "pa.PaNaziv as Nalogodavac, " +
                                   "uk.BrojKontejnera," +
                                   "'' AS Kamion " +
                         "from     RadniNalogDrumski rn " +
                                   "inner join VrstaManipulacije vm on vm.ID = rn.IDVrstaManipulacije " +
                                   "inner join UvozKonacna uk ON uk.ID = rn.KontejnerID " +
                                   "left join Partnerji pa ON pa.PaSifra = uk.Nalogodavac3 " +
                                   "where rn.Uvoz = 1 and (rn.KamionID is NULL or rn.KamionID = 0) " +
                         "union all " +
                         "select   rn.ID, rn.NalogID, rn.KontejnerID ,  " +
                                   "pa.PaNaziv as Nalogodavac, " +
                                   "u.BrojKontejnera," +
                                   "'' AS Kamion " +
                         "from     RadniNalogDrumski rn " +
                                   "inner join VrstaManipulacije vm on vm.ID = rn.IDVrstaManipulacije " +
                                   "inner join Uvoz u ON u.ID = rn.KontejnerID " +
                                   "left join Partnerji pa ON pa.PaSifra = u.Nalogodavac3 " +
                                   "where rn.Uvoz = 1 and (rn.KamionID is NULL or rn.KamionID = 0) " +
                         "union all " +
                         "select   rn.ID, rn.NalogID, rn.KontejnerID ,  " +
                                   "pa.PaNaziv as Nalogodavac, " +
                                   "rn.BrojKontejnera," +
                                   "'' AS Kamion " +
                         "from     RadniNalogDrumski rn " +
                                   "left join Partnerji pa ON pa.PaSifra = rn.Klijent " +
                                   "where rn.Uvoz in (2, 3) and rn.NalogID > 0 and (rn.KamionID is NULL or rn.KamionID = 0)";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);
            dataGridView2.RowHeadersWidth = 30;

            if (dataGridView2.Columns.Contains("ID"))
            {
                dataGridView2.Columns["ID"].Visible = false;
            }
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
                var select = "select  rn.ID, " +
                                     "pa.PaNaziv as Nalogodavac, " +
                                     "i.BrojKontejnera," +
                                     "'' as BrojKontejnera2, " +
                                     "au.RegBr AS Kamion, " +
                                     "p.PaNaziv AS Prevoznik, " +
                                     "vv.Naziv as TipVozila, " +
                                     "au.ID as KamionID, " +
                                     "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, mu.Naziv  AS MestoUtovara, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,  (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, " +
                                     "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara,  mi.Naziv AS MestoIstovara , rn.AdresaIstovara, " +
                                     "rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave," +
                                     " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena, CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera," +
                                     "i.NapomenaZaRobu AS NapomenaZaPozicioniranje, rn.NalogID ,  '' AS OdredisnaCarina," +
                                     "'' as polaznaCarinarnica, '' as polaznaSpedicija, '' as OdredisnaSpedicija," +
                                     "ISNULL(rn.PDV,0) AS PDV, rn.Uvoz " +
                             " from  RadniNalogDrumski rn " +
                                     "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                     "inner join Automobili au on au.ID = rn.KamionID " +
                                     "inner join Izvoz i ON i.ID = rn.KontejnerID " +
                                     "left join partnerjiKontOsebaMU pko ON pko.PaKOSifra = i.MesoUtovara AND pko.PaKOZapSt = i.KontaktOsoba " +
                                     "left join Partnerji pa ON pa.PaSifra = i.Klijent3 " +
                                     "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                     "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                     "left join MestaUtovara mu on i.MesoUtovara = mu.ID  " +
                                     "left join MestaUtovara mi on rn.MestoIstovara = mi.ID  " +
                                     "where rn.Uvoz = 0 and rn.KamionID is not NULL AND rn.KamionID != 0  " +
                                     "      AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( " + statusiZaUpit + "))" +
                             " union all " +
                             " select  rn.ID, " +
                                       "pa.PaNaziv as Nalogodavac, " +
                                       "ik.BrojKontejnera," +
                                        "'' AS BrojKontejnera2," +
                                       "au.RegBr AS Kamion, " +
                                       "p.PaNaziv AS Prevoznik, " +
                                       "vv.Naziv as TipVozila, " +
                                       "au.ID as KamionID, " +
                                       "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, mu.Naziv  AS MestoUtovara, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,  (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, " +
                                       "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara,   mi.Naziv AS MestoIstovara, rn.AdresaIstovara," +
                                       "rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave," +
                                       " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena, CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera ," +
                                       "ik.NapomenaZaRobu as NapomenaZaPozicioniranje, rn.NalogID,  '' AS OdredisnaCarina, " +
                                       "'' as polaznaCarinarnica, '' as polaznaSpedicija, '' as OdredisnaSpedicija," +
                                       "ISNULL(rn.PDV,0) AS PDV, rn.Uvoz " + 
                             " from     RadniNalogDrumski rn " +
                                       "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                       "inner join Automobili au on au.ID = rn.KamionID " +
                                       "inner join IzvozKonacna ik ON ik.ID = rn.KontejnerID " +
                                       "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = ik.MesoUtovara AND pko.PaKOZapSt = ik.KontaktOsoba " +
                                       "left join Partnerji pa ON pa.PaSifra = ik.Klijent3 " +
                                       "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                       "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                       "left join MestaUtovara mu on ik.MesoUtovara = mu.ID  " +
                                       "left join MestaUtovara mi on rn.MestoIstovara = mi.ID  " +
                                       "where rn.Uvoz = 0 and rn.KamionID is NOT NULL AND rn.KamionID != 0 " +
                                       "       AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( " + statusiZaUpit + "))" +
                             " union all " +
                             " select  rn.ID,  " +
                                       "pa.PaNaziv as Nalogodavac, " +
                                       "uk.BrojKontejnera," +
                                       " '' as BrojKontejnera2," +
                                       "au.RegBr AS Kamion, " +
                                       "p.PaNaziv AS Prevoznik, " +
                                       "vv.Naziv as TipVozila, " +
                                       "au.ID as KamionID, " +
                                       "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara,mu.Naziv  AS MestoUtovara, rn.AdresaUtovara, uk.KontaktOsobe as KontaktOsobaUtovarIstovar, " +
                                       "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara, mi.Naziv AS MestoIstovara,  (Rtrim(pko.PaKOOpomba)) AS AdresaIstovara, " +
                                       "rn.PoslataNajava,Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave," +
                                       " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena, CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera," +
                                       " np.Naziv as NapomenaZaPozicioniranje, rn.NalogID, c.Naziv as OdredisnaCarina," +
                                       "'' as polaznaCarinarnica, '' as polaznaSpedicija, p2.PaNaziv as OdredisnaSpedicija, " +
                                       "ISNULL(rn.PDV,0) AS PDV , rn.Uvoz" +
                             " from     RadniNalogDrumski rn " +
                                       "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                       "inner join Automobili au on au.ID = rn.KamionID " +
                                       "inner join VrstaManipulacije vm on vm.ID = rn.IDVrstaManipulacije " +
                                       "inner join UvozKonacna uk ON uk.ID = rn.KontejnerID " +
                                       "LEFT JOIN Carinarnice c on c.ID = uk.OdredisnaCarina " +
                                       "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = uk.NapomenaZaPozicioniranje " +
                                       "left join partnerjiKontOsebaMU pko ON pko.PaKOSifra = uk.MestoIstovara AND PaKOZapSt = uk.AdresaMestaUtovara " +
                                       "left join Partnerji pa ON pa.PaSifra = uk.Nalogodavac3 " +
                                       "LEFT JOIN Partnerji p2 on p2.PaSifra = uk.OdredisnaSpedicija " +
                                       "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                       "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                       "left join MestaUtovara mu on  rn.MestoUtovara = mu.ID  " +
                                       "left join MestaUtovara mi on  uk.MestoIstovara = mi.ID  " +
                                       "where rn.Uvoz = 1 and rn.KamionID is NOT NULL AND rn.KamionID != 0 " +
                                       "       AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( " + statusiZaUpit + "))" +
                             " union all " +
                             " select   rn.ID,  " +
                                       "pa.PaNaziv as Nalogodavac, " +
                                       "u.BrojKontejnera," +
                                       "'' AS BrojKontejnera2," +
                                       "au.RegBr AS Kamion, " +
                                       "p.PaNaziv AS Prevoznik, " +
                                       "vv.Naziv as TipVozila, " +
                                       "au.ID as KamionID, " +
                                       "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, mu.Naziv  AS MestoUtovara,rn.AdresaUtovara, u.KontaktOsobe as KontaktOsobaUtovarIstovar, " +
                                       "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara,  mi.Naziv AS MestoIstovara,  (Rtrim(pko.PaKOOpomba)) AS AdresaIstovara," +
                                       "rn.PoslataNajava, Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave , " +
                                       " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena , CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera," +
                                       "np.Naziv as NapomenaZaPozicioniranje, rn.NalogID, c.Naziv as OdredisnaCarina, '' as polaznaCarinarnica, '' as polaznaSpedicija, p2.PaNaziv as OdredisnaSpedicija, " +
                                       "ISNULL(rn.PDV, 0) AS PDV , rn.Uvoz" +
                             " from     RadniNalogDrumski rn " +
                                       "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                       "inner join Automobili au on au.ID = rn.KamionID " +
                                       "inner join Uvoz u ON u.ID = rn.KontejnerID " +
                                       "LEFT JOIN Carinarnice c on c.ID = u.OdredisnaCarina " +
                                       "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = u.NapomenaZaPozicioniranje " +
                                       "left join partnerjiKontOsebaMU pko ON pko.PaKOSifra = u.MestoIstovara AND pko.PaKOZapSt = u.AdresaMestaUtovara " +
                                       "left join Partnerji pa ON pa.PaSifra = u.Nalogodavac3 " +
                                       "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                       "left join Partnerji p on au.PartnerID = p.PaSifra " +
                                       "LEFT JOIN Partnerji p2 on p2.PaSifra = u.OdredisnaSpedicija " +
                                       "left join MestaUtovara mu on  rn.MestoUtovara = mu.ID  " +
                                       "left join MestaUtovara mi on  u.MestoIstovara = mi.ID  " +
                                       "where rn.Uvoz = 1 and rn.KamionID is NOT NULL and rn.KamionID != 0 " +
                                       "       AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( " + statusiZaUpit + "))" +
                             " union all " +
                             " select   rn.ID,  " +
                                       "pa.PaNaziv as Nalogodavac, " +
                                       "rn.BrojKontejnera," +
                                       "rn.BrojKontejnera2," +
                                       "au.regbr AS Kamion, " +
                                       "p.PaNaziv AS Prevoznik, " +
                                       "vv.Naziv as TipVozila, " +
                                       "au.ID as KamionID, " +
                                       "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, mu.Naziv  AS MestoUtovara, rn.AdresaUtovara,  rn.KontaktOsobaNaIstovaru AS KontaktOsobaUtovarIstovar, " +
                                       "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara,  mi.Naziv AS MestoIstovara , rn.AdresaIstovara AS AdresaIstovara, " +
                                       "rn.PoslataNajava,Rtrim(dk.DeIme) + ' ' +  Rtrim(dk.DePriimek) as NajavuPoslao,CONVERT(varchar,rn.NajavaPoslataDatum,104) AS SlanjeNajave," +
                                       " CAST(rn.Cena AS DECIMAL(18,2)) AS Cena , CONVERT(varchar,rn.DtPreuzimanjaPraznogKontejnera,104) AS DtPreuzimanjaPraznogKontejnera," +
                                       "rn.NapomenaZaPozicioniranje as NapomenaZaPozicioniranje, rn.NalogID, rn.OdredisnaCarinarnica as OdredisnaCarina," +
                                       "rn.PolaznaCarinarnica as polaznaCarinarnica, rn.PolaznaSpedicijaKontakt as polaznaSpedicija,rn.OdredisnaSpedicijaKontakt as OdredisnaSpedicija, " +
                                       "ISNULL(rn.PDV, 0) AS PDV, rn.Uvoz " +
                             " from     RadniNalogDrumski rn " +
                                       "left join Delavci dk on dk.DeSifra = rn.NajavuPoslaoKorisnik " +
                                       "inner join Automobili au on au.ID = rn.KamionID " +
                                       "left join Partnerji pa ON pa.PaSifra = rn.Klijent " +
                                       "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                       "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                       "left join MestaUtovara mu on  rn.MestoUtovara = mu.ID  " +
                                       "left join MestaUtovara mi on  rn.MestoIstovara = mi.ID  " +
                                       "where rn.Uvoz in (2, 3) and rn.NalogID > 0 and rn.KamionID is not NULL AND rn.KamionID != 0" +
                                       "       AND ISNULL(rn.Arhiviran, 0) <> 1  AND (rn.Status IS NULL OR rn.Status NOT IN ( " + statusiZaUpit + "))";

                var da = new SqlDataAdapter(select, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView3.ReadOnly = true;
                dataGridView3.DataSource = ds.Tables[0];
            }
            // Pretpostavimo da je ime kolone "PoslataNajava"
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

            DodajDugmadKolonu();
            if (!cellClickHandlerAttached)
            {
                dataGridView3.CellClick += dataGridView3_CellContentClick;
                cellClickHandlerAttached = true;
            }

            PodesiDatagridView(dataGridView3);

            dataGridView3.RowHeadersWidth = 30; // ili bilo koja vrednost u pikselima

            string[] koloneZaSakrivanje = new string[] {
                    "ID", "KamionID", "Cena", "DtPreuzimanjaPraznogKontejnera", "AdresaUtovara", "AdresaIstovara", "MestoUtovara", "MestoIstovara", "BrojKontejnera2",
                    "KontaktOsobaUtovarIstovar", "NapomenaZaPozicioniranje", "NalogID" , "OdredisnaCarina", "PolaznaCarinarnica", "PolaznaSpedicija", "OdredisnaSpedicija",
                    "PDV", "Uvoz"
                    };

            foreach (string kolona in koloneZaSakrivanje)
            {
                if (dataGridView3.Columns.Contains(kolona))
                {
                    dataGridView3.Columns[kolona].Visible = false;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           // RefreshDataGrid3();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // selektovani red u dataGridView1 (kamion)
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Molimo selektujte jedan kamion.");
                return;
            }

            DataGridViewRow selectedKamion = dataGridView1.SelectedRows[0];
            int kamionId = Convert.ToInt32(selectedKamion.Cells[0].Value);

            // Prolazi kroz sve selektovane redove u dataGridView2 (radni nalozi)
            foreach (DataGridViewRow selectedNalog in dataGridView2.SelectedRows)
            {
                int nalogId = Convert.ToInt32(selectedNalog.Cells[0].Value);

                InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                ins.DodeliKamion(nalogId, kamionId);
            }

            RefreshDataGrid1();
            RefreshDataGrid2();
            RefreshDataGrid3();
        }

        private void frmPakovanjeKamiona_Load(object sender, EventArgs e)
        {
            //tableLayoutPanel1.SetColumnSpan(dataGridView3, 3);
            UcitajFiltere();
            RefreshDataGrid1();
            RefreshDataGrid2();
            RefreshDataGrid3();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            // Proveri da li je kliknuta kolona "Registracija"
            var columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (columnName != "RegBr")
            {
                txtIzabran.Text = "";
                dragLabel = null;
                return;
            }
            dragRow = e.RowIndex;
            if (dragLabel == null) dragLabel = new System.Windows.Forms.Label();
            dragLabel.Text = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
            txtIzabran.Text = dragLabel.Text;
        }

        private void dataGridView2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            //dragRow = e.RowIndex;
            //if (dragLabel == null) dragLabel = new System.Windows.Forms.Label();
            //dataGridView2[e.ColumnIndex, e.RowIndex].Value = txtIzabran.Text;
            var columnName = dataGridView2.Columns[e.ColumnIndex].Name;
            if (columnName != "Kamion") return; // Dozvoli upis samo u kolonu Kamion

            if (string.IsNullOrWhiteSpace(txtIzabran.Text))
            {
                MessageBox.Show("Niste izabrali registraciju iz liste vozila.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtIzabran.Text))
            {
                MessageBox.Show("Niste izabrali registraciju iz liste vozila.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dataGridView2[e.ColumnIndex, e.RowIndex].Value = txtIzabran.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int IDS = 0;
            string BrojKamiona = "";
            // Lista ID-eva redova koji su preneti
            List<int> prenetiIdjevi = new List<int>();
            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
           
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {

                    IDS = Convert.ToInt32(row.Cells[0].Value.ToString());
                    BrojKamiona = row.Cells[5].Value.ToString();
                    ins.DodeliKamionP(IDS, BrojKamiona);
                    prenetiIdjevi.Add(IDS); // Pamti koje su preneti

                }
                RefreshDataGrid3();
                RefreshDataGrid2();
                RefreshDataGrid1();

                dataGridView3.ClearSelection();

                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (!row.IsNewRow && prenetiIdjevi.Contains(Convert.ToInt32(row.Cells["ID"].Value)))
                    {
                        row.Selected = true;
                    }
                }

                // Opcionalno: skrol do prvog selektovanog
                if (dataGridView3.SelectedRows.Count > 0)
                {
                    dataGridView3.FirstDisplayedScrollingRowIndex = dataGridView3.SelectedRows[0].Index;
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
            List<int> prenetiIdjevi = new List<int>();
            try
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    // Proveri da li red nije novi prazan red
                    if (row.IsNewRow) continue;

                    //  (int) i KamionID (int)
                    int id = Convert.ToInt32(row.Cells["ID"].Value);
                    int kamion = Convert.ToInt32(row.Cells["KamionID"].Value);
                    prenetiIdjevi.Add(id); // Pamti koje su preneti

                    ins.VratiKamionUNerasporedjene(id, kamion);
                }
                RefreshDataGrid3();
                RefreshDataGrid2();
                RefreshDataGrid1();
                dataGridView2.ClearSelection();

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow && prenetiIdjevi.Contains(Convert.ToInt32(row.Cells["ID"].Value)))
                    {
                        row.Selected = true;
                    }
                }

                // Opcionalno: skrol do prvog selektovanog
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.SelectedRows[0].Index;
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void btnNajavaVozila_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 0)
            {
                MessageBox.Show("Morate selektovati makar jedan red!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int? poslataNajava = 0;

            int? radniNalogDrumskiID = 0;
            if (dataGridView3.SelectedRows[0].Cells["ID"].Value != DBNull.Value && int.TryParse(dataGridView3.SelectedRows[0].Cells["ID"].Value.ToString(), out int parsedRadniNalogDrumskiID))
                radniNalogDrumskiID = parsedRadniNalogDrumskiID;

            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
            poslataNajava = ProveriDaLiJeNajavaPoslata (radniNalogDrumskiID);
            if (poslataNajava > 0)
            {
                DialogResult result = MessageBox.Show(
                       "Najava za ovaj nalog je već poslata.\nDa li želite da je ponovo pošaljete?",
                       "Upozorenje",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return; // prekida dalje izvršavanje metode
                }
            }

            int temp = PostaviVrednostZaposleni();
            int? NajavuPoslaoKorisnik = temp == 0 ? (int?)null : temp;

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<html><body>");

            //  Podaci iz baze
            string nalogodavac = dataGridView3.SelectedRows[0].Cells["Nalogodavac"].Value?.ToString() ?? "";
            string datumPreuzimanja = dataGridView3.SelectedRows[0].Cells["DatumUtovara"].Value?.ToString();

            string odredisnaCarinarnica = dataGridView3.SelectedRows[0].Cells["OdredisnaCarina"].Value?.ToString();
            string napomenaZaPozicioniranje = dataGridView3.SelectedRows[0].Cells["NapomenaZaPozicioniranje"].Value?.ToString();

            string carinjenje = " na carinjenju";

            string polaznaCarinarnica = dataGridView3.SelectedRows[0].Cells["polaznaCarinarnica"].Value?.ToString() ?? "";
            int PDV = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells["PDV"].Value);
            if (PDV == 1 && string.IsNullOrEmpty(polaznaCarinarnica) && string.IsNullOrEmpty(odredisnaCarinarnica))
                carinjenje = " na istovaru";
            int Uvoz = -1;
            var cellValue = dataGridView3.SelectedRows[0].Cells["Uvoz"].Value;
            if (cellValue != DBNull.Value && int.TryParse(cellValue.ToString(), out int parsedUvozID))
                Uvoz = parsedUvozID;
            string datumUtovara = "";
            if (Uvoz == 0 || Uvoz == 3)
             datumUtovara = dataGridView3.SelectedRows[0].Cells["DatumUtovara"].Value?.ToString();
            else if(Uvoz == 1 || Uvoz == 2)
                datumUtovara = dataGridView3.SelectedRows[0].Cells["DatumIstovara"].Value?.ToString();
           
            //  Uvodni tekst
            htmlBuilder.AppendLine("<p>Poštovani,</p>");
            htmlBuilder.AppendLine($"<p>Podaci vozila koje danas preuzima kontejner za <b>{nalogodavac}</b>,</p>");
            htmlBuilder.AppendLine($"<p>Kontejner preuzima <b>{datumPreuzimanja:dd.MM.yyyy}</b></p>");
            htmlBuilder.AppendLine($"<p>Na <b>{odredisnaCarinarnica}</b> je <b>{datumUtovara:dd.MM.yyyy}</b> {carinjenje}</p>");
            htmlBuilder.AppendLine($"<p><b>{napomenaZaPozicioniranje}</b></p>");
            
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
               
                string kontejnerString = row.Cells["BrojKontejnera"].Value?.ToString() ?? "";
               
                if (!string.IsNullOrEmpty(row.Cells["BrojKontejnera2"].Value?.ToString()))
                    kontejnerString += ", " + row.Cells["BrojKontejnera2"].Value?.ToString();
                string cena = row.Cells["Cena"].Value?.ToString() ?? "";
                string kontejner = kontejnerString;
                string tipVozila = row.Cells["TipVozila"].Value?.ToString() ?? "";
                string tablice = row.Cells["Kamion"].Value?.ToString() ?? "";
                int kamionID = Convert.ToInt32(row.Cells["KamionID"].Value);
                (string vozac, string brLK, string telefon) = DobaviVozaca(kamionID);
                htmlBuilder.AppendLine($"<p style='color:red; font-weight:bold;'>Molimo vas notirajte, cena za ovu relaciju je {cena} EUR</p>");

                htmlBuilder.AppendLine("<table border='1' cellpadding='4' cellspacing='0' style='border-collapse: collapse; font-family: Arial; font-size: 14px; margin-bottom: 15px;'>");
                htmlBuilder.AppendLine($"<tr><td><b>Kontejner:</b></td><td>{kontejner}</td></tr>");
                htmlBuilder.AppendLine($"<tr><td><b>Kamion i vrsta:</b></td><td>{tipVozila}</td></tr>");
                htmlBuilder.AppendLine($"<tr><td><b>Kamion - tablice:</b></td><td>{tablice}</td></tr>");
                htmlBuilder.AppendLine($"<tr><td><b>Vozač:</b></td><td>{vozac}</td></tr>");
                htmlBuilder.AppendLine($"<tr><td><b>BR. L.K:</b></td><td>{brLK}</td></tr>");
                htmlBuilder.AppendLine($"<tr><td><b>MOB VOZAČA:</b></td><td>{telefon}</td></tr>");

                int Id = Convert.ToInt32(row.Cells["ID"].Value);

                ins.UpdateRadniNalogDrumskiPoslataNajava(Id, NajavuPoslaoKorisnik);
                InsertFakture insf = new InsertFakture();
                int? vecPostojiFaktura = 0;

                if (radniNalogDrumskiID > 0)
                {
                    vecPostojiFaktura = ProveriPostojanjeRadnogNaloga(radniNalogDrumskiID);
                    if (vecPostojiFaktura == 0)
                        insf.InsFaktura(radniNalogDrumskiID);

                }
                htmlBuilder.AppendLine("</table>");
            }

            htmlBuilder.AppendLine("</body></html>");

            // Kopiraj kao HTML u clipboard
            SetClipboardHtml(htmlBuilder.ToString());
            MessageBox.Show("Podaci su kopirani u clipboard.");

            RefreshDataGrid3();
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


        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 )
            {

                int poslateInstrukcije = 0;
                var grid = dataGridView3;
                var kolona = grid.Columns[e.ColumnIndex].Name;

                if (kolona == "Instrukcije")
                {

                    if (dataGridView3.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                    {
                        var row = dataGridView3.Rows[e.RowIndex];
                        int? radniNalogDrumskiID = 0;
                        if (row.Cells["ID"].Value != DBNull.Value && int.TryParse(row.Cells["ID"].Value.ToString(), out int parsedRadniNalogDrumskiID))
                            radniNalogDrumskiID = parsedRadniNalogDrumskiID;
                        string kontejnerString = row.Cells["BrojKontejnera"].Value?.ToString() ?? "";

                        poslateInstrukcije = ProveriDaLiJePorukaPoslata(radniNalogDrumskiID);
                        if (poslateInstrukcije > 0)
                        {
                            DialogResult result = MessageBox.Show(
                                   "Za ovaj nalog instrukcije su već poslate.\nDa li želite da je ponovo pošaljete?",
                                   "Upozorenje",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question);

                            if (result == DialogResult.No)
                            {
                                return; // prekida dalje izvršavanje metode
                            }
                        }

                        // Čitanje vrednosti iz reda
                      
                        if (!string.IsNullOrEmpty(row.Cells["BrojKontejnera2"].Value?.ToString()))
                            kontejnerString += ", " + row.Cells["BrojKontejnera2"].Value?.ToString();
                        string datumUtovara = row.Cells["DatumUtovara"].Value?.ToString();
                        string datumIstovara = row.Cells["DatumIstovara"].Value?.ToString();
                        string propratnicuRadi = row.Cells["Nalogodavac"].Value?.ToString();
                        string mestoUtovara = row.Cells["MestoUtovara"].Value?.ToString(); 
                        string adresaUtovara = row.Cells["AdresaUtovara"].Value?.ToString(); 
                        string mestoIstovara = row.Cells["MestoIstovara"].Value?.ToString();
                        string adresaIstovara = row.Cells["AdresaIstovara"].Value?.ToString(); 
                        string brojNaloga = row.Cells["NalogID"].Value?.ToString();
                        string kontaktUtovaraIstovara = row.Cells["KontaktOsobaUtovarIstovar"].Value?.ToString();
                        string napomenaZaPozicioniranje = row.Cells["NapomenaZaPozicioniranje"].Value?.ToString();
                        string odredisnaCarinarnica = row.Cells["OdredisnaCarina"].Value?.ToString();
                        string polaznaCarinarnica = row.Cells["PolaznaCarinarnica"].Value?.ToString();
                        string polaznaSpedicija = row.Cells["PolaznaSpedicija"].Value?.ToString();
                        string odredisnaSpedicija = row.Cells["OdredisnaSpedicija"].Value?.ToString();


                        // Formiranje poruke
                        string poruka = $"Kontejner {kontejnerString} preuzimate {datumUtovara} na {mestoUtovara}\n" +
                                        $"Propratnicu Vam radi {polaznaCarinarnica} {polaznaSpedicija} \n" +
                                        $"Javljate se {datumIstovara} na {odredisnaCarinarnica}: {odredisnaSpedicija}\n" +
                                        $"Kontejner istovarate {mestoIstovara} {adresaIstovara}\n" +
                                        $",{kontaktUtovaraIstovara}\n" +
                                        $"Posebni uslovi transporta: {napomenaZaPozicioniranje}\n" +
                                        $"Broj naloga: {brojNaloga}";

                        InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                        ins.SnimiToken(radniNalogDrumskiID);
                        int temp = PostaviVrednostZaposleni();
                        int? NajavuPoslaoKorisnik = temp == 0 ? (int?)null : temp;
                        ins.PoslateInstrukcije(radniNalogDrumskiID, NajavuPoslaoKorisnik);
                        // Prikaz
                        MessageBox.Show(poruka, "Tekst za Viber", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Alternativa ako koristiš npr. RichTextBox umesto MessageBox-a
                        // richTextBox1.Text = poruka;
                    }
                }
                else if (kolona == "Upload")
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

        private void btnUploadDokumenta_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();

            try
            {
                foreach (DataGridViewRow row in dataGridView3.SelectedRows)
                {
                    // Proveri da li red nije novi prazan red
                    if (row.IsNewRow) continue;

                    //  (int) i KamionID (int)
                    int id = Convert.ToInt32(row.Cells["ID"].Value);

                    ins.ArhiviranRadniNalogDrumski(id);
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
    }
}
