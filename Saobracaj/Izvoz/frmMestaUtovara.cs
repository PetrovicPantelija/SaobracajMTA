﻿using System;
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

namespace Saobracaj.Izvoz
{
    public partial class frmMestaUtovara : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        public static string code = "frmMestaUtovara";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        public frmMestaUtovara()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");

        }
       
        private void frmMestaUtovara_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT ID,Naziv FROM  MestaUtovara";



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Šifra";
            dataGridView1.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 150;



        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtID.Enabled = false;
            txtNaziv.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (status == true)
            {
                InsertMestaUtovara ins = new InsertMestaUtovara();
                ins.InsMestaUtovara(txtNaziv.Text);
                status = false;
            }
            else
            {
                InsertMestaUtovara upd = new InsertMestaUtovara();
                upd.UpdMestaUtovara(Convert.ToInt32(txtID.Text), txtNaziv.Text);

            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertMestaUtovara del = new InsertMestaUtovara();
            del.DelMestaUtovara(Convert.ToInt32(txtID.Text));
            RefreshDataGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                        txtNaziv.Text = row.Cells[1].Value.ToString();

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmKontaktOsobeMU ko = new frmKontaktOsobeMU(Convert.ToInt32(txtID.Text));
            ko.Show();
        }
    }
}
