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
    public partial class frmValute : Form
    {
        public static string code = "frmValute";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        string niz = "";

        bool status = false;
        public frmValute()
        {
            InitializeComponent();

        }
        
        private void frmValute_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            var select = " SELECT VaSifra,VaNaziv FROM  Valute";



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
          //  txtSifra.Enabled = false;
            txtNaziv.Text = "";
        }

        private void tsSave_Click(object sender, EventArgs e)
        {

            if (status == true)
            {
                InsertValute ins = new InsertValute();
                ins.InsValute(txtSifra.Text, txtNaziv.Text);
            }
            else
            {
                InsertValute upd = new InsertValute();
                upd.UpdValute(txtSifra.Text, txtNaziv.Text);
            }
            RefreshDataGrid();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertValute del = new InsertValute();
            del.DelValute(txtSifra.Text);
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
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        txtNaziv.Text = row.Cells[1].Value.ToString();

                    }
                }
            }
            catch
            {
                MessageBox.Show("Nije uspela selekcija stavki");
            }
        }
    }
}
