﻿using Saobracaj.Sifarnici;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class Carinarnice : Syncfusion.Windows.Forms.Office2010Form
    {
        string Kor = Saobracaj.Sifarnici.frmLogovanje.user.ToString();
        bool status = false;
        string connection = frmLogovanje.connectionString;
        public Carinarnice()
        {
            InitializeComponent();
            FillGV();

        }

        private void FillGV()
        {
            var select = "Select * From Carinarnice order by ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 150;



        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            tsNew.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertCarinarnice ins = new InsertCarinarnice();
            if (status == true)
            {
                ins.InsCarinarnice(txtNaziv.Text.ToString().TrimEnd(), txtCINaziv.Text, txtCIOznaka.Text, txtCIEmail.Text, txtCITelefon.Text);
            }
            else
            {
                ins.UpdCarinarnice(Convert.ToInt32(txtID.Text.ToString()), txtNaziv.Text.ToString().TrimEnd(), txtCINaziv.Text, txtCIOznaka.Text, txtCIEmail.Text, txtCITelefon.Text);
            }
            FillGV();
            tsNew.Enabled = true;
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertCarinarnice ins = new InsertCarinarnice();
            ins.DelCarinarnice(Convert.ToInt32(txtID.Text.ToString()));
            FillGV();
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
                        txtCINaziv.Text = row.Cells[2].Value.ToString();
                        txtCIOznaka.Text = row.Cells[3].Value.ToString();
                        txtCIEmail.Text = row.Cells[4].Value.ToString();
                        txtCITelefon.Text = row.Cells[5].Value.ToString();
                    }
                }
            }
            catch { }
        }

        private void Carinarnice_Load(object sender, EventArgs e)
        {

        }
    }
}
