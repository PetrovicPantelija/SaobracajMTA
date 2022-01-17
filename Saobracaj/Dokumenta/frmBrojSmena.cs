using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using MetroFramework.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace Saobracaj.Dokumenta
{
    public partial class frmBrojSmena : Form
    {
        DataTable dt = new DataTable();
        public frmBrojSmena()
        {
            InitializeComponent();
        }

        private void frmBrojSmena_Load(object sender, EventArgs e)
        {

            var select3 = " select DmSifra, Rtrim(DmNaziv) as DmNaziv from DelovnaMesta";
            var s_connection3 = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
            cboRadnaMesta.DataSource = ds3.Tables[0];
            cboRadnaMesta.DisplayMember = "DmNaziv";
            cboRadnaMesta.ValueMember = "DmSifra";



        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string x = "";
                string y = "";
                string z = "";

                if (cboX.SelectedItem != null)
                    x = cboX.SelectedItem.ToString();
                if (cboY.SelectedItem != null)
                    y = cboY.SelectedItem.ToString();
                if (cboZ.SelectedItem != null)
                    z = cboZ.SelectedItem.ToString();
                
                DataTable newDt = new DataTable();
                if (y != "" && z != "")
                    newDt = PivotTable.GetInversedDataTable(dt, x, y, z, txttNullValue.Text, chkSumValues.Checked);
                else
                    newDt = PivotTable.GetInversedDataTable(dt, x, y);

                newDt.Columns.Add("SUMA");

                double sum = 0;
                int Zadnja = 0;

              

                    dataGridView2.DataSource = newDt;
                for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                {
                    sum = 0;
                    for (int j = 0; j < dataGridView2.ColumnCount-1; ++j)
                    {
                        if (j> 0)
                        {
                            string pom = "";
                            pom = dataGridView2.Rows[i].Cells[j].Value.ToString();
                            if (pom != "")
                            { 
                            double pomd = Convert.ToDouble(pom);
                            sum +=  pomd;
                            }
                            Zadnja = j;
                        }
                    }


                   
                        dataGridView2.Rows[i].Cells[Zadnja+1].Value = sum;

                    

                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            var select = "select (Rtrim(Delavci.DePriimek) + ' ' + Rtrim(Delavci.DeIme)) as Zaposleni, CONVERT(char(10), VremeOd,104) as VremeOd," +
                " (Select " +
" CASE " +
"    WHEN Sum(Aktivnosti.Ukupno) < 7 and Sum(Aktivnosti.Ukupno) > 0 THEN 0.5 " +
"    WHEN Sum(Aktivnosti.Ukupno) > 7 and Sum(Aktivnosti.Ukupno) < 17 THEN 1 " +
 "   WHEN Sum(Aktivnosti.Ukupno) > 17  THEN 0 " +
"  END) as Smena  " +
                "  from Aktivnosti " +
            " inner join Delavci on Aktivnosti.Zaposleni = Delavci.DeSifra " +
            " inner join DelovnaMesta on Delavci.DeSifDelMes = DelovnaMesta.DmSifra " +
            " where Aktivnosti.VremeOd >= '" + dtpVremeOd.Text + "' and Aktivnosti.VremeOd < '" + dtpVremeDo.Text + "' and DelovnaMesta.DmSifra = " + Convert.ToInt32(cboRadnaMesta.SelectedValue) +
            " group by (Rtrim(Delavci.DePriimek) + ' ' + Rtrim(Delavci.DeIme)), VremeOd";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dt = ds.Tables[0];

            foreach (DataColumn dc in dt.Columns)
                cboX.Items.Add(dc.ColumnName);
            foreach (DataColumn dc in dt.Columns)
                cboY.Items.Add(dc.ColumnName);
            foreach (DataColumn dc in dt.Columns)
                cboZ.Items.Add(dc.ColumnName);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < dataGridView2.ColumnCount - 1; ++j)
            {
                if (j!=0)
                { 
                dataGridView2.Columns[j].Visible = false;
                }
                // dataGridView2
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < dataGridView2.ColumnCount - 1; ++j)
            {
                if (j != 0)
                {
                    dataGridView2.Columns[j].Visible = true;
                }
                // dataGridView2
            }
        }
    }
}
