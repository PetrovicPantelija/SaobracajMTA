using Microsoft.Office.Interop.Excel;
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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;

namespace Saobracaj.VSD
{
    public partial class frmDnevniExcel : Form
    {
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

                    for (int i = 2; i <= rowCount; i++)
                    {
                        for (int j = 1; j <= 8; j++)
                        {
                            dt.Columns.Add(excelRange.Cells[i, j].Value2.ToString());
                        }
                        break;
                    }
                    //Get Row Data of Excel              
                    int rowCounter;  //This variable is used for row index number
                    for (int i = 3; i <= rowCount; i++) //Loop for available row of excel data
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

        private void FillDG2Konacna()
        { 
        
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {


                InsertUvozKonacna ins = new InsertUvozKonacna();
              //  ins.InsUvozNHMDiana(Convert.ToInt32(txtID.Text), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), Convert.ToDouble(row.Cells[3].Value.ToString()), Convert.ToDouble(row.Cells[4].Value.ToString()), Convert.ToDouble(row.Cells[5].Value.ToString()), Convert.ToDouble(row.Cells[6].Value.ToString()), row.Cells[7].Value.ToString());


            }

            FillDG2Konacna();
        }
    }
}
