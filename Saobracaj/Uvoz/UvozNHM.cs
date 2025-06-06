﻿using Microsoft.Office.Interop.Excel;
using Saobracaj.Sifarnici;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;


using System.Runtime.InteropServices;



namespace Saobracaj.Uvoz
{
    public partial class UvozNHM : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int NHMObrni = 0;
        int pomNerasporedjeni = 0;
        public UvozNHM()
        {
            InitializeComponent();
        }

        public UvozNHM(string ID, int Nerasporedjeni)
        {
            InitializeComponent();
            txtID.Text = ID;
           
            ChangeTextBox();
            pomNerasporedjeni = Nerasporedjeni;
            if (Nerasporedjeni == 0)
            { FillDG2(); }
            else 
            { FillDG2Konacna(); }
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;



                foreach (Control control in this.Controls)
                {

                }


                foreach (Control control in this.Controls)
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



        private void button2_Click(object sender, EventArgs e)
        {
            string file = "";   //variable for the Excel File Location
            System.Data.DataTable dt = new System.Data.DataTable();   //container for our excel data
            DataRow row;
            int Dodati = 1;
            DialogResult result = openFileDialog1.ShowDialog();  // Show the dialog.
            if (result == DialogResult.OK)   // Check if Result == "OK".
            {
                file = openFileDialog1.FileName; //get the filename with the location of the file
                try

                {
                    //Create Object for Microsoft.Office.Interop.Excel that will be use to read excel file

                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                    Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(file);

                    Microsoft.Office.Interop.Excel._Worksheet excelWorksheet = excelWorkbook.Sheets[1];

                    Microsoft.Office.Interop.Excel.Range excelRange = excelWorksheet.UsedRange;


                    int rowCount = excelRange.Rows.Count;  //get row count of excel data

                    int colCount = excelRange.Columns.Count; // get column count of excel data

                    //Get the first Column of excel file which is the Column Name                  

                    for (int i = 5; i <= rowCount; i++)
                    {
                        for (int j = 1; j <= 8;  j++)
                        {
                            dt.Columns.Add(excelRange.Cells[i, j].Value2.ToString());
                        }
                        break;
                    }
                    //Get Row Data of Excel              
                    int rowCounter;  //This variable is used for row index number
                    for (int i = 6; i <= rowCount; i++) //Loop for available row of excel data
                    {
                        row = dt.NewRow();  //assign new row to DataTable
                        rowCounter = 0;
                        for (int j = 1; j <= 8; j++) //Loop for available column of excel data
                        {
                            //check if cell is empty
                            if (excelRange.Cells[i, j] != null && excelRange.Cells[i, j].Value2 != null)
                            {
                                row[rowCounter] = excelRange.Cells[i, j].Value2.ToString();

                            }
                            else
                            {
                                row[j] = "";
                                if (j == 1)
                                {
                                    Dodati = 0;
                                    break;
                                }
                            }

                            rowCounter++;
                        }
                        if (Dodati == 1)
                        {
                            dt.Rows.Add(row);
                        }


                        //add row to DataTable
                    }

                    dataGridView1.DataSource = dt; //assign DataTable as Datasource for DataGridview

                    //Close and Clean excel process
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Marshal.ReleaseComObject(excelRange);
                    Marshal.ReleaseComObject(excelWorksheet);
                    excelWorkbook.Close();
                    Marshal.ReleaseComObject(excelWorkbook);

                    //quit 
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sredite Excel radi ucitavanja, izbrisite ukupno redove na kraju, da vam tabela pocinje od 5 reda naslov itd");
                }
            }
        }

        private void UvozNHM_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);

            var nhm = "";
            if (chkInterni.Checked == true)
            {
                nhm = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by Naziv ";
            }
            else
            {
                nhm = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM order by Naziv";
            }

            var nhmSAD = new SqlDataAdapter(nhm, conn);
            var nhmSDS = new DataSet();
            nhmSAD.Fill(nhmSDS);
            cboNHM.DataSource = nhmSDS.Tables[0];
            cboNHM.DisplayMember = "Naziv";
            cboNHM.ValueMember = "ID";

            if (pomNerasporedjeni == 0)
            { 
            chkNerasporedjeni.Checked = true;
            }
            else
            {
                chkNerasporedjeni .Checked = false;
            }
        }

        private void OpstiInterni(int Interni)
        {
            SqlConnection conn = new SqlConnection(connection);
            /*
          
            */
            if (Interni == 1)
            {
                switch (NHMObrni)
                {
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM where Interni = 1 order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                switch (NHMObrni)
                {
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }




        }

        int OpstiProm = 0;
        int InterniProm = 0;
        int PrviPut = 0;
        int NeRadiOpsti = 0;
        int NeRadiInterni = 0;

        private void chkInterni_CheckedChanged(object sender, EventArgs e)
        {


            if (InterniProm == 0 || NeRadiInterni == 1)
            {
                if (chkInterni.Checked == true)
                {
                    OpstiInterni(1);
                }
                else
                {
                    OpstiInterni(0);

                }


            }
            if (PrviPut == 0)
            {
                PrviPut = 1;
                NeRadiOpsti = 1;
            }

            if (chkInterni.Checked == true)
            {
                chkOpsti.Checked = false;
            }
            else
            {
                chkOpsti.Checked = true;

            }


            InterniProm = 0;
            OpstiProm = 1;
        }

        private void chkOpsti_CheckedChanged(object sender, EventArgs e)
        {
            if (OpstiProm == 0 || NeRadiOpsti == 1)
            {
                if (chkOpsti.Checked == true)
                {
                    OpstiInterni(0);
                }
                else
                {
                    OpstiInterni(1);

                }

            }
            if (PrviPut == 0)
            {
                PrviPut = 1;
                NeRadiInterni = 1;
            }

            if (chkOpsti.Checked == true)
            {
                chkInterni.Checked = false;
            }
            else
            {
                chkInterni.Checked = true;

            }

            InterniProm = 1;
            OpstiProm = 0;
        }

        private void FillDG2()
        {
            if (txtID.Text == "")
            {
                txtID.Text = "0";
            }
            var select = " SELECT     UvozNHM.ID, NHM.Broj, UvozNHM.IDNHM, NHM.Naziv, KomercijalniNaziv, TarifniBroj, BrojKoleta, Neto, Bruto, Vrednost, Valuta FROM NHM INNER JOIN " +
                      " UvozNHM ON NHM.ID = UvozNHM.IDNHM where Uvoznhm.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by Uvoznhm.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];


            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Broj";
            dataGridView2.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "ID";
            dataGridView2.Columns[2].Width = 20;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "NHM";
            dataGridView2.Columns[3].Width = 400;



        }

        private void FillDG2Konacna()
        {
            if (txtID.Text == "")
            {
                txtID.Text = "0";
            }
            var select = " SELECT     UvozKonacnaNHM.ID, NHM.Broj, UvozKonacnaNHM.IDNHM, NHM.Naziv, KomercijalniNaziv, TarifniBroj, BrojKoleta, Neto, Bruto, Vrednost, Valuta FROM NHM INNER JOIN " +
                      " UvozKonacnaNHM ON NHM.ID = UvozKonacnaNHM.IDNHM where UvozKonacnanhm.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by UvozKonacnanhm.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];


            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkGray;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(240, 240, 248); ;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 20;

            DataGridViewColumn column2 = dataGridView2.Columns[1];
            dataGridView2.Columns[1].HeaderText = "Broj";
            dataGridView2.Columns[1].Width = 100;

            DataGridViewColumn column3 = dataGridView2.Columns[2];
            dataGridView2.Columns[2].HeaderText = "ID";
            dataGridView2.Columns[2].Width = 20;

            DataGridViewColumn column4 = dataGridView2.Columns[3];
            dataGridView2.Columns[3].HeaderText = "NHM";
            dataGridView2.Columns[3].Width = 400;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUvozNHM(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNHM.SelectedValue));
          //  VratiADRIzNHM(Convert.ToInt32(cboNHM.SelectedValue)); // Nije potrebno jer ovde ionako ne mo\e da se vrati

            if (chkNerasporedjeni.Checked == true )
            {
                FillDG2();
            }
            else
            {
                FillDG2Konacna();
            }
           
        }

     

        private void button11_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            if (chkInterni.Checked == true)
            {
                switch (NHMObrni)
                {
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM where Interni = 1 order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM where Interni = 1 order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                switch (NHMObrni)
                {
                    case 1:
                        {
                            var nhm = "Select ID,Rtrim(Broj) + '-' + (Rtrim(Naziv)) as Naziv from NHM order by NHM.Broj";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 0;
                            break;

                        }
                    case 0:
                        {
                            var nhm = "Select ID,Rtrim(Naziv) + '-' + (Rtrim(Broj)) as Naziv from NHM order by NHM.Naziv";
                            var nhmSAD = new SqlDataAdapter(nhm, conn);
                            var nhmSDS = new DataSet();
                            nhmSAD.Fill(nhmSDS);
                            cboNHM.DataSource = nhmSDS.Tables[0];
                            cboNHM.DisplayMember = "Naziv";
                            cboNHM.ValueMember = "ID";
                            NHMObrni = 1;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();

            if (chkNerasporedjeni.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    uvK.DelUvozNHM(Convert.ToInt32(row.Cells[0].Value.ToString()));


                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    uvK.DelUvozKonacnaNHM(Convert.ToInt32(row.Cells[0].Value.ToString()));


                }
            }


            if (chkNerasporedjeni.Checked == true)
            {
                FillDG2();
            }
            else
            {
                FillDG2Konacna();
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (chkNerasporedjeni.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {


                    InsertUvozKonacna ins = new InsertUvozKonacna();
                    ins.InsUvozNHMDiana(Convert.ToInt32(txtID.Text), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), Convert.ToDouble(row.Cells[3].Value.ToString()), Convert.ToDouble(row.Cells[4].Value.ToString()), Convert.ToDouble(row.Cells[5].Value.ToString()), Convert.ToDouble(row.Cells[6].Value.ToString()), row.Cells[7].Value.ToString());


                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {


                    InsertUvozKonacna ins = new InsertUvozKonacna();
                    ins.InsUvozKonacnaNHMDiana(Convert.ToInt32(txtID.Text), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), Convert.ToDouble(row.Cells[3].Value.ToString()), Convert.ToDouble(row.Cells[4].Value.ToString()), Convert.ToDouble(row.Cells[5].Value.ToString()), Convert.ToDouble(row.Cells[6].Value.ToString()), row.Cells[6].Value.ToString());


                }
            }

            InsertUvozKonacna upd = new InsertUvozKonacna();





            if (chkNerasporedjeni.Checked == true)
            {
                FillDG2();
                upd.UpdUvozNHMTezine(Convert.ToInt32(txtID.Text), 0);

            }
            else
            {
                FillDG2Konacna();
                upd.UpdUvozNHMTezine(Convert.ToInt32(txtID.Text), 1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (chkNerasporedjeni.Checked == true)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected == true)
                    {
                        txtNHMID.Text = row.Cells[0].Value.ToString();
                        InsertUvozKonacna del = new InsertUvozKonacna();
                        del.DelUvozNHMDiana(Convert.ToInt32(txtNHMID.Text));

                    }

                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected == true)
                    {
                        txtNHMID.Text = row.Cells[0].Value.ToString();
                        InsertUvozKonacna del = new InsertUvozKonacna();
                        del.DelUvozKonacnaNHMDiana(Convert.ToInt32(txtNHMID.Text));

                    }

                }


            }
            if (chkNerasporedjeni.Checked == true)
            {
                FillDG2();
            }
            else
            {
                FillDG2Konacna();
            }
        }
    }
    }
