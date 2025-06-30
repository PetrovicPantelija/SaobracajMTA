using Org.BouncyCastle.Crypto;
using Saobracaj.Izvoz;
using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;


namespace Saobracaj.Drumski
{
    public partial class frmPakovanjeKamiona : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int dragRow = -1;
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
                foreach (Control control in Controls)
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
                foreach (Control control in tableLayoutPanel1.Controls)
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
                        " WHERE VoziloDrumskog = 1" + condition;

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand(select, conn);

            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


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
                                   "inner join VrstaManipulacije vm on vm.ID = rn.IDVrstaManipulacije " +
                                   "left join Partnerji pa ON pa.PaSifra = rn.Klijent " +
                                   "where rn.Uvoz in (2, 3) and rn.NalogID > 0 and (rn.KamionID is NULL or rn.KamionID = 0)";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

            if (dataGridView2.Columns.Contains("ID"))
            {
                dataGridView2.Columns["ID"].Visible = false;
            }
        }

        private void RefreshDataGrid3()
        {
                var select = "select  rn.ID, "+
                                     "pa.PaNaziv as Nalogodavac, " +
                                     "i.BrojKontejnera," +
                                     "au.RegBr AS Kamion, " +
                                     "p.PaNaziv AS Prevoznik, " +
                                     "vv.Naziv as TipVozila, " +
                                     "au.ID as KamionID, " +
                                     "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, " +
                                     "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara " +
                             " from  RadniNalogDrumski rn " +
                                     "inner join Automobili au on au.ID = rn.KamionID " +
                                     "inner join Izvoz i ON i.ID = rn.KontejnerID " +
                                     "left join Partnerji pa ON pa.PaSifra = i.Klijent3 " +
                                     "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                     "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                     "where rn.Uvoz = 0 and rn.KamionID is not NULL AND rn.KamionID != 0" +
                             " union all " +
                             " select  rn.ID, " +
                                       "pa.PaNaziv as Nalogodavac, " +
                                       "ik.BrojKontejnera," +
                                       "au.RegBr AS Kamion, " +
                                       "p.PaNaziv AS Prevoznik, " +
                                       "vv.Naziv as TipVozila, " +
                                       "au.ID as KamionID, " +
                                       "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, " +
                                       "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara " +
                             " from     RadniNalogDrumski rn " +
                                       "inner join Automobili au on au.ID = rn.KamionID " +
                                       "inner join IzvozKonacna ik ON ik.ID = rn.KontejnerID " +
                                       "left join Partnerji pa ON pa.PaSifra = ik.Klijent3 " +
                                       "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                       "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                       "where rn.Uvoz = 0 and rn.KamionID is NOT NULL AND rn.KamionID != 0 " +
                             " union all " +
                             " select  rn.ID,  " +
                                       "pa.PaNaziv as Nalogodavac, " +
                                       "uk.BrojKontejnera," +
                                       "au.RegBr AS Kamion, " +
                                       "p.PaNaziv AS Prevoznik, " +
                                       "vv.Naziv as TipVozila, " +
                                       "au.ID as KamionID, " +
                                       "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, " +
                                       "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara " +
                             " from     RadniNalogDrumski rn " +
                                       "inner join Automobili au on au.ID = rn.KamionID " +
                                       "inner join VrstaManipulacije vm on vm.ID = rn.IDVrstaManipulacije " +
                                       "inner join UvozKonacna uk ON uk.ID = rn.KontejnerID " +
                                       "left join Partnerji pa ON pa.PaSifra = uk.Nalogodavac3 " +
                                       "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                       "left join Partnerji p on au.PartnerID = p.PaSifra  " +
                                       "where rn.Uvoz = 1 and rn.KamionID is NOT NULL AND rn.KamionID != 0 " +
                             " union all " +
                             " select   rn.ID,  " +
                                       "pa.PaNaziv as Nalogodavac, " +
                                       "u.BrojKontejnera," +
                                       "au.RegBr AS Kamion, " +
                                       "p.PaNaziv AS Prevoznik, " +
                                       "vv.Naziv as TipVozila, " +
                                       "au.ID as KamionID, " +
                                       "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, " +
                                       "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara " +
                             " from     RadniNalogDrumski rn " +
                                       "inner join Automobili au on au.ID = rn.KamionID " +
                                       "inner join Uvoz u ON u.ID = rn.KontejnerID " +
                                       "left join Partnerji pa ON pa.PaSifra = u.Nalogodavac3 " +
                                       "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                       "left join Partnerji p on au.PartnerID = p.PaSifra " +
                                       "where rn.Uvoz = 1 and rn.KamionID is NOT NULL and rn.KamionID != 0 " +
                             " union all " +
                             " select   rn.ID,  " +
                                       "pa.PaNaziv as Nalogodavac, " +
                                       "rn.BrojKontejnera," +
                                       "au.regbr AS Kamion, " +
                                       "p.PaNaziv AS Prevoznik, " +
                                       "vv.Naziv as TipVozila, " +
                                       "au.ID as KamionID, " +
                                       "CONVERT(varchar,rn.DatumUtovara,104) AS DatumUtovara, " +
                                     "CONVERT(varchar,rn.DatumIstovara,104) AS DatumIstovara " +
                             " from     RadniNalogDrumski rn " +
                                       "inner join Automobili au on au.ID = rn.KamionID " +
                                       "left join Partnerji pa ON pa.PaSifra = rn.Klijent " +
                                       "left join VrstaVozila vv on au.VlasnistvoLegeta = vv.ID " +
                                       "left join Partnerji p on au.PartnerID = p.PaSifra  "+
                                       "where rn.Uvoz in (2, 3) and rn.NalogID > 0 and rn.KamionID is not NULL AND rn.KamionID = 0";
 

                SqlConnection conn = new SqlConnection(connection);
                var da = new SqlDataAdapter(select, conn);
                var ds = new DataSet();
                da.Fill(ds);
                dataGridView3.ReadOnly = true;
                dataGridView3.DataSource = ds.Tables[0];

                PodesiDatagridView(dataGridView3);

                if (dataGridView3.Columns.Contains("ID"))
                {
                    dataGridView3.Columns["ID"].Visible = false;
                }
                if (dataGridView3.Columns.Contains("KamionID"))
                {
                    dataGridView3.Columns["KamionID"].Visible = false;
                }
            //  }
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

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }
    }
}
