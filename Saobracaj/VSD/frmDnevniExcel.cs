using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.Win32;
using Saobracaj.Sifarnici;
using Saobracaj.Uvoz;
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
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Web.Mail;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;


namespace Saobracaj.VSD
{
    public partial class frmDnevniExcel : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        System.Net.Mail.MailMessage mailMessage;
        public frmDnevniExcel()
        {
            InitializeComponent();
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

                    for (int i = 1; i <= rowCount; i++)
                    {
                        for (int j = 1; j <= 7; j++)
                        {
                            dt.Columns.Add(excelRange.Cells[i, j].Value2.ToString());
                        }
                        break;
                    }
                    //Get Row Data of Excel              
                    int rowCounter;  //This variable is used for row index number
                    for (int i = 2; i <= rowCount; i++) //Loop for available row of excel data
                    {
                        row = dt.NewRow();  //assign new row to DataTable
                        rowCounter = 0;
                        for (int j = 1; j <= 7; j++) //Loop for available column of excel data
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

        private void FillDG2Konacna()
        { 
        
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {


                InsertVSDDnevni ins = new InsertVSDDnevni();
              //  ins.InsUvozNHMDiana(Convert.ToInt32(txtID.Text), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), Convert.ToDouble(row.Cells[3].Value.ToString()), Convert.ToDouble(row.Cells[4].Value.ToString()), Convert.ToDouble(row.Cells[5].Value.ToString()), Convert.ToDouble(row.Cells[6].Value.ToString()), row.Cells[7].Value.ToString());


            }

            FillDG2Konacna();
        }

        private void frmDnevniExcel_Load(object sender, EventArgs e)
        {

            dtpNaDan.Value = DateTime.Now;
            FillCombo();
         
            VSDDataSet1TableAdapters.PlanViewTableAdapter ta = new VSDDataSet1TableAdapters.PlanViewTableAdapter();
            VSDDataSet1.PlanViewDataTable dt = new VSDDataSet1.PlanViewDataTable();
            
           

            ta.Fill(dt);
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dt;

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.ReportPath = "rptVSD.rdlc";

            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();


            
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var dir = "Select ID,Naziv from [Plan] order by ID DESC";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new System.Data.DataSet();
            dirAD.Fill(dirDS);
            cboPlan.DataSource = dirDS.Tables[0];
            cboPlan.DisplayMember = "Naziv";
            cboPlan.ValueMember = "ID";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string cuvaj = "panta0307@yahoo.com";
                mailMessage = new System.Net.Mail.MailMessage("disp@kprevoz.co.rs", "mtasoftver@gmail.com");
                mailMessage.CC.Add(cuvaj);
                mailMessage.Subject = "Dnevni izvestaj";

                var select = "   SELECT MAx(dbo.DnevniERP.Datum) as NaDatum, MAx(dbo.DnevniERP.Komercijalista) as Komercijalista, MAx(dbo.DnevniERP.Brend) as Brend, " +
               " Sum(dbo.DnevniERP.Kolicina) as KolicinaSum,  Sum(dbo.DnevniERP.PVrednost) as UkupnaProdaja " +
" FROM dbo.DnevniERP  where dbo.DnevniERP.Komercijalista = 'ALEKSANDAR.JOVIC' and Brend = '1.REVLON'";
                var dataAdapter = new SqlDataAdapter(select, connection);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new System.Data.DataSet();
                dataAdapter.Fill(ds);
                string body = "";

                body = body + "PRODAJA UČINAK: <br/><br/>";
                foreach (DataRow myRow in ds.Tables[0].Rows)
                {
                    body = body + "Na datum: " + myRow["NaDatum"].ToString() + "<br/>";
                    body = body + "Komercijalista: " + myRow["Komercijalista"].ToString() + "<br/>";
                    body = body + "Brend: " + myRow["Brend"].ToString() + "<br/>";
                    body = body + "Ukupna kolicina: " + myRow["KolicinaSum"].ToString() + "<br/>";
                    body = body + "Ukupna vrednost: " + myRow["UkupnaProdaja"].ToString() + "<br/>";

                    body = body + "Salje na mail (dok je u fazi testiranja sluzi za proveru mail adresa):  <br/><br/>";
                }
                body = body + "Srdačan pozdrav, <br/>" + "Prodajno odeljenje VSD";

                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "mail.kprevoz.co.rs";
              


                smtpClient.Port = 25;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential("disp@kprevoz.co.rs", "QmoqV}%Ep$0@");

                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                MessageBox.Show("Uspesno poslato");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
